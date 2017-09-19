using System.Collections.Generic;

namespace Plaswijzer.MessageBuilder.Model
{
    public interface IMessage
    {
    }

    // list view
    public class MessageList : IMessage
    {
        public MessageList(Attachment attachment)
        {
            this.attachment = attachment;
        }

        public Attachment attachment { get; set; }
    }

    // simple text
    public class MessageText : IMessage
    {
        public MessageText(string text)
        {
            this.text = text;
        }
        public string text { get; set; }
    }

    // quickreplies
    public class MessageQuick : IMessage
    {
        public MessageQuick(string text, List<QuickReplies> quick)
        {
            this.text = text;
            this.quick = quick;
        }
        public List<QuickReplies> quick { get; set; }
        public string text { get; set; }

    }

}
