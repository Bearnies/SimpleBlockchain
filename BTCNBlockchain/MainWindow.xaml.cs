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
using NBitcoin.RPC;
using QBitNinja.Client;
using QBitNinja.Client.Models;
using HBitcoin;

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
            Money moneyAmount = Money.Zero; //Khởi tạo balance = 0
            //---------------------------
        }

        private void createUserWallet_Click(object sender, RoutedEventArgs e)
        {
            createWallet();
        }
    }
}
