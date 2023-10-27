using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

    public string GenerateNewNumber()
    {
        using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        {
            string cardNumber;
            do
            {
                byte[] data = new byte[8];
                rng.GetBytes(data);

                long longValue = BitConverter.ToInt64(data, 0);
                long positiveValue = Math.Abs(longValue);
                cardNumber = positiveValue.ToString().Substring(0, 16);
            } while (DoesCardExist(cardNumber));
            
            return cardNumber;
        }
    }

    public bool DoesCardExist(string number)
    {
        return _cards.Count(c => c.Number == number) != 0;
    }
}