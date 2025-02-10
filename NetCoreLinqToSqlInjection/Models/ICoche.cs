namespace NetCoreLinqToSqlInjection.Models
{
    public interface ICoche
    {
        //LAS INTERFCAES NO TIENEN AMBITO NI CODIGO EN SUS PROPIEDADES/METODOS
        //SON DECLARACIONES
        string Marca { get; set; }
        string Modelo { get; set; }
        string Imagen { get; set; }
        int Velocidad { get; set; }
        int VelocidadMaxima { get; set; }
        void Acelerar();
        void Frenar();
    }
}
