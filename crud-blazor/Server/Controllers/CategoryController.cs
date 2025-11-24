using crud_blazor.Server.Repositories;
using crud_blazor.Server.Repositories.Contracts;
using crud_blazor.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crud_blazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        public readonly ICategoryRepository<Category> _categoryRepository;

        public CategoryController()
        {
            _categoryRepository = new CategoryRepository();
        }

        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return _categoryRepository.GetCategories();
        }

        [HttpGet("{id}")]
        public Category Get(int id)
        {
            return _categoryRepository.GetCategories().FirstOrDefault(c => c.Id == id);
        }

        [HttpPost]
        public void Post(Category category)
        {
            var categories = _categoryRepository.GetCategories();

            if (categories.Count() == 0)
            {
                category.Id = 1;
                category.CreatedDate = DateTime.Now;
            }
            else
            {
                category.Id = (categories.Max(c => c.Id) + 1);
                category.CreatedDate = DateTime.Now;
            }

            _categoryRepository.AddCategory(category);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var category = _categoryRepository.GetCategories().FirstOrDefault(c => c.Id == id);

            if (category != null)
            {
                _categoryRepository.DeleteCategory(category.Id);
            }
        }
    }
}
