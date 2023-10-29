using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Globalization;

namespace SmartWallet
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static List<CultureInfo> _languages = new List<CultureInfo>();

        public static List<CultureInfo> Languages
        {
            get => _languages;
        }

        public static CultureInfo Language
        {
            get => System.Threading.Thread.CurrentThread.CurrentUICulture;
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (value == System.Threading.Thread.CurrentThread.CurrentUICulture) return;

                System.Threading.Thread.CurrentThread.CurrentUICulture = value;

                ResourceDictionary dict = new ResourceDictionary();
                switch (value.Name)
                {
                    case "uk-UK":
                        dict.Source = new Uri(String.Format("UI/Localization/lang.{0}.xaml", value.Name), UriKind.Relative);
                        break;
                    default:
                        dict.Source = new Uri("UI/Localization/lang.xaml", UriKind.Relative);
                        break;
                }

                ResourceDictionary oldDict = (from d in Application.Current.Resources.MergedDictionaries
                    where d.Source != null && d.Source.OriginalString.StartsWith("UI/Localization/lang.")
                    select d).FirstOrDefault();

                if (oldDict != null)
                {
                    int ind = Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);
                    Application.Current.Resources.MergedDictionaries.Remove(oldDict);
                    Application.Current.Resources.MergedDictionaries.Insert(ind, dict);
                }
                else
                {
                    Application.Current.Resources.MergedDictionaries.Add(dict);
                }
            }
        }
        
        public App()
        {
            _languages.Clear();
            
            _languages.Add(new CultureInfo("en-US"));
            _languages.Add(new CultureInfo("uk-UK"));
        }
    }
}
