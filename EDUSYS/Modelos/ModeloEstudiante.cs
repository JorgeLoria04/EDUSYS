using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
public class ModeloEstudiante
    {
        public int ID {  get; set; }
        public string Identificacion {  get; set; }
        public string Primer_Nombre {  get; set; }
        public string Segundo_Nombre { get; set; }
        public string Primer_Apellido {  get; set; }
        public string Segundo_Apellido { get; set; }
        public DateTime Nacimiento {  get; set; }
        public string Direccion {  get; set; }
        public string Padecimientos {  get; set; }
        public string Nombre_Madre {  get; set; }
        public string Nombre_Padre {  get; set; }
        public string Celular {  get; set; }

        public string Casa { get; set; }

        public string TelEncargado { get; set; }

        public string Correo {  get; set; }
        public Byte[] Foto { get; set; }


        private static string connectionString = "Server=DESKTOP-ELGAIO4\\SQLEXPRESS;Database=EDUSYS;Integrated Security=True;";


    //MÉTODO QUE INSERTA UN ESTUDIANTE EN LA BASE DE DATOS
    public static bool GuardarEstudiante(ModeloEstudiante estudiante, string usuario)
    {
        try
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                SqlCommand cmd = new SqlCommand(@"INSERT INTO Estudiantes(Identificacion,Primer_Nombre,Segundo_Nombre,Primer_Apellido,Segundo_Apellido,Nacimiento,Direccion,Padecimientos,Nombre_Madre,Nombre_Padre,Celular,Casa,TelEncargado,Correo,Foto)
                                                VALUES (@Identificacion,@Primer_Nombre,@Segundo_Nombre,@Primer_Apellido,@Segundo_Apellido,@Nacimiento,@Direccion,@Padecimientos,@Nombre_Madre,@Nombre_Padre,@Celular,@Casa,@TelEncargado,@Correo,@Foto)", conn, transaction);

                cmd.Parameters.AddWithValue("@Identificacion", estudiante.Identificacion);
                cmd.Parameters.AddWithValue("@Primer_Nombre", estudiante.Primer_Nombre);
                cmd.Parameters.AddWithValue("@Segundo_Nombre", estudiante.Segundo_Nombre ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Primer_Apellido", estudiante.Primer_Apellido);
                cmd.Parameters.AddWithValue("@Segundo_Apellido", estudiante.Segundo_Apellido);
                cmd.Parameters.AddWithValue("@Nacimiento", estudiante.Nacimiento);
                cmd.Parameters.AddWithValue("@Direccion", estudiante.Direccion);
                cmd.Parameters.AddWithValue("@Padecimientos", estudiante.Padecimientos ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Nombre_Madre", estudiante.Nombre_Madre);
                cmd.Parameters.AddWithValue("@Nombre_Padre", estudiante.Nombre_Padre);
                cmd.Parameters.AddWithValue("@Celular", estudiante.Celular);
                cmd.Parameters.AddWithValue("@Casa", estudiante.Casa);
                cmd.Parameters.AddWithValue("@TelEncargado", estudiante.TelEncargado);
                cmd.Parameters.AddWithValue("@Correo", estudiante.Correo ?? (object)DBNull.Value);
                cmd.Parameters.Add("@Foto", System.Data.SqlDbType.VarBinary).Value = estudiante.Foto ?? (object)DBNull.Value;

                int filasAfectadas = cmd.ExecuteNonQuery();

                if (filasAfectadas > 0)
                {
                    if (string.IsNullOrWhiteSpace(usuario))
                    {
                        MessageBox.Show("No se pudo registrar en bitácora porque el nombre de usuario es nulo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        transaction.Rollback();
                        return false;
                    }

                    string observaciones = $"INSERT Identificacion {estudiante.Identificacion}";

                    SqlCommand cmdBitacora = new SqlCommand(@"INSERT INTO Bitacora_Movimientos (Usuario,Movimiento,Fecha, Observaciones)
                                                        VALUES (@Usuario, @Movimiento, @Fecha , @Observaciones)", conn, transaction);
                    cmdBitacora.Parameters.AddWithValue("@Usuario", usuario);
                    cmdBitacora.Parameters.AddWithValue("@Movimiento", "INSERT");
                    cmdBitacora.Parameters.AddWithValue("@Fecha", DateTime.Now);
                    cmdBitacora.Parameters.AddWithValue("@Observaciones", observaciones);

                    cmdBitacora.ExecuteNonQuery();

                    transaction.Commit();
                    return true;
                }
                else
                {
                    transaction.Rollback();
                    return false;
                }
            }

        }
        catch (SqlException ex)
        {
            if (ex.Number == 2627)
            {
                MessageBox.Show("Ya existe un estudiante con esta identificación", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Ocurrió un error al insertar el estudiante:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
    
    }


    //MÉTODO QUE PERMITE CONSULTAR ESTUDIANTES A TRAVÉS DE SU IDENTIFICACIÓN
    public static ModeloEstudiante ConsultarXIdentificacion(string Identificacion)
    {
        ModeloEstudiante estudiante = null;
        try
        {



            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Estudiantes WHERE Identificacion = @Identificacion";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Identificacion", Identificacion);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            estudiante = new ModeloEstudiante
                            {
                                Identificacion = reader["Identificacion"].ToString(),
                                Primer_Nombre = reader["Primer_Nombre"].ToString(),
                                Segundo_Nombre = reader["Segundo_Nombre"].ToString(),
                                Primer_Apellido = reader["Primer_Apellido"].ToString(),
                                Segundo_Apellido = reader["Segundo_Apellido"].ToString(),
                                Nacimiento = Convert.ToDateTime(reader["Nacimiento"]),
                                Direccion = reader["Direccion"].ToString(),
                                Nombre_Madre = reader["Nombre_Madre"].ToString(),
                                Nombre_Padre = reader["Nombre_Padre"].ToString(),
                                Celular = reader["Celular"].ToString(),
                                Casa = reader["Casa"].ToString(),
                                TelEncargado = reader["TelEncargado"].ToString(),
                                Correo = reader["Correo"].ToString(),
                                Padecimientos = reader["Padecimientos"].ToString(),
                                Foto = reader["Foto"] != DBNull.Value ? (byte[])reader["Foto"] : null
                            };
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error al consultar el estudiante: " + ex.Message);
        } return estudiante;
    }




    //MÉTODO QUE PERMITE ACTUALIZAR ESTUDIANTES EN LA BASE DE DATOS
    public bool Actualizar(string usuario)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                string query = @"UPDATE Estudiantes SET
                Primer_Nombre = @Primer_Nombre,
                Segundo_Nombre = @Segundo_Nombre,
                Primer_Apellido = @Primer_Apellido,
                Segundo_Apellido = @Segundo_Apellido,
                Nacimiento = @Nacimiento,
                Direccion = @Direccion,
                Nombre_Madre = @Nombre_Madre,
                Nombre_Padre = @Nombre_Padre,
                Celular = @Celular,
                Casa = @Casa,
                TelEncargado = @TelEncargado,
                Correo = @Correo,
                Padecimientos = @Padecimientos,
                Foto = @Foto
                WHERE Identificacion = @Identificacion";

            using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
            {
                    cmd.Parameters.AddWithValue("@Primer_Nombre", Primer_Nombre);
                    cmd.Parameters.AddWithValue("@Segundo_Nombre", Segundo_Nombre);
                    cmd.Parameters.AddWithValue("@Primer_Apellido", Primer_Apellido);
                    cmd.Parameters.AddWithValue("@Segundo_Apellido", Segundo_Apellido);
                    cmd.Parameters.AddWithValue("@Nacimiento", Nacimiento);
                    cmd.Parameters.AddWithValue("@Direccion", Direccion);
                    cmd.Parameters.AddWithValue("@Nombre_Madre", Nombre_Madre);
                    cmd.Parameters.AddWithValue("@Nombre_Padre", Nombre_Padre);
                    cmd.Parameters.AddWithValue("@Celular", Celular);
                    cmd.Parameters.AddWithValue("@Casa", Casa);
                    cmd.Parameters.AddWithValue("@TelEncargado", TelEncargado);
                    cmd.Parameters.AddWithValue("@Correo", Correo);
                    cmd.Parameters.AddWithValue("@Padecimientos", Padecimientos);
                    cmd.Parameters.AddWithValue("@Identificacion", Identificacion);
                    cmd.Parameters.AddWithValue("@Foto", Foto);
                    int filas = cmd.ExecuteNonQuery();

                    if (filas > 0)
                    {
                        string observaciones = $"UPDATE Identificacion {Identificacion}";

                        SqlCommand cmdBitacora = new SqlCommand(@"INSERT INTO Bitacora_Movimientos (Usuario,Movimiento,Fecha, Observaciones)
                                                              VALUES (@Usuario, @Movimiento, @Fecha , @Observaciones)", conn, transaction);

                        cmdBitacora.Parameters.AddWithValue("@Usuario", usuario);
                        cmdBitacora.Parameters.AddWithValue("@Movimiento", "UPDATE");
                        cmdBitacora.Parameters.AddWithValue("@Fecha", DateTime.Now);
                        cmdBitacora.Parameters.AddWithValue("@Observaciones", observaciones);

                        cmdBitacora.ExecuteNonQuery();

                        transaction.Commit();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        return false;
                    }
                }

            }
        }
        catch (Exception ex)
        {

            MessageBox.Show("Ocurrió un error al actualizar al estudiante: " + ex.Message);
            return false;
        }
  
    }



    //MÉTODO QUE PERMITE ELIMINAR ESTUDIANTES DE LA BASE DE DATOS
    public static bool Eliminar(String Identificacion, string usuario)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();


                string query = "DELETE FROM Estudiantes WHERE Identificacion = @Identificacion";
                using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@Identificacion", Identificacion);
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        string observaciones = $"DELETE Identificación: {Identificacion}";

                        string queryBitacora = @"INSERT INTO Bitacora_Movimientos (Usuario, Movimiento, Fecha, Observaciones)
                                             VALUES (@Usuario, @Movimiento, @Fecha, @Observaciones)";
                        using (SqlCommand cmdBitacora = new SqlCommand(queryBitacora, conn, transaction))
                        {
                            cmdBitacora.Parameters.AddWithValue("@Usuario", usuario);
                            cmdBitacora.Parameters.AddWithValue("@Movimiento", "DELETE");
                            cmdBitacora.Parameters.AddWithValue("@Fecha", DateTime.Now);
                            cmdBitacora.Parameters.AddWithValue("@Observaciones", observaciones);

                            cmdBitacora.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ocurrió un error al eliminar el estudiante: " + ex.Message);
            return false;
        }
    }
            

}

