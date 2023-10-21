using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using MaterialDesignThemes.Wpf;
using SmartWallet.DAL;
using SmartWallet.DAL.Entity;
using SmartWallet.DAL.Repository;
using SmartWallet.UI.Controls;
using Card = SmartWallet.DAL.Entity.Card;

namespace SmartWallet.Providers;

public class UserProvider
{
    private SmartWalletContext _context;
    
    private UserRepository<User> _userRepository;
    private UserRepository<Card> _cardRepository;
    
    private List<User> _users;
    private List<Card> _cards;
    
    public UserProvider()
    {
        _context = new SmartWalletContext();
        _userRepository = new UserRepository<User>(_context);
        _cardRepository = new UserRepository<Card>(_context);

        _users = _userRepository.GetAll().ToList();
        _cards = _cardRepository.GetAll().ToList();
    }

    public List<User> GetAllUsers()
    {
        return _users;
    }
    
    public List<Card> GetAllCards(int userId)
    {
        return _users.Find(u => u.Id == userId).Cards;
    }
}