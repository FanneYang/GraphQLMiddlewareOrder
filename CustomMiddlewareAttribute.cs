using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using System.Reflection;

namespace FacebookPost
{
    public class CustomMiddlewareAttribute : ObjectFieldDescriptorAttribute
    {
        public string Domain { get; set; }

        public override void OnConfigure(
            IDescriptorContext context,
            IObjectFieldDescriptor descriptor,
            MemberInfo memberInfo)
        {
            switch (Domain)
            {
                case "Post":
                    {
                        descriptor.Use<CustomMiddleware<Post>>();
                        return;
                    }

                case "User":
                    {
                        descriptor.Use<CustomMiddleware<User>>();
                        return;
                    }

                case "Interaction":
                    {
                        descriptor.Use<CustomMiddleware<Interaction>>();
                        return;
                    }

                default:
                    return;
            }

        }
    }
}
