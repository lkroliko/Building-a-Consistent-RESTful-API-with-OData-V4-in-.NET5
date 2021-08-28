using AirVinyl.Entities;
using AirVinyl.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AirVinyl.WebApi.Controllers
{
    public partial class PeopleVinylRecordsController : ODataController
    {
        private readonly IRepository _repository;

        public PeopleVinylRecordsController(IRepository repository)
        {
            _repository = repository;
        }

        #region GET

        [EnableQuery]
        [HttpGet("odata/People({key})/VinylRecords")]
        public async Task<IActionResult> GetPersonVinylRecords(int key)
        {
            if (await _repository.AnyAsync<Person>(key) == false)
                return NotFound();

            var vinylRecords = _repository.AsQueryable<VinylRecord>().Where(v => v.Person.Id == key);
            return Ok(vinylRecords);
        }

        [EnableQuery]
        [HttpGet("odata/People({personKey})/VinylRecords({vinylRecordKey})")]
        public IActionResult GetPersonVinylRecord(int personKey, int vinylRecordKey)
        {
            var person = _repository.AsQueryable<Person>().Where(p => p.Id == personKey);
            if (person.Any() == false)
                return NotFound();

            var vinylRecord = _repository.AsQueryable<VinylRecord>().Where(v => v.Person.Id == personKey && v.Id == vinylRecordKey);
            if (vinylRecord.Any() == false)
                return NotFound();

            return Ok(SingleResult.Create(vinylRecord));
        }

        #endregion

        #region POST

        [HttpPost("odata/People({key})/VinylRecords")]
        public async Task<IActionResult> PostVinylRecord(int key, [FromBody] VinylRecord vinylRecord)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            var person = await _repository.GetByIdAsync<Person>(key);
            if (person == null)
                return NotFound();

            person.VinylRecords.Add(vinylRecord);
            await _repository.UpdateAsync(person);

            return Created(vinylRecord);
        }

        #endregion

        #region PATCH

        [HttpPatch("odata/People({personKey})/VinylRecords({vinylRecordKey})")]
        public async Task<IActionResult> PatchVinylRecord(int personKey, int vinylRecordKey, [FromBody] Delta<VinylRecord> patch)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            if (patch.GetChangedPropertyNames().Contains("Id"))
                return BadRequest();

            if (await _repository.AnyAsync<Person>(personKey) == false)
                return NotFound();

            var vinylRecord = await _repository.AsQueryable<VinylRecord>().FirstOrDefaultAsync(v => v.Id == vinylRecordKey && v.Person.Id == personKey);
            if (vinylRecord == null)
                return NotFound();

            patch.Patch(vinylRecord);
            await _repository.UpdateAsync(vinylRecord);

            return NoContent();
        }

        #endregion

        #region DELETE

        [HttpDelete("odata/People({personKey})/VinylRecords({vinylRecordKey})")]
        public async Task<IActionResult> DeleteVinylRecord(int personKey, int vinylRecordKey)
        {
            if (await _repository.AnyAsync<Person>(personKey) == false)
                return NotFound();

            var vinylRecord = await _repository.GetByIdAsync<VinylRecord>(vinylRecordKey);
            if (vinylRecord == null)
                return NotFound();

            await _repository.DeleteAsync(vinylRecord);
            return NoContent();
        }

        #endregion
    }
}
