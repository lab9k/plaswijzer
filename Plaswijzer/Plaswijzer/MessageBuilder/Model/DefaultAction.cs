using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plaswijzer.MessageBuilder.Model
{
    public class DefaultAction
    {
        public DefaultAction(string type, string url)
        {
            this.type = type; // required
            this.url = url; // required
        }
        public string type { get; set; }
        public string url { get; set; }

    }
}
