using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plaswijzer.BotData;
using static Plaswijzer.BotData.MessengerData;

namespace Plaswijzer.MessengerManager
{
    public class MessageHandler : IMessageHandler
    {
        private IReplyManager replyManager;
        private ITextHandler textHandler;
        public string Language_choice { get; set; }

        public MessageHandler(IReplyManager manager, ITextHandler textHandler)
        {
            replyManager = (ReplyManager)manager;
            this.textHandler = textHandler;
        }

        /// <summary>
        /// Looking if it is a normal text message and giving it to the RandomTextHandler. Check-up after checking for payload
        /// </summary>
        /// <param name="message"></param>
        public void CheckForKnowText(Messaging message)
        {
            if (!string.IsNullOrWhiteSpace(message?.message?.text))
            {
                string txt = message.message.text;
                textHandler.CheckText(message.sender.id, txt);
            }
        }


        /// <summary>
        /// If messages corresponds to any of the already defined payloads, set the payload
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Messaging MessageRecognized(Messaging message)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(message?.postback?.payload) && !string.IsNullOrWhiteSpace(message?.message?.text))
                {
                    Console.WriteLine("Zoeken naar payload");
                    string response = textHandler.GetPayload(message.message.text);
                    if (response != null)
                    {
                        SetPayload(message, response);
                    }
                }
                return message;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public void SetPayload(Messaging msg, String pl)
        {
            msg.postback = new Postback { payload = pl };
        }


    }
}
