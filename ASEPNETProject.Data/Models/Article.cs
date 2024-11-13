
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASEPNETProject.Data.Models
{
    [Table("Article")]
    public class Article
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Body { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public int AuthorId { get; set; }
        public required Person Author { get; set; }
    }
}
