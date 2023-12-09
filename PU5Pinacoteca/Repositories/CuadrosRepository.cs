using Microsoft.EntityFrameworkCore;
using PU5Pinacoteca.Models.Entities;

namespace PU5Pinacoteca.Repositories
{
    public class CuadrosRepository : Repository<Cuadro>
    {
        //INYECTANDO EL REPOSITORIO
        public CuadrosRepository(PinacotecabdContext context) : base(context)
        {
        }
        public override IEnumerable<Cuadro> GetAll()
        {
            return Context.Cuadro.Include(x => x.IdColeccionNavigation).Include(x=>x.IdPintorNavigation).OrderBy(x => x.TituloCuadro);
        }

        
        public IEnumerable<Cuadro> GetCuadroByColeccion(int coleccion)
        {
            return Context.Cuadro.
                Include(x => x.IdColeccionNavigation)
                .Where(x => x.IdColeccion == coleccion).
                OrderBy(x => x.TituloCuadro);
        }
        public IEnumerable<Cuadro> GetCuadroByColeccion(string coleccion)
        {
            return Context.Cuadro.
                Include(x => x.IdColeccionNavigation)
               
                .Where(x => x.IdColeccionNavigation != null && x.IdColeccionNavigation.Nombre == coleccion).
                OrderBy(x => x.TituloCuadro);
        }

        public Cuadro? GetByNombre(string nombre)
        {
            return Context.Cuadro
                .Include(x => x.IdColeccionNavigation)
                .Include(x=>x.IdPintorNavigation)
                .FirstOrDefault(x => x.TituloCuadro == nombre);
        }
    }
}
