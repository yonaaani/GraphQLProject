using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using GraphQLProject.Data;
using GraphQLProject.DataLoader;

namespace GraphQLProject.Types
{
    public class BookType : ObjectType<Book>
    {
        protected override void Configure(IObjectTypeDescriptor<Book> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) => ctx.DataLoader<BookByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

            descriptor
                .Field(t => t.BookPublishers)
                .ResolveWith<BookPublishers>(t => t.GetPublishersAsync(default!, default!, default!, default))
                .UseDbContext<ApplicationDbContext>()
                .Name("publishers");
        }

        private class BookPublishers
        {
            public async Task<IEnumerable<Publisher>> GetPublishersAsync(
                [Parent] Book book,
                [ScopedService] ApplicationDbContext dbContext,
                PublisherByIdDataLoader publisherById,
                CancellationToken cancellationToken)
            {
                int[] bookIds = await dbContext.Books
                    .Where(s => s.Id == book.Id)
                    .Include(s => s.BookPublishers)
                    .SelectMany(s => s.BookPublishers.Select(t => t.PublisherId))
                    .ToArrayAsync();

                return await publisherById.LoadAsync(bookIds, cancellationToken);
            }
        }
    }
}