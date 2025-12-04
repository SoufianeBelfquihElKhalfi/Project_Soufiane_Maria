using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Soufiane_Maria
{
    public partial class receptionist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadRooms();

                if (Session["username"] != null && Session["profile"] != null)
                {
                    string username = Session["username"].ToString();
                    string profile = Session["profile"].ToString();

                    LabelUsername.Text = "Welcome, " + username;
                    LabelProfile.Text = "Your profile is: " + profile;

                    if (profile != "receptionist")
                    {
                        Response.Redirect("login.aspx");
                    }
                }
                else
                {
                    Response.Redirect("login.aspx");
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("login.aspx");
        }

        
        private void LoadRooms()
        {
            List<ListItem> roomItems = new List<ListItem>();

            string dbPath = Server.MapPath("~/database1.db"); 

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + dbPath + ";Version=3;"))
                {
                    conn.Open();
                    string query = "SELECT id, type, price, capacity FROM rooms";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string roomInfo = $"{reader["type"]} | Price: {reader["price"]} | Capacity: {reader["capacity"]}";
                                roomItems.Add(new ListItem(roomInfo, reader["id"].ToString()));
                            }
                        }
                    }
                }

                ListBoxRooms.Items.Clear();

                foreach (ListItem roomItem in roomItems)
                {
                    ListBoxRooms.Items.Add(roomItem);
                }
            }
            catch (Exception ex)
            {
                LabelSelectedRoom.Text = "Error: " + ex.Message;
            }
        }

       


