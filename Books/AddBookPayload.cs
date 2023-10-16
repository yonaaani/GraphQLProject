using System.Collections.Generic;
using GraphQLProject.Data;
using GraphQLProject.Common;

namespace GraphQLProject.Books
{
    public class AddBookPayload : BookPayloadBase
    {
        public AddBookPayload(Book book) : base(book) { }

        public AddBookPayload(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }
    }
}
