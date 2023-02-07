using BMS.Sql.Library;
using BMS.Sql.Library.Models;

namespace BMS.Sql.Library.Services
{
    public class OcppStatusService : ModelServiceBase
    {
        public OcppStatusService(BMSDbContext bmsDbContext) : base(bmsDbContext) { }

        public OCPPStatus Get(int id)
        {
            return BMSDbContext.OCPPStatus.SingleOrDefault(x => x.Id == id);
        }
        public List<OCPPStatus> GetByChargeControllerID(int chargeControllerId)
        {
            return BMSDbContext.OCPPStatus.Where(x => x.ChargeController.Id == chargeControllerId).ToList();
        }
    }
}
