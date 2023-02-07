namespace BMS.Data.Api.Models
{
    public class Slave
    {
        public object Connection { get; set; }
        public object System { get; set; }
        public object Network { get; set; }
        public object Devices { get; set; }
        public object Applications { get; set; }
        public object Charging_Controllers { get; set; }
    }
}
