namespace NetCoreLinqToSqlInjection.Models
{
    public class Deportivo : ICoche
    {

        public Deportivo()
        {
            this.Marca = "BMW";
            this.Modelo = "i35";
            this.Imagen = "bmw.jpg";
            this.Velocidad = 0;
            this.VelocidadMaxima = 302;
        }

        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Imagen { get; set; }
        public int Velocidad { get; set; }
        public int VelocidadMaxima { get; set; }

        public void Acelerar()
        {
            this.Velocidad += 10;
            if (this.Velocidad >= this.VelocidadMaxima)
            {
                this.Velocidad = this.VelocidadMaxima;
            }
        }

        public void Frenar()
        {
            this.Velocidad -= 10;
            if (this.Velocidad < 0)
            {
                this.Velocidad = 0;
            }
        }
    }
}
