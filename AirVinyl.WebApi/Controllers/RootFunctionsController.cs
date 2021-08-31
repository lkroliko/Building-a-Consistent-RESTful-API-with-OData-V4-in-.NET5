using AirVinyl.Entities;
using AirVinyl.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Linq;

namespace AirVinyl.WebApi.Controllers
{
    [Route("odata")]
    public class RootFunctionsController : ODataController
    {
        private readonly IRepository _repository;

        public RootFunctionsController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetHighRatedRecordStores(minimumRating={minimumRating})")]
        public IActionResult GetHighRatedRecordStores(int minimumRating)
        {
            return Ok(_repository.AsQueryable<RecordStore>().Where(r => (r.Ratings.Sum(s => s.Value) / r.Ratings.Count) >= minimumRating));
        }
    }
}
