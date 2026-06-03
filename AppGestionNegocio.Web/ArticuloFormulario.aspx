<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticuloFormulario.aspx.cs" Inherits="AppGestionNegocio.Web.ArticuloFormulario" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registro de producto</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous" />
</head>
<body>
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-12 col-md-10 col-lg-8">
                <h2 class="mb-4">Registrar Nuevo Producto</h2>
                <form id="form1" runat="server">
                    <div class="row">
                        <div class="col-md-4 mb-3">
                            <asp:Label ID="lblCategoria" runat="server" CssClass="form-label fw-bold" Text="Categoría"></asp:Label>
                            <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select"></asp:DropDownList>
                        </div>
                        <div class="col-md-4 mb-3">
                            <asp:Label ID="lblMarca" runat="server" CssClass="form-label fw-bold" Text="Marca"></asp:Label>
                            <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-select"></asp:DropDownList>
                        </div>
                        <div class="col-md-4 mb-3">
                            <asp:Label ID="lblIva" runat="server" CssClass="form-label fw-bold" Text="IVA"></asp:Label>
                            <asp:DropDownList ID="ddlIva" runat="server" CssClass="form-select"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="mb-3">
                        <asp:Label ID="lblNombre" runat="server" CssClass="form-label fw-bold" Text="Nombre del Producto"></asp:Label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ej: Pro Plan adulto carne x20kg'"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <asp:Label ID="lblDescripcion" runat="server" CssClass="form-label fw-bold" Text="Descripción"></asp:Label>
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Detalles del producto..."></asp:TextBox>
                    </div>
                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <asp:Label ID="lblPrecio" runat="server" CssClass="form-label fw-bold" Text="Precio ($)"></asp:Label>
                            <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" TextMode="Number" step="0.01" placeholder="0.00"></asp:TextBox>
                        </div>
                        <div class="col-md-6 mb-3">
                            <asp:Label ID="lblStock" runat="server" CssClass="form-label fw-bold" Text="Stock Inicial"></asp:Label>
                            <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" TextMode="Number" placeholder="0"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row mb-4">
                        <div class="col-md-8 mb-3">
                            <asp:Label ID="lblUrlImagen" runat="server" CssClass="form-label fw-bold" Text="URL de la Imagen o formato png"></asp:Label>
                            <asp:TextBox ID="txtUrlImagen" runat="server" CssClass="form-control" placeholder="https://ejemplo.com/imagen.png" oninput="mostrarVistaPrevia()"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblImagenTitulo" runat="server" CssClass="form-label fw-bold" Text="Imagen"></asp:Label>
                            <asp:Image ID="imgPreview" runat="server" ImageUrl="https://tse3.mm.bing.net/th/id/OIP.WSFLrRsftwVkbJyYqpMLAAHaGo?pid=Api&P=0&h=180" style="max-height: 150px;" />
                        </div>
                    </div>
                    <div class="border-top pt-3">
                        <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary btn-lg me-2" Text="Guardar" />
                        <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-outline-secondary btn-lg" Text="Cancelar" />
                    </div>
                </form>
            </div>
        </div>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</body>
</html>