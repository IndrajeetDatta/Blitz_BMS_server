using BMS.Sql.Library;
using BMS.Sql.Library.Models;
using Microsoft.EntityFrameworkCore;

namespace BMS.Sql.Library.Services
{
    public class RfidService : ModelServiceBase
    {
        public RfidService(BMSDbContext bmsDbContext) : base(bmsDbContext) { }

        public RFID Get(int id)
        {
            return BMSDbContext.RFID.SingleOrDefault(x => x.Id == id);
        }

        public List<RFID> GetAll()
        {
            return BMSDbContext.RFID.ToList();
        }

        public List<RFID> GetByChargeControllerId(int chargeControllerId)
        {
            return BMSDbContext.RFID.Where(x => x.ChargeController.Id == chargeControllerId).ToList();
        }
        public RFID Update(RFID rfid)
        {
            RFID updatedRFID = BMSDbContext.RFID.Update(rfid).Entity;
            BMSDbContext.SaveChanges();
            return updatedRFID;
        }
    }
}
