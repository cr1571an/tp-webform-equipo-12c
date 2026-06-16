<%@ Page Title="Marcas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Marcas.aspx.cs" Inherits="AppGestionNegocio.Web.Marcas" %>

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

        .form-section-title {
            font-size: 18px;
            font-weight: 700;
            margin-bottom: 16px;
        }

        label {
            font-weight: 600;
            font-size: 14px;
            color: #374151;
        }

        .brand-name {
            font-weight: 600;
        }

        .message {
            display: block;
            font-size: 14px;
            font-weight: 600;
            margin-top: 6px;
            margin-bottom: 0;
        }

        .col-name {
            width: 70%;
        }

        .col-actions {
            width: 30%;
            text-align: center;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-3">
        <div>
            <h1 class="page-title">Marcas</h1>
        </div>

        <div class="page-actions">
            <asp:TextBox
                ID="txtFiltroNombre"
                runat="server"
                CssClass="form-control filtro-input"
                placeholder="Buscar marca">
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
        </div>
    </div>

    <div class="dashboard-card mb-3">
        <h5 class="form-section-title">Nueva marca</h5>

        <div class="row">
            <div class="col-md-10 mb-3">
                <label>Nombre</label>

                <asp:TextBox
                    ID="txtNombre"
                    runat="server"
                    CssClass="form-control"
                    placeholder="Ej: Royal Canin">
                </asp:TextBox>

                <asp:Label
                    ID="lblMensaje"
                    runat="server"
                    CssClass="message text-danger">
                </asp:Label>
            </div>

            <div class="col-md-2 mb-3 d-flex align-items-end">
                <asp:Button
                    ID="btnAgregar"
                    runat="server"
                    Text="Agregar"
                    CssClass="btn btn-primary w-100"
                    OnClick="btnAgregar_Click" />
            </div>
        </div>
    </div>

    <div class="dashboard-card">
        <h5 class="form-section-title">Marcas registradas</h5>

        <div class="table-responsive">
            <asp:GridView
                ID="dgvMarcas"
                runat="server"
                CssClass="table table-striped table-hover mb-0"
                AutoGenerateColumns="false"
                GridLines="None"
                DataKeyNames="IdMarca"
                EmptyDataText="No hay marcas activas registradas."
                OnRowEditing="dgvMarcas_RowEditing"
                OnRowCancelingEdit="dgvMarcas_RowCancelingEdit"
                OnRowUpdating="dgvMarcas_RowUpdating"
                OnRowCommand="dgvMarcas_RowCommand">

                <Columns>

                    <asp:TemplateField HeaderText="Nombre">
                        <ItemTemplate>
                            <span class="brand-name"><%# Eval("Nombre") %></span>
                        </ItemTemplate>

                        <EditItemTemplate>
                            <asp:TextBox
                                ID="txtNombreEdit"
                                runat="server"
                                CssClass="form-control"
                                Text='<%# Bind("Nombre") %>'>
                            </asp:TextBox>
                        </EditItemTemplate>

                        <ItemStyle CssClass="col-name" />
                        <HeaderStyle CssClass="col-name" />
                    </asp:TemplateField>

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
                                    CommandName="EliminarMarca"
                                    CommandArgument='<%# Eval("IdMarca") %>'
                                    OnClientClick="return confirm('¿Seguro que querés eliminar esta marca?');" />
                            </div>
                        </ItemTemplate>

                        <EditItemTemplate>
                            <div class="table-actions">
                                <asp:Button
                                    ID="btnGuardarEdit"
                                    runat="server"
                                    Text="Guardar"
                                    CssClass="btn btn-sm btn-success"
                                    CommandName="Update" />

                                <asp:Button
                                    ID="btnCancelarEdit"
                                    runat="server"
                                    Text="Cancelar"
                                    CssClass="btn btn-sm btn-secondary"
                                    CommandName="Cancel" />
                            </div>
                        </EditItemTemplate>

                        <ItemStyle CssClass="col-actions" />
                        <HeaderStyle CssClass="col-actions" />
                    </asp:TemplateField>

                </Columns>

            </asp:GridView>
        </div>
    </div>

</asp:Content>