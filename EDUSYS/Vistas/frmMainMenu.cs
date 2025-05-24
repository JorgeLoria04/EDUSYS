using EDUSYS.Modelos;
using EDUSYS.Vistas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDUSYS
{
    public partial class frmMainMenu : Form
    {
        private LoginController loginController;


        //CÓDIGO NECESARIO PARA PODER MOVER LA VENTANA
        [DllImport("user32.dll")]
        private static extern void ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;



        public List<int> ObtenerPermisosDeRol(int idRol)
        {
            ModeloRolPermiso modeloRolPermiso = new ModeloRolPermiso();
            return modeloRolPermiso.ObtenerPermisosPorRol(idRol);
        }

        public frmMainMenu()
        {
            InitializeComponent();
            loginController = new LoginController();
        }

        //FUNCIÓN PARA ARRASTRAR LA VENTANA CON EL MOUSE
        private void frmMainMenu_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void AbrirFormularioHijo(object FormHijo)
        {
            if (this.PanelContenedor.Controls.Count> 0)      
                this.PanelContenedor.Controls.RemoveAt(0);
            Form fh = FormHijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.PanelContenedor.Controls.Add(fh);
            this.PanelContenedor.Tag = fh;
            fh.Show();
            
        }


        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState |= FormWindowState.Maximized;
            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true;
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnRestaurar.Visible=false;
            btnMaximizar.Visible=true;
        }

        private void picApagar_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnEstudiantes_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new frmEstudiantes());
        }

        private void PanelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnRoles_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new frmRolesyPermisos());
        }

        private void btnMatricula_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new frmMatricula());
        }

        private void frmMainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (frmLogin.idbitacora != -1)
            {
                LoginController login = new LoginController();
                login.CerrarSesion(frmLogin.idbitacora);
            }
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new frmUsuarios());
        }


        //FUNCIÓN QUE FORMATEA LA APERTURA DE LOS FORMULARIOS EN EL PANEL CONTENEDOR
        private void AbrirFormularioEnPanel(Form formHijo)
        {

            foreach (Control control in PanelContenedor.Controls)
            {
                if (control is Form)
                {
                    control.Hide();
                }
            }

            formHijo.TopLevel = false;
            formHijo.FormBorderStyle = FormBorderStyle.None; 
            formHijo.Dock = DockStyle.Fill;

            PanelContenedor.Controls.Add(formHijo);
            formHijo.Show();
        }


        private void AbrirFormulario(Form formulario)
        {
            PanelContenedor.Controls.Clear();
            formulario.TopLevel = false;
            PanelContenedor.Controls.Add(formulario);
            formulario.Show();
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmReportes());
        }

        private void btnAcerca_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmAcercade());
        }


        //FUNCIÓN QUE ABRE EL MANUAL DE USO DEL SISTEMA AL PRESIONAR EL BOTÓN DE AYUDA
        private void btnAyuda_Click(object sender, EventArgs e)
        {
            string rutaPDF = System.IO.Path.Combine(Application.StartupPath, @"Documentacion\Manual de Usuario.pdf");
            System.Diagnostics.Process.Start(rutaPDF);
        }
    }
}
