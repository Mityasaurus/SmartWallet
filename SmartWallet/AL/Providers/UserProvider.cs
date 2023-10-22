using SmartWallet.DAL.Entity;
using SmartWallet.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartWallet.DAL;

namespace SmartWallet.Providers
{
    public class UserProvider
    {
        public static void AddUser(User user)
        {
            SmartWalletContext context = new SmartWalletContext();
            Repository<User> repository = new Repository<User>(context);
            repository.Add(user);
        }
        
        public static void AddUsers(List<User> users)
        {
            SmartWalletContext context = new SmartWalletContext();
            Repository<User> repository = new Repository<User>(context);
            users.ForEach(user => repository.Add(user));
        }
        
        public static User GetUserByID(int id)
        {
            SmartWalletContext context = new SmartWalletContext();
            Repository<User> userRepository = new Repository<User>(context);
            Repository<Card> cardRepository = new Repository<Card>(context);
            List<User> users = userRepository.GetAll().ToList();
            List<Card> cards = cardRepository.GetAll().ToList();
            return userRepository.Get(id);
        }

        public static User GetUserByCredentials(string email, string password)
        {
            SmartWalletContext context = new SmartWalletContext();
            Repository<User> repository = new Repository<User>(context);
            return repository.GetAll().First(u => u.Email == email && u.Password == password);
        }

        public static List<User> GetAllUsers()
        {
            SmartWalletContext context = new SmartWalletContext();
            Repository<User> userRepository = new Repository<User>(context);
            Repository<Card> cardRepository = new Repository<Card>(context);
            List<User> users = userRepository.GetAll().ToList();
            List<Card> cards = cardRepository.GetAll().ToList(); // Cannot load cards from users without this line
            return users;
        }

        public static void RemoveUser(User user)
        {
            SmartWalletContext context = new SmartWalletContext();
            Repository<User> repository = new Repository<User>(context);
            repository.Remove(user);
        }

        public static void UpdateUser(User user)
        {
            SmartWalletContext context = new SmartWalletContext();
            Repository<User> repository = new Repository<User>(context);
            repository.Update(user);
        }

        public static string UserExists(User user)
        {
            SmartWalletContext context = new SmartWalletContext();
            Repository<User> repository = new Repository<User>(context);
            
            var emails = repository.GetAll().Select(u => u.Email);
            if(emails.Contains(user.Email))
            {
                return "This email is already registered";
            }

            var phones = repository.GetAll().Select(u => u.Phone);
            if (phones.Contains(user.Phone))
            {
                return "This phone is already registered";
            }

            return "0";
        }

        public static string CheckLogin(string email, string password)
        {
            SmartWalletContext context = new SmartWalletContext();
            Repository<User> repository = new Repository<User>(context);
            
            var emails = repository.GetAll().Select(u => u.Email);
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
