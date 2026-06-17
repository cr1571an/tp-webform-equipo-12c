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

        .badge-active {
            padding: 5px 10px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 600;
            background-color: #dcfce7;
            color: #166534;
        }

        .badge-inactive {
            padding: 5px 10px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 600;
            background-color: #fee2e2;
            color: #991b1b;
        }

        .empty-state {
            text-align: center;
            color: #6b7280;
            font-weight: 500;
            padding: 28px;
            background-color: #ffffff;
        }

        .col-user {
            width: 18%;
        }

        .col-employee {
            width: 24%;
        }

        .col-role {
            width: 16%;
        }

        .col-status {
            width: 12%;
        }

        .col-security {
            width: 14%;
        }

        .col-actions {
            width: 16%;
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

            <a href="UsuarioFormulario.aspx" class="btn btn-primary">Nuevo usuario
            </a>
        </div>
    </div>

    <div class="dashboard-card">

        <h5 class="form-section-title">Usuarios registrados</h5>

    </div>
    <div class="dashboard-card">

        <div class="table-responsive">
            <asp:GridView ID="dgvUsuarios" runat="server" CssClass="table table-striped table-hover mb-0" AutoGenerateColumns="false" GridLines="None" AllowPaging="True" PageSize="10" PagerStyle-CssClass="grid-pager" OnPageIndexChanging="dgvUsuarios_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="Usuario">
                        <ItemTemplate>
                            <div class="article-cell">
                                <span class="article-name"><%# Eval("Nombre") %></span>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Empleado">
                        <ItemTemplate>
                            <span class="badge-category"><%# Eval("Empleado.Nombre") + " " + Eval("Empleado.Apellido") %></span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rol">
                        <ItemTemplate>
                            <%# Eval("Rol.Nombre") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <a href='UsuarioFormulario.aspx?id=<%# Eval("IdUsuario") %>' class="btn btn-sm btn-outline-primary grid-action-btn">Editar</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

    </div>

</asp:Content>