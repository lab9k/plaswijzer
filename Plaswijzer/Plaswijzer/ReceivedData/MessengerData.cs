﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Plaswijzer.BotData
{


    public class MessengerData
    {
        [JsonProperty(PropertyName = "object")]
        public String Object { get; set; }
        public List<Entry> entry { get; set; }


        public class Sender
        {
            public string id { get; set; }
        }

        public class Recipient
        {
            public string id { get; set; }
        }

        public class Messaging
        {
            public long timestamp { get; set; }
            public Sender sender { get; set; }
            public Recipient recipient { get; set; }
            public Message message { get; set; }

            public class QuickReply
            {
                public string payload { get; set; }
            }

            public class Message
            {
                public string mid { get; set; }
                public string text { get; set; }
                public QuickReply quick_reply { get; set; }
            }
        }

        public class Entry
        {
            public string id { get; set; }
            public long time { get; set; }
            public List<Messaging> messaging { get; set; }
        }
    }
        
}