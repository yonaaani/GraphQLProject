using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using GraphQLProject.Data;
using GraphQLProject;
using GraphQLProject.DataLoader;

namespace GraphQLProject.Publishers
{
    [ExtendObjectType("Query")]
    public class PublisherQueries
    {
        [UseApplicationDbContext]
        [UsePaging]
        public IQueryable<Publisher> GetPublishers(
    [ScopedService] ApplicationDbContext context) =>
    context.Publishers.OrderBy(t => t.Name);


        [UseApplicationDbContext]
        public Task<Publisher> GetPublisherByNameAsync(
            string name,
            [ScopedService] ApplicationDbContext context,
            CancellationToken cancellationToken) =>
            context.Publishers.FirstAsync(t => t.Name == name);

        [UseApplicationDbContext]
        public async Task<IEnumerable<Publisher>> GetPublisherByNamesAsync(
            string[] names,
            [ScopedService] ApplicationDbContext context,
            CancellationToken cancellationToken) =>
            await context.Publishers.Where(t => names.Contains(t.Name)).ToListAsync();

        public Task<Publisher> GetPublisherByIdAsync(
            [ID(nameof(Publisher))] int id,
            PublisherByIdDataLoader publisherById,
            CancellationToken cancellationToken) =>
            publisherById.LoadAsync(id, cancellationToken);

        public async Task<IEnumerable<Publisher>> GetPublishersByIdAsync(
            [ID(nameof(Publisher))] int[] ids,
            PublisherByIdDataLoader publisherById,
            CancellationToken cancellationToken) =>
            await publisherById.LoadAsync(ids, cancellationToken);
    }
}