using System.ComponentModel;

namespace DomainModel.Entities
{
    public class ShippingDetails : IDataErrorInfo
    {
        public string Name { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public bool GiftWrap { get; set; }
        
        public string this[string columnName] // Validation rules
        {
            get {
                if ((columnName == "Name") && string.IsNullOrEmpty(Name))
                    return "Please enter a name";
                if ((columnName == "Line1") && string.IsNullOrEmpty(Line1))
                    return "Please enter the first address line";
                if ((columnName == "City") && string.IsNullOrEmpty(City))
                    return "Please enter a city name";
                if ((columnName == "State") && string.IsNullOrEmpty(State))
                    return "Please enter a state name";
                if ((columnName == "Country") && string.IsNullOrEmpty(Country))
                    return "Please enter a country name"; 
                return null;
            }
        }

        public string Error { get { return null; } } // Not required
    }
}