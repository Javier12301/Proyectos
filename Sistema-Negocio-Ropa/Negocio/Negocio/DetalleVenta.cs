using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Negocio
{
    public class DetalleVenta
    {

        public int DetalleVentaID { get; set; }
        public VentaM oVenta { get; set; }
        public Producto oProducto { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Cantidad { get; set; }
        public decimal Descuento { get; set; }
        public decimal SubTotal { get; set; }


    }
}
