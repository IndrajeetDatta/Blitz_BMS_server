namespace BMS.Web.Api.Controllers
{
    public partial class UserData
    {
        public UserData() { }

        public UserData(Sql.Library.Models.UserData userData)
        {
            if (userData == null)
                return;
            Id = userData.Id;
            JsonData = userData.JsonData;
            Created = userData.Created;
        }
    }
}
