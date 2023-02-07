using Newtonsoft.Json;

namespace BMS.Sql.Library.Models
{
    public class RFID
    {
        // Keys
        public int Id { get; set; }

        [JsonIgnore]
        public ChargeController ChargeController { get; set; }

        // Metadata

        public bool? AllowCharging { get; set; }

        public int? EvConsumptionRateKWhPer100KM { get; set; }

        public string? Name { get; set; }
        public string? SerialNumber { get; set; }

        public DateTimeOffset? ExpiryDate { get; set; }

        public string? Type { get; set; }
    }
}
