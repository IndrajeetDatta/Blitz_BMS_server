using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Sql.Library.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int ChargeControllerId { get; set; }
        public int ChargePointId { get; set; }
        public string TransactionId { get; set; }
        public string? ChargePointName { get; set; }
        public string? RfidTag { get; set; }
        public string? RfidName { get; set; }
        public string? StartDay { get; set; }
        public string? StartMonth { get; set; }
        public string? StartYear { get; set; }
        public string? StartDayOfWeek { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public string? DurationDays { get; set; }
        public double? ConnectedTimeSec { get; set; }
        public double? ChargeTimeSec { get; set; }
        public double? AveragePower { get; set; }
        public double? ChargedEnergy { get; set; }
        public double? ChargedDistance { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
    }
}
