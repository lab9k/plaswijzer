using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plaswijzer.BotData;

namespace Plaswijzer.MessengerManager
{
    public class PayloadHandler : IPayloadHandler
    {
        private ReplyManager rmanager;
        private TempUserData UserLanguage;
        private DataConstants Constants;
        private RemoteDataManager remote;
        private ILocationFactory locationFactory;
        private ILogger<PayloadHandler> _logger;

        public PayloadHandler(ILogger<PayloadHandler> logger,IReplyManager manager, ITempUserData userData, IDataConstants dataConstants, IRemoteDataManager remoteDataManager, ILocationFactory locationFactory)
        {
            _logger = logger;
            rmanager = (ReplyManager)manager;
            UserLanguage = (TempUserData) userData;
            Constants = (DataConstants) dataConstants;
            remote = (RemoteDataManager) remoteDataManager;
            this.locationFactory = locationFactory;
        }
        
        public void handle(MessengerData.Messaging message)
        {
            long id = message.sender.id;
            PayloadData payload = new PayloadData(message.postback.payload);
        
            switch (payload.Payload)
            {
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
