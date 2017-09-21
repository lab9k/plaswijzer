using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plaswijzer.Models
{
    public interface IToilet
    {
        float Lat
        {
            get;
            set;
        }
        float Lon
        {
            get;
            set;
        }
        string Type_locat
        {
            get;
            set;
        }
        string Situering
        {
            get;
            set;
        }
    }
}
