﻿using System;
using System.Windows;
using System.Windows.Controls;
using SmartWallet.Aplication.Navigator;
using SmartWallet.DAL;
using SmartWallet.Providers;
using SmartWallet.UI.Pages;

namespace SmartWallet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow(int userId)
        {
            InitializeComponent();

            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            this.Left = (screenWidth - this.Width) / 2;
            this.Top = (screenHeight - this.Height) / 2;

            NavigatorObject.pageSwitcher = this;

            HomeScreen homeScreen = new HomeScreen(userId);
            homeScreen.UserInformation.CloseMainWindow += this.Close;

            NavigatorObject.Switch(homeScreen);
        }

        public Action? CloseAction { get; set; }

        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

        public void Navigate(UserControl nextPage, object state)
        {
            this.Content = nextPage;
            INavigator? s = nextPage as INavigator;

            if (s != null)
                s.UtilizeState(state);
            else
                throw new ArgumentException("NextPage is not INavigator! " + nextPage.Name.ToString());
        }
    }
}
