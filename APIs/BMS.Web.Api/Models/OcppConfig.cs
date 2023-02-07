namespace BMS.Web.Api.Controllers
{
    public partial class OcppConfig
    {
        public OcppConfig() { }

        public OcppConfig(Sql.Library.Models.OCPPConfig? ocppConfig)
        {
            if (ocppConfig == null)
                return;
            Id = ocppConfig.Id;
            if (ocppConfig.ChargeController != null)
                ChargeController = new ChargeController(ocppConfig.ChargeController);
            ChargeControllerId = ocppConfig.ChargeControllerId;
            OcppProtocolVersion = ocppConfig.OcppProtocolVersion;
            NetworkInterface = ocppConfig.NetworkInterface;
            BackendURL = ocppConfig.BackendURL;
            ServiceRFID = ocppConfig.ServiceRFID;
            FreeModeRFID = ocppConfig.FreeModeRFID;
            FreeMode = ocppConfig.FreeMode ?? false;
            ChargeStationModel = ocppConfig.ChargeStationModel;
            ChargeStationVendor = ocppConfig.ChargeStationVendor;
            ChargeStationSerialNumber = ocppConfig.ChargeStationSerialNumber;
            ServiceRestart = ocppConfig.ServiceRestart ?? false;
            MeterType = ocppConfig.MeterType;
            MeterSerialNumber = ocppConfig.MeterSerialNumber;
            Imsi = ocppConfig.Imsi;
            Iccid = ocppConfig.Iccid;
            ChargeBoxSerialNumber = ocppConfig.ChargeBoxSerialNumber;
            ChargeBoxID = ocppConfig.ChargeBoxID;
        }
    }
}
