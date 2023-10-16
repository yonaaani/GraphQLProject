using GraphQLProject.Data;
using HotChocolate.Types.Relay;

namespace GraphQLProject.Publishers
{
    public record RenamePublisherInput([ID(nameof(Publisher))] int Id, string Name);
}


