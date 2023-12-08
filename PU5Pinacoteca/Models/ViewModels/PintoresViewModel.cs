namespace PU5Pinacoteca.Models.ViewModels
{
    public class PintoresViewModel
    {
        public IEnumerable<PaisModel> Paises { get; set; } = null!;
    }
    public class PaisModel
    {
        public string NombrePais { get; set; } = null!;
        public IEnumerable<PintorModel> Pintores { get; set; } = null!;
    }
    public class PintorModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string FechaNacim { get; set; } = null!;
        public string FechaFallec { get; set; } = null!;
    }
}
