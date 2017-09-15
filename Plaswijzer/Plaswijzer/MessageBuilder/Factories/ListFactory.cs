using Plaswijzer.MessageBuilder.Model;
using System.Collections.Generic;
using static Chatbot_GF.BotData.MessengerData;

namespace Plaswijzer.MessageBuilder.Factories
{
    public class ListFactory : IListFactory
    {
        public GenericMessage makeList(long id, List<Toilet> toilets)
        {
            if (toilets.Count > 4)
            {
                toilets = toilets.GetRange(0, 4);
            }

            // kijk als er minder dan 2 zijn


            List<Element> elements = new List<Element>();
            foreach (var toilet in toilets)
            {
                List<IButton> buttons = new List<IButton>(); // only 1 url
                buttons.Add(new ButtonUrl("Show me", "web_url", "www.google.be/maps", true, "tall"));
                DefaultAction defaultAction = new DefaultAction("web_url", "www.google.be/maps", true, "tall"); // tall to full??? and change url

                elements.Add(new Element(toilet.name, null, toilet.subtitle, buttons, defaultAction));
            }
            List<IButton> buttonPayload = new List<IButton>();
            buttonPayload.Add(new ButtonPayload("Back/more", "postback", "???"));
            PayloadList payload = new PayloadList("list", elements, "large", buttonPayload); // large or compact for head image
            return new GenericMessage(id, new Attachment("list", payload));
        }
    }
}
