using System;
using System.Collections.Generic;
using System.Linq;
using SmartWallet.DAL;
using SmartWallet.DAL.Entity;
using SmartWallet.DAL.Repository;

namespace SmartWallet.Providers;

public class TransactionProvider
{
    public static List<Transaction> GetAllTransactions()
    {
        SmartWalletContext context = new SmartWalletContext();
        Repository<Transaction> repository = new Repository<Transaction>(context);
        return repository.GetAll().ToList();
    }

    public static List<Transaction> GetAllTransactionSendByCardId(int id)
    {
        SmartWalletContext context = new SmartWalletContext();
        Repository<Transaction> repository = new Repository<Transaction>(context);
        return repository.GetAll().Where(t => t.SenderCardId == id).ToList();
    }
    
    public static List<Transaction> GetAllTransactionReceivedByCardId(int id)
    {
        SmartWalletContext context = new SmartWalletContext();
        Repository<Transaction> repository = new Repository<Transaction>(context);
        return repository.GetAll().Where(t => t.RecipientCardId == id).ToList();
    }

    public static Transaction GetTransactionById(int id)
    {
        SmartWalletContext context = new SmartWalletContext();
        Repository<Transaction> repository = new Repository<Transaction>(context);
        return repository.Get(id);
    }
    
    public static List<Transaction> GetAllTransactionByCardId(int id)
    {
        SmartWalletContext context = new SmartWalletContext();
        Repository<Transaction> repository = new Repository<Transaction>(context);
        return repository.GetAll().Where(t => t.RecipientCardId == id || t.SenderCardId == id).ToList();
    }

    public static List<Transaction> GetTransactionsAfterDate(DateTime dateTime)
    {
        SmartWalletContext context = new SmartWalletContext();
        Repository<Transaction> repository = new Repository<Transaction>(context);
        return repository.GetAll().Where(t => t.DateTime > dateTime).ToList();
    }
    
    public static List<Transaction> GetTransactionsBeforeDate(DateTime dateTime)
    {
        SmartWalletContext context = new SmartWalletContext();
        Repository<Transaction> repository = new Repository<Transaction>(context);
        return repository.GetAll().Where(t => t.DateTime < dateTime).ToList();
    }
    
    public static List<Transaction> GetTransactionsByDate(DateTime dateTime)
    {
        SmartWalletContext context = new SmartWalletContext();
        Repository<Transaction> repository = new Repository<Transaction>(context);
        return repository.GetAll().Where(t => t.DateTime == dateTime).ToList();
    }
    
    public static void AddNewTransaction(string senderNumber, string recipientNumber, double amount)
    {
        List<Card> cards = CardProvider.GetAllCards();
        Card senderCard = cards.Find(card => card.Number == senderNumber);
        Card recipientCard = cards.Find(card => card.Number == recipientNumber);

        double rate = MoneyProvider.GetRate(senderCard.Currency, recipientCard.Currency);
        
        AddNewTransaction(senderCard.UserId, senderCard.Id, recipientCard.UserId, recipientCard.Id, amount, rate);
    }
    
    private static void AddNewTransaction(int senderId, int senderCardId, int recipientId, int recipientCardId, double amount, double rate)
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