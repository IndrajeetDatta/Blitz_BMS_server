namespace BMS.Sql.Library.Models
{
    public class UserData
    {
        // Keys
        public int Id { get; set; }
        public ChargeController ChargeController { get; set; }
        public int ChargeControllerId { get; set; }

        // Metadata

        public string? JsonData { get; set; }
        public DateTimeOffset? Created { get; set; }
    }
}
