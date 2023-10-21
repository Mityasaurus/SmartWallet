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

namespace SmartWallet.UI.Controls
{
    /// <summary>
    /// Interaction logic for TotalIncome.xaml
    /// </summary>
    public partial class TotalIncome : UserControl
    {
        private Card card;
        public Card Card
        {
            get => card;
            set
            {
                card = value;
                SetIncome();
            }
        }
        public TotalIncome()
        {
            InitializeComponent();
        }

        private void SetIncome()
        {
            //TODO logic for getting total income
            tb_Income.Text = card.Balance.ToString();
        }
    }
}
