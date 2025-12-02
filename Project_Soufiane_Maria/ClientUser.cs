using System;
using System.Data.SQLite;
using System.Web;

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

     
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Profile
        {
            get { return profile; }
            set { profile = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public DateTime DoB
        {
            get { return dob; }
            set { dob = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public int Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }

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


    }
}
