using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Repositories;
using TestProject.Core.UnitOfWorks;
using TestProject.Data.Repositories;

namespace TestProject.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private ProductRepository _productRepository;

        private CategoryRepository _CategoryRepository;

        //repository lere ulaşmak için nesneler tanımlandı

        public IProductRepository products => _productRepository = _productRepository ?? new ProductRepository(_context);

        public ICategoryRepository categories => _CategoryRepository = _CategoryRepository ?? new CategoryRepository(_context);

        

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
