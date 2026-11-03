namespace TestREST.Models
{
    public class Record
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Artist { get; set; }
        public int Duration { get; set; }
        public int publicationYear { get; set; }
        public int Listeners { get; set; }
    }
}
