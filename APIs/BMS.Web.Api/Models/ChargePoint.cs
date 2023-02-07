namespace BMS.Web.Api.Controllers
{
    public partial class ChargePoint
    {
        public ChargePoint() { }

        public ChargePoint(Sql.Library.Models.ChargePoint chargePoint)
        {
            if (chargePoint == null) return;

            Id = chargePoint.Id;
            ChargeController = new ChargeController(chargePoint.ChargeController);
            Enabled = chargePoint.Enabled ?? false;
            ChargingTimeInSeconds = chargePoint.ChargingTimeInSeconds ?? 0;
            ChargeCurrentMinimumInAmpers = chargePoint.ChargeCurrentMinimumInAmpers ?? 0;
            ChargeCurrentMaximumInAmpers = chargePoint.ChargeCurrentMaximumInAmpers ?? 0;
            FallbackCurrentInAmpers = chargePoint.FallbackCurrentInAmpers ?? 0;
            FallbackTimeInSeconds = chargePoint.FallbackTimeInSeconds ?? 0;
            RfidTimeoutInSeconds = chargePoint.RFIDTimeoutInSeconds ?? 0;
            OcppConnectorId = chargePoint.OCPPConnectorId ?? 0;
            ChargingRate = (float)(chargePoint.ChargingRate ?? 0);
            SerialNumber = chargePoint.SerialNumber;
            ChargePointUid = chargePoint.ChargePointUid;
            ChargeControllerUid = chargePoint.ChargeControllerUid;
            Name = chargePoint.Name;
            SoftwareVersion = chargePoint.SoftwareVersion;
            Location = chargePoint.Location;
            State = chargePoint.State;
            EnergyType = chargePoint.EnergyType;
            PhaseRotation = chargePoint.PhaseRotation;
            ReleaseChargingMode = chargePoint.ReleaseChargingMode;
            RfidReader = chargePoint.RFIDReader;
            RfidReaderType = chargePoint.RFIDReaderType;
            HighLevelCommunication = chargePoint.HighLevelCommunication;
            ExternalRelease = chargePoint.ExternalRelease ?? false;
        }
    }
}
