namespace PU5Pinacoteca.Areas.Admin.Models
{
    public class AdminAgregarCuadroViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public IEnumerable<AdminPintorModel> Pintores { get; set; } = null!;
        public DateOnly FechaPintado { get; set; }
        public string Tecnica { get; set; } = null!;
        public string Dimensiones { get; set; } = null!;
        public IEnumerable<AdminColeccionModel> Colecciones { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public IFormFile? Archivo { get; set; } 
    }
    public class AdminPintorModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

    }
    public class AdminColeccionModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
