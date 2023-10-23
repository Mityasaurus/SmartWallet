using SmartWallet.DAL.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartWallet.UI.Controls
{
    /// <summary>
    /// Interaction logic for TotalIncome.xaml
    /// </summary>
    public partial class TotalIncome : UserControl
    {
        private int cardId;
        public int CardId
        {
            get => cardId;
            set
            {
                cardId = value;
                SetIncome();
            }
        }
        public TotalIncome()
        {
            InitializeComponent();
        }

        private void SetIncome()
        {
            var income = CardProvider.GetIncomeByMonth(DateTime.Now.Month, cardId);
            tb_Income.Text = $"{income:f2}";
        }
    }
}
