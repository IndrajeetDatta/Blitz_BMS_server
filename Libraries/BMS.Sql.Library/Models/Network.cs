namespace BMS.Sql.Library.Models
{
    public class Network
    {
        public int Id { get; set; }

        public ChargeController ChargingStation { get; set; }

        public string? Name { get; set; }
        public string? IPV4Address { get; set; }
        public string? IPV6Address { get; set; }
        public string? MACAddress { get; set; }

        public long? ReceivedBytes { get; set; }
        public long? TransmittedBytes { get; set; }
    }
}
