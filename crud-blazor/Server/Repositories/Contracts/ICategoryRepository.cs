using crud_blazor.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crud_blazor.Server.Repositories.Contracts
{
    public interface ICategoryRepository<TEntity> 
    {
        public IEnumerable<TEntity> GetCategories();
        public TEntity GetCategory(int id);
        public void AddCategory(TEntity obj);
        public void UpdateCategory(TEntity obj);
        public void DeleteCategory(int id);
    }
}
