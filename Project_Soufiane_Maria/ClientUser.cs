using System;
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
        public void InsertSelf(Page pageReference)
        {
            // Ruta física de database1.db
            string pathDB = pageReference.Server.MapPath("~/database1.db");

            // Pooling desactivado para evitar bloqueos
            string connectionString = "Data Source=" + pathDB + ";Version=3;Pooling=False;";

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                // Evita que SQLite lance "database is locked" instantáneamente
                using (SQLiteCommand pragmaCmd = new SQLiteCommand("PRAGMA busy_timeout=5000;", conn))
                {
                    pragmaCmd.ExecuteNonQuery();
                }

                // Transacción para insertar en ambas tablas de forma atómica
                using (SQLiteTransaction trans = conn.BeginTransaction())
                {
                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        cmd.Transaction = trans;

                        try
                        {
                            // ----------------------------------------------
                            // INSERT CREDENTIALS
                            // ----------------------------------------------
                            // Hashear la contraseña
                            string hashedPassword = HashPassword(this.Password);

                            cmd.CommandText = @"
                    INSERT INTO credentials (username, profile, password)
                    VALUES (@username, @profile, @password);";

                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@username", this.Username);
                            cmd.Parameters.AddWithValue("@profile", this.Profile);
                            cmd.Parameters.AddWithValue("@password", hashedPassword); // Contraseña hasheada

                            cmd.ExecuteNonQuery();

                            // ----------------------------------------------
                            // INSERT CLIENTS
                            // ----------------------------------------------
                            cmd.CommandText = @"
                    INSERT INTO clients (ID, name, DOB, address, mobile)
                    VALUES (@ID, @name, @DOB, @address, @mobile);";

                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@ID", this.Id);
                            cmd.Parameters.AddWithValue("@name", this.Username); // Usamos username como nombre
                            cmd.Parameters.AddWithValue("@DOB", this.DoB);
                            cmd.Parameters.AddWithValue("@address", this.Address);
                            cmd.Parameters.AddWithValue("@mobile", this.Mobile);

                            cmd.ExecuteNonQuery();

                            // Commit final
                            trans.Commit();
                        }
                        catch
                        {
                            // Si ocurre un error, revertir la transacción
                            trans.Rollback();
                            throw; // Propagar el error para que sea gestionado en el nivel superior (ej. receptionist.aspx.cs)
                        }
                    }
                }
            }
        }

        // Método para hashear la contraseña usando MD5
        private string HashPassword(string password)
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

    }
}
