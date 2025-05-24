using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDUSYS.Vistas
{
    public partial class frmRecuperacion : Form
    {
        public frmRecuperacion()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        //FUNCIÓN PARA CONFIRMAR LA RECUPERACIÓN DE LA CONTRASEÑA
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            string Usuario = txtUsuario.Text.Trim();

            LoginController controller = new LoginController();

            bool UsuarioExistente = controller.VerificarUsuarioExistente(Usuario);

            if (UsuarioExistente)
            {
                string NuevoPassword = txtPassword.Text.Trim();
                string PasswordConfirmada = txtPasswordConfirm.Text.Trim();

                if (string.IsNullOrEmpty(NuevoPassword) || string.IsNullOrEmpty(PasswordConfirmada))
                {
                    MessageBox.Show("Por favor, ingrese y confirme una nueva contraseña.");
                    return;
                }

                if (NuevoPassword == PasswordConfirmada)
                {
                    bool actualizado = controller.CambiarPassword(Usuario, NuevoPassword);

                    if (actualizado)
                    {
                        MessageBox.Show("Tu contraseña se ha cambiado correctamente!");
                        this.Hide();

                        Form frmLogin = Application.OpenForms["frmLogin"];

                        if (frmLogin != null)
                        {
                            frmLogin.BringToFront();
                        }
                        else
                        {

                            frmLogin = new frmLogin();
                            frmLogin.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Hubo un error al actualizar tu contraseña.");
                    }
                }
                else
                {
                    MessageBox.Show("Las contraseñas no coinciden. Porfavor, intenta nuevamente.");
                }
            }
            else
            {
                MessageBox.Show("El usuario ingresado no existe.");
            }
        }

        private void frmRecuperacion_Load(object sender, EventArgs e)
        {

        }
    }
}
