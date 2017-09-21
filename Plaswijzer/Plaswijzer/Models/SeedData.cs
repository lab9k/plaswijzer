using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Plaswijzer.Data;
using Plaswijzer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plaswijzer.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using(var context = new ToiletContext(
                serviceProvider.GetRequiredService<DbContextOptions<ToiletContext>>()))
            {
                if (context.Toilets.Any())
                {
                    return; //DB has already been seeded
                }
                ParserKML parser = new ParserKML();
                var toilets = parser.Toilets;
                var urinoirs = parser.Urinoirs;
                var dogtoilets = parser.Dogtoilets;
                var gehandtoilets = parser.GehandToilets;

                foreach (Toilet t in toilets)
                {
                    Console.WriteLine("hier toevoegen");
                    context.Toilets.Add(t);
                }
                context.SaveChanges();
                /*
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
                */
            }
        
        }
    }
}
