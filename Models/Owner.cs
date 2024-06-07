namespace Camp.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        // Add more owner-specific properties as needed

        // Navigation property to represent the camping spots owned by this owner
        public ICollection<Spot> OwnedCampingSpots { get; set; }
    }
}
