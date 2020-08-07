using System.Collections.Generic;

namespace Blockchain
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
        /// Количество блоков.
        /// </summary>
        public int Count => Blocks.Count;


        /// <summary>
        /// Создание новой цепочки.
        /// </summary>
        public Chain()
        {
            Blocks = new List<Block>();

            var genesisBlock = new Block();
            Blocks.Add(genesisBlock);
            Last = genesisBlock;
        }

        /// <summary>
        /// Добавление блока.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userName"></param>
        public void Add(string data, string userName)
        {
            var block = new Block(data, userName, Last);
            Blocks.Add(block);
            Last = block;
        }
    }
}