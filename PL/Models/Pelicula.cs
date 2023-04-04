namespace PL.Models
{
    public class Pelicula
    {
        public int media_id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public bool favorite { get; set; }
        public string media_type { get; set; }
        public List<object> Peliculas { get; set; }
    }
}
