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
        public List<Category> Categories { get; set; }
        public CategoryController()
        {
            Categories = new List<Category>()
            {
                new Category() {Id=1, Name="c1"},
                new Category() {Id=2, Name="c2"},
                new Category() {Id=3, Name="c3"}
            };
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
    }
}
