using System.Collections.Generic;
using System.Linq;
using SmartWallet.DAL;
using SmartWallet.DAL.Entity;
using SmartWallet.DAL.Repository;

namespace SmartWallet.Providers;

public class CardProvider
{
    private Repository<Card> _repository;

    public CardProvider(SmartWalletContext context)
    {
        _repository = new Repository<Card>(context);
    }
    
    public List<Card> GetAllCards()
    {
        return _repository.GetAll().ToList();
    }
    
    public Card GetCardById(int id)
    {
        return _repository.Get(id);
    }
}