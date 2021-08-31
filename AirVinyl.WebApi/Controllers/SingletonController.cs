using AirVinyl.Entities;
using AirVinyl.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AirVinyl.WebApi.Controllers
{
    [Route("odata/Lukasz")]
    public class SingletonController : ODataController
    {
        private readonly IRepository _repository;

        public SingletonController(IRepository repository)
        {
            _repository = repository;
        }

        #region GET

        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(SingleResult.Create( _repository.AsQueryable<Person>().Where(p => p.Id == 4)));
        }

        [HttpGet("{property}")]
        public async Task<IActionResult> GetPropertyValue(string property)
        {
            var propertyInfo = typeof(Person).GetProperty(property);
            if (propertyInfo == null)
                return BadRequest();

            var person = await _repository.GetByIdAsync<Person>(4);

            return Ok(propertyInfo.GetValue(person));
        }

        [HttpGet("{property}/$value")]
        public async Task<IActionResult> GetPropertyRawValue(string property)
        {
            var propertyInfo = typeof(Person).GetProperty(property);
            if (propertyInfo == null)
                return BadRequest();

            var person = await _repository.GetByIdAsync<Person>(4);

            return Ok(propertyInfo.GetValue(person).ToString());
        }

        #endregion

        #region PATCH

        [HttpPatch]
        public async Task<IActionResult> Patch(Delta<Person> patch)
        {
            if (patch.GetChangedPropertyNames().Contains("Id", StringComparer.OrdinalIgnoreCase))
                return BadRequest();

            var person = await _repository.GetByIdAsync<Person>(4);
            patch.Patch(person);

            await _repository.UpdateAsync(person);

            return NoContent();
        }

        #endregion
    }
}
