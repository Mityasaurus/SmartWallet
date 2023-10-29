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

    public static string GenerateCVV()
    {
        Random rand = new Random();

        string cvv;

        do
        {
            cvv = "";
            for (int i = 0; i < 3; i++)
            {
                cvv += rand.Next(0, 10);
            }
        } while (cvv[0] == cvv[1] && cvv[1] == cvv[2]);

        return cvv;
    }

    public static DateTime GenerateDateExpire()
    {
        return DateTime.Now.AddMonths(50);
    }

    public bool DoesCardExist(string number)
    {
        return _cards.Count(c => c.Number == number) != 0;
    }

    public void AddNewCard(string cardNumber, DateTime dateExpire, string cvv, string type, Currency currency, double balance, int userId)
    {
        Card newCard = new Card()
        {
            Number = cardNumber,
            DateExpire = dateExpire,
            Cvv = cvv,
            Type = type,
            Currency = currency,
            Balance = balance,
            UserId = userId
        };

        _cardRepository.Add(newCard);
    }
}