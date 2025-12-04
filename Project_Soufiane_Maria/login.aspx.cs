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
           
            string username = TextBox1.Text.Trim();
            string password = TextBox2.Text.Trim();


            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                LabelMessage.Text = "Please enter both username and password.";
                return;
            }

            
            string usernamePattern = @"^[a-zA-Z0-9_]+$";  
            string passwordPattern = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$";  

            if (!Regex.IsMatch(username, usernamePattern) || !Regex.IsMatch(password, passwordPattern))
            {
                LabelMessage.Text = "Invalid username or password format.";
                return;
            }

            try
            {
                
                string pathDB = Server.MapPath("~/database1.db");
                
                string connectionString = "Data Source=" + pathDB + ";Version=3;Pooling=False;";

                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                   
                    using (SQLiteCommand pragmaCmd = new SQLiteCommand("PRAGMA busy_timeout=5000;", conn))
                    {
                        pragmaCmd.ExecuteNonQuery();
                    }

                    
                    string query = "SELECT profile, password FROM credentials WHERE username = @username";

                    using (SQLiteCommand comm = new SQLiteCommand(query, conn))
                    {
                        comm.Parameters.AddWithValue("@username", username);

                        using (SQLiteDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedHashPassword = reader["password"].ToString();
                                string profile = reader["profile"].ToString();

                               
                                if (VerifyPasswordHash(password, storedHashPassword))
                                {
                                   
                                    Session["profile"] = profile;
                                    Session["username"] = username;

                                  
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
                } 
            }
            catch (Exception ex)
            {
                LabelMessage.Text = "Error: " + ex.Message;
            }
        }

      
        private bool VerifyPasswordHash(string enteredPassword, string storedHashPassword)
        {
            
            string hashedEnteredPassword = ClientUser.HashPassword(enteredPassword);

           
            return hashedEnteredPassword.Equals(storedHashPassword, StringComparison.OrdinalIgnoreCase);
        }

        protected void btnBackToIndex_Click(object sender, EventArgs e)
        {
           
            Response.Redirect("index.aspx");
        }

    }
}
