using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ModeloPermiso
{
    private string conexion = "Server=DESKTOP-ELGAIO4\\SQLEXPRESS;Database=EDUSYS;Integrated Security=True;";

    public int ID_Permiso { get; set; }
    public string Nombre_Permiso { get; set; }


    public override string ToString()
    {
        return Nombre_Permiso;
    }


    //FUNCIÓN PARA AGREGAR UN NUEVO PERMISO
    public bool AgregarPermiso(string Nombre_Permiso)
    {
        using (SqlConnection conn = new SqlConnection(conexion))
        {
            conn.Open();
            string query = "INSERT INTO Permisos (Nombre_Permiso) VALUES (@Nombre_Permiso)";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Nombre_Permiso", Nombre_Permiso);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }


    public bool TienePermisosAsignados(int idPermiso)
    {
        using (SqlConnection conn = new SqlConnection(conexion))
        {
            string query = "SELECT COUNT(*) FROM Roles_Permisos WHERE ID_Permiso = @ID_Permiso";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ID_Permiso", idPermiso);

            conn.Open();
            int cantidad = (int)cmd.ExecuteScalar();
            return cantidad > 0;
        }
    }



    //FUNCIÓN PARA OBTENER TODOS LOS PERMISOS GUARDADOS EN LA BASE DE DATOS
    public List<ModeloPermiso> ObtenerPermisos()
    {
        List<ModeloPermiso> permisos = new List<ModeloPermiso>();


        using (SqlConnection conn = new SqlConnection(conexion))
        {
            conn.Open();
            string query = "SELECT ID_Permiso, Nombre_Permiso FROM Permisos";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    permisos.Add(new ModeloPermiso
                    {
                        ID_Permiso = Convert.ToInt32(reader["ID_Permiso"]),
                        Nombre_Permiso = reader["Nombre_Permiso"].ToString()
                    });
                }
            }

            return permisos;
        }
    }


    //FUNCIÓN PARA ELIMINAR UN PERMISO DE LA BASE DE DATOS
    public bool EliminarPermiso(int ID_Permiso)
    {
        using (SqlConnection conn = new SqlConnection(conexion))
        {
            conn.Open();
            string query = "DELETE FROM Permisos WHERE ID_Permiso = @ID_Permiso";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ID_Permiso", ID_Permiso);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
        
  
