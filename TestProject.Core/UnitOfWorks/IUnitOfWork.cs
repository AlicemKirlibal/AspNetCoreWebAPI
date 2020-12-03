using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Repositories;

namespace TestProject.Core.UnitOfWorks
{
   public  interface IUnitOfWork
    {
        IProductRepository products { get; }
        ICategoryRepository categories { get; }

        Task CommitAsync();
        void Commit();

    }
}
