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
        justify-content: center;
        gap: 50px;
        padding: 50px 30px;
    }

    .card {
        background-color: white;
        border-radius: 10px;
        padding: 30px 25px;
        width: 320px;
        box-shadow: 0 5px 15px rgba(0,0,0,0.1);
    }

    .card h2 {
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

    .reservations-listbox {
        width: 100%;
        height: 140px;
        font-size: 14px;
        border-radius: 6px;
        border: 1.5px solid darkslateblue;
        padding: 4px;
        color: #2c3e50;
        cursor: pointer;
    }

</style>
    </head>
<body>
    <form id="form1" runat="server">
        <header>
            <asp:Label ID="LabelWelcome" runat="server" Text="Welcome!"></asp:Label>
            <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" CssClass="header-btn" />
        </header>

        <div class="container">
            <div class="card">
                <h2>Client Info</h2>

                    <asp:Label ID="LabelId" runat="server"></asp:Label><br><br>
                    <asp:Label ID="LabelDob" runat="server"></asp:Label><br><br>
                    <asp:Label ID="LabelAddress" runat="server"></asp:Label><br><br>
                    <asp:Label ID="LabelMobile" runat="server"></asp:Label>

            </div>

            <div class="card">
                <h2>Reservations</h2>
                <asp:ListBox ID="ReservationsList" runat="server"
                             SelectionMode="Single" CssClass="reservations-listbox"
                             AutoPostBack="true"
                             OnSelectedIndexChanged="ReservationsList_SelectedIndexChanged">
                </asp:ListBox>
                <br><br>

                    <asp:Label ID="LabelArrival" runat="server"></asp:Label><br><br>
                    <asp:Label ID="LabelDeparture" runat="server"></asp:Label><br><br>
                    <asp:Label ID="LabelRoom" runat="server"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>