using SmartWallet.DAL.Entity;
using SmartWallet.Providers;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SmartWallet.UI.Controls
{
    /// <summary>
    /// Interaction logic for NewCardWindow.xaml
    /// </summary>
    public partial class EditCard : UserControl
    {
        public static CardProvider CardProvider;
        public static int CardId;

        public delegate void MainWindowHandler();
        public event MainWindowHandler CloseEditCardWindow;
        private bool _defaulted = true;

        public event MainWindowHandler UpdateUI; // Temporary solution

        public EditCard()
        {
            InitializeComponent();
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            BackgroundPreview.Source =
                new BitmapImage(new Uri("pack://application:,,,/UI/Images/editCard.png"));
            _defaulted = true;
            CloseEditCardWindow?.Invoke();
        }

        private void btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (CardProvider == null || CardId == null) return;
            
            Card card = CardProvider.GetCardById(CardId);

            if (_defaulted)
            {
                card.Background = new byte[]{};
            }
            else
            {
                byte[] imgBytes;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create((BitmapImage)BackgroundPreview.Source));
                    encoder.Save(memoryStream);

                    imgBytes = memoryStream.ToArray();
                }

                card.Background = imgBytes;
            }

            CardProvider.Update(card);
            
            btn_Cancel_Click(sender, e);
            if (UpdateUI != null) UpdateUI?.Invoke();
        }

        private void btn_Default_Click(object sender, RoutedEventArgs e)
        {
            BackgroundPreview.Source =
                new BitmapImage(new Uri("pack://application:,,,/UI/Images/editCard.png"));
            _defaulted = true;
        }

        private void BackgroundPreview_OnDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);

                try
                {
                    BackgroundPreview.Source = new BitmapImage(new Uri(fileNames[0]));
                    _defaulted = false;
                }
                catch (Exception exception) { }
            }
        }
    }
}
