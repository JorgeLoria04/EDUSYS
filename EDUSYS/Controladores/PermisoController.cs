using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDUSYS.Controladores
{
    internal class PermisoController
    {
        private ModeloPermiso modelo = new ModeloPermiso();

        public bool AgregarPermiso(string Nombre_Permiso)
        {
            if (modelo.AgregarPermiso(Nombre_Permiso))
            {
                MessageBox.Show("Permiso agregado correctamente!.");
                return true;
            }
            else
            {
                MessageBox.Show("Error al agregar el permiso.");
                return false;
            }
        }


        public bool EliminarPermiso(int ID_Permiso)
        {
            if (modelo.EliminarPermiso(ID_Permiso))
            {
                MessageBox.Show("Permiso eliminado correctamente!.");
                return true;
            }
            else
            {
                MessageBox.Show("Ocurrió un error al eliminar el permiso!.");
                return false;
            }
        }

        public bool TienePermisosAsignados(int idRol)
        {
            try
            {
                return modelo.TienePermisosAsignados(idRol);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al verificar los permisos del rol: {ex.Message}");
                return false;
            }
        }


        public List<ModeloPermiso> ObtenerPermisos()
        {
            return modelo.ObtenerPermisos();
        }



    }
}
