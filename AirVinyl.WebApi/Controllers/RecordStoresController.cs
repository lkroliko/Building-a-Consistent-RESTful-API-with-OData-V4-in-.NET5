using AirVinyl.Entities;
using AirVinyl.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Linq;
using System.Threading.Tasks;

namespace AirVinyl.WebApi.Controllers
{
    [Route("odata")]
    public class RecordStoresController : ODataController
    {
        private readonly IRepository _repository;

        public RecordStoresController(IRepository repository)
        {
            _repository = repository;
        }

        #region GET

        [HttpGet("RecordStores")]
        [EnableQuery]
        public IActionResult GetRecordStores()
        {
            return Ok(_repository.AsQueryable<RecordStore>());
        }

        [HttpGet("RecordStores({key})")]
        [EnableQuery]
        public async Task<IActionResult> GetRecordStore(int key)
        {
            if (await _repository.AnyAsync<RecordStore>(key) == false)
                return NotFound();

            return Ok(SingleResult.Create(_repository.AsQueryable<RecordStore>().Where(r => r.Id == key)));
        }

        #endregion

        #region POST

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RecordStore recordStore)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            await _repository.AddAsync(recordStore);

            return Created(recordStore);
        }

        #endregion

        #region PATCH

        [HttpPatch("RecordStores({id})")]
        [HttpPatch("RecordStores({id})/AirVinyl.SpecializedRecordStore")]
        public async Task<IActionResult> Patch(int id, Delta<RecordStore> patch)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            var recordStore = await _repository.GetByIdAsync<RecordStore>(id);
            if (recordStore == null)
                return NotFound();

            patch.Patch(recordStore);
            await _repository.UpdateAsync(recordStore);

            return NoContent();
        }

        #endregion

        #region DELETE

        [HttpDelete("RecordStores({id})")]
        [HttpDelete("RecordStores({id})/AirVinyl.SpecializedRecordStore")]
        public async Task<IActionResult> Delete(int id)
        {
            var recordStore = await _repository.GetByIdAsync<RecordStore>(id);
            if (recordStore == null)
                return NotFound();

            await _repository.DeleteAsync(recordStore);

            return NoContent();
        }

        #endregion
    }
}
