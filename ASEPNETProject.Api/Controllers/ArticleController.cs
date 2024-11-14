using ASEPNETProject.Data.Models;
using ASEPNETProject.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ASEPNETProject.Api.Controllers
{
    [Route("api/article")]
    [ApiController]
    public class ArticleController : Controller
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ILogger<ArticleController> _logger;

        public ArticleController(IArticleRepository articleRepository, ILogger<ArticleController> logger)
        {
            _articleRepository = articleRepository;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddArticle(Article article)
        {
            try
            {
                var createdArticle = await _articleRepository.CreateArticleAsync(article);
                createdArticle.Date = DateTime.Now;
                return CreatedAtAction(nameof(AddArticle), createdArticle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    Message = ex.Message
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateArticle(Article articleUpdate)
        {
            try
            {
                var existingArticle = await _articleRepository.GetArticleByIdAsync(articleUpdate.Id);
                if (existingArticle == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        message = "Record not found"
                    });
                }
                existingArticle.AuthorId = articleUpdate.AuthorId;
                existingArticle.Title = articleUpdate.Title;
                existingArticle.Body = articleUpdate.Body;
                existingArticle.Author = articleUpdate.Author;
                existingArticle.Date = DateTime.Now;
                await _articleRepository.UpdateArticleAsync(existingArticle);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    message = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            try
            {
                var existingArticle = await _articleRepository.GetArticleByIdAsync(id);
                if (existingArticle == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        message = "Record Not Found"
                    });
                }
                await _articleRepository.DeleteArticleAsync(existingArticle);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    message = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticle(int id)
        {
            try
            {
                var article = await _articleRepository.GetArticleByIdAsync(id);
                if (article == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        message = "Record Not Found"
                    });
                }
                return Ok(article);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    message = ex.Message
                });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetArticle()
        {
            try
            {
                var article = await _articleRepository.GetArticlesAsync();
                return Ok(article);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    message = ex.Message
                });
            }
        }
    }
}
