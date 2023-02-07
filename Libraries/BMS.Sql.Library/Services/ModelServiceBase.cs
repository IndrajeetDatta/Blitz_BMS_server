using BMS.Sql.Library;

namespace BMS.Sql.Library.Services
{
    public class ModelServiceBase
    {
        public BMSDbContext BMSDbContext { get; set; }
        public ModelServiceBase(BMSDbContext bmsDbContext)
        {
            BMSDbContext = bmsDbContext;
        }
    }
}
