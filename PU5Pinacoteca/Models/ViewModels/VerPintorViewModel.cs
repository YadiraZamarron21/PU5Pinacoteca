namespace PU5Pinacoteca.Models.ViewModels
{
    public class VerPintorViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Pais { get; set; } = null!;
        public string Ciudad { get; set; } = null!;
        public string FechaNacimiento { get; set; } = null!;
        public string FechaFallecimiento { get; set; } = null!;
        public string Biografia { get; set; } = null!;


    }
}