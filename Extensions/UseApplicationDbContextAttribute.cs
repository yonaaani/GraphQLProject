using System.Reflection;
using GraphQLProject.Data;
using GraphQLProject;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;

namespace GraphQLProject
{
    public class UseApplicationDbContextAttribute : ObjectFieldDescriptorAttribute
    {
        protected override void OnConfigure(
            IDescriptorContext context,
            IObjectFieldDescriptor descriptor,
            MemberInfo member)
        {
            descriptor.UseDbContext<ApplicationDbContext>();
        }
    }
}