<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="AppGestionNegocio.Web.Clientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .page-actions {
            display: flex;
            gap: 8px;
        }

        .table td,
        .table th {
            vertical-align: middle;
            white-space: nowrap;
        }

        .client-name {
            font-weight: 600;
        }

        .client-contact {
            color: #6b7280;
            font-size: 13px;
        }

        .badge-status {
            padding: 5px 10px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 600;
            background-color: #dcfce7;
            color: #166534;
        }

        .grid-action-btn {
            padding: 4px 10px;
            font-size: 13px;
            border-radius: 6px;
        }

        .grid-pager table {
            margin: 12px auto 0 auto;
        }

        .grid-pager a,
        .grid-pager span {
            padding: 6px 10px;
            margin: 0 2px;
            border-radius: 6px;
            border: 1px solid #e5e7eb;
            text-decoration: none;
        }

        .grid-pager span {
            background-color: #6f42c1;
            color: white;
            border-color: #6f42c1;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-3">
        <div>
            <h1 class="page-title">Clientes</h1>
        </div>

        <div class="page-actions">
            <button type="button" class="btn btn-outline-secondary">
                Filtrar
            </button>

            <a href="ClienteFormulario.aspx" class="btn btn-primary">
                Nuevo cliente
            </a>
        </div>
    </div>

    <div class="dashboard-card">

        <div class="table-responsive">

            <asp:GridView ID="dgvClientes" runat="server"
                CssClass="table table-striped table-hover mb-0"
                AutoGenerateColumns="false"
                GridLines="None"
                EmptyDataText="No se encontraron clientes registrados."
                OnPageIndexChanging="dgvClientes_PageIndexChanging"
                AllowPaging="True"
                PageSize="10"
                PagerStyle-CssClass="grid-pager">

                <Columns>

                    <asp:BoundField HeaderText="CUIT" DataField="Cuit" />

                    <asp:TemplateField HeaderText="Cliente">
                        <ItemTemplate>
                            <span class="client-name">
                                <%# Eval("Nombre") %> <%# Eval("Apellido") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField HeaderText="Teléfono" DataField="Telefono" />

                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <span class="client-contact">
                                <%# Eval("Email") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Condición IVA">
                        <ItemTemplate>
                            <%# Eval("CondicionIva.Condicion") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <span class="badge-status">Activo</span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField 
                        HeaderText="Acciones"
                        ShowSelectButton="true"
                        SelectText="Ver"
                        ControlStyle-CssClass="btn btn-sm btn-outline-primary grid-action-btn" />

                </Columns>

            </asp:GridView>

        </div>

    </div>

</asp:Content>