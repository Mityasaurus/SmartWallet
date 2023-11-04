using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SmartWallet.DAL.Entity;
using SmartWallet.Providers;
using Image = System.Drawing.Image;

namespace SmartWallet.UI.Controls;

public partial class CardControl : UserControl
{
    public event EventHandler EditClick;
    
    public static readonly DependencyProperty CardProperty = DependencyProperty.Register(
        "Card", typeof(Card), typeof(CardControl), new PropertyMetadata(null));

    public Card CardData
    {
        get { return (Card)GetValue(CardProperty); }
        set
        {
            SetValue(CardProperty, value);
            Balance.Text = value.Currency >= Currency.BTC ? value.Balance.ToString("#,0.####################") : value.Balance.ToString("N2");
            Number.Text = formatCardNumber(value.Number);
            DateExpire.Text = formatDateExpire(value.DateExpire);
            CurrencySymbol.Text = MoneyProvider.Symbols[value.Currency];

            // background
            if (value.Background.Length == 0)
            {
                Card.Background =
                    new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/UI/Images/CardBackground.png")));
            }
            else
            {
                using (MemoryStream ms = new MemoryStream(value.Background))
                {
                    BitmapImage img = new BitmapImage();
                    img.BeginInit();
                    img.CacheOption = BitmapCacheOption.OnLoad;
                    img.StreamSource = ms;
                    img.EndInit();
                    
                    if (img.CanFreeze) img.Freeze();

                    Card.Background = new ImageBrush(img);
                }
            }
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

    private void EditCard_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (EditClick != null) EditClick(sender, e);
    }
}