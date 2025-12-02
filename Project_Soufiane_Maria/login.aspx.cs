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

    // Crear la conexión a la base de datos
    SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathDB + ";Version=3;");
    conn.Open();

    // Consulta SQL para validar el usuario y la contraseña
    string query = "SELECT profile FROM credentials WHERE username = @username AND password = @password";

    // Crear el comando SQL
    SQLiteCommand comm = new SQLiteCommand(query, conn);
    comm.Parameters.AddWithValue("@username", username);
    comm.Parameters.AddWithValue("@password", password);

    // Crear un DataReader para ejecutar la consulta
    SQLiteDataReader reader = comm.ExecuteReader();

    // Crear un DataTable para almacenar los resultados
    DataTable table = new DataTable();
    table.Load(reader);

    // Cerrar el DataReader
    reader.Close();

    // Verificar si se encontró algún usuario con las credenciales correctas
    if (table.Rows.Count > 0)
    {
        // Solo debería haber una fila (un usuario)
        DataRow row = table.Rows[0];  // Obtener la primera fila (el primer usuario)
        string profile = row["profile"].ToString();  // Obtener el perfil del usuario

        // Guardar la información en la sesión
        Session["profile"] = profile;
        Session["username"] = username;  // Guardar el nombre de usuario

        // Redirigir según el perfil del usuario
        if (profile == "client")
        {
            Response.Redirect("client.aspx");
        }
        else if (profile == "receptionist")
        {
            Response.Redirect("receptionist.aspx");
        }
    }
    else
    {
        // Si no se encontró el usuario o las credenciales son incorrectas
        LabelMessage.Text = "Wrong credentials";
    }

    // Cerrar la conexión
    conn.Close();
}
catch (Exception ex)
{
    // En caso de error, mostrar el mensaje
    LabelMessage.Text = "Error: " + ex.Message;
}
        }
    }
}