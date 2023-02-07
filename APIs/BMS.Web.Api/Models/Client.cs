namespace BMS.Web.Api.Controllers
{
    public partial class Client
    {
        public Client() { }

        public Client(Sql.Library.Models.Client client)
        {
            if (client == null)
                return;
            Id = client.Id;
            Name = client.Name;
            Email = client.Email;
            Location = client.Location;
            Phone = client.Phone;
        }

        public Sql.Library.Models.Client ToSqlModel()
        {
            return new Sql.Library.Models.Client
            {
                Id = Id,
                Name = Name,
                Phone = Phone,
                Email = Email,
                Location = Location,
            };
        }
    }
}
