<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LockScreen.aspx.cs" Inherits="SpecimenTracking.LockScreen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <!-- Tell the browser to be responsive to screen width -->
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title></title>
    <!-- Font Awesome -->
    <link rel="stylesheet" href="plugins/fontawesome-free/css/all.min.css">
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css">
    <!-- Theme style -->
    <link rel="stylesheet" href="dist/css/adminlte.min.css">
    <!-- Google Font: Source Sans Pro -->
    <link href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700" rel="stylesheet">
    <script src="dist/js/sweetalert.min.js"></script>

</head>
<body class="hold-transition lockscreen">
    <!-- Automatic element centering -->
    <div class="lockscreen-wrapper">
        <div class="lockscreen-logo">
            <b>Login</b>
        </div>
        <div>
            <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-size: 17px; font-weight: bold;"
                ForeColor="Red"></asp:Label>
        </div>
        <!-- User name -->

        <div class="lockscreen-name" style="text-align-last: center">
            <asp:Label ID="lblFullName" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
        </div>
        <div>
            <asp:Label ID="lblUserName" runat="server" Visible="false" ForeColor="Black" Font-Bold="true"></asp:Label>
        </div>

        <!-- START LOCK SCREEN ITEM -->
        <div class="lockscreen-item">
            <!-- lockscreen image -->
            <div class="lockscreen-image">
                <img src="dist/img/avatar5.png" style="height: 75px;" class="img-circle elevation-2" alt="User Image">
            </div>
            <!-- /.lockscreen-image -->

            <!-- lockscreen credentials (contains the form) -->
            <form class="lockscreen-credentials" runat="server">
                <div class="input-group">
                    <asp:TextBox ID="txtPassword" runat="server" class="form-control form-control-lg" TextMode="Password" placeholder="Password"></asp:TextBox>

                    <div class="input-group-append">
                        <span id="togglePassword" style="position: absolute; right: 40px; top: 50%; transform: translate(0px, -10px); cursor: pointer;">
                            <i class="fa fa-eye" id="iconShow"></i>
                            <i class="fa fa-eye-slash" id="iconHide" style="display: none;"></i></span>
                        <asp:LinkButton ID="btnLogin" Style="align-content: center; background-color: yellowgreen" runat="server" class="btn" OnClick="btnLogin_Click"><i class="fas fa-sign-in-alt" style="font-size:15px"></i></asp:LinkButton>
                    </div>
                </div>
                <br />
                <div>
                    <asp:LinkButton ID="lbtnLoginAnuser" runat="server" Text="Login with another user?" ForeColor="Blue" OnClick="lbtnLoginAnuser_Click"></asp:LinkButton>
                </div>
                <br />
            </form>
        </div>
    </div>
    <script src="plugins/jquery/jquery.min.js"></script>
    <!-- Bootstrap 4 -->
    <script src="plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script>
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
    
    <script type="text/javascript">
        document.addEventListener("DOMContentLoaded", function () {
            var loginButton = document.getElementById('<%= btnLogin.ClientID %>');

            document.addEventListener('keypress', function (e) {
                if (e.key === 'Enter') {
                    loginButton.click();
                }
            });
        });
    </script>

</body>
</html>
