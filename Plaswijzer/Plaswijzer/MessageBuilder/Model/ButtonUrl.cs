﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plaswijzer.MessageBuilder.Model
{
    public class ButtonUrl : IButton
    {
        public ButtonUrl(string title, string type, string url, bool messenger_extensions, string webview_height_ratio)
        {
            this.type = type; // required
            this.url = url; // required for web_url type
            this.messenger_extensions = messenger_extensions; // not required
            this.title = title;  // required
            this.webview_height_ratio = webview_height_ratio; // not required
        }
        public string type { get; set; }
        public bool messenger_extensions { get; set; }
        public string url { get; set; }
        public string title { get; set; }
        public string webview_height_ratio { get; set; }
    }
}
}
