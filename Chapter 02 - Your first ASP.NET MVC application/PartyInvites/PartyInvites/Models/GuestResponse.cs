
namespace PartyInvites.Models
{
    using System.ComponentModel;
    using System.Text.RegularExpressions;
    using System.Net.Mail;
    using System.Linq;
    using System.Text;
    using System;

    public class GuestResponse : IDataErrorInfo
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool? WillAttend { get; set; }

        public string Error { get { return null; } } // Not required for this example

        public string this[string propName]
        {
            get
            {
                if ((propName == "Name") && string.IsNullOrEmpty(Name))
                    return "Please enter your name";
                if ((propName == "Email") && !Regex.IsMatch(Email, ".+\\@.+\\..+"))
                    return "Please enter a valid email address";
                if ((propName == "Phone") && string.IsNullOrEmpty(Phone))
                    return "Please enter your phone number";
                if ((propName == "WillAttend") && !WillAttend.HasValue)
                    return "Please specify whether you'll attend";
                return null;
            }
        }

        public void Submit()
        {
            EnsureCurrentlyValid();

            // Send via email
            var message = new StringBuilder();
            message.AppendFormat("Date: {0:yyyy-MM-dd hh:mm}\n", DateTime.Now);
            message.AppendFormat("RSVP from: {0}\n", Name);
            message.AppendFormat("Email: {0}\n", Email);
            message.AppendFormat("Phone: {0}\n", Phone);
            message.AppendFormat("Can come: {0}\n", WillAttend.Value ? "Yes" : "No");

            // ** Note - if you enable this code, you must also configure SMTP server settings in web.config
            // ** (see <system.net>/<mailSettings> in web.config)
            //SmtpClient smtpClient = new SmtpClient();
            //smtpClient.Send(new MailMessage(
            //    "rsvps@example.com",                                          // From
            //    "party-organizer@example.com",                                // To
            //    Name + (WillAttend.Value ? " will attend" : " won't attend"), // Subject
            //    message.ToString()                                            // Body
            //));
        }

        private void EnsureCurrentlyValid()
        {
            // Check that IDataErrorInfo.this[] returns null for every property
            var propsToValidate = new[] { "Name", "Email", "Phone", "WillAttend" };
            bool isValid = propsToValidate.All(x => this[x] == null);
            if (!isValid)
                throw new InvalidOperationException("Can't submit invalid GuestResponse");
        }
    }
}