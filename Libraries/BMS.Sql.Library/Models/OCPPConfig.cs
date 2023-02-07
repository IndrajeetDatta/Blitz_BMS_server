using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Sql.Library.Models
{
    public class OCPPConfig
    {
        // Keys
        public int Id { get; set; }

        [JsonIgnore]
        public ChargeController ChargeController { get; set; }
        public int ChargeControllerId { get; set; }


        // Metadata

        public string? OcppProtocolVersion { get; set; }
        public string? NetworkInterface { get; set; }
        public string? BackendURL { get; set; }
        public string? ServiceRFID { get; set; }
        public string? FreeModeRFID { get; set; }
        public bool? FreeMode { get; set; }
        public string? ChargeStationModel { get; set; }
        public string? ChargeStationVendor { get; set; }
        public string? ChargeStationSerialNumber { get; set; }
        public string? ChargeBoxID { get; set; }
        public string? ChargeBoxSerialNumber { get; set; }
        public string? Iccid { get; set; }
        public string? Imsi { get; set; }
        public string? MeterSerialNumber { get; set; }
        public string? MeterType { get; set; }
        public bool? ServiceRestart { get; set; }

    }
}
