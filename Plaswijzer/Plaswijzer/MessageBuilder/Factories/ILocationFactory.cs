using Plaswijzer.MessageBuilder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plaswijzer.MessageBuilder.Factories
{
    interface ILocationFactory
    {
        GenericMessage MakeLocationButton(long id, string type, string lang);
    }
}
