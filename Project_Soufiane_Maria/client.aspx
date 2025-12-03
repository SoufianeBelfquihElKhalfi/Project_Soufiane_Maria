<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="client.aspx.cs" Inherits="Project_Soufiane_Maria.client" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Client</title>
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body {
            font-family: Arial, sans-serif;
            background-color: #e9ecef;
            height: 100vh;
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
        }

        .header-welcome {
            font-size: 18px;
            font-weight: bold;
        }

        .header-btn {
            background-color: white;
            color: darkslateblue;
            border: none;
            padding: 8px 16px;
            border-radius: 4px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        .header-btn:hover {
            background-color: cornflowerblue;
            color: white;
        }

        .container {
            display: flex;
            flex: 1;
            justify-content: space-between;
            align-items: center;
            padding: 50px;
        }

        .left-div {
            flex: 1;
        }

        .right-div {
            flex: 1;
            display: flex;
            justify-content: center;
        }

        .btn {
            padding: 12px 24px;
            background-color: darkslateblue;
            color: white;
            border-radius: 4px;
            font-size: 18px;
            border: none;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        .btn:hover {
            background-color: cornflowerblue;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <header>
            <div class="header-welcome">
                <asp:Label ID="LabelWelcome" runat="server" Text="Welcome, user!"></asp:Label>
            </div>
            <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" CssClass="header-btn" />
        </header>

        <div class="container">
            <div class="left-div">
    <asp:Label ID="LabelId" runat="server" ></asp:Label><br />
    <asp:Label ID="LabelDob" runat="server" ></asp:Label><br />
    <asp:Label ID="LabelAddress" runat="server"></asp:Label><br />
    <asp:Label ID="LabelMobile" runat="server"></asp:Label><br />
</div>

            <div class="right-div">
                <asp:Label ID="LabelArrival" runat="server" ></asp:Label><br />
<asp:Label ID="LabelDeparture" runat="server" ></asp:Label><br />
<asp:Label ID="LabelRoom" runat="server"></asp:Label><br />
<asp:Label ID="LabelDNI" runat="server"></asp:Label><br />
            </div>
        </div>
    </form>
</body>
</html>