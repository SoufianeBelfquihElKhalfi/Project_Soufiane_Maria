using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;


namespace Project_Soufiane_Maria
{
    public class ClientUser
    {
        private string username;
        private string profile;
        private string password;
        private string id;
        private DateTime dob;
        private string address;
        private int mobile;

        // Propiedades para los datos del usuario
        public string Username { get; set; }
        public string Profile { get; set; }
        public string Password { get; set; }
        public string Id { get; set; }
        public DateTime DoB { get; set; }
        public string Address { get; set; }
        public int Mobile { get; set; }

        // Constructor para inicializar la clase
        public ClientUser(string username, string profile, string password, string id, DateTime dob, string address, int mobile)
        {
            Username = username;
            Profile = profile;
            Password = password;
            Id = id;
            DoB = dob;
            Address = address;
            Mobile = mobile;
        }

        // Método para obtener un cliente por nombre
        public static ClientUser GetClientByName(string name)
        {
            string pathDB = HttpContext.Current.Server.MapPath("~/database1.db");
            string connectionString = "Data Source=" + pathDB + ";Version=3;";
            ClientUser user = null;

            string sql = @"SELECT ID, name, DOB, address, mobile 
                           FROM clients 
                           WHERE name = @name";

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@name", name);

                conn.Open();

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new ClientUser(
                            reader["name"].ToString(),
                            "", // profile no está en esta tabla
                            "", // password no está en esta tabla
                            reader["ID"].ToString(),
                            Convert.ToDateTime(reader["DOB"]),
                            reader["address"].ToString(),
                            Convert.ToInt32(reader["mobile"])
                        );
                    }
                }
            }

            return user;
        }

        // Método para insertar al usuario en la base de datos
        // Método para insertar al usuario en la base de datos
        

        // Método para hashear la contraseña usando MD5
        public static string HashPassword(string password)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Convertir la contraseña en bytes y calcular el hash
                byte[] data = md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                // Convertir el array de bytes a una cadena hexadecimal
                var sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }

         

        public static List<ClientUser> SearchClientsByName(string nameFragment)
        {
            string pathDB = HttpContext.Current.Server.MapPath("~/database1.db");
            string connectionString = "Data Source=" + pathDB + ";Version=3;";
            List<ClientUser> clients = new List<ClientUser>();

            string sql = @"SELECT name, DOB, address, mobile, ID 
                   FROM clients 
                   WHERE name LIKE @nameFragment";  // Búsqueda por fragmento de nombre

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@nameFragment", "%" + nameFragment + "%");

                conn.Open();

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ClientUser client = new ClientUser(
                            reader["name"].ToString(),
                            "", // profile no está en esta tabla
                            "", // password no está en esta tabla
                            reader["ID"].ToString(),
                            Convert.ToDateTime(reader["DOB"]),
                            reader["address"].ToString(),
                            Convert.ToInt32(reader["mobile"])
                        );

                        clients.Add(client);
                    }
                }
            }

            return clients;
        }










    }
}
