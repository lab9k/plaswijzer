using Plaswijzer.Data;
using Plaswijzer.MessageBuilder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plaswijzer.MessageBuilder.Factories
{
    public class LocationFactory : ILocationFactory
    {
        private DataConstants Constants;
        public LocationFactory(IDataConstants Constants)
        {
            this.Constants = (DataConstants)Constants;
        }

        public GenericMessage MakeLocationButton(long id, string type, string lang)
        {
            List<QuickReplies> quick_replies = new List<QuickReplies>();
            quick_replies.Add(new QuickReplies("location"));
            // String hmess = Constants.GetMessage("Pick_map", lang);
            return new GenericMessage(id, "geive", quick_replies);
        }
    }
