using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;
using NBitcoin;

namespace BTCNBlockchain
{
    /// <summary>
    /// Interaction logic for TransactionWindow.xaml
    /// </summary>
    public partial class TransactionWindow : Window
    {
        public TransactionWindow()
        {
            InitializeComponent();
        }

        private void generateSignature_Click(object sender, RoutedEventArgs e)
        {
            if (txbPrivateKeySender.Text != null)
            {
                var message = txbMessage.Text.ToString();
                var bitcoinPrivateKey = txbPrivateKeySender.Text.ToString();
                var convertedPK = new BitcoinSecret(bitcoinPrivateKey, Network.Main);
                string signature = convertedPK.PrivateKey.SignMessage(message);
                txbSign.Text = signature;
            }
            else
            {
                txbPrivateKeySender.Text = "Xin nhập khóa bí mật !";
            }
        }

        public class TransactionInfo
        {
            public string sender { get; set; }
            public string recipient { get; set; }
            public string coin { get; set; }
            public string date { get; set; }
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
                var data = new TransactionInfo { sender = txbAddressSender.Text, recipient = txbAddressRecipent.Text, coin = txbAmountOfCoinSend.Text, date =  DateTime.Now.ToString()};

                var transaction = Transaction.Create(Network.Main);

                byte[] byteString = Encoding.Default.GetBytes(txbAddressRecipent.Text.ToString());
                var hexString = BitConverter.ToString(byteString);
                hexString = hexString.Replace("-", "");

                var recipientPubKeyHash = new KeyId(hexString);
                var recipientAdd = recipientPubKeyHash.GetAddress(Network.Main);

                //Gửi xu đi
                transaction.Outputs.Add(Money.Coins(decimal.Parse(txbAmountOfCoinSend.Text)), recipientAdd.ScriptPubKey);

                var bitcoinPrivateKey = txbPrivateKeySender.Text.ToString();
                var convertedPK = new BitcoinSecret(bitcoinPrivateKey, Network.Main);
                var change = 1m - decimal.Parse(txbAmountOfCoinSend.Text.ToString());
                transaction.Outputs.Add(new Money(change, MoneyUnit.BTC), convertedPK.PrivateKey.ScriptPubKey);

                transactionDataGrid.Items.Add(data);
            }
        }

    }
}
