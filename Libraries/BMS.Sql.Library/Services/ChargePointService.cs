using BMS.Sql.Library;
using BMS.Sql.Library.Models;
using Microsoft.EntityFrameworkCore;

namespace BMS.Sql.Library.Services
{
    public class ChargePointService : ModelServiceBase
    {
        public ChargePointService(BMSDbContext bmsDbContext) : base(bmsDbContext) { }

        public ChargePoint Get(int? id)
        {
            if (id == null)
                return new ChargePoint();
            return BMSDbContext.ChargePoints.SingleOrDefault(x => x.Id == id);
        }
        public List<ChargePoint> GetAll()
        {
            return BMSDbContext.ChargePoints.ToList();
        }
        public List<ChargePoint> GetAllWithChargeController(ApplicationUser user)
        {
            if (user.Role != UserRoleEnum.Admin)
            {
                return BMSDbContext
                    .ChargePoints
                    .Include(x => x.ChargeController)
                    .Include(x => x.ChargeController.Installer)
                    .Include(x => x.ChargeController.Client)
                    .Include(x => x.ChargeController.Transactions)
                    .Join(
                        BMSDbContext.UserRoles, 
                        chargePoint => new { chargecontroller = chargePoint.ChargeController.Id, user.Id },
                        userRole => new { chargecontroller = userRole.ChargeController.Id, userRole.User.Id },
                        (chargePoint, userRole) => chargePoint)
                    .ToList();
            }
            
            return BMSDbContext
                .ChargePoints
                .Include(x => x.ChargeController)
                .Include(x => x.ChargeController.Transactions)
                .Include(x => x.ChargeController.Installer)
                .Include(x => x.ChargeController.Client)
                .ToList();
        }
        public ChargePoint? GetWithChargeController(int? id, ApplicationUser user)
        {
            if (id == null) return null;

            if (user.Role != UserRoleEnum.Admin)
            {
                return BMSDbContext
                    .ChargePoints
                    .Include(x => x.ChargeController)
                    .Include(x => x.ChargeController.Installer)
                    .Include(x => x.ChargeController.Client)
                    .Join(
                        BMSDbContext.UserRoles,
                        chargePoint => new { chargecontroller = chargePoint.ChargeController.Id, user.Id },
                        userRole => new { chargecontroller = userRole.ChargeController.Id, userRole.User.Id },
                        (chargePoint, userRole) => chargePoint)
                    .SingleOrDefault(x => x.Id == id);
            }

            return BMSDbContext
                .ChargePoints
                .Include(x => x.ChargeController)
                .Include(x => x.ChargeController.Installer)
                .Include(x => x.ChargeController.Transactions)
                .Include(x => x.ChargeController.Client)
                .SingleOrDefault(x => x.Id == id);
        }
        public List<ChargePoint> GetByChargeControllerId(int chargeControllerId)
        {
            return BMSDbContext.ChargePoints.Where(x => x.ChargeController.Id == chargeControllerId).ToList();
        }
        public ChargePoint Update(ChargePoint chargePoint)
        {
            ChargePoint updatedChargePoint = BMSDbContext.ChargePoints.Update(chargePoint).Entity;
            BMSDbContext.SaveChanges();
            return updatedChargePoint;
        }
    }
}
