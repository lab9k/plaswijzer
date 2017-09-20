using Plaswijzer.Client;
using Plaswijzer.Data;
using Plaswijzer.MessageBuilder.Factories;
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
        private ILocationFactory locationFactory;
    
    public void SendWelcomeMessage(long id, string lang)
        {                 
            GenericMessage message = new GenericMessage(id, "Welcome have a reply" /*Constants.GetMessage("Welcome", lang)*/);
            Console.WriteLine(message);
            api.SendMessageToUser(message);
        }
        
     public void SendGetLocationButton(long id, string type, string lang)
        {
            GenericMessage message = locationFactory.MakeLocationButton(id, type, lang);
            api.SendMessageToUser(message);

        }
        
    }
}
