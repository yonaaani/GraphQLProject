using System.Threading;
using System.Threading.Tasks;
using GraphQLProject.Data;
using GraphQLProject;
using HotChocolate;
using HotChocolate.Types;

namespace GraphQLProject.Publishers
{
    [ExtendObjectType("Mutation")]
    public class PublisherMutations
    {
        [UseApplicationDbContext]
        public async Task<AddPublisherPayload> AddPublisherAsync(
            AddPublisherInput input,
            [ScopedService] ApplicationDbContext context,
            CancellationToken cancellationToken)
        {
            var publisher = new Publisher { Name = input.Name };
            context.Publishers.Add(publisher);

            await context.SaveChangesAsync(cancellationToken);

            return new AddPublisherPayload(publisher);
        }

        [UseApplicationDbContext]
        public async Task<RenamePublisherPayload> RenamePublisherAsync(
    RenamePublisherInput input,
    [ScopedService] ApplicationDbContext context,
    CancellationToken cancellationToken)
        {
            Publisher publisher = await context.Publishers.FindAsync(input.Id);
            publisher.Name = input.Name;

            await context.SaveChangesAsync(cancellationToken);

            return new RenamePublisherPayload(publisher);
        }
    }
}