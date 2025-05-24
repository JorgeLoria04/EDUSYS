using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class ReportesController
{
    private ModeloReportes modelo = new ModeloReportes();

    public DataTable CargarReportesEstudiantes()
    {
        return modelo.ObtenerEstudiantes();
    }

    public DataTable CargarReporteMovimientos()
    {
        return modelo.ObtenerMovimientosMatricula();
    }

    public DataTable FiltrarEstudiantesPorNombre(string nombre)
    { 
        return modelo.ObtenerEstudiantesFiltrados(nombre);
    }

    public DataTable FiltrarEstudiantesPorNacimiento(string fechaNacimiento)
    {
        return modelo.ObtenerEstudiantesPorNacimiento(fechaNacimiento);
    }

    public DataTable ObtenerReporteBitacora(DateTime fechaInicio, DateTime fechaFin, string usuario, string tipoMovimiento)
    {
        return modelo.ObtenerBitacoraMovimientos();
    }

}

