using AirVinyl.Entities;
using AirVinyl.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Linq;
using System.Threading.Tasks;

namespace AirVinyl.WebApi.Controllers
{
    public class RecordStoresController : ODataController
    {
        private readonly IRepository _repository;

        public RecordStoresController(IRepository repository)
        {
            _repository = repository;
        }

        #region GET

        [HttpGet("odata/RecordStores")]
        [EnableQuery]
        public IActionResult GetRecordStores()
        {
            return Ok(_repository.AsQueryable<RecordStore>());
        }

        [HttpGet("odata/RecordStores({key})")]
        [EnableQuery]
        public async Task<IActionResult> GetRecordStore(int key)
        {
            if (await _repository.AnyAsync<RecordStore>(key) == false)
                return NotFound();

            return Ok(SingleResult.Create(_repository.AsQueryable<RecordStore>().Where(r=>r.Id == key)));
        }

        #endregion
    }
}
