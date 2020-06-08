using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using Base58Check;
using Secp256k1Net;

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

        public static byte[] GetPrivateKey()
        {
            using (var secp256k1 = new Secp256k1())
            {
                var hash = new byte[32];
                var rnd = RandomNumberGenerator.Create();
                do {
                    rnd.GetBytes(hash);
                }
                while (!secp256k1.SecretKeyVerify(hash));

                return hash;
            }

        }

        public static byte[] GetPublicKey(byte[] privateKey)
        {
            using (var secp256k1 = new Secp256k1())
            {
                var publicKey = new byte[64];
                secp256k1.PublicKeyCreate(publicKey, privateKey);

                return publicKey;
            }
        }

        public static string GetAddress(byte[] data)
        {
            var hash = SHA1.Create().ComputeHash(data);
            var result = Base58CheckEncoding.Encode(hash);
            return result;
        }

        public static string ConvertByteToString(byte[] data)
        {
            return BitConverter.ToString(data).Replace("-", string.Empty);
        }

        public void CreateWallet()
        {
            //-----Tạo địa chỉ cho Wallet
            /*
            NBitcoin.Key privateKey = new NBitcoin.Key();
            PubKey publicKey = privateKey.PubKey;
            BitcoinAddress bitcoinAddress = publicKey.GetAddress(ScriptPubKeyType.Legacy, Network.Main); //Địa chỉ
            BitcoinSecret bitcoinPrivateKey = privateKey.GetWif(Network.Main); //Bitcoin Private key, GetWif = Wallet Import Format
            //var transactionId = Coin.ByteArrayToString(Coin.StringToSha256(publicKey.ToString())); //Tạo Transaction ID cho các transaction sau này
            var givenCoins = new Money(1m, MoneyUnit.BTC);
            */

            //txbAddress.Text = bitcoinAddress.ToString();
            //txbPrivateKey.Text = bitcoinPrivateKey.ToString();
            //amountOfCoin.Text = givenCoins.ToString();
            //---------------------------

            //-----Tạo địa chỉ cho Wallet
            byte[] privateKey = GetPrivateKey(); //System.Byte[] cần được chuyển thành string
            byte[] publicKey = GetPublicKey(privateKey); //System.Byte[] cần được chuyển thành string
            string address = GetAddress(publicKey);
            var givenCoins = 1m;
            //---

            txbAddress.Text = address.ToString();
            txbPrivateKey.Text = ConvertByteToString(privateKey); //Chuyển array byte thành string ở hệ hex tương ứng
            amountOfCoin.Text = givenCoins.ToString();
        }

        private void createUserWallet_Click(object sender, RoutedEventArgs e)
        {
            CreateWallet();
        }

        private void makeTransaction_Click(object sender, RoutedEventArgs e)
        {
            TransactionWindow tW = new TransactionWindow();
            tW.Show();
        }
    }
}
