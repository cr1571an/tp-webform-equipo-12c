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

        .provider-box {
            background-color: #f9fafb;
            border: 1px solid #e5e7eb;
            border-radius: 10px;
            padding: 14px;
        }

        .provider-badge {
            display: inline-block;
            background-color: #ede9fe;
            color: #6d28d9;
            font-weight: 600;
            font-size: 13px;
            padding: 4px 8px;
            border-radius: 999px;
        }

        .form-message {
            display: block;
            text-align: left;
            font-weight: 600;
            font-size: 14px;
            margin-bottom: 18px;
            padding: 10px 18px;
            border-radius: 8px;
            background-color: #fee2e2;
            color: #b91c1c;
            border: 1px solid #fecaca;
        }

        .table td,
        .table th {
            vertical-align: middle;
            white-space: nowrap;
        }

        .col-action {
            text-align: center;
            width: 120px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="d-flex justify-content-between align-items-center mb-4">
        <h1 class="page-title">
            <asp:Label ID="lblTitulo" runat="server" Text="Registrar artículo"></asp:Label>
        </h1>
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

        <asp:UpdatePanel ID="upFormulario" runat="server">
            <ContentTemplate>

                <h5 class="form-section-title mt-4">Proveedores asociados</h5>

                <div class="provider-box mb-3">

                    <div class="row align-items-end mb-3">
                        <div class="col-md-8">
                            <asp:Label ID="lblProveedor" runat="server" CssClass="form-label-custom d-block" Text="Proveedor"></asp:Label>

                            <asp:DropDownList
                                ID="ddlProveedor"
                                runat="server"
                                CssClass="form-control">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-4">
                            <asp:Button
                                ID="btnAsociarProveedor"
                                runat="server"
                                Text="Asociar proveedor"
                                CssClass="btn btn-outline-primary w-100"
                                CausesValidation="false"
                                OnClick="btnAsociarProveedor_Click" />
                        </div>
                    </div>

                    <asp:GridView
                        ID="dgvProveedoresAsociados"
                        runat="server"
                        CssClass="table table-striped table-hover mb-0"
                        AutoGenerateColumns="false"
                        GridLines="None"
                        EmptyDataText="No hay proveedores asociados."
                        OnRowCommand="dgvProveedoresAsociados_RowCommand">

                        <Columns>
                            <asp:TemplateField HeaderText="Proveedor">
                                <ItemTemplate>
                                    <span class="provider-badge"><%# Eval("Nombre") %></span>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField HeaderText="Teléfono" DataField="Telefono" />
                            <asp:BoundField HeaderText="Email" DataField="Email" />

                            <asp:TemplateField HeaderText="Acción">
                                <ItemTemplate>
                                    <asp:Button
                                        ID="btnQuitarProveedor"
                                        runat="server"
                                        Text="Quitar"
                                        CssClass="btn btn-sm btn-outline-danger"
                                        CommandName="QuitarProveedor"
                                        CommandArgument='<%# Eval("IdProveedor") %>'
                                        CausesValidation="false" />
                                </ItemTemplate>

                                <ItemStyle CssClass="col-action" />
                                <HeaderStyle CssClass="col-action" />
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>

                </div>

                <div class="mt-3">
                    <asp:Label
                        ID="lblMensajeError"
                        runat="server"
                        CssClass="form-message"
                        Visible="false">
                    </asp:Label>
                </div>

                <div class="form-actions">

                    <asp:Button
                        ID="btnCancelar"
                        runat="server"
                        CssClass="btn btn-outline-secondary"
                        Text="Cancelar"
                        CausesValidation="false"
                        OnClick="btnCancelar_Click" />

                    <asp:Button
                        ID="btnGuardar"
                        runat="server"
                        CssClass="btn btn-primary"
                        Text="Guardar artículo"
                        OnClick="btnGuardar_Click" />
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>

    </div>

</asp:Content>