using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestREST.Models;

namespace TestREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : Controller
    {
        private RecordRepositoryList recordRepo;
        public RecordController(RecordRepositoryList repo)
        {
            recordRepo = repo;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Record>> GetAllRecords()
        {
            try
            {
                IEnumerable<Record> result = recordRepo.GetAllRecords();

                if (result == null || !result.Any())
                {
                    return NoContent(); // Returnerer 204 NoContent hvis listen er tom
                }

                return Ok(result); // Returnerer 200 OK med listen af Phone
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // Returnerer 400 BadRequest ved ugyldige argumenter
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<Record> Get(int id)
        {
            Record? record = recordRepo.GetRecordById(id);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }

        // POST api/<RecordsController>
        //[Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Record> Post([FromBody] Record newRecord)
        {
            try
            {
                Record createdRecord = recordRepo.AddRecord(newRecord);
                return Ok(createdRecord); // Eller CreatedAtAction hvis du vil returnere 201
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<RecordsController>/5
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Record?> Delete(int id)
        {
            Record? deletedRecord = recordRepo.RemoveRecord(id);
            if (deletedRecord == null)
            {
                return NotFound();
            }
            return Ok(deletedRecord);
        }
    }
}
