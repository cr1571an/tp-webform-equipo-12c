<%@ Page Title="Medios de Pago" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MediosPago.aspx.cs" Inherits="AppGestionNegocio.Web.MediosPago" %>

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

        .payment-name {
            font-weight: 600;
        }

        .payment-description {
            color: #6b7280;
            font-size: 13px;
        }

        .badge-sales {
            padding: 5px 10px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 600;
            background-color: #dbeafe;
            color: #1d4ed8;
        }

        .badge-purchases {
            padding: 5px 10px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 600;
            background-color: #ede9fe;
            color: #6f42c1;
        }

        .badge-active {
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
            width: 22%;
        }

        .col-description {
            width: 34%;
        }

        .col-sales {
            width: 14%;
        }

        .col-purchases {
            width: 14%;
        }

        .col-actions {
            width: 16%;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-3">
        <div>
            <h1 class="page-title">Medios de Pago</h1>
        </div>

        <div class="page-actions">
            <button type="button" class="btn btn-outline-secondary">
                Filtrar
            </button>
        </div>
    </div>

    <div class="dashboard-card mb-3">
        <h5 class="form-section-title">Nuevo medio de pago</h5>

        <div class="row">
            <div class="col-md-4 mb-3">
                <label>Nombre</label>
                <input type="text" class="form-control" placeholder="Ej: Efectivo" />
            </div>

            <div class="col-md-6 mb-3">
                <label>Descripción</label>
                <input type="text" class="form-control" placeholder="Ej: Pago realizado en caja" />
            </div>

            <div class="col-md-2 mb-3 d-flex align-items-end">
                <button type="button" class="btn btn-primary w-100">
                    Guardar
                </button>
            </div>
        </div>
    </div>

    <div class="dashboard-card">

        <h5 class="form-section-title">Medios de pago registrados</h5>

        <div class="table-responsive">
            <table class="table table-striped table-hover mb-0">
                <colgroup>
                    <col class="col-name" />
                    <col class="col-description" />
                    <col class="col-sales" />
                    <col class="col-purchases" />
                    <col class="col-actions" />
                </colgroup>

                <thead>
                    <tr>
                        <th>Medio de pago</th>
                        <th>Descripción</th>
                        <th>Ventas asociadas</th>
                        <th>Compras asociadas</th>
                        <th class="text-center">Acciones</th>
                    </tr>
                </thead>
            </table>
        </div>

    </div>

</asp:Content>