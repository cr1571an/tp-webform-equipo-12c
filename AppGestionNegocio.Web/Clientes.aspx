<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="AppGestionNegocio.Web.Clientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .page-actions {
            display: flex;
            gap: 8px;
            align-items: center;
        }

        .filtro-input {
            width: 240px;
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

        .table-actions {
            display: flex;
            gap: 6px;
            justify-content: center;
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

        .message {
            display: block;
            font-size: 14px;
            font-weight: 600;
            margin-bottom: 12px;
        }

        .col-actions {
            text-align: center;
        }

        .modal-top .modal-dialog {
            margin-top: 40px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-3">
        <div>
            <h1 class="page-title">Clientes</h1>
        </div>

        <div class="page-actions">

            <asp:TextBox
                ID="txtFiltroCliente"
                runat="server"
                CssClass="form-control filtro-input"
                placeholder="Nombre, apellido o CUIT">
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

            <a href="ClienteFormulario.aspx" class="btn btn-primary">
                Nuevo cliente
            </a>

        </div>
    </div>

    <asp:Label
        ID="lblMensaje"
        runat="server"
        CssClass="message">
    </asp:Label>

    <div class="dashboard-card">

        <div class="table-responsive">

            <asp:GridView
                ID="dgvClientes"
                runat="server"
                CssClass="table table-striped table-hover mb-0"
                AutoGenerateColumns="false"
                GridLines="None"
                DataKeyNames="IdCliente"
                EmptyDataText="No se encontraron clientes registrados."
                OnPageIndexChanging="dgvClientes_PageIndexChanging"
                OnRowCommand="dgvClientes_RowCommand"
                AllowPaging="True"
                PageSize="10"
                PagerStyle-CssClass="grid-pager">

                <Columns>

                    <asp:TemplateField HeaderText="Cliente">
                        <ItemTemplate>
                            <span class="client-name">
                                <%# Eval("Nombre") %> <%# Eval("Apellido") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="CUIT">
                        <ItemTemplate>
                            <%# !string.IsNullOrWhiteSpace(Convert.ToString(Eval("Cuit"))) ? Eval("Cuit") : "—" %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Teléfono">
                        <ItemTemplate>
                            <%# !string.IsNullOrWhiteSpace(Convert.ToString(Eval("Telefono"))) ? Eval("Telefono") : "—" %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <span class="client-contact">
                                <%# !string.IsNullOrWhiteSpace(Convert.ToString(Eval("Email"))) ? Eval("Email") : "—" %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Condición IVA">
                        <ItemTemplate>
                            <%# Eval("CondicionIva") != null ? Eval("CondicionIva.Condicion") : "—" %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <div class="table-actions">

                                <a href='<%# "ClienteFormulario.aspx?id=" + Eval("IdCliente") %>'
                                   class="btn btn-sm btn-outline-primary grid-action-btn">
                                    Modificar
                                </a>

                                <asp:Button
                                    ID="btnEliminar"
                                    runat="server"
                                    Text="Eliminar"
                                    CssClass="btn btn-sm btn-outline-danger grid-action-btn"
                                    CommandName="AbrirModalEliminar"
                                    CommandArgument='<%# Eval("IdCliente") %>' />

                            </div>
                        </ItemTemplate>

                        <ItemStyle CssClass="col-actions" />
                        <HeaderStyle CssClass="col-actions" />
                    </asp:TemplateField>

                </Columns>

            </asp:GridView>

        </div>

    </div>

    <div class="modal fade modal-top" id="modalEliminarCliente" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-header justify-content-center">
                    <h5 class="modal-title">Eliminar cliente</h5>
                </div>

                <div class="modal-body text-center">

                    <asp:HiddenField ID="hfIdClienteEliminar" runat="server" />

                    <p class="mb-2">
                        ¿Seguro que querés eliminar este cliente?
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