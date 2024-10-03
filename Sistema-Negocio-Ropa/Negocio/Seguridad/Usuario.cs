using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Seguridad
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        // datos de usuario
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public bool Estado { get; set; }


        // Definición de grupos
        public Grupo oGrupo { get; set; }
        public List<Modulo> ModulosPermitidos { get; set; }

        public int ObtenerGrupoID()
        {
            return oGrupo.GrupoID;
        }

        public string ObtenerNombreGrupo()
        {
            return oGrupo.Nombre;
        }

        public bool ObtenerEstadoGrupo()
        {
            return oGrupo.Estado;
        }

        public string ObtenerContraseña()
        {
            // clave encriptada
            return Password;
        }

        public string ObtenerNombreUsuario()
        {
            return NombreUsuario;
        }

        public string ObtenerNombreCompleto()
        {
            return $"{Nombre} {Apellido}";
        }

        public List<Modulo> ObtenerModulosPermitidos()
        {
            return ModulosPermitidos;
        }

        public string GrupoPerteneciente()
        {
            return oGrupo.Nombre;
        }
    }
}
