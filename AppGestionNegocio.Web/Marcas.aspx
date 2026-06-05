<%@ Page Title="Marcas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Marcas.aspx.cs" Inherits="AppGestionNegocio.Web.Marcas" %>

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

        .badge-count {
            padding: 5px 10px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 600;
            background-color: #ede9fe;
            color: #6f42c1;
        }

        .brand-name {
            font-weight: 600;
        }

        .empty-state {
            text-align: center;
            color: #6b7280;
            font-weight: 500;
            padding: 28px;
            background-color: #ffffff;
        }

        .col-name {
            width: 55%;
        }

        .col-count {
            width: 25%;
        }

        .col-actions {
            width: 20%;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-3">
        <div>
            <h1 class="page-title">Marcas</h1>
        </div>

        <div class="page-actions">
            <button type="button" class="btn btn-outline-secondary">
                Filtrar
            </button>
        </div>
    </div>

    <div class="dashboard-card mb-3">
        <h5 class="form-section-title">Nueva marca</h5>

        <div class="row">
            <div class="col-md-10 mb-3">
                <label>Nombre</label>
                <input type="text" class="form-control" placeholder="Ej: Royal Canin" />
            </div>

            <div class="col-md-2 mb-3 d-flex align-items-end">
                <button type="button" class="btn btn-primary w-100">
                    Guardar
                </button>
            </div>
        </div>
    </div>

    <div class="dashboard-card">

        <h5 class="form-section-title">Marcas registradas</h5>

        <div class="table-responsive">
            <table class="table table-striped table-hover mb-0">
                <colgroup>
                    <col class="col-name" />
                    <col class="col-count" />
                    <col class="col-actions" />
                </colgroup>

                <thead>
                    <tr>
                        <th>Nombre</th>
                        <th>Artículos asociados</th>
                        <th class="text-center">Acciones</th>
                    </tr>
                </thead>
            </table>
        </div>

    </div>

</asp:Content>