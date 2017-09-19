using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Plaswijzer.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Plaswijzer.MessengerManager
{
    public class RandomTextHandler : ITextHandler
    {
        private IConfiguration ReplyStore;
        private ReplyManager RMmanager;
        private DataConstants Constants;
        private ILogger<RandomTextHandler> _logger;


        public RandomTextHandler(ILogger<RandomTextHandler> logger, IReplyManager rmanager, IDataConstants Constants)
        {
            _logger = logger;
            RMmanager = (ReplyManager)rmanager;
            this.Constants = (DataConstants)Constants;
            InitReplies();
        }

        private void InitReplies()
        {
            _logger.LogInformation(this.GetType().ToString() + "Loading replies");
            var builder = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("replies.json");
            ReplyStore = builder.Build();
        }
            

        public string GetPayload(string text)
        {
            try
            {
                return ReplyStore["payloads:" + text + ":payload"];
            }
            catch (Exception ex)
            {
                _logger.LogWarning(101, ex, "Exception while searching for keyword");
                return null;
            }
        }

     

        public static bool IsEqual(string first, string second, int tollerance)
        {
            return string.Compare(first, second) < tollerance;
        }

    }
}
