using BMS.Data.Api.Models;
using BMS.Sql.Library;
using BMS.Sql.Library.Models;
using BMS.Sql.Library.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Timers;

namespace BMS.Data.Api.Utilities
{
    public class MqttDataService
    {
        public BMSDbContext BMSDbContext { get; set; }
        private readonly ChargeControllerService ChargeControllerService;
        private readonly OcppMessageService OcppMessageService;
        private readonly TransactionService TransactionService;
        public MqttDataService(BMSDbContext bMSDbContext)
        {
            BMSDbContext = bMSDbContext;
            ChargeControllerService = new ChargeControllerService(bMSDbContext);
            OcppMessageService = new OcppMessageService(BMSDbContext);
            TransactionService = new TransactionService(BMSDbContext);
        }

        public void ProcessData(string? body, HttpRequest request)
        {
            DateTime parseTimeNow = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

            MqttData jData = JsonConvert.DeserializeObject<MqttData>(body);


            JObject newMaster = ParseObjectToJObject(jData.Master);
            JObject newSlaves = ParseObjectToJObject(jData.Slaves);
            JObject newUserInfo = ParseObjectToJObject(jData.UserInfo);
            JObject newBlitzVersionInfo = ParseObjectToJObject(jData.BlitzVersionInfo);
            JObject whiteList = ParseObjectToJObject(jData.Whitelist);
            JObject ocpp = ParseObjectToJObject(jData.OCPP);
            JObject transactions = ParseObjectToJObject(jData.Transactions);
            JObject emails = ParseObjectToJObject(jData.Emails);
            // taking Id from master object
            foreach (JProperty masterProperty in newMaster.Properties())
            {
                string id = masterProperty.Name;

                ChargeController? newController = ChargeControllerService.GetBySerialNumberWithData(id);
                if (newController == null)
                    newController = new ChargeController();

                newController.SerialNumber = id;

                if (newController.Client == null)
                    newController.Client = new Client();

                string clientName =
                    AssignValue(newUserInfo, "first-name", typeof(string), string.Empty) + " " +
                    AssignValue(newUserInfo, "last-name", typeof(string), string.Empty);
                if (clientName != " ") newController.Client.Name = clientName;
                string clientLocation =
                    AssignValue(newUserInfo, "address_main", typeof(string), string.Empty) + ", " +
                    AssignValue(newUserInfo, "country", typeof(string), string.Empty);
                if (clientLocation != ", ") newController.Client.Location = clientLocation;
                newController.Client.Phone = AssignValue(newUserInfo, "phone", typeof(string), newController.Client.Phone);
                newController.Client.Email = AssignValue(newUserInfo, "email", typeof(string), newController.Client.Email);

                if (newController.Networks == null)
                    newController.Networks = new List<Network>();

                Network networkEth0 = newController.Networks.FindLast((netWork) => netWork.Name == "eth0");
                if (networkEth0 == null)
                {
                    networkEth0 = new Network();
                    newController.Networks.Add(networkEth0);
                }
                Network networkEth1 = newController.Networks.FindLast((netWork) => netWork.Name == "eth1");
                if (networkEth1 == null)
                {
                    networkEth1 = new Network();
                    newController.Networks.Add(networkEth1);
                }

                networkEth0.Name = AssignValue(newMaster, $"{id}.network.status._v_.eth0.name", typeof(string), networkEth0.Name);
                networkEth0.IPV4Address = AssignValue(newMaster, $"{id}.network.status._v_.eth0.inet.addr", typeof(string), networkEth0.IPV4Address);
                networkEth0.IPV6Address = AssignValue(newMaster, $"{id}.network.status._v_.eth0.inet6.addr", typeof(string), networkEth0.IPV6Address);
                networkEth0.MACAddress = AssignValue(newMaster, $"{id}.network.status._v_.eth0.mac", typeof(string), networkEth0.MACAddress);
                networkEth0.ReceivedBytes = AssignValue(newMaster, $"{id}.network.status._v_.eth0.rx_bytes", typeof(long), networkEth0.ReceivedBytes);
                networkEth0.TransmittedBytes = AssignValue(newMaster, $"{id}.network.status._v_.eth0.tx_bytes", typeof(long), networkEth0.TransmittedBytes);

                networkEth1.Name = AssignValue(newMaster, $"{id}.network.status._v_.eth1.name", typeof(string), networkEth1.Name);
                networkEth1.IPV4Address = AssignValue(newMaster, $"{id}.network.status._v_.eth1.inet.addr", typeof(string), networkEth1.IPV4Address);
                networkEth1.IPV6Address = AssignValue(newMaster, $"{id}.network.status._v_.eth1.inet6.addr", typeof(string), networkEth1.IPV6Address);
                networkEth1.MACAddress = AssignValue(newMaster, $"{id}.network.status._v_.eth1.mac", typeof(string), networkEth1.MACAddress);
                networkEth1.ReceivedBytes = AssignValue(newMaster, $"{id}.network.status._v_.eth1.rx_bytes", typeof(long), networkEth1.ReceivedBytes);
                networkEth1.TransmittedBytes = AssignValue(newMaster, $"{id}.network.status._v_.eth1.tx_bytes", typeof(long), networkEth1.TransmittedBytes);

                if (networkEth0.Name == null || networkEth0.Name != "eth0")
                    newController.Networks.Remove(networkEth0);
                if (networkEth1.Name == null || networkEth1.Name != "eth1")
                    newController.Networks.Remove(networkEth1);

                JArray masterChargePoints = AssignValue(newMaster, $"{id}.charging_controllers", typeof(JArray), new JArray());
                if (newController.ChargePoints == null)
                    newController.ChargePoints = new List<ChargePoint>();
                List<string> remainChargePointUids = new List<string>();
                foreach (JObject newChargePoint in masterChargePoints.Children())
                {
                    string remainUid = ParseChargePoint(newMaster, newChargePoint, newController.ChargePoints, id, id);
                    if (remainUid != null)
                        remainChargePointUids.Add(remainUid);
                }

                foreach (JProperty slaveProperty in newSlaves.Properties())
                {
                    string slaveKey = slaveProperty.Name;
                    JArray slaveChargePoints = AssignValue(newSlaves, $"{slaveKey}.charging_controllers", typeof(JArray), new JArray());
                    foreach (JObject newChargePoint in slaveChargePoints.Children())
                    {
                        string remainUid = ParseChargePoint(newMaster, newChargePoint, newController.ChargePoints, slaveKey, id);
                        if (remainUid != null)
                            remainChargePointUids.Add(remainUid);
                    }
                }
                if (remainChargePointUids.Count != 0)
                    newController.ChargePoints.RemoveAll(chargePoint => !remainChargePointUids.Contains(chargePoint.ChargeControllerUid));

                newController.NetworkConnection = true;
                newController.OCPPConnection = AssignValue(newMaster, $"{id}.applications.ocpp16.info.central_system_connection._v_", typeof(string), newController.OCPPConnection);
                newController.SimActive = AssignValue(newMaster, $"{id}.modem.config.enabled", typeof(bool), newController.SimActive);
                newController.UptimeInSeconds = AssignValue(newMaster, $"{id}.system.status._v_.uptime.up", typeof(long), newController.UptimeInSeconds);
                newController.Address = AssignValue(newUserInfo, "address_main", typeof(string), newController.Address);
                newController.LocationData = AssignValue(newUserInfo, "address-installation", typeof(string), newController.LocationData);
                newController.DateInstalled = AssignValue(newUserInfo, "datetime", typeof(DateTimeOffset), newController.DateInstalled);
                newController.CompanyName = AssignValue(newUserInfo, "company-name", typeof(string), newController.CompanyName);
                newController.SystemTime = AssignValue(newMaster, $"{id}.system.datetime.status._v_.current_time", typeof(DateTimeOffset), newController.SystemTime);
                if (jData != null && jData.PayloadType == "full-heartbeat")
                {
                    newController.FullHeartbeat = parseTimeNow;
                }
                else
                {
                    newController.Heartbeat = parseTimeNow;
                }
                newController.NetworkType = AssignValue(newMaster, $"{id}.modem.status._v_.network_type", typeof(string), newController.NetworkType);
                newController.ETH0DHCP = AssignValue(newMaster, $"{id}.network.config.dhcp", typeof(bool), newController.ETH0DHCP);
                newController.ETH0IPAddress = AssignValue(newMaster, $"{id}.network.config.addresses", typeof(string), newController.ETH0IPAddress);
                newController.MacAddress = AssignValue(newMaster, $"{id}.network.status._v_.eth0.mac", typeof(string), newController.MacAddress);
                newController.ETH0SubnetMask = AssignValue(newMaster, $"{id}.network.config.netmask", typeof(string), newController.ETH0SubnetMask);
                newController.ETH0Gateway = AssignValue(newMaster, $"{id}.network.config.gateway", typeof(string), newController.ETH0Gateway);
                newController.ETH0NoGateway = AssignValue(newMaster, $"{id}.network.config.nogateway", typeof(bool), newController.ETH0NoGateway);
                newController.ModemServiceActive = AssignValue(newMaster, $"{id}.modem.config.enabled", typeof(bool), newController.ModemServiceActive);
                newController.ModemPreferOverETH0 = AssignValue(newMaster, $"{id}.modem.config.prefermodem", typeof(bool), newController.ModemPreferOverETH0);
                newController.ModemDefaultRoute = AssignValue(newMaster, $"{id}.modem.config.defaultroute", typeof(bool), newController.ModemDefaultRoute);
                newController.ModemSignalRSSI = AssignValue(newMaster, $"{id}.modem.status._v_.signal_rssi", typeof(int), newController.ModemSignalRSSI);
                newController.ModemSignalCQI = AssignValue(newMaster, $"{id}.modem.status._v_.signal_cqi", typeof(int), newController.ModemSignalCQI);
                newController.ModemReceivedBytes = AssignValue(newMaster, $"{id}.network.status._v_.eth0.rx_bytes", typeof(long), newController.ModemReceivedBytes);
                newController.ModemTransmittedBytes = AssignValue(newMaster, $"{id}.network.status._v_.eth0.tx_bytes", typeof(long), newController.ModemTransmittedBytes);
                newController.ModemAPN = AssignValue(newMaster, $"{id}.modem.status._v_.apn", typeof(string), newController.ModemAPN);
                newController.ModemSimPin = AssignValue(newMaster, $"{id}.modem.config.pin", typeof(string), newController.ModemSimPin);
                newController.UseAccessCredentials = AssignValue(newMaster, $"{id}.modem.config.useaccesscredentials", typeof(bool), newController.UseAccessCredentials);
                newController.ModemUsername = AssignValue(newMaster, $"{id}.modem.config.username", typeof(string), newController.ModemUsername);
                newController.ModemPassword = AssignValue(newMaster, $"{id}.modem.config.password", typeof(string), newController.ModemPassword);
                newController.ModemIPAddress = AssignValue(newMaster, $"{id}.modem.status._v_.pdp_addr", typeof(string), newController.ModemIPAddress);
                newController.ModemPrimaryDNSServer = AssignValue(newMaster, $"{id}.modem.status._v_.primary_dns", typeof(string), newController.ModemPrimaryDNSServer);
                newController.ModemSecondaryDNSServer = AssignValue(newMaster, $"{id}.modem.status._v_.secondary_dns", typeof(string), newController.ModemSecondaryDNSServer);
                newController.ModemIMSI = AssignValue(newMaster, $"{id}.modem.network.info._v_.imsi", typeof(string), newController.ModemIMSI);
                newController.ModemICCID = AssignValue(newMaster, $"{id}.modem.network.info._v_.iccid", typeof(string), newController.ModemICCID);
                newController.ModemExtendedReport = AssignValue(newMaster, $"{id}.modem.status._v_.extended_error_report.category", typeof(string), newController.ModemExtendedReport);
                newController.ModemConnectionStatus = AssignValue(newMaster, $"{id}.modem.network.info._v_.sim_status", typeof(string), newController.ModemConnectionStatus);

                newController.ModemProvider = AssignValue(newMaster, $"{id}.modem.info._v_.manufacturer", typeof(string), newController.ModemProvider);
                newController.ModemRegistrationStatus = AssignValue(newMaster, $"{id}.modem.status._v_.registration_status", typeof(string), newController.ModemRegistrationStatus);
                newController.ModemRoamingStatus = AssignValue(newMaster, $"{id}.modem.status._v_.roaming_status", typeof(string), newController.ModemRoamingStatus);
                newController.ModemSignalQuality = AssignValue(newMaster, $"{id}.modem.status._v_.signal_quality", typeof(string), newController.ModemSignalQuality);
                newController.ModemRadioTechnology = AssignValue(newMaster, $"{id}.modem.status._v_.network_type", typeof(string), newController.ModemRadioTechnology);
                newController.ModemSimStatus = AssignValue(newMaster, $"{id}.modem.status._v_.sim_status", typeof(string), newController.ModemSimStatus);

                newController.CPUTemperatureCelsius = AssignValue(newMaster, $"{id}.system.status._v_.cpu.temp", typeof(int), newController.CPUTemperatureCelsius);
                newController.CPUUtilizationPercentage = AssignValue(newMaster, $"{id}.system.status._v_.cpu.user", typeof(int), newController.CPUUtilizationPercentage);
                newController.RAMAvailableBytes = AssignValue(newMaster, $"{id}.system.status._v_.memory.available", typeof(long), newController.RAMAvailableBytes);
                newController.RAMTotalBytes = AssignValue(newMaster, $"{id}.system.status._v_.memory.total", typeof(long), newController.RAMTotalBytes);
                newController.RAMUsedBytes = AssignValue(newMaster, $"{id}.system.status._v_.memory.used", typeof(long), newController.RAMUsedBytes);
                newController.DataUtilizationPercentage = AssignValue(newMaster, $"{id}.system.status._v_.file_system_disk_space_usage./data.used_percentage", typeof(int), newController.DataUtilizationPercentage);
                newController.DataTotalBytes = AssignValue(newMaster, $"{id}.system.status._v_.file_system_disk_space_usage./data.available", typeof(long), newController.DataTotalBytes);
                newController.LogUtilizationPercentage = AssignValue(newMaster, $"{id}.system.status._v_.file_system_disk_space_usage./log.used_percentage", typeof(int), newController.LogUtilizationPercentage);
                long logAvailableBytes = AssignValue(newMaster, $"{id}.system.status._v_.file_system_disk_space_usage./log.available", typeof(long), -1);
                long logUsedBytes = AssignValue(newMaster, $"{id}.system.status._v_.file_system_disk_space_usage./log.used", typeof(long), -1);
                if (logAvailableBytes != -1 && logUsedBytes != -1) newController.LogTotalBytes = logAvailableBytes + logUsedBytes;
                newController.VarVolatileUtilizationPercentage = AssignValue(newMaster, $"{id}.system.status._v_.file_system_disk_space_usage./var/volatile.used_percentage", typeof(int), newController.VarVolatileUtilizationPercentage);
                long volatileAvailableBytes = AssignValue(newMaster, $"{id}.system.status._v_.file_system_disk_space_usage./var/volatile.available", typeof(long), -1);
                long volatileUsedBytes = AssignValue(newMaster, $"{id}.system.status._v_.file_system_disk_space_usage./var/volatile.used", typeof(long), -1);
                if (volatileUsedBytes != -1 && volatileAvailableBytes!= -1) newController.VarVolatileTotalBytes = volatileAvailableBytes + volatileUsedBytes;
                newController.SystemMonitorVersion = AssignValue(newMaster, $"{id}.applications.sm.info.version._v_", typeof(string), newController.SystemMonitorVersion);
                newController.SystemMonitorStatus = AssignValue(newMaster, $"{id}.applications.sm.alive", typeof(string), newController.SystemMonitorStatus);
                newController.ControllerAgentVersion = AssignValue(newMaster, $"{id}.applications.ca.info.version._v_", typeof(string), newController.ControllerAgentVersion);
                newController.ControllerAgent_Status = AssignValue(newMaster, $"{id}.applications.ca.alive", typeof(string), newController.ControllerAgent_Status);
                newController.OCPP16_Version = AssignValue(newMaster, $"{id}.applications.ocpp16.info.version._v_", typeof(string), newController.OCPP16_Version);
                newController.OCPP16Status = AssignValue(newMaster, $"{id}.applications.ocpp16.info.agent_status._v_", typeof(string), newController.OCPP16Status);
                newController.ModbusClientVersion = AssignValue(newMaster, $"{id}.applications.mb.info.version._v_", typeof(string), newController.ModbusClientVersion);
                newController.ModbusClientStatus = AssignValue(newMaster, $"{id}.applications.mb.alive", typeof(string), newController.ModbusClientStatus);
                newController.ModbusServerVersion = AssignValue(newMaster, $"{id}.applications.ms.info.version._v_", typeof(string), newController.ModbusServerVersion);
                newController.ModbusServerStatus = AssignValue(newMaster, $"{id}.applications.ms.alive", typeof(string), newController.ModbusServerStatus);
                newController.JupicoreVersion = AssignValue(newMaster, $"{id}.applications.jc.info.version._v_", typeof(string), newController.JupicoreVersion);
                newController.JupicoreStatus = AssignValue(newMaster, $"{id}.applications.jc.alive", typeof(string), newController.JupicoreStatus);
                newController.LoadManagementVersion = AssignValue(newMaster, $"{id}.applications.loadmanagement.info.version._v_", typeof(string), newController.LoadManagementVersion);
                newController.LoadManagementStatus = AssignValue(newMaster, $"{id}.applications.loadmanagement.alive", typeof(string), newController.LoadManagementStatus);
                newController.WebserverVersion = AssignValue(newMaster, $"{id}.applications.website.info.version._v_", typeof(string), newController.WebserverVersion);
                newController.WebserverStatus = AssignValue(newMaster, $"{id}.applications.website.alive", typeof(string), newController.WebserverStatus);
                newController.LoadManagementActive = AssignValue(newMaster, $"{id}.applications.loadmanagement.data.active._v_", typeof(bool), newController.LoadManagementActive);
                newController.OsReleaseVersion = AssignValue(newMaster, $"{id}.system.info._v_.os_release", typeof(string), newController.OsReleaseVersion);
                newController.WebAppVersion = AssignValue(newBlitzVersionInfo, $"current-tag", typeof(string), newController.WebAppVersion); ;
                if (newController.Id < 1)
                    newController.AllowTestModeCommands = false;

                if (newController.WhitelistRFIDs == null)
                    newController.WhitelistRFIDs = new List<RFID>();
                List<string> remainRFIDSerialNumbers = new List<string>();

                foreach (JProperty whiteListProperty in whiteList.Properties())
                {
                    string remainSerialNumber = ParrseRFID(whiteList, newController.WhitelistRFIDs, whiteListProperty.Name);
                    if (remainSerialNumber != null)
                        remainRFIDSerialNumbers.Add(remainSerialNumber);
                }
                if (remainRFIDSerialNumbers.Count != 0)
                    newController.WhitelistRFIDs.RemoveAll(rfid => !remainRFIDSerialNumbers.Contains(rfid.SerialNumber));

                // Load Management fields

                newController.ChargingParkName = AssignValue(newMaster, $"{id}.applications.loadmanagement.data.agent.load_circuit_measure_device._v_.load_circuits[0].name", typeof(string), newController.ChargingParkName);
                newController.LoadCircuitFuse = AssignValue(newMaster, $"{id}.applications.loadmanagement.data.agent.load_circuit_measure_device._v_.load_circuits[0].fuse", typeof(string), newController.LoadCircuitFuse);
                newController.HighLevelMeasuringDeviceModbus = AssignValue(newMaster, $"{id}.applications.loadmanagement.data.agent.load_circuit_measure_device._v_.measuring_total_current.modbus_id", typeof(string), newController.HighLevelMeasuringDeviceModbus);
                newController.HighLevelMeasuringDeviceControllerId = AssignValue(newMaster, $"{id}.applications.loadmanagement.data.agent.load_circuit_measure_device._v_.measuring_total_current.controller_id", typeof(string), newController.HighLevelMeasuringDeviceControllerId);
                newController.MeasuringDeviceType = AssignValue(newMaster, $"{id}.applications.loadmanagement.data.agent.load_circuit_measure_device._v_.ipdevice.Type", typeof(string), newController.MeasuringDeviceType);
                newController.LoadManagementIpAddress = AssignValue(newMaster, $"{id}.applications.loadmanagement.data.agent.load_circuit_measure_device._v_.ipdevice.IP-Address", typeof(string), newController.MeasuringDeviceType);
                newController.LoadStrategy = AssignValue(newMaster, $"{id}.applications.loadmanagement.data.agent.load_circuit_measure_device._v_.load_circuits.charging_rule", typeof(string), newController.LoadStrategy);
                newController.CurrentI1 = AssignValue(newMaster, $"{id}.applications.loadmanagement.data.load_circuit.dispatched_current._v_.i1", typeof(string), newController.CurrentI1);
                newController.CurrentI2 = AssignValue(newMaster, $"{id}.applications.loadmanagement.data.load_circuit.dispatched_current._v_.i2", typeof(string), newController.CurrentI2);
                newController.CurrentI3 = AssignValue(newMaster, $"{id}.applications.loadmanagement.data.load_circuit.dispatched_current._v_.i3", typeof(string), newController.CurrentI3);   
                newController.PlannedCurrentI1 = AssignValue(newMaster, $"{id}.applications.loadmanagement.data.load_circuit.dispatched_current_planned._v_.i1", typeof(string), newController.PlannedCurrentI1);
                newController.PlannedCurrentI2 = AssignValue(newMaster, $"{id}.applications.loadmanagement.data.load_circuit.dispatched_current_planned._v_.i2", typeof(string), newController.PlannedCurrentI2);
                newController.PlannedCurrentI3 = AssignValue(newMaster, $"{id}.applications.loadmanagement.data.load_circuit.dispatched_current_planned._v_.i3", typeof(string), newController.PlannedCurrentI3);

                newController.SupervisionMeterCurrentI1 = AssignValue(newMaster, $"{id}.applications.loadmanagement.data.supervision_meter_current._v_.i1", typeof(string), newController.SupervisionMeterCurrentI1);
                newController.SupervisionMeterCurrentI2 = AssignValue(newMaster, $"{id}.applications.loadmanagement.data.supervision_meter_current._v_.i2", typeof(string), newController.SupervisionMeterCurrentI2);
                newController.SupervisionMeterCurrentI3 = AssignValue(newMaster, $"{id}.applications.loadmanagement.data.supervision_meter_current._v_.i3", typeof(string), newController.SupervisionMeterCurrentI3);

                newController.MonitoredCps = AssignValue(newMaster, $"{id}.applications.loadmanagement.data.load_circuit.monitored_charging_points._v_", typeof(string), newController.MonitoredCps);

                if (newController.OCPPConfig == null)
                    newController.OCPPConfig = new OCPPConfig();
                newController.OCPPConfig.OcppProtocolVersion = AssignValue(ocpp, "config.info.ocpp_application_settings.OcppParameters.FirmwareVersion", typeof(string), newController.OCPPConfig.OcppProtocolVersion);
                newController.OCPPConfig.NetworkInterface = AssignValue(ocpp, "config.info.ocpp_application_settings.OcppParameters.Interface", typeof(string), newController.OCPPConfig.NetworkInterface);
                newController.OCPPConfig.BackendURL = AssignValue(ocpp, "config.info.ocpp_application_settings.OcppParameters.BackendURL", typeof(string), newController.OCPPConfig.BackendURL);
                newController.OCPPConfig.ServiceRFID = AssignValue(ocpp, "config.info.ocpp_application_settings.OcppParameters.ServiceUID", typeof(string), newController.OCPPConfig.ServiceRFID);
                newController.OCPPConfig.FreeModeRFID = AssignValue(ocpp, "config.info.ocpp_application_settings.OcppParameters.FreeModeUID", typeof(string), newController.OCPPConfig.FreeModeRFID);
                newController.OCPPConfig.ChargeStationModel = AssignValue(ocpp, "config.info.ocpp_application_settings.OcppParameters.ChargePointModel", typeof(string), newController.OCPPConfig.ChargeStationModel);
                newController.OCPPConfig.ChargeStationVendor = AssignValue(ocpp, "config.info.ocpp_application_settings.OcppParameters.ChargePointVendor", typeof(string), newController.OCPPConfig.ChargeStationVendor);
                newController.OCPPConfig.ChargeStationSerialNumber = AssignValue(ocpp, "config.info.ocpp_application_settings.OcppParameters.ChargePointSerialNumber", typeof(string), newController.OCPPConfig.ChargeStationSerialNumber);
                newController.OCPPConfig.MeterSerialNumber = AssignValue(ocpp, "config.info.ocpp_application_settings.OcppParameters.MeterSerialNumber", typeof(string), newController.OCPPConfig.MeterSerialNumber);
                newController.OCPPConfig.MeterType = AssignValue(ocpp, "config.info.ocpp_application_settings.OcppParameters.MeterType", typeof(string), newController.OCPPConfig.MeterType);
                newController.OCPPConfig.ChargeBoxID = AssignValue(ocpp, "config.info.ocpp_application_settings.OcppParameters.ChargeBoxID", typeof(string), newController.OCPPConfig.ChargeBoxID);
                newController.OCPPConfig.ChargeBoxSerialNumber = AssignValue(ocpp, "config.info.ocpp_application_settings.OcppParameters.ChargeBoxSerialNumber", typeof(string), newController.OCPPConfig.ChargeBoxSerialNumber);
                newController.OCPPConfig.ServiceRestart = AssignValue(ocpp, "config.info.ocpp_application_settings.OcppParameters.ServiceRestart", typeof(bool), newController.OCPPConfig.ServiceRestart);
                newController.OCPPConfig.FreeMode = AssignValue(ocpp, "config.info.ocpp_application_settings.OcppParameters.FreeMode", typeof(bool), newController.OCPPConfig.FreeMode);
                newController.OCPPConfig.Iccid = AssignValue(ocpp, "config.info.ocpp_application_settings.OcppParameters.Iccid", typeof(string), newController.OCPPConfig.Iccid);
                newController.OCPPConfig.Imsi = AssignValue(ocpp, "config.info.ocpp_application_settings.OcppParameters.Imsi", typeof(string), newController.OCPPConfig.Imsi);

                JArray ocppStatusArray = AssignValue(ocpp, "diagnostic.chargingpoint-overview.status_ocpp_chargepoints", typeof(JArray), new JArray());
                if (newController.oCPPStatus == null)
                    newController.oCPPStatus = new List<OCPPStatus>();
                List<string> remainOCPPStatusDeviceUids = new List<string>();
                foreach (JObject newOcppStatus in ocppStatusArray.Children())
                {
                    string remainDeviceUid = ParseOCPP(newOcppStatus, newController.oCPPStatus);
                    if (remainDeviceUid != null)
                        remainOCPPStatusDeviceUids.Add(remainDeviceUid);
                }
                if (remainOCPPStatusDeviceUids.Count != 0)
                    newController.oCPPStatus.RemoveAll(ocppStatus => !remainOCPPStatusDeviceUids.Contains(ocppStatus.Device_uid));

                JArray ocppMessageArray = AssignValue(ocpp, "diagnostic.ocpp-telegram-protocol.message_log", typeof(JArray), new JArray());
                newController.oCPPMessages = new List<OCPPMessage>();
                foreach (JObject newOcppMessage in ocppMessageArray.Children())
                    newController.oCPPMessages.Add(ParseOCPPMessage(newOcppMessage));

                JObject transactionArray = AssignValue(transactions, "transactions", typeof(JObject), new JObject());
                (newController.Transactions, List<string> transactionsIds) = TransactionService.GetAllTransactionsIdsForChargeControler(newController.Id);
                foreach (JProperty transaction in transactionArray.Children())
                {
                    JObject transactionObject = new JObject(transaction);
                    string transactionDataString = AssignValue(transactionObject, "", typeof(string), "");

                    string transactionsId = transactionDataString.Split('"').ToList()[1];
                    if (!transactionsIds.Contains(transactionsId))
                    {
                        JObject transactionData = AssignValue(transactionObject, $"{transactionsId}", typeof(JObject), new JObject());
                        newController.Transactions.Add(ParseTransaction(transactionsId, transactionData, newController.Id));
                    }
                }

                JObject emailsArray = AssignValue(emails, "email-list", typeof(JObject), new JObject());
                if (newController.Emails == null)
                {
                    newController.Emails = new List<Email>();
                }
                List<string> emailsAlreadySaved = ChargeControllerService.GetAllEmailsForChargeController(newController.Id);
                foreach (JProperty email in emailsArray.Children())
                {
                    JObject emailObject = new JObject(email);
                    string emailDataString = AssignValue(emailObject, "", typeof(string), "");

                    int emailId = Int32.Parse(emailDataString.Split('"').ToList()[1]);
                    JObject emailData = AssignValue(emailObject, $"{emailId}", typeof(JObject), new JObject());
                    Email newEmail = ParseEmails(emailData, newController.Id);
                    if (newEmail != null && !emailsAlreadySaved.Contains(newEmail.EmailReceiver))
                    {
                        newController.Emails.Add(newEmail);
                    }
                }

                string userData = AssignValue(transactions, "transactions", typeof(string), "");
                if (newController.UserData == null)
                    newController.UserData = new UserData();
                if (userData.Length > 0)
                {
                    newController.UserData.Created = parseTimeNow;
                    newController.UserData.JsonData = userData;
                }

                if (newController.oCPPMessages.Count > 0)
                    OcppMessageService.DisableLastHeartbeat(newController.Id);
                ChargeControllerService.Save(newController);
                break;
            }
        }

