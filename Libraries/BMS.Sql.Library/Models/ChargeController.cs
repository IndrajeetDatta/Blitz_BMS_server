using System.Linq.Expressions;

namespace BMS.Sql.Library.Models
{
    public class ChargeController
    {
        #region Keys

        public int Id { get; set; }

        public Client Client { get; set; }
        public ApplicationUser? Installer { get; set; }

        public List<Network> Networks { get; set; }
        public List<ChargePoint> ChargePoints { get; set; }
        public List<RFID> WhitelistRFIDs { get; set; }

        public OCPPConfig OCPPConfig { get; set; }
        public UserData UserData { get; set; }
        public List<OCPPStatus> oCPPStatus { get; set; }
        public List<OCPPMessage> oCPPMessages { get; set; }
        public List<Transaction> Transactions { get; set; }
        public List<Email> Emails { get; set; }
        #endregion

        public string? CompanyName { get; set; }

        #region Metadata

        public string? OsReleaseVersion { get; set; }
        public bool? NetworkConnection { get; set; }
        public string? OCPPConnection { get; set; }
        public bool? SimActive { get; set; }

        public long? UptimeInSeconds { get; set; }

        public string? SerialNumber { get; set; }
        public string? Address { get; set; }
        public string? LocationData { get; set; }

        public DateTimeOffset? DateInstalled { get; set; }
        public DateTimeOffset? LastMaintenance { get; set; }
        public DateTimeOffset? SystemTime { get; set; }
        public DateTimeOffset? Heartbeat { get; set; }

        public string? NetworkType { get; set; }

        #endregion

        #region ETH0

        public bool? ETH0DHCP { get; set; }

        public string? ETH0IPAddress { get; set; }
        public string? ETH0SubnetMask { get; set; }
        public string? ETH0Gateway { get; set; }
        public bool? ETH0NoGateway { get; set; }

        #endregion

        #region Modem

        public bool? ModemServiceActive { get; set; }
        public bool? ModemPreferOverETH0 { get; set; }
        public bool? ModemDefaultRoute { get; set; }

        public int? ModemSignalRSSI { get; set; }
        public int? ModemSignalCQI { get; set; }

        public long? ModemReceivedBytes { get; set; }
        public long? ModemTransmittedBytes { get; set; }

        public string? ModemAPN { get; set; }
        public string? ModemSimPin { get; set; }
        public bool? UseAccessCredentials { get; set; }
        public string? ModemUsername { get; set; }
        public string? ModemPassword { get; set; }
        public string? ModemIPAddress { get; set; }
        public string? ModemPrimaryDNSServer { get; set; }
        public string? ModemSecondaryDNSServer { get; set; }
        public string? ModemIMSI { get; set; }
        public string? ModemICCID { get; set; }
        public string? ModemExtendedReport { get; set; }
        public string? ModemConnectionStatus { get; set; }
        public string? ModemProvider { get; set; }
        public string? ModemRegistrationStatus { get; set; }
        public string? ModemRoamingStatus { get; set; }
        public string? ModemSignalQuality { get; set; }
        public string? ModemRadioTechnology { get; set; }
        public string? ModemSimStatus { get; set; }


        #endregion

        #region System

        public int? CPUTemperatureCelsius { get; set; }
        public int? CPUUtilizationPercentage { get; set; }

        public long? RAMAvailableBytes { get; set; }
        public long? RAMTotalBytes { get; set; }
        public long? RAMUsedBytes { get; set; }

        public int? DataUtilizationPercentage { get; set; }
        public long? DataTotalBytes { get; set; }

        public int? LogUtilizationPercentage { get; set; }
        public long? LogTotalBytes { get; set; }

        public int? VarVolatileUtilizationPercentage { get; set; }
        public long? VarVolatileTotalBytes { get; set; }

        #endregion

        #region Applications

        public string? SystemMonitorVersion { get; set; }
        public string? SystemMonitorStatus { get; set; }

        public string? ControllerAgentVersion { get; set; }
        public string? ControllerAgent_Status { get; set; }

        public string? OCPP16_Version { get; set; }
        public string? OCPP16Status { get; set; }

        public string? ModbusClientVersion { get; set; }
        public string? ModbusClientStatus { get; set; }

        public string? ModbusServerVersion { get; set; }
        public string? ModbusServerStatus { get; set; }

        public string? JupicoreVersion { get; set; }
        public string? JupicoreStatus { get; set; }

        public string? LoadManagementVersion { get; set; }
        public string? LoadManagementStatus { get; set; }

        public string? WebserverVersion { get; set; }
        public string? WebserverStatus { get; set; }

        #endregion

        #region Load Management

        public bool? LoadManagementActive { get; set; }
        public string? ChargingParkName { get; set; }
        public string? LoadCircuitFuse { get; set; }
        public string? MeasuringDeviceType { get; set; }
        public string? LoadStrategy { get; set; }
        public string? CurrentI1 { get; set; }
        public string? CurrentI2 { get; set; }
        public string? CurrentI3 { get; set; }
        public string? PlannedCurrentI1 { get; set; }
        public string? PlannedCurrentI2 { get; set; }
        public string? PlannedCurrentI3 { get; set; }
        public string? SupervisionMeterCurrentI1 { get; set; }
        public string? SupervisionMeterCurrentI2 { get; set; }
        public string? SupervisionMeterCurrentI3 { get; set; }
        public string? MonitoredCps { get; set; }
        public string? HighLevelMeasuringDeviceModbus { get; set; }
        public string? HighLevelMeasuringDeviceControllerId { get; set; }

        #endregion

        public string? WebAppVersion {get; set;}
        public bool? AllowTestModeCommands { get; set; }
    }

    public enum NetworkType { LAN }
    public enum ConnectionStatus { Connected }
    public enum CelullarProvider { Orange }
    public enum RegistrationStatus { Registered }
    public enum RoamingStatuse { Roaming }
    public enum SingnalQuality { Ok }
    public enum RadioTechnology { LTE }
    public enum SimStatus { Ready }
    public enum ApplicationStatus { Running }
}
