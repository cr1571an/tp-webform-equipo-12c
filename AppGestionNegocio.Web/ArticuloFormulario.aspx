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

        .helper-text {
            color: #6b7280;
            font-size: 13px;
            margin-top: 4px;
        }

        .form-actions {
            display: flex;
            justify-content: flex-end;
            gap: 8px;
            border-top: 1px solid #e5e7eb;
            padding-top: 18px;
            margin-top: 10px;
        }

        .detail-box {
            background-color: #f9fafb;
            border: 1px solid #e5e7eb;
            border-radius: 10px;
            padding: 16px;
            margin-top: 8px;
        }

        .preview-box {
            background-color: #f9fafb;
            border: 1px solid #e5e7eb;
            border-radius: 12px;
            padding: 16px;
            text-align: center;
            height: 100%;
        }

        .preview-img {
            max-width: 100%;
            max-height: 150px;
            border-radius: 10px;
            border: 1px solid #e5e7eb;
            background-color: white;
            padding: 8px;
        }

        .preview-placeholder {
            width: 160px;
            height: 120px;
            max-width: 100%;
            margin: 0 auto;
            border: 1px dashed #c4b5fd;
            border-radius: 10px;
            background-color: #ede9fe;
            color: #6f42c1;
            display: flex;
            align-items: center;
            justify-content: center;
            flex-direction: column;
            font-weight: 600;
        }

        .preview-placeholder i {
            font-size: 28px;
            margin-bottom: 6px;
        }

        .empty-state {
            text-align: center;
            color: #6b7280;
            font-weight: 500;
            padding: 22px;
            background-color: #ffffff;
        }

        .table td,
        .table th {
            vertical-align: middle;
            white-space: nowrap;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-3">
        <div>
            <h1 class="page-title">Registrar artículo</h1>
        </div>

        <a href="Articulos.aspx" class="btn btn-outline-secondary">
            Volver al listado
        </a>
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
                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" TextMode="Number" placeholder="0.00"></asp:TextBox>
            </div>

            <div class="col-md-6 mb-3">
                <asp:Label ID="lblStock" runat="server" CssClass="form-label-custom d-block" Text="Stock inicial"></asp:Label>
                <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" TextMode="Number" placeholder="0"></asp:TextBox>
            </div>
        </div>

        <h5 class="form-section-title mt-4">Imagen del artículo</h5>

        <div class="row">
            <div class="col-md-8 mb-3">
                <asp:Label ID="lblUrlImagen" runat="server" CssClass="form-label-custom d-block" Text="URL de imagen"></asp:Label>
                <asp:TextBox ID="txtUrlImagen" runat="server" CssClass="form-control" placeholder="https://ejemplo.com/imagen.png"></asp:TextBox>
            </div>

            <div class="col-md-4 mb-3">
                <div class="preview-box">
                    <asp:Label ID="lblImagenTitulo" runat="server" CssClass="form-label-custom d-block mb-2" Text="Vista previa"></asp:Label>

                    <div class="preview-placeholder">
                        <i class="fas fa-image"></i>
                        Sin imagen
                    </div>

                    <asp:Image ID="imgPreview" runat="server" CssClass="preview-img" Style="display:none;" />
                </div>
            </div>
        </div>

        <h5 class="form-section-title mt-4">Proveedores asociados</h5>

        <div class="detail-box">
            <div class="row">
                <div class="col-md-8 mb-3">
                    <label class="form-label-custom d-block">Proveedor</label>
                    <select class="form-control">
                        <option>Seleccione un proveedor</option>
                    </select>
                </div>

                <div class="col-md-4 mb-3 d-flex align-items-end">
                    <button type="button" class="btn btn-outline-primary w-100">
                        Asociar proveedor
                    </button>
                </div>
            </div>

            <div class="table-responsive mt-2">
                <table class="table table-sm table-striped table-hover mb-0">
                    <thead>
                        <tr>
                            <th>Proveedor</th>
                            <th>Teléfono</th>
                            <th>Email</th>
                            <th class="text-center">Acción</th>
                        </tr>
                    </thead>
                </table>
            </div>
        </div>

        <div class="form-actions">
            <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-outline-secondary" Text="Cancelar" CausesValidation="false" />
            <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary" Text="Guardar artículo" />
        </div>

    </div>

</asp:Content>