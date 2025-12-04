<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Project_Soufiane_Maria.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>

    <!-- Estilos CSS -->
    <style>
        /* Estilos generales para el formulario */
        form {
            width: 100%;
            max-width: 400px;  /* Ajusta el ancho del formulario */
            margin: 0 auto;  /* Centra el formulario en la página */
            padding: 20px;
            border-radius: 8px;
            background-color: #f9f9f9;  /* Fondo claro */
            box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1);  /* Sombra suave */
        }

        /* Estilo de los campos de texto (TextBox) */
        input[type="text"],
        input[type="password"] {
            width: 100%;
            padding: 10px;
            margin: 10px 0;  /* Espacio entre campos */
            border: 1px solid #ccc;  /* Borde gris */
            border-radius: 4px;  /* Bordes redondeados */
            font-size: 16px;
            box-sizing: border-box;
        }


        /* Estilo del botón (Button) */
        .btn{
            width: 100%;
            padding: 12px;
            background-color: darkslateblue;  /* Color de fondo del botón */
            color: white;  /* Color del texto */
            border: none;
            border-radius: 4px;
            font-size: 18px;
            cursor: pointer;  /* Puntero de mano al pasar por encima */
            box-sizing: border-box;
            transition: background-color 0.3s ease;  /* Transición de color de fondo */
        }

        /* Cambio de color al pasar el ratón sobre el botón */
        .btn:hover
        {
            background-color: cornflowerblue;
        }

        /* Espaciado entre los elementos */
        br {
            margin: 5px 0;
        }

        /* Estilo de los mensajes de validación */
        .LabelMessage {
            color: red;
            font-size: 14px;
            text-align: center;
        }

        /* Estilo del contenedor de la página */
        body {
            font-family: Arial, sans-serif;
            background-color: #e9ecef;  /* Color de fondo suave */
            padding: 30px;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
        }

        /* Estilo de la cabecera */
        h1 {
            text-align: center;
            font-size: 24px;
            color: #333;
            margin-bottom: 20px;
        }

        #btnBackToIndex {
            display: inline-block; /* Evita que ocupe todo el ancho */
width: auto; /* Asegura tamaño ajustado al contenido */
            padding: 12px;
             background-color: darkslateblue;  /* Color de fondo del botón */
 color: white;  /* Color del texto */
 border: none;
 border-radius: 4px;
 font-size: 18px;
 cursor: pointer;  /* Puntero de mano al pasar por encima */
 box-sizing: border-box;
 transition: background-color 0.3s ease;  /* Transición de color de fondo */
            
        }

        /* Cambio de color al pasar el ratón sobre el botón */
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
