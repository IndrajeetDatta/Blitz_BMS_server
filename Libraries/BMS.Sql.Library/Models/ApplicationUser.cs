namespace BMS.Sql.Library.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string? ExternalId { get; set; }
        public UserRoleEnum? Role { get; set; }
        public string IdentityUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string NickName { get; set; }
    }
    public enum UserRoleEnum
    {
        Admin,
        Manufacturer,
        Installer,
        Client
    }
}
