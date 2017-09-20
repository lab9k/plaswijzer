using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plaswijzer.MessageBuilder.Model
{
    public class PayloadMessage : IPayload
    {
        public PayloadMessage(string template_type, List<Element> elements)
        {
            this.template_type = template_type; // required
            this.elements = elements; // required
        }
        public string template_type { get; set; }
        public List<Element> elements { get; set; }
    }
}
