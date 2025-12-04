using System;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

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
                    string query = "SELECT profile, password FROM credentials WHERE username = @username";

                    using (SQLiteCommand comm = new SQLiteCommand(query, conn))
                    {
                        comm.Parameters.AddWithValue("@username", username);

                        using (SQLiteDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedHashPassword = reader["password"].ToString(); // Obtener el hash de la contraseña almacenada
                                string profile = reader["profile"].ToString();

                                // Hashear la contraseña ingresada y compararla con la almacenada
                                if (VerifyPasswordHash(password, storedHashPassword))
                                {
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

        // Método para verificar la contraseña hasheada
        private bool VerifyPasswordHash(string enteredPassword, string storedHashPassword)
        {
            // Hashear la contraseña ingresada
            string hashedEnteredPassword = ClientUser.HashPassword(enteredPassword);

            // Comparar el hash de la contraseña ingresada con el hash almacenado
            return hashedEnteredPassword.Equals(storedHashPassword, StringComparison.OrdinalIgnoreCase);
        }

        // Método para hashear la contraseña usando MD5
        
    }
}
