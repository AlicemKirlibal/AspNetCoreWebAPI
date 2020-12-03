using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Models;
using TestProject.Core.Repositories;
using TestProject.Core.Services;
using TestProject.Core.UnitOfWorks;
using TestProject.Data.UnitOfWorks;

namespace TestProject.Service.Services
{
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Product> _repository;

        public ProductService(IUnitOfWork unitOfWork, IRepository<Product> repository) : base(unitOfWork, repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<Product> GetWithCategoryByIdAsync(int productId)
        {
            return await _unitOfWork.products.GetWithCategoryByIdAsync(productId);
        }
    }
}
