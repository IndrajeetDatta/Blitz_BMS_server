using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMS.Sql.Library.Models;

namespace BMS.Sql.Library.Services
{
    public class LogService
    {
        private readonly BMSDbContext DbContext;

        public LogService(BMSDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public Log? GetById(int id)
        {
            return DbContext.Logs.SingleOrDefault(x => x.Id == id);
        }

        public Log Save(Log log)
        {
            if (log == null) return new Log();
           
            DbContext.Logs.Add(log);

            try
            {
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return log;
        }
    }
}