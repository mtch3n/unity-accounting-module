using System;
using System.IO;
using FisherConfig.Exceptions;
using FisherConfig.Utils;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;

namespace FisherConfig
{
    public class Loader
    {
        private static Config _config;

        private readonly ILogger<Loader> _logger;

        private readonly string _path;

        public Loader(string path, ILogger<Loader> logger = null)
        {
            _logger = logger ?? NullLogger();
            _path = FindPath(path);
        }

        private ILogger<Loader> NullLogger()
        {
            return new NullLogger<Loader>();
        }

        private string FindPath(string name)
        {
            var p = Path.GetFullPath(name);

            _logger.LogDebug("file exists status: {Exists}", ConfigExists(p));

            return p;
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

            var jsonStr = JsonConvert.SerializeObject(_config, Formatting.Indented);
            _logger.LogDebug("writing config:\n {Json}", jsonStr);


            using var file = File.CreateText(_path);
            var serializer = new JsonSerializer();
            serializer.Serialize(file, _config);
        }

        private void Validation()
        {
            throw new NotImplementedException();
        }

        public void Load(string config = null)
        {
            _logger.LogDebug("loading config file from path: {Path}", _path);
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