using System;
using System.Collections.Generic;

namespace PU5Pinacoteca.Models.Entities;

public partial class Cuadro
{
    public int Id { get; set; }

    public string TituloCuadro { get; set; } = null!;

    public int FechaPintado { get; set; }

    public string Tecnica { get; set; } = null!;

    public string Dimensiones { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int IdColeccion { get; set; }

    public int IdPintor { get; set; }

    public virtual Coleccion IdColeccionNavigation { get; set; } = null!;

    public virtual Pintor IdPintorNavigation { get; set; } = null!;
}
