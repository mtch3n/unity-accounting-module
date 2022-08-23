using System;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FisherConfig
{
    public class Loader
    {
        private static Config config;

        private readonly ILogger<Loader> _logger;

        private string Path { get; set; }

        public Loader(string path, ILogger<Loader> logger = null)
        {
            Path = path;
            _logger = logger;
        }

        public Config GetConfig()
        {
            // return
            return new Config();
        }

        private Config Deserialize()
        {
            throw new NotImplementedException();
        }

        private string Read(string path)
        {
            throw new NotImplementedException();
        }

        private void Write(string path)
        {
            throw new NotImplementedException();
        }

        private string Serialize()
        {
            var json = JsonConvert.SerializeObject(new Config(), Formatting.Indented);


            Console.Write(json);

            return json;
        }

        public void Load(string path)
        {
            var loader = new Loader(path);

            // return loader.GetConfig();
        }

        public static bool CheckPassword(string pwd)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void SaveAndReload()
        {
            throw new NotImplementedException();
        }
    }
}