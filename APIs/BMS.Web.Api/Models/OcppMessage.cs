namespace BMS.Web.Api.Controllers
{
    public partial class OcppMessage
    {
        public OcppMessage() { }

        public OcppMessage(Sql.Library.Models.OCPPMessage? ocppMessage)
        {
            if (ocppMessage == null)
                return;
            Id = ocppMessage.Id;
            if (ocppMessage.ChargeController != null)
                Ocpp = new ChargeController(ocppMessage.ChargeController);

            Timestamp = ocppMessage.Timestamp;
            Type = ocppMessage.Type ?? -1;
            Action = ocppMessage.Action;
            MessageData = ocppMessage.MessageData;
            FromLastHeartbeat = (bool)(ocppMessage.FromLastHeartbeat != null ? ocppMessage.FromLastHeartbeat : false);
        }
    }
}
