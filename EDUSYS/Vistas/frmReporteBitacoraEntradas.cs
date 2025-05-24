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
    public partial class frmReporteBitacoraEntradas : Form
    {
        private ReportViewer reportViewer1;
        public frmReporteBitacoraEntradas()
        {
            InitializeComponent();
            InicializarReportViewer();
        }

        private void frmReporteBitacoraEntradas_Load(object sender, EventArgs e)
        {
            try
            {
                ModeloReportes modelo = new ModeloReportes();
                DataTable dt = modelo.ObtenerBitacora();
                DateTime fechaInicio = new DateTime(1753, 1, 1);
                DateTime fechaFin = new DateTime(9999, 12, 31);
                CargarReporte("", fechaInicio, fechaFin);

                dtpDesde.Value = DateTime.Today.AddMonths(-1);
                dtpHasta.Value = DateTime.Today;

                ReportDataSource rds = new ReportDataSource("BitacoraEntradas", dt);
                reportViewer1.LocalReport.ReportPath = "Vistas/ReporteBitacoraEntradas.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);

                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el reporte de bitácora: " + ex.Message);
            }
        }


        //FUNCIÓN PARA INICIALIZAR EL REPORTVIEWER QUE SE VA A ENCARGAR DE MOSTRAR EL REPORTE
        private void InicializarReportViewer()
        {
            reportViewer1 = new ReportViewer();
            reportViewer1.Dock = DockStyle.Fill;
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.Controls.Add(reportViewer1);

            this.Load += frmReporteBitacoraEntradas_Load;
        }



        // FUNCIÓN PARA VOLVER AL MENÚ
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        //FUNCIÓN PARA HABILITAR LOS FILTROS DISPONIBLES
        private void btnHabilitarFiltros_Click(object sender, EventArgs e)
        {
            panel1.Visible = !panel1.Visible;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }



        //FUNCIÓN PARA APLICAR LOS FILTROS ELEGIDOS
        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            DateTime fechaDesde = dtpDesde.Value.Date;
            DateTime fechaHasta = dtpHasta.Value.Date;

            CargarReporte(usuario, fechaDesde, fechaHasta);
        }


        //FUNCIÓN PARA CARGAR EL REPORTE
        private void CargarReporte(string usuario, DateTime desde, DateTime hasta)
        {
            try
            {
                ModeloReportes modelo = new ModeloReportes();
                DataTable dt = modelo.ObtenerBitacoraFiltrada(usuario, desde, hasta);

                reportViewer1.LocalReport.ReportPath = "Vistas/ReporteBitacoraEntradas.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                ReportDataSource rds = new ReportDataSource("BitacoraEntradas", dt);
                reportViewer1.LocalReport.DataSources.Add(rds);
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el reporte: " + ex.Message);
            }
        }



        //FUNCIÓN PARA LIMPIAR LOS FILTROS
        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            try
            {
                txtUsuario.Text = "";

                dtpDesde.Value = DateTime.Today.AddMonths(-1);
                dtpHasta.Value = DateTime.Today;

                DateTime fechaInicio = new DateTime(1753, 1, 1);
                DateTime fechaFin = new DateTime(9999, 12, 31);

                // Volver a cargar el reporte sin filtros
                CargarReporte("", fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al limpiar filtros: " + ex.Message);
            }
        }
    }
}
