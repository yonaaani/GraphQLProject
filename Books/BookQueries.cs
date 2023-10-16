using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GraphQLProject.Data;
using GraphQLProject.DataLoader;
using HotChocolate;


namespace GraphQLProject.Books
{
    [ExtendObjectType("Query")]
    public class BookQueries
    {
        [UseApplicationDbContext]
        [UsePaging]
        public IQueryable<Book> GetBooks(
     [ScopedService] ApplicationDbContext context) =>
     context.Books.OrderBy(t => t.Title);

        public Task<Book> GetBookByIdAsync(
     [ID(nameof(Book))] int id,
    BookByIdDataLoader dataLoader,
    CancellationToken cancellationToken) =>
    dataLoader.LoadAsync(id, cancellationToken);

        public async Task<IEnumerable<Book>> GetBooksByIdAsync(
           [ID(nameof(Book))] int[] ids,
           BookByIdDataLoader dataLoader,
           CancellationToken cancellationToken) =>
           await dataLoader.LoadAsync(ids, cancellationToken);
    }
}

    