﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plaswijzer.MessageBuilder.Model
{
    public interface IMessage
    {       
    }

    public class MessageAttachment :IMessage
    {
        public MessageAttachment(Attachment attachment)
        {
            // for a carousel message
            this.attachment = attachment;
        }
        public Attachment attachment { get; set; }
    }

    public class MessageText : IMessage
    {
        public MessageText(string text)
        {
            // message with only text
            this.text = text;
        }
        public string text { get; set; }
    }

    public class MessageQuickReply : IMessage
    {
        public MessageQuickReply(string text, List<SimpleQuickReply> quick_replies)
        {
            // message with text and buttons

            this.text = text;
            this.quick_replies = quick_replies;
        }
        public string text { get; set; }
        public List<SimpleQuickReply> quick_replies { get; set; }
    }
}
