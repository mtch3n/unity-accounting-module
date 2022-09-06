using System.IO;

namespace Report
{
    public class LogEntry
    {
        private readonly string _path;
        public byte[] data;
        public ulong size; // 8 Bytes

        public LogEntry(string path)
        {
            _path = path;
        }

        public void AppendBinaryFile()
        {
            if (!File.Exists(_path))
            {
                using (FileStream fs = File.Create(_path))
                {
                    fs.Close();
                }
            }

            using (var fileStream = new FileStream(_path, FileMode.Append, FileAccess.Write, FileShare.None))
            using (var bw = new BinaryWriter(fileStream))
            {
                bw.Write(size);
                bw.Write(data);
            }
        }

        // public byte[] LoadEntry()
        // {
        //     using (var fileStream = new FileStream(_path, FileMode.Open, FileAccess.Read))
        //     using (var br = new BinaryReader(fileStream))
        //     {
        //         data = br.ReadBytes((int)fileStream.Length);
        //     }
        //
        //     return data;
        // }
    }
}