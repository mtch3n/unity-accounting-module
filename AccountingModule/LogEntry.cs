using System.IO;

namespace AccountingModule
{
    public class LogEntry
    {
        private readonly string _path;
        public byte[] data;
        public ulong size;

        public LogEntry(string path)
        {
            _path = path;
        }

        public void AppendBinaryFile()
        {
            if (!File.Exists(_path))
                using (var fs = File.Create(_path))
                {
                    fs.Close();
                }

            using (var fileStream = new FileStream(_path, FileMode.Append, FileAccess.Write, FileShare.None))
            using (var bw = new BinaryWriter(fileStream))
            {
                bw.Write(size);
                bw.Write(data);
            }
        }
    }
}