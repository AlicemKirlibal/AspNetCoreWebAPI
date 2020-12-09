using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestProject.Web.ApiService;
using TestProject.Web.DTOs;

namespace TestProject.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductApiService _productApiService;
        private readonly IMapper _mapper;

        public ProductController(ProductApiService productApiService,IMapper mapper)
        {
            _productApiService = productApiService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productApiService.GetAllAsync();

            return View(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Create(ProductDto productDto)
        {
            await _productApiService.AddAsync(productDto);

            return RedirectToAction("Index");
        }


    }
}