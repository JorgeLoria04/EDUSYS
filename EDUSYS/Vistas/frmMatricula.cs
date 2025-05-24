using EDUSYS.Controladores;
using EDUSYS.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDUSYS
{
    public partial class frmMatricula : Form
    {
        private MatriculaController matriculaController;
        public frmMatricula()
        {
            InitializeComponent();
            matriculaController = new MatriculaController(this);
        }

        private void frmMatricula_Load(object sender, EventArgs e)
        {
            matriculaController.CargarMovimientos(lstMovimientos);

            List<string> generos = matriculaController.ObtenerGeneros();
            List<string> tiposDeMovimiento = matriculaController.ObtenerTiposDeMovimiento();

            CargarComboBoxGeneros(generos);
            CargarComboBoxTiposDeMovimiento(tiposDeMovimiento);
        }


        //CARGA LOS COMBOBOX DE TIPO DE MOVIMIENTO PARA PODER USARLOS POSTERIORMENTE
        public void CargarComboBoxTiposDeMovimiento(List<string> tiposDeMovimiento)
        {
            cmbTipo_Movimiento.Items.Clear();
            cmbTipo_Movimiento.Items.AddRange(tiposDeMovimiento.ToArray());
        }


        //CARGA LOS COMBOBOX DE GENERO PARA PODER USARLOS POSTERIORMENTE
        public void CargarComboBoxGeneros(List<string> generos)
        {
            cmbGenero.Items.Clear();
            cmbGenero.Items.AddRange(generos.ToArray());
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        //MÉTODO QUE CARGA LOS DATOS DEL MOVIMIENTO DE MÁTRICULA DESPÚES DE SELECCIONARLO EN LA LISTA
        private void lstMovimientos_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (lstMovimientos.SelectedItems.Count > 0)
            {
                ListViewItem item = lstMovimientos.SelectedItems[0];

                dtpFecha_Movimiento.Value = DateTime.Parse(item.SubItems[1].Text);

                txtIdentificacion.Text = item.SubItems[2].Text;

                cmbTipo_Movimiento.SelectedItem = item.SubItems[3].Text;

                cmbGenero.SelectedItem = item.SubItems[4].Text;

                txtObservaciones.Text = item.SubItems[5].Text;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }


        private void LimpiarCampos()
        {
            dtpFecha_Movimiento.Value = DateTime.Today;
            txtIdentificacion.Clear();
            cmbTipo_Movimiento.SelectedIndex = -1;
            cmbGenero.SelectedIndex = -1;
            txtObservaciones.Clear();
        }

        //MÉTODO QUE GUARDA EL MOVIMIENTO DE MATRÍCULA EN LA BASE DE DATOS
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ModeloMatricula nuevoMovimiento = new ModeloMatricula
            {
                Identificacion_Estudiante = txtIdentificacion.Text,
                Fecha_Movimiento = dtpFecha_Movimiento.Value.ToString("dd-MM-yyyy"),
                Tipo_Movimiento = cmbTipo_Movimiento.SelectedItem.ToString(),
                Genero = cmbGenero.SelectedItem.ToString()[0],
                Observaciones = txtObservaciones.Text
            };

            MatriculaController controller = new MatriculaController(this);

            bool resultado = controller.AgregarMovimiento(nuevoMovimiento);

            if (resultado)
            {
                MessageBox.Show("Movimiento de matrícula agregado correctamente.");
                LimpiarCampos();
                matriculaController.CargarMovimientos(lstMovimientos);
            }
            else
            {
                MessageBox.Show("Ocurrió un error al agregar el movimiento.");
            }
        }

        //MÉTODO QUE IMPIDE QUE SE DIGITEN CARACTÉRES NO NUMÉRICOS EN EL CAMPO DE IDENFICACIÓN DEL ESTUDIANTE INVOLUCRADO EN EL MOVIMIENTO
        private void txtIdentificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            else if (char.IsDigit(e.KeyChar) && txtIdentificacion.Text.Length >= 9)
            {
                e.Handled = true;
            }
        }

        private void txtIdentificacion_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (lstMovimientos.SelectedItems.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un movimiento para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idMovimiento = Convert.ToInt32(lstMovimientos.SelectedItems[0].SubItems[0].Text);

            DialogResult confirmacion = MessageBox.Show("¿Está seguro de que desea eliminar este movimiento?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                MatriculaController controller = new MatriculaController(this);
                bool eliminado = controller.EliminarMovimiento(idMovimiento);

                if (eliminado)
                {
                    MessageBox.Show("Movimiento eliminado correctamente.");
                    matriculaController.CargarMovimientos(lstMovimientos);
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Ocurrió un error al eliminar el movimiento.");
                }
            }
        }
    }
}

