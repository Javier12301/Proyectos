using Negocio.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Negocio
{
    public class Compra
    {
        public int CompraID { get; set; }
        public Usuario oUsuario { get; set; }
        public string Tipo_Factura { get; set; }
        public DateTime FechaCompra { get; set; }
        public bool Estado { get; set; }
    }
}
