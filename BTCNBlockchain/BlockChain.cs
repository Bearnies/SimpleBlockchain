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
            return new Block(null, "{}",DateTime.Now); //Block có thể trống
        }

        public Block GetLatestBlock() //Tìm block mới nhất
        {
            return blockChain[blockChain.Count - 1];
        }

        public void AddBlockToBlockChain(Block block)
        {
            Block latestBlock = GetLatestBlock();
            block.blockIndex = latestBlock.blockIndex + 1;
            block.previousHash = latestBlock.hash;
            block.hash = block.CalculateHash();
            blockChain.Add(block);
        }

        public bool IsBlockValid()
        {
            for (int i = 1; i < blockChain.Count; i++)
            {
                Block currentBlock = blockChain[i];
                Block previousBlock = blockChain[i - 1];

                if (currentBlock.hash != currentBlock.CalculateHash())
                {
                    return false;
                }

                if (currentBlock.previousHash != previousBlock.hash)
                {
                    return false;
                }
            }
            return true;
        }
        

    }
}
