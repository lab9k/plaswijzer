using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plaswijzer.Model
{
    public class Name
    {
        public string nl { get; set; }
    }

    public class FreeToilet: IToilet
    {
        public Name name { get; set; }
        public FreeToilet()
        {
            name = new Name();
        }

    }
}
