namespace Airbnb.Models
{
    namespace Airbnb.Models
    {
        public class Booking
        {
            public int BookingId { get; set; } 
            public int UserId { get; set; }
            public DateTime BookingDate { get; set; }
            public DateTime ArrivalDate { get; set; }
            public DateTime DepartureDate { get; set; }
            
            public int NoOfGuests { get; set; }
            
            public int CampingSpaceId { get; set; }
            public string CampingSpaceName { get; set; } 

            public User User { get; set; }
        }
    }

}
