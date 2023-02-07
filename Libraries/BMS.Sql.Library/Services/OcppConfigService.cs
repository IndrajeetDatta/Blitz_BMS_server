using BMS.Sql.Library;
using BMS.Sql.Library.Models;
using Microsoft.EntityFrameworkCore;

namespace BMS.Sql.Library.Services
{
    public class OcppConfigService : ModelServiceBase
    {
        public OcppConfigService(BMSDbContext bmsDbContext) : base(bmsDbContext) { }
        
        public OCPPConfig Get(int id)
        {
            return BMSDbContext.OCPPConfig.SingleOrDefault(x => x.Id == id);
        }
        public OCPPConfig GetByChargeControllerID(int chargeControllerId)
        {
            return BMSDbContext.OCPPConfig.Include(x => x.ChargeController).SingleOrDefault(x => x.ChargeController.Id == chargeControllerId);
        }
        public OCPPConfig Update(OCPPConfig ocppConfig)
        {
            OCPPConfig updatedOCPPConfig = BMSDbContext.OCPPConfig.Update(ocppConfig).Entity;
            BMSDbContext.SaveChanges();
            return updatedOCPPConfig;
        }
    }
}
