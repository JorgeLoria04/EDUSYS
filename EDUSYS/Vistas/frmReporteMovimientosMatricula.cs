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
    public partial class frmReporteMovimientosMatricula : Form
    {
        private ReportViewer reportViewer1;

        public frmReporteMovimientosMatricula()
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
    

        private void frmReporteMovimientosMatricula_Load(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    cmbGenero.Items.AddRange(new string[] { "Todos", "M", "F" });
                    cmbGenero.SelectedIndex = 0;

                    cmbTipo.Items.AddRange(new string[] { "Todos", "Nuevo Ingreso", "Traslado", "Deserción" });
                    cmbTipo.SelectedIndex = 0;

                    CargarReporte();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar el formulario: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el reporte: " + ex.Message);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        //FUNCIÓN PARA MOSTRAR LOS FILTROS DISPONIBLES
        private void btnHabilitarFiltros_Click(object sender, EventArgs e)
        {
            panel1.Visible = !panel1.Visible;
        }


        //FUNCIÓN PARA VOLVER AL MENÚ PRINCIPAL
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        //FUNCIÓN PARA CARGAR EL REPORTE DE MOVIMIENTOS DE MATRÍCULA
        private void CargarReporte(string genero = "Todos", string tipoMovimiento = "Todos")
        {
            try
            {
                ModeloReportes modelo = new ModeloReportes();
                DataTable dt = modelo.ObtenerMovimientosMatricula();

                if (genero != "Todos")
                {
                    var filtrado = dt.AsEnumerable()
                                     .Where(row => row.Field<string>("Genero") == genero);
                    if (filtrado.Any())
                        dt = filtrado.CopyToDataTable();
                    else
                        dt.Rows.Clear();
                }

                if (tipoMovimiento != "Todos")
                {
                    var filtrado = dt.AsEnumerable()
                                     .Where(row => row.Field<string>("Tipo_Movimiento") == tipoMovimiento);
                    if (filtrado.Any())
                        dt = filtrado.CopyToDataTable();
                    else
                        dt.Rows.Clear();
                }

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron resultados con los filtros aplicados.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                ReportDataSource rds = new ReportDataSource("MovimientoMatricula", dt);
                reportViewer1.LocalReport.ReportPath = "Vistas/ReporteMovimientos.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);

                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar el reporte: " + ex.Message);
            }
        }


        //FUNCIÓN PARA APLICAR LOS FILTROS ESTABLECIDOS
        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            string genero = cmbGenero.SelectedItem.ToString();
            string tipo = cmbTipo.SelectedItem.ToString();

            CargarReporte(genero, tipo);
        }
    }
}
