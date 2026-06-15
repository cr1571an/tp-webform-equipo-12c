<%@ Page Title="Artículo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ArticuloFormulario.aspx.cs" Inherits="AppGestionNegocio.Web.ArticuloFormulario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-section-title {
            font-size: 18px;
            font-weight: 700;
            margin-bottom: 16px;
        }
        .form-label-custom {
            font-weight: 600;
            font-size: 14px;
            color: #374151;
            margin-bottom: 6px;
        }
        .form-actions {
            display: flex;
            justify-content: flex-end;
            gap: 8px;
            border-top: 1px solid #e5e7eb;
            padding-top: 18px;
            margin-top: 10px;
        }
        .preview-box {
            background-color: #f9fafb;
            border: 1px solid #e5e7eb;
            border-radius: 12px;
            padding: 16px;
            text-align: center;
            min-height: 140px;
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
        }
        .preview-img {
            max-width: 100%;
            max-height: 150px;
            border-radius: 10px;
            border: 1px solid #e5e7eb;
            background-color: white;
            padding: 8px;
        }
        select.form-control {
            height: 38px;
            padding: 6px 12px;
            border: 1px solid #ced4da;
            border-radius: 6px;
        }
        .dashboard-card {
        background: #ffffff;
        padding: 24px;
        border: 1px solid #e5e7eb;
        border-radius: 12px;
    }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="d-flex justify-content-between align-items-center mb-4">
        <h1 class="page-title">Registrar artículo</h1>
        <asp:Button ID="btnVolver" runat="server" Text="Volver al listado" CssClass="btn btn-outline-secondary" OnClick="btnVolver_Click"/>
    </div>

    <div class="dashboard-card">
        <h5 class="form-section-title">Datos principales</h5>
        <div class="row">
            <div class="col-md-4 mb-3">
                <asp:Label ID="lblCategoria" runat="server" CssClass="form-label-custom d-block" Text="Categoría"></asp:Label>
                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>

            <div class="col-md-4 mb-3">
                <asp:Label ID="lblMarca" runat="server" CssClass="form-label-custom d-block" Text="Marca"></asp:Label>
                <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>

            <div class="col-md-4 mb-3">
                <asp:Label ID="lblIva" runat="server" CssClass="form-label-custom d-block" Text="Alícuota IVA"></asp:Label>
                <asp:DropDownList ID="ddlIva" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>

        <div class="mb-3">
            <asp:Label ID="lblNombre" runat="server" CssClass="form-label-custom d-block" Text="Nombre del artículo"></asp:Label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ej: Pro Plan adulto carne x20kg"></asp:TextBox>
        </div>

        <div class="mb-3">
            <asp:Label ID="lblDescripcion" runat="server" CssClass="form-label-custom d-block" Text="Descripción"></asp:Label>
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Detalles del artículo..."></asp:TextBox>
        </div>

        <h5 class="form-section-title mt-4">Precio y stock</h5>

        <div class="row">
            <div class="col-md-6 mb-3">
                <asp:Label ID="lblPrecio" runat="server" CssClass="form-label-custom d-block" Text="Precio de venta"></asp:Label>
                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" placeholder="0.00"></asp:TextBox>
            </div>

            <div class="col-md-6 mb-3">
                <asp:Label ID="lblStock" runat="server" CssClass="form-label-custom d-block" Text="Stock inicial"></asp:Label>
                <asp:TextBox ID="txtStock" runat="server" Text="0" CssClass="form-control" TextMode="Number" placeholder="0"></asp:TextBox>
            </div>
        </div>

        <h5 class="form-section-title mt-4">Imagen del artículo</h5>
        <asp:UpdatePanel ID="upImagen" runat="server">
            <ContentTemplate>
                <div class="row align-items-start">
                    <div class="col-md-8 mb-3">
                        <asp:Label ID="lblUrlImagen" runat="server" CssClass="form-label-custom d-block" Text="URL de imagen"></asp:Label>
                        <asp:TextBox ID="txtImagenUrl" runat="server" CssClass="form-control" placeholder="https://ejemplo.com" AutoPostBack="true" OnTextChanged="txtImagenUrl_TextChanged"></asp:TextBox>
                    </div>

                    <div class="col-md-4 mb-3">
                        <div class="preview-box">
                            <asp:Label ID="lblImagenTitulo" runat="server" CssClass="form-label-custom d-block mb-2" Text="Vista previa"></asp:Label>
                            <asp:Image ID="imgPreview" runat="server" CssClass="preview-img" ImageUrl="https://grupoact.com.ar/wp-content/uploads/2020/04/placeholder.png" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="upBotones" runat="server">
            <ContentTemplate>
                <div class="mt-3">
                    <asp:Label ID="lblMensajeError" runat="server" CssClass="alert alert-danger d-block" Visible="false" />
                </div>

                <div class="form-actions">
                    <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-danger" Text="Eliminar" Visible="false" CausesValidation="false" OnClick="btnEliminar_Click" OnClientClick="return confirm('¿Estás seguro de que deseas eliminar este artículo?');" />
                    <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-outline-secondary" Text="Cancelar" OnClick="btnCancelar_Click"/>
                    <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary" Text="Guardar artículo" OnClick="btnGuardar_Click"/>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>