namespace Camp.Models
{
    public class Spot
    {
        public int Id { get; set; } 

        
        public string Name { get; set; }
        public string Description { get; set; }

        public string Location { get; set; }  

        public double PricePerNight { get; set; }


        public int OwnerId { get; set; } 
    }
}
