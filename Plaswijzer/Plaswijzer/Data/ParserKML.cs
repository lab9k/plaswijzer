using Plaswijzer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plaswijzer.Data
{
    public class ParserKML: IParserKML
    {
        public DogToilet[] Dogtoilets { get; set; }
        public GehandToilet[] GehandToilets { get; set; }
        public Toilet[] Toilets { get; set; }
        public Urinoir[] Urinoirs { get; set; }

        public ParserKML()
        {
            //hier de private methodes oproepen om het parsen te maken
            //de methodes moeten zorgen dat je arrays gevuld worden
            OpenFile();
            ReadInformationDogToilets();
            ReadInformationDogToilets();

            Toilets = new Toilet[]
                {
            new Toilet{ID="PS_33",Lon=3.710278911258f, Lat=51.0363375047756f, Type="Basic",Situering="Sint-Pietersstation",Open7op7=1, Openuren="4u - 1u",Type_locat="stations NMBS",Gratis=1 }
               };
        }

        private void OpenFile()
        {
            //open bestand
        }

        private void ReadInformationToilets()
        {

        }

        private void ReadInformationDogToilets() { }


    }
}
