using EDUSYS.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDUSYS.Controladores
{
    internal class RolPermisoController
    {
        private ModeloRolPermiso modelo = new ModeloRolPermiso();

        public List<int> ObtenerPermisosDeRol(int idRol)
        {
            return modelo.ObtenerPermisosPorRol(idRol);
        }

        public bool GuardarPermisosAsignados(int idRol, List<int> permisos)
        {
            return modelo.AsignarPermisos(idRol, permisos);
        }

        public bool AsignarPermisosARol(int ID_Rol, List<int> ID_Permiso)
        {
            ModeloRolPermiso modelo = new ModeloRolPermiso();
            return modelo.GuardarPermisosDelRol(ID_Rol, ID_Permiso);
        }


    }
}
