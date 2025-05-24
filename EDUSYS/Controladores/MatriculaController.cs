using EDUSYS.Modelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDUSYS.Controladores
{
    public class MatriculaController
    {
        private frmMatricula frmMatricula;

        public MatriculaController(frmMatricula formulario)
        {
            frmMatricula = formulario;
        }

        //FUNCIÓN QUE CARGA LOS MOVIMIENTOS DE MATRÍCULA EN UNA LISTA
        public void CargarMovimientos(ListView lstMovimientos)
        {
            lstMovimientos.Items.Clear();

            lstMovimientos.View = View.Details;

            List <ModeloMatricula> movimientos = ModeloMatricula.CargarMovimientos();

            foreach (var movimiento in movimientos)
            {
                ListViewItem item = new ListViewItem(movimiento.ID_Movimiento.ToString());
                item.SubItems.Add(movimiento.Fecha_Movimiento);
                item.SubItems.Add(movimiento.Identificacion_Estudiante);
                item.SubItems.Add(movimiento.Tipo_Movimiento);
                item.SubItems.Add(movimiento.Genero.ToString());
                item.SubItems.Add(string.IsNullOrEmpty(movimiento.Observaciones) ? "Sin Observaciones" : movimiento.Observaciones);

                lstMovimientos.Items.Add(item);
            }
        }


        //FUNCIÓN PARA OBTENER LOS DATOS CON LOS QUE SE VAN A RELLENAR LOS COMBOBOX
        public List<string> ObtenerTiposDeMovimiento()
        {
            return new List<string> { "Nuevo Ingreso", "Traslado", "Deserción" };
        }



        // MÉTODO PARA OBTENER LOS GÉNEROS
        public List<string> ObtenerGeneros()
        {
            return new List<string> { "M", "F"};
        }


        // Método para cargar los ComboBox con los datos
        public void CargarComboBoxes()
        {
            List<string> tiposDeMovimiento = ObtenerTiposDeMovimiento();
            List<string> generos = ObtenerGeneros();

            frmMatricula.CargarComboBoxTiposDeMovimiento(tiposDeMovimiento);
            frmMatricula.CargarComboBoxGeneros(generos);
        }



        //MÉTODO PARA INGRESAR MOVIMIENTOS DE MATRÍCULA EN LA BASE DE DATOS
        public bool AgregarMovimiento(ModeloMatricula movimiento)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Server=DESKTOP-ELGAIO4\\SQLEXPRESS;Database=EDUSYS;TRUSTED_Connection=True;"))
                {
                    conn.Open();

                    string query = @"INSERT INTO Movimientos_Matricula (Identificacion_Estudiante, Fecha_Movimiento, Tipo_Movimiento, Genero, Observaciones)
                                 VALUES (@Identificacion_Estudiante, @Fecha_Movimiento, @Tipo_Movimiento, @Genero, @Observaciones)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Identificacion_Estudiante", movimiento.Identificacion_Estudiante);
                        cmd.Parameters.AddWithValue("@Fecha_Movimiento", movimiento.Fecha_Movimiento);
                        cmd.Parameters.AddWithValue("@Tipo_Movimiento", movimiento.Tipo_Movimiento);
                        cmd.Parameters.AddWithValue("@Genero", movimiento.Genero);
                        cmd.Parameters.AddWithValue("@Observaciones", movimiento.Observaciones ?? (object)DBNull.Value);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al agregar el movimiento: " + ex.Message);
                return false;
            }
        }


        public bool EliminarMovimiento(int idMovimiento)
        {
            return ModeloMatricula.EliminarMovimiento(idMovimiento);
        }
    }

}

