using System.Collections.Generic;
using GraphQLProject.Common;
using GraphQLProject.Data;

namespace GraphQLProject.Authors
{
    public class AuthorPayloadBase : Payload
    {
        protected AuthorPayloadBase(Author author)
        {
            Author = author;
        }

        protected AuthorPayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public Author? Author { get; }
    }
}