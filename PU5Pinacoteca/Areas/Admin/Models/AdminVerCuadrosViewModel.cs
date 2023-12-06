namespace PU5Pinacoteca.Areas.Admin.Models
{
    public class AdminVerCuadrosViewModel
    {
        public IEnumerable<AdminVerColeccionModel> Colecciones { get; set; } = null!;
    }
    public class AdminVerColeccionModel
    {
        public string Clasificacion { get; set; } = null!;
        public IEnumerable<AdminVerCuadroModel> Cuadros { get; set; } = null!;
    }
    public class AdminVerCuadroModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string NombrePintor {  get; set; } = null!;
    }
    
}
