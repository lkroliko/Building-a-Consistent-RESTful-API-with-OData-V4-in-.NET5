using AirVinyl.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AirVinyl.WebApi.Extensions;
using AirVinyl.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirVinyl.WebApi.Controllers
{
    [Route("odata")]
    public class RootActionController : ODataController
    {
        private readonly IRepository _repository;

        public RootActionController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("RemoveRecordStoreRatings")]
        public async Task<IActionResult> RemoveRecordStoreRatings(ODataActionParameters parameters)
        {
            if (parameters.TryGetValue<int>("personId", out int personId) == false)
                return BadRequest();

            var ratings = await _repository.AsQueryable<Rating>().Where(r => r.RatedBy.Id == personId).ToListAsync();

            await _repository.DeleteRangeAsync(ratings);

            return NoContent();
        }
    }
}
