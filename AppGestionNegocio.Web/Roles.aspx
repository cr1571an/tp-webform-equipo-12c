<%@ Page Title="Roles" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="AppGestionNegocio.Web.Roles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .page-actions {
            display: flex;
            gap: 8px;
        }

        .table-actions {
            display: flex;
            gap: 6px;
            flex-wrap: nowrap;
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

        .role-name {
            font-weight: 600;
        }

        .role-description {
            color: #6b7280;
            font-size: 13px;
        }

        .badge-users {
            padding: 5px 10px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 600;
            background-color: #ede9fe;
            color: #6f42c1;
        }

        .badge-active {
            padding: 5px 10px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 600;
            background-color: #dcfce7;
            color: #166534;
        }

        .empty-state {
            text-align: center;
            color: #6b7280;
            font-weight: 500;
            padding: 28px;
            background-color: #ffffff;
        }

        .col-name {
            width: 24%;
        }

        .col-description {
            width: 42%;
        }

        .col-users {
            width: 16%;
        }

        .col-actions {
            width: 18%;
        }

        .btn-primary {
            height: 26px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-3">
        <div>
            <h1 class="page-title">Roles</h1>
        </div>

        <div class="page-actions">
            <asp:TextBox
                ID="txtFiltroNombre"
                runat="server"
                CssClass="form-control filtro-input"
                placeholder="Buscar Rol">
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
        <h5 class="form-section-title">Nuevo rol</h5>

        <div class="row">
            <div class="col-md-3 mb-3">
                <label>Nombre</label>&nbsp;
                <asp:TextBox
                    ID="txtNombre"
                    runat="server"
                    CssClass="form-control"
                    MaxLength="60"
                    placeholder="Ej: Vendedor"></asp:TextBox>
            </div>

            <div class="col-md-9 mb-3">
                <label>Descripción</label>&nbsp;
                <asp:TextBox
                    ID="txtDescripcion"
                    runat="server"
                    CssClass="form-control"
                    MaxLength="255"
                    placeholder="Ej: Registra ventas en el sistema"></asp:TextBox>
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
                    Style="height: 38px;"
                    OnClick="btnAgregar_Click" />
            </div>
        </div>
    </div>

    <div class="dashboard-card">

        <h5 class="form-section-title">Roles registrados</h5>

        <div class="table-responsive">
            <asp:GridView
                ID="dgvRoles"
                runat="server"
                CssClass="table table-striped table-hover mb-0"
                AutoGenerateColumns="false"
                GridLines="None"
                DataKeyNames="IdRol"
                EmptyDataText="No hay roles activos registrados."
                OnRowCommand="dgvRoles_RowCommand">

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

                    <asp:TemplateField HeaderText="Descripción">
                        <ItemTemplate>
                            <span title='<%# Eval("Descripcion") %>'>
                                <%# Eval("Descripcion") != null && Eval("Descripcion").ToString().Length > 40
                ? Eval("Descripcion").ToString().Substring(0, 40) + "..."
                : Eval("Descripcion") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <div class="table-actions">
                                <asp:Button
                                    ID="btnEditar"
                                    runat="server"
                                    Text="Modificar"
                                    CssClass="btn btn-sm btn-outline-primary"
                                    CommandName="EditarModal"
                                    CommandArgument='<%# Eval("IdRol") %>' />

                                <asp:Button
                                    ID="btnEliminar"
                                    runat="server"
                                    Text="Eliminar"
                                    CssClass="btn btn-sm btn-outline-danger"
                                    CommandName="EliminarRol"
                                    CommandArgument='<%# Eval("IdRol") %>'
                                    OnClientClick="return confirm('¿Seguro que querés eliminar este rol?');" />
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

    <div class="modal fade" id="modalEditar" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title">Editar Rol</h5>
                </div>

                <div class="modal-body">

                    <asp:HiddenField ID="hfIdRol" runat="server" />

                    <label>Nombre</label>
                    <asp:TextBox ID="txtNombreModal" runat="server" CssClass="form-control" />

                    <label class="mt-2">Descripción</label>
                    <asp:TextBox
                        ID="txtDescripcionModal"
                        runat="server"
                        CssClass="form-control"
                        TextMode="MultiLine"
                        Rows="4"
                        Style="resize: vertical;" />

                    <asp:Label
                        ID="lblMensajeModal"
                        runat="server"
                        CssClass="text-danger">
                    </asp:Label>


                </div>

                <div class="modal-footer">
                    <asp:Button
                        ID="btnGuardarModal"
                        runat="server"
                        Text="Guardar"
                        CssClass="btn btn-success"
                        OnClick="btnGuardarModal_Click" />

                    <button type="button" class="btn btn-secondary" data-dismiss="modal">
                        Cancelar
                    </button>
                </div>

            </div>
        </div>
    </div>


</asp:Content>
