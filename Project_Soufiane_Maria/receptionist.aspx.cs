using System;
using System.Collections.Generic;
using System.Data.SQLite;
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

        // Método para cargar todas las habitaciones en el ListBox
        private void LoadRooms()
        {
            List<ListItem> roomItems = new List<ListItem>();

            string dbPath = Server.MapPath("~/database1.db"); // Si está en el directorio del proyecto

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

        // Método para registrar usuario + cliente
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

                if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(profile) || string.IsNullOrWhiteSpace(password))
                {
                    LabelMessage.Text = "Please fill at least ID, username, profile and password.";
                    return;
                }

                DateTime dob;
                if (!DateTime.TryParse(dobStr, out dob))
                {
                    LabelMessage.Text = "Invalid date format (use yyyy-mm-dd).";
                    return;
                }

                int mobile;
                if (!int.TryParse(mobileStr, out mobile))
                {
                    LabelMessage.Text = "Mobile must be numeric.";
                    return;
                }

                ClientUser nuevo = new ClientUser(username, profile, password, id, dob, address, mobile);
                nuevo.InsertSelf(this);

                LabelMessage.ForeColor = System.Drawing.Color.Green;
                LabelMessage.Text = "User/client successfully registered.";
            }
            catch (Exception ex)
            {
                LabelMessage.ForeColor = System.Drawing.Color.Red;
                LabelMessage.Text = "Error while registering: " + ex.Message;
            }
        }

        // Método para realizar la búsqueda de clientes por nombre
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

        // Método para manejar la selección de un cliente desde el ListBox
        protected void ListBoxClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verificar si un cliente válido ha sido seleccionado
            if (ListBoxClients.SelectedIndex >= 0)
            {
                string clientName = ListBoxClients.SelectedItem.Text.Split('|')[0].Trim();

                // Depuración: Verificar que el nombre del cliente se obtiene correctamente
                LabelSelectedClient.Text = "Selected Client: " + clientName;

                // Obtener la información del cliente utilizando el método GetClientByName
                ClientUser selectedClient = ClientUser.GetClientByName(clientName);

                // Verificar que el cliente se encontró correctamente
                if (selectedClient != null)
                {
                    // Mostrar la información del cliente seleccionado
                    LabelSelectedClient.Text = $"Selected Client: {selectedClient.Username}, " +
                                              $"Address: {selectedClient.Address}, Mobile: {selectedClient.Mobile}";
                }
                else
                {
                    // Si no se encuentra el cliente, mostrar un mensaje
                    LabelSelectedClient.Text = "Client not found.";
                }
            }
            else
            {
                // Si no se seleccionó un cliente válido (por ejemplo, "Select Client"), limpiar el mensaje
                LabelSelectedClient.Text = string.Empty;
            }
        }



        protected void ListBoxRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verificar si una habitación válida ha sido seleccionada
            if (ListBoxRooms.SelectedIndex >= 0)
            {
                try
                {
                    int roomId = Convert.ToInt32(ListBoxRooms.SelectedItem.Value);

                    // Mostrar el ID de la habitación seleccionada para depuración
                    LabelSelectedRoom.Text = "Selected Room ID: " + roomId;

                    // Consulta a la base de datos para obtener la habitación seleccionada
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
                                    // Mostrar la información de la habitación seleccionada
                                    LabelSelectedRoom.Text = $"Selected Room: {reader["type"]}, " +
                                                             $"Price: {reader["price"]}, Capacity: {reader["capacity"]}";
                                }
                                else
                                {
                                    // Si no se encuentra la habitación, mostrar mensaje
                                    LabelSelectedRoom.Text = "Room not found.";
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Mostrar el error si algo falla
                    LabelSelectedRoom.Text = "Error: " + ex.Message;
                }
            }
            else
            {
                // Si no se seleccionó una habitación válida (por ejemplo, "Select Room"), limpiar el mensaje
                LabelSelectedRoom.Text = string.Empty;
            }
        }

        protected void btnCreateReservation_Click(object sender, EventArgs e)
        {
            // Obtener el nombre del cliente seleccionado
            string clientName = ListBoxClients.SelectedValue;

            // Obtener el ID de la habitación seleccionada
            string roomId = ListBoxRooms.SelectedValue;

            // Verificar si ambos campos fueron seleccionados correctamente
            if (string.IsNullOrEmpty(clientName) || string.IsNullOrEmpty(roomId))
            {
                LabelSelectedClient.Text = "Please select both a client and a room.";
                return;
            }

            // Verificar que las fechas de llegada y salida sean válidas
            DateTime arrivalDate;
            DateTime departureDate;

            // Intentar convertir las fechas de los TextBox a DateTime
            if (!DateTime.TryParse(TextBoxArrival.Text, out arrivalDate) || !DateTime.TryParse(TextBoxDeparture.Text, out departureDate))
            {
                LabelSelectedClient.Text = "Please enter valid arrival and departure dates.";
                return;
            }

            // Obtener el DNI del cliente desde la base de datos usando el nombre del cliente
            string clientId = GetClientDNI(clientName);

            if (string.IsNullOrEmpty(clientId))
            {
                LabelSelectedClient.Text = "Client not found.";
                return;
            }

            try
            {
                // Crear la reserva en la base de datos
                using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + Server.MapPath("~/database1.db") + ";Version=3;"))
                {
                    conn.Open();

                    // Establecer el tiempo de espera para los bloqueos
                    using (SQLiteCommand cmd = new SQLiteCommand("PRAGMA busy_timeout = 5000;", conn))
                    {
                        cmd.ExecuteNonQuery();  // Ejecutar el comando para establecer el tiempo de espera
                    }

                    // Consulta para insertar la nueva reserva
                    string query = @"
            INSERT INTO reservations (arrival, departure, client_id, room_id)
            VALUES (@arrival, @departure, @client_id, @room_id)";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Añadir los parámetros necesarios
                        cmd.Parameters.AddWithValue("@arrival", arrivalDate);
                        cmd.Parameters.AddWithValue("@departure", departureDate);
                        cmd.Parameters.AddWithValue("@client_id", clientId);  // Usar el DNI del cliente
                        cmd.Parameters.AddWithValue("@room_id", roomId);

                        // Ejecutar la inserción
                        cmd.ExecuteNonQuery();
                    }
                }

                // Mostrar un mensaje de éxito
                LabelSelectedClient.Text = "Reservation successfully created!";
            }
            catch (Exception ex)
            {
                // Manejar cualquier error
                LabelSelectedClient.Text = "Error creating reservation: " + ex.Message;
            }
        }

        private string GetClientDNI(string clientName)
        {
            string clientId = null;
            string pathDB = Server.MapPath("~/database1.db"); // Ruta a la base de datos

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathDB + ";Version=3;"))
                {
                    conn.Open();
                    string query = "SELECT ID FROM clients WHERE name = @name";  // Suponiendo que "name" es el nombre de usuario

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", clientName);  // Usamos el nombre de usuario para obtener el DNI

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clientId = reader["ID"].ToString();  // Obtenemos el DNI del cliente
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error
                LabelSelectedClient.Text = "Error fetching client ID: " + ex.Message;
            }

            return clientId;
        }

        // Dentro de la clase 'receptionist : System.Web.UI.Page'

        protected void btnDeleteClient_Click(object sender, EventArgs e)
        {
            // Verificar si un cliente está seleccionado
            if (ListBoxClients.SelectedIndex < 0 || string.IsNullOrEmpty(ListBoxClients.SelectedValue))
            {
                LabelSelectedClient.Text = "Please select a client to delete.";
                LabelSelectedClient.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // El ListBoxClients usa el Username como SelectedValue
            string clientUsername = ListBoxClients.SelectedValue;

            try
            {
                // 1. Obtener la información completa del cliente (incluyendo ID)
                ClientUser clientToDelete = ClientUser.GetClientByName(clientUsername);

                if (clientToDelete == null)
                {
                    LabelSelectedClient.Text = $"Error: Client '{clientUsername}' not found in database.";
                    LabelSelectedClient.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // 2. Llamar al método auxiliar para realizar la eliminación en la DB
                DeleteClient(clientToDelete);

                // 3. Actualizar la interfaz de usuario
                LabelSelectedClient.Text = $"Client '{clientUsername}' deleted successfully.";
                LabelSelectedClient.ForeColor = System.Drawing.Color.Green;

                // Recargar la lista de clientes para que desaparezca el eliminado
                btnSearch_Click(null, null);
            }
            catch (Exception ex)
            {
                LabelSelectedClient.Text = "Deletion Error: " + ex.Message;
                LabelSelectedClient.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Dentro de la clase 'receptionist : System.Web.UI.Page'

        /// <summary>
        /// Realiza la eliminación transaccional de un cliente de las tablas 'clients' y 'credentials'.
        /// </summary>
        /// <param name="user">El objeto ClientUser a eliminar.</param>
        private void DeleteClient(ClientUser user)
        {
            string pathDB = Server.MapPath("~/database1.db");
            string connectionString = "Data Source=" + pathDB + ";Version=3;Pooling=False;";

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                // Configurar el timeout de ocupado (Buena práctica para evitar 'database is locked')
                using (SQLiteCommand pragmaCmd = new SQLiteCommand("PRAGMA busy_timeout=5000;", conn))
                {
                    pragmaCmd.ExecuteNonQuery();
                }

                // Transacción para asegurar la atomicidad (ambos borrados se hacen o ninguno)
                using (SQLiteTransaction trans = conn.BeginTransaction())
                {
                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        cmd.Transaction = trans;

                        try
                        {
                            // 1. DELETE CLIENTS (Usando ID)
                            cmd.CommandText = "DELETE FROM clients WHERE ID = @ID;";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@ID", user.Id);
                            cmd.ExecuteNonQuery();

                            // 2. DELETE CREDENTIALS (Usando username)
                            cmd.CommandText = "DELETE FROM credentials WHERE username = @username;";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@username", user.Username);
                            cmd.ExecuteNonQuery();

                            // 3. Commit final
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            // Rollback en caso de error
                            trans.Rollback();
                            throw new Exception("The user could not be deleted from the database. Transaction reverted.", ex);
                        }
                    }
                }
            }
        }





    }
}
