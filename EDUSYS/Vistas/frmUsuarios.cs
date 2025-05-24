using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDUSYS
{
    public partial class frmUsuarios : Form
    {
        private UsuarioController controlador = new UsuarioController();

        private ModeloUsuario modelo  = new ModeloUsuario();

        public frmUsuarios()
        {
            InitializeComponent();
            CargarRoles();
            CargarUsuarios();
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            CargarRoles();
            lstUsuarios.SelectedIndexChanged += lstUsuarios_SelectedIndexChanged;
            LimpiarCampos();
        }

        //FUNCIÓN PARA AGREGAR UN USUARIO A LA BASE DE DATOS
        private void btnAgregarUS_Click(object sender, EventArgs e)
        {
            try
            {
                string Usuario = txtNuevoUsuario.Text;
                string Password = txtPassword.Text;
                string Nombre_Rol = cmbRol.Text;

                if (controlador.BuscarUsuarioPorNombre(Usuario) != null)
                {
                    MessageBox.Show("El usuario ya existe. Por favor, elija otro nombre de usuario.");
                    return;
                }

                bool registrado = controlador.RegistrarUsuario(Usuario, Password, Nombre_Rol, out string mensaje);

                if (registrado)
                {
                    MessageBox.Show("El usuario se ha registrado correctamente!");
                    CargarUsuarios();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("No se pudo registrar el usuario: " + mensaje);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar el usuario: " + ex.Message);
            }
        }


        //FUNCIÓN PARA CARGAR LOS ROLES QUE PUEDEN TENER LOS USUARIOS
        private void CargarRoles()
        {
            DataTable roles = modelo.ObtenerTodosLosRoles();

            cmbRol.DataSource = roles;

            cmbRol.DisplayMember = "Nombre_Rol";
            cmbRol.ValueMember = "ID_Rol";
        }



        //FUNCIÓN PARA CARGAR LOS USUARIOS EN UNA TABLA
        private void CargarUsuarios()
        {
            lstUsuarios.Items.Clear();
            DataTable dt = controlador.ObtenerUsuarios();

            foreach (DataRow row in dt.Rows)
            {
                ListViewItem item = new ListViewItem(row["Usuario"].ToString());
                item.SubItems.Add(row["Nombre_Rol"].ToString());
                lstUsuarios.Items.Add(item);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }



        //FUNCIÓN PARA ELIMINAR UN USUARIO PRESENTE EN LA BASE DE DATOS
        private void btnEliminarUS_Click(object sender, EventArgs e)
        {
            if (lstUsuarios.SelectedItems.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un usuario de la lista.");
                return;
            }

            string usuarioSeleccionado = lstUsuarios.SelectedItems[0].Text;

            if (usuarioSeleccionado == ModeloLogin.UsuarioLogueado)
            {
                MessageBox.Show("No puede eliminar al usuario que está actualmente logueado.", "Acción no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult resultado = MessageBox.Show("¿Está seguro que desea eliminar este usuario?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                bool eliminado = controlador.EliminarUsuario(usuarioSeleccionado);

                if (eliminado)
                {
                    MessageBox.Show("Usuario eliminado correctamente.");
                    CargarUsuarios();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el usuario.");
                }
            }
        }


        private void LimpiarCampos()
        {
            txtNuevoUsuario.Clear();
            txtNuevoUsuario.Enabled = true;
            txtPassword.Enabled = true;
            txtPassword.Clear();
            cmbRol.SelectedIndex = -1;
        }



        //FUNCIÓN PARA EDITAR UN USUARIO PRESENTE EN LA BASE DE DATOS
        private void btnEditarUS_Click(object sender, EventArgs e)
        {
            string usuarioAntiguo = "";
            txtPassword.Enabled = false;

            if (lstUsuarios.SelectedItems.Count > 0)
            {
                usuarioAntiguo = lstUsuarios.SelectedItems[0].Text;
            }
            else if (!string.IsNullOrWhiteSpace(txtNuevoUsuario.Text))
            {
                usuarioAntiguo = txtNuevoUsuario.Text;
            }
            else
            {
                MessageBox.Show("Debe seleccionar un usuario de la lista o escribir el nombre del usuario original.");
                return;
            }

            string nuevoUsuario = txtNuevoUsuario.Text;
            string nuevaPassword = txtPassword.Text.Trim();

            DataRowView seleccionado = (DataRowView)cmbRol.SelectedItem;
            int idRol = (int)cmbRol.SelectedValue;

            if (string.IsNullOrEmpty(usuarioAntiguo) || idRol == 0)
            {
                MessageBox.Show("Debe llenar todos los campos correctamente.");
                return;
            }

            if (string.IsNullOrEmpty(nuevaPassword))
            {
                nuevaPassword = null;
            }

            try
            {
                if (nuevoUsuario != usuarioAntiguo && controlador.BuscarUsuarioPorNombre(nuevoUsuario) != null)
                {
                    MessageBox.Show("El usuario ya existe. Por favor, elija otro nombre de usuario.");
                    return;
                }

                bool actualizado = controlador.ActualizarUsuario(usuarioAntiguo, nuevoUsuario, nuevaPassword, idRol);

                if (actualizado)
                {
                    MessageBox.Show("Usuario actualizado correctamente.");
                    LimpiarCampos();
                    CargarUsuarios();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el usuario. Verifique si el usuario existe.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el usuario: " + ex.Message);
            }
        }


        //FUNCIÓN PARA CARGAR LOS USUARIOS EN LOS CAMPOS AL SELECCIONARLOS EN UNA LISTA
        private void lstUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstUsuarios.SelectedItems.Count > 0)
            {
                ListViewItem item = lstUsuarios.SelectedItems[0];

                string usuario = item.SubItems[0].Text;
                string nombreRol = item.SubItems[1].Text;

                txtNuevoUsuario.Text = usuario;
                txtNuevoUsuario.Enabled = false;
                txtPassword.Enabled = false;

                cmbRol.SelectedIndex = cmbRol.FindStringExact(nombreRol);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        //FUNCIÓN PARA CONSULTAR ESTUDIANTES EN LA BASE DE DATOS POR SU NOMBRE DE USUARIO
        private void btnConsultarUS_Click(object sender, EventArgs e)
        {
            string nombreUsuario = txtNuevoUsuario.Text.Trim();

            if (!string.IsNullOrEmpty(nombreUsuario))
            {
                ModeloUsuario usuario = controlador.BuscarUsuarioPorNombre(nombreUsuario);

                if (usuario != null)
                {
                    txtNuevoUsuario.Text = usuario.Usuario;
                    txtNuevoUsuario.Enabled = false;
                    txtPassword.Enabled = false;
                    cmbRol.SelectedValue = usuario.ID_Rol;
                }
                else
                {
                    MessageBox.Show("Usuario no encontrado.");
                }
            }
            else
            {
                MessageBox.Show("Ingrese un nombre de usuario para buscar.");
            }
        }
    }
}


