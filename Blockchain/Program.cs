using System;
using System.Collections.Generic;

namespace Blockchain
{
    class Program
    {
        static void Main()
        {
            const string minerAddress = "miner1";
            const string user1Address = "A";
            const string user2Address = "B";
            BlockChain blockChain = new BlockChain(proofOfWorkDifficulty: 2, miningReward: 10);
            blockChain.CreateTransaction(new Transaction(user1Address, user2Address, 200));
            blockChain.CreateTransaction(new Transaction(user2Address, user1Address, 10));
            Console.WriteLine($"Is valid: {blockChain.IsValidChain()}");
            Console.WriteLine();
            Console.WriteLine("--------- Start mining ---------");
            blockChain.MineBlock(minerAddress);
            Console.WriteLine($"BALANCE of the miner: { blockChain.GetBalance(minerAddress)}");
            blockChain.CreateTransaction(new Transaction(user1Address, user2Address, 5));
            Console.WriteLine();
            Console.WriteLine("--------- Start mining ---------");
            blockChain.MineBlock(minerAddress);
            Console.WriteLine($"BALANCE of the miner: {blockChain.GetBalance(minerAddress)}");
            Console.WriteLine();
            PrintChain(blockChain);
            Console.WriteLine();
            Console.WriteLine("Hacking the blockchain...");
            blockChain.Chain[1].Transaction = new List<Transaction> { new Transaction(user1Address, minerAddress, 150) };
            Console.WriteLine($"Is valid: {blockChain.IsValidChain()}");
            Console.ReadKey();
        }
        private static void PrintChain(BlockChain blockChain)
        {
            Console.WriteLine("----------------- Start Blockchain -----------------");
            foreach (Block block in blockChain.Chain)
            {
                Console.WriteLine();
                Console.WriteLine("------ Start Block ------");
                Console.WriteLine($"Hash: {block.Hash}");
                Console.WriteLine($"Previous Hash: {block.PreviousHash}");
                Console.WriteLine("--- Start Transactions ---");
                foreach (Transaction transaction in block.Transaction)
                {
                    Console.WriteLine($"From: {transaction.From} To {transaction.To} Amount {transaction.Amount}");
                }
                Console.WriteLine("--- End Transactions ---");
                Console.WriteLine("------ End Block ------");
            }
            Console.WriteLine("----------------- End Blockchain -----------------");
        }
    }
}
