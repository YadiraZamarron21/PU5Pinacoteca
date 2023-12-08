namespace PU5Pinacoteca.Models.ViewModels
{
    public class VerCuadrosPorPintorViewModel
    {
        public string NombrePintor { get; set; } = null!;
        public IEnumerable<CuadroPModel> CuadrosPintor { get; set; } = null!;
    }
    public class CuadroPModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
