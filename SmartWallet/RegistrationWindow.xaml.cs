using Microsoft.EntityFrameworkCore;
using SmartWallet.Providers;
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
using System.Windows.Shapes;

namespace SmartWallet
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private DBProvider _dbProvider; 

        public RegistrationWindow()
        {
            InitializeComponent();

            _dbProvider = new DBProvider();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            //TODO Redirection to login window
        }
    }
}
