using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plaswijzer.BotData;

namespace Plaswijzer.MessengerManager
{
    public class PayloadHandler : IPayloadHandler
    {
        public void handle(MessengerData.Messaging message)
        {
            long id = message.sender.id;
            PayloadData payload = new PayloadData(message.postback.payload);

            switch (payload.Payload)
            {
                case "STARTED":

                    break;
                case "TOILET":

                    break;
                case "FREE_TOILET":

                    break;
                case "WHEELCHAIR":

                    break;
                case "DOG_TOILET":

                    break;
                case "SET_NL":

                    break;
                case "SET_EN":

                    break;
                case "SET_FR":

                    break;
            }
        }
    }
}
