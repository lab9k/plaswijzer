using Plaswijzer.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;

namespace Plaswijzer.Data
{
    public class ParserKML: IParserKML
    {
        public List<DogToilet> Dogtoilets { get; set; }
        public List<GehandToilet> GehandToilets { get; set; }
        public List<Toilet> Toilets { get; set; }
        public List<Urinoir> Urinoirs { get; set; }

        public ParserKML()
        {
            // Initialise fields
            Dogtoilets = new List<DogToilet>();
            GehandToilets = new List<GehandToilet>();
            Toilets = new List<Toilet>();
            Urinoirs = new List<Urinoir>();

            parseHuman();
            parseDog();
        }

        private void parseHuman()
        {
            // Get the xml document
            var kmlNameSpace = "{http://www.opengis.net/kml/2.2}";
            var publieksanitair = XDocument.Load("https://datatank.stad.gent/4/infrastructuur/publieksanitair.kml");

            // Parse the document
            foreach (var toilet in publieksanitair.Elements().Elements().Elements().Elements($"{kmlNameSpace}Placemark"))
            {
                // Get the fields
                var props = new Dictionary<String, XElement>();

                foreach (var prop in toilet.Element($"{kmlNameSpace}ExtendedData").Element($"{kmlNameSpace}SchemaData").Elements())
                {
                    props[prop.FirstAttribute.Value] = prop;
                }

                var point = toilet.Element($"{kmlNameSpace}Point");
                var lon = float.Parse(point.Element($"{kmlNameSpace}coordinates").Value.Split(",")[0], CultureInfo.InvariantCulture);
                var lat = float.Parse(point.Element($"{kmlNameSpace}coordinates").Value.Split(",")[1], CultureInfo.InvariantCulture);

                // Create the corresponding object
                var type = props["type_sanit"].Value;

                // Create a toilet
                Toilets.Add(new Toilet
                {
                    ID = props["IDGENT"].Value,
                    Lon = lon,
                    Lat = lat,
                    Type = props["type_sanit"].Value,
                    Situering = props["SITUERING"].Value == "" ? "Gent" : props["SITUERING"].Value,
                    Open7op7 = props["open7op7da"].Value == "Ja" ? 1 : 0,
                    Openuren = props["openuren"].Value,
                    Type_locat = props["type_locat"].Value,
                    Gratis = props["prijs_toeg"].Value == "gratis" ? 1 : 0
                });

                // if it's an urinoir aswell, also create urinoir object
                if (type.Contains("urinoir")) {
                    Urinoirs.Add(new Urinoir {
                        ID = props["IDGENT"].Value,
                        Lon = lon,
                        Lat = lat,
                        Situering = props["SITUERING"].Value == "" ? "Gent" : props["SITUERING"].Value,
                        Open7op7 = props["open7op7da"].Value == "Ja" ? 1 : 0,
                        Openuren = props["openuren"].Value,
                        Type_locat = props["type_locat"].Value,
                        Gratis = props["prijs_toeg"].Value == "gratis" ? 1 : 0
                    });
                }

                // if it's a toilet for disabled people also a specific object for it
                if (type.Contains("gehand")) {
                    GehandToilets.Add(new GehandToilet
                    {
                        ID = props["IDGENT"].Value,
                        Lon = lon,
                        Lat = lat,
                        Situering = props["SITUERING"].Value == "" ? "Gent" : props["SITUERING"].Value,
                        Open7op7 = props["open7op7da"].Value == "Ja" ? 1 : 0,
                        Openuren = props["openuren"].Value,
                        Type_locat = props["type_locat"].Value,
                        Gratis = props["prijs_toeg"].Value == "gratis" ? 1 : 0
                    });
                }
            }
        }

        private void parseDog()
        {
            // Get the xml document
            var kmlNameSpace = "{http://www.opengis.net/kml/2.2}";
            var hondensanitair = XDocument.Load("https://datatank.stad.gent/4/infrastructuur/hondenvoorzieningen.kml");

            // Parse the document
            foreach (var hondentoilet in hondensanitair.Elements().Elements().Elements().Elements($"{kmlNameSpace}Placemark"))
            {
                // Get the fields
                var props = new Dictionary<String, XElement>();

                foreach (var prop in hondentoilet.Element($"{kmlNameSpace}ExtendedData").Element($"{kmlNameSpace}SchemaData").Elements())
                {
                    props[prop.FirstAttribute.Value] = prop;
                }

                var point = hondentoilet.Element($"{kmlNameSpace}Point");
                var lon = float.Parse(point.Element($"{kmlNameSpace}coordinates").Value.Split(",")[0], CultureInfo.InvariantCulture);
                var lat = float.Parse(point.Element($"{kmlNameSpace}coordinates").Value.Split(",")[1], CultureInfo.InvariantCulture);

                // Create a toilet (has to have a gent id though)
                if (props.ContainsKey("GentID") ) {
                    Dogtoilets.Add(new DogToilet
                    {
                        ID = props["GentID"].Value,
                        Lon = lon,
                        Lat = lat,
                        Situering = props.ContainsKey("Straat") && props["Straat"].Value != "" ? props["Straat"].Value : "Gent",
                        Type_locat = props.ContainsKey("Plaatsomschrijving") ? props["Plaatsomschrijving"].Value : "Gent"
                    });
                }
            }
        }
    }
}
