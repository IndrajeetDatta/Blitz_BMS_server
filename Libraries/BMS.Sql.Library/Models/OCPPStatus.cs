using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Sql.Library.Models
{
    public class OCPPStatus
    {
        // Keys
        public int Id { get; set; }
        public ChargeController ChargeController { get; set; }

        // Metadata

        public string? Status { get; set; }
        public string? Device_uid { get; set; }
        public string? OccpStatus { get; set; }
        public DateTimeOffset? OccpStatusSentDate { get; set; }
        public bool? Operative { get; set; }
    }
}
