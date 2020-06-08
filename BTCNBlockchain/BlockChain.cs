using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTCNBlockchain
{
    public class BlockChain
    {
        public IList<Block> blockChain { get; set; } //Các Blockchain là 1 dãy các đối tượng có thể được quản lý bằng index

        public BlockChain()
        {
            blockChain = new List<Block>(); //Blockchain gồm nhiều block
            blockChain.Add(CreateBlock());
        }

        public Block CreateBlock()
        {
            return new Block(null, "{}",DateTime.Now, 0); //Block có thể trống
        }

        public Block GetLatestBlock() //Tìm block mới nhất
        {
            return blockChain[blockChain.Count - 1];
        }

        public void AddBlockToBlockChain(Block block)
        {
            Block latestBlock = GetLatestBlock();
            block.BlockIndex = latestBlock.BlockIndex + 1;
            block.PreviousHash = latestBlock.Hash;
            block.Hash = block.CalculateHash();
            block.TimeStamp = DateTime.Now;
            block.Nonce = 0;
            while (!IsBlockValid())
            {
                block.Nonce = block.Nonce + 1;
                block.TimeStamp = DateTime.Now;
                block.Hash = block.CalculateHash();
            }
            blockChain.Add(block);
        }

        public bool IsBlockValid()
        {
            for (int i = 1; i < blockChain.Count; i++)
            {
                Block currentBlock = blockChain[i];
                Block previousBlock = blockChain[i - 1];

                if (currentBlock.Hash != currentBlock.CalculateHash())
                {
                    return false;
                }

                if (currentBlock.PreviousHash != previousBlock.Hash)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
