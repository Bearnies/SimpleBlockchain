using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BTCNBlockchain
{
    public class Block
    {
        public int blockIndex { get; set; }
        public string data { get; set; }
        public string previousHash { get; set; }
        public string hash { get; set; }
        public DateTime timeStamp { get; set; }

        public Block(string blockPreviousHash, string blockData, DateTime blockTimeStamp)
        {
            blockIndex = 0;
            previousHash = blockPreviousHash;
            data = blockData;
            timeStamp = blockTimeStamp;
            hash = CalculateHash();
        }

        public string CalculateHash()
        {
            SHA256 sha256 = SHA256.Create();
            
            byte[] inputBytes = Encoding.ASCII.GetBytes($"{timeStamp} - {previousHash ?? ""}-{data}"); //Dựa vào timeStamp và kiểm tra xem có previousHash không để tạo inputcode cho Block
            byte[] outputBytes = sha256.ComputeHash(inputBytes); //Tính toán giá trị hash cho byte array inputcode

            return Convert.ToBase64String(outputBytes); //Chuyển giá trị hash thành string 64-digit và return giá trị hash
            
        }
    }
}
