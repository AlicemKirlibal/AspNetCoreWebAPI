using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Models;
using TestProject.Core.Repositories;
using TestProject.Core.Services;
using TestProject.Core.UnitOfWorks;

namespace TestProject.Service.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Category> _repository;

        public CategoryService(IUnitOfWork unitOfWork,IRepository<Category> repository):base(unitOfWork,repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
        public async Task<Category> GetWithProductsByIdAsync(int categoryId)
        {
            return await _unitOfWork.categories.GetWithProductsByIdAsync(categoryId); 
        }
    }
}
