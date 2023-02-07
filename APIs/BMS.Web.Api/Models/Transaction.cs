namespace BMS.Web.Api.Controllers
{
    public partial class Transaction
    {
        public Transaction() { }

        public Transaction(Sql.Library.Models.Transaction transaction)
        {
            if (transaction == null)
                return;
            Id = transaction.Id;
            ChargeControllerId = transaction.ChargeControllerId;
            ChargePointId = transaction.ChargePointId;
            ChargePointName = transaction.ChargePointName;
            RfidTag = transaction.RfidTag;
            RfidName = transaction.RfidName;
            StartDay = transaction.StartDay;
            StartMonth = transaction.StartMonth;
            StartYear = transaction.StartYear;
            StartDayOfWeek = transaction.StartDayOfWeek;
            StartTime = transaction.StartTime;
            EndTime = transaction.EndTime;
            DurationDays = transaction.DurationDays;
            ConnectedTimeSec = transaction.ConnectedTimeSec ?? 0;
            ChargeTimeSec = transaction.ChargeTimeSec ?? 0;
            AveragePower = transaction.AveragePower ?? 0;
            ChargedEnergy = transaction.ChargedEnergy ?? 0;
            ChargedDistance = transaction.ChargedDistance ?? 0;
            TransactionId = transaction.TransactionId;
            CreatedDate = transaction.CreatedDate;
        }

        public Sql.Library.Models.Transaction ToSqlModel()
        {
            return new Sql.Library.Models.Transaction
            {
                Id = Id,
                ChargeControllerId = ChargeControllerId,
                ChargePointId = ChargePointId,
                ChargePointName = ChargePointName,
                RfidTag = RfidTag,
                RfidName = RfidName,
                StartDay = StartDay,
                StartMonth = StartMonth,
                StartYear = StartYear,
                StartDayOfWeek = StartDayOfWeek,
                StartTime = StartTime,
                EndTime = EndTime,
                DurationDays = DurationDays,
                ConnectedTimeSec = ConnectedTimeSec,
                ChargeTimeSec = ChargeTimeSec,
                AveragePower = AveragePower,
                ChargedEnergy = ChargedEnergy,
                ChargedDistance = ChargedDistance,
        };
        }
    }
}
