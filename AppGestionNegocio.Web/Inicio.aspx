<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="AppGestionNegocio.Web.Inicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
<h2>Inicio</h2>
    <p>Bienvenido al sistema de gestión comercial.</p>

    <div class="row mt-4">

        <div class="col-md-3">
            <div class="card card-dashboard">
                <div class="card-body">
                    <h5>Ventas del Día</h5>
                    <h3>$ 120.000</h3>
                </div>
            </div>
        </div>

        <div class="col-md-3">
            <div class="card card-dashboard">
                <div class="card-body">
                    <h5>Compras del Día</h5>
                    <h3>$ 80.000</h3>
                </div>
            </div>
        </div>

        <div class="col-md-3">
            <div class="card card-dashboard">
                <div class="card-body">
                    <h5>Productos</h5>
                    <h3>350</h3>
                </div>
            </div>
        </div>

        <div class="col-md-3">
            <div class="card card-dashboard">
                <div class="card-body">
                    <h5>Clientes</h5>
                    <h3>120</h3>
                </div>
            </div>
        </div>

    </div>

    <h4 class="mt-5">Accesos Rápidos</h4>

    <div class="row">

        <div class="col-md-3">
            <div class="card text-center p-3">
                <i class="fas fa-shopping-cart fa-2x"></i>
                <h5 class="mt-2">Ventas</h5>
            </div>
        </div>

        <div class="col-md-3">
            <div class="card text-center p-3">
                <i class="fas fa-box fa-2x"></i>
                <h5 class="mt-2">Productos</h5>
            </div>
        </div>

        <div class="col-md-3">
            <div class="card text-center p-3">
                <i class="fas fa-truck fa-2x"></i>
                <h5 class="mt-2">Compras</h5>
            </div>
        </div>

        <div class="col-md-3">
            <div class="card text-center p-3">
                <i class="fas fa-users fa-2x"></i>
                <h5 class="mt-2">Clientes</h5>
            </div>
        </div>

    </div>

    <h4 class="mt-5">Actividad Reciente</h4>

    <ul class="list-group">
        <li class="list-group-item">Nueva venta registrada</li>
        <li class="list-group-item">Nuevo producto agregado</li>
        <li class="list-group-item">Nueva compra registrada</li>
        <li class="list-group-item">Nuevo cliente registrado</li>
    </ul>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>
