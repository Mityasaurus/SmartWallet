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
using SmartWallet.DAL.Entity;
using SmartWallet.DAL.Repository;

namespace SmartWallet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            SmartWalletContext context = new SmartWalletContext();
            UserRepository<User> userRepository = new UserRepository<User>(context);
            UserRepository<Card> cardRepository = new UserRepository<Card>(context);

            List<User> users = userRepository.GetAll().ToList();
            List<Card> cards = cardRepository.GetAll().ToList();

            CardViewer.Cards = users[0].Cards;

            // users[0].Cards = new List<Card>();
            // users[0].Cards.Add(new Card()
            // {
            //     Number = "8922334455667862",
            //     Cvv = "123",
            //     DateExpire = new DateTime(2027, 04, 01),
            //     Type = "Money",
            //     Balance = 123956.2,
            //     Currency = "UAH"
            // });
            //
            // userRepository.Update(users[0]);
            
            Console.Write("test");
        }
    }
}
