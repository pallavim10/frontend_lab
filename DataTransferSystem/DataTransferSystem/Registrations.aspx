<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registrations.aspx.cs" Inherits="DataTransferSystem.Registrations" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="plugins/fontawesome-free/css/all.min.css">
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css">
    <!-- icheck bootstrap -->
    <link rel="stylesheet" href="plugins/icheck-bootstrap/icheck-bootstrap.min.css">
    <!-- Theme style -->
    <link rel="stylesheet" href="dist/css/adminlte.min.css">
    <!-- Google Font: Source Sans Pro -->
    <link href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700" rel="stylesheet">
    <script src="dist/js/sweetalert.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <section class="content">
            <div class="container">
                <div class="row">
                    <!-- left column -->
                    <div class="col-md-12">
                        <!-- jquery validation -->
                        <div class="card card-primary">
                            <div class="card-header">
                                <h3 class="card-title">Registration User</h3>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label>User Name </label>
                                            <asp:TextBox runat="server" ID="txtUserName" ReadOnly="true" CssClass="form-control required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label>Full Name </label>
                                            <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label>Email Address </label>
                                            <asp:TextBox runat="server" ID="txtEmailID" CssClass="form-control "></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmailID" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label>Contact No </label>
                                            <asp:TextBox runat="server" ID="txtContactNo" CssClass="form-control "></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label>Department</label>
                                            <asp:TextBox runat="server" ID="txtDepartment" CssClass="form-control "></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <!-- textarea -->
                                        <div class="form-group">
                                            <label>Designation </label>
                                            <asp:TextBox runat="server" ID="txtDesignation" CssClass="form-control "></asp:TextBox>
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
