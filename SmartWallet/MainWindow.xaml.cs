﻿using System;
using System.Windows;
using SmartWallet.DAL;
using SmartWallet.Providers;

namespace SmartWallet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
        
        public MainWindow(int userId)
        {
            InitializeComponent();
            _userId = userId;
            
            UpdateUI();
            CardViewer.SetSelectedCardId += SetSelectedCardId;
            UserInformation.CloseMainWindow += this.Close;

            UserInformation.UserId = userId;

            //TransactionProvider.AddNewTransaction("9438547896267294", "8922334455667862", 65000);

            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

            this.Left = (screenWidth - this.Width) / 2;
            this.Top = (screenHeight - this.Height) / 2;
        }

        public void SetSelectedCardId(int id)
        {
            CardId = id;
        }

        private void UpdateUI()
        {
            // refresh context and providers
            _context = new SmartWalletContext();
            _cardProvider = new CardProvider(_context);
            _transactionProvider = new TransactionProvider(_context);
            _userProvider = new UserProvider(_context);
            
            // Greeting line
            UserName.Text = _userProvider.GetUserById(_userId).Name;
            
            // MyCards Control
            CardViewer.TransactionProvider = _transactionProvider;
            CardViewer.Cards = _userProvider.GetUserById(_userId).Cards;
            
            // Analytics Control
            Analytics.CardNumber = CardId;
            Analytics.TransactionProvider = _transactionProvider;
            Analytics.Refresh();
            
            // Transaction control
            TransactionsView.CardProvider = _cardProvider;
            TransactionsView.TransactionProvider = _transactionProvider;
            TransactionsView.CardId = CardId;
            TransactionsView.Refresh();

            // User information control
            UserInformation.UserProvider = _userProvider;
            
            // TotalIncome control
            TotalIncome.TransactionProvider = _transactionProvider;
            TotalIncome.CardId = CardId;
            TotalIncome.Refresh();
            
            // TotalOutcome control
            TotalOutcome.TransactionProvider = _transactionProvider;
            TotalOutcome.CardId = CardId;
            TotalOutcome.Refresh();
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            UpdateUI();
        }
    }
}
