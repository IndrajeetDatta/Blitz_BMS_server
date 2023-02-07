namespace BMS.Web.Api.Controllers
{
    public partial class Email
    {
        public Email() { }

        public Email(Sql.Library.Models.Email email)
        {
            if (email == null)
                return;
            Id = email.Id;
            Ftype = email.Ftype;
            Rfid = email.Rfid;
            ReceiverName = email.ReceiverName;
            EmailReceiver = email.EmailReceiver;
        }
    }
}
