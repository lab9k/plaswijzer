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
            GenericMessage message = new GenericMessage(id, Constants.GetMessage("Welcome", "NL") );
            api.SendMessageToUser(message); 
        }
        
        public void SendGetLocationButton(long id, string type, string lang)
        {
            List<SimpleQuickReply> lijst = new List<SimpleQuickReply>();
            lijst.Add(new SimpleQuickReply("location"));
            GenericMessage message = new GenericMessage(id, Constants.GetMessage("Location", lang), lijst); 
            api.SendMessageToUser(message);
        }

        public void SendTextMessage(long id, string text)
        {
            GenericMessage message = new GenericMessage(id, text);
            api.SendMessageToUser(message);
        }

        public void SendList(long id, double lat, double lon)
        {
            // calculate nearest 4 or 3
            // steek deze in lijst
            List<string> bestToilets = new List<string>();
            bestToilets.Add("tset");
            bestToilets.Add("tset");
            bestToilets.Add("tset");
            bestToilets.Add("tset");
            api.SendMessageToUser(MakeList(id, bestToilets, lang));
        }

        public GenericMessage MakeList(long id, List<string/*speciale wc klasse*/> bestToilets, string lang)
        {
            
            int index = 0;
            List<Element> elements = new List<Element>();
            foreach(var toilet in bestToilets)
            {
                string url = "https://www.google.com/" /* "maps/@" + toilet.lat + "," + toilet.lon*/;
                string img_url = "https://img12.deviantart.net/65e4/i/2013/003/6/6/png_floating_terrain_by_moonglowlilly-d5qb58m.png";
                List<IButton> buttons = new List<IButton>();
                DefaultAction defaultAction = new DefaultAction("web_url", url);
                buttons.Add(new ButtonUrl("Show me", "web_url", url));
                if (index == 0)
                    {
                        elements.Add(new Element("naam", img_url, "subtitle", buttons, defaultAction));
                } else
                    {
                        elements.Add(new Element("subtitle", null, "subtitle", buttons, defaultAction));
                    }
                index++;
            }
            IPayload payload = new PayloadMessage("list", elements);
            return new GenericMessage(id, new Attachment("template", payload));
        }

    }
}
