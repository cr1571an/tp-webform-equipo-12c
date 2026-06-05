<%@ Page Title="Ventas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="AppGestionNegocio.Web.Ventas" %>

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

        .badge-payment {
            padding: 5px 10px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 600;
            background-color: #ede9fe;
            color: #6f42c1;
        }

        .badge-detail {
            padding: 5px 10px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 600;
            background-color: #dbeafe;
            color: #1d4ed8;
        }

        .sale-total {
            font-weight: 700;
            color: #111827;
        }

        .sale-client {
            font-weight: 600;
        }

        .sale-user {
            color: #6b7280;
            font-size: 13px;
        }

        .form-section-title {
            font-size: 18px;
            font-weight: 700;
            margin-bottom: 16px;
        }

        .empty-state {
            text-align: center;
            color: #6b7280;
            font-weight: 500;
            padding: 28px;
            background-color: #ffffff;
        }

        .col-date {
            width: 10%;
        }

        .col-client {
            width: 17%;
        }

        .col-user {
            width: 10%;
        }

        .col-payment {
            width: 13%;
        }

        .col-invoice {
            width: 15%;
        }

        .col-items {
            width: 10%;
        }

        .col-total {
            width: 10%;
        }

        .col-actions {
            width: 15%;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-3">
        <div>
            <h1 class="page-title">Ventas</h1>
        </div>

        <div class="page-actions">
            <button type="button" class="btn btn-outline-secondary">
                Filtrar
            </button>

            <a href="VentaFormulario.aspx" class="btn btn-primary">
                Registrar venta
            </a>
        </div>
    </div>

    <div class="dashboard-card">

        <h5 class="form-section-title">Ventas registradas</h5>

        <div class="table-responsive">
            <table class="table table-striped table-hover mb-0">
                <colgroup>
                    <col class="col-date" />
                    <col class="col-client" />
                    <col class="col-user" />
                    <col class="col-payment" />
                    <col class="col-invoice" />
                    <col class="col-items" />
                    <col class="col-total" />
                    <col class="col-actions" />
                </colgroup>

                <thead>
                    <tr>
                        <th>Fecha</th>
                        <th>Cliente</th>
                        <th>Usuario</th>
                        <th>Medio de pago</th>
                        <th>Factura</th>
                        <th>Artículos</th>
                        <th>Total</th>
                        <th class="text-center">Acciones</th>
                    </tr>
                </thead>
            </table>
        </div>

    </div>

</asp:Content>