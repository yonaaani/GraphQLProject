using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using GraphQLProject.Data;
using GraphQLProject;
using GraphQLProject.DataLoader;
using GraphQLProject.Types;

namespace GraphQLProject.Authors
{
    [ExtendObjectType("Query")]
    public class AuthorQueries
    {
        [UseApplicationDbContext]
        [UsePaging(typeof(NonNullType<AuthorType>))]
        [UseFiltering(typeof(AuthorFilterInputType))]
        [UseSorting]
        public IQueryable<Author> GetAuthors(
    [ScopedService] ApplicationDbContext context) =>
    context.Authors;

        public Task<Author> GetAuthorByIdAsync(
            [ID(nameof(Author))] int id,
            AuthorByIdDataLoader authorById,
            CancellationToken cancellationToken) =>
            authorById.LoadAsync(id, cancellationToken);

        public async Task<IEnumerable<Author>> GetAuthorsByIdAsync(
            [ID(nameof(Author))] int[] ids,
            AuthorByIdDataLoader authorById,
            CancellationToken cancellationToken) =>
            await authorById.LoadAsync(ids, cancellationToken);
    }
}