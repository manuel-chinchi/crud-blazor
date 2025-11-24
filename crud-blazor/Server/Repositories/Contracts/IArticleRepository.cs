using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crud_blazor.Server.Repositories.Contracts
{
    public interface IArticleRepository<TEntity>
    {
        public IEnumerable<TEntity> GetArticles();
        public void AddArticle(TEntity obj);
        public void GetArticle(int id);
        public void UpdateArticle(TEntity obj);
        public void DeleteArticle(int id);
    }
}
