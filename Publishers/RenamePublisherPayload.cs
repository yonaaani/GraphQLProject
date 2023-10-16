using System.Collections.Generic;
using GraphQLProject.Common;
using GraphQLProject.Data;

namespace GraphQLProject.Publishers
{
    public class RenamePublisherPayload : PublisherPayloadBase
    {
        public RenamePublisherPayload(Publisher publisher)
            : base(publisher)
        {
        }

        public RenamePublisherPayload(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }
    }
}