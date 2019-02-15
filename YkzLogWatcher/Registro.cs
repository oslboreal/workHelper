using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YkzWorkHelper
{
    public class Registro
    {
        public string Nombre { get; set; }
        public string Ruta { get; set; }

        public Registro(string nombre, string ruta)
        {
            this.Nombre = nombre;
            this.Ruta = ruta;
        }
    }
}
