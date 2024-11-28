using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Seguridad
{
    public class Permiso
    {
        public int PermisoID { get; set; }
        public Grupo oGrupo { get; set; }
        public Accion oAccion { get; set; }
        public Modulo oModulo { get; set; }
        public bool Estado { get; set; }

        // Permisos Alta, Modificar, Baja
        public bool Alta { get; set; }
        public bool Modificar { get; set; }
        public bool Baja { get; set; }
        public bool Exportar { get; set; }
        public bool Vender { get; set; }
        public bool Comprar { get; set; }
        public bool Permitir_Acceso { get; set; }
    }
}
