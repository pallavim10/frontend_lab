<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterSuperUser.aspx.cs" Inherits="SpecimenTracking.RegisterSuperUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head runat="server">
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
    <script src="https://code.jquery.com/jquery-3.7.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.numeric').on('keypress', function (e) {
                var keyCode = e.which ? e.which : e.keyCode;

                if (!(keyCode >= 48 && keyCode <= 57) && keyCode !== 8 && keyCode !== 46 &&
                    keyCode !== 37 && keyCode !== 39 && keyCode !== 9 && keyCode !== 13) {
                    e.preventDefault();
                }
            });
        });
    </script>
    <style type="text/css">
        .hide {
            display: none;
        }


        .brd-1px-redimp {
            border: solid 1px !important;
            border-color: Red !important;
        }

        .brd-1px-maroonimp {
            border: solid !important;
            border-color: Maroon !important;
        }
    </style>
    <script type="text/javascript">
        $(".numeric").on("keypress keyup blur", function (event) {
            $(this).val($(this).val().replace(/[^\d].+/, ""));
            if ((event.which < 48 || event.which > 57)) {
                event.preventDefault();
            }
        });

        $('.numeric').keypress(function (event) {

            if (event.keyCode == 8 || event.keyCode == 9 || event.charCode == 48 || event.charCode == 49 || event.charCode == 50 || event.charCode == 51
                || event.charCode == 52 || event.charCode == 52 || event.charCode == 53 || event.charCode == 54 || event.charCode == 55 || event.charCode == 56 || event.charCode == 57) {
                // let it happen, don't do anything
                return true;
            }
            else {
                event.preventDefault();
            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <section class="content">
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card card-primary">
                            <div class="card-header">
                                <h3 class="card-title">Register Super User</h3>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label>Sponsor Name :</label>
                                            <asp:Label ID="Label5" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                            <asp:TextBox runat="server" ID="txtSponsor" CssClass="form-control required"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label>Project Name :</label>
                                            <asp:Label ID="lblPROJECT" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                            <asp:TextBox runat="server" ID="txtPROJECT" CssClass="form-control required"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label>First Name :</label>
                                            <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                            <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label>Last Name :</label>
                                            <asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                            <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control required"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label>Email Address :</label>
                                            <asp:Label ID="Label3" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                            <asp:TextBox runat="server" ID="txtEmailID" CssClass="form-control required"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmailID" ForeColor="Red" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label>Contact Number :</label>
                                            <asp:Label ID="Label4" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                            <asp:TextBox runat="server" ID="txtContactNo" CssClass="form-control numeric" MaxLength="10"></asp:TextBox>
                                            <asp:RegularExpressionValidator
                                                runat="server"
                                                ID="revContactNo"
                                                ControlToValidate="txtContactNo"
                                                ErrorMessage="Invalid Contact Number."
                                                ValidationExpression="^[0-9]{10}$"
                                                ForeColor="Red" />
                                        </div>
                                    </div>
                                </div>
                                <center>
                                    <asp:LinkButton runat="server" ID="lbtnSubmit" Text="Submit" Style="width: 93px;" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnSubmit_Click"></asp:LinkButton>
                                </center>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </form>
</body>
</html>
