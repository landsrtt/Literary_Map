namespace Literary_Map.Models
{
    public class Book 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Year { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public Book() { }

        public Book(int id, string title, string author, string year, string genre, string description, double longitude, double latitude)
        {
            Id = id;
            Title = title;
            Author = author;
            Year = year;
            Genre = genre;
            Description = description;
            Longitude = longitude;
            Latitude = latitude;
        }

        public override string ToString()
        {
            return $"id {Id} title {Title} author {Author}";
        }
    }
}