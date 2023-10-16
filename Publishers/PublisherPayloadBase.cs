using System.Collections.Generic;
using GraphQLProject.Data;
using GraphQLProject.Common;

namespace GraphQLProject.Publishers
{
    public class PublisherPayloadBase : Payload
    {
        public PublisherPayloadBase(Publisher publisher)
        {
            Publisher = publisher;
        }

        public PublisherPayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public Publisher? Publisher { get; }
    }
}