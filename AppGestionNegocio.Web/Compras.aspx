<%@ Page Title="Compras" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Compras.aspx.cs" Inherits="AppGestionNegocio.Web.Compras" %>

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
            background-color: #dcfce7;
            color: #166534;
        }

        .purchase-total {
            font-weight: 700;
            color: #111827;
        }

        .purchase-provider {
            font-weight: 600;
        }

        .purchase-observation {
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

        .col-provider {
            width: 16%;
        }

        .col-user {
            width: 10%;
        }

        .col-payment {
            width: 12%;
        }

        .col-receipt {
            width: 14%;
        }

        .col-items {
            width: 10%;
        }

        .col-total {
            width: 10%;
        }

        .col-observations {
            width: 10%;
        }

        .col-actions {
            width: 8%;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-3">
        <div>
            <h1 class="page-title">Compras</h1>
        </div>

        <div class="page-actions">
            <button type="button" class="btn btn-outline-secondary">
                Filtrar
            </button>

            <a href="CompraFormulario.aspx" class="btn btn-primary">
                Registrar compra
            </a>
        </div>
    </div>

    <div class="dashboard-card">

        <h5 class="form-section-title">Compras registradas</h5>

        <div class="table-responsive">
            <table class="table table-striped table-hover mb-0">
                <colgroup>
                    <col class="col-date" />
                    <col class="col-provider" />
                    <col class="col-user" />
                    <col class="col-payment" />
                    <col class="col-receipt" />
                    <col class="col-items" />
                    <col class="col-total" />
                    <col class="col-observations" />
                    <col class="col-actions" />
                </colgroup>

                <thead>
                    <tr>
                        <th>Fecha</th>
                        <th>Proveedor</th>
                        <th>Usuario</th>
                        <th>Medio de pago</th>
                        <th>Comprobante</th>
                        <th>Artículos</th>
                        <th>Total</th>
                        <th>Observaciones</th>
                        <th class="text-center">Acciones</th>
                    </tr>
                </thead>
            </table>
        </div>

    </div>

</asp:Content>