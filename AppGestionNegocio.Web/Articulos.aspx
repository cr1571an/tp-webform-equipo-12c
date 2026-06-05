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
            <button type="button" class="btn btn-outline-secondary">
                Filtrar
            </button>

            <a href="ArticuloFormulario.aspx" class="btn btn-primary">
                Nuevo artículo
            </a>
        </div>
    </div>

    <div class="dashboard-card">

        <div class="table-responsive">
            <table class="table table-striped table-hover mb-0">
                <thead>
                    <tr>
                        <th>Artículo</th>
                        <th>Categoría</th>
                        <th>Marca</th>
                        <th>Precio</th>
                        <th>Stock</th>
                        <th class="text-center">Acciones</th>
                    </tr>
                </thead>
            </table>
        </div>

    </div>

</asp:Content>