using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Plaswijzer.BotData.MessengerData;

namespace Plaswijzer.MessengerManager
{
    public interface IPayloadHandler
    {
        void handle(Messaging message);
    }
}
