namespace Cars.Models
{
    public class Car
    {
        public int Id { get; set; }
        public required string Brand { get; set; }
        public required string Model { get; set; }
        public required Decimal Price { get; set; }
        public required DateTime Year { get; set; }
        public required string Image {  get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
