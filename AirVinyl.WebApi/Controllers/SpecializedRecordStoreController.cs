using AirVinyl.Entities;
using AirVinyl.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Linq;

namespace AirVinyl.WebApi.Controllers
{
    [Route("odata")]
    public class SpecializedRecordStoreController : ODataController
    {
        private readonly IRepository _repository;

        public SpecializedRecordStoreController(IRepository repository)
        {
            _repository = repository;
        }

        [EnableQuery]
        [HttpGet("RecordStores/AirVinyl.SpecializedRecordStore")]
        public IActionResult GetSpecializedRecordStores()
        {
            return Ok(_repository.AsQueryable<RecordStore>().Where(r => r is SpecializedRecordStore).Select( r=> r as SpecializedRecordStore));
        }

        [EnableQuery]
        [HttpGet("RecordStores({id})/AirVinyl.SpecializedRecordStore")]
        public IActionResult GetSpecializedRecordStore(int id)
        {
            var specializedRecordStore = _repository.AsQueryable<RecordStore>().Where(r => r is SpecializedRecordStore && r.Id == id).Select(r => r as SpecializedRecordStore);
            if (specializedRecordStore == null)
                return NotFound();

            return Ok(SingleResult.Create(specializedRecordStore));
        }
    }
}
