using System;
using System.Collections.Generic;
using System.Linq;
using FuegoDeQuasar.Models;

namespace FuegoDeQuasar.Context
{
    /// <summary>
    /// Mockeo de un Contexto de Base de Datos
    /// </summary>
    public static class SateliteContext
    {
        public static List<Satelite> Satelites()
        {
            return new List<Satelite>
            {
                new Satelite()
                {
                    Name = "kenobi",
                    Distance = 100.0,
                    Message = new string[] {"este", "", "", "mensaje", ""},
                    X = -500,
                    Y = -200

                },
                new Satelite()
                {
                    Name = "skywalker",
                    Distance = 115.5,
                    Message = new string[] {"", "es", "", "", "secreto"},
                    X = 100,
                    Y = -100

                },
                new Satelite()
                {
                    Name = "sato",
                    Distance = 142.7,
                    Message = new string[] {"este", "", "un", "", ""},
                    X = 500,
                    Y = 100
                },
            };
        }
    }
}
