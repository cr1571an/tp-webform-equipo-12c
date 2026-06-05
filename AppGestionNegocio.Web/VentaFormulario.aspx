<%@ Page Title="Registrar Venta" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VentaFormulario.aspx.cs" Inherits="AppGestionNegocio.Web.VentaFormulario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-section-title {
            font-size: 18px;
            font-weight: 700;
            margin-bottom: 16px;
        }

        .subsection-title {
            font-size: 16px;
            font-weight: 700;
            margin-top: 8px;
            margin-bottom: 14px;
            color: #374151;
        }

        label {
            font-weight: 600;
            font-size: 14px;
            color: #374151;
        }

        .detail-box {
            background-color: #f9fafb;
            border: 1px solid #e5e7eb;
            border-radius: 10px;
            padding: 16px;
            margin-top: 8px;
        }

        .table td,
        .table th {
            vertical-align: middle;
            white-space: nowrap;
        }

        .badge-stock {
            padding: 5px 10px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 600;
            background-color: #dcfce7;
            color: #166534;
        }

        .form-actions {
            display: flex;
            justify-content: flex-end;
            gap: 8px;
            border-top: 1px solid #e5e7eb;
            padding-top: 18px;
            margin-top: 10px;
        }

        .sale-total-box {
            background-color: #ede9fe;
            color: #4c1d95;
            border-radius: 10px;
            padding: 12px 16px;
            font-weight: 700;
            text-align: right;
        }

        .empty-state {
            text-align: center;
            color: #6b7280;
            font-weight: 500;
            padding: 22px;
            background-color: #ffffff;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-3">
        <div>
            <h1 class="page-title">Registrar venta</h1>
        </div>

        <a href="Ventas.aspx" class="btn btn-outline-secondary">
            Volver al listado
        </a>
    </div>

    <div class="dashboard-card mb-3">
        <h5 class="form-section-title">Datos de la venta</h5>

        <div class="row">
            <div class="col-md-3 mb-3">
                <label>Cliente</label>
                <select class="form-control">
                    <option>Seleccione un cliente</option>
                </select>
            </div>

            <div class="col-md-3 mb-3">
                <label>Medio de pago</label>
                <select class="form-control">
                    <option>Seleccione un medio de pago</option>
                </select>
            </div>

            <div class="col-md-3 mb-3">
                <label>Fecha de venta</label>
                <input type="date" class="form-control" />
            </div>

            <div class="col-md-3 mb-3">
                <label>Número de factura</label>
                <input type="text" class="form-control" placeholder="Ej: B-0001-00000456" />
            </div>
        </div>

        <div class="detail-box">
            <h6 class="subsection-title">Detalle de venta</h6>

            <div class="row">
                <div class="col-md-3 mb-3">
                    <label>Artículo</label>
                    <select class="form-control">
                        <option>Seleccione un artículo</option>
                    </select>
                </div>

                <div class="col-md-2 mb-3">
                    <label>Stock disponible</label>
                    <input type="text" class="form-control" placeholder="Seleccione un artículo" readonly />
                </div>

                <div class="col-md-1 mb-3">
                    <label>Cantidad</label>
                    <input type="number" class="form-control" placeholder="0" />
                </div>

                <div class="col-md-2 mb-3">
                    <label>Precio unitario</label>
                    <input type="text" class="form-control" placeholder="$ 0,00" />
                </div>

                <div class="col-md-2 mb-3">
                    <label>Subtotal</label>
                    <input type="text" class="form-control" placeholder="$ 0,00" />
                </div>

                <div class="col-md-2 mb-3 d-flex align-items-end">
                    <button type="button" class="btn btn-outline-primary w-100">
                        Agregar
                    </button>
                </div>
            </div>

            <div class="table-responsive mt-2">
                <table class="table table-sm table-striped table-hover mb-0">
                    <thead>
                        <tr>
                            <th>Artículo</th>
                            <th>Stock</th>
                            <th>Cantidad</th>
                            <th>Precio unitario</th>
                            <th>Subtotal</th>
                            <th class="text-center">Acción</th>
                        </tr>
                    </thead>
                </table>
            </div>
        </div>

        <div class="row mt-3">
            <div class="col-md-8 mb-3">
                <label>Observaciones</label>
                <input type="text" class="form-control" placeholder="Observaciones generales de la venta" />
            </div>

            <div class="col-md-4 mb-3">
                <label>Total</label>
                <div class="sale-total-box">
                    Total estimado: $ 0,00
                </div>
            </div>
        </div>

        <div class="form-actions">
            <a href="Ventas.aspx" class="btn btn-outline-secondary">
                Cancelar
            </a>

            <button type="button" class="btn btn-primary">
                Guardar venta
            </button>
        </div>
    </div>

</asp:Content>