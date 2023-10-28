using SmartWallet.DAL.Entity;
using SmartWallet.DAL.Repository;
using System.Collections.Generic;
using System.Linq;
using SmartWallet.DAL;

namespace SmartWallet.Providers
{
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
        
        // public static User GetUserByIDStatic(int id)
        // {
        //     SmartWalletContext context = new SmartWalletContext();
        //     Repository<User> userRepository = new Repository<User>(context);
        //     Repository<Card> cardRepository = new Repository<Card>(context);
        //     List<User> users = userRepository.GetAll().ToList();
        //     List<Card> cards = cardRepository.GetAll().ToList();
        //     return userRepository.Get(id);
        // }

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
            // SmartWalletContext context = new SmartWalletContext();
            // Repository<User> userRepository = new Repository<User>(context);
            // Repository<Card> cardRepository = new Repository<Card>(context);
            // List<User> users = userRepository.GetAll().ToList();
            // List<Card> cards = cardRepository.GetAll().ToList(); // Cannot load cards from users without this line
            return _users;
        }

        public void RemoveUser(User user)
        {
            _userRepository.Remove(user);
        }

        public void UpdateUser(User user, int id)
        {
            _userRepository.Update(user, id);
        }

        public string UserExists(User user)
        {
            var emails = _users.Select(u => u.Email);
            if(emails.Contains(user.Email))
            {
                return "This email is already registered";
            }

            var phones = _users.Select(u => u.Phone);
            if (phones.Contains(user.Phone))
            {
                return "This phone is already registered";
            }

            return "0";
        }

        public string CheckLogin(string email, string password)
        {
            var emails = _users.Select(u => u.Email);
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
