using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;
using Newtonsoft.Json;
using Secp256k1Net;

namespace BTCNBlockchain
{
    /// <summary>
    /// Interaction logic for TransactionWindow.xaml
    /// </summary>
    public partial class TransactionWindow : Window
    {
        BlockChain transactionList = new BlockChain();

        public TransactionWindow()
        {
            InitializeComponent();
        }

        public static byte[] ObjectToSHA256(Object data)
        {
            var myContent = JsonConvert.SerializeObject(data);
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var hash = SHA256.Create().ComputeHash(buffer);

            return hash;
        }

        public static byte[] SignTransaction(Object data, byte[] privateKey)
        {
            var signature = new byte[64];
            var dataHash = ObjectToSHA256(data);

            using (var secp256k1 = new Secp256k1())
            {
                secp256k1.Sign(signature, dataHash, privateKey);
                return signature;
            }
        }

        private void generateSignature_Click(object sender, RoutedEventArgs e)
        {
            if (txbPrivateKeySender.Text != null)
            {
                var message = txbMessage.Text.ToString();
                var bitcoinPrivateKey = txbPrivateKeySender.Text.ToString();
                //var convertedPK = new BitcoinSecret(bitcoinPrivateKey, Network.Main);
                byte[] convertedPK = ObjectToSHA256(bitcoinPrivateKey);
                //string signature = convertedPK.PrivateKey.SignMessage(message);
                byte[] byteSignature = SignTransaction(message, convertedPK);
                string signature = BitConverter.ToString(byteSignature);
                txbSign.Text = signature;
            }
            else
            {
                txbPrivateKeySender.Text = "Xin nhập khóa bí mật !";
            }
        }

        private void confirmSendCoin_Click(object sender, RoutedEventArgs e)
        {
            if (txbAddressSender.Text == null)
            {
                txbAddressSender.Text = "Xin nhập địa chỉ người gửi !";
            }
            else if (txbAddressRecipent.Text == null)
            {
                txbAddressRecipent.Text = "Xin nhập địa chỉ người nhận !";
            }
            else if (txbPrivateKeySender.Text == null)
            {
                txbPrivateKeySender.Text = "Xin nhập khóa bí mật !";
            }
            else if (txbAmountOfCoinSend.Text == null)
            {
                txbAmountOfCoinSend.Text = "Xin nhập số xu !";
            }
            else
            {
                var dataTransaction = new Transaction { sender = txbAddressSender.Text, recipient = txbAddressRecipent.Text, coin = txbAmountOfCoinSend.Text, date =  DateTime.Now.ToString() };
                byte[] dataToByte = ObjectToSHA256(dataTransaction);
                string dataByteToString = BitConverter.ToString(dataToByte);

                var dataToList = new Transaction { sender = txbAddressSender.Text, recipient = txbAddressRecipent.Text, data = dataByteToString, coin = txbAmountOfCoinSend.Text, date = DateTime.Now.ToString() };
                /*
                var transaction = Transaction.Create(Network.Main);

                byte[] byteArray = Encoding.Default.GetBytes(txbAddressRecipent.Text.ToString());
                var hexString = BitConverter.ToString(byteArray);
                hexString = hexString.Replace("-", "");

                var recipientPubKeyHash = new KeyId(hexString);
                var recipientAdd = recipientPubKeyHash.GetAddress(Network.Main);

                //Gửi xu đi
                transaction.Outputs.Add(Money.Coins(decimal.Parse(txbAmountOfCoinSend.Text)), recipientAdd.ScriptPubKey);

                var bitcoinPrivateKey = txbPrivateKeySender.Text.ToString();
                var convertedPK = new BitcoinSecret(bitcoinPrivateKey, Network.Main);
                var change = 1m - decimal.Parse(txbAmountOfCoinSend.Text.ToString());
                transaction.Outputs.Add(new Money(change, MoneyUnit.BTC), convertedPK.PrivateKey.ScriptPubKey);
                */

                transactionList.AddBlockToBlockChain(new Block(null, dataByteToString, DateTime.Now, 0));

                transactionDataGrid.Items.Add(dataToList);
            }
        }

    }
}
