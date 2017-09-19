﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Plaswijzer.Client;
using RestEase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Plaswijzer.Data
{
    public class DataConstants: IDataConstants
    {
        private IConfigurationRoot MessengerConfig;
        private ILogger<DataConstants> _logger;

        public DataConstants(ILogger<DataConstants> logger)
        {
            MessengerConfig = Init("messenger.json");
            _logger = logger;
        }

        public IMessengerApi GetMessengerApi()
        {
            return RestClient.For<IMessengerApi>(GetMessengerConfig("apiUrl", "development"));
        }

        public string GetMessengerConfig(string type, string name)
        {
            return MessengerConfig[$"messenger:{type}:{name}"];
        }

        private IConfigurationRoot Init(string json)
        {
            _logger.LogInformation(this.GetType().ToString() + $"Loading {json}");

            var builder = new ConfigurationBuilder()
                               .SetBasePath(Directory.GetCurrentDirectory())
                               .AddJsonFile(json);
            return builder.Build();
        }

    }
}
