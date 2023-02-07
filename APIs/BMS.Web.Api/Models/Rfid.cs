namespace BMS.Web.Api.Controllers
{
    public partial class Rfid
    {
        public Rfid() { }

        public Rfid(Sql.Library.Models.RFID rfid)
        {
            if (rfid == null)
                return;
            Id = rfid.Id;
            Name = rfid.Name;
            AllowCharging = rfid.AllowCharging ?? false;
            EvConsumptionRateKWhPer100KM = rfid.EvConsumptionRateKWhPer100KM ?? 0;
            SerialNumber = rfid.SerialNumber;
            ExpiryDate = rfid.ExpiryDate;
            Type = rfid.Type;
        }
    }
}
