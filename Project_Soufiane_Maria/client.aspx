<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="client.aspx.cs" Inherits="Project_Soufiane_Maria.client" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Welcome Client</h1>
            <!-- Aquí defino los controles Label en el archivo .aspx -->
            <asp:Label ID="LabelUsername" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="LabelProfile" runat="server" Text=""></asp:Label>
            <br />

            <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" />
        </div>
    </form>
</body>
</html>
