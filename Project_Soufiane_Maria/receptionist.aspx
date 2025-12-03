<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="receptionist.aspx.cs" Inherits="Project_Soufiane_Maria.receptionist" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Receptionist Dashboard</title>
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body {
            font-family: Arial, sans-serif;
            background-color: #e9ecef;
            min-height: 100vh;
            display: flex;
            flex-direction: column;
        }

        header {
            background-color: darkslateblue;
            color: white;
            padding: 15px 30px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            font-weight: bold;
            font-size: 20px;
            box-shadow: 0 2px 6px rgba(0,0,0,0.15);
        }

        .header-btn {
            background-color: white;
            color: darkslateblue;
            border: none;
            padding: 8px 16px;
            border-radius: 4px;
            cursor: pointer;
            transition: background-color 0.3s ease;
            font-weight: normal;
            font-size: 14px;
        }

        .header-btn:hover {
            background-color: cornflowerblue;
            color: white;
        }

        .container {
            display: flex;
            justify-content: space-between;
            gap: 20px;
            padding: 50px 30px;
            flex-wrap: wrap; /* Esto permite que los elementos se ajusten en diferentes líneas si el espacio es reducido */
        }

        .card {
            background-color: white;
            border-radius: 10px;
            padding: 30px 25px;
            width: 320px;
            box-shadow: 0 5px 15px rgba(0,0,0,0.1);
        }

        .card h3 {
            color: darkslateblue;
            margin-bottom: 25px;
            font-size: 22px;
            border-bottom: 2px solid cornflowerblue;
            padding-bottom: 6px;
        }

        .label {
            font-weight: bold;
            color: darkslateblue;
            margin-right: 6px;
            display: inline-block;
            width: 75px;
        }

        input[type="text"], input[type="date"], .reservations-listbox {
            width: 100%;
            padding: 8px;
            margin: 10px 0;
            border-radius: 6px;
            border: 1.5px solid darkslateblue;
            font-size: 14px;
            color: #2c3e50;
        }

        .reservations-listbox {
            height: 200px;
            font-size: 14px;
            border-radius: 6px;
            padding: 4px;
            color: #2c3e50;
            cursor: pointer;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">

        <!-- HEADER DE LA PÁGINA -->
        <header>
            <asp:Label ID="LabelUsername" runat="server" />
            <asp:Label ID="LabelProfile" runat="server" />
            <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" CssClass="header-btn" />
        </header>

        <!-- Contenedor de los formularios al mismo nivel -->
        <div class="container">
            <!-- FORMULARIO ÚNICO PARA REGISTRAR USUARIO Y CLIENTE -->
            <div class="card">
                <h3>Register New User / Client</h3>

                <asp:Label ID="LabelID" runat="server" Text="ID:" class="label"></asp:Label><br />
                <asp:TextBox ID="TextBoxID" runat="server"></asp:TextBox><br /><br />

                <asp:Label ID="Label1" runat="server" Text="Username / Name:" class="label"></asp:Label><br />
                <asp:TextBox ID="TextBoxUsername" runat="server"></asp:TextBox><br /><br />


                <asp:Label ID="LabelPassword" runat="server" Text="Password:" class="label"></asp:Label><br />
                <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password"></asp:TextBox><br /><br />

                <asp:Label ID="LabelDOB" runat="server" Text="Date of Birth (yyyy-mm-dd):" class="label"></asp:Label><br />
                <asp:TextBox ID="TextBoxDOB" runat="server"></asp:TextBox><br /><br />

                <asp:Label ID="LabelAddress" runat="server" Text="Address:" class="label"></asp:Label><br />
                <asp:TextBox ID="TextBoxAddress" runat="server"></asp:TextBox><br /><br />

                <asp:Label ID="LabelMobile" runat="server" Text="Mobile Phone:" class="label"></asp:Label><br />
                <asp:TextBox ID="TextBoxMobile" runat="server"></asp:TextBox><br /><br />

                <asp:Button ID="btnRegisterUser" runat="server" Text="Register User / Client" OnClick="btnRegisterUser_Click" />
                <br /><br />

                <asp:Label ID="LabelMessage" runat="server" ForeColor="Red"></asp:Label>
            </div>

            <!-- SECCIÓN PARA CONSULTAS -->
            <div class="card">
                <h3>Search Clients</h3>

                <asp:Label ID="LabelSearch" runat="server" Text="Enter Name Fragment:" class="label"></asp:Label><br />
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
            <div class="card">
                <h3>All Rooms</h3>

                <!-- ListBox para mostrar todas las habitaciones -->
                <asp:ListBox ID="ListBoxRooms" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ListBoxRooms_SelectedIndexChanged" style="width: 100%; height: 200px;">
                    <asp:ListItem Text="Select Room" Value="" />
                </asp:ListBox>

                <br /><br />
                <asp:Label ID="LabelSelectedRoom" runat="server" />
            </div>

            <!-- Formulario para fechas de llegada y salida -->
            <div class="card">
                <h3>Reservation Dates</h3>

                <asp:Label ID="LabelArrival" runat="server" Text="Arrival Date:" class="label"></asp:Label><br />
                <asp:TextBox ID="TextBoxArrival" runat="server" TextMode="Date" /><br /><br />

                <asp:Label ID="LabelDeparture" runat="server" Text="Departure Date:" class="label"></asp:Label><br />
                <asp:TextBox ID="TextBoxDeparture" runat="server" TextMode="Date" /><br /><br />

                <asp:Button ID="btnCreateReservation" runat="server" Text="Create Reservation" OnClick="btnCreateReservation_Click" />
            </div>
        </div>

    </form>
</body>
</html>
