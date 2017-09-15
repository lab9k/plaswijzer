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


}