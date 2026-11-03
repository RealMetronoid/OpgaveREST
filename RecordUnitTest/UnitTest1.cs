using TestREST.Models;
using Record = TestREST.Models.Record;

namespace RecordUnitTest
{
    public class UnitTest1
    {
        private IRecordRepository repo;
        public UnitTest1()
        {
            repo = new RecordRepositoryList(includeData: false);
        }

        [Fact]
        public void GetAllRecordsTest()
        {
            // Arrange
            var recordRepo = new RecordRepositoryList();

            // Act
            var records = recordRepo.GetAllRecords();

            // Assert
            Assert.NotNull(records);
        }

        [Fact]
        public void AddRecord_ValidRecord_AssignsIdAndAddsToList()
        {
            // Arrange
            var newRecord = new Record { Title = "Mad in love", Artist = "Progressive Metal" };

            // Act
            var addedRecord = repo.AddRecord(newRecord);

            // Assert
            Assert.Equal(1, addedRecord.Id);
            Assert.Single(repo.GetAllRecords());
            Assert.Equal("Mad in love", repo.GetRecordById(1)?.Title);
        }

        [Fact]
        public void RemoveRecord_DeletesAndReturns()
        {
            Record record = new Record { Artist = "Remover", Duration = 120, Listeners = 3, publicationYear = 2026, Title = "Get Removed" };
            Record added = repo.AddRecord(record);

            Record? removed = repo.RemoveRecord(added.Id);
            Assert.NotNull(removed);
            Assert.Equal(added.Id, removed!.Id);

            Record? lookup = repo.GetRecordById(added.Id);
            Assert.Null(lookup);

            Assert.Empty(repo.GetAllRecords());

        }
    }
}
