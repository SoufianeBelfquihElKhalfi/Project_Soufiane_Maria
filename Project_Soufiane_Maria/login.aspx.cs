using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;


namespace Project_Soufiane_Maria
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btOK_Click(object sender, EventArgs e)
        {
            // Obtener los valores de usuario y contraseña
            string username = TextBox1.Text.Trim();
            string password = TextBox2.Text.Trim();

            // Validar que los campos no estén vacíos
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                LabelMessage.Text = "Please enter both username and password.";
                return;
            }

            // Regex para validar el formato de usuario y contraseña
            string usernamePattern = @"^[a-zA-Z0-9_]+$";  // Alfanumérico y guión bajo
            string passwordPattern = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$";  // Mínimo 6 caracteres, al menos una letra y un número

            if (!Regex.IsMatch(username, usernamePattern) || !Regex.IsMatch(password, passwordPattern))
            {
                LabelMessage.Text = "Invalid username or password format.";
                return;
            }

            try
            {
                // Ruta a la base de datos SQLite
                string pathDB = Server.MapPath("~/database1.db");
                // IMPORTANTE: Pooling=False para evitar conexiones recicladas que bloquean
                string connectionString = "Data Source=" + pathDB + ";Version=3;Pooling=False;";

                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    // Evitar bloqueos instantáneos si está ocupada
                    using (SQLiteCommand pragmaCmd = new SQLiteCommand("PRAGMA busy_timeout=5000;", conn))
                    {
                        pragmaCmd.ExecuteNonQuery();
                    }

                    // Consulta SQL para validar el usuario y la contraseña
                    string query = "SELECT profile FROM credentials WHERE username = @username AND password = @password";

                    using (SQLiteCommand comm = new SQLiteCommand(query, conn))
                    {
                        comm.Parameters.AddWithValue("@username", username);
                        comm.Parameters.AddWithValue("@password", password);

                        using (SQLiteDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string profile = reader["profile"].ToString();

                                // Guardar la información en la sesión
                                Session["profile"] = profile;
                                Session["username"] = username;

                                // Redirigir según el perfil del usuario
                                if (profile == "client")
                                {
                                    Response.Redirect("client.aspx");
                                }
                                else if (profile == "receptionist")
                                {
                                    Response.Redirect("receptionist.aspx");
                                }
                                else
                                {
                                    LabelMessage.Text = "Unknown profile.";
                                }
                            }
                            else
                            {
                                LabelMessage.Text = "Wrong credentials";
                            }
                        }
                    }
                } // aquí SIEMPRE se cierra la conexión, incluso si hay excepción dentro
            }
            catch (Exception ex)
            {
                LabelMessage.Text = "Error: " + ex.Message;
            }
        }

    }
}