﻿using System;
using System.Windows;
using System.Windows.Threading;
using SmartWallet.Providers;
using SmartWallet.UI.Controls;

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
                //if (value < UserProvider.GetUserByID(_userId).Cards.Count && value >= 0)
                //{
                    _cardId = value;
                    UpdateUI();
                    Console.WriteLine(value);
                //}
            }
        }
        
        public MainWindow(int userId)
        {
            InitializeComponent();
            _userId = userId;
            UserName.Text = UserProvider.GetUserByID(_userId).Name;

            UpdateUI();
            CardViewer.SetSelectedCardId += SetSelectedCardId;

            //TransactionProvider.AddNewTransaction("9438547896267294", "8922334455667862", 65000);
        }

        public void SetSelectedCardId(int id)
        {
            CardId = id;
        }

        private void UpdateUI()
        {
            CardViewer.Cards = UserProvider.GetUserByID(_userId).Cards;
            Analytics.CardNumber = CardId;
            TransactionsView.CardId = CardId;
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            UpdateUI();
        }
    }
}
