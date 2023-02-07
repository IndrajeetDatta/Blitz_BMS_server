namespace BMS.Web.Api.Controllers
{
    public partial class ApplicationUser
    {
        public ApplicationUser() { }

        public ApplicationUser(Sql.Library.Models.ApplicationUser user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
        }

        public Sql.Library.Models.ApplicationUser ToSqlModel()
        {
            return new Sql.Library.Models.ApplicationUser
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
            };
        }
    }
}
