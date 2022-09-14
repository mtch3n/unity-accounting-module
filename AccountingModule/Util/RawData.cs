using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace AccountingModule.Util
{
    public static class RawData
    {
        public static T Deserialize<T>(byte[] param)
        {
            using (var ms = new MemoryStream(param))
            {
                IFormatter br = new BinaryFormatter();
                return (T)br.Deserialize(ms);
            }
        }
    }
}