using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SmartWallet.DAL.Entity;

namespace SmartWallet.UI.Controls;

public partial class CardControl : UserControl
{
    public static readonly DependencyProperty CardProperty = DependencyProperty.Register(
        "Card", typeof(Card), typeof(CardControl), new PropertyMetadata(null));

    public Card CardData
    {
        get { return (Card)GetValue(CardProperty); }
        set { SetValue(CardProperty, value); }
    }
    
    public CardControl()
    {
        InitializeComponent();

        DataContext = CardData;
    }
}