﻿using System;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;

namespace Blockchain.Model
{
    /// <summary>
    /// Блок данных.
    /// </summary>
    [DataContract]
    public class Block
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int BlockId { get; private set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [DataMember]
        public string UserName { get; private set; }

        /// <summary>
        /// Данные пользователя.
        /// </summary>
        [DataMember]
        public string Data { get; private set; }

        /// <summary>
        /// Дата и время создания.
        /// </summary>
        [DataMember]
        public DateTime Created { get; private set; }

        /// <summary>
        /// Хэш-код блока.
        /// </summary>
        [DataMember]
        public string Hash { get; private set; }

        /// <summary>
        /// Хэш-код предыдущего блока.
        /// </summary>
        [DataMember]
        public string PreviousHash { get; private set; }

        /// <summary>
        /// Конструктор генезис-блока.
        /// </summary>
        public Block()
        {
            UserName = "Admin";
            BlockId = 1;
            Data = "Hello, world!";
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
            BlockId = block.BlockId + 1;
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

        /// <summary>
        /// Сериализация объекта в json-строку.
        /// </summary>
        /// <returns></returns>
        public string Serialize()
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(Block));

            using (var ms = new MemoryStream())
            {
                jsonSerializer.WriteObject(ms, this);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        /// <summary>
        /// Десериализация json-строки в объект.
        /// </summary>
        /// <returns></returns>
        public static Block Deserialize(string json)
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(Block));
            var bytes = Encoding.UTF8.GetBytes(json);

            using (var ms = new MemoryStream(bytes))
            {
                return (Block)jsonSerializer.ReadObject(ms);
            }
        }
    }
}