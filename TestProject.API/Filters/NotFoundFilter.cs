using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.API.DTOs;
using TestProject.Core.Services;

namespace TestProject.API.Filters
{
    public class NotFoundFilter:ActionFilterAttribute
    {
        private readonly IProductService _productservice;
        public NotFoundFilter(IProductService productService)
        {
            _productservice = productService;
        }


        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int id = (int)context.ActionArguments.Values.FirstOrDefault();

            var product = await _productservice.GetByIdAsync(id);

            if (product!=null)
            {
                await next();
            }
            else
            {
                ErrorDto errorDto = new ErrorDto();
                errorDto.Status = 404;

                errorDto.Errors.Add($"Idsi {id} olan ürün veritabanımızda bulunamadı");

                context.Result = new NotFoundObjectResult(errorDto);
            }

        }


    }
}
