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

        [Fact]
        public void GetAllRecords_Filtering()
        {
            repo = new RecordRepositoryList(includeData: true);

            var titleRecords = repo.GetAllRecords("Still human", "Daft Punk");

            Assert.Equal(1, titleRecords.Count());
        }

        [Fact]
        public void GetAllRecords_SortBy1()
        {
            repo = new RecordRepositoryList(includeData: true);

            var titleRecords = repo.GetAllRecords(sortBy: "title");

            // Define the expected order of titles
            List<string> expectedTitles = new()
            {
                "Hearts and minds",
                "Kojak",
                "Mad in love",
                "Still human"
            };

            // Extract titles from the actual result
            var actualTitles = titleRecords.Select(r => r.Title).ToList();

            Assert.Equal(expectedTitles, actualTitles);
        }

        [Fact]
        public void GetAllRecords_SortBy2()
        {
            repo = new RecordRepositoryList(includeData: true);

            var titleRecords = repo.GetAllRecords(sortBy: "title");

            //List<Record> unsorted_list = [
            //    new Record { Title = "Mad in love", Artist = "Progressive Metal", Duration = 300, publicationYear = 2023, Listeners = 1000 },
            //    new Record { Title = "Still human", Artist = "Daft Punk", Duration = 350, publicationYear = 2016, Listeners = 100000 },
            //    new Record { Title = "Hearts and minds", Artist = "Kale Eaters", Duration = 200, publicationYear = 2020, Listeners = 50 },
            //    new Record { Title = "Kojak", Artist = "One Republic", Duration = 400, publicationYear = 2026, Listeners = 2000 }
            //];

            IEnumerable<Record> sorted_list = [
                new Record {Id = 3, Title = "Hearts and minds", Artist = "Kale Eaters", Duration = 200, publicationYear = 2020, Listeners = 50 },
                new Record {Id = 4, Title = "Kojak", Artist = "One Republic", Duration = 400, publicationYear = 2026, Listeners = 2000 },
                new Record {Id = 1, Title = "Mad in love", Artist = "Progressive Metal", Duration = 300, publicationYear = 2023, Listeners = 1000 },
                new Record {Id = 2, Title = "Still human", Artist = "Daft Punk", Duration = 350, publicationYear = 2016, Listeners = 100000 }
            ];


            Assert.Equal(sorted_list, titleRecords);

        }
    }
}
