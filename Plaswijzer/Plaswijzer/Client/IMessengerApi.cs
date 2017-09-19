using Plaswijzer.MessageBuilder.Model;
using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Plaswijzer.Client
{
    public interface IMessengerApi
    {
        [Post]
        Task<String> SendMessageToUser([Body] GenericMessage message);
    }
}
