<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="SpecimenTracking.LoginPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <%--runat="server"--%>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <!-- Tell the browser to be responsive to screen width -->
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <!-- Font Awesome -->
    <link rel="stylesheet" href="plugins/fontawesome-free/css/all.min.css" />
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css" />
    <!-- icheck bootstrap -->
    <link rel="stylesheet" href="plugins/icheck-bootstrap/icheck-bootstrap.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="dist/css/adminlte.min.css" />
    <!-- Google Font: Source Sans Pro -->
    <link href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700" rel="stylesheet" />
    <script type="text/javascript" src="dist/js/sweetalert.min.js"></script>
    <style type="text/css">
        .captcha-container {
            width: 150px;
            height: 46px;
            overflow: hidden;
            border: 1px solid #ccc;
            position: relative;
            text-align: center;
        }

        .captcha-image {
            position: absolute;
            left: 50%;
            top: 50%;
            width: 100%;
            color: #fff;
            font-size: 35px;
            text-align: center;
            letter-spacing: 10px;
            transform: translate(-50%, -50%);
            text-shadow: 0px 0px 2px #b1b1b1;
            font-family: 'Noto Serif', serif;
        }

        .reload-btn {
            font-size: 30px;
        }

        .logo-3d-border {
            height: 45px;
            width: 45px;
            border: 2px solid revert;
            border-radius: 20px;
            box-shadow: 0px 0px 0px rgba(0, 0, 0, 0.6);
            padding: 0px;
            background-color: white;
        }
    </style>
</head>
<body class="login-page" style="min-height: 405px;">
    <div class="login-box card card-outline card-primary" style="width: 33%;">
        <div class="login-logo mt-3">
            <%--<span class="form-avatar">
                <%--<img src="dist/img/tracking-icon.jpg" class="logo-3d-border" alt="AVATAR"/>--%>
            <%--</span>--%>
            <h3><b>Specimen Tracker</b></h3>

        </div>
        <!-- /.login-logo -->
        <div class="card-body login-card-body">
            <form id="form1" runat="server" method="post">
                <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-size: 17px; font-weight: bold;"
                    ForeColor="Red"></asp:Label>
                <div class="input-group mb-3">
                    <asp:TextBox ID="txtUserName" runat="server" class="form-control form-control-lg" placeholder="User ID"></asp:TextBox>
                    <div class="input-group-append">
                        <div class="input-group-text">
                            <span class="fa fa-user"></span>
                        </div>
                    </div>
                </div>
                <div class="input-group mb-3">
                    <asp:TextBox ID="txtPassword" runat="server" class="form-control form-control-lg passwordClass" TextMode="Password" placeholder="Password"></asp:TextBox>
                    <div class="input-group-append">
                        <div class="input-group-text" id="Divicon">
                            <span id="togglePassword" style="position: absolute; right: 35px; top: 50%; transform: translate(0px, -10px); cursor: pointer;">
                                <i class="fa fa-eye" id="iconShow"></i>
                                <i class="fa fa-eye-slash" id="iconHide" style="display: none;"></i>
                            </span><span class="fas fa-user"></span>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-9 captcha-container" style="margin-left: 10px;">
                        <asp:Image ID="imgCaptcha" CssClass="captcha-image" runat="server" />
                    </div>
                    <div class="col-2">
                        <asp:LinkButton ID="btnrefcaptcha" class="btn btn-info reload-btn" runat="server" OnClick="btnrefcaptcha_Click"><i class="fas ion-ios-refresh" style="margin-left:7px; margin-right:7px;" ></i></asp:LinkButton>
                    </div>
                </div>
                <br />
                <div class="input-group mb-3">
                    <asp:TextBox ID="txtCaptcha" runat="server" class="form-control-lg form-control" placeholder="Enter captcha"></asp:TextBox>
                    <div class="input-group-append">
                        <div class="input-group-text">
                        </div>
                    </div>
                </div>
                <div class="row">
                    <asp:Button ID="btnLogin" runat="server" class="btn btn-info btn-block btn-lg" Text="Log In" OnClick="btnLogin_Click"></asp:Button>
                </div>
                <div class="row" style="margin-top: 7px;">
                    <div class="col-8">
                    </div>
                    <!-- /.col -->
                    <div class="col-4">
                        <a href="Forgot_Password.aspx">Forgot password?</a>
                    </div>
                </div>
            </form>
        </div>
    </div>
    <script type="text/javascript" src="plugins/jquery/jquery.min.js"></script>
    
    <script type="text/javascript" src="plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
    
    <script type="text/javascript" src="dist/js/adminlte.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#togglePassword").hover(function () {
                var passwordInput = $("#txtPassword");
                var iconShow = $("#iconShow");
                var iconHide = $("#iconHide");

                if (passwordInput.attr("type") === "password") {
                    passwordInput.attr("type", "text");
                    iconShow.hide();
                    iconHide.show();
                } else {
                    passwordInput.attr("type", "password");
                    iconShow.show();
                    iconHide.hide();
                }
            });
        });
    </script>
</body>
</html>
