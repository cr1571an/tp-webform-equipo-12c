<%@ Page Title="Compras" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Compras.aspx.cs" Inherits="AppGestionNegocio.Web.Compras" %>

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

        .badge-payment {
            padding: 5px 10px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 600;
            background-color: #ede9fe;
            color: #6f42c1;
        }

        .badge-detail {
            padding: 5px 10px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 600;
            background-color: #dcfce7;
            color: #166534;
        }

        .purchase-total {
            font-weight: 700;
            color: #111827;
        }

        .purchase-provider {
            font-weight: 600;
        }

        .purchase-observation {
            color: #6b7280;
            font-size: 13px;
        }

        .form-section-title {
            font-size: 18px;
            font-weight: 700;
            margin-bottom: 16px;
        }

        .empty-state {
            text-align: center;
            color: #6b7280;
            font-weight: 500;
            padding: 28px;
            background-color: #ffffff;
        }

        .col-date {
            width: 10%;
        }

        .col-provider {
            width: 16%;
        }

        .col-user {
            width: 10%;
        }

        .col-payment {
            width: 12%;
        }

        .col-receipt {
            width: 14%;
        }

        .col-items {
            width: 10%;
        }

        .col-total {
            width: 10%;
        }

        .col-observations {
            width: 10%;
        }

        .col-actions {
            width: 8%;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-3">
        <div>
            <h1 class="page-title">Compras</h1>
        </div>

        <div class="page-actions">

            <asp:TextBox
                ID="txtFiltroCompra"
                runat="server"
                CssClass="form-control filtro-input"
                placeholder="Proveedor o comprobante">
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

            <a href="CompraFormulario.aspx"
                class="btn btn-primary">Registrar compra
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
                ID="dgvCompras"
                runat="server"
                CssClass="table table-striped table-hover mb-0"
                AutoGenerateColumns="false"
                GridLines="None"
                DataKeyNames="IdCompra"
                EmptyDataText="No se encontraron compras registradas."
                OnPageIndexChanging="dgvCompras_PageIndexChanging"
                OnRowCommand="dgvCompras_RowCommand"
                AllowPaging="True"
                PageSize="10"
                PagerStyle-CssClass="grid-pager">

                <Columns>

                    <asp:TemplateField HeaderText="Fecha">
                        <ItemTemplate>
                            <%# Eval("FechaCompra", "{0:dd/MM/yyyy}") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Proveedor">
                        <ItemTemplate>
                            <span class="client-name">
                                <%# Eval("Proveedor.Nombre") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Medio de pago">
                        <ItemTemplate>
                            <%# Eval("MedioPago.Descripcion") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Comprobante">
                        <ItemTemplate>
                            <%# Eval("NumeroComprobante") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Total">
                        <ItemTemplate>
                            $ <%# Eval("Total", "{0:N2}") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acciones">

                        <ItemTemplate>

                            <div class="table-actions">

                                <a href='<%# "CompraFormulario.aspx?id=" + Eval("IdCompra") %>'
                                    class="btn btn-sm btn-outline-primary grid-action-btn">Modificar
                                </a>

                                <asp:Button
                                    ID="btnEliminar"
                                    runat="server"
                                    Text="Eliminar"
                                    CssClass="btn btn-sm btn-outline-danger grid-action-btn"
                                    CommandName="AbrirModalEliminar"
                                    CommandArgument='<%# Eval("IdCompra") %>' />

                            </div>

                        </ItemTemplate>

                        <ItemStyle CssClass="col-actions" />
                        <HeaderStyle CssClass="col-actions" />
                    </asp:TemplateField>

                </Columns>

            </asp:GridView>

        </div>

    </div>

    <div class="modal fade modal-top" id="modalEliminarCompra" tabindex="-1">

        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-header justify-content-center">
                    <h5 class="modal-title">Eliminar compra
                    </h5>
                </div>

                <div class="modal-body text-center">

                    <asp:HiddenField
                        ID="hfIdCompraEliminar"
                        runat="server" />

                    <p class="mb-2">
                        ¿Seguro que querés eliminar esta compra?
                    </p>

                </div>

                <div class="modal-footer justify-content-center">

                    <asp:Button
                        ID="btnConfirmarEliminar"
                        runat="server"
                        Text="Eliminar"
                        CssClass="btn btn-danger"
                        OnClick="btnConfirmarEliminar_Click" />

                    <button
                        type="button"
                        class="btn btn-secondary"
                        data-dismiss="modal">
                        Cancelar
                    </button>

                </div>

            </div>
        </div>

    </div>

</asp:Content>