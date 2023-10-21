using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using SmartWallet.DAL.Entity;

namespace SmartWallet.UI.Controls;

public partial class CardViewer : UserControl
{
    private int _selectedIndex { get; set; }
    private List<Border> CardDotsList = new List<Border>();

    public static readonly DependencyProperty CardsProperty = DependencyProperty.Register(
        "Cards", typeof(List<Card>), typeof(CardViewer), new PropertyMetadata(null));

    public List<Card> Cards
    {
        get { return (List<Card>)GetValue(CardsProperty); }
        set
        {
            SetValue(CardsProperty, value);
            showDots(value.Count);
            if (value.Count > 0)
            {
                _selectedIndex = 0;
                SelectDot(_selectedIndex);
                DisplayedCard.CardData = Cards[0];
            }
        }
    }


    public CardViewer()
    {
        InitializeComponent();
        // showDots(6);
    }

    private void showDots(int number)
    {
        CardDots.Children.Clear();
        
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
        UnselectDot(_selectedIndex);
        _selectedIndex = CardDots.Children.IndexOf((Border)sender);
        SelectDot(_selectedIndex);
    }

    private void SelectDot(int index)
    {
        DoubleAnimation widthAnimation = new DoubleAnimation(30, TimeSpan.FromSeconds(0.1));
        CardDotsList[index].BeginAnimation(Border.WidthProperty, widthAnimation);
        CardDotsList[index].Background = new SolidColorBrush(Color.FromRgb(99, 89, 233));
    }
    
    private void UnselectDot(int index)
    {
        DoubleAnimation widthAnimation = new DoubleAnimation(10, TimeSpan.FromSeconds(0.1));
        CardDotsList[index].BeginAnimation(Border.WidthProperty, widthAnimation);
        CardDotsList[index].Background = new SolidColorBrush(Color.FromRgb(39, 38, 78));
    }

    private void TransferClick(object sender, RoutedEventArgs e)
    {
        // TODO Transfer
    }
}