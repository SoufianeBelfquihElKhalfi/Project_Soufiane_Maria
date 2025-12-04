<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Project_Soufiane_Maria.index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hotel</title>
   <link href="styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <header class="navbar">
            <div class="navbar-left">
                <asp:Image ID="imgLogo" runat="server" ImageUrl="~/images/logo.png" CssClass="navbar-logo" />
                <asp:Label ID="lblTitulo" runat="server" Text="Trivago Hotels" CssClass="navbar-title" />
            </div>
            <asp:Button ID="btnLogin" runat="server" Text="Log in" CssClass="navbar-login" OnClick="btnLogin_Click" />
        </header>

        <div class="main-content">

            
            <div class="welcome-section">
                <div class="welcome-box">
                    <h2>Welcome to Trivago Hotels</h2>
                    <p>Enjoy a unique lodging experience. We offer you the best in comfort and services.</p>
                </div>
            </div>

            
            <div class="service-section">
                <div class="service-card">
                    <asp:Image ID="imgHabitaciones" runat="server" ImageUrl="~/images/habitaciones.png" AlternateText="Rooms" />
                    <h3>Rooms</h3>
                    <p>Comfort and luxury in every room. Enjoy the best views.</p>
                </div>

                <div class="service-card">
                    <asp:Image ID="imgRestaurante" runat="server" ImageUrl="~/images/restaurante.png" AlternateText="Restaurant" />
                    <h3>Restaurant</h3>
                    <p>Exceptional flavors that make the difference.</p>
                </div>

                <div class="service-card">
                    <asp:Image ID="imgSpa" runat="server" ImageUrl="~/images/spa.png" AlternateText="Spa & Wellness" />
                    <h3>Spa & Wellness</h3>
                    <p>Relax and rejuvenate in our exclusive spa.</p>
                </div>
            </div>

        </div>

        
        <div class="footer">
            <p>&copy; 2025 Trivago Hotels. All rights reserved.</p>
        </div>
    </form>
</body>
</html>
