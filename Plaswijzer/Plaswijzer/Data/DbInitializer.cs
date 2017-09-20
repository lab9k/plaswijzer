using Plaswijzer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plaswijzer.Data
{
    public static class DbInitializer
    {

        /// <summary>
        /// Uses ParserKML object his arrays. Those arrays are filled while parsing
        /// </summary>
        /// <param name="context"></param>
        public static void Initialize(ToiletContext context)
        {
            context.Database.EnsureCreated();

            ParserKML parser = new ParserKML();
            var toilets = parser.Toilets;
            var urinoirs = parser.Urinoirs;
            var dogtoilets = parser.Dogtoilets;
            var gehandtoilets = parser.GehandToilets;
            
            foreach (Toilet t in toilets)
            {
                context.Toilets.Add(t);
            }
            context.SaveChanges();
            
            foreach (Urinoir u in urinoirs)
            {
                context.Urinoirs.Add(u);
            }
            context.SaveChanges();

            foreach (GehandToilet g in gehandtoilets)
            {
                context.GehandToilets.Add(g);
            }
            context.SaveChanges();

            foreach (DogToilet d in dogtoilets)
            {
                context.DogToilets.Add(d);
            }
            context.SaveChanges();
        }
    }
}
