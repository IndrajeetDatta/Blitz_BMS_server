using BMS.Sql.Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BMS.Sql.Library.Services
{
    public class OcppMessageService : ModelServiceBase
    {
        public OcppMessageService(BMSDbContext bmsDbContext) : base(bmsDbContext) { }

        public OCPPMessage Get(int id)
        {
            return BMSDbContext.OCPPMessage.SingleOrDefault(x => x.Id == id);
        }
        public List<OCPPMessage> GetByChargeControllerID(int chargeControllerId, bool getLastHeartbeat)
        {
            if (getLastHeartbeat == true)
                return BMSDbContext.OCPPMessage.Where(x => x.ChargeController.Id == chargeControllerId && x.FromLastHeartbeat == true).ToList();

            return BMSDbContext.OCPPMessage.Where(x => x.ChargeController.Id == chargeControllerId).ToList();
        }

        public void DisableLastHeartbeat(int chargeControllerId) {
            if (chargeControllerId > 0)
            {
                try
                {
                    List<OCPPMessage> oCPPMessages = BMSDbContext
                        .OCPPMessage
                        .Where(x => x.ChargeController.Id == chargeControllerId && x.FromLastHeartbeat == true)
                        .ToList();
                    
                    oCPPMessages.ForEach(oCPPMessage => oCPPMessage.FromLastHeartbeat = false);

                    BMSDbContext.OCPPMessage.UpdateRange(oCPPMessages);
                    BMSDbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public void DeleteOcppMessages(int deleteOCPPMessagesInHours) {
            try
            {
                BMSDbContext.ChangeTracker.Clear();
                List<OCPPMessage> oCPPMessages = BMSDbContext.OCPPMessage.ToList();
                List<OCPPMessage> removeOCPPMessages = new List<OCPPMessage>();

                oCPPMessages.ForEach(oCPPMessage =>
                {
                    if (oCPPMessage.Timestamp != null && 
                        DateTime.UtcNow > oCPPMessage.Timestamp.Value.DateTime.ToUniversalTime().AddHours(deleteOCPPMessagesInHours)
                    )
                    {
                        oCPPMessage.ChargeController = null;
                        removeOCPPMessages.Add(oCPPMessage);
                    }
                });

                BMSDbContext.OCPPMessage.RemoveRange(removeOCPPMessages);
                if (removeOCPPMessages.Count > 0) BMSDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.ToString());
            }
        }
    }
}