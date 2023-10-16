using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GraphQLProject.Data
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(200)]
        public string? LastName { get; set; }

        [Required]
        [StringLength(200)]
        public string? UserName { get; set; }

        [StringLength(256)]
        public string? EmailAddress { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; } =
            new List<BookAuthor>();
    }
}