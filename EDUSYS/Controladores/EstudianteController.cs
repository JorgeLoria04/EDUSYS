using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 public class EstudianteController
    {
    public bool GuardarEstudiante(ModeloEstudiante estudiante, string usuario)
    {
            return ModeloEstudiante.GuardarEstudiante(estudiante, usuario);
    }

    public ModeloEstudiante ConsultarEstudiante(string Identificacion)
    {
        return ModeloEstudiante.ConsultarXIdentificacion(Identificacion);
    }

    public bool ModificarEstudiante(ModeloEstudiante estudiante, string usuario)
    {
        return estudiante.Actualizar(usuario);
    }


    public bool EliminarEstudiante(string identificacion, string usuario)
    {
        return ModeloEstudiante.Eliminar(identificacion, usuario);
    }



}

