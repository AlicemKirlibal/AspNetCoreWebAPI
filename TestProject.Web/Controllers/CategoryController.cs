using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestProject.Web.DTOs;

using TestProject.Web.Filters;
using TestProject.Web.ApiService;

namespace TestProject.Web.Controllers
{
    public class CategoryController : Controller
    {

        private readonly CategoryApiService _categoryApiService;
        private readonly IMapper _mapper;


        public CategoryController(IMapper mapper,CategoryApiService categoryApiService)
        {

            _categoryApiService = categoryApiService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryApiService.GetAllAsync();

            return View(_mapper.Map<IEnumerable<CategoryDto>>(categories));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto category)
        {
             await _categoryApiService.AddAsync(category);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var updated = await _categoryApiService.GetByIdAsync(id);

            return View(_mapper.Map<CategoryDto>(updated));
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryDto categoryDto)
        {
           await _categoryApiService.Update(categoryDto); 

            return RedirectToAction("Index");
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        public async Task<IActionResult> Delete(int id)
        {
     

           await _categoryApiService.Remove(id);
            return RedirectToAction("Index");

        }


    }
}