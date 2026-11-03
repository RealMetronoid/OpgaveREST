namespace TestREST.Models
{
    public interface IRecordRepository
    {
        IEnumerable<Record> GetAllRecords(string? title = null, string? artist = null, string? sortBy = null);
        Record? GetRecordById(int id);
        Record AddRecord(Record record);
        Record? RemoveRecord(int id);
    }
}
