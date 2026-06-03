<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="AppGestionNegocio.Web.Clientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <div class="mb-4">
        <h1>Lista de Clientes</h1>
    </div>

    <asp:GridView ID="dgvClientes" runat="server"
        CssClass="table table-striped table-hover table-bordered" 
        AutoGenerateColumns="false"
        OnPageIndexChanging="dgvClientes_PageIndexChanging"
        AllowPaging="True" PageSize="10">
        <Columns>
            <asp:BoundField HeaderText="CUIT" DataField="Cuit" />
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Apellido" DataField="Apellido" />
            <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="✍" />
        </Columns>
    </asp:GridView>

</asp:Content>