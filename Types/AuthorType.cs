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
    public class AuthorType : ObjectType<Author>
    {
        protected override void Configure(IObjectTypeDescriptor<Author> descriptor)
        {

            descriptor
               .ImplementsNode()
               .IdField(t => t.Id)
               .ResolveNode((ctx, id) => ctx.DataLoader<AuthorByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

            descriptor
                .Field(t => t.BookAuthors)
                .ResolveWith<AuthorResolvers>(t => t.GetAuthorsAsync(default!, default!, default!, default))
                .UseDbContext<ApplicationDbContext>()
                .Name("authors");

            descriptor
               .Field(t => t.Id)
               .ID(nameof(Author));
        }

        private class AuthorResolvers
        {
            public async Task<IEnumerable<Book>> GetAuthorsAsync(
                Author author,
                [ScopedService] ApplicationDbContext dbContext,
                BookByIdDataLoader bookById,
                CancellationToken cancellationToken)
            {
                int[] bookIds = await dbContext.Authors
                    .Where(s => s.Id == author.Id)
                    .Include(s => s.BookAuthors)
                    .SelectMany(s => s.BookAuthors.Select(t => t.BookId))
                    .ToArrayAsync();

                return await bookById.LoadAsync(bookIds, cancellationToken);
            }
        }
    }
}