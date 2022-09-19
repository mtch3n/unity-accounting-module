using System.IO;
using ConfigModule.Exceptions;
using ConfigModule.Utils;
using Newtonsoft.Json;

namespace ConfigModule
{
    public class Loader
    {
        private static Config _config;

        // private readonly ILogger _logger;

        private readonly string _path;

        public Loader(Option options)
        {
            _path = FindPath(options.Path);
        }

        private string FindPath(string name)
        {
            return Path.GetFullPath(name);
            ;
        }

        private static bool ConfigExists(string p)
        {
            return File.Exists(p);
        }

        public Config GetConfig()
        {
            return _config;
        }

        private Config ReadConfigJson()
        {
            if (!ConfigExists(_path))
                throw new FileNotFoundException("Abort loading config due to config not found in path.");

            using var file = File.OpenText(_path);
            var serializer = new JsonSerializer();
            var cfgJson = (Config)serializer.Deserialize(file, typeof(Config));

            return cfgJson;
        }

        private Config ReadConfigJson(string jsonStr)
        {
            if (jsonStr == null) throw new InvalidDataException("String cannot be null");

            return JsonConvert.DeserializeObject<Config>(jsonStr);
        }

        private void SaveConfigJson()
        {
            if (_config == null) throw new ConfigNotInitializedException("Make sure to load config before save.");

            using var file = File.CreateText(_path);
            var serializer = new JsonSerializer
            {
                Formatting = Formatting.Indented
            };
            serializer.Serialize(file, _config);
        }

        public void Load(string config = null)
        {
            _config = config != null ? ReadConfigJson(config) : ReadConfigJson();
        }

        public void Reload()
        {
            Load();
        }

        public bool Valid(string pwd)
        {
            var c = ReadConfigJson();
            return Hash.GetSha256String(pwd) == c.Info.Password;
        }

        public bool RemoteValid(string pwd)
        {
            var c = ReadConfigJson();
            return Hash.GetSha256String(pwd) == c.Remote.Password;
        }

        public void NewRemotePassword(string pwd)
        {
            _config.Remote.Password = Hash.GetSha256String(pwd);
        }

        public void NewMachinePassword(string pwd)
        {
            _config.Info.Password = Hash.GetSha256String(pwd);
        }

        public void Save()
        {
            SaveConfigJson();
            Load();
        }
    }
}