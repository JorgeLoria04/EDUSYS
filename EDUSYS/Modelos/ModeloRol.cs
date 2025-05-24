using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUSYS.Modelos
{
    public class ModeloRol
    {
        private string conexion = "Server=DESKTOP-ELGAIO4\\SQLEXPRESS;Database=EDUSYS;Integrated Security=True;";

        public int ID_Rol { get; set; }
        public string Nombre_Rol { get; set; }

        public override string ToString()
        {
            return Nombre_Rol;
        }



        //FUNCIÓN PARA AGREGAR UN NUEVO ROL
        public bool AgregarRol(string Nombre_Rol)
        {
            using (SqlConnection conn = new SqlConnection(conexion))
            {
                conn.Open();
                string query = "INSERT INTO Roles (Nombre_Rol) VALUES (@Nombre_Rol)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Nombre_Rol", Nombre_Rol);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }



        //FUNCIÓN PARA OBTENER LOS ROLES GUARDADOS EN UNA BASE DE DATOS
        public List<ModeloRol> ObtenerRoles()
        {
            List<ModeloRol> roles = new List<ModeloRol>();

            using (SqlConnection conn = new SqlConnection(conexion))
            {
                conn.Open();
                string query = "SELECT ID_Rol, Nombre_Rol FROM Roles";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        roles.Add(new ModeloRol
                        {
                            ID_Rol = Convert.ToInt32(reader["ID_Rol"]),
                            Nombre_Rol = reader["Nombre_Rol"].ToString()
                        });
                    }
                }
            }

            return roles;
        }


        public bool ExisteUsuariosConRol(int idRol)
        {
            using (SqlConnection conn = new SqlConnection(conexion))
            {
                string query = "SELECT COUNT(*) FROM Usuarios WHERE ID_Rol = @IdRol";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdRol", idRol);

                conn.Open();
                int cantidad = (int)cmd.ExecuteScalar();
                return cantidad > 0;
            }
        }



        //FUNCIÓN PARA ELIMINAR UN ROL DE LA BASE DE DATOS
        public bool EliminarRol(int idRol, out string mensaje)
        {
            mensaje = "";

            if (ExisteUsuariosConRol(idRol))
            {
                mensaje = "No se puede eliminar el rol porque tiene usuarios asignados.";
                return false;
            }

            using (SqlConnection conn = new SqlConnection(conexion))
            {
                string sql = "DELETE FROM Roles WHERE ID_Rol = @ID_Rol";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ID_Rol", idRol);

                try
                {
                    conn.Open();
                    int filas = cmd.ExecuteNonQuery();
                    return filas > 0;                
                }
                catch (Exception ex)
                {
                    mensaje = "Error al eliminar el rol: " + ex.Message;
                    return false;
                }
            }
        }


        public bool TienePermisosAsignados(int idRol)
        {
            bool tienePermisos = false;

            using (SqlConnection conn = new SqlConnection(conexion))
            {
                string query = "SELECT COUNT(*) FROM Roles_Permisos WHERE ID_Rol = @ID_Rol";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID_Rol", idRol);
                    conn.Open();

                    int cantidad = (int)cmd.ExecuteScalar();
                    tienePermisos = cantidad > 0;
                }
            }

            return tienePermisos;
        }



    }
}
