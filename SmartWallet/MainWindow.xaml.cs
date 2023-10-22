using SmartWallet.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private DispatcherTimer _timer;
        private int _userId;
        
        public MainWindow(int userId)
        {
            InitializeComponent();
            _userId = userId;
            
            UpdateUI();
            
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(5);
            _timer.Tick += TimerTick;
            _timer.Start();
            TransactionProvider.AddNewTransaction("8922334455667862", "9438547896267294", 1);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            CardViewer.Cards = UserProvider.GetUserByID(_userId).Cards;
        }
    }
}
