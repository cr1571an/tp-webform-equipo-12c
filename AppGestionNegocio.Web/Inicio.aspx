<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="AppGestionNegocio.Web.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .stat-card {
            display: flex;
            align-items: center;
            min-height: 110px;
        }

        .stat-icon {
            width: 58px;
            height: 58px;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 24px;
            margin-right: 18px;
            background-color: #ede9fe;
            color: #6f42c1;
        }

        .stat-title {
            color: #4b5563;
            font-size: 14px;
            margin-bottom: 6px;
        }

        .stat-value {
            font-size: 24px;
            font-weight: 700;
            margin-bottom: 4px;
            color: #111827;
        }

        .stat-detail {
            font-size: 13px;
            margin-bottom: 0;
            color: #6f42c1;
            font-weight: 600;
        }

        .module-link {
            color: inherit;
            text-decoration: none;
            display: block;
            height: 100%;
        }

        .module-link:hover {
            color: inherit;
            text-decoration: none;
        }

        .module-card {
            transition: all 0.2s ease;
            min-height: 145px;
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            text-align: center;
        }

        .module-card:hover {
            transform: translateY(-3px);
        }

        .module-icon {
            width: 58px;
            height: 58px;
            border-radius: 50%;
            margin-bottom: 14px;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 24px;
            background-color: #ede9fe;
            color: #6f42c1;
        }

        .module-card h5 {
            margin-bottom: 6px;
        }

        .module-card p {
            margin-bottom: 0;
            color: #6b7280;
            font-size: 13px;
        }

        .activity-card {
            height: 100%;
            min-height: 335px;
            display: flex;
            flex-direction: column;
        }

        .activity-content {
            flex: 1;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .empty-activity {
            text-align: center;
            color: #6b7280;
            font-weight: 500;
        }

        .empty-activity i {
            display: block;
            font-size: 36px;
            color: #6f42c1;
            background-color: #ede9fe;
            width: 64px;
            height: 64px;
            line-height: 64px;
            border-radius: 50%;
            margin: 0 auto 12px auto;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="mb-4">
        <h1 class="page-title">Inicio</h1>
        <p class="page-subtitle">Bienvenido al sistema de gestión del Pet Shop</p>
    </div>

    <div class="row mb-4">

        <div class="col-md-3 mb-4">
            <div class="dashboard-card stat-card">
                <div class="stat-icon">
                    <i class="bi bi-bag-check"></i>
                </div>

                <div>
                    <p class="stat-title">Ventas del día</p>
                    <h3 class="stat-value">$ 0,00</h3>
                    <p class="stat-detail">0 ventas</p>
                </div>
            </div>
        </div>

        <div class="col-md-3 mb-4">
            <div class="dashboard-card stat-card">
                <div class="stat-icon">
                    <i class="bi bi-cart-check"></i>
                </div>

                <div>
                    <p class="stat-title">Compras del día</p>
                    <h3 class="stat-value">$ 0,00</h3>
                    <p class="stat-detail">0 compras</p>
                </div>
            </div>
        </div>

        <div class="col-md-3 mb-4">
            <div class="dashboard-card stat-card">
                <div class="stat-icon">
                    <i class="bi bi-box-seam"></i>
                </div>

                <div>
                    <p class="stat-title">Artículos</p>
                    <h3 class="stat-value">0</h3>
                    <p class="stat-detail">Activos</p>
                </div>
            </div>
        </div>

        <div class="col-md-3 mb-4">
            <div class="dashboard-card stat-card">
                <div class="stat-icon">
                    <i class="bi bi-people"></i>
                </div>

                <div>
                    <p class="stat-title">Clientes</p>
                    <h3 class="stat-value">0</h3>
                    <p class="stat-detail">Activos</p>
                </div>
            </div>
        </div>

    </div>

    <div class="row align-items-stretch">

        <div class="col-lg-8 mb-4">

            <h3 class="mb-4">Módulos principales</h3>

            <div class="row">

                <div class="col-md-3 mb-4">
                    <a href="Articulos.aspx" class="module-link">
                        <div class="dashboard-card module-card">
                            <div class="module-icon">
                                <i class="bi bi-box-seam"></i>
                            </div>
                            <h5>Artículos</h5>
                            <p>Gestioná artículos y stock</p>
                        </div>
                    </a>
                </div>

                <div class="col-md-3 mb-4">
                    <a href="Categorias.aspx" class="module-link">
                        <div class="dashboard-card module-card">
                            <div class="module-icon">
                                <i class="bi bi-tags"></i>
                            </div>
                            <h5>Categorías</h5>
                            <p>Administrá categorías</p>
                        </div>
                    </a>
                </div>

                <div class="col-md-3 mb-4">
                    <a href="Marcas.aspx" class="module-link">
                        <div class="dashboard-card module-card">
                            <div class="module-icon">
                                <i class="bi bi-award"></i>
                            </div>
                            <h5>Marcas</h5>
                            <p>Gestioná marcas</p>
                        </div>
                    </a>
                </div>

                <div class="col-md-3 mb-4">
                    <a href="Proveedores.aspx" class="module-link">
                        <div class="dashboard-card module-card">
                            <div class="module-icon">
                                <i class="bi bi-truck"></i>
                            </div>
                            <h5>Proveedores</h5>
                            <p>Administrá proveedores</p>
                        </div>
                    </a>
                </div>

                <div class="col-md-3 mb-4">
                    <a href="Clientes.aspx" class="module-link">
                        <div class="dashboard-card module-card">
                            <div class="module-icon">
                                <i class="bi bi-people"></i>
                            </div>
                            <h5>Clientes</h5>
                            <p>Gestioná clientes</p>
                        </div>
                    </a>
                </div>

                <div class="col-md-3 mb-4">
                    <a href="Compras.aspx" class="module-link">
                        <div class="dashboard-card module-card">
                            <div class="module-icon">
                                <i class="bi bi-cart-check"></i>
                            </div>
                            <h5>Compras</h5>
                            <p>Registrá compras</p>
                        </div>
                    </a>
                </div>

                <div class="col-md-3 mb-4">
                    <a href="Ventas.aspx" class="module-link">
                        <div class="dashboard-card module-card">
                            <div class="module-icon">
                                <i class="bi bi-receipt"></i>
                            </div>
                            <h5>Ventas</h5>
                            <p>Registrá ventas</p>
                        </div>
                    </a>
                </div>

                <div class="col-md-3 mb-4">
                    <a href="MediosPago.aspx" class="module-link">
                        <div class="dashboard-card module-card">
                            <div class="module-icon">
                                <i class="bi bi-credit-card"></i>
                            </div>
                            <h5>Medios de Pago</h5>
                            <p>Gestioná medios de pago</p>
                        </div>
                    </a>
                </div>

            </div>
        </div>

        <div class="col-lg-4 mb-4 d-flex flex-column">

            <h3 class="mb-4">Actividad reciente</h3>

            <div class="dashboard-card activity-card">
                <div class="activity-content">
                    <div class="empty-activity">
                        <i class="bi bi-clock-history"></i>
                        No hay actividad reciente para mostrar.
                    </div>
                </div>
            </div>

        </div>

    </div>

</asp:Content>