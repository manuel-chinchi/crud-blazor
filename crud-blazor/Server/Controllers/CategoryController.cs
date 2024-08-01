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
        public static List<Category> Categories { get; set; } = new List<Category>()
            {
                new Category() {Id=1, Name="c1"},
                new Category() {Id=2, Name="c2"},
                new Category() {Id=3, Name="c3"}
            };

        public CategoryController()
        {
        }

        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return Categories;
        }

        [HttpGet("{id}")]
        public Category Get(int id)
        {
            return Categories.FirstOrDefault(c => c.Id == id);
        }

        [HttpPost]
        public void Post(Category category)
        {
            if (Categories.Count == 0)
            {
                category.Id = 1;
                Categories.Add(category);
            }
            else
            {
            category.Id = (Categories.Max(c => c.Id) + 1);
            Categories.Add(category);
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var c = Categories.FirstOrDefault(c => c.Id == id);
            if (c != null)
            {
                Categories.Remove(c);
            }
        }
    }
}
