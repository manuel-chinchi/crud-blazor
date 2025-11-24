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
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepository<Article> _articleRepository;
        private readonly ICategoryRepository<Category> _categoryRepository;

        public ArticleController()
        {
            _articleRepository = new ArticleRepository();
            _categoryRepository = new CategoryRepository();
        }

        [HttpGet]
        public IEnumerable<Article> Get()
        {
            return _articleRepository.GetArticles();
        }

        [HttpGet("{id}")]
        public Article Get(int id)
        {
            return _articleRepository.GetArticles().FirstOrDefault(a => a.Id == id);
        }

        [HttpPost]
        public void Post(Article article)
        {
            var articles = _articleRepository.GetArticles();

            if (articles.Count() == 0)
            {
                article.Id = 1;
                article.CreatedDate = DateTime.Now;
            }
            else
            {
                article.Id = (articles.Max(a => a.Id) + 1);
                article.CreatedDate = DateTime.Now;
            }

            _articleRepository.AddArticle(article);
        }

        [HttpPut]
        public void Put(Article editedArticle)
        {
            var article = _articleRepository.GetArticles().FirstOrDefault(a => a.Id == editedArticle.Id);

            if (article != null)
            {
                article.Name = editedArticle.Name;
                var category = _categoryRepository.GetCategory(Convert.ToInt32(editedArticle.CategoryId));
                article.Category = category;
                
                _articleRepository.UpdateArticle(article);
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var article = _articleRepository.GetArticles().FirstOrDefault(a => a.Id == id);

            if (article != null)
            {
                _articleRepository.DeleteArticle(article.Id);
            }
        }
    }
}
