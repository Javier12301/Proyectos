using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Negocio
{
    public class DetalleCompra
    {

        public int DetalleCompraID { get; set; }
        public Compra oCompra { get; set; }
        public Producto oProducto { get; set; }
        public int PrecioCompra { get; set; }
        public int Cantidad { get; set; }
        public decimal SubTotal { get; set; }
    }
}
