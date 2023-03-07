using BMS.Sql.Library;
using Microsoft.AspNetCore.Mvc;
using BMS.Sql.Library.Utils;
using BMS.Sql.Library.Services;
using BMS.Web.Api.Authentication;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using BMS.Sql.Library.Models;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Newtonsoft.Json.Linq;

namespace BMS.Web.Api.Controllers
{
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public class BMSWebApiController : BMSWebApiControllerBase
    {
        public BMSDbContext BMSDbContext { get; set; }
        private readonly ApplicationUserService ApplicationUserService;
        private readonly AuthenticationService AuthenticationService;
        private readonly ChargeControllerService ChargeControllerService;
        private readonly ChargePointService ChargePointService;
        private readonly NetWorkService NetWorkService;
        private readonly RfidService RfidService;
        private readonly OcppConfigService OcppConfigService;
        private readonly OcppStatusService OcppStatusService;
        private readonly OcppMessageService OcppMessageService;
        private readonly CommandService CommandService;
        private readonly EmailService EmailService;
        private readonly IConfiguration Configuration;

        public BMSWebApiController(
            BMSDbContext bmsDbContext,
            IdentityUserService identityUserService,
            AuthenticationService authenticationService,
            IConfiguration configuration
        )
        {
            BMSDbContext = bmsDbContext;
            AuthenticationService = authenticationService;
            ApplicationUserService = new ApplicationUserService(bmsDbContext, identityUserService);
            ChargeControllerService = new ChargeControllerService(bmsDbContext);
            ChargePointService = new ChargePointService(bmsDbContext);
            NetWorkService = new NetWorkService(bmsDbContext);
            RfidService = new RfidService(bmsDbContext);
            OcppConfigService = new OcppConfigService(bmsDbContext);
            OcppMessageService = new OcppMessageService(bmsDbContext);
            OcppStatusService = new OcppStatusService(bmsDbContext);
            CommandService = new CommandService(bmsDbContext);
            EmailService = new EmailService(bmsDbContext);
            Configuration = configuration;
        }

        [AllowAnonymous]
        public override async Task<ActionResult<AuthenticationResponse>> AuthenticationBasic([FromBody] AuthenticationBasicRequest body)
        {
            try
            {
                if (body.Email.Length == 0 || body.ExternalId.Length == 0)
                    throw new Exception("Bad Email or External Id");
                return await AuthenticationService.AuthenticateBasic(body);
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }

        public override async Task<ActionResult<ICollection<ApplicationUser>>> GetUsers()
        {
            return BMSDbContext.ApplicationUsers.Select(x => new ApplicationUser(x)).ToList();
        }

        public override async Task<ActionResult<ApplicationUser>> CreateUsers([FromBody] ApplicationUser body)
        {
            var dbUser = BMSDbContext.ApplicationUsers.Add(body.ToSqlModel()).Entity;
            BMSDbContext.SaveChanges();
            return new ApplicationUser(dbUser);
        }

        public override async Task<ActionResult<ApplicationUser>> UpdateUsers(
            [FromBody] ApplicationUserUpdateRequest body, 
            [FromHeader] string userEmail, 
            [FromHeader] string externalId
        )
        {
            try
            {
                Sql.Library.Models.ApplicationUser? user = ApplicationUserService.Get(userEmail, externalId);
                if (user == null || user.Id < 1) return new StatusCodeResult(401);

                if (body.FirstName != null && body.FirstName != string.Empty && body.LastName != null && body.LastName != string.Empty)
                {
                    user.FirstName = body.FirstName;
                    user.LastName = body.LastName;
                }
                if (body.NewMasterUid != null && body.NewMasterUid != string.Empty)
                {
                    if (body.NewMasterUid.Contains(',') || body.NewMasterUid.Contains(';'))
                    {
                        string errorUids = "";
                        foreach (string uid in body.NewMasterUid.Split(',', ';'))
                        {
                            string serialNumber = uid.Trim();
                            BMS.Sql.Library.Models.ChargeController? chargeController = ChargeControllerService.GetBySerialNumber(serialNumber);
                            if (chargeController == null)
                            {
                                errorUids += uid.Trim() + ", ";
                            }
                            else
                            {
                                ApplicationUserService.AddChargeController(chargeController, user);
                            }
                        }
                        if(errorUids != string.Empty)
                            return new StatusCodeResult(403);
                    }
                    else
                    {
                        BMS.Sql.Library.Models.ChargeController? chargeController = ChargeControllerService.GetBySerialNumber(body.NewMasterUid);
                        if (chargeController == null) return new StatusCodeResult(403);
                        ApplicationUserService.AddChargeController(chargeController, user);
                    }
                }


                Sql.Library.Models.ApplicationUser updatedApplicationUser = ApplicationUserService.Update(user);
                return new ApplicationUser(updatedApplicationUser);
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }

        public async override Task<ActionResult<ChargeController>> GetChargeStationConfiguration([FromHeader] string userEmail, [FromHeader] string externalId, int id)
        {
            try
            {
                Sql.Library.Models.ApplicationUser? user = ApplicationUserService.Get(userEmail, externalId);
                if (user == null || user.Id < 1) return new StatusCodeResult(401);

                Sql.Library.Models.ChargeController? chargingStation = ChargeControllerService.GetWithReferences(id, user);
                if (chargingStation == null) return new StatusCodeResult(403);

                chargingStation.Networks = NetWorkService.SortNetworks(chargingStation.Networks);
                chargingStation.ChargePoints.ForEach(chargePoint => chargePoint.ChargeController = null);

                return new ChargeController(chargingStation);
            } 
            catch
            {
                return new StatusCodeResult(500);
            }
        }
        
        public async override Task<ActionResult<ChargeController>> GetChargeStationWhitelist([FromHeader] string userEmail, [FromHeader] string externalId, int id)
        {
            try
            {
                Sql.Library.Models.ApplicationUser? user = ApplicationUserService.Get(userEmail, externalId);
                if (user == null || user.Id < 1) return new StatusCodeResult(401);

                Sql.Library.Models.ChargeController? chargingStation = ChargeControllerService.GetWithWhitelist(id, user);
                if (chargingStation == null) return new StatusCodeResult(403);

                return new ChargeController(chargingStation);
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }
        
        public async override Task<ActionResult<ChargeController>> GetChargeStationTransaction(
            [FromHeader] string userEmail, 
            [FromHeader] bool getAllTransactions, 
            [FromHeader] string externalId,
            int id
        )
        {
            try
            {
                Sql.Library.Models.ApplicationUser? user = ApplicationUserService.Get(userEmail, externalId);
                if (user == null || user.Id < 1) return new StatusCodeResult(401);

                Sql.Library.Models.ChargeController? chargingStation;
                if (!getAllTransactions)
                {
                    int lastCreatedDateTransactionsInDays = 0;
                    int.TryParse(Configuration["lastCreatedDateTransactionsInDays"], out lastCreatedDateTransactionsInDays);
                    DateTimeOffset lastCreatedDate = DateTime.SpecifyKind(DateTime.Now.AddDays(-lastCreatedDateTransactionsInDays), DateTimeKind.Utc);
                    chargingStation = ChargeControllerService.GetWithTransaction(id, lastCreatedDate, user);
                }
                else
                {
                    chargingStation = ChargeControllerService.GetWithTransaction(id, null, user);
                }

                if (chargingStation == null) return new StatusCodeResult(403);
                chargingStation.Installer = new Sql.Library.Models.ApplicationUser();
                return new ChargeController(chargingStation);
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }
        
        public async override Task<ActionResult<ChargeController>> UpdateChargeStationConfigurationId(
            [FromBody] ChargeController body, 
            [FromHeader] string userEmail, 
            [FromHeader] string externalId, 
            int id
        )
        {
            try
            {
                Sql.Library.Models.ApplicationUser? user = ApplicationUserService.Get(userEmail, externalId);
                if (user == null || user.Id < 1) return new StatusCodeResult(401);

                Sql.Library.Models.ChargeController? chargeController = ChargeControllerService.Get(id, user);
                if (chargeController == null) return new ChargeController();

                Sql.Library.Models.ApplicationUser? installer = BMSDbContext.ApplicationUsers.Where(x => x.Id == body.Installer.Id).FirstOrDefault();

                chargeController.ETH0DHCP = body.Eth0DHCP;
                chargeController.ETH0IPAddress = body.Eth0IPAddress;
                chargeController.ETH0SubnetMask = body.Eth0SubnetMask;
                chargeController.ETH0Gateway = body.Eth0Gateway;
                chargeController.ModemServiceActive = body.ModemServiceActive;
                chargeController.ModemSimPin = body.ModemSimPin;
                chargeController.ModemAPN = body.ModemAPN;
                chargeController.ModemUsername = body.ModemUsername;
                chargeController.ModemPassword = body.ModemPassword;
                chargeController.ModemDefaultRoute = body.ModemDefaultRoute;
                chargeController.ModemPreferOverETH0 = body.ModemPreferOverETH0;
                if (user.Role == UserRoleEnum.Admin)
                    chargeController.AllowTestModeCommands = body.AllowTestModeCommands;
                chargeController.Installer = installer;
                if (body.LastMaintenance != null && DateTime.UtcNow > body.LastMaintenance)
                    chargeController.LastMaintenance = body.LastMaintenance;

                Sql.Library.Models.ChargeController updatedChargingStation = ChargeControllerService.Update(chargeController);

                return new ChargeController(updatedChargingStation);
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }
        
        public async override Task<ActionResult<ICollection<ChargePoint>>> GetChargeStationsOverview([FromHeader] string userEmail, [FromHeader] string externalId)
        {
            try
            {
                Sql.Library.Models.ApplicationUser? user = ApplicationUserService.Get(userEmail, externalId);
                if (user == null || user.Id < 1) return new StatusCodeResult(401);

                List<ChargePoint> chargePoints = new List<ChargePoint>();

                ChargePointService.GetAllWithChargeController(user).ForEach(chargePoint =>
                {
                    chargePoint.ChargeController.ChargePoints = null;
                    chargePoints.Add(new ChargePoint(chargePoint));
                });

                return chargePoints;
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }
        
        public async override Task<ActionResult<ChargePoint>> GetChargeStationChargePoint([FromHeader] string userEmail, [FromHeader] string externalId, int id)
        {
            try
            {
                Sql.Library.Models.ApplicationUser? user = ApplicationUserService.Get(userEmail, externalId);
                if (user == null || user.Id < 1) return new StatusCodeResult(401);

                Sql.Library.Models.ChargePoint? chargePoint = ChargePointService.GetWithChargeController(id, user);
                if (chargePoint == null) return new StatusCodeResult(403);

                chargePoint.ChargeController.ChargePoints = null;
                return new ChargePoint(chargePoint);
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }
        
        public async override Task<ActionResult<ChargePoint>> UpdateChargeStationChargePointId([FromBody] ChargePoint body, int id)
        {
            try
            {
                return body;
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }
        
        public async override Task<ActionResult<Response>> GetChargeStationOcpp([FromHeader] string userEmail, [FromHeader] string externalId, int chargeControllerId)
        {
            try
            {
                Sql.Library.Models.ApplicationUser? user = ApplicationUserService.Get(userEmail, externalId);
                if (user == null || user.Id < 1) return new StatusCodeResult(401);

                Sql.Library.Models.ChargeController? chargingStation = ChargeControllerService.Get(chargeControllerId, user);
                if (chargingStation == null) return new StatusCodeResult(403);

                List<OcppMessage> ocppMessageList = new List<OcppMessage>();
                List<OcppStatus> ocppStatusList = new List<OcppStatus>();
                OcppConfig ocppConfig;

                OCPPConfig ocppConfigModel = OcppConfigService.GetByChargeControllerID(chargingStation.Id);
                if (ocppConfigModel != null && ocppConfigModel.ChargeController != null)
                    ocppConfigModel.ChargeController.OCPPConfig = null;
                ocppConfig = new OcppConfig(ocppConfigModel);

                OcppStatusService.GetByChargeControllerID(chargingStation.Id).ForEach(ocppStatus =>
                {
                    if (ocppStatus != null && ocppStatus.ChargeController != null)
                        ocppStatus.ChargeController.oCPPStatus = null;
                    ocppStatusList.Add(new OcppStatus(ocppStatus));
                });

                OcppMessageService.GetByChargeControllerID(chargingStation.Id, true).ForEach(ocppMessage =>
                {
                    if (ocppMessage != null && ocppMessage.ChargeController != null)
                        ocppMessage.ChargeController.oCPPMessages = null;
                    ocppMessageList.Add(new OcppMessage(ocppMessage));
                });

                Response response = new Response();
                response.OcppMessages = ocppMessageList;
                response.OcppStatus = ocppStatusList;
                response.OcppConfig = ocppConfig;

                return response;
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }
        
        public async override Task<ActionResult<OcppConfig>> UpdateChargeStationOcppConfig([FromBody] OcppConfig body, int id)
        {
            try
            {
                OCPPConfig oCPPConfig = OcppConfigService.GetByChargeControllerID((int)id);
                if (oCPPConfig == null)
                    return new OcppConfig();

                oCPPConfig.OcppProtocolVersion = body.OcppProtocolVersion;
                oCPPConfig.NetworkInterface = body.NetworkInterface;
                oCPPConfig.BackendURL = body.BackendURL;
                oCPPConfig.ServiceRFID = body.ServiceRFID;
                oCPPConfig.FreeModeRFID = body.FreeModeRFID;
                oCPPConfig.ChargeStationModel = body.ChargeStationModel;
                oCPPConfig.ChargeStationVendor = body.ChargeStationVendor;
                oCPPConfig.ChargeStationSerialNumber = body.ChargeStationSerialNumber;

                OCPPConfig updatedOcppConfig = OcppConfigService.Update(oCPPConfig);
                if (updatedOcppConfig != null && updatedOcppConfig.ChargeController != null)
                    updatedOcppConfig.ChargeController.OCPPConfig = null;

                return new OcppConfig(updatedOcppConfig);
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }
        
        public async override Task<ActionResult<ICollection<OcppMessage>>> GetOcppMessages(
            [FromHeader] string userEmail,
            [FromHeader] string externalId,
            int chargeControllerId
        )
        {
            try
            {
                Sql.Library.Models.ApplicationUser? user = ApplicationUserService.Get(userEmail, externalId);
                if (user == null || user.Id < 1) return new StatusCodeResult(401);

                Sql.Library.Models.ChargeController? chargingStation = ChargeControllerService.Get(chargeControllerId, user);
                if (chargingStation == null) return new StatusCodeResult(403);

                List<OcppMessage> ocppMessageList = new List<OcppMessage>();

                OcppMessageService.GetByChargeControllerID(chargingStation.Id, false).ForEach(ocppMessage =>
                {
                    ocppMessage.ChargeController = null;
                    ocppMessageList.Add(new OcppMessage(ocppMessage));
                });

                return ocppMessageList;
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }
        public async override Task<ActionResult<Response2>> GetChargeStationCommandHistory(
            [FromHeader] string userEmail,
            [FromHeader] string externalId,
            string masterId,
            int chargeControllerId)
        {
            try
            {
                Sql.Library.Models.ApplicationUser? user = ApplicationUserService.Get(userEmail,externalId);
                if (user == null || user.Id < 1) return new StatusCodeResult(401);

                List<CommandHistory> commandHistoryList = new List<CommandHistory>();
                Sql.Library.Models.ChargeController? chargeController = ChargeControllerService.Get(chargeControllerId, user);
                if (chargeController == null) return new StatusCodeResult(403);

                int lastCreatedDateCommandsHistoryInDays = 0;
                int.TryParse(Configuration["lastCreatedDateCommandsHistoryInDays"], out lastCreatedDateCommandsHistoryInDays);
                DateTimeOffset lastCreatedDate = DateTime.SpecifyKind(DateTime.Now.AddDays(-lastCreatedDateCommandsHistoryInDays), DateTimeKind.Utc);
                List<Sql.Library.Models.Command> commands = CommandService.GetByMasterUid(masterId, lastCreatedDate);

                foreach (Sql.Library.Models.Command command in commands)
                    commandHistoryList.Add(new CommandHistory(command));

                Response2 response2 = new Response2();
                response2.ChargeController = new ChargeController(chargeController);
                response2.CommandsHistoryList= commandHistoryList;

                return response2;
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }
        
        public async override Task<ActionResult<int>> PostCommand([FromBody] Command body, [FromHeader] string userEmail, [FromHeader] string externalId)
        {
            try
            {
                Sql.Library.Models.ApplicationUser? user = ApplicationUserService.Get(userEmail, externalId);
                if (user == null || user.Id < 1) return new StatusCodeResult(401);

                List<Sql.Library.Models.Command> pendingCommands = new List<Sql.Library.Models.Command>();
                string enableCommandsTestMode = Configuration["enableCommandsTestMode"];

                Sql.Library.Models.ChargeController? chargeController = null;
                Sql.Library.Models.ChargePoint? chargePoint = ChargePointService.GetWithChargeController(body.ChargePointId, user);
                if (chargePoint == null)
                    chargeController = ChargeControllerService.Get(body.ChargeControllerId, user);

                if (chargePoint != null)
                {
                    // procces commands only with "allowTestModeCommands" == true
                    if (enableCommandsTestMode.ToLower() == "true" && chargePoint.ChargeController.AllowTestModeCommands != true)
                        return new StatusCodeResult(401);

                    pendingCommands = CommandService.GetCommandsByStatus(
                        chargePoint.ChargeController.SerialNumber,
                        new List<CommandStatus> { CommandStatus.Pending, CommandStatus.Processing }
                    );
                    if (pendingCommands.Count >= 5)
                        return new StatusCodeResult(413);
                    else if (pendingCommands.Find(command => Command.IsRebootCommand(command.CommandType)) != null)
                        return new StatusCodeResult(412);

                    if (body.Type == CommandType.SaveChargingPoint)
                    {
                        body.Payload = JSONParse.ConvertToHeartbeatField(typeof(BMS.Sql.Library.Models.ChargePoint), body.Payload);

                        Sql.Library.Models.Command command = new Sql.Library.Models.Command(
                            chargePoint.ChargeController.SerialNumber,
                            chargePoint.ChargePointUid,
                            chargePoint.ChargeControllerUid,
                            null,
                            body.Type.ToString(),
                            "edit-cp",
                            "PUT",
                            $"/api/v1.0/charging-points/{chargePoint.ChargePointUid}/config",
                            body.Payload,
                            5555,
                            false
                        );

                        CommandService.Save(command);
                    }
                    else if (body.Type == CommandType.EnableDisableChargePoint)
                    {
                        string[] fieldsToModify = {nameof(chargePoint.ExternalRelease).ToLower()};

                        body.Payload = JSONParse.ConvertToHeartbeatField(typeof(BMS.Sql.Library.Models.ChargePoint), body.Payload, fieldsToModify.ToList());

                        Sql.Library.Models.Command command = new Sql.Library.Models.Command(
                            chargePoint.ChargeController.SerialNumber,
                            chargePoint.ChargePointUid,
                            chargePoint.ChargeControllerUid,
                            null,
                            body.Type.ToString(),
                            "enable-disable-cp",
                            "PUT",
                            $"/api/v1.0/charging-controllers/{chargePoint.ChargeControllerUid}/control",
                            body.Payload,
                            5555,
                            true
                        );

                        CommandService.Save(command);
                    }
                } 
                else if (chargeController != null) 
                {
                    // procces commands only with "allowTestModeCommands" == true
                    if (enableCommandsTestMode.ToLower() == "true" && chargeController.AllowTestModeCommands != true)
                        return new StatusCodeResult(401);

                    pendingCommands = CommandService.GetCommandsByStatus(
                        chargeController.SerialNumber,
                        new List<CommandStatus> { CommandStatus.Pending, CommandStatus.Processing }
                    );
                    if (pendingCommands.Count >= 5)
                        return new StatusCodeResult(413);
                    else if (pendingCommands.Find(command => Command.IsRebootCommand(command.CommandType)) != null)
                        return new StatusCodeResult(412);

                    if (body.Type == CommandType.RestartApp)
                    {
                        body.Payload = "{\"app\":\"system_reboot\"}";

                        Sql.Library.Models.Command command = new Sql.Library.Models.Command(
                            chargeController.SerialNumber,
                            null,
                            null,
                            null,
                            body.Type.ToString(),
                            "restart-sys",
                            "POST",
                            "/api/v1.0/web/restart-app",
                            body.Payload,
                            5000,
                            true
                        );

                        CommandService.Save(command);
                    }
                    else if (Command.RestartAppList.Contains(body.Type))
                    {
                        body.Payload = Command.GetPayloadForRestartAppTypes(body.Type);
                        string name = Command.GetNameForRestartAppTypes(body.Type);

                        Sql.Library.Models.Command command = new Sql.Library.Models.Command(
                            chargeController.SerialNumber,
                            null,
                            null,
                            null,
                            body.Type.ToString(),
                            name,
                            "POST",
                            $"/api/v1.0/web/restart-app",
                            body.Payload,
                            5000,
                            true
                        );

                        CommandService.Save(command);
                    }
                    else if (body.Type == CommandType.ImportRFIDs)
                    {
                        List<BMS.Sql.Library.Models.RFID>? whitelist = JsonConvert.DeserializeObject<List<BMS.Sql.Library.Models.RFID>>(body.Payload);
                        if (whitelist == null) return new StatusCodeResult(404);

                        body.Payload = "{";
                        foreach (RFID rfid in whitelist)
                        {
                            if (rfid.SerialNumber != null)
                            {
                                string rfidJson = JsonConvert.SerializeObject(rfid);
                                body.Payload += $"\"{rfid.SerialNumber}\": " + $"{JSONParse.ConvertToHeartbeatField(typeof(RFID), rfidJson)}" + ", ";
                            }
                        }
                        body.Payload += "}";

                        Sql.Library.Models.Command command = new Sql.Library.Models.Command(
                            chargeController.SerialNumber,
                            null,
                            null,
                            null,
                            body.Type.ToString(),
                            "import-rfids",
                            "POST",
                            $"/api/v1.0/rfid-whitelist",
                            body.Payload,
                            5555,
                            true
                        );

                        CommandService.Save(command);
                    }
                    else if (body.Type == CommandType.EditRFID)
                    {
                        body.Payload = $"{{\"{body.AdditionalValue}\": {JSONParse.ConvertToHeartbeatField(typeof(RFID), body.Payload)}}}";

                        Sql.Library.Models.Command command = new Sql.Library.Models.Command(
                            chargeController.SerialNumber,
                            null,
                            null,
                            body.AdditionalValue.ToString(),
                            body.Type.ToString(),
                            "edit-rfid",
                            "PUT",
                            $"/api/v1.0/rfid-whitelist/{body.AdditionalValue.ToString()}",
                            body.Payload,
                            5555,
                            true
                        );

                        CommandService.Save(command);
                    }
                    else if (body.Type == CommandType.OcppConfig)
                    {
                        body.Payload = $"{{\"OcppParameters\": {JSONParse.ConvertToHeartbeatField(typeof(OCPPConfig), body.Payload)}}}";

                        Sql.Library.Models.Command command = new Sql.Library.Models.Command(
                            chargeController.SerialNumber,
                            null,
                            null,
                            null,
                            body.Type.ToString(),
                            "ocpp-config",
                            "PUT",
                            "/api/v1.0/ocpp16/config/set-section-parameter",
                            body.Payload,
                            2106,
                            false
                        );

                        CommandService.Save(command);
                    }
                    else if (body.Type == CommandType.ModemConfig)
                    {
                        string[] fieldsToModify = { 
                            nameof(chargeController.ModemServiceActive).ToLower(),
                            nameof(chargeController.ModemAPN).ToLower(),
                            nameof(chargeController.UseAccessCredentials).ToLower(),
                            nameof(chargeController.ModemUsername).ToLower(),
                            nameof(chargeController.ModemPassword).ToLower(),
                            nameof(chargeController.ModemSimPin).ToLower(),
                            nameof(chargeController.ModemDefaultRoute).ToLower(),
                            nameof(chargeController.ModemPreferOverETH0).ToLower()
                        };

                        body.Payload = JSONParse.ConvertToHeartbeatField(typeof(Sql.Library.Models.ChargeController), body.Payload, fieldsToModify.ToList());

                        Sql.Library.Models.Command command = new Sql.Library.Models.Command(
                            chargeController.SerialNumber,
                            null,
                            null,
                            null,
                            body.Type.ToString(),
                            "modem-config",
                            "POST",
                            "/api/v1.0/web/modem",
                            body.Payload,
                            5000,
                            true
                        );

                        CommandService.Save(command);
                    }
                    else if (body.Type == CommandType.NetworkConfig)
                    {
                        string[] fieldsToModify = {
                            nameof(chargeController.ETH0DHCP).ToLower(), nameof(chargeController.ETH0IPAddress).ToLower(), nameof(chargeController.ETH0SubnetMask).ToLower(),
                            nameof(chargeController.ETH0Gateway).ToLower(), nameof(chargeController.ETH0NoGateway).ToLower()
                        };

                        body.Payload = JSONParse.ConvertToHeartbeatField(typeof(BMS.Sql.Library.Models.ChargeController), body.Payload, fieldsToModify.ToList());

                        Sql.Library.Models.Command command = new Sql.Library.Models.Command(
                            chargeController.SerialNumber,
                            null,
                            null,
                            null,
                            body.Type.ToString(),
                            "network-config",
                            "POST",
                            "/api/v1.0/web/network",
                            body.Payload,
                            5000,
                            true
                        );

                        CommandService.Save(command);
                    }
                    else if (body.Type == CommandType.DeleteRFID)
                    {
                        body.Payload = $"{{\"serial_number\":\"{body.AdditionalValue}\"}}";

                        Sql.Library.Models.Command command = new Sql.Library.Models.Command(
                            chargeController.SerialNumber,
                            null,
                            null,
                            body.AdditionalValue.ToString(),
                            body.Type.ToString(),
                            "delete-rfid",
                            "POST",
                            $"/api/v1.0/rfid-whitelist",
                            body.Payload,
                            5555,
                            true
                        );

                        CommandService.Save(command);
                    }
                    else if (body.Type == CommandType.DeleteAllRFIDs)
                    {
                        body.Payload = "{}";

                        Sql.Library.Models.Command command = new Sql.Library.Models.Command(
                            chargeController.SerialNumber,
                            null,
                            null,
                            null,
                            body.Type.ToString(),
                            "delete-all",
                            "POST",
                            $"/api/v1.0/rfid-whitelist",
                            body.Payload,
                            5555,
                            true
                        );

                        CommandService.Save(command);
                    }
                    else if (body.Type == CommandType.SaveLoadManagement)
                    {
                        string[] fieldsToModify = {
                            nameof(chargeController.ChargingParkName).ToLower(), nameof(chargeController.LoadCircuitFuse).ToLower(), nameof(chargeController.HighLevelMeasuringDeviceModbus).ToLower(),
                            nameof(chargeController.ETH0IPAddress).ToLower(), nameof(chargeController.MeasuringDeviceType).ToLower(), nameof(chargeController.LoadStrategy).ToLower(),
                            nameof(chargeController.ChargePoints).ToLower(), nameof(chargeController.HighLevelMeasuringDeviceControllerId).ToLower(), nameof(chargeController.LoadManagementIpAddress).ToLower()
                        };

                        body.Payload = JSONParse.ConvertToHeartbeatField(typeof(Sql.Library.Models.ChargeController), body.Payload, fieldsToModify.ToList());

                        Sql.Library.Models.Command command = new Sql.Library.Models.Command(
                            chargeController.SerialNumber,
                            null,
                            null,
                            null,
                            body.Type.ToString(),
                            "edit-lm",
                            "PUT",
                            "/api/v1.0/load-management/config",
                            body.Payload,
                            5000,
                            false
                        );

                        CommandService.Save(command);
                    }
                    else if (body.Type == CommandType.GetLogFiles)
                    {
                        body.Payload = "{}";

                        BMS.Sql.Library.Models.Command command = new Sql.Library.Models.Command(
                            chargeController.SerialNumber,
                            null,
                            null,
                            null,
                            body.Type.ToString(),
                            "get-log",
                            "POST",
                            $"/api/v1.0/get-log",
                            body.Payload,
                            0,
                            false
                        );

                        CommandService.Save(command);
                    }
                }

                return pendingCommands.Count + 1;
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }
        
        public async override Task<ActionResult<ICollection<CommandHistory>>> GetCommands([FromQuery] bool? inPendingOrProcessingCommands, [FromQuery] string masterId)
        {
            try
            {
                List<CommandHistory> commandHistoryList = new List<CommandHistory>();
                List<Sql.Library.Models.Command> commands;
                if (inPendingOrProcessingCommands == true)
                    commands = CommandService.GetCommandsByStatus(
                        masterId,
                        new List<CommandStatus> { CommandStatus.Pending, CommandStatus.Processing }
                    );
                else
                    commands = CommandService.GetByMasterUid(masterId);

                foreach (Sql.Library.Models.Command command in commands)
                    commandHistoryList.Add(new CommandHistory(command));
                
                return commandHistoryList;
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }

        public async override Task<ActionResult<ICollection<Email>>> GetChargeStationEmails([FromHeader] string userEmail, [FromHeader] string externalId, int id)
        {
            try
            {
                List<Email> emailsList = new List<Email>();
                List<Sql.Library.Models.Email> emails = EmailService.GetAllEmailsForChargeControler(id);
                foreach (Sql.Library.Models.Email email in emails)
                    emailsList.Add(new Email(email));
                return emailsList;
            }
            catch 
            {
                return new StatusCodeResult(500);
            }
        }
        public async override Task<ActionResult<ICollection<ApplicationUser>>> GetInstallersForChargeStation(
            [FromHeader] string userEmail,
            [FromHeader] string externalId,
            int chargeControllerId)
        {
            try
            {
                List<ApplicationUser> installersToSend = new List<ApplicationUser>();
                List<Sql.Library.Models.ApplicationUser> installers = ChargeControllerService.GetInstallersForChargeStation(chargeControllerId);
                foreach (Sql.Library.Models.ApplicationUser installer in installers)
                    installersToSend.Add(new ApplicationUser(installer));
                return installersToSend;
            }
            catch 
            {
                return new StatusCodeResult(500);
            }
        }

	public async override Task<ActionResult<Response3>> GetChargeStationLogFiles([FromHeader] string userEmail, [FromHeader] string externalId, int id)
        {
            BMS.Sql.Library.Models.ApplicationUser? user = ApplicationUserService.Get(userEmail, externalId);
            if (user == null || user.Id < 1) return new StatusCodeResult(401);
            try
            {
                string connectionString = Configuration["BlobConnectionString"];
                string containerPrefix = Configuration["BlobContainerPrefix"];

                BMS.Sql.Library.Models.ChargeController? chargeController = ChargeControllerService.Get(id, user);
                BlobContainerClient container = new BlobContainerClient(connectionString, containerPrefix + chargeController.SerialNumber);
                List<BlobData> blobs = new List<BlobData>();
                foreach (BlobItem blob in container.GetBlobs())
                {
                    BlobClient blobClient = new BlobClient(connectionString, containerPrefix + chargeController.SerialNumber, blob.Name);

                    using (var stream = new MemoryStream())
                    {
                        BlobData blobData = new BlobData();
                        await blobClient.DownloadToAsync(stream);
                        stream.Position = 0;
                        var contentType = (await blobClient.GetPropertiesAsync()).Value.ContentType;
                        blobData.Name = blob.Name;
                        blobData.CreatedOn = blob.Properties.CreatedOn.ToString();
                        blobData.ContentType = contentType.ToString();
                        blobs.Add(blobData);
                        blobData.Url = blobClient.Uri.ToString();
                    }
                }


                string json = JsonConvert.SerializeObject(blobs, Formatting.Indented);
                Response3 response3 = new Response3();
                response3.ChargeController = new ChargeController(chargeController);
                response3.Json = json;
                return response3;
            } catch (Exception ex)
            {
                BMS.Sql.Library.Models.ChargeController? chargeController = ChargeControllerService.Get(id, user);
                Response3 response3 = new Response3();
                response3.ChargeController = new ChargeController(chargeController);
                response3.Json = "";
                return response3;
            }
        }

	public async override Task<ActionResult<ICollection<ApplicationUser>>> GetAllInstallers([FromHeader] string userEmail, [FromHeader] string externalId)
        {
            try
            {
                BMS.Sql.Library.Models.ApplicationUser? user = ApplicationUserService.Get(userEmail, externalId);
                if (user == null || user.Id < 1 || UserRoleEnum.Admin != user.Role) return new StatusCodeResult(401);

                List<ApplicationUser> installersToSend = new List<ApplicationUser>();
                List<BMS.Sql.Library.Models.ApplicationUser> installers = ApplicationUserService.GetAllInstallers();
                foreach (BMS.Sql.Library.Models.ApplicationUser installer in installers)
                    installersToSend.Add(new ApplicationUser(installer));
                return installersToSend;
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        public async override Task<IActionResult> AddAndRemoveAccessForInstallers([FromHeader] string userEmail, [FromHeader] string externalId, [FromBody] IEnumerable<IEnumerable<ApplicationUser>> body, int chargeControllerId)
        {
            try
            {
                BMS.Sql.Library.Models.ApplicationUser? user = ApplicationUserService.Get(userEmail, externalId);
                if (user == null || user.Id < 1 || UserRoleEnum.Admin != user.Role) return new StatusCodeResult(401);
                foreach(ApplicationUser installerToRemoveAccess in body.ToList()[0].ToList())
                {
                    ApplicationUserService.RemoveAccessFromChargeStation(installerToRemoveAccess.Id, chargeControllerId);
                }
                foreach (ApplicationUser installerToAddAccess in body.ToList()[1].ToList())
                {
                    ApplicationUserService.AddAccessToChargeStation(installerToAddAccess.Id, chargeControllerId);
                }
                return new StatusCodeResult(200);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

    }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
}
