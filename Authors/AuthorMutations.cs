using System.Threading;
using System.Threading.Tasks;
using GraphQLProject.Common;
using GraphQLProject.Data;
using GraphQLProject;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Subscriptions;

namespace GraphQLProject.Authors
{
    [ExtendObjectType("Mutation")]
    public class AuthorMutations
    {
        [UseApplicationDbContext]
        public async Task<AddAuthorPayload> AddAuthorAsync(
            AddAuthorInput input,
            [ScopedService] ApplicationDbContext context,
            CancellationToken cancellationToken, ITopicEventSender sender)
        {
            if (string.IsNullOrEmpty(input.FirstName))
            {
                return new AddAuthorPayload(
                    new UserError("The FirstName cannot be empty.", "FIRSTNAME_EMPTY"));
            }

            if (string.IsNullOrEmpty(input.UserName))
            {
                return new AddAuthorPayload(
                    new UserError("The UserName cannot be empty.", "USERNAME_EMPTY"));
            }

            if (string.IsNullOrEmpty(input.EmailAddress))
            {
                return new AddAuthorPayload(
                    new UserError("The EmailAddress cannot be empty.", "EMAILADDRESS_EMPTY"));
            }

            if (input.AuthorIds.Count == 0)
            {
                return new AddAuthorPayload(
                    new UserError("No books yet.", "NO_BOOK"));
            }

            var author = new Author
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                UserName = input.UserName,
                EmailAddress = input.EmailAddress
            };

            foreach (int authorId in input.AuthorIds)
            {
                author.BookAuthors.Add(new BookAuthor
                {
                    AuthorId = authorId
                });
            }

            context.Authors.Add(author);
            await context.SaveChangesAsync(cancellationToken);

            await sender.SendAsync(nameof(Subscription.OnAuthorChanged), author);

            return new AddAuthorPayload(author);
        }
    }
}