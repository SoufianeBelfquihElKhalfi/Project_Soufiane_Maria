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
        <div>
            <h1>Hello world</h1>
        </div>
    </form>
</body>
</html>
