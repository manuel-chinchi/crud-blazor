using crud_blazor.Server.Repositories.Contracts;
using crud_blazor.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crud_blazor.Server.Repositories
{
    public class CategoryRepository : ICategoryRepository<Category>
    {
        public readonly JSONRepository<Category> _jsonRepository;

        public CategoryRepository()
        {
            _jsonRepository = new JSONRepository<Category>("categories.json");
        }

        public void AddCategory(Category obj)
        {
            _jsonRepository.Add(obj);
        }

        public void DeleteCategory(int id)
        {
            _jsonRepository.Delete(id);
        }

        public IEnumerable<Category> GetCategories()
        {
            var categories = _jsonRepository.GetItems();
            return categories;
        }

        public Category GetCategory(int id)
        {
            var category = _jsonRepository.Get(id);
            return category;
        }

        public void UpdateCategory(Category obj)
        {
            _jsonRepository.Update(obj);
        }
    }
}
