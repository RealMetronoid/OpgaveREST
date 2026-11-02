namespace TestREST.Models
{
    public interface IRecordRepository
    {
        IEnumerable<Record> GetAllRecords();
        Record? GetRecordById(int id);
        Record AddRecord(Record record);
        Record? RemoveRecord(int id);
    }
}
