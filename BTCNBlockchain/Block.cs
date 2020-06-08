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
        public int BlockIndex { get; set; }
        public string Data { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public DateTime TimeStamp { get; set; }
        public int Nonce { get; set; }

        public Block(string blockPreviousHash, string blockData, DateTime blockTimeStamp, int nonce)
        {
            BlockIndex = 0;
            PreviousHash = blockPreviousHash;
            Data = blockData;
            TimeStamp = blockTimeStamp;
            Hash = CalculateHash();
            Nonce = nonce; 
        }

        public string CalculateHash()
        {
            SHA256 sha256 = SHA256.Create();
            
            byte[] inputBytes = Encoding.ASCII.GetBytes($"{TimeStamp} - {PreviousHash ?? ""}-{Data}"); //Dựa vào timeStamp và kiểm tra xem có previousHash không để tạo inputcode cho Block
            byte[] outputBytes = sha256.ComputeHash(inputBytes); //Tính toán giá trị hash cho byte array inputcode

            return Convert.ToBase64String(outputBytes); //Chuyển giá trị hash thành string 64-digit và return giá trị hash
        }
    }
}
