namespace BMS.Web.Api.Models
{
    public class OcppStatus { }
}

namespace BMS.Web.Api.Controllers
{
    public partial class OcppStatus
    {
        public OcppStatus() { }

        public OcppStatus(Sql.Library.Models.OCPPStatus? ocppStatus)
        {
            if (ocppStatus == null)
                return;
            Id = ocppStatus.Id;
            if (ocppStatus.ChargeController != null)
                ChargeController = new ChargeController(ocppStatus.ChargeController);
            Device_uid = ocppStatus.Device_uid;
            Status = ocppStatus.Status;
            OccpStatusSentDate = ocppStatus.OccpStatusSentDate;
            OccpStatus = ocppStatus.OccpStatus;
            Operative = ocppStatus.Operative ?? false;
        }
    }
}
