using System.Collections.Generic;
using GraphQLProject.Common;
using GraphQLProject.Data;

namespace GraphQLProject.Books
{
    public class BookPayloadBase : Payload
    {
        protected BookPayloadBase(Book book)
        {
            Book = book;
        }

        protected BookPayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public Book? Book { get; }
    }
}