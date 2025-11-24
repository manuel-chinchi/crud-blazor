using crud_blazor.Server.Repositories.Contracts;
using crud_blazor.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crud_blazor.Server.Repositories
{
    public class ArticleRepository : IArticleRepository<Article>
    {
        public readonly JSONRepository<Article> _jsonRepository;

        public ArticleRepository()
        {
            _jsonRepository = new JSONRepository<Article>("articles.json");
        }

        public void AddArticle(Article obj)
        {
            _jsonRepository.Add(obj);
        }

        public void DeleteArticle(int id)
        {
            _jsonRepository.Delete(id);
        }

        public void GetArticle(int id)
        {
            _jsonRepository.Get(id);
        }

        public IEnumerable<Article> GetArticles()
        {
            var articles = _jsonRepository.GetItems();
            return articles;
        }

        public void UpdateArticle(Article obj)
        {
            _jsonRepository.Update(obj);
        }
    }
}
