using ASEPNETProject.Data.Models;

namespace ASEPNETProject.Data.Repositories
{
    public interface IArticleRepository
    {
        Task<Article> CreateArticleAsync(Article article);
        Task DeleteArticleAsync(Article article);
        Task<Article> GetArticleByIdAsync(int id);
        Task<IEnumerable<Article>> GetArticlesAsync();
        Task UpdateArticleAsync(Article article);
    }
}