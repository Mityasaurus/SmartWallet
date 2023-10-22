using System;
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

public class DBProvider
{
    private SmartWalletContext _context;
    
    private Repository<User> _repository;
    private Repository<Card> _cardRepository;
    private Repository<Transaction> _transactionRepository;
    
    private List<User> _users;
    private List<Card> _cards;
    private List<Transaction> _transactions;
    
    public DBProvider()
    {
        _context = new SmartWalletContext();
        _repository = new Repository<User>(_context);
        _cardRepository = new Repository<Card>(_context);
        _transactionRepository = new Repository<Transaction>(_context);

        _users = _repository.GetAll().ToList();
        _cards = _cardRepository.GetAll().ToList();
        _transactions = _transactionRepository.GetAll().ToList();
    }

    public List<User> GetAllUsers()
    {
        return _users;
    }
    
    public List<Card> GetAllCards(int userId)
    {
        return _users.Find(u => u.Id == userId).Cards;
    }

    public void AddNewTransaction(string senderNumber, string recipientNumber, double amount)
    {
        Card senderCard = _cards.Find(card => card.Number == senderNumber);
        Card recipientCard = _cards.Find(card => card.Number == recipientNumber);

        double rate = MoneyProvider.GetRate(senderCard.Currency, recipientCard.Currency);
        
        AddNewTransaction(senderCard.UserId, senderCard.Id, recipientCard.UserId, recipientCard.Id, amount, rate);
    }
    
    private void AddNewTransaction(int senderId, int senderCardId, int recipientId, int recipientCardId, double amount, double rate)
    {
        _transactionRepository.Add(new Transaction()
        {
            SenderId = senderId,
            SenderCardId = senderCardId,
            RecipientId = recipientId,
            RecipientCardId = recipientCardId,
            Amount = amount,
            Rate = rate
        });
    }
}