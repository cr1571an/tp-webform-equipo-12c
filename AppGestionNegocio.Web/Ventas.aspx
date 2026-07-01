<%@ Page Title="Ventas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="AppGestionNegocio.Web.Ventas" %>

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

        .sale-client { 
            font-weight: 600; 
        }

        .badge-payment { 
            padding: 5px 10px;
            border-radius: 20px; 
            font-size: 12px; 
            font-weight: 600; 
            background-color: #ede9fe; 
            color: #6f42c1;
        }

        .sale-total { 
            font-weight: 700;
            color: #111827; 
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
            <h1 class="page-title">Ventas</h1>
        </div>
    
        <div class="page-actions">
                <asp:DropDownList ID="ddlFiltro" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFiltro_SelectedIndexChanged">
                    <asp:ListItem Text="Más recientes" Value="MasRecientes"></asp:ListItem>
                    <asp:ListItem Text="Más antiguas" Value="MasAntiguas"></asp:ListItem>
                </asp:DropDownList>

            <asp:Button ID="btnNuevaVenta" runat="server" Text="Registrar venta" CssClass="btn btn-primary" OnClick="btnNuevaVenta_Click"/>
        </div>
    </div>

    <div class="dashboard-card">
    
        <div class="table-responsive">
            <asp:GridView ID="dgvVentas" runat="server" CssClass="table table-striped table-hover mb-0" AutoGenerateColumns="false" GridLines="None" AllowPaging="True" PageSize="10" PagerStyle-CssClass="grid-pager" OnPageIndexChanging="dgvVentas_PageIndexChanging" OnRowCommand="dgvVentas_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Fecha">
                        <ItemTemplate>
                            <%# Eval("Fecha", "{0:dd/MM/yyyy}") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Cliente">
                        <ItemTemplate>
                            <span class="sale-client"><%# Eval("Cliente.Nombre") %></span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Medio de pago">
                        <ItemTemplate>
                            <span class="badge-payment"><%# Eval("MedioPago.Nombre") %></span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Factura">
                        <ItemTemplate>
                            <%# Eval("NumeroFactura") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Total">
                        <ItemTemplate>
                            <span class="sale-total"><%# Eval("Total", "{0:C}") %></span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <div class="table-actions">
                                <a href='VentaFormulario.aspx?id=<%# Eval("IdVenta") %>' class="btn btn-sm btn-outline-primary grid-action-btn">Ver Detalle</a>

                                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-sm btn-outline-danger grid-action-btn" CommandName="AbrirModalEliminar" CommandArgument='<%# Eval("IdVenta") %>' />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

    </div>

    <div class="modal fade modal-top" id="modalEliminarVenta" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-header justify-content-center">
                    <h5 class="modal-title">Eliminar venta</h5>
                </div>

                <div class="modal-body text-center">
                    <asp:HiddenField ID="hfIdVentaEliminar" runat="server" />

                    <p class="mb-2">
                        ¿Seguro que querés eliminar esta venta?
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