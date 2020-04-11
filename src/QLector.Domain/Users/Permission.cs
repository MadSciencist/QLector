using QLector.Domain.Core;

namespace QLector.Domain.Users
{
    public class Permission : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public static Permission Create(string name, string description)
        {
            return new Permission
            {
                Name = name,
                Description = description
            };
        }
    }
}
