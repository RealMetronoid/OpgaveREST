using TestREST.Models;

namespace RecordUnitTest
{
    public class UnitTest1
    {
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
            var repo = new RecordRepositoryList();
            var newRecord = new Record { Title = "Mad in love", Artist = "Progressive Metal" };

            // Act
            var addedRecord = repo.AddRecord(newRecord);

            // Assert
            Assert.Equal(1, addedRecord.Id);
            Assert.Single(repo.GetAllRecords());
            Assert.Equal("Mad in love", repo.GetRecordById(1)?.Title);
        }
    }
}
