<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Acceso.aspx.cs" Inherits="AppGestionNegocio.Web.Acceso" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Acceso al Sistema</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous" />
</head>
<body>
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-12 col-md-6 col-lg-4">          
                <h2 class="mb-4 text-center">Iniciar Sesión</h2>
                <form id="form1" runat="server">
                    <div class="mb-3">
                        <asp:Label ID="lblCorreo" runat="server" CssClass="form-label fw-bold" Text="Usuario/Correo"></asp:Label>
                        <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" TextMode="Email" placeholder="nombre@gmail.com"></asp:TextBox>
                    </div>
                    
                    <div class="mb-4">
                        <asp:Label ID="lblContrasenia" runat="server" CssClass="form-label fw-bold" Text="Contraseña"></asp:Label>
                        <asp:TextBox ID="txtContrasenia" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    </div>
                    <div class="d-grid">
                        <asp:Button ID="btnIniciarSesion" runat="server" CssClass="btn btn-primary btn-lg" Text="Ingresar" />
                    </div>
                </form>
            </div>
        </div>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</body>
</html>
