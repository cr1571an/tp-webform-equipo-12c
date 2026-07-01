<%@ Page Title="Artículos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Articulos.aspx.cs" Inherits="AppGestionNegocio.Web.Articulos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .page-actions {
            display: flex;
            gap: 8px;
            align-items: center;
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

        .article-cell {
            display: flex;
            align-items: center;
            gap: 12px;
        }

        .article-thumb {
            width: 46px;
            height: 46px;
            object-fit: cover;
            border-radius: 10px;
            border: 1px solid #e5e7eb;
            background-color: #f9fafb;
        }

        .article-name {
            font-weight: 600;
        }

        .badge-stock {
            padding: 5px 10px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 600;
        }

        .stock-ok {
            background-color: #dcfce7;
            color: #166534;
        }

        .stock-low {
            background-color: #fee2e2;
            color: #991b1b;
        }

        .badge-category {
            padding: 5px 10px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 600;
            background-color: #ede9fe;
            color: #6f42c1;
        }

        .price-text {
            font-weight: 700;
            color: #111827;
        }

        .empty-state {
            text-align: center;
            color: #6b7280;
            font-weight: 500;
            padding: 28px;
            background-color: #ffffff;
        }

        .grid-action-btn {
            padding: 4px 10px;
            font-size: 13px;
            border-radius: 6px;
        }

        .modal-top .modal-dialog {
            margin-top: 40px;
        }

        .filtro-orden {
            width: 190px;
        }

        .form-section-title {
            font-size: 18px;
            font-weight: 700;
            margin-bottom: 16px;
        }

        .message {
            display: block;
            font-size: 14px;
            font-weight: 600;
            margin-bottom: 12px;
        }

        .switch-container {
            display: flex;
            align-items: center;
            gap: 8px;
            flex-wrap: nowrap;
        }

        .switch-text {
            font-size: 13px;
            font-weight: 600;
            color: #374151;
            white-space: nowrap;
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
            <h1 class="page-title">Artículos</h1>
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

            <asp:DropDownList
                ID="ddlFiltro"
                runat="server"
                CssClass="form-control filtro-orden"
                AutoPostBack="true"
                OnSelectedIndexChanged="ddlFiltro_SelectedIndexChanged">

                <asp:ListItem Text="Nombre A-Z" Value="Nombre"></asp:ListItem>
                <asp:ListItem Text="Mayor a menor Stock" Value="StockMayorMenor"></asp:ListItem>
                <asp:ListItem Text="Menor a mayor Stock" Value="StockMenorMayor"></asp:ListItem>
                <asp:ListItem Text="Mayor a menor Precio" Value="PrecioMayorMenor"></asp:ListItem>
                <asp:ListItem Text="Menor a mayor Precio" Value="PrecioMenorMayor"></asp:ListItem>
            </asp:DropDownList>

            <asp:Button
                ID="btnNuevoArticulo"
                runat="server"
                Text="Nuevo artículo"
                CssClass="btn btn-primary"
                OnClick="btnNuevoArticulo_Click" />

        </div>
    </div>

    <asp:Label
        ID="lblMensaje"
        runat="server"
        CssClass="message text-danger">
    </asp:Label>

    <div class="dashboard-card">

        <h5 class="form-section-title">
            <asp:Label ID="lblTituloListado" runat="server" Text="Artículos registrados"></asp:Label>
        </h5>

        <div class="table-responsive">
            <asp:GridView
                ID="dgvArticulos"
                runat="server"
                CssClass="table table-striped table-hover mb-0"
                AutoGenerateColumns="false"
                GridLines="None"
                AllowPaging="True"
                PageSize="10"
                PagerStyle-CssClass="grid-pager"
                EmptyDataText="No hay artículos activos registrados."
                OnPageIndexChanging="dgvArticulos_PageIndexChanging"
                OnRowCommand="dgvArticulos_RowCommand"
                OnRowDataBound="dgvArticulos_RowDataBound">

                <Columns>

                    <asp:TemplateField HeaderText="Artículo">
                        <ItemTemplate>
                            <div class="article-cell">
                                <span class="article-name"><%# Eval("Nombre") %></span>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Categoría">
                        <ItemTemplate>
                            <span class="badge-category"><%# Eval("Categoria.Nombre") %></span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Marca">
                        <ItemTemplate>
                            <%# Eval("Marca.Nombre") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Precio">
                        <ItemTemplate>
                            <span class="price-text"><%# Eval("Precio", "{0:C}") %></span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Stock">
                        <ItemTemplate>
                            <span class='badge-stock <%# Convert.ToInt32(Eval("Stock")) <= 5 ? "stock-low" : "stock-ok" %>'>
                                <%# Eval("Stock") %> u.
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <div class="table-actions">

                                <asp:HyperLink
                                    ID="lnkModificar"
                                    runat="server"
                                    NavigateUrl='<%# "ArticuloFormulario.aspx?id=" + Eval("IdArticulo") %>'
                                    CssClass="btn btn-sm btn-outline-primary grid-action-btn"
                                    Text="Modificar">
                                </asp:HyperLink>

                                <asp:Button
                                    ID="btnEliminar"
                                    runat="server"
                                    Text="Eliminar"
                                    CssClass="btn btn-sm btn-outline-danger grid-action-btn"
                                    CommandName="AbrirModalEliminar"
                                    CommandArgument='<%# Eval("IdArticulo") %>' />

                                <asp:Button
                                    ID="btnRestaurar"
                                    runat="server"
                                    Text="Restaurar"
                                    CssClass="btn btn-sm btn-outline-success grid-action-btn"
                                    CommandName="Restaurar"
                                    CommandArgument='<%# Eval("IdArticulo") %>' />

                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>

            </asp:GridView>
        </div>

    </div>

    <div class="modal fade modal-top" id="modalEliminarArticulo" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-header justify-content-center">
                    <h5 class="modal-title">Eliminar artículo</h5>
                </div>

                <div class="modal-body text-center">
                    <asp:HiddenField ID="hfIdArticuloEliminar" runat="server" />

                    <p class="mb-2">
                        ¿Seguro que querés eliminar este artículo?
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