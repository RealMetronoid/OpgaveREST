namespace TestREST.Models
{
    public class RecordRepositoryList : IRecordRepository
    {
        private readonly List<Record> records = new List<Record>();

        private int nextId = 1;


        public IEnumerable<Record> GetAllRecords()
        {
            return records.AsReadOnly();
        }

        public Record? GetRecordById(int id)
        {
            return records.FirstOrDefault(r => r.Id == id);
        }

        public Record AddRecord(Record record)
        {
            if (record == null) {
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
