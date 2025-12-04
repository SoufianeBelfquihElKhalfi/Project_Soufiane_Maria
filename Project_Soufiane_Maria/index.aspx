<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Project_Soufiane_Maria.index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hotel</title>
    <style>
        html, body {
            height: 100%;
            margin: 0;
            padding: 0;
        }

        body {
            display: flex;
            flex-direction: column;
            min-height: 100vh;
            background-image: url('/images/foto_fondo.jpg');
            background-size: cover;
            background-position: center;
            background-attachment: fixed;
        }

        form {
            display: flex;
            flex-direction: column;
            flex: 1;
            margin: 0;
        }

       
        .navbar {
            width: 100%;
            height: 80px;
            display: flex;
            align-items: center;
            justify-content: space-between;
            padding: 0 20px;
            border-bottom: 1px solid #e4e4e4;
            background-color: #ffffff;
            box-sizing: border-box;
        }

        .navbar-left {
            display: flex;
            align-items: center;
            gap: 15px;
        }

        .navbar-logo {
            height: 40px;
            width: auto;
        }

        .navbar-title {
            font-size: 22px;
            font-weight: 600;
        }

        .navbar-login {
            padding: 12px 28px;
            background-color: #0d6efd;
            color: white;
            border: none;
            border-radius: 8px;
            font-size: 16px;
            cursor: pointer;
            margin-right: 20px;
        }

        .navbar-login:hover {
            opacity: 0.85;
        }

       
        .main-content {
            padding: 50px 100px;
            flex: 1;
            box-sizing: border-box;
        }

        
        .welcome-section {
            text-align: center;
            margin-bottom: 50px;
        }

        .welcome-box {
            background-color: rgba(0, 0, 0, 0.55);
            padding: 25px 35px;
            border-radius: 12px;
            display: inline-block;
            text-align: center;
        }

        .welcome-box h2,
        .welcome-box p {
            color: #ffffff;
            margin: 0 0 10px 0;
        }

        .welcome-box p:last-child {
            margin-bottom: 0;
        }

       
        .service-section {
            display: flex;
            justify-content: space-between;
            gap: 20px;
            margin-bottom: 50px;
        }

        .service-card {
            width: 30%;
            padding: 20px;
            border: 1px solid #ddd;
            border-radius: 8px;
            background-color: rgba(255, 255, 255, 0.92);
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
            text-align: center;
            box-sizing: border-box;
        }

        .service-card img {
            width: 100%;
            border-radius: 8px;
        }

        .service-card h3 {
            font-size: 22px;
            margin-top: 15px;
            color: #333;
        }

        .service-card p {
            font-size: 16px;
            color: #777;
        }

        
        .footer {
            background-color: #333;
            color: white;
            padding: 30px;
            text-align: center;
            margin-top: 0;
            width: 100%;
            box-sizing: border-box;
        }

        .footer p {
            margin: 0;
            font-size: 14px;
        }

        
        @media (max-width: 768px) {
            .main-content {
                padding: 20px;
            }

            .service-section {
                flex-direction: column;
                align-items: center;
            }

            .service-card {
                width: 80%;
            }

            .navbar-title {
                font-size: 18px;
            }
        }

        @media (max-width: 480px) {
            .navbar {
                flex-direction: column;
                height: auto;
                padding: 10px;
            }

            .navbar-left {
                margin-bottom: 10px;
            }

            .navbar-title {
                font-size: 16px;
            }

            .service-card {
                width: 90%;
            }
        }

    </style>
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
