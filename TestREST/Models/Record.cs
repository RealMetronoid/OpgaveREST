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

        public override bool Equals(object? obj)
        {
            if (obj  == null) return false;

            if (obj.GetType() != typeof(Record)) return false;

            Record record = (Record)obj;
            return this.Id == record.Id;
        }
    }
}
