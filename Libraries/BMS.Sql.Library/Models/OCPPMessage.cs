using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Sql.Library.Models
{
    public class OCPPMessage
    {
        // Keys
        public int Id { get; set; }
        public ChargeController ChargeController { get; set; }

        // Metadata

        public DateTimeOffset? Timestamp { get; set; }

        public int? Type { get; set; }

        public string? Action { get; set; }
        public string? MessageData { get; set; }
        public bool? FromLastHeartbeat { get; set; }
    }
}
