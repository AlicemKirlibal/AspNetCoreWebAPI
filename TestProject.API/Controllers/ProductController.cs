using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestProject.API.DTOs;
using TestProject.API.Filters;
using TestProject.Core.Models;
using TestProject.Core.Services;

namespace TestProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        private readonly IMapper _mapper;

        public ProductController(IProductService productService,IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            var products = await _productService.GetByIdAsync(id);

            return Ok(_mapper.Map<ProductDto>(products));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var created = await _productService.AddAsync(_mapper.Map<Product>(productDto));

            return Created(string.Empty,_mapper.Map<ProductDto>(created));
        }

        [HttpPut]
        public IActionResult Update(ProductDto productDto)
        {

            var updated = _productService.Update(_mapper.Map<Product>(productDto));

            return NoContent();
           
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            var deleted = _productService.GetByIdAsync(id).Result;
            _productService.Remove(deleted);

            return NoContent();
        }


        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpGet("{id}/categories")]
        public async Task<IActionResult>GetCategoriesById(int id)
        {
            var product = await _productService.GetWithCategoryByIdAsync(id);
            return Ok(_mapper.Map<ProductWithCategoryDto>(product));

        }





    }
}