        private string? ParseChargePoint(JObject jObjectMaster, JObject jObjectChargePoint, List<ChargePoint> actualChargePoints, string slaveSerialNumber, string masterSerialNumber)
        {
            //TODO: Rename JObject jObjectChargePoint to jObjectChargeController
            string chargeControllerUid = AssignValue(jObjectChargePoint, "uid", typeof(string), null);
            if (chargeControllerUid == null)
                return null;

            ChargePoint newChargePoint = new ChargePoint();
            JArray chargingPointList = AssignValue(jObjectChargePoint, "charging_points", typeof(JArray), new JArray());

            bool isNewChargePoint = true;
            foreach (ChargePoint chargePoint in actualChargePoints)
                if (chargePoint.ChargeControllerUid == chargeControllerUid)
                {
                    newChargePoint = chargePoint;
                    isNewChargePoint = false;
                    break;
                }
            if (isNewChargePoint)
                actualChargePoints.Add(newChargePoint);

            foreach (JObject chargingPoint in chargingPointList.Children())
            {
                newChargePoint.ChargeControllerUid = chargeControllerUid;
                newChargePoint.ChargePointUid = AssignValue(chargingPoint, "uid", typeof(string), newChargePoint.ChargePointUid);
                newChargePoint.Enabled = AssignValue(jObjectChargePoint, "control.charge_enable._v_", typeof(bool), newChargePoint.Enabled);
                newChargePoint.ChargingTimeInSeconds = AssignValue(jObjectChargePoint, "data.charge_time_sec._v_", typeof(int), newChargePoint.ChargingTimeInSeconds);
                newChargePoint.ChargeCurrentMinimumInAmpers = AssignValue(jObjectChargePoint, "function_config.minimum_charge_current", typeof(int), newChargePoint.ChargeCurrentMinimumInAmpers);
                newChargePoint.ChargeCurrentMaximumInAmpers = AssignValue(jObjectChargePoint, "function_config.maximum_charge_current", typeof(int), newChargePoint.ChargeCurrentMaximumInAmpers);
                newChargePoint.FallbackCurrentInAmpers = AssignValue(jObjectChargePoint, "function_config.fallback_charge_current", typeof(int), newChargePoint.FallbackCurrentInAmpers);

                newChargePoint.FallbackTimeInSeconds = AssignValue(chargingPoint, "fallback_charge_current._v_", typeof(int), newChargePoint.FallbackTimeInSeconds);
                newChargePoint.RFIDTimeoutInSeconds = AssignValue(jObjectMaster, $"{masterSerialNumber}.applications.config.ChargingPoints.RfidChargeGrantTimeoutSec", typeof(int), newChargePoint.RFIDTimeoutInSeconds);
                newChargePoint.OCPPConnectorId = AssignValue(chargingPoint, "ocpp16_connector_id._v_", typeof(int), newChargePoint.OCPPConnectorId);

                decimal chargingRate = 0;
                decimal.TryParse((string)jObjectChargePoint.SelectToken("data.energy._v_.real_power.value"), out chargingRate);
                newChargePoint.ChargingRate = chargingRate == 0 ? newChargePoint.ChargingRate : Math.Round(chargingRate / 1000, 2);

                newChargePoint.SlaveSerialNumber = slaveSerialNumber;
                newChargePoint.SerialNumber = AssignValue(jObjectChargePoint, "serial_number", typeof(string), newChargePoint.SerialNumber);
                newChargePoint.Name = AssignValue(chargingPoint, "charging_point_name._v_", typeof(string), newChargePoint.Name);
                newChargePoint.SoftwareVersion = AssignValue(jObjectChargePoint, "info._v_.firmware_version", typeof(string), newChargePoint.SoftwareVersion);
                newChargePoint.Location = AssignValue(chargingPoint, "location._v_", typeof(string), newChargePoint.Location);
                newChargePoint.State = AssignValue(jObjectChargePoint, "data.iec_61851_state._v_", typeof(string), newChargePoint.State);
                newChargePoint.EnergyType = AssignValue(chargingPoint, "energy_meter_type._v_", typeof(string), newChargePoint.EnergyType);
                newChargePoint.PhaseRotation = AssignValue(chargingPoint, "connector_phase_rotation._v_", typeof(string), newChargePoint.PhaseRotation);
                newChargePoint.ReleaseChargingMode = AssignValue(chargingPoint, "release_charging_mode._v_", typeof(string), newChargePoint.ReleaseChargingMode);
                newChargePoint.RFIDReader = AssignValue(chargingPoint, "rfid_reader_device_uid._v_", typeof(string), newChargePoint.RFIDReader);
                newChargePoint.RFIDReaderType = AssignValue(chargingPoint, "rfid_reader_type._v_", typeof(string), newChargePoint.RFIDReaderType);
                newChargePoint.HighLevelCommunication = AssignValue(chargingPoint, "evse_hlc_policy._v_", typeof(string), newChargePoint.HighLevelCommunication);
                newChargePoint.ExternalRelease = AssignValue(jObjectChargePoint, "control.external_release._v_", typeof(bool), newChargePoint.ExternalRelease);
                newChargePoint.LocalBusState = AssignValue(jObjectChargePoint, "status._v_", typeof(string), newChargePoint.LocalBusState);
                newChargePoint.ChargingDuration = AssignValue(jObjectChargePoint, "data.charge_time_sec._v_", typeof(string), newChargePoint.ChargingDuration);
                newChargePoint.PluginDuration = AssignValue(jObjectChargePoint, "data.connected_time_sec._v_", typeof(string), newChargePoint.PluginDuration);
                newChargePoint.ChargingCurrentLimit = AssignValue(jObjectChargePoint, "data.pwm_duty_cycle_ampere._v_", typeof(string), newChargePoint.ChargingCurrentLimit);
                newChargePoint.BusPosition = AssignValue(jObjectChargePoint, "info._v_.position", typeof(string), newChargePoint.BusPosition);
                newChargePoint.status = AssignValue(jObjectChargePoint, "data.iec_61851_state._v_", typeof(string), newChargePoint.status);
                newChargePoint.errorStatus = AssignValue(jObjectChargePoint, "data.error_status_enum._v_", typeof(string), newChargePoint.errorStatus);
                newChargePoint.externalTemperature = AssignValue(chargingPoint, "temperature_sensor_type._v_", typeof(string), newChargePoint.externalTemperature);
                newChargePoint.CurrentI1 = AssignValue(jObjectChargePoint, "data.energy._v_.i1.value", typeof(string), newChargePoint.CurrentI1);
                newChargePoint.CurrentI2 = AssignValue(jObjectChargePoint, "data.energy._v_.i2.value", typeof(string), newChargePoint.CurrentI2);
                newChargePoint.CurrentI3 = AssignValue(jObjectChargePoint, "data.energy._v_.i3.value", typeof(string), newChargePoint.CurrentI3);
                newChargePoint.VoltageU1 = AssignValue(jObjectChargePoint, "data.energy._v_.u1.value", typeof(string), newChargePoint.VoltageU1);
                newChargePoint.VoltageU2 = AssignValue(jObjectChargePoint, "data.energy._v_.u2.value", typeof(string), newChargePoint.VoltageU2);
                newChargePoint.VoltageU3 = AssignValue(jObjectChargePoint, "data.energy._v_.u3.value", typeof(string), newChargePoint.VoltageU3);
                newChargePoint.totalEnergy = AssignValue(jObjectChargePoint, "data.energy._v_.energy_real_power.value", typeof(string), newChargePoint.totalEnergy);
                newChargePoint.powerFactor = AssignValue(jObjectChargePoint, "data.energy._v_.power_factor.value", typeof(string), newChargePoint.powerFactor);
                newChargePoint.frequency = AssignValue(jObjectChargePoint, "data.energy._v_.frequency.value", typeof(string), newChargePoint.frequency);
                break;
            }

            return chargeControllerUid;
        }

