using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Seguridad
{
    public class Sesion
    {
        // Almacenar el usuario en sesión
        public Usuario _usuario { get; private set; }

        // Al crear una sesión, la idea es trabajar con un Singleton para que no se puedan crear más de una sesión
        private static Sesion _sesion;

        // Para evitar que se creen más de una sesión
        private static readonly object _lock = new object();

        // Propiedad que garantiza el acceso seguro a la instancia
        public static Sesion ObtenerInstancia
        {
            get
            {
                lock (_lock)
                {
                    // Si no existe una sesión, se crea una nueva
                    if (_sesion == null)
                        _sesion = new Sesion();

                    return _sesion;
                }
            }
        }

        public Usuario UsuarioEnSesion()
        {
            return _usuario;
        }

        // Constructor privado para evitar la creación directa de la instancia
        private Sesion() { }

        // Método para iniciar sesión
        public static void IniciarSesion(Usuario usuario)
        {
            lock (_lock) // Bloquea el acceso a esta sección para un hilo a la vez
            {
                if (_sesion == null)
                    _sesion = new Sesion();

                // Si no hay usuario en sesión, se asigna el usuario que se está logueando
                if (_sesion._usuario == null)
                {
                    _sesion._usuario = usuario;
                }
                else if (_sesion._usuario.NombreUsuario == usuario.NombreUsuario)
                {
                    throw new Exception("Ya existe una sesión activa con este usuario.");
                }
                else
                {
                    throw new Exception("Otro usuario ya ha iniciado sesión.");
                }
            }
        }

        public static void CerrarSesion()
        {
            // con multi-hilo se refiere a sistemas tipo web o con sucursales
            lock (_lock) // Bloqueo para asegurar que la operación sea segura en multi-hilo
            {
                if (_sesion != null && _sesion._usuario != null)
                {
                    // Limpiar el usuario y la sesión
                    _sesion._usuario = null;
                    _sesion = null;
                }
                else
                {
                    throw new Exception("No hay sesión iniciada.");
                }
            }
        }

    }

}
