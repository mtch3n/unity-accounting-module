using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace AccountingModule.Data
{
    [Serializable]
    public class Book
    {
        public List<Journal> JournalArchives = new List<Journal>();

        public byte[] Serialize()
        {
            byte[] bytes;
            IFormatter formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, this);
                bytes = stream.ToArray();
            }

            return bytes;
        }

        public void Write(string path)
        {
            using (var fileStream =
                   new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                using (var bw = new BinaryWriter(fileStream))
                {
                    bw.Write(Serialize());
                }
            }
        }

        public static Book Load(string path)
        {
            if (!File.Exists(path)) return new Book();

            var fs = new FileStream(path, FileMode.Open);
            var formatter = new BinaryFormatter();
            var l = (Book)formatter.Deserialize(fs);

            fs.Close();
            return l;
        }
    }
}