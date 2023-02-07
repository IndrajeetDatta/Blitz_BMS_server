using BMS.Sql.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Sql.Library.Services
{
    public class EmailService : ModelServiceBase
    {
        public EmailService(BMSDbContext bmsDbContext) : base(bmsDbContext) { }

        public Email Get(int id)
        {
            return BMSDbContext.Emails.SingleOrDefault(x => x.Id == id);
        }
        public List<Email> GetAll(string masterId, DateTimeOffset? lastCreatedDate = null)
        {
            return BMSDbContext.Emails.ToList();
        }

        public List<Email> GetAllEmailsForChargeControler(int chargeControllerId)
        {
            return BMSDbContext.Emails.Where(x => x.ChargeControllerId == chargeControllerId).ToList();
        }

        public Email Save(Email email)
        {
            try
            {
                if (email == null || email.Id > 1) return new Email();

                Email savedEmail = BMSDbContext.Emails.Add(email).Entity;
                BMSDbContext.SaveChanges();

                return savedEmail;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Email();
            }
        }
    }
}
