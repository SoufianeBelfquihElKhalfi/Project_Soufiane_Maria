<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Project_Soufiane_Maria.index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hotel</title>
    <style>
        /* Header */
        .navbar {
            width: 100%;
            height: 80px;
            display: flex;
            align-items: center;
            justify-content: space-between;
            padding: 0 50px;
            border-bottom: 1px solid #e4e4e4;
            background-color: #ffffff;
        }

        /* Lado izquierdo: logo + nombre */
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

        /* Botón login */
        .navbar-login {
            padding: 12px 28px;
            background-color: #0d6efd;
            color: #ffffff;
            border: none;
            border-radius: 8px;
            font-size: 16px;
            cursor: pointer;
            margin-right: 20px; /* Añadido para separar del borde derecho */
        }

        .navbar-login:hover {
            opacity: 0.85;
        }

        /* Cuerpo */
.main-content {
    padding: 50px 100px;
    background-color: #f9f9f9;
}

/* Sección de bienvenida */
.welcome-section {
    text-align: center;
    margin-bottom: 50px;
}

.welcome-section h2 {
    font-size: 28px;
    font-weight: 700;
}

.welcome-section p {
    font-size: 18px;
    color: #555555;
}

/* Sección de servicios */
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
    background-color: #ffffff;
    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
    text-align: center;
}

.service-card img {
    width: 100%;
    height: auto;
    border-radius: 8px;
}

.service-card h3 {
    font-size: 22px;
    margin-top: 15px;
}

.service-card p {
    font-size: 16px;
    color: #777;
}

/* Footer */
.footer {
    background-color: #333;
    color: #ffffff;
    padding: 30px;
    text-align: center;
    margin-top: 50px; /* Espacio superior */
}

.footer p {
    margin: 0;
    font-size: 14px;
}

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <header class="navbar">
            <div class="navbar-left">
                <asp:Image ID="imgLogo" runat="server" ImageUrl="~/images/logo.png" CssClass="navbar-logo" />
                <asp:Label ID="lblTitulo" runat="server" Text="Hoteles Trivago" CssClass="navbar-title" />
            </div>
            <asp:Button ID="btnLogin" runat="server" Text="Iniciar sesión" CssClass="navbar-login" OnClick="btnLogin_Click" />
        </header>
        <!-- Cuerpo de la página -->
<div class="main-content">
            <!-- Sección de bienvenida -->
            <div class="welcome-section">
                <h2>Bienvenido a Hoteles Trivago</h2>
                <p>Disfruta de una experiencia única de hospedaje. Te ofrecemos lo mejor en confort y servicios.</p>
            </div>

            <!-- Sección de servicios -->
            <div class="service-section">
                <div class="service-card">
                    <asp:Image ID="imgHabitaciones" runat="server" ImageUrl="~/images/habitaciones.png" AlternateText="Habitaciones" />
                    <h3>Habitaciones</h3>
                    <p>Comodidad y lujo en cada habitación. Disfruta de las mejores vistas.</p>
                </div>

                <div class="service-card">
                    <asp:Image ID="imgRestaurante" runat="server" ImageUrl="~/images/restaurante.png" AlternateText="Restaurante" />
                    <h3>Restaurante</h3>
                    <p>Sabores excepcionales que hacen la diferencia.</p>
                </div>

                <div class="service-card">
                    <asp:Image ID="imgSpa" runat="server" ImageUrl="~/images/spa.png" AlternateText="Spa & Wellness" />
                    <h3>Spa & Wellness</h3>
                    <p>Relájate y rejuvenece en nuestro exclusivo spa.</p>
                </div>
            </div>
        </div>

        <!-- Footer -->
<div class="footer">
    <p>&copy; 2025 Hoteles Trivago. Todos los derechos reservados.</p>
</div>


    </form>
</body>
</html>
