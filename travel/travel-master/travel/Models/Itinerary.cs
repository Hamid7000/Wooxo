namespace travel.Models
{
    public class Itinerary
    {
        public int ItineraryId { get; set; }

        // Foreign key
        public int TripId { get; set; }

        // Navigation property to Trip
        public trip Trip { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }

}
