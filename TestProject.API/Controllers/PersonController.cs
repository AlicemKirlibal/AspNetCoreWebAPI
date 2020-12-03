using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestProject.Core.Models;
using TestProject.Core.Services;

namespace TestProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IService<Person> _service;

        public PersonController(IService<Person> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var persons = await _service.GetAllAsync();

            return Ok(persons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            var selected = await _service.GetByIdAsync(id);

            return Ok(selected);
        }
        [HttpPost]
        public async Task<IActionResult> Save(Person person)
        {
            var selected = await _service.AddAsync(person);

            return Ok(selected);
        }
        [HttpPut]
        public IActionResult Update(Person person)
        {
            var updated = _service.Update(person);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            var selected = _service.GetByIdAsync(id).Result;
            _service.Remove(selected);

            return NoContent();

        }





    }
}