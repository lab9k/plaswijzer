using System.Collections.Generic;

namespace Plaswijzer.MessageBuilder.Model
{
    public class PayloadList
    {
        public PayloadList(string template_type, List<Element> elements, List<IButton> buttons)
        {
            this.template_type = template_type; // required
            this.elements = elements; // required MAX 4 MIN 2 
            this.buttons = buttons; // not required max 1
        }

        public string template_type { get; set; }
        public List<Element> elements { get; set; }
        public List<IButton> buttons { get; set; }

    }
}
