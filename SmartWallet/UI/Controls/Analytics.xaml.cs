using LiveCharts.Wpf;
using LiveCharts;
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
using SmartWallet.DAL.Entity;
using SmartWallet.Providers;

namespace SmartWallet.UI.Controls
{
    /// <summary>
    /// Interaction logic for Analytics.xaml
    /// </summary>
    public partial class Analytics : UserControl
    {
        private int _cardNumber;

        public int CardNumber
        {
            get => _cardNumber;
            set
            {
                _cardNumber = value;
                UpdateChartSeries();
            }
        }

        public SeriesCollection ChartSeries { get; set; }
        public List<string> MonthLabels { get; set; }

        public Analytics()
        {
            InitializeComponent();
        }

        private void UpdateChartSeries()
        {
            var Transactions = TransactionProvider.GetAllTransactionByCardId(CardNumber);

            if (Transactions == null)
            {
                return;
            }

            ChartSeries = new SeriesCollection();
            MonthLabels = new List<string>();

            var uniqueMonths = Transactions.Select(t => t.DateTime.Month).Distinct().Reverse().Take(6).Reverse();

            // Создаем серии для income и outcome
            var incomeSeries = new ColumnSeries
            {
                Title = "Income",
                Values = new ChartValues<double>(),
                DataLabels = true,
                LabelPoint = point => $"{point.Y:N2}",
            };

            var outcomeSeries = new ColumnSeries
            {
                Title = "Outcome",
                Values = new ChartValues<double>(),
                DataLabels = true,
                LabelPoint = point => $"{point.Y:N2}",
            };

            foreach (var month in uniqueMonths)
            {
                var income = CardProvider.GetIncomeByMonth(month, CardNumber);
                var outcome = CardProvider.GetOutcomeByMonth(month, CardNumber);

                // Добавляем данные в серии
                incomeSeries.Values.Add(income);
                outcomeSeries.Values.Add(outcome);

                // Добавляем месяц внизу диаграммы
                MonthLabels.Add($"{month}");
            }

            // Добавляем серии в ChartSeries
            ChartSeries.Add(incomeSeries);
            ChartSeries.Add(outcomeSeries);

            AnalyticsDiagram.Series = ChartSeries;
            AnalyticsLables.Labels = MonthLabels;

            DataContext = this;
        }

    }
}
