using Plaswijzer.Client;
using Plaswijzer.Data;
using Plaswijzer.MessageBuilder.Model;
using Plaswijzer.Model;
using Plaswijzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plaswijzer.MessengerManager
{
    public class ReplyManager: IReplyManager
    {
        const int AANTAL = 3; //indicates number of toilets to be showed when nearest
        private IMessengerApi api;
        public string lang { get; set; }
        public DataConstants Constants;
        private IQueryManager qm;

        public ReplyManager(IDataConstants Constants, IQueryManager qm)
        {
            this.Constants = (DataConstants)Constants;
            this.qm = qm;
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

        public void SendAllToiletsList(long id, float lon, float lat, string type)
        {
            Console.WriteLine("in send all toilets");
            List<IToilet> toilets = new List<IToilet>();
            switch (type)
            {
                case "Basic":
                    Console.WriteLine("basic");
                    toilets = qm.GetNearestToilets(lon,lat,AANTAL);
                    break;
                case "Free":
                   toilets = qm.GetNearestFreeToilets(lon, lat, AANTAL);
                    break;
                case "Gehand":
                   toilets = qm.GetNearestGehandToilets(lon, lat, AANTAL);
                    break;
                case "Dog":
                   toilets = qm.GetNearestDogToilets(lon, lat, AANTAL);
                    break;
                case "Urinoir":
                   toilets = qm.GetNearestUriToilets(lon, lat, AANTAL);
                    break;
            }
            api.SendMessageToUser(MakeListAllSorts(id, toilets, lon, lat, lang));
        }

        public GenericMessage MakeListAllSorts(long id, List<IToilet> bestToilets, float lon, float lat, string lang)
        {
            int index = 0;
            List<Element> elements = new List<Element>();
            foreach(var toilet in bestToilets)
            {
                string url = $"https://www.google.com/maps/dir/{toilet.Lat},{toilet.Lon}/{lat},{lon}";
                string detailsurl = $"https://plaswijzer.lab9k.gent/Toilets/Details?id={toilet.ID}";
                string img_url = "https://img12.deviantart.net/65e4/i/2013/003/6/6/png_floating_terrain_by_moonglowlilly-d5qb58m.png";
                List<IButton> buttons = new List<IButton>();
                DefaultAction defaultAction = new DefaultAction("web_url", url);
                buttons.Add(new ButtonUrl("Show me", "web_url", detailsurl));
                if (index == 0)
                    {
                        elements.Add(new Element(toilet.Situering, img_url, toilet.Type_locat, buttons, defaultAction));
                } else
                    {
                        elements.Add(new Element(toilet.Situering, null, toilet.Type_locat, buttons, defaultAction));
                    }
                index++;
            }
            IPayload payload = new PayloadMessage("list", elements);
            return new GenericMessage(id, new Attachment("template", payload));
        }

    }
}
