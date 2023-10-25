using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using SmartWallet.DAL.Entity;
using SmartWallet.Providers;

namespace SmartWallet.UI.Controls;

enum TransactionStatus
{
    GET,
    SENT
}

class TransactionsItem
{
    public string Card { get; set; }
    public string Date { get; set; }
    public string Amount { get; set; }
    public string TransactionStatus { get; set; }
}

public class StatusToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        Brush brushGet = new SolidColorBrush(Color.FromArgb(85, 2, 177, 90));
        Brush brushSent = new SolidColorBrush(Color.FromArgb(85, 235, 0, 27));
        return ((TransactionsItem)value).TransactionStatus == "GET" ? brushGet : brushSent;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public partial class TransactionsControl : UserControl
{
    private List<TransactionsItem> _transactions { get; set; }

    public static readonly DependencyProperty CardIdProperty = DependencyProperty.Register(
        nameof(CardId), typeof(int), typeof(TransactionsControl), new PropertyMetadata(default(int)));
    
    public int CardId
    {
        get { return (int)GetValue(CardIdProperty); }
        set
        {
            SetValue(CardIdProperty, value);
            // Task upodateList = new Task(() => UpdateList());
            // upodateList.Start();
            UpdateList();
        }
    }
    
    public TransactionsControl()
    {
        InitializeComponent();
    }

    private async Task UpdateList()
    {
        _transactions = await ParseTransactions(TransactionProvider.GetAllTransactionByCardId(CardId).Take(10).ToList());
        Console.WriteLine("first");
        TransactionsList.ItemsSource = _transactions;
        TransactionsList.Items.Refresh();
    }

    private async Task<List<TransactionsItem>> ParseTransactions(List<Transaction> transactions)
    {
        List<TransactionsItem> items = new List<TransactionsItem>();
        foreach (Transaction transaction in transactions)
        {
            Card recipient = CardProvider.GetAllCards().Find(c => c.Id == transaction.RecipientCardId);
            Card sender = CardProvider.GetAllCards().Find(c => c.Id == transaction.SenderCardId);
            // Card sender = CardProvider.GetCardById(transaction.SenderCardId);
            TransactionsItem item = new TransactionsItem()
            {
                Card = transaction.SenderCardId == CardId 
                    ? recipient.Number 
                    : sender.Number,
                Date = transaction.DateTime.ToString("ddd,dd MMM yyyy"),
                Amount = transaction.Amount.ToString() + MoneyProvider.Symbols[CardProvider.GetCardById(CardId).Currency],
                TransactionStatus = transaction.SenderCardId == CardId ? TransactionStatus.SENT.ToString() : TransactionStatus.GET.ToString()
            };
            
            items.Add(item);
        }

        return items;
    }
}