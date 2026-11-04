namespace TestREST.Models
{
    public class RecordRepositoryList : IRecordRepository
    {
        private readonly List<Record> records = new List<Record>();

        private int nextId = 1;

        public RecordRepositoryList(bool includeData = true)
        {
            if (includeData)
            {
                AddRecord(new Record { Title = "Mad in love", Artist = "Progressive Metal", Duration = 300, publicationYear = 2023, Listeners = 1000 });
                AddRecord(new Record { Title = "Still human", Artist = "Daft Punk", Duration = 350, publicationYear = 2016, Listeners = 100000 });
                AddRecord(new Record { Title = "Hearts and minds", Artist = "Kale Eaters", Duration = 200, publicationYear = 2020, Listeners = 50 });
                AddRecord(new Record { Title = "Kojak", Artist = "One Republic", Duration = 400, publicationYear = 2026, Listeners = 2000 });
            }
        }


        public IEnumerable<Record> GetAllRecords(string? title = null, string? artist = null, string? sortBy = null)
        {
            List<Record> tempRecords = new(records);

            if (title != null)
            {
                tempRecords = tempRecords.FindAll(record => record.Title.Contains(title));
            }

            if (artist != null)
            {
                tempRecords = tempRecords.FindAll(record => record.Artist.Contains(artist));
            }

            if (sortBy != null)
            {
                switch (sortBy.ToLower())
                {
                    case "id":
                        tempRecords = tempRecords.OrderBy(record => record.Id).ToList();
                        break;
                    case "title":
                        tempRecords = tempRecords.OrderBy(record => record.Title).ToList();
                        break;
                    case "artist":
                        tempRecords = tempRecords.OrderBy(record => record.Artist).ToList();
                        break;
                    case "listnerasc":
                        tempRecords = tempRecords.OrderBy(record => record.Listeners).ToList();
                        break;
                    case "listenerdesc":
                        tempRecords = tempRecords.OrderByDescending(record => record.Listeners).ToList();
                        break;
                }
            }

            return tempRecords;
        }

        public Record? GetRecordById(int id)
        {
            return records.FirstOrDefault(r => r.Id == id);
        }

        public Record AddRecord(Record record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            record.Id = nextId++;
            records.Add(record);
            return record;
        }

        public Record? RemoveRecord(int id)
        {
            var record = GetRecordById(id);
            if (record != null)
            {
                records.Remove(record);
                return record;
            }
            return null;
        }
    }
}
