using System;
using System.Collections.Generic;
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
    private int _selectedIndex
    {
        get => _selectedIndex;
        set
        {
            SelectDot(_selectedIndex != null ? _selectedIndex : 0);
            _selectedIndex = value;
        }
    }

    public List<Card> Cards
    {
        get
        {
            return Cards;
        }
        set
        {
            Cards = value;
            if (value.Count > 0) _selectedIndex = 0;
            showDots(6);
        }
    }

    public CardViewer()
    {
        InitializeComponent();
        NameScope.SetNameScope(this, new NameScope());
        showDots(6);
    }

    private void showDots(int number)
    {
        CardDots.Children.Clear();
        
        for (int i = 0; i < number; i++)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Name = $"CardDot{i}";
            ellipse.Style = FindResource("CardDot") as Style;
            ellipse.MouseLeftButtonUp += CardDotClick;
            ellipse.Margin = new Thickness(0, 0, i < number - 1 ? 4 : 0, 0);

            CardDots.Children.Add(ellipse);
            
            this.RegisterName(ellipse.Name, ellipse);
        }
        
        if (number > 0) SelectDot(0);
    }

    private void CardDotClick(object sender, MouseButtonEventArgs e)
    {
        _selectedIndex = CardDots.Children.IndexOf((Ellipse)sender);
    }

    private void SelectDot(int index)
    {
        var animation = new DoubleAnimation();
        animation.From = 6;
        animation.To = 20;
        animation.Duration = new Duration(TimeSpan.FromSeconds(5));

        var storyboard = new Storyboard();
        storyboard.Children.Add(animation);
        Storyboard.SetTargetName(animation, $"CardDot{index}");
        Storyboard.SetTargetProperty(animation, new PropertyPath(WidthProperty));
        storyboard.Begin();
    }
}