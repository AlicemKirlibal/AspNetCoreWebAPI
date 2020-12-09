using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestProject.Web.DTOs;

namespace TestProject.Web.ApiService
{
    public class ProductApiService
    {
        private readonly HttpClient _httpClient;

        public ProductApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            IEnumerable<ProductDto> productDtos;

            var response = await _httpClient.GetAsync("product");

            if (response.IsSuccessStatusCode)
            {
                productDtos = JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(await
                    response.Content.ReadAsStringAsync());
            }
            else
            {
                return null;
            }
            return productDtos;
        }

        public async Task<ProductDto> AddAsync(ProductDto productDto)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(productDto), Encoding.UTF8, ("application/json"));

            var response = await _httpClient.PostAsync("product", stringContent);

            if (response.IsSuccessStatusCode)
            {
                productDto = JsonConvert.DeserializeObject<ProductDto>(await

                    response.Content.ReadAsStringAsync());

                return productDto;
            }
            else
            {
                return null;
            }


        }



    }
}
