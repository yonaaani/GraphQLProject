using System.Collections.Generic;
using GraphQLProject.Data;
using GraphQLProject.Common;

namespace GraphQLProject.Publishers
{
    public class AddPublisherPayload : PublisherPayloadBase
    {
        public AddPublisherPayload(Publisher publisher)
            : base(publisher)
        {
        }

        public AddPublisherPayload(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }
    }
}