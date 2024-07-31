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
    public class ArticleController : ControllerBase
    {
        public static List<Article> Articles { get; set; }
        public ArticleController()
        {
            Category category = new Category() { Name = "c1" };
            Articles = new List<Article>()
            {
                new Article(){Id=1,Name="a1",CreatedDate=new DateTime(1993,2,28), UpdatedDate=null, Category=category },
                new Article(){Id=2,Name="a2",CreatedDate=new DateTime(1993,2,28), UpdatedDate=null, Category=category },
                new Article(){Id=3,Name="a3",CreatedDate=new DateTime(1993,2,28), UpdatedDate=null ,Category=category}
            };
        }

        [HttpGet]
        public IEnumerable<Article> Get()
        {
            return Articles;
        }

        [HttpGet("{id}")]
        public Article Get(int id)
        {
            return Articles.FirstOrDefault(a => a.Id == id);
        }
    }
}
