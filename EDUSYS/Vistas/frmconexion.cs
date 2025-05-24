using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDUSYS.Vistas
{
    public partial class frmconexion : Form
    {
        public frmconexion()
        {
            InitializeComponent();
        }

        private void btnVerificar_Click(object sender, EventArgs e)
        {
           
           string connectionString = "Server=tcp:edusysserver.database.windows.net,1433;Initial Catalog=EDUSYS;Persist Security Info=False;User ID=LORIA;Password=235689jP;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

           using (SqlConnection connection = new SqlConnection(connectionString))
           {
               try
               {
                        connection.Open();
                        lblResultado.Text = "¡Conexión exitosa a Azure SQL Database!";
                        lblResultado.ForeColor = System.Drawing.Color.Green;
                    }
                    catch (Exception ex)
                    {
                        lblResultado.Text = "Error de conexión: " + ex.Message;
                        lblResultado.ForeColor = System.Drawing.Color.Red;
                    }
                }
            
        }
    }
}
