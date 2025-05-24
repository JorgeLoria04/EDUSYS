using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class ModeloReportes
{

    private string conexion = "Server=DESKTOP-ELGAIO4\\SQLEXPRESS;Database=EDUSYS;Integrated Security=True;";


    //FUNCIÓN PARA OBTENER LOS ESTUDIANTES NECESARIOS PARA LOS REPORTES
    public DataTable ObtenerEstudiantes()
    {
        using (SqlConnection conn = new SqlConnection(conexion))
        {
            string query = @"SELECT Identificacion, Primer_Nombre, Segundo_Nombre, Primer_Apellido, Segundo_Apellido,Nacimiento, Direccion, Padecimientos, Nombre_Madre, Nombre_Padre, Telefono, Correo
                            FROM Estudiantes";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            DataTable tabla = new DataTable();
            adapter.Fill(tabla);
            return tabla;
        }
    }


    //FUNCIÓN PARA OBTENER LOS MOVIMIENTOS DE MATRÍCULA NECESARIOS PARA LOS REPORTES
    public DataTable ObtenerMovimientosMatricula()
    {
        using (SqlConnection conn = new SqlConnection(conexion))
        {
            string query = @"SELECT Fecha_Movimiento, Identificacion_Estudiante, Tipo_Movimiento, Genero, Observaciones 
                             FROM Movimientos_Matricula";

            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            DataTable tabla = new DataTable();
            adapter.Fill(tabla);
            return tabla;
        }
    }


    //FUNCIÓN PARA OBTENER LOS NOMBRES ÚNICOS DE LOS ESTUDIANTES PARA LOS REPORTES
    public DataTable ObtenerNombresUnicos()
    {
        using (SqlConnection conn = new SqlConnection(conexion))
        {
            string query = "SELECT DISTINCT Primer_Nombre FROM Estudiantes ORDER BY Primer_Nombre";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            DataTable tabla = new DataTable();
            adapter.Fill(tabla);
            return tabla;
        }
    }


    //FUNCIÓN PARA OBTENER LOS ESTUDIANTES A TRAVÉS DE SU NOMBRE
    public DataTable ObtenerEstudiantesPorNombre(string Nombre)
    {
        using (SqlConnection conn = new SqlConnection(conexion))
        {
            string query = @"SELECT * FROM Estudiantes WHERE Primer_Nombre = @Nombre";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.Parameters.AddWithValue("@Nombre", Nombre);
            DataTable tabla = new DataTable();
            adapter.Fill(tabla);
            return tabla;
        }
    }


    //FUNCIÓN PARA OBTENER A LOS ESTUDIANTES FILTRADOS
    public DataTable ObtenerEstudiantesFiltrados(string nombre)
    {
        try
        {
            using (var connection = new SqlConnection(conexion))
            {
                string query = "SELECT * FROM Estudiantes";

                if (!string.IsNullOrEmpty(nombre))
                {
                    query += " WHERE Primer_Nombre LIKE @nombre";
                }

                SqlCommand command = new SqlCommand(query, connection);

                if (!string.IsNullOrEmpty(nombre))
                {
                    command.Parameters.AddWithValue("@nombre", "%" + nombre + "%");
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al obtener los estudiantes: " + ex.Message);
            return new DataTable();
        }
    }


    //FUNCIÓN PARA OBTENER LOS ESTUDIANTES POR SU FECHA DE NACIMIENTO
    public DataTable ObtenerEstudiantesPorNacimiento(string fechaNacimiento)
    {
        using (SqlConnection conn = new SqlConnection(conexion))
        {
            string query = @"SELECT * FROM Estudiantes WHERE CONVERT(date, Nacimiento) = @Fecha";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.Parameters.AddWithValue("@Fecha", fechaNacimiento);
            DataTable tabla = new DataTable();
            adapter.Fill(tabla);
            return tabla;
        }
    }



    //FUNCIÓN PARA OBTENER LOS MOVIMIENTOS FILTRADOS
    public DataTable ObtenerMovimientosFiltrados(string genero, string tipoMovimiento)
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(conexion))
        {
            string query = @"SELECT Fecha_Movimiento, Identificacion_Estudiante, Tipo_Movimiento, Genero, Observaciones
                         FROM Movimientos_Matricula
                         WHERE Genero LIKE @Genero AND Tipo_Movimiento LIKE @Tipo_Movimiento";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Genero", genero);
                cmd.Parameters.AddWithValue("@Tipo_Movimiento", tipoMovimiento);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
        }
        return dt;
    }



    //FUNCIÓN PARA OBTENER LA BITÁCORA DE ENTRADAS Y SALIDAS
    public DataTable ObtenerBitacora()
    {
        DataTable tabla = new DataTable();

        using (SqlConnection con = new SqlConnection(conexion))
        {
            string query = @"SELECT ID_Movimiento,Usuario, Fecha_Ingreso, Fecha_Salida FROM Bitacora_Entradas_Salidas";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
            }
        }

        return tabla;
    }


    //FUNCIÓN PARA OBTENER LA BITÁCORA DE ENTRADAS Y SALIDAS FILTRADA
    public DataTable ObtenerBitacoraFiltrada(string usuario, DateTime desde, DateTime hasta)
    {
        DataTable dt = new DataTable();

        using (SqlConnection conn = new SqlConnection(conexion))
        {
            conn.Open();

            string query = @"SELECT ID_Movimiento,Usuario, Fecha_Ingreso, Fecha_Salida
                         FROM Bitacora_Entradas_Salidas 
                         WHERE 1 = 1";

            if (!string.IsNullOrEmpty(usuario))
            {
                query += " AND Usuario LIKE @usuario";
            }

            query += " AND CAST(Fecha_Ingreso AS DATE) BETWEEN @desde AND @hasta";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (!string.IsNullOrEmpty(usuario))
                    cmd.Parameters.AddWithValue("@usuario", "%" + usuario + "%");

                cmd.Parameters.AddWithValue("@desde", desde);
                cmd.Parameters.AddWithValue("@hasta", hasta);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
        }

        return dt;
    }



    // FUNCIÓN PARA OBTENER LA BITÁCORA DE MOVIMIENTOS
    public DataTable ObtenerBitacoraMovimientos()
    {
        DataTable dt = new DataTable();
        using (SqlConnection conn = new SqlConnection(conexion))
        {
            string query = "SELECT * FROM Bitacora_Movimientos";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
        }
        return dt;
    }



    //FUNCIÓN PARA OBTENER LA BITÁCORA DE MOVIMIENTOS FILTRADA 
    public DataTable ObtenerBitacoraMovimientosFiltrada(string usuario, string tipoMovimiento, DateTime desde, DateTime hasta)
    {
        DataTable dt = new DataTable();
        using (SqlConnection conn = new SqlConnection(conexion))
        {
            string query = @"
            SELECT * 
            FROM Bitacora_Movimientos 
            WHERE Fecha BETWEEN @Desde AND @Hasta
              AND (@Usuario = '' OR Usuario LIKE '%' + @Usuario + '%')
              AND (@TipoMovimiento = '' OR Movimiento = @TipoMovimiento)";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Desde", desde);
                cmd.Parameters.AddWithValue("@Hasta", hasta);
                cmd.Parameters.AddWithValue("@Usuario", usuario);
                cmd.Parameters.AddWithValue("@TipoMovimiento", tipoMovimiento);

                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
        }
        return dt;
    }


}



