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
        private UserTemp UserLanguage;
        private DataConstants Constants;
        private ILogger<PayloadHandler> _logger;

        public PayloadHandler(ILogger<PayloadHandler> logger,IReplyManager manager, IUserTemp userData, IDataConstants dataConstants)
        {
            _logger = logger;
            rmanager = (ReplyManager)manager;
            UserLanguage = (UserTemp) userData;
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
                    rmanager.SendAllToiletsList(id, float.Parse(co[0]),  float.Parse(co[1]));
                    break;
                case "STARTED":
                    rmanager.SendWelcomeMessage(id, payload.Language);
                    break;
                case "TOILET":
                    rmanager.SendGetLocationButton(id, "ST", payload.Language);
                    break;
                case "FREE_TOILET":
                    rmanager.SendGetLocationButton(id, "FT", payload.Language);
                    break;
                case "WHEELCHAIR":
                    rmanager.SendGetLocationButton(id, "WT", payload.Language);
                    break;
                case "DOG_TOILET":
                    rmanager.SendGetLocationButton(id, "DT", payload.Language);
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
