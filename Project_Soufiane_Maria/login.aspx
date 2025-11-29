<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Project_Soufiane_Maria.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <br />
            <br />
            Username:<br />
            <br />
            <asp:TextBox ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
            <br />
            <br />
            Password:<br />
            <br />
            <asp:TextBox ID="TextBox2" runat="server" OnTextChanged="TextBox2_TextChanged" TextMode="Password"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btOK" runat="server" Text="OK" OnClick="btOK_Click" />
             <br />
            <br />
            <asp:Label ID="LabelMessage" runat="server" ForeColor="Red"></asp:Label>

        </div>
    </form>
</body>
</html>
