using EDUSYS.Controladores;
using EDUSYS.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDUSYS.Vistas
{
    public partial class frmRolesyPermisos : Form
    {
        ModeloRol modelo = new ModeloRol();
        RolController rol = new RolController();
        PermisoController permiso = new PermisoController();
        ModeloRolPermiso rolPermiso = new ModeloRolPermiso();

        public frmRolesyPermisos()
        {
            InitializeComponent();
        }

        private void frmRolesyPermisos_Load(object sender, EventArgs e)
        {
            CargarRoles();
            CargarPermisos();
            CargarOpcionesPermisos();
            CargarRolesEnComboBox();
            lstPermisos.DisplayMember = "Nombre_Permiso";
            lstPermisos.ValueMember = "ID_Permiso";
        }


        //FUNCIÓN PARA CARGAR LOS ROLES EN UNA TABLA
        private void CargarRoles()
        {
           List<ModeloRol> roles = modelo.ObtenerRoles();

            lstRoles.Items.Clear();
            foreach (var rol in roles)
            {
                lstRoles.Items.Add(rol);
            }
        }


        //FUNCIÓN PARA CARGAR LOS PERMISOS EN UNA TABLA
        private void CargarPermisos()
        {
            List<ModeloPermiso> permisos = permiso.ObtenerPermisos();

            lstPermisos.Items.Clear();

            foreach (var permiso in permisos)
            {
                lstPermisos.Items.Add(permiso);
            }
        }



        private void btnAgregarRol_Click(object sender, EventArgs e)
        {
         string Nombre_Rol = txtRol.Text;
         rol.AgregarRol(Nombre_Rol);
            CargarRoles();
            LimpiarCampos();
            CargarRolesEnComboBox();
        }



        //FUNCIÓN PARA CARGAR LOS ROLES EN LA CAJA DE TEXTO AL SELECCIONARLO DE LA LISTA
        private void lstRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstRoles.SelectedItem != null)
            {
                ModeloRol rolSeleccionado = (ModeloRol)lstRoles.SelectedItem;

                txtRol.Text = rolSeleccionado.Nombre_Rol;
            }
        }


        private void LimpiarCampos()
        {
            txtRol.Clear();
            txtPermiso.Clear();
        }


        //FUNCIÓN PARA ELIMINAR UN ROL
        private void btnEliminarRol_Click(object sender, EventArgs e)
        {
            if (lstRoles.SelectedItem != null)
            {
                ModeloRol rolSeleccionado = (ModeloRol)lstRoles.SelectedItem;

                int idRol = rolSeleccionado.ID_Rol;

                string mensaje;

                bool eliminado = rol.EliminarRol(idRol, out mensaje);

                if (eliminado)
                {
                    MessageBox.Show("Rol eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarRoles();
                    CargarRolesEnComboBox();
                }
                else
                {
                    MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione un rol.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        //FUNCIÓN PARA AGREGAR UN NUEVO PERMISO
        private void btnAgregarPermiso_Click(object sender, EventArgs e)
        {
            string Nombre_Permiso = txtPermiso.Text;

            var rolSeleccionado = (ModeloRol)cmbRoles.SelectedItem;
            int ID_Rol = rolSeleccionado.ID_Rol;

            if (string.IsNullOrWhiteSpace(Nombre_Permiso))
            {
                MessageBox.Show("Porfavor ingrese un nombre para el permiso.");
                return;
            }

            bool agregado = permiso.AgregarPermiso(Nombre_Permiso);

            if (agregado)
            {
                CargarPermisos();
                LimpiarCampos();
                CargarOpcionesPermisos();
                CargarRolesEnComboBox();

                MarcarPermisosDelRol(ID_Rol);
            }
            else
            {
                MessageBox.Show("Error al agregar el permiso.");
            }
        }


        //FUNCIÓN PARA ELIMINAR UN PERMISO
        private void btnEliminarPermiso_Click(object sender, EventArgs e)
        {
            if (lstPermisos.SelectedItem != null)
            {
                var permisoSeleccionado = (ModeloPermiso)lstPermisos.SelectedItem;
                var rolSeleccionado = (ModeloRol)cmbRoles.SelectedItem;

                int ID_Rol = rolSeleccionado.ID_Rol;
                int ID_Permiso = permisoSeleccionado.ID_Permiso;

                // Verificar si el permiso está asignado a algún rol antes de eliminarlo
                if (permiso.TienePermisosAsignados(ID_Permiso))
                {
                    MessageBox.Show("No se puede eliminar este permiso porque está asignado a un rol.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool Eliminado = permiso.EliminarPermiso(ID_Permiso);

                if (Eliminado)
                {
                    MessageBox.Show("Permiso eliminado correctamente.");
                    CargarPermisos();
                    LimpiarCampos();
                    CargarOpcionesPermisos();
                    MarcarPermisosDelRol(ID_Rol);
                }
                else
                {
                    MessageBox.Show("Error al eliminar el permiso.");
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione un permiso para eliminar.");
            }
        }



        //FUNCIÓN PARA CARGAR LOS POSIBLES PERMISOS EN UNA LISTA
        private void CargarOpcionesPermisos()
        { 
            clbPermisos.Items.Clear();

            ModeloPermiso modeloPermiso = new ModeloPermiso();

            List<ModeloPermiso> listaPermisos = modeloPermiso.ObtenerPermisos();

            foreach (var permiso in listaPermisos)
            {
                clbPermisos.Items.Add(permiso, false);
            }

        }



        //FUNCIÓN PARA CARGAR LOS ROLES EN UN COMBOBOX
        private void CargarRolesEnComboBox()
        {
            ModeloRol modeloRol = new ModeloRol();
            List<ModeloRol> listaRoles = modeloRol.ObtenerRoles();

            cmbRoles.DataSource = listaRoles;
            cmbRoles.DisplayMember = "Nombre_Rol";
            cmbRoles.ValueMember = "ID_Rol";
        }



        private void cmbRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRoles.SelectedItem != null)
            {
                ModeloRol rolSeleccionado = (ModeloRol)cmbRoles.SelectedItem;
                MarcarPermisosDelRol(rolSeleccionado.ID_Rol);
            }
        }


        //FUNCIÓN PARA GUARDAR LOS PERMISOS QUE SE VAN A ASIGNAR A UN ROL
        private void MarcarPermisosDelRol(int ID_Rol)
        {
            ModeloRolPermiso modeloRolPermiso = new ModeloRolPermiso();
            List<int> permisosDelRol = modeloRolPermiso.ObtenerPermisosPorRol(ID_Rol);

            for (int i = 0; i < clbPermisos.Items.Count; i++)
            {
                ModeloPermiso permiso = (ModeloPermiso)clbPermisos.Items[i];
                clbPermisos.SetItemChecked(i, permisosDelRol.Contains(permiso.ID_Permiso));
            }
        }


        //FUNCIÓN PARA ASIGNAR PERMISOS A LOS ROLES
        private void btnGuardarPermisos_Click(object sender, EventArgs e)
        {
            if (cmbRoles.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione un rol.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ModeloRol rolseleccionado = (ModeloRol)cmbRoles.SelectedItem;

            List<int> permisosSeleccionados = new List<int>();
            for (int i = 0; i < clbPermisos.Items.Count; i++)
            {
                if (clbPermisos.GetItemChecked(i))
                {
                    ModeloPermiso permiso = (ModeloPermiso)clbPermisos.Items[i];
                    permisosSeleccionados.Add(permiso.ID_Permiso);
                }
            }

                RolPermisoController controller = new RolPermisoController();
                bool exito = controller.GuardarPermisosAsignados(rolseleccionado.ID_Rol, permisosSeleccionados);

            if (exito)
            {
                MessageBox.Show("Permisos actualizados correctamente!");
            }
            else
            {
                MessageBox.Show("Hubo un error al guardar los permisos.");
            }

        }

        private void lstPermisos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstPermisos.SelectedItem != null)
            {
                ModeloPermiso PermisoSeleccionado = (ModeloPermiso)lstPermisos.SelectedItem;

                txtPermiso.Text = PermisoSeleccionado.Nombre_Permiso;
            }
        }
    }
}
