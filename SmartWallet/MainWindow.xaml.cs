using System;
using System.Globalization;
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
        
        public MainWindow()
        {
            InitializeComponent();

            App.Language = new CultureInfo("en-US");

            NavigatorObject.pageSwitcher = this;

            LoginWindow loginWindow = new LoginWindow();
            NavigatorObject.Switch(loginWindow);
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
