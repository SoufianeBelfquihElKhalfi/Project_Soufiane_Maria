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
        // En el archivo receptionist.aspx.cs


protected void btnRegisterUser_Click(object sender, EventArgs e)
    {
        // 1. Recolección y Validación de Datos (CON TUS REGEX EXACTAS)
        try
        {
            string id = TextBoxID.Text.Trim();
            string username = TextBoxUsername.Text.Trim();
            string profile = "client"; // Perfil fijo
            string password = TextBoxPassword.Text.Trim();
            string dobStr = TextBoxDOB.Text.Trim();
            string address = TextBoxAddress.Text.Trim();
            string mobileStr = TextBoxMobile.Text.Trim();

            // **A. Validación de Campos Obligatorios** (De tu segundo código)
            string[] fields = { id, username, password, dobStr, address, mobileStr };
            if (fields.Any(string.IsNullOrWhiteSpace))
            {
                
                LabelMessage.Text = "Please fill in all fields (ID, username, password, date of birth, address, and mobile number).";
                return;
            }

            // **B. Validaciones por Expresión Regular** (TUS REGEX EXACTAS)

            // Username: solo letras, números y guiones bajos (^[a-zA-Z0-9_]+$)
            string usernamePattern = @"^[a-zA-Z0-9_]+$";
            if (!Regex.IsMatch(username, usernamePattern))
            {
                
                LabelMessage.Text = "Username can only contain letters, numbers, and underscores.";
                return;
            }

                // Contraseña: mínimo 6 caracteres, al menos una letra y un número
                string passwordPattern = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$";

                if (!Regex.IsMatch(password, passwordPattern))
                {
                    LabelMessage.Text = "Password must be at least 6 characters long, with at least one letter and one number.";
                    return;
                }


                // ID: 8 dígitos seguidos de una letra (^\d{8}[A-Za-z]$)
                string idPattern = @"^\d{8}[A-Za-z]$";
            if (!Regex.IsMatch(id, idPattern))
            {
               
                LabelMessage.Text = "ID must be 8 digits followed by a letter.";
                return;
            }

            // Fecha de Nacimiento: formato yyyy-mm-dd (^\d{4}-\d{2}-\d{2}$)
            string dobPattern = @"^\d{4}-\d{2}-\d{2}$";
            if (!Regex.IsMatch(dobStr, dobPattern))
            {
                
                LabelMessage.Text = "Invalid date format. Please use yyyy-mm-dd.";
                return;
            }

            // Dirección: solo letras, números, espacios, comas, puntos y guiones, min 5 caracteres (^[a-zA-Z0-9\s,.-]{5,}$ )
            string addressPattern = @"^[a-zA-Z0-9\s,.-]{5,}$";
            if (!Regex.IsMatch(address, addressPattern))
            {
                
                LabelMessage.Text = "Address must contain only letters, numbers, spaces, commas, periods, and hyphens. It should be at least 5 characters long.";
                return;
            }

            // Número de Móvil: exactamente 9 dígitos (^\d{9}$)
            string mobilePattern = @"^\d{9}$";
            if (!Regex.IsMatch(mobileStr, mobilePattern))
            {
                
                LabelMessage.Text = "Mobile number must be exactly 9 digits.";
                return;
            }

            // **C. Conversión de Tipos de Datos**

            // La conversión debe ser ParseExact porque el regex exige el formato yyyy-mm-dd.
            DateTime dob;
            if (!DateTime.TryParseExact(dobStr, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dob))
            {
                // Esto captura fechas que pasan el formato de regex pero son inválidas (ej. 2025-02-30)
               
                LabelMessage.Text = "The date is not a valid calendar date (e.g., 2025-02-30).";
                return;
            }

            // Conversión a int (es segura si pasó la regex de 9 dígitos)
            int mobile = int.Parse(mobileStr);

            // -------------------------------------------------------------
            // 2. Lógica de Base de Datos Directamente en el Code-Behind (DE TU PRIMER CÓDIGO)
            // -------------------------------------------------------------

            // **IMPORTANTE**: Usamos 'Server.MapPath' directamente porque estamos en el contexto Page.
            string pathDB = Server.MapPath("~/database1.db");
            string connectionString = "Data Source=" + pathDB + ";Version=3;Pooling=False;";

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                // Evita que SQLite lance "database is locked"
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
                            // Se asume que ClientUser.HashPassword es accesible y funcional
                            // Nota: Si la clase ClientUser no existe, esta línea fallará.
                            // La he mantenido de tu código original.
                            string hashedPassword = ClientUser.HashPassword(password);

                            // INSERT CREDENTIALS
                            cmd.CommandText = @"
                            INSERT INTO credentials (username, profile, password)
                            VALUES (@username, @profile, @password);";

                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@username", username);
                            cmd.Parameters.AddWithValue("@profile", profile);
                            cmd.Parameters.AddWithValue("@password", hashedPassword);

                            cmd.ExecuteNonQuery();

                            // ----------------------------------------------
                            // INSERT CLIENTS
                            // ----------------------------------------------
                            cmd.CommandText = @"
                            INSERT INTO clients (ID, name, DOB, address, mobile)
                            VALUES (@ID, @name, @DOB, @address, @mobile);";

                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@ID", id);
                            cmd.Parameters.AddWithValue("@name", username);
                            cmd.Parameters.AddWithValue("@DOB", dob); // DateTime
                            cmd.Parameters.AddWithValue("@address", address);
                            cmd.Parameters.AddWithValue("@mobile", mobile); // int

                            cmd.ExecuteNonQuery();

                            // Commit final
                            trans.Commit();

                            // Mensaje de éxito
                            LabelMessage.ForeColor = System.Drawing.Color.Green;
                            LabelMessage.Text = "User/client successfully registered.";

                        }
                        catch (Exception ex)
                        {
                            // Si ocurre un error, revertir la transacción
                            trans.Rollback();
                            throw; // Propagar el error al catch exterior
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LabelMessage.ForeColor = System.Drawing.Color.Red;
            // El mensaje de error será más útil si no es solo la validación, sino un error DB
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
