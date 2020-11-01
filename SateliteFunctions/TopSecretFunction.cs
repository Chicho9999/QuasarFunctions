using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FuegoDeQuasar.Models;
using FuegoDeQuasar.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FuegoDeQuasarFunctions
{
    public class TopSecretFunction
    {
        private readonly ISateliteService _sateliteService;

        /// <summary>
        /// Inyeccion de dependencia del servicio Satelite
        /// </summary>
        /// <param name="sateliteService"></param>
        public TopSecretFunction(ISateliteService sateliteService)
        {
            _sateliteService = sateliteService;
        }

        [FunctionName("topsecret")]
        public async Task<IActionResult> TopSecret([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log)
        {

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var satelites = JsonConvert.DeserializeObject<List<Satelite>>(requestBody);

            var distancias = satelites.Select(satelite => satelite.Distance).ToArray();
            var mensajes = satelites.Select(satelite => satelite.Message).ToList();

            if (distancias.Length != 3 || distancias.Any(d1 => d1 < 0))
            {
                return new NotFoundResult(); ;
            }

            var position = _sateliteService.GetLocation(distancias);

            var mensajeDeterminado = _sateliteService.GetMessage(mensajes);

            if (mensajeDeterminado == string.Empty) return new NotFoundResult();

            return new OkObjectResult(new
            {
                x = position[0],
                y = position[1],
                mensaje = mensajeDeterminado
            });
        }

        [FunctionName("topsecret_split")]
        public IActionResult TopSecretSplit([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            string name = req.Query["name"];

            var satelite = _sateliteService.GetSatelite(name);

            if (satelite == null) return new NotFoundResult();

            if(req.Method.Equals("Post", StringComparison.InvariantCultureIgnoreCase)) return new OkObjectResult(new
            {
                distance = satelite.Distance,
                message = satelite.Message
            });

            return new OkObjectResult(new
            {
                x = satelite.X,
                y = satelite.Y,
                mensaje = satelite.Message
            });
        }
    }
}