using System.Collections.Generic;
using GraphQLProject.Data;
using HotChocolate.Types.Relay;

namespace GraphQLProject.Authors
{
    public record AddAuthorInput(
        string FirstName,
        string? LastName,
        string UserName,
        string EmailAddress,
        [ID(nameof(Author))]
        IReadOnlyList<int> AuthorIds);
}