export type ddType = {fetchValue: string, displayedValue: string}[]

export const ddEnergyType: ddType = [
    {fetchValue: "DISABLED", displayedValue: "Disabled"},
    {fetchValue: "EEM350", displayedValue: "Phoenix Contact EEM-350"},
    {fetchValue: "EEM357", displayedValue: "Phoenix Contact EEM-EM357 / EEM-DM357"},
    {fetchValue: "EEM357EE", displayedValue: "Phoenix Contact EEM357EE"},
    {fetchValue: "EEM_AM157_70", displayedValue: "Phoenix Contact EEM-AM157-70"},
    {fetchValue: "EM24", displayedValue: "Carlo Gavazzi EM24"},
    {fetchValue: "WM3M4_C", displayedValue: "Iskra WM3M4(C)"},
    {fetchValue: "PRO380", displayedValue: "Inepro Metering PRO380"},
    {fetchValue: "EM340", displayedValue: "Carlo Gavazzi EM340"},
]

export const ddPhaseRotation: ddType = [
    {fetchValue: "UNKNOWN", displayedValue: "Unknown"},
    {fetchValue: "RST", displayedValue: "RST - I1 I2 I3"},
    {fetchValue: "RTS", displayedValue: "RTS - I1 I3 I2"},
    {fetchValue: "SRT", displayedValue: "SRT - I2 I1 I3"},
    {fetchValue: "STR", displayedValue: "STR - I2 I3 I1"},
    {fetchValue: "TRS", displayedValue: "TRS - I3 I1 I2"},
    {fetchValue: "TSR", displayedValue: "TSR - I3 I2 I1"},
]

export const ddReleaseChargingMode: ddType = [
    {fetchValue: "LOCAL_INPUT", displayedValue: "By Dashboard"},
    {fetchValue: "RFID_WHITELIST", displayedValue: "By Local Whitelist"},
    {fetchValue: "ALWAYS", displayedValue: "Always release charging"},
    {fetchValue: "OCPP_CONTROL", displayedValue: "By OCPP"},
    {fetchValue: "MODBUS_CONTROL", displayedValue: "By Modbus"},
    {fetchValue: "REMOTE_CONTROL", displayedValue: "By Remote Control"},
]

export const ddRfidReaderType: ddType = [
    {fetchValue: "ELATEC_TWN4", displayedValue: "ELATEC TWN4"},
    {fetchValue: "DUALI_DE_950", displayedValue: "DUALI DE 950"},
]

export const ddHighLvlCommunication: ddType = [
    {fetchValue: "DISABLED", displayedValue: "Disabled (No high-level communication.)"},
    {fetchValue: "OPTIONAL", displayedValue: "Optional (High-level communication only on EV request.)"},
    {fetchValue: "REQUIRED", displayedValue: "Required (Only high-level communication.)"},
]