namespace BMS.Web.Api.Controllers
{
    public partial class Network
    {
        public Network() { }

        public Network(Sql.Library.Models.Network network)
        {
            if (network == null)
                return;
            Id = network.Id;
            Name = network.Name;
            if (network.ChargingStation != null)
                ChargingStationId = network.ChargingStation.Id;
            IpV4Address = network.IPV4Address;
            IpV6Address = network.IPV6Address;
            MacAddress = network.MACAddress;
            ReceivedBytes = network.ReceivedBytes ?? 0;
            TransmittedBytes = network.TransmittedBytes ?? 0;

        }
    }
}
