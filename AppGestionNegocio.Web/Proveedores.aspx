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
            flex-wrap: nowrap;
            justify-content: center;
            align-items: center;
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
            width: 150px;
            min-width: 150px;
        }

        .modal-top .modal-dialog {
            margin-top: 40px;
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
            <h1 class="page-title">Proveedores</h1>
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

            <a id="lnkNuevoProveedor" runat="server" href="ProveedorFormulario.aspx" class="btn btn-primary">
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
        <h5 class="form-section-title">
            <asp:Label ID="lblTituloListado" runat="server" Text="Proveedores registrados"></asp:Label>
        </h5>

        <div class="table-responsive">

            <asp:GridView
                ID="dgvProveedores"
                runat="server"
                CssClass="table table-striped table-hover mb-0"
                AutoGenerateColumns="false"
                GridLines="None"
                DataKeyNames="IdProveedor"
                EmptyDataText="No se encontraron proveedores registrados."
                OnRowCommand="dgvProveedores_RowCommand"
                OnRowDataBound="dgvProveedores_RowDataBound">

                <Columns>

                    <asp:TemplateField HeaderText="Proveedor">
                        <ItemTemplate>
                            <span class="provider-name">
                                <%# Eval("Nombre") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField HeaderText="CUIT" DataField="Cuit" />

                    <asp:BoundField HeaderText="Teléfono" DataField="Telefono" />

                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <span class="provider-contact">
                                <%# !string.IsNullOrWhiteSpace(Convert.ToString(Eval("Email"))) ? Eval("Email") : "—" %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField HeaderText="Domicilio" DataField="Domicilio" />

                    <asp:TemplateField HeaderText="CP">
                        <ItemTemplate>
                            <%# !string.IsNullOrWhiteSpace(Convert.ToString(Eval("Cp"))) ? Eval("Cp") : "—" %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Contacto">
                        <ItemTemplate>
                            <%# !string.IsNullOrWhiteSpace(Convert.ToString(Eval("PersonaContacto"))) ? Eval("PersonaContacto") : "—" %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Observaciones">
                        <ItemTemplate>
                            <span class="provider-observations" title='<%# Eval("Observaciones") %>'>
                                <%#
                                    !string.IsNullOrWhiteSpace(Convert.ToString(Eval("Observaciones")))
                                    ? (
                                        Convert.ToString(Eval("Observaciones")).Length > 80
                                        ? Convert.ToString(Eval("Observaciones")).Substring(0, 80) + "..."
                                        : Eval("Observaciones")
                                      )
                                    : "—"
                                %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <div class="table-actions">

                                <asp:HyperLink
                                    ID="lnkModificar"
                                    runat="server"
                                    NavigateUrl='<%# "ProveedorFormulario.aspx?id=" + Eval("IdProveedor") %>'
                                    CssClass="btn btn-sm btn-outline-primary grid-action-btn"
                                    Text="Modificar">
                                </asp:HyperLink>

                                <asp:Button
                                    ID="btnEliminar"
                                    runat="server"
                                    Text="Eliminar"
                                    CssClass="btn btn-sm btn-outline-danger grid-action-btn"
                                    CommandName="AbrirModalEliminar"
                                    CommandArgument='<%# Eval("IdProveedor") %>' />

                                <asp:Button
                                    ID="btnRestaurar"
                                    runat="server"
                                    Text="Restaurar"
                                    CssClass="btn btn-sm btn-outline-success grid-action-btn"
                                    CommandName="Restaurar"
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