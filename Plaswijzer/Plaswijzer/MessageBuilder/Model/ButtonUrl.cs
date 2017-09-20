using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plaswijzer.MessageBuilder.Model
{
    public class ButtonUrl : IButton
    {
        public ButtonUrl(string title, string type, string url)
        {
            this.type = type; // required
            this.url = url; // required for web_url type
            this.title = title;  // required
        }
        public string type { get; set; }
        public string url { get; set; }
        public string title { get; set; }
    }
}
