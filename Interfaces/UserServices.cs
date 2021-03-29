using deploy_test.Data;
using deploy_test.Models;
using deploy_test.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace deploy_test.Interfaces
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Create(User user, string password);
        void Update(User user, string currentPassword, string password, string confirmPassword);
        void Delete(int id);
    }

    public class UserService : IUserService
    {
        private Context _context;
        public UserService(Context context)
        {
            _context = context;
        }

        static string GenerateRandomKey(int keyLength)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            byte[] randomBytes = new byte[keyLength];
            provider.GetBytes(randomBytes);
            string hashString = "";
            foreach (var hashbyte in randomBytes)
            {
                hashString += hashbyte.ToString("x2");
            }
            return hashString;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var user = _context.users.FirstOrDefault(x => x.username.Equals(username)) ?? null;

            if (user == null)
            {
                return null;
            }

            if (!computeHash(password).Equals(user.passwordhash))
            {
                return null;
            }
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.users;
        }

        public User GetById(int id)
        {
            return _context.users.Find(id);
        }

        public User Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ExceptionRaiser("Missing password");
            }

            if (_context.users.Any(x => x.username.Equals(user.username)))
            {
                throw new ExceptionRaiser("Username already taken: " + user.username);
            }

            user.passwordhash = computeHash(password);

            _context.users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Update(User userSettings, string currentPassword, string password, string confirmPassword)
        {
            var user = _context.users.Find(userSettings.id);

            if (user == null)
            {
                throw new ExceptionRaiser("User not found");
            }

            //Check if the name changed
            if (!string.IsNullOrWhiteSpace(userSettings.username) && (!userSettings.username.Equals(user.username)))
            {
                //Check if the username is already taken
                if (_context.users.Any(x => x.username.Equals(userSettings.username)))
                {
                    throw new ExceptionRaiser("Username already taken: " + user.username);
                }
                else
                {
                    user.username = userSettings.username;
                }
            }

            //Check if the firstname changed
            if (!string.IsNullOrWhiteSpace(userSettings.firstname) && (!userSettings.firstname.Equals(user.firstname)))
            {
                user.firstname = userSettings.firstname;
            }

            //Check if the lastname changed
            if (!string.IsNullOrWhiteSpace(userSettings.lastname) && (!userSettings.lastname.Equals(user.lastname)))
            {
                user.lastname = userSettings.lastname;
            }

            if (!string.IsNullOrWhiteSpace(currentPassword))
            {
                if (!computeHash(currentPassword).Equals(user.passwordhash))
                {
                    throw new ExceptionRaiser("Invalid password !");
                }

                if (currentPassword.Equals(password))
                {
                    throw new ExceptionRaiser("You need to change your password");
                }

                if (!password.Equals(confirmPassword))
                {
                    throw new ExceptionRaiser("Password doesn't match !");
                }
                user.passwordhash = computeHash(password);
            }
            _context.users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.users.Find(id);
            if (user != null)
            {
                _context.users.Remove(user);
                _context.SaveChanges();
            }
        }

        private static string computeHash(string password)
        {
            MD5 digest = new MD5CryptoServiceProvider();
            var input = digest.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hashedString = "";
            foreach (var hashbyte in input)
            {
                hashedString += hashbyte.ToString("x2");
            }
            return hashedString;
        }
    }
}