        private string? ParrseRFID(JObject jObjectWhitelistRFID, List<RFID> actualWhitelistRFIDs, string serialNumber)
        {
            if (serialNumber == null)
                return null;

            RFID newRFID = new RFID();

            bool isNewRFID = true;
            foreach (RFID rfid in actualWhitelistRFIDs)
                if (rfid.SerialNumber == serialNumber)
                {
                    newRFID = rfid;
                    isNewRFID = false;
                    break;
                }
            if (isNewRFID)
                actualWhitelistRFIDs.Add(newRFID);

            newRFID.SerialNumber = serialNumber;
            newRFID.Name = AssignValue(jObjectWhitelistRFID, $"{serialNumber}.label", typeof(string), newRFID.Name);
            newRFID.AllowCharging = AssignValue(jObjectWhitelistRFID, $"{serialNumber}.active", typeof(bool), newRFID.AllowCharging);
            newRFID.EvConsumptionRateKWhPer100KM = AssignValue(jObjectWhitelistRFID, $"{serialNumber}.vehicle_consumption", typeof(int), newRFID.EvConsumptionRateKWhPer100KM);
            newRFID.ExpiryDate = AssignValue(jObjectWhitelistRFID, $"{serialNumber}.expiry_date_utc", typeof(DateTimeOffset), newRFID.ExpiryDate);
            newRFID.Type = AssignValue(jObjectWhitelistRFID, $"{serialNumber}.type", typeof(string), newRFID.Type);

            return serialNumber;
        }
        private string? ParseOCPP(JObject jObjectOCPP, List<OCPPStatus> actualOCPPStatusList)
        {
            string device_uid = AssignValue(jObjectOCPP, "device_uid", typeof(string), null);
            if (device_uid == null)
                return null;

            OCPPStatus newOcppStatus = new OCPPStatus();

            bool isNewOCPPStatus = true;
            foreach (OCPPStatus ocppStatus in actualOCPPStatusList)
                if (ocppStatus.Device_uid == device_uid)
                {
                    newOcppStatus = ocppStatus;
                    isNewOCPPStatus = false;
                    break;
                }
            if (isNewOCPPStatus)
                actualOCPPStatusList.Add(newOcppStatus);

            newOcppStatus.Device_uid = device_uid;
            newOcppStatus.Status = AssignValue(jObjectOCPP, "state_61851", typeof(string), newOcppStatus.Status);
            newOcppStatus.OccpStatus = AssignValue(jObjectOCPP, "ocpp_status", typeof(string), newOcppStatus.OccpStatus);
            newOcppStatus.OccpStatusSentDate = AssignValue(jObjectOCPP, "ocpp_status_send", typeof(DateTimeOffset), newOcppStatus.OccpStatusSentDate);
            newOcppStatus.Operative = AssignValue(jObjectOCPP, "availability", typeof(bool), newOcppStatus.Operative);

            return device_uid;
        }
        private OCPPMessage ParseOCPPMessage(JObject jObjectOCPPMessage)
        {
            OCPPMessage newOcppMessage = new OCPPMessage();

            newOcppMessage.Timestamp = AssignValue(jObjectOCPPMessage, "utc_timestamp", typeof(DateTimeOffset), newOcppMessage.Timestamp);
            newOcppMessage.Type = AssignValue(jObjectOCPPMessage, "message_type", typeof(int), newOcppMessage.Type);
            newOcppMessage.Action = AssignValue(jObjectOCPPMessage, "message_action", typeof(string), newOcppMessage.Action);
            newOcppMessage.MessageData = AssignValue(jObjectOCPPMessage, "message_data", typeof(string), newOcppMessage.MessageData);
            newOcppMessage.FromLastHeartbeat = true;

            return newOcppMessage;
        }
        private Email ParseEmails(JObject jObjectEmail, int controllerId)
        {
            Email newEmail = new Email();
            newEmail.ChargeControllerId = controllerId;
            newEmail.EmailReceiver = AssignValue(jObjectEmail, "receiver_email", typeof(string), newEmail.EmailReceiver);
            newEmail.ReceiverName = AssignValue(jObjectEmail, "receiver_name", typeof(string), newEmail.ReceiverName);
            newEmail.Ftype = AssignValue(jObjectEmail, "ftype", typeof(string), newEmail.Ftype);
            newEmail.Rfid = AssignValue(jObjectEmail, "rfid", typeof(string), newEmail.Rfid);
            return newEmail;
        }
        private Transaction ParseTransaction(string transactionId, JObject jObjectTrasaction, int controllerId)
        {
            Transaction newTransaction = new Transaction();

            newTransaction.TransactionId = transactionId;
            newTransaction.ChargeControllerId = controllerId;
            newTransaction.ChargePointId = AssignValue(jObjectTrasaction, "charge_point_id", typeof(int), newTransaction.ChargePointId);
            newTransaction.ChargePointName = AssignValue(jObjectTrasaction, "charge_point_name", typeof(string), newTransaction.ChargePointName);
            newTransaction.RfidTag = AssignValue(jObjectTrasaction, "rfid_tag", typeof(string), newTransaction.RfidTag);
            newTransaction.RfidName = AssignValue(jObjectTrasaction, "rfid_name", typeof(string), newTransaction.RfidName);
            newTransaction.StartDay = AssignValue(jObjectTrasaction, "start_day", typeof(string), newTransaction.StartDay);
            newTransaction.StartMonth = AssignValue(jObjectTrasaction, "start_month", typeof(string), newTransaction.StartMonth);
            newTransaction.StartYear = AssignValue(jObjectTrasaction, "start_year", typeof(string), newTransaction.StartYear);
            newTransaction.StartDayOfWeek = AssignValue(jObjectTrasaction, "start_day_of_week", typeof(string), newTransaction.StartDayOfWeek);
            newTransaction.StartTime = AssignValue(jObjectTrasaction, "start_time", typeof(string), newTransaction.StartTime);
            newTransaction.EndTime = AssignValue(jObjectTrasaction, "end_time", typeof(string), newTransaction.EndTime);
            newTransaction.DurationDays = AssignValue(jObjectTrasaction, "duration_days", typeof(string), newTransaction.DurationDays);
            newTransaction.ConnectedTimeSec = AssignValue(jObjectTrasaction, "connected_time_sec", typeof(double), newTransaction.ConnectedTimeSec);
            newTransaction.ChargeTimeSec = AssignValue(jObjectTrasaction, "charge_time_sec", typeof(double), newTransaction.ChargeTimeSec);
            newTransaction.AveragePower = AssignValue(jObjectTrasaction, "average_power", typeof(double), newTransaction.AveragePower);
            newTransaction.ChargedEnergy = AssignValue(jObjectTrasaction, "charged_energy", typeof(double), newTransaction.ChargedEnergy);
            newTransaction.ChargedDistance = AssignValue(jObjectTrasaction, "charged_distance", typeof(double), newTransaction.ChargedDistance);
            DateTime createdDate;
            if (DateTime.TryParse($"{newTransaction.StartYear}-{newTransaction.StartMonth}-{newTransaction.StartDay}", out createdDate))
                newTransaction.CreatedDate = DateTime.SpecifyKind(createdDate, DateTimeKind.Utc);
            return newTransaction;
        }
        private dynamic? AssignValue(JObject jObj, string key, Type type, dynamic? defaultReturnValue)
        {
            try
            {
                if (type == typeof(string))
                {
                    var token = jObj.SelectToken(key);
                    return token == null ? defaultReturnValue : token.ToString();
                }
                else if (type == typeof(bool))
                {
                    bool boolReturn;
                    if (defaultReturnValue != null) boolReturn = defaultReturnValue;
                    if (bool.TryParse((string)jObj.SelectToken(key), out boolReturn))
                        return boolReturn;
                    return defaultReturnValue;
                }
                else if (type == typeof(long))
                {
                    long longReturn;
                    if (defaultReturnValue != null) longReturn = defaultReturnValue;
                    if (long.TryParse((string)jObj.SelectToken(key), out longReturn))
                        return longReturn;
                    return defaultReturnValue;
                }
                else if (type == typeof(int))
                {
                    int intReturn;
                    if (defaultReturnValue != null) intReturn = defaultReturnValue;
                    if (int.TryParse((string)jObj.SelectToken(key), out intReturn))
                        return intReturn;
                    return defaultReturnValue;
                }
                else if (type == typeof(double))
                {
                    double intReturn;
                    if (defaultReturnValue != null) intReturn = defaultReturnValue;
                    if (double.TryParse((string)jObj.SelectToken(key), out intReturn))
                        return intReturn;
                    return defaultReturnValue;
                }
                else if (type == typeof(JObject))
                {
                    JToken token = jObj.SelectToken(key);
                    return token == null ? defaultReturnValue : JObject.FromObject(token);
                }
                else if (type == typeof(JArray))
                {
                    JToken token = jObj.SelectToken(key);
                    return token == null ? defaultReturnValue : JArray.FromObject(token);
                }
                else if (type == typeof(DateTimeOffset))
                {
                    JToken token = jObj.SelectToken(key);
                    return token == null ? defaultReturnValue : DateTime.SpecifyKind(DateTime.Parse(token.ToString()), DateTimeKind.Utc);
                }
            } catch (Exception ex) {}

            return defaultReturnValue;
        }
        private JObject ParseObjectToJObject(object obj)
        {
            try
            {
                JObject jObj = JObject.FromObject(obj);
                if (jObj == null) throw new Exception();

                return jObj;

            } catch (Exception ex)
            {
                return new JObject();
            }
        }
    }
}