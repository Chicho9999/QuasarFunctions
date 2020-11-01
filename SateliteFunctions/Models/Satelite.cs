namespace FuegoDeQuasar.Models
{
    public class Satelite : Punto
    {
        public string Name { get; set; }

        public double Distance { get; set; }

        public string[] Message { get; set; }

    }
}