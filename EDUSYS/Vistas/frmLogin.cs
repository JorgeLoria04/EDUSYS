using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.Data.SqlClient;
using EDUSYS.Vistas;

namespace EDUSYS
{
    public partial class frmLogin : Form
    {
        LoginController login = new LoginController();
        public static int idbitacora = -1;

        //CODIGO NECESARIO PARA LA FUNCIONALIDAD DE ARRASTRAR LA VENTANA
        [DllImport("user32.dll")]
        private static extern void ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;


        
        public frmLogin()
        {
            InitializeComponent();
        }


        //FUNCIÓN QUE PERMITE ARRASTRAR LA VENTANA
        private void frmLOG_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }


        private void frmLOG_Load(object sender, EventArgs e)
        {
            TextboxPersonalizadas();
            
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        //PERSONALIZACIÓN PARA LA TEXTBOX DE USUARIO Y CONTRASEÑA
        private void TextboxPersonalizadas()
        {
            txtUsuario.AutoSize = false;
            txtUsuario.Size = new Size(350, 30);
            txtPassword.AutoSize = false;
            txtPassword.Size = new Size(350, 30);
            txtPassword.PasswordChar = '*';
        }

        private void btnLogin_Paint(object sender, PaintEventArgs e)
        {
            RedondearBoton(btnLogin, 20);
        }


        //PERSONALIZACIÓN PARA EL BOTÓN DE INICIAR SESIÓN
        private void RedondearBoton(Button btn, int radio)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radio, radio, 180, 90);
            path.AddArc(btn.Width - radio, 0, radio, radio, 270, 90);
            path.AddArc(btn.Width - radio, btn.Height - radio, radio, radio, 0, 90);
            path.AddArc(0, btn.Height - radio, radio, radio, 90, 90);
            path.CloseFigure();
            btn.Region = new Region(path);
        }
        


        //FUNCIÓN QUE MANEJA LAS VALIDACIONES NECESARIAS PARA INICIAR SESIÓN
        private void btnLogin_Click(object sender, EventArgs e)
        { 
                string Usuario = txtUsuario.Text.Trim();
                string Password = txtPassword.Text.Trim();

                LoginController login = new LoginController();

            int idbitacora;

            if (login.IniciarSesion(Usuario, Password, out idbitacora))
            {
                frmLogin.idbitacora = idbitacora;

                int ID_Usuario = ModeloUsuario.ObtenerIDUsuarioPorNombre(Usuario);

                int ID_Rol = ModeloUsuario.ObtenerID_RolPorUsuario(ID_Usuario);

                frmMainMenu frm = new frmMainMenu();
                login.SetVista(frm);
                login.AplicarPermisosPorRol(ID_Rol);

                frm.Show();
                this.Hide();
            }
            else
            {
                lblError.Text = "Usuario o contraseña incorrectos";
                lblError.ForeColor = Color.Red;
                lblError.Visible = true;
                txtUsuario.Clear();
                txtPassword.Clear();
                txtUsuario.Focus();
            }
        }
        
    

        private void BarraTItulo_Paint(object sender, PaintEventArgs e)
        {

        }


        //BOTÓN PARA VER U OCULTAR LA CONTRASEÑA
        private void btnOcultar_Click(object sender, EventArgs e)
        {
                {
                    if (txtPassword.PasswordChar == '\0')
                    {
                        txtPassword.PasswordChar = '*';
                    }
                    else
                    {
                        txtPassword.PasswordChar = '\0';
                    }
                }
        }


        //FUNCIÓN PARA ACCEDER AL FORMULARIO DE RECUPERACIÓN EN CASO DE PÉRDIDA DE CONTRASEÑA
        private void lblOlvidada_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmRecuperacion recuperarform = new frmRecuperacion();
            recuperarform.ShowDialog();
            lblError.Visible = false;
            txtUsuario.Clear();
            txtPassword.Clear();
        }
    }
}
