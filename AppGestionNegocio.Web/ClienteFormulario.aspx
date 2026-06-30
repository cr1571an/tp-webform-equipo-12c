<%@ Page Title="Cliente" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClienteFormulario.aspx.cs" Inherits="AppGestionNegocio.Web.ClienteFormulario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-section-title {
            font-size: 18px;
            font-weight: 700;
            margin-bottom: 16px;
        }

        .form-label-custom {
            font-weight: 600;
            font-size: 14px;
            color: #374151;
            margin-bottom: 6px;
        }

        .helper-text {
            color: #6b7280;
            font-size: 13px;
            margin-top: 4px;
        }

        .form-actions {
            display: flex;
            justify-content: flex-end;
            gap: 8px;
            border-top: 1px solid #e5e7eb;
            padding-top: 18px;
            margin-top: 10px;
        }

        .form-message {
            display: block;
            text-align: left;
            font-weight: 600;
            font-size: 14px;
            margin-bottom: 18px;
            padding: 10px 18px;
            border-radius: 8px;
            background-color: #fee2e2;
            color: #b91c1c;
            border: 1px solid #fecaca;
        }

        .form-message:empty {
            display: none;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-3">
        <div>
            <h1 class="page-title">
                <asp:Label ID="lblTitulo" runat="server" Text="Registrar cliente"></asp:Label>
            </h1>
        </div>
    </div>

    <div class="dashboard-card">

        <h5 class="form-section-title">Datos fiscales</h5>

        <div class="row">
            <div class="col-md-6 mb-3">
                <asp:Label
                    ID="lblCondicionIva"
                    runat="server"
                    CssClass="form-label-custom d-block"
                    Text="Condición IVA">
                </asp:Label>

                <asp:DropDownList
                    ID="ddlCondicionIva"
                    runat="server"
                    CssClass="form-control">

                    <asp:ListItem Text="Responsable Inscripto" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Monotributo" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Consumidor Final" Value="3"></asp:ListItem>

                </asp:DropDownList>
            </div>

            <div class="col-md-6 mb-3">
                <asp:Label
                    ID="lblCuit"
                    runat="server"
                    CssClass="form-label-custom d-block"
                    Text="CUIT">
                </asp:Label>

                <asp:TextBox
                    ID="txtCuit"
                    runat="server"
                    CssClass="form-control"
                    MaxLength="13"
                    placeholder="Ej: 20-12345678-9">
                </asp:TextBox>

                <div class="helper-text">Puede quedar vacío para Consumidor Final.</div>
            </div>
        </div>

        <h5 class="form-section-title mt-4">Datos personales</h5>

        <div class="row">
            <div class="col-md-6 mb-3">
                <asp:Label
                    ID="lblNombre"
                    runat="server"
                    CssClass="form-label-custom d-block"
                    Text="Nombre">
                </asp:Label>

                <asp:TextBox
                    ID="txtNombre"
                    runat="server"
                    CssClass="form-control"
                    MaxLength="55"
                    placeholder="Ingrese el nombre">
                </asp:TextBox>
            </div>

            <div class="col-md-6 mb-3">
                <asp:Label
                    ID="lblApellido"
                    runat="server"
                    CssClass="form-label-custom d-block"
                    Text="Apellido">
                </asp:Label>

                <asp:TextBox
                    ID="txtApellido"
                    runat="server"
                    CssClass="form-control"
                    MaxLength="55"
                    placeholder="Ingrese el apellido">
                </asp:TextBox>
            </div>
        </div>

        <h5 class="form-section-title mt-4">Datos de contacto</h5>

        <div class="row">
            <div class="col-md-6 mb-3">
                <asp:Label
                    ID="lblTelefono"
                    runat="server"
                    CssClass="form-label-custom d-block"
                    Text="Teléfono">
                </asp:Label>

                <asp:TextBox
                    ID="txtTelefono"
                    runat="server"
                    CssClass="form-control"
                    MaxLength="15"
                    TextMode="Phone"
                    placeholder="Ej: 11 1234-5678">
                </asp:TextBox>
            </div>

            <div class="col-md-6 mb-3">
                <asp:Label
                    ID="lblEmail"
                    runat="server"
                    CssClass="form-label-custom d-block"
                    Text="Email">
                </asp:Label>

                <asp:TextBox
                    ID="txtEmail"
                    runat="server"
                    CssClass="form-control"
                    MaxLength="100"
                    TextMode="Email"
                    placeholder="correo@gmail.com">
                </asp:TextBox>
            </div>
        </div>

        <h5 class="form-section-title mt-4">Domicilio</h5>

        <div class="row">
            <div class="col-md-3 mb-3">
                <asp:Label
                    ID="lblCodigoPostal"
                    runat="server"
                    CssClass="form-label-custom d-block"
                    Text="Código Postal">
                </asp:Label>

                <asp:TextBox
                    ID="txtCodigoPostal"
                    runat="server"
                    CssClass="form-control"
                    MaxLength="6"
                    placeholder="Ej: 1427">
                </asp:TextBox>
            </div>

            <div class="col-md-9 mb-3">
                <asp:Label
                    ID="lblDomicilio"
                    runat="server"
                    CssClass="form-label-custom d-block"
                    Text="Domicilio">
                </asp:Label>

                <asp:TextBox
                    ID="txtDomicilio"
                    runat="server"
                    CssClass="form-control"
                    MaxLength="255"
                    placeholder="Ej: Av. Corrientes 6271">
                </asp:TextBox>
            </div>
        </div>

        <asp:Label
            ID="lblMensaje"
            runat="server"
            CssClass="form-message">
        </asp:Label>

        <div class="form-actions">
            <asp:Button
                ID="btnCancelar"
                runat="server"
                CssClass="btn btn-outline-secondary"
                Text="Cancelar"
                CausesValidation="false"
                OnClick="btnCancelar_Click" />

            <asp:Button
                ID="btnGuardar"
                runat="server"
                CssClass="btn btn-primary"
                Text="Guardar cliente"
                OnClick="btnGuardar_Click" />
        </div>

    </div>

</asp:Content>