using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Windows.Forms;

namespace EDUSYS.Vistas
{
    public partial class frmReporteBitacoraMovimientos : Form
    {
        private ReportViewer reportViewer1;

        public frmReporteBitacoraMovimientos()
        {
            InitializeComponent();
            InicializarReportViewer();
        }

        private void frmReporteBitacoraMovimientos_Load(object sender, EventArgs e)
        {
            try
            {
                cmbTipo.Items.Clear();
                cmbTipo.Items.Add("INSERT");
                cmbTipo.Items.Add("UPDATE");
                cmbTipo.Items.Add("DELETE");

                ModeloReportes modelo = new ModeloReportes();
                DataTable dt = modelo.ObtenerBitacoraMovimientos();

                DateTime fechaInicio = new DateTime(1753, 1, 1);
                DateTime fechaFin = new DateTime(9999, 12, 31);

                CargarReporte("", "", fechaInicio, fechaFin);

                dtpDesde.Value = DateTime.Today.AddMonths(-1);
                dtpHasta.Value = DateTime.Today;

                ReportDataSource rds = new ReportDataSource("BitacoraMovimientos", dt);
                reportViewer1.LocalReport.ReportPath = "Vistas/ReporteBitacoraMovimientos.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);

                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el reporte de bitácora de movimientos: " + ex.Message);
            }
        }



        //FUNCIÓN PARA INSTANCIAR EL REPORTVIEWER QUE VA A MOSTRAR EL REPORTE
        private void InicializarReportViewer()
        {
            reportViewer1 = new ReportViewer();
            reportViewer1.Dock = DockStyle.Fill;
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.Controls.Add(reportViewer1);

            this.Load += frmReporteBitacoraMovimientos_Load;
        }



        //FUNCIÓN PARA VOLVER AL MENÚ PRINCIPAL
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        //FUNCIÓN PARA APLICAR LOS FILTROS
        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string tipoMovimiento = cmbTipo.SelectedItem != null ? cmbTipo.SelectedItem.ToString() : "";
            DateTime desde = dtpDesde.Value.Date;
            DateTime hasta = dtpHasta.Value.Date.AddDays(1).AddSeconds(-1);

            CargarReporte(usuario, tipoMovimiento, desde, hasta);
        }



        //FUNCIÓN PARA CARGAR EL REPORTE
        private void CargarReporte(string usuario, string tipoMovimiento, DateTime desde, DateTime hasta)
        {
            try
            {
                ModeloReportes modelo = new ModeloReportes();
                DataTable dt = modelo.ObtenerBitacoraMovimientosFiltrada(usuario, tipoMovimiento, desde, hasta);

                reportViewer1.LocalReport.ReportPath = "Vistas/ReporteBitacoraMovimientos.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                ReportDataSource rds = new ReportDataSource("BitacoraMovimientos", dt);
                reportViewer1.LocalReport.DataSources.Add(rds);
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el reporte filtrado: " + ex.Message);
            }
        }



        //FUNCIÓN PARA HABILITAR LOS FILTROS DISPONIBLES
        private void btnHabilitarFiltros_Click_1(object sender, EventArgs e)
        {
            panel1.Visible = !panel1.Visible;
        }




        //FUNCIÓN PARA LIMPIAR LOS FILTROS
        private void btnLimpiarFiltros_Click_1(object sender, EventArgs e)
        {
            txtUsuario.Text = "";
            cmbTipo.SelectedIndex = -1;
            dtpDesde.Value = DateTime.Today.AddMonths(-1);
            dtpHasta.Value = DateTime.Today;

            DateTime fechaInicio = new DateTime(1753, 1, 1);
            DateTime fechaFin = new DateTime(9999, 12, 31);

            CargarReporte("", "", fechaInicio, fechaFin);
        }
    }
}