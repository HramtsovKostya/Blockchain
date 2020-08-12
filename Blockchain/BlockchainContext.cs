using Blockchain.Model;
using System.Data.Entity;

namespace Blockchain
{
    public class BChContext : DbContext
    {
        public BChContext() : base("BlockchainConnection") { }

        public DbSet<Block> Blocks { get; set; }
    }
}