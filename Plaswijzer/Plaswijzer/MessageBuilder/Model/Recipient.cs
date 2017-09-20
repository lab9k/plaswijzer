namespace Plaswijzer.MessageBuilder.Model
{
    public class Recipient
    {
        public Recipient(long id)
        {
            this.id = id;
        }
        public long id { get; set; }

        public override string ToString()
        {
            return "\"recipient\": { \"id\": " + this.id + "}";
        }
    }
}