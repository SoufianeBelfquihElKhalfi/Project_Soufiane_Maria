using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Soufiane_Maria
{
	public partial class receptionist : System.Web.UI.Page
	{

        protected Label LabelUsername;
        protected Label LabelProfile;
        protected Button btnLogout;

        //hacer inserciones y usar el codigo de hash para la contraseña
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
                    if (profile != "receptionist")
                    {
                        // Si el perfil no es "receptionist", redirigir a la página de login o acceso denegado
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

        // Método para registrar las credenciales del usuario
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string credentialID = TextBoxCredentialID.Text.Trim();
            string username = TextBoxUsername.Text.Trim();
            string profile = TextBoxProfile.Text.Trim();
            string password = TextBoxPassword.Text.Trim();

            //regex

            // Aquí podrías agregar código para guardar estos datos en la base de datos.
            // Ejemplo: Agregar a la base de datos, etc.
            LabelMessage.Text = "User registered successfully!";
        }

        // Método para registrar los datos del cliente
        protected void btnRegisterClient_Click(object sender, EventArgs e)
        {
            string clientID = TextBoxClientID.Text.Trim();
            string clientName = TextBoxtName.Text.Trim();
            string dob = TextBoxDOB.Text.Trim();
            string address = TextBoxAddress.Text.Trim();
            string mobile = TextBoxMobile.Text.Trim();

            //regex

            // Aquí puedes agregar código para guardar los datos del cliente en la base de datos.
            // Ejemplo: Agregar a la base de datos, etc.
            LabelClientMessage.Text = "Client registered successfully!";
        }

    }
}