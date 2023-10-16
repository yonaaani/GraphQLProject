using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GraphQLProject.DataLoader;
using HotChocolate;
using HotChocolate.Types;
using GraphQLProject.Data;

namespace GraphQLProject.Types
{
    public class PublisherType : ObjectType<Publisher>
    {
        protected override void Configure(IObjectTypeDescriptor<Publisher> descriptor)
        {
            descriptor
               .ImplementsNode()
               .IdField(t => t.Id)
               .ResolveNode((ctx, id) => ctx.DataLoader<PublisherByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

            descriptor
                .Field(t => t.Name)
                .UseUpperCase();

            descriptor
    .Field(t => t.Books)
    .ResolveWith<BookPublishers>(t => t.GetPublishersAsync(default!, default!, default!, default))
    .UseDbContext<ApplicationDbContext>()
    .UsePaging<NonNullType<BookType>>()
    .Name("books");
        }

        private class BookPublishers
        {
            public async Task<IEnumerable<Publisher>> GetPublishersAsync(
                Book book,
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