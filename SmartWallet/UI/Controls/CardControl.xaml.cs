using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SmartWallet.DAL.Entity;
using SmartWallet.Providers;

namespace SmartWallet.UI.Controls;

public partial class CardControl : UserControl
{
    public static readonly DependencyProperty CardProperty = DependencyProperty.Register(
        "Card", typeof(Card), typeof(CardControl), new PropertyMetadata(null));

    public Card CardData
    {
        get { return (Card)GetValue(CardProperty); }
        set
        {
            SetValue(CardProperty, value);
            Balance.Text = value.Balance.ToString("N2");
            Number.Text = formatCardNumber(value.Number);
            DateExpire.Text = formatDateExpire(value.DateExpire);
            CurrencySymbol.Text = MoneyProvider.Symbols[value.Currency];
        }
    }
    
    public CardControl()
    {
        InitializeComponent();
    }

    private string formatDateExpire(DateTime dateExpire)
    {
        return (dateExpire.Month < 10 ? "0" + dateExpire.Month : dateExpire.Month) + "/" + dateExpire.Year.ToString().Substring(2);
    }
    
    private string formatCardNumber(string number)
    {
        return $"{number.Substring(0, 4)} {number.Substring(4, 4)} {number.Substring(8, 4)} {number.Substring(12)}";
    }
}