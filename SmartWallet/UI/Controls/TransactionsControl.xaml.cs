using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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

public class StatusTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        ResourceDictionary dict = (from d in Application.Current.Resources.MergedDictionaries
            where d.Source != null && d.Source.OriginalString.StartsWith("UI/Localization/lang.")
            select d).FirstOrDefault();

        if (dict != null)
        {
            switch ((string)value)
            {
                case "SENT":
                    return dict["transactionsControl_Grid_ActionSent"];
                case "GET":
                    return dict["transactionsControl_Grid_ActionGet"];
            }
            return value;
        }
        
        return value;
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
        set { SetValue(CardIdProperty, value); }
    }

    public CardProvider CardProvider;
    public TransactionProvider TransactionProvider;
    public User User;
    
    public TransactionsControl()
    {
        InitializeComponent();
        
        EndDatePicker.SelectedDate = DateTime.Today;
        StartDatePicker.SelectedDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        
        AnimationView.PlayAnimation();
        
        App.LanguageChanged += LanguageChanged;
    }

    public void LanguageChanged(object sender, EventArgs e)
    {
        TranslateHeaders();
        TransactionsList.Items.Refresh();
    }

    public void TranslateHeaders()
    {
        ResourceDictionary dict = (from d in Application.Current.Resources.MergedDictionaries
            where d.Source != null && d.Source.OriginalString.StartsWith("UI/Localization/lang.")
            select d).FirstOrDefault();

        if (dict != null)
        {
            CardNumberColumn.Header = dict["transactionsControl_Grid_CardNumber"];
            DateColumn.Header = dict["transactionsControl_Grid_Date"];
            AmountColumn.Header = dict["transactionsControl_Grid_Amount"];
            ActionColumn.Header = dict["transactionsControl_Grid_Action"];
        }
    }

    public void Refresh()
    {
        StartDatePicker.DisplayDateStart = User.RegistrationDate;
        EndDatePicker.DisplayDateStart = User.RegistrationDate;
        
        StartDatePicker.DisplayDateEnd = DateTime.Today;
        EndDatePicker.DisplayDateEnd = DateTime.Today;
        
        UpdateList();
    }

    private void UpdateList()
    {
        if (StartDatePicker.SelectedDate == null 
            || EndDatePicker.SelectedDate == null
            || TransactionProvider == null) return;
        DateTime startDate = StartDatePicker.SelectedDate.Value;
        DateTime endDate = EndDatePicker.SelectedDate.Value;
        
        _transactions = ParseTransactions(TransactionProvider.GetTransactionsBetweenDate(startDate, endDate, CardId).OrderByDescending(t => t.DateTime).ToList());

        if (_transactions.Count == 0)
        {
            TransactionsList.Visibility = Visibility.Collapsed;
            NoGraphData.Visibility = Visibility.Visible;
        }
        else
        {
            TransactionsList.ItemsSource = _transactions;
            TransactionsList.Items.Refresh();
            
            TransactionsList.Visibility = Visibility.Visible;
            NoGraphData.Visibility = Visibility.Collapsed;
        }
    }

    private List<TransactionsItem> ParseTransactions(List<Transaction> transactions)
    {
        List<TransactionsItem> items = new List<TransactionsItem>();
        
        foreach (Transaction transaction in transactions)
        {
            Card recipient = CardProvider.GetCardById(transaction.RecipientCardId);
            Card sender = CardProvider.GetCardById(transaction.SenderCardId);
            Card current = CardProvider.GetCardById(CardId);
            
            TransactionsItem item = new TransactionsItem()
            {
                Card = transaction.SenderCardId == CardId 
                    ? recipient.Number 
                    : sender.Number,
                Date = transaction.DateTime.ToString("MM/dd/yyyy"),
                Amount = transaction.Amount.ToString() + (transaction.SenderCardId == CardId ? MoneyProvider.Symbols[current.Currency] : MoneyProvider.Symbols[sender.Currency]) ,
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
            DateTime possibleDate = new DateTime(selectedDate.Year, selectedDate.Month, 1);
            StartDatePicker.SelectedDate = possibleDate < StartDatePicker.DisplayDateStart
                ? StartDatePicker.DisplayDateStart
                : possibleDate;
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