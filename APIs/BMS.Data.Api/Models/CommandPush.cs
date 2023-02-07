using BMS.Sql.Library.Models;

namespace BMS.Data.Api.Controllers
{
    public partial class CommandPush
    {
        public CommandPush() { }

        public CommandPush(Command command)
        {
            Url = command.MasterUrl;
            Name = command.Name;
            Method = command.Method;
            Payload = Newtonsoft.Json.JsonConvert.DeserializeObject(command.Payload);
            ControllerId = command.ChargeControllerUid;
            CreatedDate = command.CreatedDate;
            CommandId = command.Id;
            Port = command.Port ?? 0;
            TokenRequired = command.TokenRequired ?? false;
        }
    }
}
