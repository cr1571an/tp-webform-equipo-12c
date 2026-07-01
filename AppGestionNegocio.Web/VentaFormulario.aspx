<%@ Page Title="Registrar Venta" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VentaFormulario.aspx.cs" Inherits="AppGestionNegocio.Web.VentaFormulario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <style>
    .form-section-title {
        font-size: 18px;
        font-weight: 700;
        margin-bottom: 16px;
    }

    .subsection-title {
        font-size: 16px;
        margin-top: 8px;
        margin-bottom: 14px;
        color: #374151;
    }

    label {
        font-weight: 600;
        font-size: 14px;
        color: #374151;
    }

    .table td,
    .table th {
        vertical-align: middle;
        white-space: nowrap;
    }

    .form-actions {
        display: flex;
        justify-content: flex-end;
        gap: 8px;
        border-top: 1px solid #e5e7eb;
        padding-top: 18px;
        margin-top: 10px;
    }

    .purchase-total-box {
        background-color: #ede9fe;
        color: #4c1d95;
        border-radius: 10px;
        padding: 12px 16px;
        font-weight: 700;
        text-align: right;
    }</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div class="d-flex justify-content-between align-items-center mb-3">
        <h1 class="page-title">Registrar Venta</h1>
        <a href="Ventas.aspx" class="btn btn-outline-secondary">Volver al listado</a>
    </div>

    <asp:UpdatePanel ID="upDetalleVenta" runat="server">
        <ContentTemplate>

            <div class="dashboard-card mb-3">
                <h5 class="form-section-title">Datos de la Venta</h5>
                <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger d-block mb-2"></asp:Label>

                <div class="row">
                    <div class="col-md-3 mb-3">
                        <label>Cliente</label>
                        <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-3 mb-3">
                        <label>Medio de Pago</label>
                        <asp:DropDownList ID="ddlMedio" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-3 mb-3">
                        <label>Fecha de Venta</label>
                        <asp:TextBox ID="txtFechaVenta" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3 mb-3">
                        <label>Número de Comprobante</label>
                        <asp:TextBox ID="txtNumeroComprobante" runat="server" CssClass="form-control" placeholder="Ej: FC-0001-00000123"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="dashboard-card mb-3">
                <h5 class="subsection-title">Detalle de Venta</h5>
                <asp:Label ID="lblMensajeDetalle" runat="server" CssClass="text-danger d-block mb-2"></asp:Label>
                
                <div class="row">
                    <div class="col-md-4 mb-3">
                        <label>Categoría</label>
                        <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged"></asp:DropDownList>
                    </div>

                    <div class="col-md-4 mb-3">
                        <label>Artículo</label>
                        <asp:DropDownList ID="ddlArticulo" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlArticulo_SelectedIndexChanged"></asp:DropDownList>
                    </div>

                    <div class="col-md-4 mb-3">
                        <label>Stock Disponible</label>
                        <asp:TextBox ID="txtStockDisponible" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-4 mb-3">
                        <label>Cantidad</label>
                        <asp:TextBox ID="txtCantidad" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-4 mb-3">
                        <label>Precio Unitario</label>
                        <asp:TextBox ID="txtPrecioUnitario" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>

                    <div class="col-md-4 mb-3 d-flex align-items-end">
                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-outline-primary w-100" OnClick="btnAgregar_Click" />
                    </div>
                </div>

                <div class="table-responsive mt-3">
                    <asp:GridView ID="gvDetalle" runat="server" CssClass="table table-sm table-striped table-hover mb-0" AutoGenerateColumns="false" DataKeyNames="PrecioUnitario" EmptyDataText="No hay artículos agregados." GridLines="None" OnRowEditing="gvDetalle_RowEditing" OnRowCancelingEdit="gvDetalle_RowCancelingEdit" OnRowUpdating="gvDetalle_RowUpdating" OnRowCommand="gvDetalle_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Artículo">
                                <ItemTemplate><%# Eval("Articulo.Nombre") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cantidad">
                                <ItemTemplate><%# Eval("Cantidad") %></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCantidadEdit" runat="server" CssClass="form-control" Text='<%# Eval("Cantidad") %>' TextMode="Number"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Precio unitario">
                                <ItemTemplate><%# Eval("PrecioUnitario", "{0:N2}") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Subtotal">
                                <ItemTemplate><%# Eval("Subtotal", "{0:N2}") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:Button ID="btnEditar" runat="server" Text="Modificar" CssClass="btn btn-sm btn-outline-primary" CommandName="Edit"/>
                                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-sm btn-outline-danger" CommandName="AbrirModalEliminar" CommandArgument='<%# Eval("Articulo.IdArticulo") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-sm btn-success" CommandName="Update" CommandArgument='<%# Eval("Articulo.IdArticulo") %>' />
                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-sm btn-secondary" CommandName="Cancel" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

                <div class="row mt-3">
                    <div class="col-md-8 mb-3">
                        <label>Observaciones</label>
                        <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control" placeholder="Observaciones generales de la venta"></asp:TextBox>
                    </div>
                    <div class="col-md-4 mb-3">
                        <label>Total</label>
                        <div class="purchase-total-box">
                            Total Venta: <asp:Label ID="lblTotal" runat="server" Text="$ 0,00"></asp:Label>
                        </div>
                    </div>
                </div>

                <div class="modal fade" id="modalEliminarArticulo" tabindex="-1">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header justify-content-center"><h5 class="modal-title">Eliminar artículo</h5></div>
                            <div class="modal-body text-center">
                                <asp:HiddenField ID="hfIdArticuloEliminar" runat="server" />
                                <p>¿Seguro que querés eliminar este artículo del detalle?</p>
                            </div>
                            <div class="modal-footer justify-content-center">
                                <asp:Button ID="btnConfirmarEliminarArticulo" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnConfirmarEliminarArticulo_Click" OnClientClick="$('#modalEliminarArticulo').modal('hide'); $('body').removeClass('modal-open'); $('.modal-backdrop').remove();" />
                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="form-actions">
        <asp:Button ID="btnCancelarVenta" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary" OnClick="btnCancelarVenta_Click"/>
        <asp:Button ID="btnGuardarVenta" runat="server" Text="Guardar Venta" CssClass="btn btn-primary" OnClick="btnGuardarVenta_Click"/>
    </div>
</asp:Content>