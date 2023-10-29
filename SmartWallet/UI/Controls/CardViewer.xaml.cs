using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using SmartWallet.Aplication.Navigator;
using SmartWallet.DAL.Entity;
using SmartWallet.Providers;
using SmartWallet.UI.Pages;

namespace SmartWallet.UI.Controls;

public partial class CardViewer : UserControl
{
    public delegate void SetSelectedCardIdHandler(int id);

    public event SetSelectedCardIdHandler SetSelectedCardId;
    
    private int selectedIndex { get; set; }

    private int _selectedIndex
    {
        get => selectedIndex;
        set
        {
            selectedIndex = value;
            if (SetSelectedCardId != null && value >= 0 && value < CardsList.Count) SetSelectedCardId.Invoke(CardsList[value].CardData.Id);
        }
    }

    private List<Border> CardDotsList = new List<Border>();
    private List<CardControl> CardsList = new List<CardControl>();

    public static readonly DependencyProperty CardsProperty = DependencyProperty.Register(
        "Cards", typeof(List<Card>), typeof(CardViewer), new PropertyMetadata(null));

    public List<Card> Cards
    {
        get { return (List<Card>)GetValue(CardsProperty); }
        set
        {
            if (value == null) SetValue(CardsProperty, new List<Card>());
            else SetValue(CardsProperty, value);
            CardsList = GenerateCardControls();
            update();
        }
    }

    public TransactionProvider TransactionProvider;

    public CardViewer()
    {
        InitializeComponent();

        NewCardWindow.CloseNewCardWindow += CancelAddNewCard;
    }

    private void update()
    {
        showDots(CardsList.Count);
        if (_selectedIndex < 0 || _selectedIndex > CardsList.Count)
        {
            _selectedIndex = 0;
        }
        SelectDot(_selectedIndex, 0);

        if (_selectedIndex >= 0 && _selectedIndex < Cards.Count) DisplayedCard.CardData = Cards[_selectedIndex];
    }
    
    private List<CardControl> GenerateCardControls()
    {
        List<CardControl> cards = new List<CardControl>();
        foreach (Card card in Cards)
        {
            CardControl control = new CardControl();
            control.CardData = card;
            cards.Add(control);
        }

        return cards;
    }
    
    private void showDots(int number)
    {
        number += 1;
        CardDots.Children.Clear();
        CardDotsList.Clear();
        
        for (int i = 0; i < number; i++)
        {
            Border rectangle = new Border();
            rectangle.Name = $"CardDot{i}";
            rectangle.Style = FindResource("CardDot") as Style;
            rectangle.MouseLeftButtonUp += CardDotClick;
            rectangle.Margin = new Thickness(0, 0, i < number - 1 ? 4 : 0, 0);

            CardDots.Children.Add(rectangle);
            CardDotsList.Add(rectangle);
        }
    }

    private void CardDotClick(object sender, MouseButtonEventArgs e)
    {
        Border dot = (Border)sender;
        Console.WriteLine($"Dot {CardDots.Children.IndexOf(dot)} clicked!");
        if (CardDots.Children.IndexOf(dot) < 0 || CardDots.Children.IndexOf(dot) == _selectedIndex) return;
        if (CardDotsList[_selectedIndex].Width > 10) UnselectDot(_selectedIndex);
        _selectedIndex = CardDots.Children.IndexOf(dot);
        SelectDot(_selectedIndex);
    }

    private void SelectDot(int index, double seconds=0.1)
    {
        if (CardDotsList[index].Width == 30) return;
        DoubleAnimation widthAnimation = new DoubleAnimation(30, TimeSpan.FromSeconds(seconds));
        CardDotsList[index].BeginAnimation(Border.WidthProperty, widthAnimation);
        CardDotsList[index].Background = new SolidColorBrush(Color.FromRgb(99, 89, 233));
        
        if (_selectedIndex == CardDots.Children.Count - 1)
        {
            DisplayedCard.Visibility = Visibility.Collapsed;
            AddNewCard.Visibility = Visibility.Visible;
        }
        else
        {
            DisplayedCard.Visibility = Visibility.Visible;
            AddNewCard.Visibility = Visibility.Collapsed;
            DisplayedCard.CardData = Cards[_selectedIndex];
        }
    }
    
    private void UnselectDot(int index, double seconds=0.1)
    {
        DoubleAnimation widthAnimation = new DoubleAnimation(10, TimeSpan.FromSeconds(seconds));
        CardDotsList[index].BeginAnimation(Border.WidthProperty, widthAnimation);
        CardDotsList[index].Background = new SolidColorBrush(Color.FromRgb(39, 38, 78));
    }

    private void TransferClick(object sender, RoutedEventArgs e)
    {
        if (TransactionProvider == null) return;
        TransactionProvider.AddNewTransaction("9438547896267294", "8922334455667862", 6000);
    }

    private void AddNewCard_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        AddNewCard.Visibility = Visibility.Collapsed;
        NewCardWindow.Visibility = Visibility.Visible;
    }

    public void CancelAddNewCard()
    {
        AddNewCard.Visibility = Visibility.Visible;
        NewCardWindow.Visibility = Visibility.Collapsed;
    }
}