protected void btnRegisterUser_Click(object sender, EventArgs e)
    {
        
        try
        {
            string id = TextBoxID.Text.Trim();
            string username = TextBoxUsername.Text.Trim();
            string profile = "client"; 
            string password = TextBoxPassword.Text.Trim();
            string dobStr = TextBoxDOB.Text.Trim();
            string address = TextBoxAddress.Text.Trim();
            string mobileStr = TextBoxMobile.Text.Trim();

            string[] fields = { id, username, password, dobStr, address, mobileStr };
            if (fields.Any(string.IsNullOrWhiteSpace))
            {
                
                LabelMessage.Text = "Please fill in all fields (ID, username, password, date of birth, address, and mobile number).";
                return;
            }

            
            string usernamePattern = @"^[a-zA-Z0-9_]+$";
            if (!Regex.IsMatch(username, usernamePattern))
            {
                
                LabelMessage.Text = "Username can only contain letters, numbers, and underscores.";
                return;
            }

                
                string passwordPattern = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$";

                if (!Regex.IsMatch(password, passwordPattern))
                {
                    LabelMessage.Text = "Password must be at least 6 characters long, with at least one letter and one number.";
                    return;
                }


                
                string idPattern = @"^\d{8}[A-Za-z]$";
            if (!Regex.IsMatch(id, idPattern))
            {
               
                LabelMessage.Text = "ID must be 8 digits followed by a letter.";
                return;
            }

       
            string dobPattern = @"^\d{4}-\d{2}-\d{2}$";
            if (!Regex.IsMatch(dobStr, dobPattern))
            {
                
                LabelMessage.Text = "Invalid date format. Please use yyyy-mm-dd.";
                return;
            }

            
            string addressPattern = @"^[a-zA-Z0-9\s,.-]{5,}$";
            if (!Regex.IsMatch(address, addressPattern))
            {
                
                LabelMessage.Text = "Address must contain only letters, numbers, spaces, commas, periods, and hyphens. It should be at least 5 characters long.";
                return;
            }

          
            string mobilePattern = @"^\d{9}$";
            if (!Regex.IsMatch(mobileStr, mobilePattern))
            {
                
                LabelMessage.Text = "Mobile number must be exactly 9 digits.";
                return;
            }

          
            DateTime dob;
            if (!DateTime.TryParseExact(dobStr, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dob))
            {
                
               
                LabelMessage.Text = "The date is not a valid calendar date (e.g., 2025-02-30).";
                return;
            }

            
            int mobile = int.Parse(mobileStr);

          
            string pathDB = Server.MapPath("~/database1.db");
            string connectionString = "Data Source=" + pathDB + ";Version=3;Pooling=False;";

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                
                using (SQLiteCommand pragmaCmd = new SQLiteCommand("PRAGMA busy_timeout=5000;", conn))
                {
                    pragmaCmd.ExecuteNonQuery();
                }

               
                using (SQLiteTransaction trans = conn.BeginTransaction())
                {
                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        cmd.Transaction = trans;

                        try
                        {
                            
                            string hashedPassword = ClientUser.HashPassword(password);

                            
                            cmd.CommandText = @"
                            INSERT INTO credentials (username, profile, password)
                            VALUES (@username, @profile, @password);";

                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@username", username);
                            cmd.Parameters.AddWithValue("@profile", profile);
                            cmd.Parameters.AddWithValue("@password", hashedPassword);

                            cmd.ExecuteNonQuery();

                           
                            cmd.CommandText = @"
                            INSERT INTO clients (ID, name, DOB, address, mobile)
                            VALUES (@ID, @name, @DOB, @address, @mobile);";

                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@ID", id);
                            cmd.Parameters.AddWithValue("@name", username);
                            cmd.Parameters.AddWithValue("@DOB", dob); 
                            cmd.Parameters.AddWithValue("@address", address);
                            cmd.Parameters.AddWithValue("@mobile", mobile); 

                            cmd.ExecuteNonQuery();

                            
                            trans.Commit();

                            
                            LabelMessage.ForeColor = System.Drawing.Color.Green;
                            LabelMessage.Text = "User/client successfully registered.";

                        }
                        catch (Exception ex)
                        {
                            
                            trans.Rollback();
                            throw; 
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LabelMessage.ForeColor = System.Drawing.Color.Red;
            
            LabelMessage.Text = "Error while registering: " + ex.Message;
        }
    }


  
    protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchFragment = TextBoxSearch.Text.Trim();

            if (!string.IsNullOrWhiteSpace(searchFragment))
            {
                List<ClientUser> clients = ClientUser.SearchClientsByName(searchFragment);

                ListBoxClients.Items.Clear();

                foreach (ClientUser client in clients)
                {
                    string clientInfo = $"{client.Username} | {client.Address} | {client.Mobile}";
                    ListBoxClients.Items.Add(new ListItem(clientInfo, client.Username));
                }

                if (clients.Count == 0)
                {
                    LabelSelectedClient.Text = "No clients found.";
                }
            }
            else
            {
                ListBoxClients.Items.Clear();
                ListBoxClients.Items.Add(new ListItem("Select Client", ""));
            }
        }

    
        protected void ListBoxClients_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (ListBoxClients.SelectedIndex >= 0)
            {
                string clientName = ListBoxClients.SelectedItem.Text.Split('|')[0].Trim();

                
                LabelSelectedClient.Text = "Selected Client: " + clientName;

                
                ClientUser selectedClient = ClientUser.GetClientByName(clientName);

                
                if (selectedClient != null)
                {
                    
                    LabelSelectedClient.Text = $"Selected Client: {selectedClient.Username}, " +
                                              $"Address: {selectedClient.Address}, Mobile: {selectedClient.Mobile}";
                }
                else
                {
                   
                    LabelSelectedClient.Text = "Client not found.";
                }
            }
            else
            {
               
                LabelSelectedClient.Text = string.Empty;
            }
        }



        protected void ListBoxRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (ListBoxRooms.SelectedIndex >= 0)
            {
                try
                {
                    int roomId = Convert.ToInt32(ListBoxRooms.SelectedItem.Value);

                    
                    LabelSelectedRoom.Text = "Selected Room ID: " + roomId;

                    
                    using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + Server.MapPath("~/database1.db") + ";Version=3;"))
                    {
                        conn.Open();
                        string query = "SELECT id, type, price, capacity FROM rooms WHERE id = @id";
                        using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", roomId);

                            using (SQLiteDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    
                                    LabelSelectedRoom.Text = $"Selected Room: {reader["type"]}, " +
                                                             $"Price: {reader["price"]}, Capacity: {reader["capacity"]}";
                                }
                                else
                                {
                                    
                                    LabelSelectedRoom.Text = "Room not found.";
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    
                    LabelSelectedRoom.Text = "Error: " + ex.Message;
                }
            }
            else
            {
                
                LabelSelectedRoom.Text = string.Empty;
            }
        }

        protected void btnCreateReservation_Click(object sender, EventArgs e)
        {
            
            string clientName = ListBoxClients.SelectedValue;

            
            string roomId = ListBoxRooms.SelectedValue;

            
            if (string.IsNullOrEmpty(clientName) || string.IsNullOrEmpty(roomId))
            {
                LabelSelectedClient.Text = "Please select both a client and a room.";
                return;
            }

            
            DateTime arrivalDate;
            DateTime departureDate;

            
            if (!DateTime.TryParse(TextBoxArrival.Text, out arrivalDate) || !DateTime.TryParse(TextBoxDeparture.Text, out departureDate))
            {
                LabelSelectedClient.Text = "Please enter valid arrival and departure dates.";
                return;
            }

           
            string clientId = GetClientDNI(clientName);

            if (string.IsNullOrEmpty(clientId))
            {
                LabelSelectedClient.Text = "Client not found.";
                return;
            }

            try
            {
                
                using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + Server.MapPath("~/database1.db") + ";Version=3;"))
                {
                    conn.Open();

                    
                    using (SQLiteCommand cmd = new SQLiteCommand("PRAGMA busy_timeout = 5000;", conn))
                    {
                        cmd.ExecuteNonQuery();  
                    }

                    
                    string query = @"
            INSERT INTO reservations (arrival, departure, client_id, room_id)
            VALUES (@arrival, @departure, @client_id, @room_id)";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        
                        cmd.Parameters.AddWithValue("@arrival", arrivalDate);
                        cmd.Parameters.AddWithValue("@departure", departureDate);
                        cmd.Parameters.AddWithValue("@client_id", clientId);  
                        cmd.Parameters.AddWithValue("@room_id", roomId);

                        
                        cmd.ExecuteNonQuery();
                    }
                }

               
                LabelSelectedClient.Text = "Reservation successfully created!";
            }
            catch (Exception ex)
            {
                
                LabelSelectedClient.Text = "Error creating reservation: " + ex.Message;
            }
        }

        private string GetClientDNI(string clientName)
        {
            string clientId = null;
            string pathDB = Server.MapPath("~/database1.db"); 

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathDB + ";Version=3;"))
                {
                    conn.Open();
                    string query = "SELECT ID FROM clients WHERE name = @name"; 

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", clientName);  

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clientId = reader["ID"].ToString(); 
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               
                LabelSelectedClient.Text = "Error fetching client ID: " + ex.Message;
            }

            return clientId;
        }

      

        protected void btnDeleteClient_Click(object sender, EventArgs e)
        {
            
            if (ListBoxClients.SelectedIndex < 0 || string.IsNullOrEmpty(ListBoxClients.SelectedValue))
            {
                LabelSelectedClient.Text = "Please select a client to delete.";
                LabelSelectedClient.ForeColor = System.Drawing.Color.Red;
                return;
            }

           
            string clientUsername = ListBoxClients.SelectedValue;

            try
            {
                
                ClientUser clientToDelete = ClientUser.GetClientByName(clientUsername);

                if (clientToDelete == null)
                {
                    LabelSelectedClient.Text = $"Error: Client '{clientUsername}' not found in database.";
                    LabelSelectedClient.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                
                DeleteClient(clientToDelete);

                
                LabelSelectedClient.Text = $"Client '{clientUsername}' deleted successfully.";
                LabelSelectedClient.ForeColor = System.Drawing.Color.Green;

               
                btnSearch_Click(null, null);
            }
            catch (Exception ex)
            {
                LabelSelectedClient.Text = "Deletion Error: " + ex.Message;
                LabelSelectedClient.ForeColor = System.Drawing.Color.Red;
            }
        }

       
        private void DeleteClient(ClientUser user)
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

               
                using (SQLiteTransaction trans = conn.BeginTransaction())
                {
                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        cmd.Transaction = trans;

                        try
                        {
                          
                            cmd.CommandText = "DELETE FROM clients WHERE ID = @ID;";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@ID", user.Id);
                            cmd.ExecuteNonQuery();

                          
                            cmd.CommandText = "DELETE FROM credentials WHERE username = @username;";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@username", user.Username);
                            cmd.ExecuteNonQuery();

                           
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            
                            trans.Rollback();
                            throw new Exception("The user could not be deleted from the database. Transaction reverted.", ex);
                        }
                    }
                }
            }
        }





    }
}
