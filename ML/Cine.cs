using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Cine
    {
        public int IdCine { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string Direccion { get; set; }
        public string Venta { get; set; }
        public decimal Porcentaje { get; set; }
        public Zona Zona { get; set; }
        public List<object> Cines { get; set; }
        public List<object> Direcciones { get; set; }
    }
}
