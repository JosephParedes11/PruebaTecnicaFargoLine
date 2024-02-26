using WebApiContenedores.Models;

namespace WebApiContenedores.Utils
{
    public class Response
    {
        public bool estado { get; set; }
        public string mensaje { get; set; }

    /*    public int codigo { get; set; }*/
        public List<ContenedorModel> Contenedores { get; set; }
        public List<TipoContenedorModel> TiposContenedor { get; set; }
    }
}
