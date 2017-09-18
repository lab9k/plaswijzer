namespace Plaswijzer.MessageBuilder.Model
{
    public class GenericMessage
    {
        /// <summary>
        /// Every message we send is a GenericMessage, and for every GenericMessage you need 2 parts a recipient(who receive it)
        /// and afcourse a message, which has multiple possiblities such as generic template or casual text
        /// </summary>

        public GenericMessage(long id, string text)
        {
            this.recipient = new Recipient(id);
            this.message = new MessageText(text);
        }

        public GenericMessage(long id, Attachment attachment)
        {
            this.recipient = new Recipient(id);
            this.message = new MessageList(attachment);
        }

        public GenericMessage(long id, string text, QuickReplies quick)
        {
            this.recipient = new Recipient(id);
            this.message = new MessageQuick(text, quick);
        }

        public IMessage message { get; set; }
        public Recipient recipient { get; set; }
    }
}
