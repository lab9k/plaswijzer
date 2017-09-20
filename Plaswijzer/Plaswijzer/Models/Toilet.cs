using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plaswijzer.Model
{
    public enum Type
    {
        Urinoir, Gehand, Basic
    }

    public class Toilet
    {
        public string ID { get; set; }
        public string Situering { get; set; }
        public Boolean Open7op7 { get; set; }
        public string Openuren { get; set; }
        public Boolean Gratis { get; set; }
        public string Type_locat { get; set; }
        public float Lon { get; set; }
        public float Lat { get; set; }
        public Type? Type { get; set; }

        public Toilet()
        {

        }
    }
}
