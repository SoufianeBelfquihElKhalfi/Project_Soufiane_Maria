<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="receptionist.aspx.cs" Inherits="Project_Soufiane_Maria.receptionist" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Welcome to the Receptionist Dashboard</h1>

        <!-- Aquí defino los controles Label en el archivo .aspx -->
            <asp:Label ID="LabelUsername" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="LabelProfile" runat="server" Text=""></asp:Label>
            <br />

            <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" />
        </div>

       <!-- Registro de Credenciales, consultar insercion!! -->
        <div style="width: 360px">
            <h3>Register User Credentials</h3>
            <asp:Label ID="LabelCredentialID" runat="server" Text="ID:"></asp:Label><br />
            <asp:TextBox ID="TextBoxCredentialID" runat="server"></asp:TextBox><br /><br />
            
            <asp:Label ID="Label1" runat="server" Text="Username:"></asp:Label><br />
            <asp:TextBox ID="TextBoxUsername" runat="server"></asp:TextBox><br /><br />
            
            <asp:Label ID="Label2" runat="server" Text="Profile:"></asp:Label><br />
            <asp:TextBox ID="TextBoxProfile" runat="server"></asp:TextBox><br /><br />
            
            <asp:Label ID="LabelPassword" runat="server" Text="Password:"></asp:Label><br />
            <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password"></asp:TextBox><br /><br />
            
            <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" />
            <asp:Label ID="LabelMessage" runat="server" ForeColor="Red"></asp:Label><br />
        </div>

        <!-- Registro de Cliente, consultar insercion!! -->
        <div style="width: 360px">
            <h3>Register Client</h3>
            <asp:Label ID="LabelClientID" runat="server" Text="ID:"></asp:Label><br />
            <asp:TextBox ID="TextBoxClientID" runat="server"></asp:TextBox><br /><br />
            
            <asp:Label ID="LabelClientName" runat="server" Text="Name:"></asp:Label><br />
            <asp:TextBox ID="TextBoxtName" runat="server"></asp:TextBox><br /><br />
            
            <asp:Label ID="LabelDOB" runat="server" Text="Date of Birth:"></asp:Label><br />
            <asp:TextBox ID="TextBoxDOB" runat="server"></asp:TextBox><br /><br />
            
            <asp:Label ID="LabelAddress" runat="server" Text="Address:"></asp:Label><br />
            <asp:TextBox ID="TextBoxAddress" runat="server"></asp:TextBox><br /><br />
            
            <asp:Label ID="LabelMobile" runat="server" Text="Mobile Phone:"></asp:Label><br />
            <asp:TextBox ID="TextBoxMobile" runat="server"></asp:TextBox><br /><br />
            
            <asp:Button ID="btnRegisterClient" runat="server" Text="Register Client" OnClick="btnRegisterClient_Click" />
            <asp:Label ID="LabelClientMessage" runat="server" ForeColor="Red"></asp:Label><br />
        </div>

        <div style="width: 360px">
            <!-- CONSULTAS -->
            Search<br />
        </div>
    </form>
</body>
</html>
