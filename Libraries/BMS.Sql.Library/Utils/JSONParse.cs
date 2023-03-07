using BMS.Sql.Library.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Sql.Library.Utils
{
    public class JSONParse
    {
        public static Dictionary<string, string> ConvertChargePointFields()
        {
            ChargePoint chargePoint = new ChargePoint();
            Dictionary<string, string> ret = new Dictionary<string, string>();

            ret.Add(nameof(chargePoint.Id).ToLower(), "id");
            ret.Add(nameof(chargePoint.Enabled).ToLower(), "charge_enable");
            ret.Add(nameof(chargePoint.ChargingTimeInSeconds).ToLower(), "charge_time_sec");
            ret.Add(nameof(chargePoint.ChargeCurrentMinimumInAmpers).ToLower(), "minimum_charge_current");
            ret.Add(nameof(chargePoint.ChargeCurrentMaximumInAmpers).ToLower(), "maximum_charge_current");
            ret.Add(nameof(chargePoint.FallbackCurrentInAmpers).ToLower(), "fallback_charge_current");
            ret.Add(nameof(chargePoint.FallbackTimeInSeconds).ToLower(), "fallback_charge_current_in_seconds");
            ret.Add(nameof(chargePoint.RFIDTimeoutInSeconds).ToLower(), "rfid_charge_grant_timeout_sec");
            ret.Add(nameof(chargePoint.OCPPConnectorId).ToLower(), "ocpp16_connector_id");
            ret.Add(nameof(chargePoint.ChargingRate).ToLower(), "real_power");
            ret.Add(nameof(chargePoint.SerialNumber).ToLower(), "serial_number");
            ret.Add(nameof(chargePoint.ChargeControllerUid).ToLower(), "charging_controller_uid");
            ret.Add(nameof(chargePoint.ChargePointUid).ToLower(), "charging_point_uid");
            ret.Add(nameof(chargePoint.Name).ToLower(), "charging_point_name");
            ret.Add(nameof(chargePoint.SoftwareVersion).ToLower(), "firmware_version");
            ret.Add(nameof(chargePoint.Location).ToLower(), "location");
            ret.Add(nameof(chargePoint.State).ToLower(), "iec_61851_state");
            ret.Add(nameof(chargePoint.EnergyType).ToLower(), "energy_meter_type");
            ret.Add(nameof(chargePoint.PhaseRotation).ToLower(), "connector_phase_rotation");
            ret.Add(nameof(chargePoint.ReleaseChargingMode).ToLower(), "release_charging_mode");
            ret.Add(nameof(chargePoint.RFIDReader).ToLower(), "rfid_reader_device_uid");
            ret.Add(nameof(chargePoint.RFIDReaderType).ToLower(), "rfid_reader_type");
            ret.Add(nameof(chargePoint.HighLevelCommunication).ToLower(), "evse_hlc_policy");
            ret.Add(nameof(chargePoint.ExternalRelease).ToLower(), "external_release");
            return ret;
        }
        public static Dictionary<string, string> ConvertChargeControllerFields()
        {
            ChargeController chargeController = new ChargeController();
            Dictionary<string, string> ret = new Dictionary<string, string>();

            ret.Add(nameof(chargeController.ModemServiceActive).ToLower(), "enabled");
            ret.Add(nameof(chargeController.ModemAPN).ToLower(), "apn");
            ret.Add(nameof(chargeController.UseAccessCredentials).ToLower(), "useaccesscredentials");
            ret.Add(nameof(chargeController.ModemUsername).ToLower(), "username");
            ret.Add(nameof(chargeController.ModemPassword).ToLower(), "password");
            ret.Add(nameof(chargeController.ModemSimPin).ToLower(), "pin");
            ret.Add(nameof(chargeController.ModemDefaultRoute).ToLower(), "defaultroute");
            ret.Add(nameof(chargeController.ModemPreferOverETH0).ToLower(), "prefermodem");
            ret.Add(nameof(chargeController.ETH0DHCP).ToLower(), "dhcp");
            ret.Add(nameof(chargeController.ETH0IPAddress).ToLower(), "addresses");
            ret.Add(nameof(chargeController.ETH0SubnetMask).ToLower(), "netmask");
            ret.Add(nameof(chargeController.ETH0Gateway).ToLower(), "gateway");
            ret.Add(nameof(chargeController.ETH0NoGateway).ToLower(), "nogateway");
            ret.Add(nameof(chargeController.ChargingParkName).ToLower(), "load_circuit_name");
            ret.Add(nameof(chargeController.LoadCircuitFuse).ToLower(), "fuse");
            ret.Add(nameof(chargeController.HighLevelMeasuringDeviceModbus).ToLower(), "modbus_id");
            ret.Add(nameof(chargeController.MeasuringDeviceType).ToLower(), "measuring_device_type");
            ret.Add(nameof(chargeController.LoadManagementIpAddress).ToLower(), "ip_address");
            ret.Add(nameof(chargeController.LoadStrategy).ToLower(), "charging_rule");
            ret.Add(nameof(chargeController.ChargePoints).ToLower(), "charging_points");
            return ret;
        }
        public static Dictionary<string, string> ConvertRFIDFields()
        {
            RFID rfid = new RFID();
            Dictionary<string, string> ret = new Dictionary<string, string>();

            ret.Add(nameof(rfid.AllowCharging).ToLower(), "active");
            ret.Add(nameof(rfid.Name).ToLower(), "label");
            ret.Add(nameof(rfid.ExpiryDate).ToLower(), "expiry_date_utc");
            ret.Add(nameof(rfid.Type).ToLower(), "type");
            return ret;
        }
        public static Dictionary<string, string> ConvertOcppConfigFields()
        {
            OCPPConfig ocppConfig = new OCPPConfig();
            Dictionary<string, string> ret = new Dictionary<string, string>();

            ret.Add(nameof(ocppConfig.BackendURL).ToLower(), "BackendURL");
            ret.Add(nameof(ocppConfig.ChargeBoxID).ToLower(), "ChargeBoxID");
            ret.Add(nameof(ocppConfig.ChargeBoxSerialNumber).ToLower(), "ChargeBoxSerialNumber");
            ret.Add(nameof(ocppConfig.ChargeStationModel).ToLower(), "ChargePointModel");
            ret.Add(nameof(ocppConfig.ChargeStationSerialNumber).ToLower(), "ChargePointSerialNumber");
            ret.Add(nameof(ocppConfig.ChargeStationVendor).ToLower(), "ChargePointVendor");
            ret.Add(nameof(ocppConfig.OcppProtocolVersion).ToLower(), "FirmwareVersion");
            ret.Add(nameof(ocppConfig.FreeMode).ToLower(), "FreeMode");
            ret.Add(nameof(ocppConfig.FreeModeRFID).ToLower(), "FreeModeUID");
            ret.Add(nameof(ocppConfig.Iccid).ToLower(), "Iccid");
            ret.Add(nameof(ocppConfig.Imsi).ToLower(), "Imsi");
            ret.Add(nameof(ocppConfig.NetworkInterface).ToLower(), "Interface");
            ret.Add(nameof(ocppConfig.MeterSerialNumber).ToLower(), "MeterSerialNumber");
            ret.Add(nameof(ocppConfig.MeterType).ToLower(), "MeterType");
            ret.Add(nameof(ocppConfig.ServiceRestart).ToLower(), "ServiceRestart");
            ret.Add(nameof(ocppConfig.ServiceRFID).ToLower(), "ServiceUID");
            return ret;
        }
        // fieldsToModify == null => modify all fields
        public static string ConvertToHeartbeatField(Type SQLModel, string stringJson, List<string>? fieldsToModify = null)
        {
            try
            {
                JObject json = JObject.Parse(stringJson);
                JObject returnJson = new JObject();
                Dictionary<string, string> convertDict = new Dictionary<string, string>();

                if (SQLModel == typeof(ChargeController))
                    convertDict = ConvertChargeControllerFields();
                else if (SQLModel == typeof(ChargePoint))
                    convertDict = ConvertChargePointFields();
                else if (SQLModel == typeof(RFID))
                {
                    convertDict = ConvertRFIDFields();
                    JToken? expiryDate = null;
                    if (json.TryGetValue("expiryDate", out expiryDate) || json.TryGetValue("ExpiryDate", out expiryDate))
                    {
                        DateTime dateTime;
                        if (DateTime.TryParse(expiryDate.ToString(), out dateTime))
                        {
                            returnJson.Add("expiry_date", $"{dateTime.Year}-{dateTime.Month}-{dateTime.Day}");
                            returnJson.Add("expiry_time", $"{dateTime.Hour}:{dateTime.Minute}:{dateTime.Second}");
                        }
                    }
                }
                else if (SQLModel == typeof(OCPPConfig))
                    convertDict = ConvertOcppConfigFields();

                foreach (JProperty property in json.Properties())
                {
                    string prop = property.Name.ToLower();
                    string convertedProp = "" ;
                    if ((fieldsToModify == null || fieldsToModify.Contains(prop)) && convertDict.TryGetValue(prop, out convertedProp))
                        if (property.Value == null || String.IsNullOrEmpty(property.Value.ToString()))
                            returnJson.Add(convertedProp, "");
                        else returnJson.Add(convertedProp, property.Value);
                   
                }

                return returnJson.ToString();
            } catch (Exception ex) { }

            return "-";
        }
        public static bool UpdateChargePoint(ChargePoint chargePoint, JObject json)
        {
            try
            {
                foreach (JProperty property in json.Properties())
                {
                    string prop = property.Name;
                    if (prop == "id")
                        continue;
                    if (prop == "charge_enable._v_")
                        chargePoint.Enabled = ConvertToken(property.Value, typeof(bool), chargePoint.Enabled);
                    else if (prop == "charge_time_sec._v_")
                        chargePoint.ChargingTimeInSeconds = ConvertToken(property.Value, typeof(int), chargePoint.ChargingTimeInSeconds);
                    else if (prop == "minimum_charge_current")
                        chargePoint.ChargeCurrentMinimumInAmpers = ConvertToken(property.Value, typeof(int), chargePoint.ChargeCurrentMinimumInAmpers);
                    else if (prop == "maximum_charge_current")
                        chargePoint.ChargeCurrentMaximumInAmpers = ConvertToken(property.Value, typeof(int), chargePoint.ChargeCurrentMaximumInAmpers);
                    else if (prop == "fallback_charge_current")
                        chargePoint.FallbackCurrentInAmpers = ConvertToken(property.Value, typeof(int), chargePoint.FallbackCurrentInAmpers);
                    else if (prop == "fallback_charge_current._v_")
                        chargePoint.FallbackTimeInSeconds = ConvertToken(property.Value, typeof(int), chargePoint.FallbackTimeInSeconds);
                    else if (prop == "RfidChargeGrantTimeoutSec")
                        chargePoint.RFIDTimeoutInSeconds = ConvertToken(property.Value, typeof(int), chargePoint.RFIDTimeoutInSeconds);
                    else if (prop == "ocpp16_connector_id._v_")
                        chargePoint.OCPPConnectorId = ConvertToken(property.Value, typeof(int), chargePoint.OCPPConnectorId);
                    else if (prop == "real_power.value")
                    {
                        decimal chargingRate = decimal.Parse((string)property.Value);
                        chargePoint.ChargingRate = chargingRate == 0 ? chargePoint.ChargingRate : Math.Round(chargingRate / 1000, 2);
                    }
                    else if (prop == "charging_point_name._v_")
                        chargePoint.Name = ConvertToken(property.Value, typeof(string), chargePoint.Name);
                    else if (prop == "firmware_version")
                        chargePoint.SoftwareVersion = ConvertToken(property.Value, typeof(string), chargePoint.SoftwareVersion);
                    else if (prop == "location._v_")
                        chargePoint.Location = ConvertToken(property.Value, typeof(string), chargePoint.Location);
                    else if (prop == "iec_61851_state._v_")
                        chargePoint.State = ConvertToken(property.Value, typeof(string), chargePoint.State);
                    else if (prop == "energy_meter_type._v_")
                        chargePoint.EnergyType = ConvertToken(property.Value, typeof(string), chargePoint.EnergyType);
                    else if (prop == "connector_phase_rotation._v_")
                        chargePoint.PhaseRotation = ConvertToken(property.Value, typeof(string), chargePoint.PhaseRotation);
                    else if (prop == "release_charging_mode._v_")
                        chargePoint.ReleaseChargingMode = ConvertToken(property.Value, typeof(string), chargePoint.ReleaseChargingMode);
                    else if (prop == "rfid_reader_device_uid._v_")
                        chargePoint.RFIDReader = ConvertToken(property.Value, typeof(string), chargePoint.RFIDReader);
                    else if (prop == "rfid_reader_type._v_")
                        chargePoint.RFIDReaderType = ConvertToken(property.Value, typeof(string), chargePoint.RFIDReaderType);
                    else if (prop == "evse_hlc_policy._v_")
                        chargePoint.HighLevelCommunication = ConvertToken(property.Value, typeof(string), chargePoint.HighLevelCommunication);
                    else if (prop == "external_release._v_")
                        chargePoint.ExternalRelease = ConvertToken(property.Value, typeof(bool), chargePoint.ExternalRelease);

                }

                return true;
            }
            catch (Exception ex) { }

            return false;
        }

        public static dynamic? ConvertToken(JToken token, Type type, dynamic? defaultReturnValue)
        {
            try
            {
                if (type == typeof(string))
                {
                    return token == null ? defaultReturnValue : token.ToString();
                }
                else if (type == typeof(bool))
                {
                    bool boolReturn;
                    if (defaultReturnValue != null) boolReturn = defaultReturnValue;
                    if (bool.TryParse((string)token, out boolReturn))
                        return boolReturn;
                    return defaultReturnValue;
                }
                else if (type == typeof(long))
                {
                    long longReturn;
                    if (defaultReturnValue != null) longReturn = defaultReturnValue;
                    if (long.TryParse((string)token, out longReturn))
                        return longReturn;
                    return defaultReturnValue;
                }
                else if (type == typeof(int))
                {
                    int intReturn;
                    if (defaultReturnValue != null) intReturn = defaultReturnValue;
                    if (int.TryParse((string)token, out intReturn))
                        return intReturn;
                    return defaultReturnValue;
                }
                else if (type == typeof(JObject))
                {
                    return token == null ? defaultReturnValue : JObject.FromObject(token);
                }
                else if (type == typeof(DateTimeOffset))
                {
                    return token == null ? defaultReturnValue : DateTime.SpecifyKind(DateTime.Parse(token.ToString()), DateTimeKind.Utc);
                }
            }
            catch (Exception ex) { }

            return defaultReturnValue;
        }


    }
}
