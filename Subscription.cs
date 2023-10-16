using GraphQLProject.Data;
using HotChocolate;
using HotChocolate.Types; 

namespace GraphQLProject
{
    public class Subscription
    {

        [Subscribe]
        public Author OnAuthorChanged([EventMessage] Author author)
            => author;
    }
}
