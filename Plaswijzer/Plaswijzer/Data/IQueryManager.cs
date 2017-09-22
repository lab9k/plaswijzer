using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plaswijzer.Model;
using Plaswijzer.Models;

namespace Plaswijzer.Data
{
    public interface IQueryManager
    {
        List<IToilet> GetNearestToilets(float lon, float lat, int v);
        List<IToilet> GetNearestFreeToilets(float lon, float lat, int count);
        List<IToilet> GetNearestGehandToilets(float lon, float lat, int count);
        List<IToilet> GetNearestDogToilets(float lon, float lat, int count);
        List<IToilet> GetNearestUriToilets(float lon, float lat, int count);

    }
}
