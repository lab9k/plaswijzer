using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plaswijzer.MessageBuilder.Model
{
    public class QuickReplies
    {
        public QuickReplies(string content_type)
            {
                this.content_type = content_type;
            }
        public string content_type { get; set; }

    }
}
