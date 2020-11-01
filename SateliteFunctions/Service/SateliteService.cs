using System;
using System.Collections.Generic;
using System.Linq;
using Extreme.Mathematics;
using FuegoDeQuasar.Context;
using FuegoDeQuasar.Enum;
using FuegoDeQuasar.Models;
using FuegoDeQuasar.Service.Interfaces;

namespace FuegoDeQuasar.Service
{
    public class SateliteService : ISateliteService
    {
        public double[] GetLocation(double[] distancias)
        {
            var p1 = Vector.Create(SatelitesPosicion.KenoviX, SatelitesPosicion.KenoviY);
            var p2 = Vector.Create(SatelitesPosicion.SkywalkerX, SatelitesPosicion.SkywalkerY);
            var p3 = Vector.Create(SatelitesPosicion.SatoX, SatelitesPosicion.SatoY);

            var ex = Vector.Subtract(p2, p1) / Vector.Subtract(p2, p1).Norm();
            var i = ex.DotProduct(p3 - p1);
            var ey = (p3 - p1 - i * ex) / (p3 - p1 - i * ex).Norm();
            var d = (p2 - p1).Norm();
            var j = ey.DotProduct(p3 - p1);

            var x = (Math.Pow(distancias[0], 2) - Math.Pow(distancias[1], 2) + Math.Pow(d, 2)) / (2 * d);
            var y = (Math.Pow(distancias[0], 2) - Math.Pow(distancias[2], 2) + Math.Pow(i, 2) + Math.Pow(j, 2)) / (2 * j) - ((i / j) * x);


            var answer = new[] { Math.Round(x, 1), Math.Round(y, 1) };
            return answer;
        }

        public string GetMessage(List<string[]> mensajes)
        {
            var valoresVacios = new List<(string value, int index)>();
            var valoresNoVacios = new List<(string value, int index)>();

            if (mensajes.Any(mensaje => mensaje.Length != 5)) return string.Empty;
            {

                foreach (var message in mensajes)
                {
                    valoresVacios.AddRange(message.Select((s, i) => (value: s.Replace(" ", string.Empty), index: i)).Where(m =>m.value == "").ToList());
                    valoresNoVacios.AddRange(message.Select((s, i) => (value: s.Replace(" ", string.Empty), index: i)).Where(m => m.value != "").ToList());
                }

                var esMensageSinDeterminar = valoresVacios.GroupBy(x => x).Any(g => g.Count() == mensajes.Count);
                if (esMensageSinDeterminar)
                {
                    return string.Empty;
                }

                var mensajeFinal = valoresNoVacios.OrderBy(valor => valor.index).GroupBy(x => x).Select(g => g.Key.value).ToList();

                return string.Join(" ", mensajeFinal);
            }
        }

        public Satelite GetSatelite(string name)
        {
            return SateliteContext.Satelites().FirstOrDefault(s => s.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}