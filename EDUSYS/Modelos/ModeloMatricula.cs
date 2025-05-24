using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDUSYS.Modelos
{
    public class ModeloMatricula
    {
        public int ID_Movimiento { get; set; }
        public string Identificacion_Estudiante { get; set; }
        public string Fecha_Movimiento { get; set; }
        public string Tipo_Movimiento { get; set; }
        public char Genero { get; set; }
        public string Observaciones {  get; set; }

        //FUNCION QUE CARGA LOS MOVIMIENTOS DE MATRICULA GUARDADOS EN LA BASE DE DATOS
        public static List <ModeloMatricula> CargarMovimientos()
        {
            List <ModeloMatricula> movimientos = new List<ModeloMatricula>();

            string conexion = "Server=DESKTOP-ELGAIO4\\SQLEXPRESS;Database=EDUSYS;TRUSTED_Connection=True;";
            using (SqlConnection conn = new SqlConnection(conexion))
            {
                conn.Open();
                string query = "SELECT * FROM Movimientos_Matricula";
                SqlCommand cmd = new SqlCommand(query,conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    movimientos.Add(new ModeloMatricula
                    {
                        ID_Movimiento = Convert.ToInt32(reader["ID_Movimiento"]),
                        Identificacion_Estudiante = reader["Identificacion_Estudiante"].ToString(),
                        Fecha_Movimiento = reader["Fecha_Movimiento"].ToString(),
                        Tipo_Movimiento = reader["Tipo_Movimiento"].ToString(),
                        Genero = Convert.ToChar(reader["Genero"]),
                        Observaciones = reader["Observaciones"] != DBNull.Value ? reader["Observaciones"].ToString() : ""
                    });
                }
                }
                return movimientos;
        }

        //FUNCIÓN QUE PERMITE AGREGAR UN NUEVO MOVIMIENTO DE MATRÍCULA
        public static bool AgregarMovimiento(ModeloMatricula movimiento)
        {
            try
            {
                string conexion = "Server=DESKTOP-ELGAIO4\\SQLEXPRESS;Database=EDUSYS;Integrated Security=True;";

                using (SqlConnection conn = new SqlConnection(conexion))
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
                        cmd.Parameters.AddWithValue("@Observaciones", string.IsNullOrEmpty(movimiento.Observaciones) ? DBNull.Value : (object)movimiento.Observaciones);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al agregar el movimiento de matrícula: " + ex.Message);
                return false;
            }
        }

        public static bool EliminarMovimiento(int idMovimiento)
        {
            try
            {
                string conexion = "Server=DESKTOP-ELGAIO4\\SQLEXPRESS;Database=EDUSYS;Integrated Security=True;";

                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    conn.Open();
                    string query = "DELETE FROM Movimientos_Matricula WHERE ID_Movimiento = @ID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", idMovimiento);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el movimiento: " + ex.Message);
                return false;
            }
        }


    }
 }

