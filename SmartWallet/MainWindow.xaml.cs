using SmartWallet.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using SmartWallet.DAL;
using SmartWallet.DAL.Repository;
using SmartWallet.Providers;

namespace SmartWallet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserProvider _userProvider = new UserProvider();
        
        public MainWindow()
        {
            InitializeComponent();

            CardViewer.Cards = _userProvider.GetAllUsers()[0].Cards;

            Console.Write("test");
        }
    }
}
