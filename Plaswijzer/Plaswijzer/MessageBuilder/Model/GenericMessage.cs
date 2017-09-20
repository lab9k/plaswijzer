﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plaswijzer.MessageBuilder.Model
{
    public class GenericMessage
    {
        public GenericMessage(long id, string text, List<SimpleQuickReply> quick_replies)
        {
            this.recipient = new Recipient(id);
            this.message = new MessageQuickReply(text, quick_replies);
        }

        public GenericMessage(long id, string text)
        {
            this.recipient = new Recipient(id);
            this.message = new MessageText(text);
        }
        public GenericMessage(long id, Attachment attachment)
        {
            this.recipient = new Recipient(id);
            this.message = new MessageAttachment(attachment);
        }

        public Recipient recipient { get; set; }
        public IMessage message { get; set; }


    }
}
