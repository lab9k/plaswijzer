﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plaswijzer.MessageBuilder.Model
{
    public class Recipient
    {
        public Recipient(long id)
        {
            this.id = id;
        }
        public long id { get; set; }
    }
}
