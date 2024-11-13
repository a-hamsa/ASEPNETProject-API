using ASEPNETProject.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ASEPNETProject.Data.Repositories;
public class ArticleRepository : IArticleRepository
{
    private readonly ArticleContext _ctx;
    public ArticleRepository(ArticleContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<IEnumerable<Article>> GetArticlesAsync()
    {
        var articles = await _ctx.Articles.Include(a => a.Author).ToListAsync();
        return articles;
    }

    public async Task<Article> GetArticleByIdAsync(int id)
    {
        return await _ctx.Articles.Include(a => a.Author).FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Article> CreateArticleAsync(Article article)
    {
        _ctx.Articles.Add(article);
        await _ctx.SaveChangesAsync();
        return article;
    }

    public async Task UpdateArticleAsync(Article article)
    {
        _ctx.Articles.Update(article);
        await _ctx.SaveChangesAsync();
    }

    public async Task DeleteArticleAsync(Article article)
    {
        _ctx.Articles.Remove(article);
        await _ctx.SaveChangesAsync();
    }
}
