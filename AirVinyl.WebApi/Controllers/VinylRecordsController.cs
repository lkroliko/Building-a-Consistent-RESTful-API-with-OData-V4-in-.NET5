//using AirVinyl.Entities;
//using AirVinyl.SharedKernel.Interfaces;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.OData.Routing.Controllers;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace AirVinyl.WebApi.Controllers
//{
//    public class VinylRecordsController : ODataController
//    {
//        private readonly IRepository _repository;

//        public VinylRecordsController(IRepository repository)
//        {
//            _repository = repository;
//        }

//        [HttpGet("odata/VinylRecords")]
//        public IActionResult GetAllVinylRecords()
//        {
//            return Ok(_repository.AsQueryable<VinylRecord>());
//        }

//        [HttpGet("odata/VinylRecords({id})")]
//        public async Task<IActionResult> GetById(int id)
//        {
//            var vinylRecord = await _repository.GetByIdAsync<VinylRecord>(id);

//            if (vinylRecord == null)
//                return NotFound();

//            return Ok(vinylRecord);
//        }
//    }
//}
