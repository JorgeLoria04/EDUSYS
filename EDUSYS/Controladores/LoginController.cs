using EDUSYS;
using EDUSYS.Controladores;
using EDUSYS.Modelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class LoginController
{

    private ModeloLogin modeloLogin = new ModeloLogin();
    private frmMainMenu vista;

    public void SetVista(frmMainMenu vista)
    {
        this.vista = vista;
    }

    //MÉTODO QUE INICIA SESIÓN Y REGISTRA EL INGRESO AL SISTEMA EN LA BITÁCORA DE INGRESOS Y SISTEMAS
    public bool IniciarSesion(string Usuario, string Password, out int idBitacora)
    {
        idBitacora = -1;

        try
        {

            bool Valido = modeloLogin.ValidarUsuario(Usuario, Password);

            if (Valido)
            {
                ModeloLogin.UsuarioLogueado = Usuario;

                ModeloLogin login = new ModeloLogin
                {
                    Usuario = Usuario
                };

                idBitacora = modeloLogin.RegistrarIngreso(login);


                ModeloUsuario modeloUsuario = new ModeloUsuario();
                int ID_Rol = modeloUsuario.ObtenerID_RolXNombre(Usuario);
                AplicarPermisosPorRol(ID_Rol);


                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ocurrió un error al iniciar sesión: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
    }


    //MÉTODO QUE APLICA LOS PERMISOS DE ACUERDO AL ROL DEL USUARIO
    public void AplicarPermisosPorRol(int ID_Rol)
    {

        RolPermisoController controlador = new RolPermisoController();
        List<int> permisos = controlador.ObtenerPermisosDeRol(ID_Rol);

        if (vista != null)
        {
            vista.btnEstudiantes.Enabled = permisos.Contains(5); //Módulo Estudiantes
            vista.btnUsuarios.Enabled = permisos.Contains(2); // Módulo Usuarios
            vista.btnRoles.Enabled = permisos.Contains(4);// Módulo Roles y Permisos
            vista.btnReportes.Enabled = permisos.Contains(1);// Módulo Reportes
            vista.btnAyuda.Enabled = permisos.Contains(6);// Módulo Ayuda
            vista.btnAcerca.Enabled = permisos.Contains(7);// Módulo Acerca de
            vista.btnMatricula.Enabled = permisos.Contains(8);// Módulo Movimientos de matrícula
        }
    }


    // MÉTODO QUE REGISTRA LA SALIDA DEL SISTEMA EN LA BÍTACORA DE ENTRADAS Y SALIDAS
    public void CerrarSesion(int idbitacora)
    {
        try
        {
            modeloLogin.RegistrarSalida(idbitacora);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ocurrió un error al cerrar sesión: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public bool CambiarPassword(string Usuario, string NuevaPassword)
    {
        try
        {
            return modeloLogin.ActualizarPassword(Usuario, NuevaPassword);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error al cambiar la contraseña: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
    }

    public bool VerificarUsuarioExistente(string Usuario)
    {
        return modeloLogin.VerificarExistenciaUsuario(Usuario);
    }



    }

