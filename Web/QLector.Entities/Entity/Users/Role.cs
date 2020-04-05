using System.Collections.Generic;

namespace QLector.Entities.Entity.Users
{
    public class Role : Entity<int>
    {
        public string Name { get; private set; }
        public string NormalizedName { get; private set; }

        private List<UserRoleLink> _userRoleLinks;
        public IReadOnlyCollection<UserRoleLink> UserRoleLinks => _userRoleLinks;

        public Role() { }

        public static Role Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Provide role name!");

            return new Role
            {
                Name = name,
                NormalizedName = name.ToUpperInvariant(),
                _userRoleLinks = new List<UserRoleLink>()
            };
        }
    }
}
