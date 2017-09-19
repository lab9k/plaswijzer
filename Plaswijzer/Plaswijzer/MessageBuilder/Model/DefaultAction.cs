using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plaswijzer.MessageBuilder.Model
{
    public class DefaultAction
    {
        public DefaultAction(string type, string url, bool messenger_extensions, string webview_height_ratio)
        {
            this.type = type; // required
            this.url = url; // required
            this.messenger_extensions = messenger_extensions; // not required
            this.webview_height_ratio = webview_height_ratio;
        }

        public string type { get; set; }
        public string url { get; set; }
        public bool messenger_extensions { get; set; }
        public string webview_height_ratio { get; set; }
    }
}
