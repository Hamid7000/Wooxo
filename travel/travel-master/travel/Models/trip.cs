namespace travel.Models
{
    public class trip
    {
        public int TripId { get; set; }

        // Foreign key
        public int UserId { get; set; }

        // Navigation property to User
        public User User { get; set; }

        public string Destination { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal TotalBudget { get; set; }

        public decimal RemainingBudget { get; set; }
    }
}
