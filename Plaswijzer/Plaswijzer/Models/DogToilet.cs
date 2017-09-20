using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plaswijzer.Model
{
    public class DogToilet
    {
        public string ID { get; set; }
        public string Situering { get; set; }
        public float Lon { get; set; }
        public float Lat { get; set; }

        public DogToilet()
        {

        }
    }
}
