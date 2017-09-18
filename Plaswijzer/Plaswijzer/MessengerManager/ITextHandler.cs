using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plaswijzer.MessengerManager
{
    public interface ITextHandler
    {
        void CheckText(long id, string text);
        string GetResponse(string text);
        string GetPayload(string text);
    }
}
