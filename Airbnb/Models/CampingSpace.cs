namespace Airbnb.Models
{
    namespace Airbnb.Models
    {
        public class CampingSpace
        {
            public int CampingSpaceId { get; set; }
            public string Name { get; set; }
            public int PricePerNight { get; set; }
            public string Address { get; set; }
            public decimal Longitude { get; set; }
            public decimal Latitude { get; set; }
            public int LocationId { get; set; }
            public int OwnerId { get; set; }
            public bool IsAvailable { get; set; }



            public List<int> AmenitiesIds { get; set; }
            public Location Location { get; set; }
            public List<Amenities> Amenities { get; set; }
        }
    }
}
