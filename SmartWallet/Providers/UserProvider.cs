using SmartWallet.DAL.Entity;
using SmartWallet.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWallet.Providers
{
    public class UserProvider
    {
        private readonly IRepository<User> _repository;

        public UserProvider(IRepository<User> repository)
        {
            _repository = repository;
        }

        public void AddUser(User user)
        {
            _repository.Add(user);
        }
        public void AddUsers(List<User> users)
        {
            users.ForEach(user => _repository.Add(user));
        }
        public User GetUserByID(int id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _repository.GetAll();
        }

        public void RemoveUser(User user)
        {
            _repository.Remove(user);
        }

        public void UpdateUser(User user)
        {
            _repository.Update(user);
        }

        public string UserExists(User user)
        {
            var emails = _repository.GetAll().Select(u => u.Email);
            if(emails.Contains(user.Email))
            {
                return "This email is already registered";
            }

            var phones = _repository.GetAll().Select(u => u.Phone);
            if (phones.Contains(user.Phone))
            {
                return "This phone is already registered";
            }

            return "0";
        }

        public string CheckLogin(string email, string password)
        {
            var emails = _repository.GetAll().Select(u => u.Email);
            if (!emails.Contains(email))
            {
                return "This email is not registered";
            }

            string userPassword = GetAllUsers().Where(u => u.Email == email).First().Password;

            if(userPassword == password)
            {
                return "";
            }
            else
            {
                return "Incorrect password";
            }
        }
    }
}
