using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YkzWorkHelper
{
    public class Filtro
    {
        public string Palabra { get; set; }
        public bool Filtrar { get; set; }

        public Filtro(string palabra, bool filtrar)
        {
            this.Palabra = palabra;
            this.Filtrar = filtrar;
        }
    }
}
