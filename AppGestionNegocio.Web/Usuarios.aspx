<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="AppGestionNegocio.Web.Usuarios" %>

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
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-3">
        <div>
            <h1 class="page-title">Usuarios</h1>
        </div>

        <div class="page-actions">
            <button type="button" class="btn btn-outline-secondary">
                Filtrar
            </button>

            <a href="UsuarioFormulario.aspx" class="btn btn-primary">
                Nuevo usuario
            </a>
        </div>
    </div>

    <div class="dashboard-card">

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
                OnPageIndexChanging="dgvUsuarios_PageIndexChanging"
                OnRowCommand="dgvUsuarios_RowCommand">

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

                                <a href='UsuarioFormulario.aspx?id=<%# Eval("IdUsuario") %>'
                                   class="btn btn-sm btn-outline-primary grid-action-btn">
                                    Modificar
                                </a>

                                <asp:Button
                                    ID="btnEliminar"
                                    runat="server"
                                    Text="Eliminar"
                                    CssClass="btn btn-sm btn-outline-danger grid-action-btn"
                                    CommandName="AbrirModalEliminar"
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