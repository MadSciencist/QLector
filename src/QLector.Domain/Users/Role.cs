using QLector.Domain.Core;
using System.Collections.Generic;

namespace QLector.Domain.Users
{
    public class Role : Entity<int>
    {
        public string Name { get; private set; }
        public string NormalizedName { get; private set; }

        private List<UserRoleLink> _userRoleLinks;
        public IReadOnlyCollection<UserRoleLink> UserRoleLinks => _userRoleLinks.AsReadOnly();

        private List<RolePermissionLink> _rolePermissionLinks;
        public IReadOnlyCollection<RolePermissionLink> RolePermissionLinks => _rolePermissionLinks.AsReadOnly();

        protected Role() { }

        public static Role Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Provide role name!");

            return new Role
            {
                Name = name,
                NormalizedName = name.ToUpperInvariant(),
                _userRoleLinks = new List<UserRoleLink>(),
                _rolePermissionLinks = new List<RolePermissionLink>()
            };
        }

        public void AddPermission(Permission permission)
        {
            // TODO
        }
    }
}
