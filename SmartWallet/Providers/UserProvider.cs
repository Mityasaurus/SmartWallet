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
    }
}
