using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plaswijzer.Model;

namespace Plaswijzer.Data
{
    public interface IQueryManager
    {
        List<Toilet> GetNearestToilets(float lon, float lat, int v);
        List<Toilet> GetNearestFreeToilets(float lon, float lat, int count);
        List<GehandToilet> GetNearestGehandToilets(float lon, float lat, int count);
        List<DogToilet> GetNearestDogToilets(float lon, float lat, int count);
        List<Urinoir> GetNearestUriToilets(float lon, float lat, int count);

    }
}
