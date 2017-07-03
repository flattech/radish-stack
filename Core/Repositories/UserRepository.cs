
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Core.Repositories.Enums;

namespace Core.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository()
        {
            base.Init("User", "Name,Username,Email,Role,Photo,PasswordHash,PasswordSalt");
        }

        public User GetByUsername(string value)
        {
            return Get("Username", value);
        }
    }

    public  class User:BaseModel
    {
        public User()
        {
        }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Photo { get; set; }

        private string Hash(string password)
        {
            SHA1Managed managed = new SHA1Managed();
            var buffer = System.Text.Encoding.Unicode.GetBytes(password);
            var result = managed.ComputeHash(buffer, 0, buffer.Length);
            return Convert.ToBase64String(result);
        }

        public bool ValidatePassword(string testPassword)
        {
            if (string.IsNullOrEmpty(testPassword)) return false;
            return Hash(testPassword).Equals(this.PasswordHash);
        }


        public bool Equals(User other)
        {
            return this.Id.Equals(other.Id);
        }

        public void SetPassword(string password)
        {
            throw new NotImplementedException();
        }

        public UserRoleEnum RoleEnum => (UserRoleEnum) Role;
    }

}
