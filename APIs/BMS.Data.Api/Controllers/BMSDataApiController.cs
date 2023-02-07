using BMS.Data.Api.Utilities;
using BMS.Sql.Library;
using BMS.Sql.Library.Models;
using BMS.Sql.Library.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BMS.Data.Api.Controllers
{
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public class BMSDataApiController : BMSDataApiControllerBase
    {
        public BMSDbContext BMSDbContext { get; set; }
        public ILogger<BMSDataApiController> Log { get; set; }
        public MqttDataService MqttDataService { get; set; }
        public CommandDataService CommandDataService { get; set; }
        private readonly LogService LogService;
        private readonly IConfiguration Configuration;

        public BMSDataApiController(
            BMSDbContext bmsDbContext,
            ILogger<BMSDataApiController> log,
            MqttDataService mqttDataService,
            CommandDataService commandDataService,
            IConfiguration configuration)
        {
            BMSDbContext = bmsDbContext;
            Log = log;
            MqttDataService = mqttDataService;
            CommandDataService = commandDataService;
            Configuration = configuration;
            LogService = new LogService(bmsDbContext);
        }

        public override async Task<IActionResult> DataPush()
        {
            string body = null;
            try
            {
                // Get the request body as string
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    body = await reader.ReadToEndAsync();
                }
                Log.LogInformation($"Request: {body}");

                // Process body
                MqttDataService.ProcessData(body, Request);

                return Ok();
            }
            catch (Exception ex)
            {
                Log.LogError(ex.ToString());
                ErrorParse(ex.ToString(), body);
                return new StatusCodeResult(500);
            }
        }

        public override async Task<ActionResult<ICollection<CommandPush>>> GetCommands()
        {
            try
            {
                if (Request.Headers.TryGetValue("MasterUid", out var masterUid))
                {
                    // push commands
                    List<CommandPush> commandPushes = CommandDataService.GetCommands(masterUid);

                    return Content(JsonConvert.SerializeObject(commandPushes), "application/json");
                }

                throw new Exception("Bad MasterUid.");
            }
            catch (Exception ex)
            {
                Log.LogError(ex.ToString());
                return new StatusCodeResult(500);
            }
        }

        public override async Task<ActionResult<ICollection<CommandProcessedResponse>>> CommandProcessed([FromBody] IEnumerable<CommandProcessedRequest> body)
        {
            try
            {
                if (Request.Headers.TryGetValue("MasterUid", out var masterUid))
                {
                    // commands processed
                    List<CommandProcessedResponse> response = CommandDataService.CommandsProcessed(masterUid, body.ToList());
                    return response.Count == 0 ? response : StatusCode(400, response);
                }

                throw new Exception("Bad MasterUid.");
            }
            catch (Exception ex)
            {
                Log.LogError(ex.ToString());
                return new StatusCodeResult(500);
            }
        }

        private void ErrorParse(string error, string? jsonData)
        {
            try
            {
                string saveMessageError = Configuration["messageError"];
                if (saveMessageError.ToLower() == "true")
                {
                    Log newLog = new Log();
                    newLog.ErrorMessage = error;
                    newLog.JsonData = jsonData != null ? jsonData : "";

                    if (Request.HttpContext.Connection.RemoteIpAddress != null)
                    {
                        newLog.IpRequest = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                        if (Request.HttpContext.Connection.RemotePort != null)
                            newLog.IpRequest += ":" + Request.HttpContext.Connection.RemotePort.ToString();
                    }

                    newLog.Status = 500;
                    newLog.CreatedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

                    LogService.Save(newLog);
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex.ToString());
            }
        }
    }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
}
