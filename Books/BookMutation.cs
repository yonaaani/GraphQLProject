using System.Threading.Tasks;
using GraphQLProject.Data;
using HotChocolate;

namespace GraphQLProject.Books
{
    [ExtendObjectType("Mutation")]
    public class BookMutation
    {
        [UseApplicationDbContext]
        public async Task<AddBookPayload> AddBookAsync(
            AddBookInput input,
            [ScopedService] ApplicationDbContext context)
        {
            var book = new Book
            {
                Title = input.Title,
                Description = input.Description
            };

            context.Books.Add(book);
            await context.SaveChangesAsync();

            return new AddBookPayload(book);
        }
    }
}