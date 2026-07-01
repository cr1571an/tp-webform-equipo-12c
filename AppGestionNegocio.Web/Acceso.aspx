<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Acceso.aspx.cs" Inherits="AppGestionNegocio.Web.Acceso" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pet Shop - Acceso</title>

    <link href="<%= ResolveUrl("~/Content/bootstrap.min.css") %>" rel="stylesheet" />

    <style>
        body {
            margin: 0;
            min-height: 100vh;
            font-family: 'Segoe UI', Arial, sans-serif;
            background: linear-gradient(135deg, #141b2d 0%, #202a3d 45%, #6f42c1 100%);
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .login-wrapper {
            width: 100%;
            max-width: 760px;
            padding: 20px;
        }

        .login-card {
            width: 100%;
            background-color: white;
            border-radius: 22px;
            padding: 48px 80px;
            box-shadow: 0 20px 45px rgba(15, 23, 42, 0.30);
        }

        .brand-area {
            text-align: center;
            margin-bottom: 28px;
        }

        .animal-icon {
            width: 100px;
            height: 100px;
            border-radius: 28px;
            background: linear-gradient(135deg, #ede9fe 0%, #ddd6fe 100%);
            color: #6f42c1;
            display: flex;
            align-items: center;
            justify-content: center;
            gap: 6px;
            margin: 0 auto 18px auto;
            font-size: 32px;
        }

        .brand-title {
            font-size: 32px;
            font-weight: 800;
            color: #111827;
            margin-bottom: 0;
        }

        .login-title {
            font-size: 24px;
            font-weight: 700;
            color: #111827;
            margin-bottom: 26px;
            text-align: center;
        }

        .form-content {
            max-width: 520px;
            margin: 0 auto;
        }

        .form-label-custom {
            font-weight: 600;
            font-size: 14px;
            color: #374151;
            margin-bottom: 7px;
        }

        .form-control {
            height: 50px;
            border-radius: 10px;
            border: 1px solid #d1d5db;
            font-size: 15px;
        }

        .form-control:focus {
            border-color: #6f42c1;
            box-shadow: 0 0 0 0.18rem rgba(111, 66, 193, 0.18);
        }

        .button-area {
            text-align: center;
            margin-top: 22px;
        }

        .btn-login {
            width: 220px;
            height: 48px;
            border-radius: 11px;
            font-weight: 700;
            background-color: #6f42c1;
            border-color: #6f42c1;
        }

        .btn-login:hover {
            background-color: #5b34a3;
            border-color: #5b34a3;
        }

        .message-error {
            display: block;
            margin-top: 6px;
            margin-bottom: 18px;
            padding: 10px 12px;
            border-radius: 10px;
            background-color: #fee2e2;
            color: #991b1b;
            font-size: 14px;
            font-weight: 600;
            text-align: center;
        }

        @media (max-width: 576px) {
            .login-card {
                padding: 34px 26px;
            }

            .login-wrapper {
                max-width: 100%;
            }

            .form-content {
                max-width: 100%;
            }
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div class="login-wrapper">
            <div class="login-card">

                <div class="brand-area">
                    <h1 class="brand-title">Pet Shop</h1>
                </div>

                <h2 class="login-title">Iniciar sesión</h2>

                <div class="form-content">

                    <div class="mb-3">
                        <asp:Label ID="lblCorreo" runat="server" CssClass="form-label form-label-custom" Text="Usuario o correo"></asp:Label>
                        <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" placeholder="Ingresá tu usuario o correo"></asp:TextBox>
                    </div>

                    <div class="mb-3">
                        <asp:Label ID="lblContrasenia" runat="server" CssClass="form-label form-label-custom" Text="Contraseña"></asp:Label>
                        <asp:TextBox ID="txtContrasenia" runat="server" CssClass="form-control" TextMode="Password" placeholder="Ingresá tu contraseña"></asp:TextBox>
                    </div>

                    <asp:Label ID="lblMensaje" runat="server" CssClass="message-error" Visible="false"></asp:Label>

                    <div class="button-area">
                        <asp:Button ID="btnIniciarSesion" runat="server" CssClass="btn btn-primary btn-login" Text="Ingresar" OnClick="btnIniciarSesion_Click" />
                    </div>

                </div>

            </div>
        </div>

        <script src="<%= ResolveUrl("~/Scripts/jquery-3.5.0.min.js") %>"></script>
        <script src="<%= ResolveUrl("~/Scripts/bootstrap.bundle.js") %>"></script>
    </form>
</body>
</html>