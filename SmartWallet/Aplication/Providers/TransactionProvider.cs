using System;
using System.Collections.Generic;
using System.Linq;
using SmartWallet.DAL;
using SmartWallet.DAL.Entity;
using SmartWallet.DAL.Repository;

namespace SmartWallet.Providers;

public class TransactionProvider
{
    private Repository<Transaction> _transactionRepository;
    private Repository<Card> _cardRepository;
    private List<Transaction> _transactions;
    private List<Card> _cards;

    public TransactionProvider(SmartWalletContext context)
    {
        _transactionRepository = new Repository<Transaction>(context);
        _cardRepository = new Repository<Card>(context);
        _transactions = _transactionRepository.GetAll().ToList();
        _cards = _cardRepository.GetAll().ToList();
    }
    
    public List<Transaction> GetAllTransactions()
    {
        return _transactions;
    }

    public List<Transaction> GetAllTransactionSendByCardId(int id)
    {
        return _transactions.Where(t => t.SenderCardId == id).ToList();
    }
    
    public List<Transaction> GetAllTransactionReceivedByCardId(int id)
    {
        return _transactions.Where(t => t.RecipientCardId == id).ToList();
    }

    public Transaction GetTransactionById(int id)
    {
        return _transactions.Find(t => t.Id == id);
    }
    
    public List<Transaction> GetAllTransactionByCardId(int id)
    {
        return _transactions.Where(t => t.RecipientCardId == id || t.SenderCardId == id).ToList();
    }

    public List<Transaction> GetTransactionsAfterDate(DateTime dateTime, int id)
    {
        return GetAllTransactionByCardId(id).Where(t => t.DateTime > dateTime).ToList();
    }
    
    public List<Transaction> GetTransactionsBeforeDate(DateTime dateTime, int id)
    {
        return GetAllTransactionByCardId(id).Where(t => t.DateTime < dateTime).ToList();
    }
    
    public List<Transaction> GetTransactionsByDate(DateTime dateTime, int id)
    {
        return GetAllTransactionByCardId(id).Where(t => t.DateTime == dateTime).ToList();
    }
    
    public List<Transaction> GetTransactionsBetweenDate(DateTime startDate, DateTime endDate, int id)
    {
        return GetAllTransactionByCardId(id).Where(t => t.DateTime <= endDate && t.DateTime >= startDate).ToList();
    }
    
    public double GetIncomeByMonth(int month, int cardId)
    {
        return GetAllTransactionReceivedByCardId(cardId).Where(t => t.DateTime.Month == month).Sum(t => t.Amount * t.Rate);
    }

    public double GetOutcomeByMonth(int month, int cardId)
    {
        return GetAllTransactionSendByCardId(cardId).Where(t => t.DateTime.Month == month).Sum(t => t.Amount);
    }
    
    public void AddNewTransaction(string senderNumber, string recipientNumber, double amount) // TODO remove static
    {
        Card senderCard = _cards.Find(card => card.Number == senderNumber);
        Card recipientCard = _cards.Find(card => card.Number == recipientNumber);

        double rate = MoneyProvider.GetRate(senderCard.Currency, recipientCard.Currency);
        
        AddNewTransaction(senderCard.UserId, senderCard.Id, recipientCard.UserId, recipientCard.Id, amount, rate);
    }
    
    private void AddNewTransaction(int senderId, int senderCardId, int recipientId, int recipientCardId, double amount, double rate)
    {
        SmartWalletContext context = new SmartWalletContext();
        Repository<Transaction> repository = new Repository<Transaction>(context);
        repository.Add(new Transaction()
        {
            SenderId = senderId,
            SenderCardId = senderCardId,
            RecipientId = recipientId,
            RecipientCardId = recipientCardId,
            Amount = amount,
            Rate = rate,
            DateTime = DateTime.Now
        });
    }
}