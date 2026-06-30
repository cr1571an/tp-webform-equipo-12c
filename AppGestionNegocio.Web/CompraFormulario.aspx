<%@ Page Title="Registrar Compra" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompraFormulario.aspx.cs" Inherits="AppGestionNegocio.Web.CompraFormulario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-section-title {
            font-size: 18px;
            font-weight: 700;
            margin-bottom: 16px;
        }

        .subsection-title {
            font-size: 16px;
            font-weight: 700;
            margin-top: 8px;
            margin-bottom: 14px;
            color: #374151;
        }

        label {
            font-weight: 600;
            font-size: 14px;
            color: #374151;
        }

        .detail-box {
            background-color: #f9fafb;
            border: 1px solid #e5e7eb;
            border-radius: 10px;
            padding: 16px;
            margin-top: 8px;
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
        }

        .empty-state {
            text-align: center;
            color: #6b7280;
            font-weight: 500;
            padding: 22px;
            background-color: #ffffff;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-3">
        <div>
            <h1 class="page-title">Registrar compra</h1>
        </div>

        <a href="Compras.aspx" class="btn btn-outline-secondary">Volver al listado
        </a>
    </div>

    <div class="dashboard-card mb-3">
        <h5 class="form-section-title">Datos de la compra</h5>

        <div class="row">
            <div class="col-md-3 mb-3">
                <label>Proveedor</label>
                <asp:DropDownList
                    ID="ddlProveedor"
                    runat="server"
                    CssClass="form-control" OnSelectedIndexChanged="ddlProveedor_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
            </div>

            <div class="col-md-3 mb-3">
                <label>Medio de Pago</label>
                <asp:DropDownList
                    ID="ddlMedio"
                    runat="server"
                    CssClass="form-control">
                </asp:DropDownList>
            </div>

            <div class="col-md-3 mb-3">
                <label>Fecha de compra</label>
                <input type="date" class="form-control" />
            </div>

            <div class="col-md-3 mb-3">
                <label>Número de comprobante</label>
                <input type="text" class="form-control" placeholder="Ej: FC-0001-00000123" />
            </div>
        </div>
    </div>

    <div class="dashboard-card mb-3">

        <h5 class="subsection-title">Detalle de compra</h5>

        <div class="row">

            <div class="col-md-4 mb-3">
                <label>Artículo</label>
                <asp:DropDownList
                    ID="ddlArticulo"
                    runat="server"
                    CssClass="form-control"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="ddlArticulo_SelectedIndexChanged">
                </asp:DropDownList>
            </div>

            <div class="col-md-2 mb-3">
                <label>Cantidad</label>
                <asp:TextBox
                    ID="txtCantidad"
                    runat="server"
                    TextMode="Number"
                    CssClass="form-control"
                    onkeyup="calcularSubtotal()">
                </asp:TextBox>
            </div>

            <div class="col-md-2 mb-3">
                <label>Precio unitario</label>
                <asp:TextBox
                    ID="txtPrecioUnitario"
                    runat="server"
                    CssClass="form-control"
                    ReadOnly="true">
                </asp:TextBox>
            </div>

            <div class="col-md-2 mb-3">
                <label>Subtotal</label>
                <asp:TextBox
                    ID="txtSubtotal"
                    runat="server"
                    CssClass="form-control"
                    ReadOnly="true">
                </asp:TextBox>
            </div>

            <div class="col-md-2 mb-3 d-flex align-items-end">
                <asp:Button
                    ID="btnAgregar"
                    runat="server"
                    Text="Agregar"
                    CssClass="btn btn-outline-primary w-100"
                    OnClick="btnAgregar_Click" />
            </div>

        </div>

        <div class="row">
            <div class="col-12">
                <asp:Label
                    ID="lblMensaje"
                    runat="server"
                    Visible="false">
                </asp:Label>
            </div>
        </div>


        <div class="dashboard-card">
            <div class="table-responsive mt-2">

                <asp:GridView
                    ID="gvDetalle"
                    runat="server"
                    CssClass="table table-sm table-striped table-hover mb-0"
                    AutoGenerateColumns="false"
                    GridLines="None"
                    DataKeyNames="IdArticulo"
                    EmptyDataText="No hay artículos agregados."
                    OnRowEditing="gvDetalle_RowEditing"
                    OnRowCancelingEdit="gvDetalle_RowCancelingEdit"
                    OnRowUpdating="gvDetalle_RowUpdating"
                    OnRowCommand="gvDetalle_RowCommand">

                    <Columns>

                        <asp:BoundField
                            DataField="NombreArticulo"
                            HeaderText="Artículo"
                            ReadOnly="true" />

                        <asp:TemplateField HeaderText="Cantidad">

                            <ItemTemplate>
                                <%# Eval("Cantidad") %>
                            </ItemTemplate>

                            <EditItemTemplate>
                                <asp:TextBox
                                    ID="txtCantidadEdit"
                                    runat="server"
                                    CssClass="form-control"
                                    Text='<%# Bind("Cantidad") %>'
                                    TextMode="Number">
                                </asp:TextBox>
                            </EditItemTemplate>

                        </asp:TemplateField>

                        <asp:BoundField
                            DataField="PrecioUnitario"
                            HeaderText="Precio unitario"
                            ReadOnly="true"
                            DataFormatString="{0:N2}" />

                        <asp:BoundField
                            DataField="Subtotal"
                            HeaderText="Subtotal"
                            ReadOnly="true"
                            DataFormatString="{0:N2}" />

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
                                        CommandArgument='<%# Eval("IdArticulo") %>' />

                                </div>

                            </ItemTemplate>

                            <EditItemTemplate>

                                <div class="table-actions">

                                    <asp:Button
                                        ID="btnGuardar"
                                        runat="server"
                                        Text="Guardar"
                                        CssClass="btn btn-sm btn-success"
                                        CommandName="Update" />

                                    <asp:Button
                                        ID="btnCancelar"
                                        runat="server"
                                        Text="Cancelar"
                                        CssClass="btn btn-sm btn-secondary"
                                        CommandName="Cancel" />

                                </div>

                            </EditItemTemplate>

                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>

            </div>
        </div>

        <div class="row mt-3">
            <div class="col-md-8 mb-3">
                <label>Observaciones</label>
                <asp:TextBox
                    ID="txtObservaciones"
                    runat="server"
                    CssClass="form-control"
                    placeholder="Observaciones generales de la compra">
                </asp:TextBox>
            </div>

            <div class="col-md-4 mb-3">
                <label>Total</label>
                <div class="purchase-total-box">
                    Total estimado:
            <asp:Label
                ID="lblTotal"
                runat="server"
                Text="$ 0,00">
            </asp:Label>
                </div>
            </div>
        </div>

        <div class="form-actions">
            <a href="Compras.aspx" class="btn btn-outline-secondary">Cancelar
            </a>

            <button type="button" class="btn btn-primary">
                Guardar compra
            </button>
        </div>
    </div>

    <div class="modal fade modal-top"
        id="modalEliminarArticulo"
        tabindex="-1">

        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-header justify-content-center">
                    <h5 class="modal-title">Eliminar artículo
                    </h5>
                </div>

                <div class="modal-body text-center">

                    <asp:HiddenField
                        ID="hfIdArticuloEliminar"
                        runat="server" />

                    <p class="mb-2">
                        ¿Seguro que querés eliminar este artículo del detalle?
                    </p>

                </div>

                <div class="modal-footer justify-content-center">

                    <asp:Button
                        ID="btnConfirmarEliminarArticulo"
                        runat="server"
                        Text="Eliminar"
                        CssClass="btn btn-danger"
                        OnClick="btnConfirmarEliminarArticulo_Click" />

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


    <script>
        function calcularSubtotal() {

            let cantidad =
                parseFloat(document.getElementById('<%= txtCantidad.ClientID %>').value) || 0;

            let precio =
                parseFloat(document.getElementById('<%= txtPrecioUnitario.ClientID %>').value) || 0;

            let subtotal = cantidad * precio;

            document.getElementById('<%= txtSubtotal.ClientID %>').value =
                subtotal.toFixed(2);
        }
    </script>


</asp:Content>
