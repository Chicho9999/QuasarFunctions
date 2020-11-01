using System.Collections.Generic;
using FuegoDeQuasar.Models;

namespace FuegoDeQuasar.Service.Interfaces
{
    public interface ISateliteService
    {
        double[] GetLocation(double[] distances);

        string GetMessage(List<string[]> messages);

        Satelite GetSatelite(string name);
    }
}
