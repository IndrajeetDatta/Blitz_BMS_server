using BMS.Data.Api.Converters;
using Newtonsoft.Json;

namespace BMS.Data.Api.Models
{
    public class MqttData
    {
        public object Master { get; set; }
        public object Slaves { get; set; }
        public object Whitelist { get; set; }
        public object OCPP { get; set; }
        [JsonProperty("user-info")]
        public object UserInfo { get; set; }
        [JsonProperty("blitz-version-info")]
        public object BlitzVersionInfo { get; set; }
        [JsonProperty("transactions")]
        public object Transactions { get; set; }
        [JsonProperty("email-preferences")]
        public object Emails { get; set; }
    }
}
