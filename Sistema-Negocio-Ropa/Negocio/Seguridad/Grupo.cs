using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Seguridad
{
    public class Grupo
    {
        public int GrupoID { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }

        public string ObtenerNombre()
        {
            return Nombre;
        }
    }
}
