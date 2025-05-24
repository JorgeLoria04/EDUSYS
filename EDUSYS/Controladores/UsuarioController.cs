using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

    public class UsuarioController
    {
        private ModeloUsuario modelo = new ModeloUsuario();

    public bool RegistrarUsuario(string Usuario, string Password, string Nombre_Rol, out string mensaje)
    {
        mensaje = string.Empty;
        int ID_Rol = ObtenerID_RolXNombre(Nombre_Rol);

        if (ID_Rol == -1)
        {
            mensaje = "Rol no válido.";
            return false;
        }

        return modelo.RegistrarUsuario(Usuario, Password, ID_Rol, out mensaje);
    }


    public bool ActualizarUsuario(string usuarioOriginal, string nuevoUsuario, string nuevaPassword, int idRol)
    {
        return modelo.ActualizarUsuario(usuarioOriginal, nuevoUsuario, nuevaPassword, idRol);
    }


    public bool EliminarUsuario(string Usuario)
        {
            return modelo.EliminarUsuario(Usuario);
        }

        public DataTable ObtenerUsuarios()
        {
            return modelo.ObtenerUsuarios();
        }

        public int ObtenerID_RolXNombre(string Nombre_Rol)
        {
            return modelo.ObtenerID_RolXNombre(Nombre_Rol);
        }

        public DataTable ObtenerRoles()
        {
            return modelo.ObtenerTodosLosRoles(); // si tienes un método para llenar el ComboBox de roles
        }

    public ModeloUsuario BuscarUsuarioPorNombre(string usuario)
    {
        return ModeloUsuario.BuscarPorUsuario(usuario);
    }

}







