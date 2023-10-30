using System;
using LiveCharts.Wpf;
using LiveCharts;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
        private int _selectedYear;

        public int CardNumber
        {
            get => _cardNumber;
            set
            {
                _cardNumber = value;
            }
        }

        public TransactionProvider TransactionProvider;
        public User User;

        public SeriesCollection ChartSeries { get; set; }

        public Analytics()
        {
            InitializeComponent();
            AnimationView.PlayAnimation();
        }

        private void FillYears()
        {
            AnalyticsYears.Items.Clear();
            if (User == null)
            {
                AnalyticsYears.Items.Add(DateTime.Today.Year);
                AnalyticsYears.SelectedItem = AnalyticsYears.Items[0];
            }
            else
            {
                int registrationYear = User.RegistrationDate.Year;
                int currentYear = DateTime.Today.Year;
                for (int i = currentYear; i >= registrationYear; i--) AnalyticsYears.Items.Add(i);
                if (AnalyticsYears.Items.Contains(_selectedYear))
                {
                    AnalyticsYears.SelectedItem = _selectedYear;
                }
                else AnalyticsYears.SelectedItem = AnalyticsYears.Items[0];
            }
        }

        public void Refresh()
        {
            if (AnalyticsYears.SelectedItem != null) _selectedYear = (int)AnalyticsYears.SelectedItem;
            FillYears();
            UpdateChartSeries();
        }

        private void UpdateChartSeries()
        {
            if (TransactionProvider == null || User == null) return;
            
            var Transactions = TransactionProvider.GetTransactionsBetweenDate(
                new DateTime(int.Parse(AnalyticsYears.SelectedItem.ToString()), 1, 1),
                new DateTime(int.Parse(AnalyticsYears.SelectedItem.ToString()), 12, 31),
                CardNumber
                );
            
            if (Transactions == null || Transactions.Count == 0)
            {
                AnalyticsDiagram.Visibility = Visibility.Collapsed;
                NoGraphData.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                AnalyticsDiagram.Visibility = Visibility.Visible;
                NoGraphData.Visibility = Visibility.Collapsed;
            }

            ChartSeries = new SeriesCollection();

            Series outcomeSeries;
            Series incomeSeries;

            string titleIncome = "Income";
            string titleOutcome = "Outcome";

            if(App.Language.Name == "uk-UK")
            {
                titleIncome = "Дохід";
                titleOutcome = "Витрати";
            }

            if (!ChartType.IsChecked.Value)
            {
                outcomeSeries = new ColumnSeries
                {
                    Title = titleOutcome,
                    Values = new ChartValues<double>(),
                    DataLabels = false,
                    LabelPoint = point => $"{point.Y:N2}",
                    Fill = new SolidColorBrush(Color.FromRgb(99, 89, 233)),
                    MaxColumnWidth = 15,
                    ColumnPadding = 8,
                    ClipToBounds = false,
                    Foreground = new SolidColorBrush(Colors.White),
                };
            
                incomeSeries = new ColumnSeries
                {
                    Title = titleIncome,
                    Values = new ChartValues<double>(),
                    DataLabels = false,
                    LabelPoint = point => $"{point.Y:N2}",
                    Fill = new SolidColorBrush(Color.FromRgb(100, 207, 246)),
                    MaxColumnWidth = 15,
                    ColumnPadding = 8,
                    ClipToBounds = false,
                    Foreground = new SolidColorBrush(Colors.White),
                };
            }
            else
            {
                outcomeSeries = new LineSeries
                {
                    Title = titleOutcome,
                    Values = new ChartValues<double>(),
                    DataLabels = false,
                    LabelPoint = point => $"{point.Y:N2}",
                    Stroke = new SolidColorBrush(Color.FromRgb(99, 89, 233)),
                    ClipToBounds = false,
                    Foreground = new SolidColorBrush(Colors.White),
                };
            
                incomeSeries = new LineSeries
                {
                    Title = titleIncome,
                    Values = new ChartValues<double>(),
                    DataLabels = false,
                    LabelPoint = point => $"{point.Y:N2}",
                    Stroke = new SolidColorBrush(Color.FromRgb(100, 207, 246)),
                    ClipToBounds = false,
                    Foreground = new SolidColorBrush(Colors.White),
                };
            }

            int month;
            
            for(month = 1; month <= (AnalyticsYears.SelectedItem.ToString() == DateTime.Today.Year.ToString()
                    ? DateTime.Today.Month
                    : 12); month++)
            {
                var income = TransactionProvider.GetIncomeByMonth(month, CardNumber);
                var outcome = TransactionProvider.GetOutcomeByMonth(month, CardNumber);
            
                incomeSeries.Values.Add(income);
                outcomeSeries.Values.Add(outcome);
            }

            if (ChartType.IsChecked.Value)
            {
                var income = TransactionProvider.GetIncomeByMonth(month - 1, CardNumber);
                var outcome = TransactionProvider.GetOutcomeByMonth(month - 1, CardNumber);

                incomeSeries.Values.Add(income);
                outcomeSeries.Values.Add(outcome);
            }

            ChartSeries.Add(incomeSeries);
            ChartSeries.Add(outcomeSeries);
            
            AnalyticsDiagram.Series = ChartSeries;
            AnalyticsDiagram.Update();
            
            DataContext = this;
        }

        private void AnalyticsYears_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AnalyticsYears.SelectedItem != null)
            {
                UpdateChartSeries();
            }
        }

        private void ChartType_OnChecked(object sender, RoutedEventArgs e)
        {
            UpdateChartSeries();
        }

        private void ChartType_OnUnchecked(object sender, RoutedEventArgs e)
        {
            UpdateChartSeries();
        }
    }
}
