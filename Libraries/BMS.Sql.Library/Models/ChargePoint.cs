namespace BMS.Sql.Library.Models
{
    public class ChargePoint
    {
        // Keys

        public int Id { get; set; }
        public ChargeController ChargeController { get; set; }

        // Metadata

        public bool? Enabled { get; set; }

        public int? ChargingTimeInSeconds { get; set; }
        public int? ChargeCurrentMinimumInAmpers { get; set; }
        public int? ChargeCurrentMaximumInAmpers { get; set; }
        public int? FallbackCurrentInAmpers { get; set; }
        public int? FallbackTimeInSeconds { get; set; }
        public int? RFIDTimeoutInSeconds { get; set; }
        public int? OCPPConnectorId { get; set; }

        public decimal? ChargingRate { get; set; }

        public string? SerialNumber { get; set; }
        public string? ChargeControllerUid { get; set; }
        public string? ChargePointUid { get; set; }
        public string? Name { get; set; }
        public string? SoftwareVersion { get; set; }
        public string? Location { get; set; }

        public string? State { get; set; }
        public string? EnergyType { get; set; }
        public string? PhaseRotation { get; set; }
        public string? ReleaseChargingMode { get; set; }
        public string? RFIDReader { get; set; }
        public string? RFIDReaderType { get; set; }
        public string? HighLevelCommunication { get; set; }
        public string? SlaveSerialNumber { get; set; }
        public bool? ExternalRelease { get; set; }
        public string? CurrentI1 { get; set; }
        public string? CurrentI2 { get; set; }
        public string? CurrentI3 { get; set; }
        public string? VoltageU1 { get; set; }
        public string? VoltageU2 { get; set; }
        public string? VoltageU3 { get; set; }
        public string? totalEnergy { get; set; }
        public string? powerFactor { get; set; }
        public string? frequency { get; set; }
        public string? LocalBusState { get; set; }
        public string? ChargingDuration { get; set; }
        public string? PluginDuration { get; set; }
        public string? ChargingCurrentLimit { get; set; }
        public string? BusPosition { get; set; }
        public string? status { get; set; }
        public string? errorStatus { get; set; }
        public string? externalTemperature { get; set; }
    }

    public enum ChargePointState { A1, C2, E, B1 }
    public enum ConfiguredType { OCPP, Whitelist, Free }
    public enum EnergyType { CarloGavazziEM24 }
    public enum PhaseRotation { RSTL1L2L3 }
    public enum ChargingMode { LocalWhitelist }
    public enum RFIDReader { BPS11 }
    public enum RFIDReaderType { ELATECTWN4 }
    public enum HighLevelCommunication { Disabled }

}
