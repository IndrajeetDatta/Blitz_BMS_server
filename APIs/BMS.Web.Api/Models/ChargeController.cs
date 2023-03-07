namespace BMS.Web.Api.Controllers
{
    public partial class ChargeController
    {
        public ChargeController() { }

        public ChargeController(Sql.Library.Models.ChargeController? chargingStation)
        {
            if (chargingStation == null) return;

            DateTime lastMaintenance;

            Id = chargingStation.Id;

            Client = new Client(chargingStation.Client);
            Installer = chargingStation.Installer != null ? new ApplicationUser(chargingStation.Installer) : new ApplicationUser();
            NetWorks = new List<Network>();
            if (chargingStation.Networks != null)
            {
                chargingStation.Networks.ForEach(network => NetWorks.Add(new Network(network)));
            }
            ChargePoints = new List<ChargePoint>();
            if (chargingStation.ChargePoints != null)
            {
                chargingStation.ChargePoints.ForEach(chargePoint => ChargePoints.Add(new ChargePoint(chargePoint)));
            }
            WhitelistRFIDs = new List<Rfid>();
            if (chargingStation.WhitelistRFIDs != null)
            {
                chargingStation.WhitelistRFIDs.ForEach(rfid => WhitelistRFIDs.Add(new Rfid(rfid)));
            }
            OcppMessages = new List<OcppMessage>();
            if (chargingStation.oCPPMessages != null)
            {
                chargingStation.oCPPMessages.ForEach(ocppMessage => OcppMessages.Add(new OcppMessage(ocppMessage)));
            }
            OcppStatus = new List<OcppStatus>();
            if (chargingStation.oCPPStatus != null)
            {
                chargingStation.oCPPStatus.ForEach(ocppStatus => OcppStatus.Add(new OcppStatus(ocppStatus)));
            }

            if (chargingStation.OCPPConfig != null)
                OcppConfig = new OcppConfig(chargingStation.OCPPConfig);

            if (chargingStation.UserData != null)
                UserData = new UserData(chargingStation.UserData);

            CompanyName = chargingStation.CompanyName;
            NetworkConnection = chargingStation.NetworkConnection ?? false;
            OcppConnection = chargingStation.OCPPConnection;
            SimActive = chargingStation.SimActive ?? false;
            UptimeInSeconds = chargingStation.UptimeInSeconds ?? 0;
            SerialNumber = chargingStation.SerialNumber;
            Address = chargingStation.Address;
            LocationData = chargingStation.LocationData;
            DateInstalled = chargingStation.DateInstalled;
            if (DateTime.TryParse(chargingStation.LastMaintenance.ToString(), out lastMaintenance))
            {
                LastMaintenance = lastMaintenance;
            }
            SystemTime = chargingStation.SystemTime;
            Heartbeat = chargingStation.Heartbeat;
            FullHeartbeat = chargingStation.FullHeartbeat;
            NetworkType = chargingStation.NetworkType;
            Eth0DHCP = chargingStation.ETH0DHCP ?? false;
            Eth0IPAddress = chargingStation.ETH0IPAddress;
            Eth0SubnetMask = chargingStation.ETH0SubnetMask;
            Eth0Gateway = chargingStation.ETH0Gateway;
            Eth0NoGateway = chargingStation.ETH0NoGateway ?? false;
            ModemServiceActive = chargingStation.ModemServiceActive ?? false;
            ModemPreferOverETH0 = chargingStation.ModemPreferOverETH0 ?? false;
            ModemDefaultRoute = chargingStation.ModemDefaultRoute ?? false;
            ModemSignalRSSI = chargingStation.ModemSignalRSSI ?? 0;
            ModemSignalCQI = chargingStation.ModemSignalCQI ?? 0;
            ModemReceivedBytes = chargingStation.ModemReceivedBytes ?? 0;
            ModemTransmittedBytes = chargingStation.ModemTransmittedBytes ?? 0;
            ModemAPN = chargingStation.ModemAPN;
            ModemSimPin = chargingStation.ModemSimPin;
            UseAccessCredentials = chargingStation.UseAccessCredentials ?? false;
            ModemUsername = chargingStation.ModemUsername;
            ModemPassword = chargingStation.ModemPassword;
            ModemIPAddress = chargingStation.ModemIPAddress;
            ModemPrimaryDNSServer = chargingStation.ModemPrimaryDNSServer;
            ModemSecondaryDNSServer = chargingStation.ModemSecondaryDNSServer;
            ModemIMSI = chargingStation.ModemIMSI;
            ModemICCID = chargingStation.ModemICCID;
            ModemExtendedReport = chargingStation.ModemExtendedReport;
            ModemConnectionStatus = chargingStation.ModemConnectionStatus;
            ModemProvider = chargingStation.ModemProvider;
            ModemRegistrationStatus = chargingStation.ModemRegistrationStatus;
            ModemRoamingStatus = chargingStation.ModemRoamingStatus;
            ModemSignalQuality = chargingStation.ModemSignalQuality;
            ModemRadioTechnology = chargingStation.ModemRadioTechnology;
            ModemSimStatus = chargingStation.ModemSimStatus;
            CpuTemperatureCelsius = chargingStation.CPUTemperatureCelsius ?? 0;
            CpuUtilizationPercentage = chargingStation.CPUUtilizationPercentage ?? 0;
            RamAvailableBytes = chargingStation.RAMAvailableBytes ?? 0;
            RamTotalBytes = chargingStation.RAMTotalBytes ?? 0;
            RamUsedBytes = chargingStation.RAMUsedBytes ?? 0;
            DataUtilizationPercentage = chargingStation.DataUtilizationPercentage ?? 0;
            DataTotalBytes = chargingStation.DataTotalBytes ?? 0;
            LogUtilizationPercentage = chargingStation.LogUtilizationPercentage ?? 0;
            LogTotalBytes = chargingStation.LogTotalBytes ?? 0;
            VarVolatileUtilizationPercentage = chargingStation.VarVolatileUtilizationPercentage ?? 0;
            VarVolatileTotalBytes = chargingStation.VarVolatileTotalBytes ?? 0;
            SystemMonitorVersion = chargingStation.SystemMonitorVersion;
            SystemMonitorStatus = chargingStation.SystemMonitorStatus;
            ControllerAgentVersion = chargingStation.ControllerAgentVersion;
            ControllerAgent_Status = chargingStation.ControllerAgent_Status;
            Ocpp16_Version = chargingStation.OCPP16_Version;
            Ocpp16Status = chargingStation.OCPP16Status;
            ModbusClientVersion = chargingStation.ModbusClientVersion;
            ModbusClientStatus = chargingStation.ModbusClientStatus;
            ModbusServerVersion = chargingStation.ModbusServerVersion;
            ModbusServerStatus = chargingStation.ModbusServerStatus;
            JupicoreVersion = chargingStation.JupicoreVersion;
            JupicoreStatus = chargingStation.JupicoreStatus;
            LoadManagementVersion = chargingStation.LoadManagementVersion;
            WebserverVersion = chargingStation.WebserverVersion;
            LoadManagementActive = chargingStation.LoadManagementActive ?? false;
            ChargingParkName = chargingStation.ChargingParkName;
            LoadCircuitFuse = chargingStation.LoadCircuitFuse;
            HighLevelMeasuringDeviceControllerId = chargingStation.HighLevelMeasuringDeviceControllerId;
            HighLevelMeasuringDeviceModbus = chargingStation.HighLevelMeasuringDeviceModbus;
            MeasuringDeviceType = chargingStation.MeasuringDeviceType;
            LoadStrategy = chargingStation.LoadStrategy;
            CurrentI1 = chargingStation.CurrentI1;
            CurrentI2 = chargingStation.CurrentI2;
            CurrentI3 = chargingStation.CurrentI3;
            PlannedCurrentI1 = chargingStation.PlannedCurrentI1;
            PlannedCurrentI2 = chargingStation.PlannedCurrentI2;
            PlannedCurrentI3 = chargingStation.PlannedCurrentI3;
            SupervisionMeterCurrentI1 = chargingStation.SupervisionMeterCurrentI1;
            SupervisionMeterCurrentI2 = chargingStation.SupervisionMeterCurrentI2;
            SupervisionMeterCurrentI3 = chargingStation.SupervisionMeterCurrentI3;
            MonitoredCps = chargingStation.MonitoredCps;
            OsReleaseVersion = chargingStation.OsReleaseVersion;
            WebAppVersion = chargingStation.WebAppVersion;
            AllowTestModeCommands = chargingStation.AllowTestModeCommands ?? false;
            Transactions = new List<Transaction>();
            if (chargingStation.Transactions != null)
            {
                chargingStation.Transactions.ForEach(transaction => Transactions.Add(new Transaction(transaction)));
            }
            LoadManagementIpAddress = chargingStation.LoadManagementIpAddress;
        }
    }
}
