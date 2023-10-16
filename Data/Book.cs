using System.ComponentModel.DataAnnotations;

namespace GraphQLProject.Data
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string? Title { get; set; }

        [StringLength(4000)]
        public string? Description { get; set; }

        public ICollection<BookPublisher> BookPublishers { get; set; } =
               new List<BookPublisher>();
    }
}