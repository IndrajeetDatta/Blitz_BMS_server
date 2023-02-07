using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Sql.Library.Models
{
    public class Email
    {
        public int Id { get; set; }
        public int ChargeControllerId { get; set; }
        public string Ftype { get; set; }
        public string EmailReceiver { get; set; }
        public string ReceiverName { get; set; }
        public string Rfid { get; set; }
    }
}
