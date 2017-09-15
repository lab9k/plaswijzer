namespace Plaswijzer.MessageBuilder.Model
{
    public class Attachment
    {
        public Attachment(string type, PayloadList payload)
        {
            this.type = type;
            this.payload = payload;
        }
        public string type { get; set; }
        public PayloadList payload { get; set; }

    }
}