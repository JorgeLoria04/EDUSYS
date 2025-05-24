using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace EDUSYS.Vistas
{
    public partial class frmReporteEstudiantes : Form
    {
        private ReportViewer reportViewer1;

        public frmReporteEstudiantes()
        {
            InitializeComponent();

            reportViewer1 = new ReportViewer();
            reportViewer1.Size = new Size(900, 600);
            reportViewer1.Location = new Point(
        (this.ClientSize.Width - reportViewer1.Width) / 2,
        (this.ClientSize.Height - reportViewer1.Height) / 2
    );
            reportViewer1.LocalReport.EnableExternalImages = true;
            reportViewer1.Dock = DockStyle.Fill;
            reportViewer1.Name = "reportViewer1";
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.Controls.Add(reportViewer1);
        }

        private void frmReporteEstudiantes_Load(object sender, EventArgs e)
        {
            try
            {
                cmbTipoFiltro.Items.Add("Filtrar por nombre");
                cmbTipoFiltro.Items.Add("Filtrar por nacimiento");
                cmbTipoFiltro.SelectedIndex = 0;

                ModeloReportes modelo = new ModeloReportes();
                DataTable dt = modelo.ObtenerEstudiantes();

                ReportDataSource rds = new ReportDataSource("Estudiantes", dt);
                reportViewer1.LocalReport.ReportPath = "Vistas/ReporteEstudiantes.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);

                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el reporte: " + ex.Message);
            }

            cmbTipoFiltro.SelectedIndexChanged += cmbTipoFiltro_SelectedIndexChanged;
            cmbTipoFiltro.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        //FUNCIÓN PARA GENERAR EL REPORTE DE ESTUDIANTES
        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            ReportesController controller = new ReportesController();
            DataTable dt = null;

            if (cmbTipoFiltro.SelectedItem.ToString() == "Filtrar por nombre")
            {
                string nombre = txtFiltroNombre.Text;
                dt = controller.FiltrarEstudiantesPorNombre(nombre);
            }
            else if (cmbTipoFiltro.SelectedItem.ToString() == "Filtrar por nacimiento")
            {
                string fecha = dtpNacimiento.Value.ToString("yyyy-MM-dd");
                dt = controller.FiltrarEstudiantesPorNacimiento(fecha);
            }

            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("Estudiantes", dt);
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
        }



        //FUNCIÓN PARA MOSTRAR LOS FILTROS DISPONIBLES PARA ESE REPORTE
        private void btnHabilitarFiltros_Click(object sender, EventArgs e)
        {
            panel1.Visible = !panel1.Visible;
        }


        //FUNCIÓN PARA TOMAR EL TIPO DE FILTRO SELECCIONADO
        private void cmbTipoFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filtroSeleccionado = cmbTipoFiltro.SelectedItem.ToString();

            if (filtroSeleccionado == "Filtrar por nombre")
            {
                txtFiltroNombre.Visible = true;
                lblNombre.Visible = true;
                dtpNacimiento.Visible = false;
                lblFecha.Visible = false;
            }
            else if(filtroSeleccionado == "Filtrar por nacimiento")
            {
                txtFiltroNombre.Visible = false;
                lblNombre.Visible = false;
                dtpNacimiento.Visible = true;
                lblFecha.Visible = true;
            }
        }


        //FUNCIÓN PARA VOLVER AL MENÚ PRINCIPAL
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
