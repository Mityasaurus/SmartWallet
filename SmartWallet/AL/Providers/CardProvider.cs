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
}