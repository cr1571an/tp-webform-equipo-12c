<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClienteFormulario.aspx.cs" Inherits="AppGestionNegocio.Web.ClienteFormulario" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registro de Cliente</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous" />
</head>
<body>
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-12 col-md-8 col-lg-6">
                <h2 class="mb-4">Registrar Cliente</h2>
                <form id="form1" runat="server">
                    <div class="mb-3">
                        <asp:Label ID="lblCuit" runat="server" CssClass="form-label" Text="CUIT"></asp:Label>
                        <asp:TextBox ID="txtCuit" runat="server" CssClass="form-control" placeholder="2X-XXXXXXXX-X"></asp:TextBox>
                    </div>
                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <asp:Label ID="lblNombre" runat="server" CssClass="form-label" Text="Nombre"></asp:Label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ingrese el nombre"></asp:TextBox>
                        </div>
                        <div class="col-md-6 mb-3">
                            <asp:Label ID="lblApellido" runat="server" CssClass="form-label" Text="Apellido"></asp:Label>
                            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Ingrese el apellido"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <asp:Label ID="lblTelefono" runat="server" CssClass="form-label" Text="Teléfono"></asp:Label>
                            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" TextMode="Phone" placeholder="Ej: 11 1234-5678"></asp:TextBox>
                        </div>
                        <div class="col-md-6 mb-3">
                            <asp:Label ID="lblEmail" runat="server" CssClass="form-label" Text="Email"></asp:Label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="correo@gmail.com"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 mb-3">
                            <asp:Label ID="lblCodigoPostal" runat="server" CssClass="form-label" Text="Código Postal"></asp:Label>
                            <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control" placeholder="Ej: 1623"></asp:TextBox>
                        </div>
                        <div class="col-md-8 mb-3">
                            <asp:Label ID="lblDomicilio" runat="server" CssClass="form-label" Text="Domicilio"></asp:Label>
                            <asp:TextBox ID="txtDomicilio" runat="server" CssClass="form-control" placeholder="Calle, Número"></asp:TextBox>
                        </div>
                    </div>
                    <div class="mt-4">
                        <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary me-2" Text="Guardar" />
                        <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-outline-secondary" Text="Cancelar" />
                    </div>
                </form>
            </div>
        </div>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</body>
</html>