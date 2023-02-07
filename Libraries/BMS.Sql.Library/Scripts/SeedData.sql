INSERT INTO public."Clients"(
    "Id", "Name", "Location", "Phone", "Email")
    VALUES (1, 'Client H', 'Heirbaan 9, 9150 Kruibeke', '03 250 12 20', 'info@hertsens.eu');

--INSERT INTO public."ChargingStations"(
--    "Id", "ClientId", "InstallerId", "Active", "NetworkConnection", "LoadManagement", "OCPPConnection", "SimActive", 
--    "Uptime", "SerialNumber", "Address", "LocationData", "SoftwareVersion", "Heartbeat", "DateInstalled", "LastMaintenance",
--    "SystemTime", "Configured", "NetworkType", "ETH0DHCP", "ETH0IPAddress", "ETH0SubnetMask", "ETH0Gateway", "ModemServiceActive",
--    "ModemPreferOverETH0", "ModemDefaultRoute", "ModemSignalRSSI", "ModemSignalCQI", "ModemReceivedBytes", "ModemTransmittedBytes", 
--    "ModemAPN", "ModemSimPin", "ModemUsername", "ModemPassword", "ModemIPAddress", "ModemPrimaryDNSServer", "ModemSecondaryDNSServer",
--    "ModemIMSI", "ModemICCID", "ModemExtendedReport", "ModemConnectionStatus", "ModemProvider", "ModemRegistrationStatus", 
--    "ModemRoamingStatus", "ModemSignalQuality", "ModemRadioTechnology", "ModemSimStatus")
--VALUES (
--    1, 1, 1, true, true, true, false, true, 
--    3469253, '21063100101', '', '', 'V1.01.05', '2022-06-01 15:31', '2022-05-05', NULL,
--    '2022-06-23 07:49:17', 'OCPP', 'LAN', true, '192.168.0.105', '255.255.255.0', '192.168.0.1',
--    true, false, false, -89, 12, 273202, 379724,
--    'wm', 1234, '', '', '100.108.50.69', '8.8.8.8', '8.8.4.4',
--    '26479846513549', '898830064549465645', 'No report available/none/none', 'Connected', 'Orange', 'Registered',
--    'Roaming', 'Ok', 'LTE', 'Ready');

--INSERT INTO public."Networks"(
--    "Id", "ChargingStationId", "Name", "IPV4Address", "IPV6Address", "MACAddress", "ReceivedBytes", "TransmittedBytes")
--    VALUES (1, 1, 'ETH0', '192.168.0.105', 'fe80::aa74::1dff::feb0::fb6', 'A8:74:1D:B0:0F:B6', 226868792, 1388639485);

--INSERT INTO public."Networks"(
--    "Id", "ChargingStationId", "Name", "IPV4Address", "IPV6Address", "MACAddress", "ReceivedBytes", "TransmittedBytes")
--    VALUES (2, 1, 'ETH1', '192.168.4.1', 'fe80::aa74::1dff::feb0::fb7', 'A8:74:1D:B0:0F:B7', 642392131, 88653177);

INSERT INTO public."ChargeControllers"(
    "Id", "ClientId", "InstallerId", "NetworkConnection", "OCPPConnection", "SimActive", 
    "UptimeInSeconds", "SerialNumber", "Address", "LocationData", "DateInstalled", "LastMaintenance", "SystemTime", "NetworkType",
    "ETH0DHCP", "ETH0IPAddress", "ETH0SubnetMask", "ETH0Gateway", "ModemServiceActive",
    "ModemPreferOverETH0", "ModemDefaultRoute", "ModemSignalRSSI", "ModemSignalCQI", "ModemReceivedBytes", "ModemTransmittedBytes", 
    "ModemAPN", "ModemSimPin", "ModemUsername", "ModemPassword", "ModemIPAddress", "ModemPrimaryDNSServer", "ModemSecondaryDNSServer",
    "ModemIMSI", "ModemICCID", "ModemExtendedReport", "ModemConnectionStatus", "ModemProvider", "ModemRegistrationStatus", 
    "ModemRoamingStatus", "ModemSignalQuality", "ModemRadioTechnology", "ModemSimStatus", "CPUTemperatureCelsius", "CPUUtilizationPercentage", 
    "RAMAvailableBytes", "RAMTotalBytes", "RAMUsedBytes", "DataUtilizationPercentage", "DataTotalBytes", "LogUtilizationPercentage",
    "LogTotalBytes", "VarVolatileUtilizationPercentage", "VarVolatileTotalBytes", "SystemMonitorVersion", "SystemMonitorStatus",
    "ControllerAgentVersion", "ControllerAgent_Status", "OCPP16_Version", "OCPP16Status", "ModbusClientVersion", "ModbusClientStatus",
    "ModbusServerVersion", "ModbusServerStatus", "JupicoreVersion", "JupicoreStatus", "LoadManagementVersion", "LoadManagementStatus",
    "WebserverVersion", "WebserverStatus", "LoadManagementActive", "ChargingParkName", "LoadCircuitFuse", "HighLevelMeasuringDeviceModbus", "HighLevelMeasuringDeviceControllerId", 
    "MeasuringDeviceType", "LoadStrategy", "CurrentL1", "CurrentL2", "CurrentL3", "PlannedCurrentL1", "PlannedCurrentL2", "PlannedCurrentL3", "MonitoredCps")
