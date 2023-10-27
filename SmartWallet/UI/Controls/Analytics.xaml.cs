using LiveCharts.Wpf;
using LiveCharts;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using SmartWallet.Providers;

namespace SmartWallet.UI.Controls
{
    /// <summary>
    /// Interaction logic for Analytics.xaml
    /// </summary>
    public partial class Analytics : UserControl
    {
        private int _cardNumber;
        private int _oldTransactionsCount;

        public int CardNumber
        {
            get => _cardNumber;
            set
            {
                if (value != _cardNumber)
                {
                    _oldTransactionsCount = -1;
                }
                _cardNumber = value;
            }
        }

        public TransactionProvider TransactionProvider;

        public SeriesCollection ChartSeries { get; set; }
        public List<string> MonthLabels { get; set; }

        public Analytics()
        {
            InitializeComponent();
        }

        public void Refresh()
        {
            UpdateChartSeries();
        }

        private void UpdateChartSeries()
        {
            if (TransactionProvider == null) return;
            
            var Transactions = TransactionProvider.GetAllTransactionByCardId(CardNumber);

            if (Transactions == null)
            {
                return;
            }

            if(Transactions.Count == _oldTransactionsCount)
            {
                return;
            }

            _oldTransactionsCount = Transactions.Count;

            ChartSeries = new SeriesCollection();
            MonthLabels = new List<string>();

            var uniqueMonths = Transactions.Select(t => t.DateTime.Month).Distinct().Reverse().Take(8).Reverse();

            var outcomeSeries = new ColumnSeries
            {
                Title = "Outcome",
                Values = new ChartValues<double>(),
                DataLabels = false,
                LabelPoint = point => $"{point.Y:N2}",
                Fill = new SolidColorBrush(Color.FromRgb(100, 207, 246)),
                MaxColumnWidth = 15,
                ColumnPadding = 8,
                Foreground = new SolidColorBrush(Colors.White),
            };

            var incomeSeries = new ColumnSeries
            {
                Title = "Income",
                Values = new ChartValues<double>(),
                DataLabels = false,
                LabelPoint = point => $"{point.Y:N2}",
                Fill = new SolidColorBrush(Color.FromRgb(99, 89, 233)),
                MaxColumnWidth = 15,
                ColumnPadding = 8,
                Foreground = new SolidColorBrush(Colors.White),
            };

            foreach (var month in uniqueMonths)
            {
                var income = TransactionProvider.GetIncomeByMonth(month, CardNumber);
                var outcome = TransactionProvider.GetOutcomeByMonth(month, CardNumber);

                incomeSeries.Values.Add(income);
                outcomeSeries.Values.Add(outcome);

                MonthLabels.Add($"{month}");
            }

            ChartSeries.Add(incomeSeries);
            ChartSeries.Add(outcomeSeries);

            AnalyticsDiagram.Series = ChartSeries;
            AnalyticsLables.Labels = MonthLabels;

            DataContext = this;
        }

    }
}
