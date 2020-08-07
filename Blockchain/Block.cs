using System;
using System.Security.Cryptography;
using System.Text;

namespace Blockchain
{
    /// <summary>
    /// Блок данных.
    /// </summary>
    public class Block
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int UserId { get; private set; }

        /// <summary>
        /// Данные пользователя.
        /// </summary>
        public string Data { get; private set; }

        /// <summary>
        /// Дата и время создания.
        /// </summary>
        public DateTime Created { get; private set; }

        /// <summary>
        /// Хэш-код блока.
        /// </summary>
        public string Hash { get; private set; }

        /// <summary>
        /// Хэш-код предыдущего блока.
        /// </summary>
        public string PreviousHash { get; private set; }

        /// <summary>
        /// Конструктор генезис-блока.
        /// </summary>
        public Block()
        {
            UserName = "Admin";
            UserId = 1;
            Data = "Hello, world";
            Created = DateTime.Parse("01.09.2018 00:00:00.000");
            PreviousHash = "111111";
            Hash = GetHash();
        }

        /// <summary>
        /// Конструктор блока.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userName"></param>
        /// <param name="block"></param>
        public Block(string data, string userName, Block block)
        {
            if (string.IsNullOrWhiteSpace(data))
                throw new ArgumentNullException("Пустой аргумент data", nameof(data));
            
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("Пустой аргумент userName", nameof(userName));

            if (block == null)
                throw new ArgumentNullException("пустой аргумент block", nameof(block));

            Data = data;
            UserName = userName;
            PreviousHash = block.Hash;
            UserId = block.UserId + 1;
            Created = DateTime.UtcNow;           
            Hash = GetHash();
        }

        /// <summary>
        /// Получение значимых данных.
        /// </summary>
        /// <returns></returns>
        private string GetData()
        {
            var result = "";

            result += UserName;
            result += UserId.ToString();
            result += Data;
            result += Created.ToString("dd.MM.yyyy HH:mm:ss.fff");
            result += PreviousHash;

            return result;
        }

        /// <summary>
        /// Хеширование данных.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string GetHash()
        {
            var data = GetData();
            var message = Encoding.ASCII.GetBytes(data);
            var hashString = new SHA256Managed();
            var hashValue = hashString.ComputeHash(message);
            var hex = "";

            foreach (var x in hashValue)
                hex += string.Format("{0:x2}", x);

            return hex;
        }

        public override string ToString() => Data;
    }
}