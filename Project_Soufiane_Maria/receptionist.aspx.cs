using System;
using System.Collections.Generic;
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

        // Método para registrar usuario + cliente
        protected void btnRegisterUser_Click(object sender, EventArgs e)
        {
            try
            {
                // Leer datos del formulario
                string id = TextBoxID.Text.Trim();
                string username = TextBoxUsername.Text.Trim();   // username y name
                string profile = TextBoxProfile.Text.Trim();
                string password = TextBoxPassword.Text.Trim();
                string dobStr = TextBoxDOB.Text.Trim();
                string address = TextBoxAddress.Text.Trim();
                string mobileStr = TextBoxMobile.Text.Trim();

                // Validación mínima
                if (string.IsNullOrWhiteSpace(id) ||
                    string.IsNullOrWhiteSpace(username) ||
                    string.IsNullOrWhiteSpace(profile) ||
                    string.IsNullOrWhiteSpace(password))
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

                // Crear objeto ClientUser
                ClientUser nuevo = new ClientUser(
                    username,
                    profile,
                    password,
                    id,
                    dob,
                    address,
                    mobile
                );

                // Insertar en la BD (credentials + clients)
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
                // Buscar clientes por nombre (fragmento)
                List<ClientUser> clients = ClientUser.SearchClientsByName(searchFragment);

                // Limpiar el ListBox antes de llenarlo
                ListBoxClients.Items.Clear();

                // Llenar el ListBox con los resultados de la búsqueda
                foreach (ClientUser client in clients)
                {
                    string clientInfo = $"{client.Username} | {client.Address} | {client.Mobile}";
                    ListBoxClients.Items.Add(new ListItem(clientInfo, client.Username));
                }

                // Si no se encuentran clientes, mostrar mensaje
                if (clients.Count == 0)
                {
                    LabelSelectedClient.Text = "No clients found.";
                }
            }
            else
            {
                // Si el campo de búsqueda está vacío, vaciar el ListBox
                ListBoxClients.Items.Clear();
                ListBoxClients.Items.Add(new ListItem("Select Client", ""));
            }
        }

        // Método para manejar la selección de un cliente desde el ListBox
        protected void ListBoxClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListBoxClients.SelectedIndex > 0)
            {
                string clientName = ListBoxClients.SelectedItem.Text.Split('|')[0].Trim();
                ClientUser selectedClient = ClientUser.GetClientByName(clientName);

                // Mostrar la información del cliente seleccionado
                LabelSelectedClient.Text = $"Selected Client: {selectedClient.Username}, " +
                                          $"Address: {selectedClient.Address}, Mobile: {selectedClient.Mobile}";
            }
            else
            {
                LabelSelectedClient.Text = string.Empty;
            }
        }
    }
}
