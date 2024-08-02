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
        public static List<Article> Articles { get; set; } = new List<Article>()
            {
                new Article(){Id=1,Name="a1",CreatedDate=new DateTime(1993,2,28), UpdatedDate=null, Category=new Category(){ Id=1, Name="c1", CreatedDate = new DateTime(1993,2,28)} },
                new Article(){Id=2,Name="a2",CreatedDate=new DateTime(1993,2,28), UpdatedDate=null, Category=new Category() { Id=2, Name="c2", CreatedDate = new DateTime(1993,2,28)} },
                new Article(){Id=3,Name="a3",CreatedDate=new DateTime(1993,2,28), UpdatedDate=null ,Category=new Category() {Id=3, Name="c3", CreatedDate = new DateTime(1993,2,28) } }
            };

        public ArticleController()
        {
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

        [HttpPost]
        public void Post(Article article)
        {
            if (Articles.Count == 0)
            {
                article.Id = 1;
                article.CreatedDate = DateTime.Now;
                Articles.Add(article);
            }
            else
            {
            article.Id = (Articles.Max(a => a.Id) + 1);
                article.CreatedDate = DateTime.Now;
            Articles.Add(article);
            }
        }

        [HttpPut]
        public void Put(Article article)
        {
            var a = Articles.FirstOrDefault(a => a.Id == article.Id);
            if (a != null)
            {
                a.Name = article.Name;
                a.Category = article.Category;
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var a = Articles.FirstOrDefault(a => a.Id == id);
            if (a != null)
            {
                Articles.Remove(a);
            }
        }
    }
}
