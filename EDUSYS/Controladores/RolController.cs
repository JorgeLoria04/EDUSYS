using EDUSYS.Modelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDUSYS.Controladores
{
    public class RolController
    {
        private ModeloRol modelo = new ModeloRol();

        public void AgregarRol(string Nombre_Rol)
        {
            if (modelo.AgregarRol(Nombre_Rol))
            {
                MessageBox.Show("Rol agregado correctamente!.");
            }
            else
            {
                MessageBox.Show("Error al agregar el rol.");
            }
        }


        public List<ModeloRol> ObtenerRoles()
        {
            return modelo.ObtenerRoles();
        }


        public bool EliminarRol(int idRol, out string mensaje)
        {
            mensaje = "";

            ModeloRol modeloRol = new ModeloRol();

            if (modeloRol.TienePermisosAsignados(idRol))
            {
                mensaje = "No se puede eliminar el rol porque tiene permisos asignados. Por favor, elimine los permisos primero.";
                return false;
            }

            if (modeloRol.ExisteUsuariosConRol(idRol))
            {
                mensaje = "No se puede eliminar el rol porque está asignado a uno o más usuarios.";
                return false;
            }

            return modeloRol.EliminarRol(idRol, out mensaje);
        }





    }
}
