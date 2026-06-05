<%@ Page Title="Proveedores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Proveedores.aspx.cs" Inherits="AppGestionNegocio.Web.Proveedores" %>

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

        .supplier-name {
            font-weight: 600;
        }

        .supplier-email {
            color: #6b7280;
            font-size: 13px;
        }

        .badge-count {
            padding: 5px 10px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 600;
            background-color: #ede9fe;
            color: #6f42c1;
        }

        .badge-buy {
            padding: 5px 10px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 600;
            background-color: #dcfce7;
            color: #166534;
        }

        .empty-state {
            text-align: center;
            color: #6b7280;
            font-weight: 500;
            padding: 28px;
            background-color: #ffffff;
        }

        .col-name {
            width: 25%;
        }

        .col-phone {
            width: 15%;
        }

        .col-email {
            width: 25%;
        }

        .col-articles {
            width: 12%;
        }

        .col-purchases {
            width: 12%;
        }

        .col-actions {
            width: 11%;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-3">
        <div>
            <h1 class="page-title">Proveedores</h1>
        </div>

        <div class="page-actions">
            <button type="button" class="btn btn-outline-secondary">
                Filtrar
            </button>
        </div>
    </div>

    <div class="dashboard-card mb-3">
        <h5 class="form-section-title">Nuevo proveedor</h5>

        <div class="row">
            <div class="col-md-4 mb-3">
                <label>Nombre</label>
                <input type="text" class="form-control" placeholder="Ej: Prove Pet" />
            </div>

            <div class="col-md-3 mb-3">
                <label>Teléfono</label>
                <input type="text" class="form-control" placeholder="Ej: 11 1234-5678" />
            </div>

            <div class="col-md-3 mb-3">
                <label>Email</label>
                <input type="email" class="form-control" placeholder="Ej: ventas@provepet.com" />
            </div>

            <div class="col-md-2 mb-3 d-flex align-items-end">
                <button type="button" class="btn btn-primary w-100">
                    Guardar
                </button>
            </div>
        </div>
    </div>

    <div class="dashboard-card">

        <h5 class="form-section-title">Proveedores registrados</h5>

        <div class="table-responsive">
            <table class="table table-striped table-hover mb-0">
                <colgroup>
                    <col class="col-name" />
                    <col class="col-phone" />
                    <col class="col-email" />
                    <col class="col-articles" />
                    <col class="col-purchases" />
                    <col class="col-actions" />
                </colgroup>

                <thead>
                    <tr>
                        <th>Proveedor</th>
                        <th>Teléfono</th>
                        <th>Email</th>
                        <th>Artículos asociados</th>
                        <th>Compras registradas</th>
                        <th class="text-center">Acciones</th>
                    </tr>
                </thead>
            </table>
        </div>

    </div>

</asp:Content>