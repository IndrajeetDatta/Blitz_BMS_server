using BMS.Sql.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMS.Sql.Library.Utils;
using Newtonsoft.Json.Linq;

namespace BMS.Sql.Library.Services
{
    public class CommandService : ModelServiceBase
    {
        public CommandService(BMSDbContext bmsDbContext) : base(bmsDbContext) { }

        public Command Get(int id)
        {
            return BMSDbContext.Commands.SingleOrDefault(x => x.Id == id);
        }
        public List<Command> GetByMasterUid(string masterId, DateTimeOffset? lastCreatedDate = null)
        {
            if (lastCreatedDate != null)
                return BMSDbContext.Commands.Where(x => x.MasterId == masterId && x.CreatedDate > lastCreatedDate).ToList();
            return BMSDbContext.Commands.Where(x => x.MasterId == masterId).ToList();
        }

        public Command Save(Command command)
        {
            try
            {
                if (command == null || command.Id > 1) return new Command();

                Command savedCommand = BMSDbContext.Commands.Add(command).Entity;
                BMSDbContext.SaveChanges();

                return savedCommand;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Command();
            }
        }

        public List<Command> GetCommandsByIds(List<int> commandsIds)
        {
            return BMSDbContext.Commands.Where(x => commandsIds.Contains(x.Id)).OrderBy(x => x.CreatedDate).ToList();
        }
        public List<Command> GetCommandsByStatus(string? masterId, List<CommandStatus> commandStatuses)
        {
            if (masterId == null || masterId == string.Empty)
                return BMSDbContext.Commands.Where(x => x.Status != null && commandStatuses.Contains((CommandStatus)x.Status)).ToList();

            return BMSDbContext.Commands.Where(x => x.MasterId == masterId && x.Status != null && commandStatuses.Contains((CommandStatus)x.Status)).ToList();
        }
        public void UpdateCommands(List<Command> commands)
        {
            BMSDbContext.UpdateRange(commands);
            BMSDbContext.SaveChanges();
        }
        public void TimeoutPendingCommands(int timeoutPendingCommandsInMinutes, int timeoutProcessingCommandsInMinutes)
        {
            try
            {
                bool needUpdate = false;
                BMSDbContext.ChangeTracker.Clear();
                List<Command> pendingAndprocessingCommands = GetCommandsByStatus(null, new List<CommandStatus> { CommandStatus.Pending, CommandStatus.Processing});

                pendingAndprocessingCommands.ForEach(command =>
                {
                    int timeout = timeoutPendingCommandsInMinutes;
                    if (command.Status == CommandStatus.Processing)
                        timeout = timeoutProcessingCommandsInMinutes;

                    if (command.CreatedDate != null && DateTime.UtcNow > command.CreatedDate.Value.DateTime.ToUniversalTime().AddMinutes(timeout))
                    {
                        command.Status = CommandStatus.Timeout;
                        BMSDbContext.Commands.Update(command);
                        needUpdate = true;
                    }
                });

                if (needUpdate) BMSDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.ToString());
            }
        }
    }
}