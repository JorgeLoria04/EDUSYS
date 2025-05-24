using EDUSYS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

    public class ModeloLogin
    {

    private string conexion = "Server=DESKTOP-ELGAIO4\\SQLEXPRESS;Database=EDUSYS;Integrated Security=True;";

    public int ID {  get; set; }
        public string Usuario {  get; set; }
        public DateTime FechaEntrada { get; set; }
        public DateTime FechaSalida { get; set; }

        public static string UsuarioLogueado { get; set; }


    //MÉTODO QUE REGISTRA EL INGRESO AL SISTEMA DEL USUARIO EN LA BITÁCORA DE ENTRADAS Y SALIDAS
    public int RegistrarIngreso(ModeloLogin login)
    {
        int idbitacora = -1;
        using (SqlConnection conn = new SqlConnection(conexion))
        {
            conn.Open();
            string query = @"INSERT INTO Bitacora_Entradas_Salidas (Usuario, Fecha_Ingreso) 
            OUTPUT INSERTED.ID_Movimiento 
            VALUES (@USUARIO, @Fecha_Ingreso)";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                DateTime ahora = DateTime.Now;
                login.FechaEntrada = new DateTime(ahora.Year, ahora.Month, ahora.Day, ahora.Hour, ahora.Minute, 0);
                cmd.Parameters.AddWithValue("@Usuario", login.Usuario);
                cmd.Parameters.AddWithValue("@Fecha_Ingreso", login.FechaEntrada);
                idbitacora = (int)cmd.ExecuteScalar();

                if (idbitacora == -1)
                {
                    MessageBox.Show("Error al generar el ID de bitácora.");
                }
            }
        }

        return idbitacora;
    }


    //MÉTODO QUE REGISTRA LA SALIDA DEL SISTEMA DEL USUARIO EN LA BITÁCORA DE ENTRADAS Y SALIDAS
    public void RegistrarSalida(int idbitacora)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(conexion))
            {
                conn.Open();
                string query = @"UPDATE Bitacora_Entradas_Salidas
                         SET Fecha_Salida = @Fecha_Salida
                         WHERE ID_Movimiento = @ID_Movimiento";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    DateTime ahora = DateTime.Now;
                    DateTime fechaSalida = new DateTime(ahora.Year, ahora.Month, ahora.Day, ahora.Hour, ahora.Minute, 0);

                    cmd.Parameters.AddWithValue("@Fecha_Salida", fechaSalida);
                    cmd.Parameters.AddWithValue("@ID_Movimiento", idbitacora);

                    Console.WriteLine($"Registrando salida con ID: {idbitacora} a las {fechaSalida}"); // Agregar log
                    cmd.ExecuteNonQuery();
                }
            }
        }catch (Exception ex)
        {
            Console.WriteLine($"Error al registrar salida: {ex.Message}");
            MessageBox.Show("Ocurrió un error al registrar la salida: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }


    //MÉTODO QUE VALORA QUE EL USUARIO SI EXISTA ANTES DE INICIAR SESIÓN EN EL SISTEMA
    public bool ValidarUsuario(string Usuario, string Password)
    {
        using (SqlConnection conn = new SqlConnection(conexion))
        {
            conn.Open();
            string query = "SELECT Password, Salt FROM Usuarios WHERE Usuario = @Usuario";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Usuario", Usuario);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    string PasswordEncriptada = reader["Password"].ToString();
                    string salt = reader["Salt"].ToString();

                    string hashIngresado = EncriptarPassword(Password, salt);

                    return hashIngresado == PasswordEncriptada;
                }

                return false;
            }
        }
    }


    //MÉTODO DE ENCRIPTACIÓN PARA LAS CONTRASEÑAS 
    public string EncriptarPassword(string Password, string salt)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            string PasswordconSal = Password + salt;

            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(PasswordconSal));

            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }


    //MÉTODO QUE ACTUALIZA LA CONTRASEÑA DE UN USUARIO EN CASO DE ACCEDER AL FORMULARIO DE RECUPERACIÓN
    public bool ActualizarPassword(string Usuario, string NuevaPassword)
    {
        try
        {
            string Salt = GenerarSalt();

            string passwordEncriptada = EncriptarPassword(NuevaPassword, Salt);

            using (SqlConnection conn = new SqlConnection(conexion))
            {
                conn.Open();
                string query = "UPDATE Usuarios SET Password = @Password, Salt = @salt WHERE Usuario = @Usuario";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Password", passwordEncriptada);
                    cmd.Parameters.AddWithValue("@Salt", Salt);
                    cmd.Parameters.AddWithValue("@Usuario", Usuario);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        catch (Exception ex)
        {

            MessageBox.Show("Ocurrió un error al actualiza la contraseña: " + ex.Message);
            return false;
        }
    }


    //MÉTODO NECESARIO PARA LA ENCRIPTACIÓN DE LAS CONTRASEÑAS
    private string GenerarSalt()
    {
        using (System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
        {
            byte[] salt = new byte[32];
            rng.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }
    }


    // MÉTODO QUE PERMITE OBTENER EL ID DE UN ROL POR MEDIO DE SU NOMBRE
    public int ObtenerID_RolXNombre(string Nombre_Rol)
    {
        using (SqlConnection conn = new SqlConnection(conexion))
        {
            conn.Open();
            string query = "SELECT ID_Rol FROM Roles WHERE Nombre_Rol = @Nombre_Rol";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Nombre_Rol", Nombre_Rol);
                object resultado = cmd.ExecuteScalar();

                return resultado != null ? Convert.ToInt32(resultado) : -1;
            }
        }
    }



    //MÉTODO QUE VERIFICA QUE UN USUARIO EXISTE ANTES DE CAMBIAR SU CONTRASEÑA
    public bool VerificarExistenciaUsuario(string Usuario)
    {
        using (SqlConnection conn = new SqlConnection(conexion))
        {
            conn.Open();
            string query = "SELECT COUNT(*) FROM Usuarios WHERE Usuario = @Usuario";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Usuario", Usuario);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
    }

}




