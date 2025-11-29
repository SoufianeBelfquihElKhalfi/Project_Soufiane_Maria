<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="receptionist.aspx.cs" Inherits="Project_Soufiane_Maria.receptionist" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 360px">
            <!-- esta insercion se prueba con login -->
            CREDENTIALS<br /> 
            ID:<br />
            <br />
            <asp:TextBox ID="TextBoxCredentialID" runat="server" OnTextChanged="TextBoxCredentialID_TextChanged"></asp:TextBox>
            <br />
            <br />
            Username:<br />
            <br />
            <asp:TextBox ID="TextBoxUsername" runat="server" OnTextChanged="TextBoxUsername_TextChanged"></asp:TextBox>
            <br />
            <br />
            Profile:<br />
            <br />
            <asp:TextBox ID="TextBoxProfile" runat="server" OnTextChanged="TextBoxProfile_TextChanged" TextMode="Password"></asp:TextBox>
            <br />
            <br />
            Password:<br />
            <br />
            <asp:TextBox ID="TextBoxPassword" runat="server" OnTextChanged="TextBoxPassword_TextChanged" TextMode="Password"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="LabelMessageCredentials" runat="server" ForeColor="Red"></asp:Label>
            <br />
            <br />
        </div>

        <div style="width: 360px">
<!-- esta insercion se consulta -->
            CLIENTS<br />
            <br />
            ID:<br />
            <br />
            <asp:TextBox ID="TextBoxClientID" runat="server" OnTextChanged="TextBoxClientID_TextChanged"></asp:TextBox>
            <br />
            <br />
            Name:<br />
            <br />
            <asp:TextBox ID="TextBoxtName" runat="server" OnTextChanged="TextBoxClientName_TextChanged"></asp:TextBox>
            <br />
            <br />
            Date of birth:<br />
            <br />
            <asp:TextBox ID="TextBoxDOB" runat="server" OnTextChanged="TextBoxClientDOB_TextChanged"></asp:TextBox>
            <br />
            <br />
            Address:<br />
            <br />
            <asp:TextBox ID="TextBoxAddress" runat="server" OnTextChanged="TextBoxClientAddress_TextChanged"></asp:TextBox>
            <br />
            <br />
            Mobile phone:<br />
            <br />
            <asp:TextBox ID="TextBoxMobile" runat="server" OnTextChanged="TextBoxClientMobile_TextChanged"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="LabelMessageClients" runat="server" ForeColor="Red"></asp:Label>
            <br />
            <br />
        </div>

        <div style="width: 360px">
<!-- esta insercion se consulta -->
            RESERVATIONS<br />
            <br />
            ID:<br />
            <br />
            <asp:TextBox ID="TextBoxReservationID" runat="server" OnTextChanged="TextBoxReservationID_TextChanged"></asp:TextBox>
            <br />
            <br />
            Arrival date:<br />
            <br />
            <asp:TextBox ID="TextBoxArrivalDate" runat="server" OnTextChanged="TextBoxArrivalDate_TextChanged"></asp:TextBox>
            <br />
            <br />
            Departure date:<br />
            <br />
            <asp:TextBox ID="TextBoxDepartureDate" runat="server" OnTextChanged="TextBoxDepartureDate_TextChanged"></asp:TextBox>
            <br />
            <br />
            Type of room:<br />
            <br />
            <asp:TextBox ID="TextBoxRoomType" runat="server" OnTextChanged="TextBoxRoomType_TextChanged"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="LabelMessageReservations" runat="server" ForeColor="Red"></asp:Label>
            <br />
            <br />
        </div>

        <div style="width: 360px">
            <!-- CONSULTAS -->
            Search<br />
        </div>
    </form>
</body>
</html>
