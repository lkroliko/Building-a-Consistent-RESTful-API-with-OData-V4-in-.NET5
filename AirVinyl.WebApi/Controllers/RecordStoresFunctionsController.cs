using AirVinyl.Entities;
using AirVinyl.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirVinyl.WebApi.Controllers
{
    [Route("odata")]
    public class RecordStoresFunctionsController : ODataController
    {
        private readonly IRepository _repository;

        public RecordStoresFunctionsController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("RecordStores({key})/AirVinyl.Functions.IsHighRated(minimumRating={minimumRating})")]
        public bool IsHighRated(int key, int minimumRating)
        {
            return _repository.AsQueryable<RecordStore>()
                .Where(r => r.Id == key && (r.Ratings.Sum(s => s.Value) / r.Ratings.Count()) >= minimumRating)
                .Any();
        }

        [HttpGet("RecordStores/AirVinyl.Functions.AreRatedBy(personIds={personIds})")]
        public IActionResult AreReatedBy([FromODataUri] IEnumerable<int> personIds)
        {
            return Ok(_repository.AsQueryable<RecordStore>().Where(r => r.Ratings.Any(r => personIds.Contains(r.RatedBy.Id))));
        }
    }
}
