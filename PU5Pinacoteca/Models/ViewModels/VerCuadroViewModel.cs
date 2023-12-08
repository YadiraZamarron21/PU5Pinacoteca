namespace PU5Pinacoteca.Models.ViewModels
{
    public class VerCuadroViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string Pintor { get; set; } = null!;
        public int FechaPintado { get; set; }
        public string Tecnica { get; set; } = null!;
        public string Dimensiones { get; set; } = null!;
        public string Coleccion { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

    }
}