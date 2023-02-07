using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Sql.Library.Models
{
    public class Command
    {
        public Command() { }
        public Command(
            string? masterId,
            string? chargePointUid,
            string? chargeControllerUid,
            string? rfidSerialNumber,
            string? commandType,
            string? name,
            string? method,
            string? masterUrl,
            string? payload,
            int? port,
            bool? tokenRequired
        )
        {
            DateTime dateNow = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

            MasterId = masterId;
            ChargePointUid = chargePointUid;
            ChargeControllerUid = chargeControllerUid;
            RfidSerialNumber = rfidSerialNumber;
            CommandType = commandType;
            Name = name;
            Method = method;
            MasterUrl = masterUrl;
            Payload = payload;
            Status = CommandStatus.Pending;
            TokenRequired = tokenRequired ?? false;
            Port = port ?? 0;
            ErrorMessage = string.Empty;
            CreatedDate = dateNow;
            ProcessedDate = null; //dateNow;
        }

        // Keys
        public int Id { get; set; }

        // Metadata
        public string? MasterId { get; set; }
        public string? ChargePointUid { get; set; }
        public string? ChargeControllerUid { get; set; }
        public string? RfidSerialNumber { get; set; }
        public string? CommandType { get; set; }
        public CommandStatus? Status { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Name { get; set; }
        public string? Method { get; set; }
        public string? MasterUrl { get; set; }
        public string? Payload { get; set; }
        public int? Port { get; set; }
        public bool? TokenRequired { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public DateTimeOffset? ProcessedDate { get; set; }
    }
    public enum CommandStatus
    {
        Pending,
        Successfull,
        Failed,
        Processing,
        Timeout
    }
}
