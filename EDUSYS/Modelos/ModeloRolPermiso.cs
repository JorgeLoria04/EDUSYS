using EDUSYS.Controladores;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDUSYS.Modelos
{
    public class ModeloRolPermiso
    {
        public int ID_Rol {  get; set; }
        public string Nombre_Permiso {  get; set; }

        private string conexion = "Server=DESKTOP-ELGAIO4\\SQLEXPRESS;Database=EDUSYS;Integrated Security=True;";



        //FUNCIÓN PARA OBTENER LOS PERMISOS A TRAVÉS DE SU ROL
        public List<int> ObtenerPermisosPorRol(int ID_Rol)
        {
            List<int> permisos = new List<int>();

            using (SqlConnection conn = new SqlConnection(conexion))
            {
                conn.Open();
                string query = "SELECT ID_Permiso FROM Roles_Permisos WHERE ID_Rol = @ID_Rol";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID_Rol", ID_Rol);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            permisos.Add(Convert.ToInt32(reader["ID_Permiso"]));
                        }
                    }
                }
            }

            return permisos;
        }



        //FUNCIÓN PARA ASIGNAR LOS PERMISOS A UN ROL
        public bool AsignarPermisos(int ID_Rol, List<int> permisosSeleccionados)
        {
            using (SqlConnection conn = new SqlConnection(conexion))
            {
                conn.Open();

                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    string deleteQuery = "DELETE FROM Roles_Permisos WHERE ID_Rol = @ID_Rol";
                    using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn, transaction))
                    {
                        deleteCmd.Parameters.AddWithValue("@ID_Rol", ID_Rol);
                        deleteCmd.ExecuteNonQuery();
                    }

                    foreach (int ID_Permiso in permisosSeleccionados)
                    {
                        string insertQuery = "INSERT INTO Roles_Permisos (ID_Rol, ID_Permiso) VALUES (@ID_Rol, @ID_Permiso)";
                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn, transaction))
                        {
                            insertCmd.Parameters.AddWithValue("@ID_Rol", ID_Rol);
                            insertCmd.Parameters.AddWithValue("@ID_Permiso", ID_Permiso);
                            insertCmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }



        //FUNCIÓN PARA GUARDAR LOS PERMISOS QUE SE ASIGNARON A UN ROL
        public bool GuardarPermisosDelRol(int ID_Rol, List<int> ID_Permisos)
        {
            using (SqlConnection conn = new SqlConnection(conexion))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {

                    string deleteQuery = "DELETE FROM Roles_Permisos WHERE ID_Rol = @ID_Rol";
                    using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn, transaction))
                    {
                        deleteCmd.Parameters.AddWithValue("@ID_Rol", ID_Rol);
                        deleteCmd.ExecuteNonQuery();
                    }

                    foreach (int idPermiso in ID_Permisos)
                    {
                        string insertQuery = "INSERT INTO Roles_Permisos (ID_Rol, ID_Permiso) VALUES (@ID_Rol, @ID_Permiso)";
                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn, transaction))
                        {
                            insertCmd.Parameters.AddWithValue("@ID_Rol", ID_Rol);
                            insertCmd.Parameters.AddWithValue("@ID_Permiso", idPermiso);
                            insertCmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }







    }



}


    

