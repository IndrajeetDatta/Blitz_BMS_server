using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Sql.Library.Models
{
    public class Log
    {
        public int Id { get; set; }

        public string? ErrorMessage { get; set; }
        public string? JsonData { get; set; }
        public string? IpRequest { get; set; }
        public int? Status { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
    }
}
