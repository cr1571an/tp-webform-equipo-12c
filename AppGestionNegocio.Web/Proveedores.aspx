<%@ Page Title="Proveedores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Proveedores.aspx.cs" Inherits="AppGestionNegocio.Web.Proveedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .page-actions {
            display: flex;
            gap: 8px;
            align-items: center;
        }

        .filtro-input {
            width: 220px;
        }

        .filtro-cp {
            width: 110px;
        }

        .table td,
        .table th {
            vertical-align: middle;
            white-space: nowrap;
        }

        .provider-name {
            font-weight: 600;
        }

        .provider-contact {
            color: #6b7280;
            font-size: 13px;
        }

        .provider-observations {
            color: #374151;
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
            <h1 class="page-title">Proveedores</h1>
        </div>

        <div class="page-actions">
            <asp:TextBox
                ID="txtFiltroNombre"
                runat="server"
                CssClass="form-control filtro-input"
                placeholder="Nombre o CUIT">
            </asp:TextBox>

            <asp:TextBox
                ID="txtFiltroCp"
                runat="server"
                CssClass="form-control filtro-cp"
                MaxLength="6"
                placeholder="CP">
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

            <a href="ProveedorFormulario.aspx" class="btn btn-primary">
                Nuevo proveedor
            </a>
        </div>
    </div>

    <asp:Label
        ID="lblMensaje"
        runat="server"
        CssClass="message text-danger">
    </asp:Label>

    <div class="dashboard-card">

        <div class="table-responsive">

            <asp:GridView
                ID="dgvProveedores"
                runat="server"
                CssClass="table table-striped table-hover mb-0"
                AutoGenerateColumns="false"
                GridLines="None"
                DataKeyNames="IdProveedor"
                EmptyDataText="No se encontraron proveedores registrados."
                OnRowCommand="dgvProveedores_RowCommand">

                <Columns>

                    <asp:TemplateField HeaderText="Proveedor">
                        <ItemTemplate>
                            <span class="provider-name"><%# Eval("Nombre") %></span>
                            <br />
                            <span class="provider-contact"><%# Eval("Email") %></span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField HeaderText="CUIT" DataField="Cuit" />

                    <asp:BoundField HeaderText="Teléfono" DataField="Telefono" />

                    <asp:BoundField HeaderText="Domicilio" DataField="Domicilio" />

                    <asp:BoundField HeaderText="CP" DataField="Cp" />

                    <asp:BoundField HeaderText="Contacto" DataField="PersonaContacto" />

                    <asp:TemplateField HeaderText="Observaciones">
                        <ItemTemplate>
                            <span class="provider-observations" title='<%# Eval("Observaciones") %>'>
                                <%# Eval("Observaciones") != null && Eval("Observaciones").ToString().Length > 45 ? Eval("Observaciones").ToString().Substring(0, 45) + "..." : Eval("Observaciones") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <div class="table-actions">

                                <a href='<%# "ProveedorFormulario.aspx?id=" + Eval("IdProveedor") %>'
                                   class="btn btn-sm btn-outline-primary grid-action-btn">
                                    Modificar
                                </a>

                                <asp:Button
                                    ID="btnEliminar"
                                    runat="server"
                                    Text="Eliminar"
                                    CssClass="btn btn-sm btn-outline-danger grid-action-btn"
                                    CommandName="AbrirModalEliminar"
                                    CommandArgument='<%# Eval("IdProveedor") %>' />

                            </div>
                        </ItemTemplate>

                        <ItemStyle CssClass="col-actions" />
                        <HeaderStyle CssClass="col-actions" />
                    </asp:TemplateField>

                </Columns>

            </asp:GridView>

        </div>

    </div>

    <div class="modal fade modal-top" id="modalEliminarProveedor" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-header justify-content-center">
                    <h5 class="modal-title">Eliminar proveedor</h5>
                </div>

                <div class="modal-body text-center">
                    <asp:HiddenField ID="hfIdProveedorEliminar" runat="server" />

                    <p class="mb-2">
                        ¿Seguro que querés eliminar este proveedor?
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