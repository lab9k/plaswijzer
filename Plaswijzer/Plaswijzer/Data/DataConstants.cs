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
        private IConfigurationRoot MessagesStore;

        public DataConstants()
        {
            MessengerConfig = Init("messenger.json");
            MessagesStore = Init("messages.json");
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

            var builder = new ConfigurationBuilder()
                               .SetBasePath(Directory.GetCurrentDirectory())
                               .AddJsonFile(json);
            return builder.Build();
        }

        public string GetMessage(string name, string locale)
        {
            return MessagesStore[$"messages:{name}:{locale}"];
        }

    }
}
