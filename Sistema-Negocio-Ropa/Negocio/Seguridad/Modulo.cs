using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Seguridad
{
    public class Modulo
    {
        public int ModuloID { get; set; }
        public string Nombre { get; set; }
        public List<Accion> ListaAcciones { get; set; }
    }
}
