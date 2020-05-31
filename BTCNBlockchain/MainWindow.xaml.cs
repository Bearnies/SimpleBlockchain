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
using NBitcoin;


namespace BTCNBlockchain
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void createWallet()
        {
            //-----Tạo địa chỉ cho Wallet
            NBitcoin.Key privateKey = new NBitcoin.Key();
            PubKey publicKey = privateKey.PubKey;
            BitcoinAddress bitcoinAddress = publicKey.GetAddress(ScriptPubKeyType.Legacy, Network.Main); //Địa chỉ
            BitcoinSecret bitcoinPrivateKey = privateKey.GetWif(Network.Main); //Bitcoin Private key, GetWif = Wallet Import Format
            var transactionId = Coin.ByteArrayToString(Coin.StringToSha256(publicKey.ToString())); //Tạo Transaction ID cho các transaction sau này
            var givenCoins = new Money(1, MoneyUnit.BTC);
            //---------------------------

            txbAddress.Text = bitcoinAddress.ToString();
            txbPrivateKey.Text = bitcoinPrivateKey.ToString();
            amountOfCoin.Text = givenCoins.ToString();
        }

        private void createUserWallet_Click(object sender, RoutedEventArgs e)
        {
            createWallet();
        }

        private void makeTransaction_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
