using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

public class DatabaseConnection
{
    private string connectionString = "Server=DESKTOP-ELGAIO4\\SQLEXPRESS;Database=EDUSYS;Integrated Security=True;";

    public void Connect()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine("¡Conexión exitosa a Azure SQL Database!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error de conexión: " + ex.Message);
            }
        }
    }
}