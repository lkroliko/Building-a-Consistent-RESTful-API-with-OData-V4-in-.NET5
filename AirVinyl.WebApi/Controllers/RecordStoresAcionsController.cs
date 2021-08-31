using AirVinyl.Entities;
using AirVinyl.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Threading.Tasks;
using AirVinyl.WebApi.Extensions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AirVinyl.WebApi.Controllers
{
    [Route("odata")]
    public class RecordStoresAcionsController : ODataController
    {
        private readonly IRepository _repository;

        public RecordStoresAcionsController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("RecordStores({key})/AirVinyl.Actions.Rate")]
        public async Task<IActionResult> Rate(int key, ODataActionParameters parameters)
        {
            var recordStore = await _repository.GetByIdAsync<RecordStore>(key);
            if (recordStore == null)
                return NotFound();

            if (parameters.TryGetValue<int>("rating", out int rating) == false)
                return BadRequest();

            if (parameters.TryGetValue<int>("personId", out int personId) == false)
                return BadRequest();

            var person = await _repository.GetByIdAsync<Person>(personId);
            if (person == null)
                return NotFound();

            recordStore.Ratings.Add(new Rating() { RatedBy = person, Value = rating });

            await _repository.UpdateAsync(recordStore);
            return Ok(true);
        }

        [HttpPost("RecordStores/AirVinyl.Actions.RemoveRatings")]
        public async Task<IActionResult> RemoveRatings(ODataActionParameters parameters)
        {
            if (parameters.TryGetValue<int>("personId", out int personId) == false)
                return BadRequest();

            var ratings = await _repository.AsQueryable<Rating>().Where(r => r.RatedBy.Id == personId).ToListAsync();
            if (ratings.Count == 0)
                return Ok(false);
            await _repository.DeleteRangeAsync(ratings);

            return Ok(true);
        }
    }
}
