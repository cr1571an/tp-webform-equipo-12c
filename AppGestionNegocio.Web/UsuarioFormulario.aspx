<%@ Page Title="Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UsuarioFormulario.aspx.cs" Inherits="AppGestionNegocio.Web.UsuarioFormulario" %>

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

        .security-box {
            background-color: #f9fafb;
            border: 1px solid #e5e7eb;
            border-radius: 10px;
            padding: 16px;
            margin-top: 8px;
        }

        .form-actions {
            display: flex;
            justify-content: flex-end;
            gap: 8px;
            border-top: 1px solid #e5e7eb;
            padding-top: 18px;
            margin-top: 10px;
        }

        .section-divider {
            border-top: 1px solid #e5e7eb;
            padding-top: 18px;
            margin-top: 8px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="d-flex justify-content-between align-items-center mb-3">
        <div>
            <h1 class="page-title">
                <asp:Label ID="lblTituloPagina" runat="server" Text="Registrar usuario"></asp:Label>
            </h1>
        </div>
    </div>

    <div class="dashboard-card">

        <h5 class="form-section-title">Datos del empleado</h5>

        <div class="row">
            <div class="col-md-4 mb-3">
                <label class="form-label-custom d-block">Nombre</label>

                <asp:TextBox
                    ID="txtEmpleadoNombre"
                    runat="server"
                    CssClass="form-control"
                    placeholder="Ej: Juan">
                </asp:TextBox>
            </div>

            <div class="col-md-4 mb-3">
                <label class="form-label-custom d-block">Apellido</label>

                <asp:TextBox
                    ID="txtEmpleadoApellido"
                    runat="server"
                    CssClass="form-control"
                    placeholder="Ej: Pérez">
                </asp:TextBox>
            </div>

            <div class="col-md-4 mb-3">
                <label class="form-label-custom d-block">DNI</label>

                <asp:TextBox
                    ID="txtEmpleadoDni"
                    runat="server"
                    CssClass="form-control"
                    placeholder="Ej: 40111222">
                </asp:TextBox>
            </div>
        </div>

        <div class="row">
            <div class="col-md-4 mb-3">
                <label class="form-label-custom d-block">Teléfono</label>

                <asp:TextBox
                    ID="txtEmpleadoTelefono"
                    runat="server"
                    CssClass="form-control"
                    placeholder="Ej: 1122334455">
                </asp:TextBox>
            </div>

            <div class="col-md-4 mb-3">
                <label class="form-label-custom d-block">Email</label>

                <asp:TextBox
                    ID="txtEmpleadoEmail"
                    runat="server"
                    CssClass="form-control"
                    placeholder="Ej: empleado@email.com">
                </asp:TextBox>
            </div>

            <div class="col-md-4 mb-3">
                <label class="form-label-custom d-block">Fecha de ingreso</label>

                <asp:TextBox
                    ID="txtFechaIngreso"
                    runat="server"
                    CssClass="form-control"
                    TextMode="Date">
                </asp:TextBox>
            </div>
        </div>

        <div class="section-divider">
            <h5 class="form-section-title">Datos de acceso</h5>

            <div class="row">
                <div class="col-md-6 mb-3">
                    <asp:Label
                        ID="lblRol"
                        runat="server"
                        CssClass="form-label-custom d-block"
                        Text="Rol">
                    </asp:Label>

                    <asp:DropDownList
                        ID="ddlRol"
                        runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>
                </div>

                <div class="col-md-6 mb-3">
                    <label class="form-label-custom d-block">Nombre de usuario</label>

                    <asp:TextBox
                        ID="txtNombre"
                        runat="server"
                        CssClass="form-control"
                        placeholder="Ej: juanp">
                    </asp:TextBox>
                </div>
            </div>
        </div>

        <div class="security-box">
            <h5 class="form-section-title">Seguridad</h5>

            <asp:Label
                ID="lblPasswordHelper"
                runat="server"
                CssClass="helper-text d-block mb-2"
                Text="Si no querés cambiar la contraseña, dejá estos campos vacíos."
                Visible="false">
            </asp:Label>

            <div class="row">
                <div class="col-md-6 mb-3">
                    <label class="form-label-custom d-block">Contraseña</label>

                    <asp:TextBox
                        ID="txtPassword"
                        runat="server"
                        CssClass="form-control"
                        TextMode="Password">
                    </asp:TextBox>
                </div>

                <div class="col-md-6 mb-3">
                    <label class="form-label-custom d-block">Confirmar contraseña</label>

                    <asp:TextBox
                        ID="txtConfirmPassword"
                        runat="server"
                        CssClass="form-control"
                        TextMode="Password">
                    </asp:TextBox>
                </div>
            </div>
        </div>

        <asp:UpdatePanel ID="upBotones" runat="server">
            <ContentTemplate>

                <div class="mt-3">
                    <asp:Label
                        ID="lblMensajeError"
                        runat="server"
                        CssClass="alert alert-danger d-block"
                        Visible="false">
                    </asp:Label>
                </div>

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
                        Text="Guardar usuario"
                        OnClick="btnGuardar_Click" />

                </div>

            </ContentTemplate>
        </asp:UpdatePanel>

    </div>

</asp:Content>