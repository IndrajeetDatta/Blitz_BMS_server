namespace BMS.Web.Api.Controllers
{
    public partial class CommandHistory
    {
        public CommandHistory() { }

        public CommandHistory(Sql.Library.Models.Command command)
        {
            if (command == null)
                return;
            CommandId = command.Id;
            MasterUrl = command.MasterUrl;
            Method = command.Method;
            Payload = command.Payload;
            Name = command.Name;
            ErrorMessage = command.ErrorMessage;
            ProcessedDate = command.ProcessedDate;
            CreatedDate = command.CreatedDate;
            Status = command.Status.ToString();
            ChargeControllerUid = command.ChargeControllerUid;
            ChargePointUid = command.ChargePointUid;
            RfidSerialNumber = command.RfidSerialNumber;
            CommandType = command.CommandType;
            Port = command.Port == null ? 0 : (int)command.Port;
            TokenRequired = command.Port == null ? false : (bool)command.TokenRequired;
        }
    }
}
