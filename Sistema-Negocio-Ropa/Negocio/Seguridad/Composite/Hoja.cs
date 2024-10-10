using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Seguridad.Composite
{
    public class Hoja : Componente
    {
        public Hoja(string Nombre) : base(Nombre)
        {
        }

        // Obtener nombre de hoja
        public string ObtenerNombre()
        {
            return Nombre;
        }

        public override void Agregar(Componente c)
        {
            throw new Exception("No se puede agregar un hijo a una hoja.");
        }

        public override void Eliminar(Componente c)
        {
            throw new Exception("No se puede eliminar un hijo a una hoja.");
        }

        public override List<Componente> ObtenerHijos()
        {
            throw new Exception("No se puede obtener hijos de una hoja.");
        }
    }
}
