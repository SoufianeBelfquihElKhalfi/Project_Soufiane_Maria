using System;
using System.Data.SQLite;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Soufiane_Maria
{
    public partial class client : System.Web.UI.Page
    {
        protected Label LabelWelcome;
        protected Label LabelId;
        protected Label LabelDob;
        protected Label LabelAddress;
        protected Label LabelMobile;
        protected Label LabelArrival;
        protected Label LabelDeparture;
        protected Label LabelRoom;
        protected Label LabelUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Comprobamos que hay sesión
                if (Session["username"] != null && Session["profile"] != null)
                {
                    string username = Session["username"].ToString();
                    string profile = Session["profile"].ToString();

                    LabelWelcome.Text = "Welcome, " + profile + " " + username;

                    if (profile != "client")
                    {
                        // Si el perfil no es "client", redirigir a la página de login o acceso denegado
                        Response.Redirect("login.aspx");
                    }


                    // ============================
                    // OBTENER DATOS DEL CLIENTE
                    // ============================
                    // Suponemos que en la tabla clients.name se guarda el mismo valor que username
                    ClientUser clientData = ClientUser.GetClientByName(username);

                    if (clientData != null)
                    {
                        LabelId.Text = "ID: " + clientData.Id;
                        LabelDob.Text = "DOB: " + clientData.DoB.ToShortDateString();
                        LabelAddress.Text = "Address: " + clientData.Address;
                        LabelMobile.Text = "Mobile: " + clientData.Mobile.ToString();
                    }
                    else
                    {
                        // Si no hay datos en clients, lo indicamos
                        LabelId.Text = "No client data found in table 'clients'.";
                        LabelDob.Text = string.Empty;
                        LabelAddress.Text = string.Empty;
                        LabelMobile.Text = string.Empty;
                    }

                    // ============================
                    // OBTENER RESERVAS DEL CLIENTE
                    // ============================
                    GetReservationsByClient(clientData.Id);
                }
                else
                {
                    // Sin sesión → login
                    Response.Redirect("login.aspx");
                }
            }
        }

        private void GetReservationsByClient(string clientId)
        {
            string pathDB = Server.MapPath("~/database1.db");

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathDB + ";Version=3;"))
            {
                conn.Open();

                string query = @"SELECT arrival, departure, room_id 
                         FROM reservations 
                         WHERE client_id = @client_id";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@client_id", clientId);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read(); // primera reserva

                            LabelArrival.Text = "Arrival: " +
                                Convert.ToDateTime(reader["arrival"]).ToShortDateString();

                            LabelDeparture.Text = "Departure: " +
                                Convert.ToDateTime(reader["departure"]).ToShortDateString();

                            LabelRoom.Text = "Room ID: " + reader["room_id"].ToString();
                        }
                        else
                        {
                            LabelArrival.Text = "No reservations found.";
                            LabelDeparture.Text = "";
                            LabelRoom.Text = "";
                        }
                    }
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("login.aspx");
        }


    }
}
