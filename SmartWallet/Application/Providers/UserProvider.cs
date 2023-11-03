using SmartWallet.DAL.Entity;
using SmartWallet.DAL.Repository;
using System.Collections.Generic;
using System.Linq;
using SmartWallet.DAL;

namespace SmartWallet.Providers
{
    public enum UserStatuses
    {
        EmailRegistered,
        PhoneRegistered,
        EmailIncorrect,
        PasswordIncorrect,
        EmptyFiled,
        EmailNotValid,
        PhoneNotValid,
        Success
    }
    
    public class UserProvider
    {
        private Repository<User> _userRepository;
        private Repository<Card> _cardRepository;
        private List<User> _users;
        private List<Card> _cards;

        public UserProvider(SmartWalletContext context)
        {
            _userRepository = new Repository<User>(context);
            _users = _userRepository.GetAll().ToList();
            _cardRepository = new Repository<Card>(context);
            _cards = _cardRepository.GetAll().ToList();
        }
        
        public UserProvider()
        {
            SmartWalletContext context = new SmartWalletContext();
            _userRepository = new Repository<User>(context);
            _users = _userRepository.GetAll().ToList();
            _cardRepository = new Repository<Card>(context);
            _cards = _cardRepository.GetAll().ToList();
        }
        
        public void AddUser(User user)
        {
            _userRepository.Add(user);
        }
        
        public void AddUsers(List<User> users)
        {
            users.ForEach(user => _userRepository.Add(user));
        }

        public User GetUserById(int id)
        {
            return _users.Find(u => u.Id == id);
        }
        
        public User GetUserByCredentials(string email, string password)
        {
            return _users.First(u => u.Email == email && u.Password == password);
        }

        public List<User> GetAllUsers()
        {
            return _users;
        }

        public void RemoveUser(User user)
        {
            _userRepository.Remove(user);
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }

        public UserStatuses UserExists(User user)
        {
            var emails = _users.Select(u => u.Email);
            if(emails.Contains(user.Email))
            {
                return UserStatuses.EmailRegistered;
            }

            var phones = _users.Select(u => u.Phone);
            if (phones.Contains(user.Phone))
            {
                return UserStatuses.PhoneRegistered;
            }

            return UserStatuses.Success;
        }

        public UserStatuses CheckLogin(string email, string password)
        {
            var emails = _users.Select(u => u.Email);
            if (!emails.Contains(email))
            {
                // return "This email is not registered";
                return UserStatuses.EmailIncorrect;
            }

            string userPassword = GetAllUsers().Where(u => u.Email == email).First().Password;

            if(userPassword == password)
            {
                return UserStatuses.Success;
            }
            // return "Incorrect password";
            return UserStatuses.PasswordIncorrect;
        }
    }
}
