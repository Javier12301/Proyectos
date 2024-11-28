using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Negocio
{
    public class Producto
    {
        public int ProductoID { get; set; }
        public string Nombre { get; set; }
        public Categoria oCategoria { get; set; }
        public decimal PrecioVenta { get; set; }
        // ESTE SOLO SE USA PARA DETALLE COMPRA , NO ES NECESARIO PARA EL PRODUCTO
        public decimal PrecioCompra { get; set; }
        public string Talle { get; set; }
        public string Equipo { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; }
        public bool Estado { get; set; }
    }
}
