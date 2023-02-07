using BMS.Sql.Library.Models;

namespace BMS.Data.Api.Controllers
{
    public partial class CommandProcessedResponse
    {
        public CommandProcessedResponse() { }

        public CommandProcessedResponse(int id, string? messageError)
        {
            Id = id;
            MessageError = messageError;
        }

        public static bool ExistId(List<CommandProcessedResponse> commandProcessedResponses, int id)
        {
            foreach (CommandProcessedResponse commandProcessedResponse in commandProcessedResponses)
                if (commandProcessedResponse.Id == id)
                    return true;
            return false;
        }
    }
}
