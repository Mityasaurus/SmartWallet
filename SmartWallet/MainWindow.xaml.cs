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
        private DBProvider _dbProvider = new DBProvider();
        
        public MainWindow()
        {
            InitializeComponent();

            CardViewer.Cards = _dbProvider.GetAllUsers()[0].Cards;

            // _dbProvider.AddNewTransaction("8922334455667862", "9438547896267294", 135.68);
        }
    }
}
