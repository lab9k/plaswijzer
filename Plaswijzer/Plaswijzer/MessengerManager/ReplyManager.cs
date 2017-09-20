using Plaswijzer.Client;
using Plaswijzer.Data;
using Plaswijzer.MessageBuilder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plaswijzer.MessengerManager
{
    public class ReplyManager: IReplyManager
    {
        private IMessengerApi api;
        public string lang { get; set; }
        public DataConstants Constants;

        public ReplyManager(IDataConstants Constants)
        {
            this.Constants = (DataConstants)Constants;
            api = this.Constants.GetMessengerApi();
        }

        public void SendWelcomeMessage(long id, string lang)
        {                 
            GenericMessage message = new GenericMessage(id, "Welcome have a reply" /*Constants.GetMessage("Welcome", lang)*/);
            api.SendMessageToUser(message);
        }
        
        public void SendGetLocationButton(long id, string type, string lang)
        {
            List<SimpleQuickReply> lijst = new List<SimpleQuickReply>();
            lijst.Add(new SimpleQuickReply("location"));
            GenericMessage message = new GenericMessage(id, "Choose your location", lijst); 
            api.SendMessageToUser(message);
        }

        public void SendTextMessage(long id, string text)
        {
            GenericMessage message = new GenericMessage(id, text);
            api.SendMessageToUser(message);
        }

    }
}
