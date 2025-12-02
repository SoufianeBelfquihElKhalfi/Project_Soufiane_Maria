using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Soufiane_Maria
{
    public partial class client : System.Web.UI.Page
    {
        protected Label LabelUsername;
        protected Label LabelProfile;
        protected Button btnLogout;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar si la página es un PostBack o no
            if (!IsPostBack)
            {
                // Si no es un PostBack, significa que es la primera vez que se carga la página
                if (Session["username"] != null && Session["profile"] != null)
                {
                    // Obtener los valores de la sesión
                    string username = Session["username"].ToString();
                    string profile = Session["profile"].ToString();

                    // Mostrar información del usuario en la página
                    LabelUsername.Text = "Welcome, " + username;
                    LabelProfile.Text = "Your profile is: " + profile;

                    // También puedes usar estos datos para controlar el acceso según el perfil
                    if (profile != "client")
                    {
                        // Si el perfil no es "client", redirigir a la página de login o acceso denegado
                        Response.Redirect("login.aspx");
                    }
                }
                else
                {
                    // Si no hay sesión activa, redirigir a la página de login
                    Response.Redirect("login.aspx");
                }
            }
            else
            {
                // Aquí puedes manejar cualquier lógica que necesite ser ejecutada solo en un PostBack
                // Por ejemplo, si el usuario hizo un clic en algún botón u otra acción que desencadena el PostBack.
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Cerrar sesión, eliminar datos de la sesión
            Session.Clear();
            Session.Abandon();  // Eliminar la sesión actual

            // Redirigir al login
            Response.Redirect("login.aspx");
        }
    }
}