using HotChocolate.Data.Filters;
using GraphQLProject.Data;

namespace GraphQLProject.Authors
{
    public class AuthorFilterInputType : FilterInputType<Author>
    {
        protected override void Configure(IFilterInputTypeDescriptor<Author> descriptor)
        {
            descriptor.Ignore(t => t.Id);
        }
    }
}