using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace EDUSYS
{
    public partial class frmEstudiantes : Form
    {
        private EstudianteController estudianteController;
        public frmEstudiantes()
        {
            InitializeComponent();
            estudianteController = new EstudianteController();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmEstudiantes_Load(object sender, EventArgs e)
        {
            CargarLista();
            txtIdentificacion.Focus();
        }


        //FUNCIÓN QUE CARGA LA LISTA DE ESTUDIANTES PARA MOSTRARLA EN EL LISTVIEW DEL FORMULARIO
        private void CargarLista()
        {
            string connectionstring = "Server=DESKTOP-ELGAIO4\\SQLEXPRESS;Database=EDUSYS;Integrated Security=True;";
            string query = "SELECT Identificacion, Primer_Nombre, Segundo_Nombre, Primer_Apellido, Segundo_Apellido FROM Estudiantes";
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader()) 
                        {
                            lstEstudiantes.Items.Clear();

                            while (reader.Read()) 
                            {
                                ListViewItem item = new ListViewItem(reader["Identificacion"].ToString());
                                item.SubItems.Add(reader["Primer_Nombre"].ToString());
                                item.SubItems.Add(reader["Segundo_Nombre"].ToString());
                                item.SubItems.Add(reader["Primer_Apellido"].ToString());
                                item.SubItems.Add(reader["Segundo_Apellido"].ToString());

                                lstEstudiantes.Items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar estudiantes: {ex.Message}", "Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //FUNCIÓN QUE CARGA LOS DATOS DEL ESTUDIANTE EN LOS CAMPOS AL SELECCIONARLO EN EL LISTVIEW
        private void lstEstudiantes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstEstudiantes.SelectedItems.Count>0)
            {
                string Identificacion = lstEstudiantes.SelectedItems[0].SubItems[0].Text;

                CargarDatosEstudiante(Identificacion);
            }
        }


        //PARTE DE LA FUNCIÓN PARA CARGAR LOS DATOS EN EL LISTVIEW
        private void CargarDatosEstudiante(string Identificacion)
        {
            string connectionstring = "Server=DESKTOP-ELGAIO4\\SQLEXPRESS;Database=EDUSYS;Integrated Security=True;";
            string query = "SELECT * FROM Estudiantes WHERE Identificacion = @Identificacion";

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Identificacion", Identificacion);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) 
                            {
                                txtIdentificacion.Text = reader["Identificacion"].ToString();
                                txtPrimerNombre.Text = reader["Primer_Nombre"].ToString();
                                txtSegundoNombre.Text = reader["Segundo_Nombre"].ToString();
                                txtPrimerApellido.Text = reader["Primer_Apellido"].ToString();
                                txtSegundoApellido.Text = reader["Segundo_Apellido"].ToString();
                                dtNacimiento.Value = Convert.ToDateTime(reader["Nacimiento"]);
                                txtDireccion.Text = reader["Direccion"].ToString();
                                txtPadecimientos.Text = reader["Padecimientos"].ToString();
                                txtNombreMadre.Text = reader["Nombre_Madre"].ToString();
                                txtNombrePadre.Text = reader["Nombre_Padre"].ToString();
                                txtCelular.Text = reader["Celular"].ToString();
                                txtCasa.Text = reader["Casa"].ToString();
                                txtTelEncargado.Text = reader["TelEncargado"].ToString();
                                txtCorreo.Text = reader["Correo"].ToString();

                                if (reader["Foto"] != DBNull.Value)
                                {
                                    byte[] datosImagen = (byte[])reader["Foto"];
                                    using (MemoryStream ms = new MemoryStream(datosImagen))
                                    {
                                        using (Image imagenTemporal = Image.FromStream(ms))
                                        {
                                            picFotoEstudiante.Image = new Bitmap(imagenTemporal);
                                        }
                                    }
                                }
                                else
                                {
                                    picFotoEstudiante.Image = null;
                                }
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        //FUNCIÓN PARA CARGAR LA FOTO QUE SE LE VA A ASIGNAR AL ESTUDIANTE
        private void btnCargarFoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de imagen (*.jpg;*.png)|*.jpg;*.png";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                picFotoEstudiante.Image = Image.FromFile(openFileDialog.FileName);
            }
        }


        //FUNCIÓN NECESARIA PARA CONVERTIR LA IMAGEN AL FORMATO NECESARIO PARA PODER GUARDARLA EN LA BASE DE DATOS
        private byte[] ConvertirImagenABytes(PictureBox picFotoEstudiante)
        {
            if (picFotoEstudiante.Image == null)
                return null;

            using (MemoryStream ms = new MemoryStream())
            {
                using (Bitmap bmp = new Bitmap(picFotoEstudiante.Image))
                {
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                }
                return ms.ToArray();
            }
        }



        //FUNCIÓN PARA AÑADIR UN ESTUDIANTE A LA BASE DE DATOS
        private void btnAgregarES_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
            {
                MessageBox.Show("Por favor, verifique que todos los campos obligatorios estén completos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            ModeloEstudiante estudiante = new ModeloEstudiante
            {
                Identificacion = txtIdentificacion.Text.Trim(),
                Primer_Nombre = txtPrimerNombre.Text.Trim(),
                Segundo_Nombre = txtSegundoNombre.Text.Trim(),
                Primer_Apellido = txtPrimerApellido.Text.Trim(),
                Segundo_Apellido = txtSegundoApellido.Text.Trim(),
                Nacimiento = dtNacimiento.Value,
                Direccion = txtDireccion.Text.Trim(),
                Padecimientos = txtPadecimientos.Text.Trim(),
                Nombre_Madre = txtNombreMadre.Text.Trim(),
                Nombre_Padre = txtNombrePadre.Text.Trim(),
                Celular = txtCelular.Text.Trim(),
                Casa = txtCasa.Text.Trim(),
                TelEncargado = txtTelEncargado.Text.Trim(),
                Correo = txtCorreo.Text.Trim(),
                Foto = ConvertirImagenABytes(picFotoEstudiante)

            };

            string usuarioLogueado = ModeloLogin.UsuarioLogueado;

            bool resultado = estudianteController.GuardarEstudiante(estudiante, usuarioLogueado);

            if (resultado)
            {
                MessageBox.Show("Estudiante ingresado correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
                CargarLista();
            }

        }


        
        private void LimpiarCampos()
        {
            txtIdentificacion.Clear();
            txtPrimerNombre.Clear();
            txtSegundoNombre.Clear();
            txtPrimerApellido.Clear();
            txtSegundoApellido.Clear();
            dtNacimiento.Value = DateTime.Today;
            txtDireccion.Clear();
            txtPadecimientos.Clear();
            txtNombreMadre.Clear();
            txtNombrePadre.Clear();
            txtCelular.Clear();
            txtCasa.Clear();
            txtTelEncargado.Clear();
            txtCorreo.Clear();
            picFotoEstudiante.Image = null;   
        }


        //FUNCIÓN QUE VALIDA QUE NINGUNO DE LOS CAMPOS OBLIGATORIOS ESTÉN VACÍOS A LA HORA DE INGRESAR UN REGISTRO
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtIdentificacion.Text) ||
                string.IsNullOrWhiteSpace(txtPrimerNombre.Text) ||
                string.IsNullOrWhiteSpace(txtPrimerApellido.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                string.IsNullOrWhiteSpace(txtCelular.Text))
            {
                return false;
            }

            return true;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }



        //FUNCIÓN PARA CONSULTAR UN ESTUDIANTE DENTRO DE LA BASE DE DATOS
        private void btnConsultarES_Click(object sender, EventArgs e)
        {
            string Identificacion = txtIdentificacion.Text;
            EstudianteController controller = new EstudianteController();
            ModeloEstudiante est = controller.ConsultarEstudiante(Identificacion);

            if (est != null)
            {
                txtPrimerNombre.Text = est.Primer_Nombre;
                txtSegundoNombre.Text = est.Segundo_Nombre;
                txtPrimerApellido.Text = est.Primer_Apellido;
                txtSegundoApellido.Text = est.Segundo_Apellido;
                dtNacimiento.Value = est.Nacimiento;
                txtDireccion.Text = est.Direccion;
                txtNombreMadre.Text = est.Nombre_Madre;
                txtNombrePadre.Text = est.Nombre_Padre;
                txtCelular.Text = est.Celular;
                txtCasa.Text = est.Casa;
                txtTelEncargado.Text = est.TelEncargado;
                txtCorreo.Text = est.Correo;
                txtPadecimientos.Text = est.Padecimientos;
                if (est.Foto != null)
                {
                    using (MemoryStream ms = new MemoryStream(est.Foto))
                    {
                        picFotoEstudiante.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    picFotoEstudiante.Image = null;
                }

                
            }
            else
            {
                MessageBox.Show("Estudiante no encontrado.");
            }    
        }


        //FUNCIÓN PARA ELIMINAR UN ESTUDIANTE DE LA BASE DE DATOS
        private void btnEliminarES_Click(object sender, EventArgs e)
        {
            EstudianteController controller = new EstudianteController();

            string usuarioLogueado = ModeloLogin.UsuarioLogueado;

            if (controller.EliminarEstudiante(txtIdentificacion.Text, usuarioLogueado))
            {
                MessageBox.Show("Estudiante Eliminado.");
                LimpiarCampos();
                CargarLista();
            }
            else
            {
                MessageBox.Show("Estudiante no encontrado.");
            }
        }


       // FUNCIÓN PARA EDITAR UN ESTUDIANTE DENTRO DE LA BASE DE DATOS
        private void btnEditarES_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdentificacion.Text) ||
                string.IsNullOrWhiteSpace(txtPrimerNombre.Text) ||
                string.IsNullOrWhiteSpace(txtPrimerApellido.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                string.IsNullOrWhiteSpace(txtCelular.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            byte[] foto = ConvertirImagenABytes(picFotoEstudiante);

            ModeloEstudiante estudiante = new ModeloEstudiante
            {
                Identificacion = txtIdentificacion.Text,
                Primer_Nombre = txtPrimerNombre.Text,
                Segundo_Nombre = txtSegundoNombre.Text,
                Primer_Apellido = txtPrimerApellido.Text,
                Segundo_Apellido = txtSegundoApellido.Text,
                Nacimiento = dtNacimiento.Value,
                Direccion = txtDireccion.Text,
                Nombre_Madre = txtNombreMadre.Text,
                Nombre_Padre = txtNombrePadre.Text,
                Celular = txtCelular.Text,
                Casa = txtCasa.Text,
                TelEncargado = txtTelEncargado.Text,
                Correo = txtCorreo.Text,
                Padecimientos = txtPadecimientos.Text,
                Foto = foto
            };

            string usuarioLogueado = ModeloLogin.UsuarioLogueado;


            EstudianteController controller = new EstudianteController();
            if (controller.ModificarEstudiante(estudiante, usuarioLogueado))
            {
                MessageBox.Show("Estudiante modificado correctamente!.");
                LimpiarCampos();
                CargarLista();
            }
            else 
            {
                MessageBox.Show("Estudiante no encontrado.");
            }
        }

        private void BarraTitulo_Paint(object sender, PaintEventArgs e)
        {

        }


        //FUNCIÓN QUE LIMITA QUE EN LA IDENTIFICACIÓN SOLO SE PUEDAN INGRESAR DATOS DE TIPO NUMÉRICO Y QUE NO SE INGRESEN MÁS DE 14 DÍGITOS
        private void txtIdentifiacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            else if (char.IsDigit(e.KeyChar) && txtIdentificacion.Text.Length >= 14)
            {
                e.Handled = true;
            }
        }

        //FUNCIÓN QUE NO PERMITE QUE SE PEGUEN DATOS DE TIPO NO NUMÉRICO EN EL CAMPO IDENTIFICACIÓN
        private void txtIdentificacion_TextChanged(object sender, EventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtIdentificacion.Text, @"^\d*$"))
            {
                txtIdentificacion.Text = new string(txtIdentificacion.Text.Where(char.IsDigit).ToArray());
                txtIdentificacion.SelectionStart = txtIdentificacion.Text.Length;
            }
        }


        //FUNCIÓN QUE LIMITA LOS CARACTÉRES A NÚMERICOS EN EL CAMPO DE TELÉFONO
        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtCelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtCasa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTelEncargado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
