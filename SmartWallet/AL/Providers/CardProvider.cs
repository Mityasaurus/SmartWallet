using System.Collections.Generic;
using System.Linq;
using SmartWallet.DAL;
using SmartWallet.DAL.Entity;
using SmartWallet.DAL.Repository;

namespace SmartWallet.Providers;

public class CardProvider
{
    public static List<Card> GetAllCards()
    {
        SmartWalletContext context = new SmartWalletContext();
        Repository<Card> repository = new Repository<Card>(context);
        return repository.GetAll().ToList();
    }
    
    public static Card GetCardById(int id)
    {
        SmartWalletContext context = new SmartWalletContext();
        Repository<Card> repository = new Repository<Card>(context);
        return repository.Get(id);
    }

    public static double GetIncomeByMonth(int month, int cardId)
    {
        SmartWalletContext context = new SmartWalletContext();
        Repository<Card> repository = new Repository<Card>(context);

        var Transactions = TransactionProvider.GetAllTransactionByCardId(cardId);

        var monthTransactions = Transactions.Where(t => t.DateTime.Month == month);

        return monthTransactions.Where(t => t.RecipientCardId == cardId).Sum(t => t.Amount * t.Rate);
    }

    public static double GetOutcomeByMonth(int month, int cardId)
    {
        SmartWalletContext context = new SmartWalletContext();
        Repository<Card> repository = new Repository<Card>(context);

        var Transactions = TransactionProvider.GetAllTransactionByCardId(cardId);

        var monthTransactions = Transactions.Where(t => t.DateTime.Month == month);

        return monthTransactions.Where(t => t.SenderCardId == cardId).Sum(t => t.Amount);
    }
}