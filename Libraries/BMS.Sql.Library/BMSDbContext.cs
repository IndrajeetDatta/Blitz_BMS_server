using BMS.Sql.Library.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BMS.Sql.Library
{
    public class BMSDbContext : IdentityDbContext
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ChargeController> ChargeControllers { get; set; }
        public DbSet<ChargePoint> ChargePoints { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Network> Networks { get; set; }
        public DbSet<RFID> RFID { get; set; }
        public DbSet<OCPPConfig> OCPPConfig { get; set; }
        public DbSet<OCPPStatus> OCPPStatus { get; set; }
        public DbSet<OCPPMessage> OCPPMessage { get; set; }
        public DbSet<UserData> UserData { get; set; }
        public DbSet<Command> Commands { get; set; }
        public DbSet<Log> Logs { get; set; }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Email> Emails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ChargeController>().Property(x => x.NetworkType).HasConversion<string>();
            modelBuilder.Entity<ChargeController>().Property(x => x.ModemConnectionStatus).HasConversion<string>();
            modelBuilder.Entity<ChargeController>().Property(x => x.ModemProvider).HasConversion<string>();
            modelBuilder.Entity<ChargeController>().Property(x => x.ModemRegistrationStatus).HasConversion<string>();
            modelBuilder.Entity<ChargeController>().Property(x => x.ModemRoamingStatus).HasConversion<string>();
            modelBuilder.Entity<ChargeController>().Property(x => x.ModemSignalQuality).HasConversion<string>();
            modelBuilder.Entity<ChargeController>().Property(x => x.ModemRadioTechnology).HasConversion<string>();
            modelBuilder.Entity<ChargeController>().Property(x => x.ModemSimStatus).HasConversion<string>();
            modelBuilder.Entity<ChargeController>().Property(x => x.SystemMonitorStatus).HasConversion<string>();
            modelBuilder.Entity<ChargeController>().Property(x => x.ControllerAgent_Status).HasConversion<string>();
            modelBuilder.Entity<ChargeController>().Property(x => x.OCPP16Status).HasConversion<string>();
            modelBuilder.Entity<ChargeController>().Property(x => x.ModbusClientStatus).HasConversion<string>();
            modelBuilder.Entity<ChargeController>().Property(x => x.ModbusServerStatus).HasConversion<string>();
            modelBuilder.Entity<ChargeController>().Property(x => x.JupicoreStatus).HasConversion<string>();
            modelBuilder.Entity<ChargeController>().Property(x => x.LoadManagementStatus).HasConversion<string>();
            modelBuilder.Entity<ChargeController>().Property(x => x.WebserverStatus).HasConversion<string>();

            modelBuilder.Entity<ChargePoint>().Property(x => x.State).HasConversion<string>();
            modelBuilder.Entity<ChargePoint>().Property(x => x.EnergyType).HasConversion<string>();
            modelBuilder.Entity<ChargePoint>().Property(x => x.PhaseRotation).HasConversion<string>();
            modelBuilder.Entity<ChargePoint>().Property(x => x.ReleaseChargingMode).HasConversion<string>();
            modelBuilder.Entity<ChargePoint>().Property(x => x.RFIDReader).HasConversion<string>();
            modelBuilder.Entity<ChargePoint>().Property(x => x.RFIDReaderType).HasConversion<string>();
            modelBuilder.Entity<ChargePoint>().Property(x => x.HighLevelCommunication).HasConversion<string>();

            modelBuilder.Entity<Command>().Property(x => x.Status).HasConversion<string>();

            modelBuilder.Entity<RFID>().Property(x => x.Type).HasConversion<string>();
        }

        // Local connection string as default
        public string ConnectionString = "Host=localhost:5432;Username=;Password=;Database=bms";

        // Empty constructor required for running `dotnet ef` commands locally
        public BMSDbContext() { }

        public BMSDbContext(DbContextOptions<BMSDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                // This should only happen when using without configuration in `Startup.cs` (`detnet ef`, Console Applications etc.)
                _ = options.UseNpgsql(ConnectionString);
            }
        }
    }
}
