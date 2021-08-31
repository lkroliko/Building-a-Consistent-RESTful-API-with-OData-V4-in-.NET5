using AirVinyl.Entities;
using AirVinyl.SharedKernel.Interfaces;
using AirVinyl.WebApi.Extensions;
using AutoMapper;
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
    [Route("odata/[controller]")]
    public class PeopleController : ODataController
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public PeopleController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        #region GET

        [EnableQuery]
        [HttpGet(Name = nameof(GetPeople))]
        public IActionResult GetPeople()
        {
            return Ok(_repository.AsQueryable<Person>());
        }

        [EnableQuery]
        [HttpGet("({id})", Name = nameof(GetPerson))]
        public IActionResult GetPerson(int id)
        {
            var person = _repository.AsQueryable<Person>().Where(p => p.Id == id);

            if (person.Any() == false)
                return NotFound();

            return Ok(SingleResult.Create(person));
        }

        [HttpGet("({id})/{property}", Name = nameof(GetPersonProperty))]
        public async Task<IActionResult> GetPersonProperty(int id, string property)
        {
            var propertyInfo = typeof(Person).GetProperty(property);

            var person = await _repository.GetByIdAsync<Person>(id);
            if (person == null)
                return NotFound();

            var value = propertyInfo.GetValue(person);

            return Ok(value);
        }

        [HttpGet("({id})/{property}/$value", Name = nameof(GetPersonPropertyRawValue))]
        public async Task<IActionResult> GetPersonPropertyRawValue(int id, string property)
        {
            var propertyInfo = typeof(Person).GetProperty(property);

            var person = await _repository.GetByIdAsync<Person>(id);
            if (person == null)
                return NotFound();

            var value = propertyInfo.GetValue(person);

            return Ok(value.ToString());
        }

        #endregion

        #region POST

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Person person)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            await _repository.AddAsync(person);

            return Created(person);
        }

        [HttpPost("({key})/VinylRecords/$ref")]
        public async Task<IActionResult> CreateLinkToVinylRecord(int key, [FromBody] Uri link)
        {
            var person = await _repository.GetByIdAsync<Person>(key);
            if (person == null)
                return NotFound();

            var refKey = link.GetOdataIntKey();

            if (person.VinylRecords.Any(v => v.Id == refKey))
                return BadRequest($"Person with id {person.Id} is already linked with Vinyl Record with id {refKey}.");

            var vinylRecord = await _repository.GetByIdAsync<VinylRecord>(refKey);
            if (vinylRecord == null)
                return NotFound();

            person.VinylRecords.Add(vinylRecord);
            await _repository.UpdateAsync(person);

            return NoContent();
        }

        #endregion

        #region PUT

        [HttpPut]
        public async Task<IActionResult> Put(int key, [FromBody] Person person)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            var personEntity = await _repository.GetByIdAsync<Person>(key);
            if (personEntity == null)
                return NotFound();

            person.Id = personEntity.Id;
            await _repository.UpdateAsync(personEntity, person);

            return NoContent();
        }

        [HttpPut("({key})/VinylRecords({relatedKey})/$ref")]
        public async Task<IActionResult> UpdateLinkToVinylRecord(int key, int relatedKey, [FromBody] Uri link)
        {
            var person = await _repository.GetByIdAsync<Person>(key);
            if (person == null)
                return NotFound($"No Person with id {key}.");

            if (person.VinylRecords.Any(v => v.Id == relatedKey) == false)
                return NotFound($"Person with id {key} is not linked with Vinyl Record with id {relatedKey}.");

            var refKey = link.GetOdataIntKey();
            var vinylRecord = await _repository.GetByIdAsync<VinylRecord>(refKey);
            if (vinylRecord == null)
                return NotFound($"No Vinyl Record with id {refKey}.");

            person.VinylRecords.Remove(person.VinylRecords.First(v => v.Id == relatedKey));
            person.VinylRecords.Add(vinylRecord);

            await _repository.UpdateAsync(person);

            return NoContent();
        }

        #endregion

        #region PATCH
        
        [HttpPatch]
        public async Task<IActionResult> Patch(int key, Delta<Person> patch)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            if (patch.GetChangedPropertyNames().Contains("Id", StringComparer.OrdinalIgnoreCase))
                return BadRequest();

            var person = await _repository.GetByIdAsync<Person>(key);
            if (person == null)
                return NotFound();

            patch.Patch(person);
            await _repository.UpdateAsync(person);

            return NoContent();
        }

        #endregion

        #region DELETE

        [HttpDelete]
        public async Task<IActionResult> Delete(int key)
        {
            var person = await _repository.GetByIdAsync<Person>(key);
            if (person == null)
                return NotFound();

            await _repository.DeleteAsync(person);

            return NoContent();
        }

        [HttpDelete("({key})/VinylRecords({relatedKey})")]
        public async Task<IActionResult> DeleteLinkToVinylRecord(int key, int relatedKey)
        {
            var person = await _repository.GetByIdAsync<Person>(key);
            if (person == null)
                return NotFound();

            var vinylRecord = person.VinylRecords.FirstOrDefault(v => v.Id == relatedKey);
            if (vinylRecord == null)
                return NotFound();

            person.VinylRecords.Remove(vinylRecord);
            await _repository.UpdateAsync(person);

            return NoContent();
        }

        #endregion
    }
}
