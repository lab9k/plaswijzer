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
                    rmanager.SendAllToiletsList(id, float.Parse(co[0]), float.Parse(co[1]), user.GetType(id), user.GetLanguage(id));
                    break;
                case "STARTED":
                    rmanager.SendWelcomeMessage(id, payload.Language);
                    break;
                case "TOILET":
                    if (user.existUser(id))
                    {
                        user.changeType(id, "Basic");
                    }
                    else
                    {
                        user.Add(id, payload.Language, "Basic");
                    }
                    rmanager.SendGetLocationButton(id, "ST", user.GetLanguage(id));
                    break;
                case "FREE_TOILET":
                    if (user.existUser(id))
                    {
                        user.changeType(id, "Free");
                    }
                    else
                    {
                        user.Add(id, payload.Language, "Free");
                    }
                    rmanager.SendGetLocationButton(id, "FT", user.GetLanguage(id));
                    break;
                case "WHEELCHAIR":
                    if (user.existUser(id))
                    {
                        user.changeType(id, "Gehand");
                    }
                    else
                    {
                        user.Add(id, payload.Language, "Gehand");
                    }
                    rmanager.SendGetLocationButton(id, "WT", user.GetLanguage(id));
                    break;
                case "DOG_TOILET":
                    if (user.existUser(id))
                    {
                        user.changeType(id, "Dog");
                    }
                    else
                    {
                        user.Add(id, payload.Language, "Dog");
                    }
                    rmanager.SendGetLocationButton(id, "DT", user.GetLanguage(id));
                    break;
                case "URINOIR":
                    if (user.existUser(id))
                    {
                        user.changeType(id, "Urinoir");
                    }
                    else
                    {
                        user.Add(id, payload.Language, "Urinoir");
                    }
                    rmanager.SendGetLocationButton(id, "UR", user.GetLanguage(id));
                    break;
                case "SET_LANGUAGE":
                    if (user.existUser(id))
                    {
                        user.changeLanguage(id,payload.Value);
                    } else
                    {
                        user.Add(id, payload.Value, "Basic");
                    }
                    rmanager.SendWelcomeMessage(id, payload.Value);
                    break;
            }

        }
    }
}
