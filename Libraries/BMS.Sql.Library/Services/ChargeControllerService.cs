using BMS.Sql.Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BMS.Sql.Library.Services
{
    public class ChargeControllerService: ModelServiceBase
    {
        public ChargeControllerService(BMSDbContext bmsDbContext) : base(bmsDbContext) { }

        public ChargeController? Get(int? id, ApplicationUser user)
        {
            if (id == null) return null;

            if (user.Role != UserRoleEnum.Admin)
            {
                UserRole? userRole = BMSDbContext.UserRoles
                    .Include(x => x.User)
                    .Include(x => x.ChargeController)
                    .SingleOrDefault(x => x.User.Id == user.Id && x.ChargeController.Id == id);


                if (userRole != null) return userRole.ChargeController;
                return null;
            }

            return BMSDbContext.ChargeControllers.SingleOrDefault(x => x.Id == id);
        }
        public ChargeController? GetWithWhitelist(int id, ApplicationUser user)
        {
            if (user.Role != UserRoleEnum.Admin)
            {
                UserRole? userRole = BMSDbContext.UserRoles
                    .Include(x => x.User)
                    .Include(x => x.ChargeController)
                    .Include(x => x.ChargeController.WhitelistRFIDs)
                    .SingleOrDefault(x => x.User.Id == user.Id && x.ChargeController.Id == id);


                if (userRole != null) return userRole.ChargeController;
                return null;
            }

            return BMSDbContext.ChargeControllers
                .Include(x => x.WhitelistRFIDs)
                .SingleOrDefault(x => x.Id == id);
        }
        public ChargeController? GetWithTransaction(int id, DateTimeOffset? lastCreatedDate, ApplicationUser user)
        {
            if (user.Role != UserRoleEnum.Admin)
            {
                UserRole? userRole = BMSDbContext.UserRoles
                    .Include(x => x.User)
                    .Include(x => x.ChargeController)
                    .Include(x => x.ChargeController.UserData)
                    .Include(x => x.ChargeController.Transactions.Where(x => lastCreatedDate == null || x.CreatedDate > lastCreatedDate))
                    .SingleOrDefault(x => x.User.Id == user.Id && x.ChargeController.Id == id);


                if (userRole != null) return userRole.ChargeController;
                return null;
            }

            return BMSDbContext.ChargeControllers
                .Include(x => x.UserData)
                .Include(x => x.Transactions.Where(x => lastCreatedDate == null || x.CreatedDate > lastCreatedDate))
                .SingleOrDefault(x => x.Id == id);
        }
        public ChargeController? GetWithReferences(int id, ApplicationUser user)
        {
            if (user.Role != UserRoleEnum.Admin)
            {
                UserRole? userRole = BMSDbContext.UserRoles
                    .Include(x => x.User)
                    .Include(x => x.ChargeController)
                    .Include(x => x.ChargeController.Client)
                    .Include(x => x.ChargeController.Installer)
                    .Include(x => x.ChargeController.ChargePoints)
                    .Include(x => x.ChargeController.Networks)
                    .SingleOrDefault(x => x.User.Id == user.Id && x.ChargeController.Id == id);


                if (userRole != null) return userRole.ChargeController;
                return null;
            }

            return BMSDbContext.ChargeControllers
                .Include(x => x.Client)
                .Include(x => x.Installer)
                .Include(x => x.ChargePoints)
                .Include(x => x.Networks)
                .SingleOrDefault(x => x.Id == id);
        }
        public ChargeController? GetBySerialNumber(string serialNumber, ApplicationUser? user = null)
        {
            if (user != null && user.Role != UserRoleEnum.Admin)
            {
                UserRole? userRole = BMSDbContext.UserRoles
                    .Include(x => x.User)
                    .Include(x => x.ChargeController)
                    .SingleOrDefault(x => x.User.Id == user.Id && x.ChargeController.SerialNumber == serialNumber);


                if (userRole != null) return userRole.ChargeController;
                return null;
            }

            return BMSDbContext.ChargeControllers.SingleOrDefault(x => x.SerialNumber == serialNumber);
        }
        public ChargeController? GetBySerialNumberWithData(string serialNumber)
        {
            return BMSDbContext.ChargeControllers
                .Include(x => x.Client)
                .Include(x => x.Installer)
                .Include(x => x.Networks)
                .Include(x => x.ChargePoints)
                .Include(x => x.WhitelistRFIDs)
                .Include(x => x.OCPPConfig)
                .Include(x => x.oCPPStatus)
                .Include(x => x.UserData)
                .SingleOrDefault(x => x.SerialNumber == serialNumber);
        }
        public List<ChargeController> GetAll()
        {
            return BMSDbContext.ChargeControllers.ToList();
        }
        public List<ChargeController> GetAllWithClientandInstaller()
        {
            return BMSDbContext.ChargeControllers.Include(x => x.Client).Include(x => x.Installer).ToList();
        }

        public ChargeController Update(ChargeController chargingStation)
        {
            ChargeController updatedChargingStation = BMSDbContext.ChargeControllers.Update(chargingStation).Entity;
            BMSDbContext.SaveChanges();
            return updatedChargingStation;
        }
        public ChargeController Save(ChargeController chargeController)
        {
            if (chargeController == null) return new ChargeController();

            if (chargeController.Id > 0)
            {
                BMSDbContext.ChargeControllers.Update(chargeController);
            }
            else
            {
                BMSDbContext.ChargeControllers.Add(chargeController);
            }
            
            try
            {
                BMSDbContext.SaveChanges();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return chargeController;
        }

        public void UpdateNetworkConnectiontoAll(int expireNetworkConnectionInSeconds, ILogger Log)
        {
            try
            {
                Log.LogInformation($"---- In UpdateNetworkConnectiontoAll - Date.Now: {DateTime.UtcNow}");
                bool needUpdate = false;
                BMSDbContext.ChangeTracker.Clear();
                List<ChargeController> chargeControllerList = BMSDbContext.ChargeControllers.ToList();
                chargeControllerList.ForEach(chargeController =>
                {
                    Log.LogInformation($"---- ChargeController: {chargeController.Id}, {chargeController.SerialNumber}, {chargeController.NetworkConnection}, {chargeController.Heartbeat}.");

                    if (chargeController.Heartbeat != null && chargeController.NetworkConnection != null &&
                        chargeController.NetworkConnection == true && DateTime.UtcNow > chargeController.Heartbeat.Value.DateTime.ToUniversalTime().AddSeconds(expireNetworkConnectionInSeconds))
                    {
                        Log.LogInformation($"-------- Update ChargeContrroller.");

                        chargeController.NetworkConnection = false;
                        BMSDbContext.ChargeControllers.Update(chargeController);
                        Log.LogInformation($"-------- Updated ChargeContrroller.");
                        needUpdate = true;
                    }

                });

                if (needUpdate) BMSDbContext.SaveChanges();

                Log.LogInformation($"---- Finish UpdateNetworkConnectiontoAll.");
                Log.LogInformation("");
            }
            catch (Exception ex)
            {
                Log.LogInformation($"---- ERROR ON Update: {ex.ToString()}");
            }
        }
        public List<ApplicationUser> GetInstallersForChargeStation(double chargeControllerId)
        {
            List<UserRole> userRoles = BMSDbContext.UserRoles
                    .Include(x => x.User)
                    .Include(x => x.ChargeController)
                    .Where(x => x.ChargeController.Id == chargeControllerId).ToList();
            List<ApplicationUser> installers = new List<ApplicationUser>();
            foreach (UserRole userRole in userRoles)
            {
                ApplicationUser user = BMSDbContext.ApplicationUsers.Where(x => x.Id == userRole.User.Id).FirstOrDefault();
                if(user != null)
                {
                    if(user.Role == UserRoleEnum.Installer)
                    {
                        installers.Add(user);
                    }
                }
            }
            return installers;
        }
    }
}