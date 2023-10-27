using System.Collections.Generic;
using System.Linq;
using SmartWallet.DAL;
using SmartWallet.DAL.Entity;
using SmartWallet.DAL.Repository;

namespace SmartWallet.Providers;

public class CardProvider
{
    private Repository<Card> _cardRepository;
    private List<Card> _cards;

    public CardProvider(SmartWalletContext context)
    {
        _cardRepository = new Repository<Card>(context);
        _cards = _cardRepository.GetAll().ToList();
    }
    
    public List<Card> GetAllCards()
    {
        return _cards;
    }
    
    public Card GetCardById(int id)
    {
        return _cardRepository.Get(id);
    }
}