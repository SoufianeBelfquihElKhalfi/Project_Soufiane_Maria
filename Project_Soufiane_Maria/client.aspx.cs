using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Soufiane_Maria
{
    public partial class client : System.Web.UI.Page
    {
        protected Label LabelId;
        protected Label LabelDob;
        protected Label LabelAddress;
        protected Label LabelMobile;
        protected Label LabelArrival;      
        protected Label LabelDeparture;
        protected Label LabelRoom; 
        protected Label LabelDNI;

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

                }
                else
                {
                    // Sin sesión → login
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


    }
}
