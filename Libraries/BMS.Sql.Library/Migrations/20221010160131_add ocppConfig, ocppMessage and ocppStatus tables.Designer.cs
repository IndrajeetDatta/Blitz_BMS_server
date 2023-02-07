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
    [Migration("20221010160131_add ocppConfig, ocppMessage and ocppStatus tables")]
    partial class addocppConfigocppMessageandocppStatustables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
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

                    b.Property<int?>("CPUTemperatureCelsius")
                        .HasColumnType("integer");

                    b.Property<int?>("CPUUtilizationPercentage")
                        .HasColumnType("integer");

                    b.Property<string>("ChargingParkName")
                        .HasColumnType("text");

                    b.Property<int>("ClientId")
                        .HasColumnType("integer");

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

                    b.Property<int?>("ChargeCurrentMaximumInAmpers")
                        .HasColumnType("integer");

                    b.Property<int?>("ChargeCurrentMinimumInAmpers")
                        .HasColumnType("integer");

                    b.Property<string>("ChargingMode")
                        .HasColumnType("text");

                    b.Property<decimal?>("ChargingRate")
                        .HasColumnType("numeric");

                    b.Property<int?>("ChargingTimeInSeconds")
                        .HasColumnType("integer");

                    b.Property<string>("Configured")
                        .HasColumnType("text");

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

                    b.Property<string>("SerialNumber")
                        .HasColumnType("text");

                    b.Property<string>("SlaveSerialNumber")
                        .HasColumnType("text");

                    b.Property<string>("SoftwareVersion")
                        .HasColumnType("text");

                    b.Property<string>("State")
                        .HasColumnType("text");

                    b.Property<string>("Uid")
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

                    b.Property<DateTimeOffset?>("created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("errorMessage")
                        .HasColumnType("text");

                    b.Property<string>("ipRequest")
                        .HasColumnType("text");

                    b.Property<string>("jsonData")
                        .HasColumnType("text");

                    b.Property<int?>("status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Log");
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

                    b.Property<int>("ChargeControllerId")
                        .HasColumnType("integer");

                    b.Property<string>("backendURL")
                        .HasColumnType("text");

                    b.Property<string>("chargeStationModel")
                        .HasColumnType("text");

                    b.Property<string>("chargeStationSerialNumber")
                        .HasColumnType("text");

                    b.Property<string>("chargeStationVendor")
                        .HasColumnType("text");

                    b.Property<string>("freeModeRFID")
                        .HasColumnType("text");

                    b.Property<string>("networkInterface")
                        .HasColumnType("text");

                    b.Property<string>("ocppProtocolVersion")
                        .HasColumnType("text");

                    b.Property<string>("serviceRFID")
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

                    b.Property<int>("ChargeControllerId")
                        .HasColumnType("integer");

                    b.Property<string>("action")
                        .HasColumnType("text");

                    b.Property<string>("messageData")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("type")
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

                    b.Property<string>("device_uid")
                        .HasColumnType("text");

                    b.Property<string>("occpStatus")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("occpStatusSentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool?>("operative")
                        .HasColumnType("boolean");

                    b.Property<string>("status")
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

            modelBuilder.Entity("BMS.Sql.Library.Models.ChargeController", b =>
                {
                    b.Navigation("ChargePoints");

                    b.Navigation("Networks");

                    b.Navigation("OCPPConfig")
                        .IsRequired();

                    b.Navigation("WhitelistRFIDs");

                    b.Navigation("oCPPMessages");

                    b.Navigation("oCPPStatus");
                });
#pragma warning restore 612, 618
        }
    }
}
