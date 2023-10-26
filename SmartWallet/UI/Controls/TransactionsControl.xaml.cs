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
            UpdateList();
        }
    }
    
    public TransactionsControl()
    {
        InitializeComponent();
        
        EndDatePicker.SelectedDate = DateTime.Today;
        StartDatePicker.SelectedDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        
        AnimationView.PlayAnimation();
    }

    private async Task UpdateList()
    {
        if (StartDatePicker.SelectedDate == null || EndDatePicker.SelectedDate == null) return;
        DateTime startDate = StartDatePicker.SelectedDate.Value;
        DateTime endDate = EndDatePicker.SelectedDate.Value;
        
        _transactions = await ParseTransactions(TransactionProvider.GetTransactionsBetweenDate(startDate, endDate, CardId).ToList());

        if (_transactions.Count == 0)
        {
            TransactionsList.Visibility = Visibility.Collapsed;
            AnimationView.Visibility = Visibility.Visible;
        }
        else
        {
            TransactionsList.ItemsSource = _transactions;
            TransactionsList.Items.Refresh();
            
            TransactionsList.Visibility = Visibility.Visible;
            AnimationView.Visibility = Visibility.Collapsed;
        }
    }

    private async Task<List<TransactionsItem>> ParseTransactions(List<Transaction> transactions)
    {
        List<TransactionsItem> items = new List<TransactionsItem>();
        
        foreach (Transaction transaction in transactions)
        {
            Card recipient = CardProvider.GetAllCards().Find(c => c.Id == transaction.RecipientCardId);
            Card sender = CardProvider.GetAllCards().Find(c => c.Id == transaction.SenderCardId);
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

    private void EndDatePicker_OnSelectedDateChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (StartDatePicker.SelectedDate == null) return;
        DateTime startDate = StartDatePicker.SelectedDate.Value;
        if (startDate > EndDatePicker.SelectedDate.Value)
        {
            DateTime selectedDate = EndDatePicker.SelectedDate.Value;
            StartDatePicker.SelectedDate = new DateTime(selectedDate.Year, selectedDate.Month, 1);
        }

        UpdateList();
    }

    private void StartDatePicker_OnSelectedDateChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (EndDatePicker.SelectedDate == null) return;
        DateTime endDate = EndDatePicker.SelectedDate.Value;
        if (StartDatePicker.SelectedDate.Value > endDate)
        {
            DateTime selectedDate = StartDatePicker.SelectedDate.Value;
            DateTime endOfMonth = new DateTime(selectedDate.Year, selectedDate.Month,
                DateTime.DaysInMonth(selectedDate.Year, selectedDate.Month));
            if (DateTime.Today < endOfMonth) EndDatePicker.SelectedDate = DateTime.Today;
            else EndDatePicker.SelectedDate = endOfMonth;
        }

        UpdateList();
    }
}