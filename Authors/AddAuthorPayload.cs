using GraphQLProject.Common;
using GraphQLProject.Data;

namespace GraphQLProject.Authors
{
    public class AddAuthorPayload : AuthorPayloadBase
    {
        public AddAuthorPayload(UserError error)
            : base(new[] { error })
        {
        }

        public AddAuthorPayload(Author authors) : base(authors)
        {
        }

        public AddAuthorPayload(IReadOnlyList<UserError> errors) : base(errors)
        {
        }
    }
}