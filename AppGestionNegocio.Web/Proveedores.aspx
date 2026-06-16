<%@ Page Title="Proveedores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Proveedores.aspx.cs" Inherits="AppGestionNegocio.Web.Proveedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .page-actions {
            display: flex;
            gap: 8px;
            align-items: center;
        }

        .filtro-input {
            width: 220px;
        }

        .table-actions {
            display: flex;
            gap: 6px;
            flex-wrap: nowrap;
            justify-content: center;
        }

        .table td,
        .table th {
            vertical-align: middle;
            white-space: nowrap;
        }

        .form-section-title {
            font-size: 18px;
            font-weight: 700;
            margin-bottom: 16px;
        }

        label {
            font-weight: 600;
            font-size: 14px;
            color: #374151;
        }

        .provider-name {
            font-weight: 600;
        }

        .message {
            display: block;
            font-size: 14px;
            font-weight: 600;
            margin-top: 6px;
            margin-bottom: 0;
        }

        .col-actions {
            text-align: center;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-3">
        <div>
            <h1 class="page-title">Proveedores</h1>
        </div>

        <div class="page-actions">
            <asp:TextBox
                ID="txtFiltroNombre"
                runat="server"
                CssClass="form-control filtro-input"
                placeholder="Buscar proveedor">
            </asp:TextBox>

            <asp:Button
                ID="btnFiltrar"
                runat="server"
                Text="Filtrar"
                CssClass="btn btn-outline-secondary"
                OnClick="btnFiltrar_Click" />

            <asp:Button
                ID="btnLimpiarFiltro"
                runat="server"
                Text="Limpiar"
                CssClass="btn btn-outline-secondary"
                OnClick="btnLimpiarFiltro_Click" />
        </div>
    </div>

    <div class="dashboard-card mb-3">
        <h5 class="form-section-title">Nuevo proveedor</h5>

        <div class="row">
            <div class="col-md-4 mb-3">
                <label>Nombre</label>
                <asp:TextBox
                    ID="txtNombre"
                    runat="server"
                    CssClass="form-control"
                    MaxLength="60"
                    placeholder="Ej: Distribuidora Patitas">
                </asp:TextBox>
            </div>

            <div class="col-md-4 mb-3">
                <label>Teléfono</label>
                <asp:TextBox
                    ID="txtTelefono"
                    runat="server"
                    CssClass="form-control"
                    MaxLength="11"
                    placeholder="Ej: 1134567890">
                </asp:TextBox>
            </div>

            <div class="col-md-4 mb-3">
                <label>Email</label>
                <asp:TextBox
                    ID="txtEmail"
                    runat="server"
                    CssClass="form-control"
                    MaxLength="100"
                    placeholder="Ej: proveedor@mail.com">
                </asp:TextBox>
            </div>

            <div class="col-md-12">
                <asp:Label
                    ID="lblMensaje"
                    runat="server"
                    CssClass="message text-danger">
                </asp:Label>
            </div>

            <div class="col-md-12 mt-3">
                <asp:Button
                    ID="btnAgregar"
                    runat="server"
                    Text="Agregar"
                    CssClass="btn btn-primary"
                    OnClick="btnAgregar_Click" />
            </div>
        </div>
    </div>

    <div class="dashboard-card">
        <h5 class="form-section-title">Proveedores registrados</h5>

        <div class="table-responsive">
            <asp:GridView
                ID="dgvProveedores"
                runat="server"
                CssClass="table table-striped table-hover mb-0"
                AutoGenerateColumns="false"
                GridLines="None"
                DataKeyNames="IdProveedor"
                EmptyDataText="No hay proveedores activos registrados."
                OnRowEditing="dgvProveedores_RowEditing"
                OnRowCancelingEdit="dgvProveedores_RowCancelingEdit"
                OnRowUpdating="dgvProveedores_RowUpdating"
                OnRowCommand="dgvProveedores_RowCommand">

                <Columns>

                    <asp:TemplateField HeaderText="Nombre">
                        <ItemTemplate>
                            <span class="provider-name"><%# Eval("Nombre") %></span>
                        </ItemTemplate>

                        <EditItemTemplate>
                            <asp:TextBox
                                ID="txtNombreEdit"
                                runat="server"
                                CssClass="form-control"
                                MaxLength="60"
                                Text='<%# Bind("Nombre") %>'>
                            </asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Teléfono">
                        <ItemTemplate>
                            <%# Eval("Telefono") %>
                        </ItemTemplate>

                        <EditItemTemplate>
                            <asp:TextBox
                                ID="txtTelefonoEdit"
                                runat="server"
                                CssClass="form-control"
                                MaxLength="11"
                                Text='<%# Bind("Telefono") %>'>
                            </asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <%# Eval("Email") %>
                        </ItemTemplate>

                        <EditItemTemplate>
                            <asp:TextBox
                                ID="txtEmailEdit"
                                runat="server"
                                CssClass="form-control"
                                MaxLength="100"
                                Text='<%# Bind("Email") %>'>
                            </asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <div class="table-actions">
                                <asp:Button
                                    ID="btnEditar"
                                    runat="server"
                                    Text="Modificar"
                                    CssClass="btn btn-sm btn-outline-primary"
                                    CommandName="Edit" />

                                <asp:Button
                                    ID="btnEliminar"
                                    runat="server"
                                    Text="Eliminar"
                                    CssClass="btn btn-sm btn-outline-danger"
                                    CommandName="EliminarProveedor"
                                    CommandArgument='<%# Eval("IdProveedor") %>'
                                    OnClientClick="return confirm('¿Seguro que querés eliminar este proveedor?');" />
                            </div>
                        </ItemTemplate>

                        <EditItemTemplate>
                            <div class="table-actions">
                                <asp:Button
                                    ID="btnGuardarEdit"
                                    runat="server"
                                    Text="Guardar"
                                    CssClass="btn btn-sm btn-success"
                                    CommandName="Update" />

                                <asp:Button
                                    ID="btnCancelarEdit"
                                    runat="server"
                                    Text="Cancelar"
                                    CssClass="btn btn-sm btn-secondary"
                                    CommandName="Cancel" />
                            </div>
                        </EditItemTemplate>

                        <ItemStyle CssClass="col-actions" />
                        <HeaderStyle CssClass="col-actions" />
                    </asp:TemplateField>

                </Columns>

            </asp:GridView>
        </div>
    </div>

</asp:Content>