VALUES (
    1, 1, 1, true, false, true, 
    3469253, '21063100101', 'Address 1', 'Location 1', '2022-06-01 15:31', '2022-05-05', '2022-06-01 15:31', '',
    true, '192.45.45.45', '255.255.255.0', '192.168.292.1', true,
    true, true, 12, 34, 12321312, 12322323,
    'APN', 'SIMPIN', 'User', 'Password', '127.0.0.1', '8.8.8.8', '8.8.8.8', 
    'IMSI', 'ICCID', 'error', 'Connected', 'Orange', 'Registered', 
    'Roaming', 'Ok', 'LTE', 'Ready', 30, 23,
    1232131, 231231312, 299999181, 51, 1233132131, 33,
    1232312333, 45, 432432432, 'V-monitor', 'Running',
    'V-controllerAgent', 'Running', 'V-OCPP16', 'Running', 'V-clientModbus', 'Running',
    'V-serverModbus', 'Running', 'V-jupicore', 'Running', 'V-loadManagement', 'Running',
    'V-webserver', 'Running', true,'Hertsens Kruibeke33', '90', 'ipdevice', '', 'MA370', 'EQUAL_DISTRIBUTION', '6.01', '6.05', '5.99', '2', '2', '2', '6');

INSERT INTO public."ChargePoints"(
    "Id", "ChargeControllerId", "Enabled", "ChargingTimeInSeconds", "ChargeCurrentMinimumInAmpers", "ChargeCurrentMaximumInAmpers",
    "FallbackCurrentInAmpers", "FallbackTimeInSeconds", "RFIDTimeoutInSeconds", "OCPPConnectorId", "ChargingRate", "SerialNumber", 
    "Uid", "Name", "SoftwareVersion", "Location", "State", "Configured", "EnergyType", "PhaseRotation", "ChargingMode",
    "RFIDReader", "RFIDReaderType", "HighLevelCommunication")
VALUES (
    1, 1, true, 1233, 324444, 32222,
    23433, 34211, 13213, 1, 34, '21063100101', '4131f5',
    'Name 1', 'V-2', 'Location 1', 'A1', 'Whitelist', 'CarloGavazziEM24', 'RSTL1L2L3', 'LocalWhitelist',
    'BPS11', 'ELATECTWN4', 'Disabled');

INSERT INTO public."Networks"(
    "Id", "ChargingStationId", "Name", "IPV4Address", "IPV6Address", "MACAddress", "ReceivedBytes", "TransmittedBytes")
    VALUES (1, 1, 'ETH0', '192.168.0.105', 'fe80::aa74::1dff::feb0::fb6', 'A8:74:1D:B0:0F:B6', 226868792, 1388639485);

INSERT INTO public."Networks"(
    "Id", "ChargingStationId", "Name", "IPV4Address", "IPV6Address", "MACAddress", "ReceivedBytes", "TransmittedBytes")
    VALUES (2, 1, 'ETH1', '192.168.4.1', 'fe80::aa74::1dff::feb0::fb7', 'A8:74:1D:B0:0F:B7', 642392131, 88653177);