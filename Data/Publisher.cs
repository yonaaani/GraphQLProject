using System.ComponentModel.DataAnnotations;

namespace GraphQLProject.Data
{
    public class Publisher
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [UseUpperCase]
        public string? Name { get; set; }

        public ICollection<Book> Books { get; set; } =
            new List<Book>();
    }
}
