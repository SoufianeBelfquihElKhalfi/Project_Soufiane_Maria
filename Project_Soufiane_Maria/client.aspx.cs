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
        protected ListBox ReservationsList;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] != null && Session["profile"] != null)
                {
                    string username = Session["username"].ToString();
                    string profile = Session["profile"].ToString();

                    LabelWelcome.Text = "Welcome, " + profile + " " + username;

                    if (profile != "client")
                    {
                        Response.Redirect("login.aspx");
                    }

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
                        LabelId.Text = "No client data found in table 'clients'.";
                        LabelDob.Text = string.Empty;
                        LabelAddress.Text = string.Empty;
                        LabelMobile.Text = string.Empty;
                    }

                    GetReservationsByClient(clientData.Id);
                }
                else
                {
                    Response.Redirect("login.aspx");
                }
            }
        }


        private void GetReservationsByClient(string clientId)
        {
            string pathDB = Server.MapPath("~/database1.db");
            ReservationsList.Items.Clear();

            using (var conn = new SQLiteConnection("Data Source=" + pathDB + ";Version=3;"))
            {
                conn.Open();
                string query = @"SELECT id, arrival, departure, room_id 
                                 FROM reservations 
                                 WHERE client_id = @client_id
                                 ORDER BY arrival";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@client_id", clientId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string id = reader["id"].ToString();
                                string arrival = Convert.ToDateTime(reader["arrival"]).ToShortDateString();
                                string departure = Convert.ToDateTime(reader["departure"]).ToShortDateString();
                                string room = reader["room_id"].ToString();
                                string text = $"{arrival} - {departure} (Room {room})";
                                ReservationsList.Items.Add(new ListItem(text, id));
                            }
                            ReservationsList.SelectedIndex = 0;
                            LoadReservationDetails(ReservationsList.SelectedValue);
                        }
                        else
                        {
                            ReservationsList.Items.Add(new ListItem("No reservations found", "-1"));
                            LabelArrival.Text = "No reservations found.";
                            LabelDeparture.Text = "";
                            LabelRoom.Text = "";
                        }
                    }
                }
            }
        }

        private void LoadReservationDetails(string reservationId)
        {
            if (string.IsNullOrEmpty(reservationId) || reservationId == "-1")
            {
                LabelArrival.Text = "No reservations found.";
                LabelDeparture.Text = "";
                LabelRoom.Text = "";
                return;
            }

            string pathDB = Server.MapPath("~/database1.db");
            using (var conn = new SQLiteConnection("Data Source=" + pathDB + ";Version=3;"))
            {
                conn.Open();
                string query = @"SELECT arrival, departure, room_id 
                                 FROM reservations 
                                 WHERE id = @id LIMIT 1";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", reservationId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            LabelArrival.Text = "Arrival: " + Convert.ToDateTime(reader["arrival"]).ToShortDateString();
                            LabelDeparture.Text = "Departure: " + Convert.ToDateTime(reader["departure"]).ToShortDateString();
                            LabelRoom.Text = "Room ID: " + reader["room_id"].ToString();
                        }
                        else
                        {
                            LabelArrival.Text = "Reservation not found.";
                            LabelDeparture.Text = "";
                            LabelRoom.Text = "";
                        }
                    }
                }
            }
        }

        protected void ReservationsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["SelectedReservation"] = ReservationsList.SelectedValue;
            LoadReservationDetails(ReservationsList.SelectedValue);
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("login.aspx");
        }
    }
}
