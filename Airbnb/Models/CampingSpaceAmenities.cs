namespace Airbnb.Models
{
    namespace Airbnb.Models
    {
        public class CampingSpaceAmenities
        {
            public int Id { get; set; }
            public int CampingSpaceId { get; set; }
            public int AmenitiesId { get; set; }

            public CampingSpace CampingSpace { get; set; }
            public Amenities Amenities { get; set; }
        }
    }

}
