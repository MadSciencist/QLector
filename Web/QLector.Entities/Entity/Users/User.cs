using System;
using System.Collections.Generic;

namespace QLector.Entities.Entity.Users
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

            _userRoleLinks.Add(new UserRoleLink { Role = role });
        }

        public void SignIn()
        {
            LastLogged = DateTime.UtcNow;
        }
    }
}
