using System;
using System.Collections.Generic;

namespace mi_primer_api.Models;

public partial class Empleado
{
    public int IdEmpleados { get; set; }

    public int IdPuestos { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public virtual Puesto IdPuestosNavigation { get; set; } = null!;
}
