<%@ Page Title="Roles" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="AppGestionNegocio.Web.Roles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .page-actions {
            display: flex;
            gap: 8px;
            align-items: center;
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

        .role-name {
            font-weight: 600;
        }

        .role-description {
            color: #6b7280;
            font-size: 13px;
        }

        .message {
            display: block;
            font-size: 14px;
            font-weight: 600;
            margin-top: 6px;
            margin-bottom: 0;
        }

        .col-name {
            width: 24%;
        }

        .col-description {
            width: 52%;
        }

        .col-actions {
            width: 24%;
            text-align: center;
        }

        .modal-top .modal-dialog {
            margin-top: 40px;
        }

        .switch-container {
            display: flex;
            align-items: center;
            gap: 8px;
        }

        .switch-text {
            font-size: 13px;
            font-weight: 600;
            color: #374151;
        }

        .custom-switch {
            position: relative;
            display: inline-block;
            width: 52px;
            height: 28px;
            margin: 0;
        }

        .custom-switch input[type="checkbox"] {
            opacity: 0;
            width: 0;
            height: 0;
        }

        .custom-slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background: #d1d5db;
            transition: 0.3s;
            border-radius: 999px;
        }

        .custom-slider:before {
            position: absolute;
            content: "";
            height: 22px;
            width: 22px;
            left: 3px;
            bottom: 3px;
            background-color: white;
            transition: 0.3s;
            border-radius: 50%;
            box-shadow: 0 2px 6px rgba(0,0,0,0.25);
        }

        .custom-switch input[type="checkbox"]:checked + .custom-slider {
            background: linear-gradient(135deg, #6f42c1 0%, #0d6efd 100%);
        }

        .custom-switch input[type="checkbox"]:checked + .custom-slider:before {
            transform: translateX(24px);
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-3">
        <div>
            <h1 class="page-title">Roles</h1>
        </div>

        <div class="page-actions">

            <div id="contenedorInactivos" runat="server" class="switch-container">
                <span class="switch-text">Ver inactivos</span>

                <label class="custom-switch">
                    <asp:CheckBox
                        ID="chkVerInactivos"
                        runat="server"
                        AutoPostBack="true"
                        OnCheckedChanged="chkVerInactivos_CheckedChanged" />
                    <span class="custom-slider"></span>
                </label>
            </div>

        </div>
    </div>

    <div id="cardNuevoRol" runat="server" class="dashboard-card mb-3">
        <h5 class="form-section-title">Nuevo rol</h5>

        <div class="row">
            <div class="col-md-3 mb-3">
                <label>Nombre</label>

                <asp:TextBox
                    ID="txtNombre"
                    runat="server"
                    CssClass="form-control"
                    MaxLength="60"
                    placeholder="Ej: Vendedor">
                </asp:TextBox>
            </div>

            <div class="col-md-7 mb-3">
                <label>Descripción</label>

                <asp:TextBox
                    ID="txtDescripcion"
                    runat="server"
                    CssClass="form-control"
                    MaxLength="255"
                    placeholder="Ej: Registra ventas en el sistema">
                </asp:TextBox>
            </div>

            <div class="col-md-2 mb-3 d-flex align-items-end">
                <asp:Button
                    ID="btnAgregar"
                    runat="server"
                    Text="Agregar"
                    CssClass="btn btn-primary w-100"
                    OnClick="btnAgregar_Click" />
            </div>

            <div class="col-md-12">
                <asp:Label
                    ID="lblMensaje"
                    runat="server"
                    CssClass="message text-danger">
                </asp:Label>
            </div>
        </div>
    </div>

    <div class="dashboard-card">

        <h5 class="form-section-title">
            <asp:Label ID="lblTituloListado" runat="server" Text="Roles registrados"></asp:Label>
        </h5>

        <asp:Label
            ID="lblMensajeListado"
            runat="server"
            CssClass="message text-danger">
        </asp:Label>

        <div class="table-responsive mt-2">
            <asp:GridView
                ID="dgvRoles"
                runat="server"
                CssClass="table table-striped table-hover mb-0"
                AutoGenerateColumns="false"
                GridLines="None"
                DataKeyNames="IdRol"
                EmptyDataText="No hay roles activos registrados."
                OnRowEditing="dgvRoles_RowEditing"
                OnRowCancelingEdit="dgvRoles_RowCancelingEdit"
                OnRowUpdating="dgvRoles_RowUpdating"
                OnRowCommand="dgvRoles_RowCommand"
                OnRowDataBound="dgvRoles_RowDataBound">

                <Columns>

                    <asp:TemplateField HeaderText="Nombre">
                        <ItemTemplate>
                            <span class="role-name"><%# Eval("Nombre") %></span>
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

                        <ItemStyle CssClass="col-name" />
                        <HeaderStyle CssClass="col-name" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Descripción">
                        <ItemTemplate>
                            <span class="role-description" title='<%# Eval("Descripcion") %>'>
                                <%# Eval("Descripcion") != null && Eval("Descripcion").ToString().Length > 80 ? Eval("Descripcion").ToString().Substring(0, 80) + "..." : Eval("Descripcion") %>
                            </span>
                        </ItemTemplate>

                        <EditItemTemplate>
                            <asp:TextBox
                                ID="txtDescripcionEdit"
                                runat="server"
                                CssClass="form-control"
                                MaxLength="255"
                                Text='<%# Bind("Descripcion") %>'>
                            </asp:TextBox>
                        </EditItemTemplate>

                        <ItemStyle CssClass="col-description" />
                        <HeaderStyle CssClass="col-description" />
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
                                    CommandName="AbrirModalEliminar"
                                    CommandArgument='<%# Eval("IdRol") %>' />

                                <asp:Button
                                    ID="btnRestaurar"
                                    runat="server"
                                    Text="Restaurar"
                                    CssClass="btn btn-sm btn-outline-success"
                                    CommandName="Restaurar"
                                    CommandArgument='<%# Eval("IdRol") %>' />
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

    <div class="modal fade modal-top" id="modalEliminarRol" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-header justify-content-center">
                    <h5 class="modal-title">Eliminar rol</h5>
                </div>

                <div class="modal-body text-center">
                    <asp:HiddenField ID="hfIdRolEliminar" runat="server" />

                    <p class="mb-2">
                        ¿Seguro que querés eliminar este rol?
                    </p>
                </div>

                <div class="modal-footer justify-content-center">

                    <asp:Button
                        ID="btnConfirmarEliminar"
                        runat="server"
                        Text="Eliminar"
                        CssClass="btn btn-danger"
                        OnClick="btnConfirmarEliminar_Click" />

                    <button type="button" class="btn btn-secondary" data-dismiss="modal">
                        Cancelar
                    </button>

                </div>

            </div>
        </div>
    </div>

</asp:Content>