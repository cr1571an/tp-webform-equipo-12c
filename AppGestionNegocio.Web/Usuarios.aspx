<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="AppGestionNegocio.Web.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .page-actions {
            display: flex;
            gap: 8px;
            align-items: center;
        }

        .filtro-rol {
            width: 190px;
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

        .user-name-table {
            font-weight: 600;
        }

        .user-employee {
            color: #6b7280;
            font-size: 13px;
        }

        .badge-role {
            padding: 5px 10px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 600;
            background-color: #ede9fe;
            color: #6f42c1;
        }

        .empty-state {
            text-align: center;
            color: #6b7280;
            font-weight: 500;
            padding: 28px;
            background-color: #ffffff;
        }

        .col-user {
            width: 25%;
        }

        .col-employee {
            width: 35%;
        }

        .col-role {
            width: 20%;
        }

        .col-actions {
            width: 20%;
            text-align: center;
        }

        .grid-action-btn {
            padding: 4px 10px;
            font-size: 13px;
            border-radius: 6px;
        }

        .modal-top .modal-dialog {
            margin-top: 40px;
        }

        .form-section-title {
            font-size: 18px;
            font-weight: 700;
            margin-bottom: 16px;
        }

        .message {
            display: block;
            font-size: 14px;
            font-weight: 600;
            margin-bottom: 12px;
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
            <h1 class="page-title">Usuarios</h1>
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

            <asp:DropDownList
                ID="ddlFiltroRol"
                runat="server"
                CssClass="form-control filtro-rol"
                AutoPostBack="true"
                OnSelectedIndexChanged="ddlFiltroRol_SelectedIndexChanged">
            </asp:DropDownList>

            <a id="lnkNuevoUsuario" runat="server" href="UsuarioFormulario.aspx" class="btn btn-primary">
                Nuevo usuario
            </a>

        </div>
    </div>

    <asp:Label
        ID="lblMensaje"
        runat="server"
        CssClass="message text-danger">
    </asp:Label>

    <div class="dashboard-card">

        <h5 class="form-section-title">
            <asp:Label ID="lblTituloListado" runat="server" Text="Usuarios registrados"></asp:Label>
        </h5>

        <div class="table-responsive">
            <asp:GridView
                ID="dgvUsuarios"
                runat="server"
                CssClass="table table-striped table-hover mb-0"
                AutoGenerateColumns="false"
                GridLines="None"
                AllowPaging="True"
                PageSize="10"
                PagerStyle-CssClass="grid-pager"
                EmptyDataText="No hay usuarios activos registrados."
                OnPageIndexChanging="dgvUsuarios_PageIndexChanging"
                OnRowCommand="dgvUsuarios_RowCommand"
                OnRowDataBound="dgvUsuarios_RowDataBound">

                <Columns>

                    <asp:TemplateField HeaderText="Usuario">
                        <ItemTemplate>
                            <span class="user-name-table"><%# Eval("Nombre") %></span>
                        </ItemTemplate>

                        <ItemStyle CssClass="col-user" />
                        <HeaderStyle CssClass="col-user" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Empleado">
                        <ItemTemplate>
                            <span class="user-employee">
                                <%# Eval("Empleado.Nombre") + " " + Eval("Empleado.Apellido") %>
                            </span>
                        </ItemTemplate>

                        <ItemStyle CssClass="col-employee" />
                        <HeaderStyle CssClass="col-employee" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Rol">
                        <ItemTemplate>
                            <span class="badge-role"><%# Eval("Rol.Nombre") %></span>
                        </ItemTemplate>

                        <ItemStyle CssClass="col-role" />
                        <HeaderStyle CssClass="col-role" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <div class="table-actions">

                                <asp:HyperLink
                                    ID="lnkModificar"
                                    runat="server"
                                    NavigateUrl='<%# "UsuarioFormulario.aspx?id=" + Eval("IdUsuario") %>'
                                    CssClass="btn btn-sm btn-outline-primary grid-action-btn"
                                    Text="Modificar">
                                </asp:HyperLink>

                                <asp:Button
                                    ID="btnEliminar"
                                    runat="server"
                                    Text="Eliminar"
                                    CssClass="btn btn-sm btn-outline-danger grid-action-btn"
                                    CommandName="AbrirModalEliminar"
                                    CommandArgument='<%# Eval("IdUsuario") %>' />

                                <asp:Button
                                    ID="btnRestaurar"
                                    runat="server"
                                    Text="Restaurar"
                                    CssClass="btn btn-sm btn-outline-success grid-action-btn"
                                    CommandName="Restaurar"
                                    CommandArgument='<%# Eval("IdUsuario") %>' />

                            </div>
                        </ItemTemplate>

                        <ItemStyle CssClass="col-actions" />
                        <HeaderStyle CssClass="col-actions" />
                    </asp:TemplateField>

                </Columns>

            </asp:GridView>
        </div>

    </div>

    <div class="modal fade modal-top" id="modalEliminarUsuario" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-header justify-content-center">
                    <h5 class="modal-title">Eliminar usuario</h5>
                </div>

                <div class="modal-body text-center">
                    <asp:HiddenField ID="hfIdUsuarioEliminar" runat="server" />

                    <p class="mb-2">
                        ¿Seguro que querés eliminar este usuario?
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