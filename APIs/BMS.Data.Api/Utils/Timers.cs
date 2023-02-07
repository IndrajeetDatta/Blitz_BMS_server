using BMS.Data.Api.Controllers;
using BMS.Sql.Library;
using BMS.Sql.Library.Services;
using System.Timers;

namespace BMS.Data.Api.Utils
{
    public class Timers
    {
        private readonly IConfiguration Configuration;
        private System.Timers.Timer updateNetworkConnectionTimer;
        private readonly ChargeControllerService ChargeControllerService;
        private readonly OcppMessageService OCPPMessageService;
        private readonly CommandService CommandService;
        public readonly ILogger Log;
        
        public Timers(BMSDbContext bMSDbContext, IConfiguration configuration, ILogger logger)
        {
            Log = logger;
            Log.LogInformation($"Timers constructor!!!");

            ChargeControllerService = new ChargeControllerService(bMSDbContext);
            OCPPMessageService = new OcppMessageService(bMSDbContext);
            CommandService = new CommandService(bMSDbContext);
            Configuration = configuration;

            int intervalToCheckNetworkConnectionInSeconds = 0;
            int.TryParse(Configuration["intervalToCheckNetworkConnectionInSeconds"], out intervalToCheckNetworkConnectionInSeconds);

            Log.LogInformation($"In Timers constructor - intervalToCheckNetworkConnectionInSeconds: {intervalToCheckNetworkConnectionInSeconds}s .");

            if (intervalToCheckNetworkConnectionInSeconds > 0)
            {
                updateNetworkConnectionTimer = new System.Timers.Timer(intervalToCheckNetworkConnectionInSeconds * 1000);
                updateNetworkConnectionTimer.Elapsed += updateNetworkConnection;
                updateNetworkConnectionTimer.Enabled = true;
                updateNetworkConnectionTimer.AutoReset = true;
                updateNetworkConnectionTimer.Start();
                Log.LogInformation($"In Timers constructor: call timer.Start()");
            }

            int intervalToCheckOCPPMessagesInHours = 0;
            int.TryParse(Configuration["intervalToCheckOCPPMessagesInHours"], out intervalToCheckOCPPMessagesInHours);

            if (intervalToCheckOCPPMessagesInHours > 0)
            {
                updateNetworkConnectionTimer = new System.Timers.Timer(intervalToCheckOCPPMessagesInHours * 3600 * 1000);
                updateNetworkConnectionTimer.Elapsed += deleteOcppMessages;
                updateNetworkConnectionTimer.Enabled = true;
                updateNetworkConnectionTimer.AutoReset = true;
                updateNetworkConnectionTimer.Start();
            }

            int intervalToCheckPendingOrProcessingCommandsInMinutes = 0;
            int.TryParse(Configuration["intervalToCheckPendingOrProcessingCommandsInMinutes"], out intervalToCheckPendingOrProcessingCommandsInMinutes);

            if (intervalToCheckPendingOrProcessingCommandsInMinutes > 0)
            {
                updateNetworkConnectionTimer = new System.Timers.Timer(intervalToCheckPendingOrProcessingCommandsInMinutes * 60 * 1000);
                updateNetworkConnectionTimer.Elapsed += timeoutPendingCommands;
                updateNetworkConnectionTimer.Enabled = true;
                updateNetworkConnectionTimer.AutoReset = true;
                updateNetworkConnectionTimer.Start();
            }
        }

        private void updateNetworkConnection(object sender, ElapsedEventArgs e)
        {
            Log.LogInformation($"In updateNetworkConnection function");

            int expireNetworkConnectionInSeconds = 0;
            int.TryParse(Configuration["expireNetworkConnectionInSeconds"], out expireNetworkConnectionInSeconds);

            Log.LogInformation($"In updateNetworkConnection function - expireNetworkConnectionInSeconds: {expireNetworkConnectionInSeconds}s .");

            if (expireNetworkConnectionInSeconds > 0)
                ChargeControllerService.UpdateNetworkConnectiontoAll(expireNetworkConnectionInSeconds, Log);
        }

        private void deleteOcppMessages(object sender, ElapsedEventArgs e)
        {
            int deleteOCPPMessagesInHours = 0;
            int.TryParse(Configuration["deleteOCPPMessagesInHours"], out deleteOCPPMessagesInHours);

            if (deleteOCPPMessagesInHours > 0)
                OCPPMessageService.DeleteOcppMessages(deleteOCPPMessagesInHours);
        }

        private void timeoutPendingCommands(object sender, ElapsedEventArgs e)
        {
            int timeoutPendingCommandsInMinutes = 0, timeoutProcessingCommandsInMinutes = 0;
            int.TryParse(Configuration["timeoutPendingCommandsInMinutes"], out timeoutPendingCommandsInMinutes);
            int.TryParse(Configuration["timeoutProcessingCommandsInMinutes"], out timeoutProcessingCommandsInMinutes);

            if (timeoutPendingCommandsInMinutes > 0 && timeoutProcessingCommandsInMinutes > 0)
                CommandService.TimeoutPendingCommands(timeoutPendingCommandsInMinutes, timeoutProcessingCommandsInMinutes);
        }
    }
}
