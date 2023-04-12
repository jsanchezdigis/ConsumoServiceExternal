using System;
using System.Collections.Generic;

namespace DL;

public partial class Cine
{
    public int IdCine { get; set; }

    public string? Latitud { get; set; }

    public string? Longitud { get; set; }

    public string? Direccion { get; set; }

    public string? Venta { get; set; }

    public int? IdZona { get; set; }
    //Agregado
    public string Nombre { get; set; }
    public decimal Ventas { get; set; }
    public string Zonas { get; set; }
    public decimal Porcentaje { get; set; }
    public virtual Zona? IdZonaNavigation { get; set; }
}
