
using Microsoft.Reporting.WinForms;
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
    public partial class frmReportes : Form
    {
        private ReportesController controlador = new ReportesController();
        public frmReportes()
        {
            InitializeComponent();
            cmbTipoReporte.Items.Add("Estudiantes");
            cmbTipoReporte.Items.Add("Movimientos de matricula");
            cmbTipoReporte.Items.Add("Bitácora de accesos");
            cmbTipoReporte.Items.Add("Bitácora de movimientos");
            cmbTipoReporte.SelectedIndex = 0;
        }



        public void CargarReporteEstudiantes()
        {
            dsReportes ds = new dsReportes();
        }

        private void frmReportes_Load (object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }

        private void btnGenerarReporte_Click_1(object sender, EventArgs e)
        {
            if (cmbTipoReporte.SelectedItem != null)
            {
                string tipo = cmbTipoReporte.SelectedItem.ToString();

                if (tipo == "Estudiantes")
                {
                    frmReporteEstudiantes frm = new frmReporteEstudiantes();
                    frm.ShowDialog();
                }
                else if (tipo == "Movimientos de matricula")
                {
                    frmReporteMovimientosMatricula frm = new frmReporteMovimientosMatricula();
                    frm.ShowDialog();
                }
                else if (tipo == "Bitácora de accesos")
                {
                    frmReporteBitacoraEntradas frm = new frmReporteBitacoraEntradas();
                    frm.ShowDialog();
                }
                else if (tipo == "Bitácora de movimientos")
                {
                    frmReporteBitacoraMovimientos frm = new frmReporteBitacoraMovimientos();
                    frm.ShowDialog();
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
