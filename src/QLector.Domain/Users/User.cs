using QLector.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLector.Domain.Users
{
    public class User : IEntity<int>
    {
        public int Id { get; private set; }

        public string UserName { get; private set; }
        public string NormalizedUserName { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }

        public DateTime Created { get; private set; }
        public DateTime? Modified { get; private set; }
        public DateTime? LastLogged { get; private set; }

        private List<Document> _documents;
        public IReadOnlyCollection<Document> Documents => _documents;

        private List<UserRoleLink> _userRoleLinks;
        public IReadOnlyCollection<UserRoleLink> UserRoleLinks => _userRoleLinks;

        public User() { }

        public static User Create(string username, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new DomainException("Provide username!");

            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("Provide email");

            if (string.IsNullOrWhiteSpace(password))
                throw new DomainException("Provide password");

            return new User
            {
                UserName = username,
                NormalizedUserName = username.ToUpperInvariant(),
                Email = email,
                Password = password,
                Created = DateTime.UtcNow,
                _documents = new List<Document>(),
                _userRoleLinks = new List<UserRoleLink>()
            };
        }

        public void AddToRole(Role role)
        {
            if (role is null)
                throw new DomainException("Role doesnt exists!");

            if (_userRoleLinks is null)
                _userRoleLinks = new List<UserRoleLink>();

            if (_userRoleLinks.Any(x => x.RoleId == role.Id))
                throw new DomainException($"User already has role {role.Name}");

            _userRoleLinks.Add(new UserRoleLink { Role = role });
        }

        public bool RemoveRole(Role role)
        {
            if (role is null)
                throw new DomainException("Role doesnt exists!");

            var toRemove = _userRoleLinks.FirstOrDefault(x => x.RoleId == role.Id);

            if (toRemove is null)
                throw new DomainException($"User doesn't have role {role.Name}");

            return _userRoleLinks.Remove(toRemove);
        }

        public void SignIn()
        {
            LastLogged = DateTime.UtcNow;
        }
    }
}
