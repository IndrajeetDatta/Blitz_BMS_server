﻿// <auto-generated />
using System;
using BMS.Sql.Library;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BMS.Sql.Library.Migrations
{
    [DbContext(typeof(BMSDbContext))]
    [Migration("20221108123630_add AllowTestModeCommands to ChargeController table")]
    partial class addAllowTestModeCommandstoChargeControllertable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BMS.Sql.Library.Models.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ApplicationUsers");
                });

            modelBuilder.Entity("BMS.Sql.Library.Models.ChargeController", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<bool?>("AllowTestModeCommands")
                        .HasColumnType("boolean");

                    b.Property<int?>("CPUTemperatureCelsius")
                        .HasColumnType("integer");

                    b.Property<int?>("CPUUtilizationPercentage")
                        .HasColumnType("integer");

                    b.Property<string>("ChargingParkName")
                        .HasColumnType("text");

                    b.Property<int>("ClientId")
                        .HasColumnType("integer");

                    b.Property<string>("CompanyName")
                        .HasColumnType("text");

                    b.Property<string>("ControllerAgentVersion")
                        .HasColumnType("text");

                    b.Property<string>("ControllerAgent_Status")
                        .HasColumnType("text");

                    b.Property<string>("CurrentL1")
                        .HasColumnType("text");

                    b.Property<string>("CurrentL2")
                        .HasColumnType("text");

                    b.Property<string>("CurrentL3")
                        .HasColumnType("text");

                    b.Property<long?>("DataTotalBytes")
                        .HasColumnType("bigint");

                    b.Property<int?>("DataUtilizationPercentage")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("DateInstalled")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool?>("ETH0DHCP")
                        .HasColumnType("boolean");

                    b.Property<string>("ETH0Gateway")
                        .HasColumnType("text");

                    b.Property<string>("ETH0IPAddress")
                        .HasColumnType("text");

                    b.Property<string>("ETH0SubnetMask")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("Heartbeat")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("HighLevelMeasuringDeviceControllerId")
                        .HasColumnType("text");

                    b.Property<string>("HighLevelMeasuringDeviceModbus")
                        .HasColumnType("text");

                    b.Property<int>("InstallerId")
                        .HasColumnType("integer");

                    b.Property<string>("JupicoreStatus")
                        .HasColumnType("text");

                    b.Property<string>("JupicoreVersion")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("LastMaintenance")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LoadCircuitFuse")
                        .HasColumnType("text");

                    b.Property<bool?>("LoadManagementActive")
                        .HasColumnType("boolean");

                    b.Property<string>("LoadManagementStatus")
                        .HasColumnType("text");

                    b.Property<string>("LoadManagementVersion")
                        .HasColumnType("text");

                    b.Property<string>("LoadStrategy")
                        .HasColumnType("text");

                    b.Property<string>("LocationData")
                        .HasColumnType("text");

                    b.Property<long?>("LogTotalBytes")
                        .HasColumnType("bigint");

                    b.Property<int?>("LogUtilizationPercentage")
                        .HasColumnType("integer");

                    b.Property<string>("MeasuringDeviceType")
                        .HasColumnType("text");

                    b.Property<string>("ModbusClientStatus")
                        .HasColumnType("text");

                    b.Property<string>("ModbusClientVersion")
                        .HasColumnType("text");

                    b.Property<string>("ModbusServerStatus")
                        .HasColumnType("text");

                    b.Property<string>("ModbusServerVersion")
                        .HasColumnType("text");

                    b.Property<string>("ModemAPN")
                        .HasColumnType("text");

                    b.Property<string>("ModemConnectionStatus")
                        .HasColumnType("text");

                    b.Property<bool?>("ModemDefaultRoute")
                        .HasColumnType("boolean");

                    b.Property<string>("ModemExtendedReport")
                        .HasColumnType("text");

                    b.Property<string>("ModemICCID")
                        .HasColumnType("text");

                    b.Property<string>("ModemIMSI")
                        .HasColumnType("text");

                    b.Property<string>("ModemIPAddress")
                        .HasColumnType("text");

                    b.Property<string>("ModemPassword")
                        .HasColumnType("text");

                    b.Property<bool?>("ModemPreferOverETH0")
                        .HasColumnType("boolean");

                    b.Property<string>("ModemPrimaryDNSServer")
                        .HasColumnType("text");

                    b.Property<string>("ModemProvider")
                        .HasColumnType("text");

                    b.Property<string>("ModemRadioTechnology")
                        .HasColumnType("text");

                    b.Property<long?>("ModemReceivedBytes")
                        .HasColumnType("bigint");

                    b.Property<string>("ModemRegistrationStatus")
                        .HasColumnType("text");

                    b.Property<string>("ModemRoamingStatus")
                        .HasColumnType("text");

                    b.Property<string>("ModemSecondaryDNSServer")
                        .HasColumnType("text");

                    b.Property<bool?>("ModemServiceActive")
                        .HasColumnType("boolean");

                    b.Property<int?>("ModemSignalCQI")
                        .HasColumnType("integer");

                    b.Property<string>("ModemSignalQuality")
                        .HasColumnType("text");

                    b.Property<int?>("ModemSignalRSSI")
                        .HasColumnType("integer");

                    b.Property<string>("ModemSimPin")
                        .HasColumnType("text");

                    b.Property<string>("ModemSimStatus")
                        .HasColumnType("text");

                    b.Property<long?>("ModemTransmittedBytes")
                        .HasColumnType("bigint");

                    b.Property<string>("ModemUsername")
                        .HasColumnType("text");

                    b.Property<string>("MonitoredCps")
                        .HasColumnType("text");

                    b.Property<bool?>("NetworkConnection")
                        .HasColumnType("boolean");

                    b.Property<string>("NetworkType")
                        .HasColumnType("text");

                    b.Property<string>("OCPP16Status")
                        .HasColumnType("text");

                    b.Property<string>("OCPP16_Version")
                        .HasColumnType("text");

                    b.Property<string>("OCPPConnection")
                        .HasColumnType("text");

                    b.Property<string>("OsReleaseVersion")
                        .HasColumnType("text");

                    b.Property<string>("PlannedCurrentL1")
                        .HasColumnType("text");

                    b.Property<string>("PlannedCurrentL2")
                        .HasColumnType("text");

                    b.Property<string>("PlannedCurrentL3")
                        .HasColumnType("text");

                    b.Property<long?>("RAMAvailableBytes")
                        .HasColumnType("bigint");

                    b.Property<long?>("RAMTotalBytes")
                        .HasColumnType("bigint");

                    b.Property<long?>("RAMUsedBytes")
                        .HasColumnType("bigint");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("text");

                    b.Property<bool?>("SimActive")
                        .HasColumnType("boolean");

                    b.Property<string>("SystemMonitorStatus")
                        .HasColumnType("text");

                    b.Property<string>("SystemMonitorVersion")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("SystemTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("UptimeInSeconds")
                        .HasColumnType("bigint");

                    b.Property<long?>("VarVolatileTotalBytes")
                        .HasColumnType("bigint");

                    b.Property<int?>("VarVolatileUtilizationPercentage")
                        .HasColumnType("integer");

                    b.Property<string>("WebAppVersion")
                        .HasColumnType("text");

                    b.Property<string>("WebserverStatus")
                        .HasColumnType("text");

                    b.Property<string>("WebserverVersion")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("InstallerId");

                    b.ToTable("ChargeControllers");
                });

            modelBuilder.Entity("BMS.Sql.Library.Models.ChargePoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChargeControllerId")
                        .HasColumnType("integer");

                    b.Property<string>("ChargeControllerUid")
                        .HasColumnType("text");

                    b.Property<int?>("ChargeCurrentMaximumInAmpers")
                        .HasColumnType("integer");

                    b.Property<int?>("ChargeCurrentMinimumInAmpers")
                        .HasColumnType("integer");

                    b.Property<string>("ChargePointUid")
                        .HasColumnType("text");

                    b.Property<decimal?>("ChargingRate")
                        .HasColumnType("numeric");

                    b.Property<int?>("ChargingTimeInSeconds")
                        .HasColumnType("integer");

                    b.Property<bool?>("Enabled")
                        .HasColumnType("boolean");

                    b.Property<string>("EnergyType")
                        .HasColumnType("text");

                    b.Property<int?>("FallbackCurrentInAmpers")
                        .HasColumnType("integer");

                    b.Property<int?>("FallbackTimeInSeconds")
                        .HasColumnType("integer");

                    b.Property<string>("HighLevelCommunication")
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("OCPPConnectorId")
                        .HasColumnType("integer");

                    b.Property<string>("PhaseRotation")
                        .HasColumnType("text");

                    b.Property<string>("RFIDReader")
                        .HasColumnType("text");

                    b.Property<string>("RFIDReaderType")
                        .HasColumnType("text");

                    b.Property<int?>("RFIDTimeoutInSeconds")
                        .HasColumnType("integer");

                    b.Property<string>("ReleaseChargingMode")
                        .HasColumnType("text");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("text");

                    b.Property<string>("SlaveSerialNumber")
                        .HasColumnType("text");

                    b.Property<string>("SoftwareVersion")
                        .HasColumnType("text");

                    b.Property<string>("State")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ChargeControllerId");

                    b.ToTable("ChargePoints");
                });

            modelBuilder.Entity("BMS.Sql.Library.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("BMS.Sql.Library.Models.Command", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ChargeControllerUid")
                        .HasColumnType("text");

                    b.Property<string>("ChargePointUid")
                        .HasColumnType("text");

                    b.Property<string>("CommandType")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("MasterId")
                        .HasColumnType("text");

                    b.Property<string>("MasterUrl")
                        .HasColumnType("text");

                    b.Property<string>("Method")
                        .HasColumnType("text");

                    b.Property<string>("Payload")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("ProcessedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Commands");
                });

            modelBuilder.Entity("BMS.Sql.Library.Models.Installer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Installers");
                });

            modelBuilder.Entity("BMS.Sql.Library.Models.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("text");

                    b.Property<string>("IpRequest")
                        .HasColumnType("text");

                    b.Property<string>("JsonData")
                        .HasColumnType("text");

                    b.Property<int?>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("BMS.Sql.Library.Models.Network", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChargingStationId")
                        .HasColumnType("integer");

                    b.Property<string>("IPV4Address")
                        .HasColumnType("text");

                    b.Property<string>("IPV6Address")
                        .HasColumnType("text");

                    b.Property<string>("MACAddress")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<long?>("ReceivedBytes")
                        .HasColumnType("bigint");

                    b.Property<long?>("TransmittedBytes")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ChargingStationId");

                    b.ToTable("Networks");
                });

            modelBuilder.Entity("BMS.Sql.Library.Models.OCPPConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("BackendURL")
                        .HasColumnType("text");

                    b.Property<int>("ChargeControllerId")
                        .HasColumnType("integer");

                    b.Property<string>("ChargeStationModel")
                        .HasColumnType("text");

                    b.Property<string>("ChargeStationSerialNumber")
                        .HasColumnType("text");

                    b.Property<string>("ChargeStationVendor")
                        .HasColumnType("text");

                    b.Property<string>("FreeModeRFID")
                        .HasColumnType("text");

                    b.Property<string>("NetworkInterface")
                        .HasColumnType("text");

                    b.Property<string>("OcppProtocolVersion")
                        .HasColumnType("text");

                    b.Property<string>("ServiceRFID")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ChargeControllerId")
                        .IsUnique();

                    b.ToTable("OCPPConfig");
                });

            modelBuilder.Entity("BMS.Sql.Library.Models.OCPPMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Action")
                        .HasColumnType("text");

                    b.Property<int>("ChargeControllerId")
                        .HasColumnType("integer");

                    b.Property<bool?>("FromLastHeartbeat")
                        .HasColumnType("boolean");

                    b.Property<string>("MessageData")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ChargeControllerId");

                    b.ToTable("OCPPMessage");
                });

            modelBuilder.Entity("BMS.Sql.Library.Models.OCPPStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChargeControllerId")
                        .HasColumnType("integer");

                    b.Property<string>("Device_uid")
                        .HasColumnType("text");

                    b.Property<string>("OccpStatus")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("OccpStatusSentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool?>("Operative")
                        .HasColumnType("boolean");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ChargeControllerId");

                    b.ToTable("OCPPStatus");
                });

            modelBuilder.Entity("BMS.Sql.Library.Models.RFID", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool?>("AllowCharging")
                        .HasColumnType("boolean");

                    b.Property<int>("ChargeControllerId")
                        .HasColumnType("integer");

                    b.Property<int?>("EvConsumptionRateKWhPer100KM")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("ExpiryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ChargeControllerId");

                    b.ToTable("RFID");
                });

            modelBuilder.Entity("BMS.Sql.Library.Models.UserData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChargeControllerId")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("JsonData")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ChargeControllerId")
                        .IsUnique();

                    b.ToTable("UserData");
                });

            modelBuilder.Entity("BMS.Sql.Library.Models.ChargeController", b =>
                {
                    b.HasOne("BMS.Sql.Library.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BMS.Sql.Library.Models.Installer", "Installer")
                        .WithMany()
                        .HasForeignKey("InstallerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Installer");
                });

            modelBuilder.Entity("BMS.Sql.Library.Models.ChargePoint", b =>
                {
                    b.HasOne("BMS.Sql.Library.Models.ChargeController", "ChargeController")
                        .WithMany("ChargePoints")
                        .HasForeignKey("ChargeControllerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChargeController");
                });

            modelBuilder.Entity("BMS.Sql.Library.Models.Network", b =>
                {
                    b.HasOne("BMS.Sql.Library.Models.ChargeController", "ChargingStation")
                        .WithMany("Networks")
                        .HasForeignKey("ChargingStationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChargingStation");
                });

            modelBuilder.Entity("BMS.Sql.Library.Models.OCPPConfig", b =>
                {
                    b.HasOne("BMS.Sql.Library.Models.ChargeController", "ChargeController")
                        .WithOne("OCPPConfig")
                        .HasForeignKey("BMS.Sql.Library.Models.OCPPConfig", "ChargeControllerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChargeController");
                });

            modelBuilder.Entity("BMS.Sql.Library.Models.OCPPMessage", b =>
                {
                    b.HasOne("BMS.Sql.Library.Models.ChargeController", "ChargeController")
                        .WithMany("oCPPMessages")
                        .HasForeignKey("ChargeControllerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChargeController");
                });

            modelBuilder.Entity("BMS.Sql.Library.Models.OCPPStatus", b =>
                {
                    b.HasOne("BMS.Sql.Library.Models.ChargeController", "ChargeController")
                        .WithMany("oCPPStatus")
                        .HasForeignKey("ChargeControllerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChargeController");
                });

            modelBuilder.Entity("BMS.Sql.Library.Models.RFID", b =>
                {
                    b.HasOne("BMS.Sql.Library.Models.ChargeController", "ChargeController")
                        .WithMany("WhitelistRFIDs")
                        .HasForeignKey("ChargeControllerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChargeController");
                });

            modelBuilder.Entity("BMS.Sql.Library.Models.UserData", b =>
                {
                    b.HasOne("BMS.Sql.Library.Models.ChargeController", "ChargeController")
                        .WithOne("UserData")
                        .HasForeignKey("BMS.Sql.Library.Models.UserData", "ChargeControllerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChargeController");
                });

            modelBuilder.Entity("BMS.Sql.Library.Models.ChargeController", b =>
                {
                    b.Navigation("ChargePoints");

                    b.Navigation("Networks");

                    b.Navigation("OCPPConfig")
                        .IsRequired();

                    b.Navigation("UserData")
                        .IsRequired();

                    b.Navigation("WhitelistRFIDs");

                    b.Navigation("oCPPMessages");

                    b.Navigation("oCPPStatus");
                });
#pragma warning restore 612, 618
        }
    }
}
