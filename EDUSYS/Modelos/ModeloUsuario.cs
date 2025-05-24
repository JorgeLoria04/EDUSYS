using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class ModeloUsuario
{

    private string conexion = "Server=DESKTOP-ELGAIO4\\SQLEXPRESS;Database=EDUSYS;Integrated Security=True;";

    public int ID_Usuario { get; set; }

    public string Usuario { get; set; }

    public string Password { get; set; }

    public string Salt { get; set; }

    public int ID_Rol { get; set; }


    //FUNCIÓN PARA ENCRIPTAR LAS CONTRASEÑAS DE LOS USUARIOS
    public string EncriptarPassword(string Password, string Salt)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            string PasswordConSalt = Password + Salt;
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(PasswordConSalt));

            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }


    //FUNCIÓN PARTE DE LA ENCRIPTACIÓN DE LAS CONTRASEÑAS
    public string GenerarSalt()
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] salt = new byte[32];
            rng.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }
    }


    // FUNCIÓN PARA OBTENER EL ID DEL ROL QUE CONTIENE UN USUARIO
        public static int ObtenerID_RolPorUsuario(int ID_Usuario)
        {
            int ID_Rol = -1; 

            try
            {
            using (SqlConnection conn = new SqlConnection("Server=DESKTOP-ELGAIO4\\SQLEXPRESS;Database=EDUSYS;Integrated Security=True;"))
            {
                conn.Open();

                string query = "SELECT ID_Rol FROM Usuarios WHERE ID_Usuario = @ID_Usuario";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID_Usuario", ID_Usuario);
                    ID_Rol = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el rol del usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return ID_Rol;
        }



    //FUNCIÓN PARA REGISTRAR UN NUEVO USUARIO EN LA BASE DE DATOS
    public bool RegistrarUsuario(string Usuario, string Password, int ID_Rol, out string mensaje)
    {
        mensaje = string.Empty;

        try
        {
            using (SqlConnection conn = new SqlConnection(conexion))
            {
                string query = "INSERT INTO Usuarios (Usuario, Password, ID_Rol) VALUES (@Usuario, @Password, @ID_Rol)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Usuario", Usuario);
                    cmd.Parameters.AddWithValue("@Password", Password);
                    cmd.Parameters.AddWithValue("@ID_Rol", ID_Rol);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        mensaje = "Usuario registrado exitosamente";
                        return true;
                    }
                    else
                    {
                        mensaje = "No se pudo registrar el usuario";
                        return false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            mensaje = "Error: " + ex.Message;
            return false;
        }
    }


    // FUNCIÓN PARA ACTUALIZAR UN USUARIO PRESENTE EN LA BASE DE DATOS
    public bool ActualizarUsuario(string usuarioOriginal, string nuevoUsuario, string nuevaPassword, int idRol)
    {
        using (SqlConnection conn = new SqlConnection(conexion))
        {
            string query = @"UPDATE Usuarios 
                         SET Usuario = @NuevoUsuario, 
                             ID_Rol = @ID_Rol" +
                                 (nuevaPassword != null ? ", Password = @Password, Salt = @Salt" : "") +
                             " WHERE Usuario = @UsuarioOriginal";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@UsuarioOriginal", usuarioOriginal);
            cmd.Parameters.AddWithValue("@NuevoUsuario", nuevoUsuario);
            cmd.Parameters.AddWithValue("@ID_Rol", idRol);

            if (nuevaPassword != null)
            {
                string salt = Guid.NewGuid().ToString();
                string passwordEncriptada = EncriptarPassword(nuevaPassword, salt);

                cmd.Parameters.AddWithValue("@Password", passwordEncriptada);
                cmd.Parameters.AddWithValue("@Salt", salt);
            }

            conn.Open();
            int filasAfectadas = cmd.ExecuteNonQuery();
            return filasAfectadas > 0;
        }
    }


    //FUNCIÓN PARA ELIMINAR UN USUARIO DE LA BASE DE DATOS
    public bool EliminarUsuario(string Usuario)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(conexion))
            {
                conn.Open();

                string verificarQuery = "SELECT COUNT(*) FROM Usuarios WHERE Usuario = @Usuario";
                using (SqlCommand verificarCmd = new SqlCommand(verificarQuery, conn))
                {
                    verificarCmd.Parameters.AddWithValue("@Usuario", Usuario);
                    int count = (int)verificarCmd.ExecuteScalar();

                    if (count == 0)
                        return false;
                }

                if (Usuario.ToLower() == "admin")
                    throw new Exception("No se puede eliminar el usuario administrador.");


                string Query = "DELETE FROM Usuarios WHERE Usuario = @Usuario";
                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@Usuario", Usuario);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error al eliminar el usuario: " + ex.Message);
        }
    }


    //FUNCIÓN PARA OBTENER LOS USUARIOS PRESENTES EN LA BASE DE DATOS
    public DataTable ObtenerUsuarios()
    {
        DataTable tabla = new DataTable();
        using (SqlConnection conn = new SqlConnection(conexion))
        {
            string query = @"SELECT U.Usuario, R.Nombre_Rol 
                         FROM Usuarios U
                         INNER JOIN Roles R ON U.ID_Rol = R.ID_Rol";

            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            da.Fill(tabla);
        }
        return tabla;
    }


    // FUNCIÓN PARA OBTENER EL ID DE UN ROL POR SU NOMBRE
    public int ObtenerID_RolXNombre(string nombreRol)
    {
        int idRol = -1;
        using (SqlConnection conn = new SqlConnection(conexion))
        {
            conn.Open();
            string query = @"SELECT ID_Rol FROM Roles WHERE Nombre_Rol = @NombreRol";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@NombreRol", nombreRol);

                // Obtener el ID
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    idRol = Convert.ToInt32(result);
                }
            }
        }
        return idRol;
    }



    // FUNCIÓN PARA BUSCAR UN USUARIO POR SU NOMBRE DE USUARIO
    public static ModeloUsuario BuscarPorUsuario(string Usuario)
    {
        ModeloUsuario usuario = null;
        string conexion = "Server=DESKTOP-ELGAIO4\\SQLEXPRESS;Database=EDUSYS;Integrated Security=True;";

        try
        {
            using (SqlConnection conn = new SqlConnection(conexion))
            {
                conn.Open();
                string query = "SELECT * FROM Usuarios WHERE Usuario = @Usuario";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Usuario", Usuario);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        usuario = new ModeloUsuario
                        {
                            ID_Usuario = Convert.ToInt32(reader["ID_Usuario"]),
                            Usuario = reader["Usuario"].ToString(),
                            Password = reader["Password"].ToString(),
                            Salt = reader["Salt"].ToString(),
                            ID_Rol = Convert.ToInt32(reader["ID_Rol"])
                        };
                    }
                    reader.Close();
                }
            }
        }
        catch (Exception ex)
        {
         
            Console.WriteLine("Error al buscar el usuario: " + ex.Message);
        }

        return usuario;
    }



    //FUNCIÓN PARA OBTENER TODOS LOS ROLES DISPONIBLES PARA LOS USUARIOS
    public DataTable ObtenerTodosLosRoles()
    {
        DataTable dt = new DataTable();
        using (SqlConnection conn = new SqlConnection(conexion))
        {
            conn.Open();
            string query = "SELECT * FROM Roles";
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                da.Fill(dt);
            }
        }
        return dt;
    }


    //FUNCIÓN PARA OBTENER EL ID DE UN USUARIO A TRAVÉS DE SU NOMBRE
    public static int ObtenerIDUsuarioPorNombre(string usuario)
    {
        int idUsuario = -1;

        string conexion = "Server=DESKTOP-ELGAIO4\\SQLEXPRESS;Database=EDUSYS;Integrated Security=True;";

        using (SqlConnection conn = new SqlConnection(conexion))
        {
            conn.Open();
            string query = "SELECT ID_Usuario FROM Usuarios WHERE Usuario = @Usuario";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Usuario", usuario);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        idUsuario = Convert.ToInt32(reader["ID_Usuario"]);
                    }
                }
            }
        }

        return idUsuario;
    }


    public bool ExisteNombreUsuario(string nombreUsuario)
    {
        bool existe = false;

        using (SqlConnection conn = new SqlConnection(conexion))
        {
            string query = "SELECT COUNT(*) FROM Usuarios WHERE Usuario = @Usuario";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Usuario", nombreUsuario);

            conn.Open();
            int cantidad = (int)cmd.ExecuteScalar();
            existe = cantidad > 0;
        }

        return existe;
    }

}

