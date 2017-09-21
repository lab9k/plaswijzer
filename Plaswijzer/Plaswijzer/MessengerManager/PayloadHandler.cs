using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plaswijzer.BotData;
using Plaswijzer.Data;
using Microsoft.Extensions.Logging;

namespace Plaswijzer.MessengerManager
{
    public class PayloadHandler : IPayloadHandler
    {
        private ReplyManager rmanager;
        private UserTemp user;
        private DataConstants Constants;
        private ILogger<PayloadHandler> _logger;

        public PayloadHandler(ILogger<PayloadHandler> logger,IReplyManager manager, IUserTemp userData, IDataConstants dataConstants)
        {
            _logger = logger;
            rmanager = (ReplyManager)manager;
            user = (UserTemp) userData;
            Constants = (DataConstants) dataConstants;
        }
        
        public void handle(MessengerData.Messaging message)
        {
            long id = message.sender.id;
            PayloadData payload = new PayloadData(message.postback.payload);
            switch (payload.Payload)
            {
                case "GET_TOILET":
                    string[] co = payload.Value.Split(':');
                    //user data contains information about which type of toilet is asked
                    Console.WriteLine("get toilet methode   " + user.GetType(id));
                    rmanager.SendAllToiletsList(id, float.Parse(co[0]), float.Parse(co[1]), user.GetType(id));
                    break;
                case "STARTED":
                    rmanager.SendWelcomeMessage(id, payload.Language);
                    break;
                case "TOILET":
                    user.Add(id, payload.Language, "Basic");
                    Console.WriteLine("gebruiker opgeslaan");
                    rmanager.SendGetLocationButton(id, "ST", payload.Language);
                    break;
                case "FREE_TOILET":
                    user.Add(id, payload.Language, "Free");
                    rmanager.SendGetLocationButton(id, "FT", payload.Language);
                    break;
                case "WHEELCHAIR":
                    user.Add(id, payload.Language, "Gehand");
                    rmanager.SendGetLocationButton(id, "WT", payload.Language);
                    break;
                case "DOG_TOILET":
                    user.Add(id, payload.Language, "Dog");
                    rmanager.SendGetLocationButton(id, "DT", payload.Language);
                    break;
                case "URINOIR":
                    user.Add(id, payload.Language, "Urinoir");
                    rmanager.SendGetLocationButton(id, "UR", payload.Language);
                    break;
                case "SET_LANGUAGE":

                    break;
            }

        }
    }
}
