using System;
using System.ComponentModel;

namespace WizardDemo.Models
{
    [Serializable]
    public class RegistrationData : IDataErrorInfo
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; }
        public string Hobbies { get; set; }

        public string this[string columnName]
        {
            get {
                if ((columnName == "Name") && string.IsNullOrEmpty(Name))
                    return "Please enter a name";
                if ((columnName == "Email") && !IsValidEmailAddress(Email))
                    return "Please enter a valid email address";
                if ((columnName == "Age") && !Age.HasValue)
                    return "Please enter a numeric age";
                return null;
            }
        }

        public string Error { get { return null; } } // Not required

        private static bool IsValidEmailAddress(string email)
        {
            // I'm sure you can improve this logic
            return (email != null) && (email.IndexOf("@") > 0);
        }
    }
}