using System;
using System.Collections.Generic;
using System.Linq;

namespace Blockchain.Model
{
    /// <summary>
    /// Цепочка блоков.
    /// </summary>
    public class Chain
    {
        /// <summary>
        /// Список всех блоков.
        /// </summary>
        public List<Block> Blocks { get; private set; }
        
        /// <summary>
        /// Последний блок.
        /// </summary>
        public Block Last { get; private set; }

        /// <summary>
        /// Создание новой цепочки.
        /// </summary>
        public Chain()
        {
            Blocks = LoadChainFromDataBase();

            if (Blocks.Count == 0)
            {
                var genesisBlock = new Block();
                AddBlockToChain(genesisBlock);                
            }
            else
            {
                if (Check()) Last = Blocks.Last();
                else
                {
                    throw new Exception("Ошибка получения блоков из базы данных." +
                    " Цепочка не прошла проверку на целостность!");
                }
            }            
        }        

        /// <summary>
        /// Добавление блока.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userName"></param>
        public void Add(string data, string userName)
        {
            var block = new Block(data, userName, Last);
            AddBlockToChain(block);            
        }

        /// <summary>
        /// Добавление блока в цепочку.
        /// </summary>
        /// <param name="newBlock"></param>
        private void AddBlockToChain(Block newBlock)
        {
            Blocks.Add(newBlock);
            Last = newBlock;
            SaveBlockToDataBase(Last);
        }

        /// <summary>
        /// Проверка корректности цепочки.
        /// </summary>
        /// <returns>
        /// True - цепочка корректна, false - цепочка не корректна.
        /// </returns>
        public bool Check()
        {
            var genesisBlock = new Block();
            var previousHash = genesisBlock.Hash;

            foreach (var block in Blocks.Skip(1))
            {
                var hash = block.PreviousHash;

                if (previousHash != hash) return false;
                previousHash = block.Hash;
            }

            return true;
        }

        /// <summary>
        /// Сохранение блока в базу данных.
        /// </summary>
        /// <param name="block"></param>
        private void SaveBlockToDataBase(Block block)
        {
            using (var db = new BChContext())
            {
                db.Blocks.Add(block);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Получение данных из базы данных.
        /// </summary>
        /// <returns></returns>
        private List<Block> LoadChainFromDataBase()
        {
            List<Block> result;

            using (var db = new BChContext())
            {
                var count = db.Blocks.Count();
                result = new List<Block>(count * 2);               
                result.AddRange(db.Blocks);
            }

            return result;
        }       
    }
}