using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Models;
using TestProject.Core.Repositories;

namespace TestProject.Data.Repositories
{

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private AppDbContext _appDbcontext { get => _context as AppDbContext; }

        public ProductRepository(AppDbContext context):base(context)
        {

        }

        public async Task<Product> GetWithCategoryByIdAsync(int productId)
        {
            return await _appDbcontext.Products.Include(x => x.Category).SingleOrDefaultAsync(x => x.Id == productId);
        }
    }
}
