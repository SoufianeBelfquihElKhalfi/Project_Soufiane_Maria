using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Soufiane_Maria
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            //string username = TextBox1.Text.Trim();  
            //string password = TextBox2.Text.Trim();
            //try
            //{
            //    // Ruta a la base de datos SQLite
            //    string pathDB = Server.MapPath("~/database.sqbpro");

            //    // Crear la conexión a la base de datos usando 'using' para asegurar que se cierre automáticamente
            //    using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathDB))
            //    {
            //        conn.Open();

            //        // Preparar la consulta SQL para comprobar el usuario y la contraseña
            //        string query = "SELECT COUNT(*) FROM users WHERE name = @username AND password = @password";

            //        using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
            //        {
            //            // Usar parámetros para evitar inyecciones SQL
            //            cmd.Parameters.AddWithValue("@username", username);  // Usuario (DNI)
            //            cmd.Parameters.AddWithValue("@password", password);  // Contraseña

            //            int userCount = Convert.ToInt32(cmd.ExecuteScalar());

                        
            //        }
            //    }  // La conexión se cierra automáticamente aquí al salir del bloque 'using'
            //}
            //catch (Exception ex)
            //{
            //    // Mostrar un mensaje en caso de error
            //    LabelMessage.Text = "Error: " + ex.Message;
            //}
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btOK_Click(object sender, EventArgs e)
        {
            string username = TextBox1.Text.Trim();
            string password = TextBox2.Text.Trim();
            try
            {
                // Ruta a la base de datos SQLite
                string pathDB = Server.MapPath("~/database1.sqbpro");

                // Crear la conexión a la base de datos usando 'using' para asegurar que se cierre automáticamente
                using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathDB))
                {
                    conn.Open();

                    // Preparar la consulta SQL para comprobar el usuario y la contraseña
                    string query = "SELECT COUNT(*) FROM users WHERE name = @username AND password = @password";


                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        

                        Response.Redirect($"client.aspx");
                    }
                }  // La conexión se cierra automáticamente aquí al salir del bloque 'using'
            }
            catch (Exception ex)
            {
            //    // Mostrar un mensaje en caso de error
                LabelMessage.Text = "Error: " + ex.Message;
            }
        }
    }
}