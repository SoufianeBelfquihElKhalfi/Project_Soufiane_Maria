<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="receptionist.aspx.cs" Inherits="Project_Soufiane_Maria.receptionist" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Receptionist Dashboard</title>
</head>
<body>
    <form id="form1" runat="server">

        <!-- HEADER DE LA PÁGINA -->
        <div>
            <asp:Label ID="LabelUsername" runat="server" />
            <asp:Label ID="LabelProfile" runat="server" />
            <br />
            <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" />
        </div>

        <!-- FORMULARIO ÚNICO PARA REGISTRAR USUARIO Y CLIENTE -->
        <div style="width: 360px; margin-top: 30px;">
            <h3>Register New User / Client</h3>

            <asp:Label ID="LabelID" runat="server" Text="ID:"></asp:Label><br />
            <asp:TextBox ID="TextBoxID" runat="server"></asp:TextBox><br /><br />

            <asp:Label ID="Label1" runat="server" Text="Username / Name:"></asp:Label><br />
            <asp:TextBox ID="TextBoxUsername" runat="server"></asp:TextBox><br /><br />

            <asp:Label ID="LabelProfileNew" runat="server" Text="Profile:"></asp:Label><br />
            <asp:TextBox ID="TextBoxProfile" runat="server"></asp:TextBox><br /><br />

            <asp:Label ID="LabelPassword" runat="server" Text="Password:"></asp:Label><br />
            <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password"></asp:TextBox><br /><br />

            <asp:Label ID="LabelDOB" runat="server" Text="Date of Birth (yyyy-mm-dd):"></asp:Label><br />
            <asp:TextBox ID="TextBoxDOB" runat="server"></asp:TextBox><br /><br />

            <asp:Label ID="LabelAddress" runat="server" Text="Address:"></asp:Label><br />
            <asp:TextBox ID="TextBoxAddress" runat="server"></asp:TextBox><br /><br />

            <asp:Label ID="LabelMobile" runat="server" Text="Mobile Phone:"></asp:Label><br />
            <asp:TextBox ID="TextBoxMobile" runat="server"></asp:TextBox><br /><br />

            <asp:Button ID="btnRegisterUser" runat="server" Text="Register User / Client" OnClick="btnRegisterUser_Click" />
            <br /><br />

            <asp:Label ID="LabelMessage" runat="server" ForeColor="Red"></asp:Label>
        </div>

        <!-- SECCIÓN PARA CONSULTAS -->
        <div style="width: 360px; margin-top: 30px;">
            <h3>Search Clients</h3>

            <asp:Label ID="LabelSearch" runat="server" Text="Enter Name Fragment:"></asp:Label><br />
            <asp:TextBox ID="TextBoxSearch" runat="server"></asp:TextBox><br /><br />

            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" /><br /><br />

            <!-- ListBox para mostrar clientes con toda la información -->
            <asp:ListBox ID="ListBoxClients" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ListBoxClients_SelectedIndexChanged" style="width: 100%; height: 200px;">
                <asp:ListItem Text="Select Client" Value="" />
            </asp:ListBox>

            <br /><br />
            <asp:Label ID="LabelSelectedClient" runat="server" />
        </div>

        <!-- SECCIÓN PARA MOSTRAR TODAS LAS HABITACIONES -->
        <div style="width: 360px; margin-top: 30px;">
            <h3>All Rooms</h3>

            <!-- ListBox para mostrar todas las habitaciones -->
            <asp:ListBox ID="ListBoxRooms" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ListBoxRooms_SelectedIndexChanged" style="width: 100%; height: 200px;">
                <asp:ListItem Text="Select Room" Value="" />
            </asp:ListBox>

            <br /><br />
            <asp:Label ID="LabelSelectedRoom" runat="server" />
        </div>

        <!-- Formulario para fechas de llegada y salida -->
<div style="width: 360px; margin-top: 30px;">
    <h3>Reservation Dates</h3>

    <asp:Label ID="LabelArrival" runat="server" Text="Arrival Date:"></asp:Label><br />
    <asp:TextBox ID="TextBoxArrival" runat="server" TextMode="Date" /><br /><br />

    <asp:Label ID="LabelDeparture" runat="server" Text="Departure Date:"></asp:Label><br />
    <asp:TextBox ID="TextBoxDeparture" runat="server" TextMode="Date" /><br /><br />

    <asp:Button ID="btnCreateReservation" runat="server" Text="Create Reservation" OnClick="btnCreateReservation_Click" />
</div>



    </form>
</body>
</html>
