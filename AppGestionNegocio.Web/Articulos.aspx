<%@ Page Title="Artículos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Articulos.aspx.cs" Inherits="AppGestionNegocio.Web.Articulos" %>

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
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="d-flex justify-content-between align-items-center mb-3">
        <div>
            <h1 class="page-title">Artículos</h1>
        </div>
    
        <div class="page-actions">
          <asp:DropDownList ID="ddlFiltro" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFiltro_SelectedIndexChanged">
                <asp:ListItem Text="Mayor a menor Stock" Value="StockMayorMenor"></asp:ListItem>
                <asp:ListItem Text="Menor a mayor Stock" Value="StockMenorMayor"></asp:ListItem>
                <asp:ListItem Text="Mayor a menor Precio" Value="PrecioMayorMenor"></asp:ListItem>
                <asp:ListItem Text="Menor a mayor Precio" Value="PrecioMenorMayor"></asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="btnNuevoArticulo" runat="server" Text="Nuevo artículo" CssClass="btn btn-primary" OnClick="btnNuevoArticulo_Click"/>
        </div>
    </div>

    <div class="dashboard-card">
    
        <div class="table-responsive">
            <asp:GridView ID="dgvArticulos" runat="server" CssClass="table table-striped table-hover mb-0" AutoGenerateColumns="false" GridLines="None" AllowPaging="True" PageSize="10" PagerStyle-CssClass="grid-pager" OnPageIndexChanging="dgvArticulos_PageIndexChanging">
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
                            <a href='ArticuloFormulario.aspx?id=<%# Eval("IdArticulo") %>' class="btn btn-sm btn-outline-primary grid-action-btn">Editar</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

    </div>

</asp:Content>
