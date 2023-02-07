using BMS.Data.Api.Controllers;
using BMS.Data.Api.Models;
using BMS.Sql.Library;
using BMS.Sql.Library.Models;
using BMS.Sql.Library.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Timers;
using System.Text.Json;

namespace BMS.Data.Api.Utilities
{
    public class CommandDataService
    {
        public BMSDbContext BMSDbContext { get; set; }
        private readonly ChargeControllerService ChargeControllerService;
        private readonly CommandService CommandService;
        public CommandDataService(BMSDbContext bMSDbContext)
        {
            BMSDbContext = bMSDbContext;
            ChargeControllerService = new ChargeControllerService(bMSDbContext);
            CommandService = new CommandService(BMSDbContext);
        }

        public List<CommandPush> GetCommands(string? masterUid)
        {
            List<CommandPush> commandPushes = new List<CommandPush>();
            List<Command> commands = CommandService.GetCommandsByStatus(masterUid, new List<CommandStatus> { CommandStatus.Pending }).OrderBy(x => x.CreatedDate).ToList();

            foreach (Command command in commands)
                commandPushes.Add(new CommandPush(command));

            return commandPushes;
        }

        public List<CommandProcessedResponse> CommandsProcessed(string? masterUid, List<CommandProcessedRequest> commandProcessedRequests)
        {
            commandProcessedRequests = commandProcessedRequests.DistinctBy(x => x.Id).ToList();
            List<int> commandsIds = commandProcessedRequests.Select(x => x.Id).ToList();

            List<CommandProcessedResponse> commandProcessedResponses = new List<CommandProcessedResponse>();
            List<Command> commands = CommandService.GetCommandsByIds(commandsIds);
            foreach (Command command in commands)
            {
                if (command.MasterId != masterUid)
                {
                    commandProcessedResponses.Add(new CommandProcessedResponse(command.Id, "Command not related to provided masterUId."));
                }
                else
                {
                    CommandProcessedRequest fromRequest = commandProcessedRequests.First(x => x.Id == command.Id);
                    if (fromRequest.Status == "Processing")
                    {
                        command.Status = CommandStatus.Processing;
                    }
                    else if (fromRequest.Status == "Success")
                    {
                        command.ProcessedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                        command.Status = CommandStatus.Successfull;
                    } else
                    {
                        command.ProcessedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                        command.Status = CommandStatus.Failed;
                        command.ErrorMessage = fromRequest.StatusMessage;
                    }
                }

                commandsIds.Remove(command.Id);
            }

            foreach (int commandId in commandsIds)
                commandProcessedResponses.Add(new CommandProcessedResponse(commandId, "Command not found in system."));

            CommandService.UpdateCommands(commands);

            return commandProcessedResponses;
        }
    }
}