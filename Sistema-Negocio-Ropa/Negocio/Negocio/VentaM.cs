using Negocio.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Negocio
{
    public class VentaM
    {
        public int VentaID { get; set; }
        public CajaM oCaja { get; set; }
        public Usuario oUsuario { get; set; }
        public string TipoComprobante { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal MontoCambio { get; set; }
        public decimal Recargo { get; set; }
        public MetodoPago oMetodoPago { get; set; }
        public decimal MontoTotal { get; set; }
        public DateTime Fecha { get; set; }
        public bool Estado { get; set; }
    }
}
