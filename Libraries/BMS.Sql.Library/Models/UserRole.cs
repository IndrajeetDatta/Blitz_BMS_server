using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Sql.Library.Models
{
    public class UserRole
    {
        public UserRole() { }
        public UserRole(ChargeController chargecontroller, ApplicationUser userId)
        {
            this.ChargeController = chargecontroller;
            this.User = userId;
        }
        
        // Keys
        public int Id { get; set; }
        public ChargeController ChargeController { get; set; }
        public ApplicationUser User { get; set; }

        public static List<ChargeController> ConvertToChargeController(List<UserRole> userRoles)
        {
            return (from userRole in userRoles select userRole.ChargeController).ToList();
        }
    }
}