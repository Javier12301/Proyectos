using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Seguridad.Composite
{
    public class Composite : Componente
    {
        List<Componente> hijo = new List<Componente>();

        public Composite(string Nombre) : base(Nombre)
        {
        }

        // Obtener nombre
        public string ObtenerNombre()
        {
            return Nombre;
        }

        public override void Agregar(Componente c)
        {
            hijo.Add(c);
        }

        public override void Eliminar(Componente c)
        {
            hijo.Remove(c);
        }

        public override List<Componente> ObtenerHijos()
        {
            return hijo;
        }
    }
}
