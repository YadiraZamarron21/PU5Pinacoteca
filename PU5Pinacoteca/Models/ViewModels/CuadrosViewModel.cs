namespace PU5Pinacoteca.Models.ViewModels
{
    public class CuadrosViewModel
    {
        public IEnumerable<ColeccionModel> Colecciones { get; set; } = null!;
    }
    public class ColeccionModel
    {
        public string Clasificacion { get; set; } = null!;
        public IEnumerable<CuadroModel> Cuadros { get; set; } = null!;
    }
    public class CuadroModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string NombrePintor { get; set; } = null!;
    }
}
