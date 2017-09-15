using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plaswijzer.MessageBuilder.Model
{
    public class Element
    {
        public Element(string title, string image_url, string subtitle, List<IButton> buttons, DefaultAction default_action)
        {
            this.title = title; // required
            this.image_url = image_url; // not required
            this.buttons = buttons; // not required MAX 1 postback
            this.subtitle = subtitle; // not required
            this.default_action = default_action; // not required if you click on the box
        }
        public string title { get; set; }
        public string image_url { get; set; }
        public string subtitle { get; set; }
        public DefaultAction default_action { get; set; }
        public List<IButton> buttons { get; set; }

    }
}