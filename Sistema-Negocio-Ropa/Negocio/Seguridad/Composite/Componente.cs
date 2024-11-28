using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Seguridad.Composite
{
    public abstract class Componente
    {
        protected string Nombre { get; set; }

        public Componente(string Nombre)
        {
            this.Nombre = Nombre;
        }

        public abstract void Agregar(Componente c);
        public abstract void Eliminar(Componente c);
        public abstract List<Componente> ObtenerHijos();
    }
}
