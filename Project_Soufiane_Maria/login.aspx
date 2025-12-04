<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Project_Soufiane_Maria.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>

  
    <style>
       
        form {
            width: 100%;
            max-width: 400px;  
            margin: 0 auto;  
            padding: 20px;
            border-radius: 8px;
            background-color: #f9f9f9; 
            box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1);  
        }

        
        input[type="text"],
        input[type="password"] {
            width: 100%;
            padding: 10px;
            margin: 10px 0;  
            border: 1px solid #ccc; 
            border-radius: 4px;  
            font-size: 16px;
            box-sizing: border-box;
        }


       
        .btn{
            width: 100%;
            padding: 12px;
            background-color: darkslateblue; 
            color: white;  
            border: none;
            border-radius: 4px;
            font-size: 18px;
            cursor: pointer; 
            box-sizing: border-box;
            transition: background-color 0.3s ease;  
        }

       
        .btn:hover
        {
            background-color: cornflowerblue;
        }

       
        br {
            margin: 5px 0;
        }

       
        .LabelMessage {
            color: red;
            font-size: 14px;
            text-align: center;
        }

        
        body {
            font-family: Arial, sans-serif;
            background-color: #e9ecef;  
            padding: 30px;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
        }

      
        h1 {
            text-align: center;
            font-size: 24px;
            color: #333;
            margin-bottom: 20px;
        }

        #btnBackToIndex {
            display: inline-block;
width: auto; 
            padding: 12px;
             background-color: darkslateblue; 
 color: white;
 border: none;
 border-radius: 4px;
 font-size: 18px;
 cursor: pointer; 
 box-sizing: border-box;
 transition: background-color 0.3s ease;  
            
        }

      
        #btnBackToIndex:hover {
            background-color: cornflowerblue;
        }
        </style>
</head>

<body>
    <form id="form1" runat="server">
        <asp:Button ID="btnBackToIndex" runat="server" Text="Back to landing" OnClick="btnBackToIndex_Click" CssClass="btn" />
<br />
<br />
            Username:<br />
            <br />
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br />
            <br />
            Password:<br />
            <br />
            <asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btOK" runat="server" Text="OK" OnClick="btOK_Click" CssClass="btn" />
             <br />
            <br />
            <asp:Label ID="LabelMessage" runat="server" ForeColor="Red"></asp:Label>

        </div>
    </form>
</body>
</html>
