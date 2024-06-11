using System.Collections.Generic;

namespace Airbnb.Models
{
    namespace Airbnb.Models
    {
        public class Location
        {
            public int LocationId { get; set; }
            
            public string Country { get; set; }
            public string City { get; set; }
            public string Postcode { get; set; }

            //public ICollection<CampingSpace> CampingSpaces { get; set; }
        }
    }

}
