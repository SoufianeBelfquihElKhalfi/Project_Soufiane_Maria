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
            <h1>Welcome to the Receptionist Dashboard</h1>

          <asp:Label ID="LabelUsername" runat="server" />
<asp:Label ID="LabelProfile" runat="server" />

            <br />
            <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" />
        </div>

        <!-- FORMULARIO ÚNICO PARA REGISTRAR USUARIO Y CLIENTE -->
        <div style="width: 360px; margin-top: 30px;">
            <h3>Register New User / Client</h3>

            <!-- ID DEL CLIENTE -->
            <asp:Label ID="LabelID" runat="server" Text="ID:"></asp:Label><br />
            <asp:TextBox ID="TextBoxID" runat="server"></asp:TextBox><br /><br />

            <!-- MISMO INPUT PARA USERNAME Y NAME -->
            <asp:Label ID="Label1" runat="server" Text="Username / Name:"></asp:Label><br />
            <asp:TextBox ID="TextBoxUsername" runat="server"></asp:TextBox><br /><br />

            <!-- PROFILE -->
            <asp:Label ID="LabelProfileNew" runat="server" Text="Profile:"></asp:Label><br />
            <asp:TextBox ID="TextBoxProfile" runat="server"></asp:TextBox><br /><br />

            <!-- PASSWORD -->
            <asp:Label ID="LabelPassword" runat="server" Text="Password:"></asp:Label><br />
            <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password"></asp:TextBox><br /><br />

            <!-- DOB -->
            <asp:Label ID="LabelDOB" runat="server" Text="Date of Birth (yyyy-mm-dd):"></asp:Label><br />
            <asp:TextBox ID="TextBoxDOB" runat="server"></asp:TextBox><br /><br />

            <!-- ADDRESS -->
            <asp:Label ID="LabelAddress" runat="server" Text="Address:"></asp:Label><br />
            <asp:TextBox ID="TextBoxAddress" runat="server"></asp:TextBox><br /><br />

            <!-- MOBILE -->
            <asp:Label ID="LabelMobile" runat="server" Text="Mobile Phone:"></asp:Label><br />
            <asp:TextBox ID="TextBoxMobile" runat="server"></asp:TextBox><br /><br />

            <!-- BOTÓN ÚNICO -->
            <asp:Button ID="btnRegisterUser" runat="server" Text="Register User / Client"
                OnClick="btnRegisterUser_Click" />
            <br /><br />

            <!-- MENSAJE DE ESTADO -->
            <asp:Label ID="LabelMessage" runat="server" ForeColor="Red"></asp:Label>
        </div>

        <!-- SECCIÓN PARA CONSULTAS (FUTURO) -->
        <div style="width: 360px; margin-top: 30px;">
            Search<br />
        </div>

    </form>
</body>
</html>
