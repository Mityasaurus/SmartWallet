using SmartWallet.DAL;
using SmartWallet.Providers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using SmartWallet.Aplication.Navigator;
using SmartWallet.DAL.Entity;
using SmartWallet.UI.Controls;

namespace SmartWallet.UI.Pages
{
    /// <summary>
    /// Interaction logic for HomeScreen.xaml
    /// </summary>
    public partial class HomeScreen : UserControl
    {
        private bool _isUpdating = false;
        private int _userId;
        private int _cardId;

        public int CardId
        {
            get => _cardId;
            set
            {
                _cardId = value;
                UpdateUI();
            }
        }

        private SmartWalletContext _context;
        private CardProvider _cardProvider;
        private TransactionProvider _transactionProvider;
        private UserProvider _userProvider;


        public HomeScreen(int userId)
        {
            InitializeComponent();

            _userId = userId;

            UpdateUI();
            CardViewer.SetSelectedCardId += SetSelectedCardId;

            UserInformation.UserId = userId;
        }

        public void ChangeLanguage(object sender, EventArgs e)
        {
            App.Language = App.Languages[1];
        }

        public void SetSelectedCardId(int id)
        {
            CardId = id;
        }

        private void UpdateGreetingLine(string name)
        {
            UserName.Text = name;
        }

        private void UpdateMyCardsControl(List<Card> cards)
        {
            CardViewer.TransactionProvider = _transactionProvider;
            CardViewer.NewCardWindow.CardProvider = _cardProvider;
            EditCard.CardProvider = _cardProvider;
            TransferControl.CardProvider = _cardProvider;
            TransferControl.TransactionProvider = _transactionProvider;
            CardViewer.Cards = cards;
            CardViewer.NewCardWindow.UserId = _userId;

            CardViewer.NewCardWindow.UpdateUI += UpdateUI; // Temporary solution
            CardViewer.EditCardWindow.UpdateUI += UpdateUI; // Temporary solution
            CardViewer.TransferWindow.UpdateUI += UpdateUI; // Temporary solution
        }

        private void UpdateAnalyticsControl(User user)
        {
            Analytics.CardNumber = CardId;
            Analytics.TransactionProvider = _transactionProvider;
            Analytics.User = user;
            Analytics.Refresh();
        }

        private void UpdateTransactionsControl(User user)
        {
            TransactionsView.CardProvider = _cardProvider;
            TransactionsView.TransactionProvider = _transactionProvider;
            TransactionsView.CardId = CardId;
            TransactionsView.User = user;
            TransactionsView.Refresh();
        }

        private void UpdateUserInfoControl()
        {
            UserInformation.UserProvider = _userProvider;
        }

        private void UpdateTotalIncomeControl()
        {
            TotalIncome.TransactionProvider = _transactionProvider;
            TotalIncome.CardId = CardId;
            TotalIncome.Refresh();
        }

        private void UpdateTotalOutcomeControl()
        {
            TotalOutcome.TransactionProvider = _transactionProvider;
            TotalOutcome.CardId = CardId;
            TotalOutcome.Refresh();
        }

        private async Task update(int userId)
        {
            // refresh context and providers
            _context = new SmartWalletContext();
            _cardProvider = new CardProvider(_context);
            _transactionProvider = new TransactionProvider(_context);
            _userProvider = new UserProvider(_context);

            // Greeting line
            Application.Current.Dispatcher.Invoke(() => UpdateGreetingLine(_userProvider.GetUserById(userId).Name));

            // MyCards Control
            Application.Current.Dispatcher.Invoke(() => UpdateMyCardsControl(_userProvider.GetUserById(_userId).Cards));

            // Analytics Control
            Application.Current.Dispatcher.Invoke(() => UpdateAnalyticsControl(_userProvider.GetUserById(_userId)));

            // Transaction control
            Application.Current.Dispatcher.Invoke(() => UpdateTransactionsControl(_userProvider.GetUserById(_userId)));

            // User information control
            Application.Current.Dispatcher.Invoke(UpdateUserInfoControl);

            // TotalIncome control
            Application.Current.Dispatcher.Invoke(UpdateTotalIncomeControl);

            // TotalOutcome control
            Application.Current.Dispatcher.Invoke(UpdateTotalOutcomeControl);
        }

        private async void UpdateUI()
        {
            if (_isUpdating) return;
            
            // animation start
            LoadingPanel.Visibility = Visibility.Visible;
            LoadingAnimation.PlayAnimation();

            _isUpdating = true;
            await Task.Run(() => update(_userId));
            _isUpdating = false;
            
            // animation stop
            LoadingPanel.Visibility = Visibility.Hidden;
            LoadingAnimation.StopAnimation();
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            UpdateUI();
        }

        private void English_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            App.Language = new CultureInfo("en-US");
        }

        private void Ukrainian_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            App.Language = new CultureInfo("uk-UK");
        }
    }
}