<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SessionExpired.aspx.cs" Inherits="SpecimenTracking.SessionExpired" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Session Expired</title>
    <link rel="stylesheet" href="../../plugins/fontawesome-free/css/all.min.css"/>
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css"/>
    <!-- icheck bootstrap -->
    <link rel="stylesheet" href="../../plugins/icheck-bootstrap/icheck-bootstrap.min.css"/>
    <!-- Theme style -->
    <link rel="stylesheet" href="../../dist/css/adminlte.min.css"/>
    <!-- Google Font: Source Sans Pro -->
    <link href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700" rel="stylesheet"/>
    <script src="dist/js/sweetalert.min.js"></script>
    <script>
        function showSessionExpiredAlert() {
            swal({
                title: "Session Expired",
                text: "Your session has expired. Please Log in again.",
                icon: "warning",
                button: "OK"
            }).then((value) => {
                window.location.href = 'LoginPage.aspx';
            });
        }

        window.onload = function() {
            showSessionExpiredAlert();
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <noscript>
                <h1>Session Expired</h1>
                <p>Your session has expired. Please <a href="LoginPage.aspx"><b>Log in</b></a> again.</p>
            </noscript>
        </div>
    </form>
</body>
</html>
