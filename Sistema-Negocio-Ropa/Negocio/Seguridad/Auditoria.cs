using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Seguridad
{
    public class Auditoria
    {
        public int AuditoriaID { get; set; }
        public DateTime FechayHora { get; set; }
        public string Movimiento { get; set; }
        public string Modulo { get; set; }
        public string Usuario { get; set; }
        public string Descripcion { get; set; }
    
        public Auditoria(string movimiento, string usuario, string modulo, string descripcion)
        {
            Movimiento = movimiento;
            Usuario = usuario;
            Modulo = modulo;
            Descripcion = descripcion;
        }
    
    }

    

}
