using AirVinyl.Entities;
using AirVinyl.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Threading.Tasks;
using AirVinyl.WebApi.Extensions;
namespace AirVinyl.WebApi.Controllers
{
    public class RecordStoresAcionsController : ODataController
    {
        private readonly IRepository _repository;

        public RecordStoresAcionsController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("odata/RecordStore({key})/AirVinyl.Actions.Rate")]
        public async Task<IActionResult> Rate(int key, ODataActionParameters parameters)
        {
            var recordStore = await _repository.GetByIdAsync<RecordStore>(key);
            if (recordStore == null)
                return NotFound();

            if (parameters.TryGetValue<int>("rating", out int rating) == false)
                return NotFound();

            if (parameters.TryGetValue<int>("personId", out int personId) == false)
                return NotFound();

            var person = await _repository.GetByIdAsync<Person>(personId);
            if (person == null)
                return NotFound();

            recordStore.Ratings.Add(new Rating() { RatedBy = person, Value = rating });

            await _repository.UpdateAsync(recordStore);
            return Ok(true);
        }
    }
}
