<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forgot_Password.aspx.cs" Inherits="SpecimenTracking.Forgot_Password" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <!-- Tell the browser to be responsive to screen width -->
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title></title>
    <!-- Font Awesome -->
    <link rel="stylesheet" href="plugins/fontawesome-free/css/all.min.css"/>
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css"/>
    <!-- icheck bootstrap -->
    <link rel="stylesheet" href="plugins/icheck-bootstrap/icheck-bootstrap.min.css"/>
    <!-- Theme style -->
    <link rel="stylesheet" href="dist/css/adminlte.min.css"/>
    <!-- Google Font: Source Sans Pro -->
    <link href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700" rel="stylesheet"/>
    <script src="dist/js/sweetalert.min.js"></script>
    <style>
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
            font-size: 22px;
        }
    </style>
    <script type="text/jscript">
        function AvoidSpace(event) {
            var k = event ? event.which : window.event.keyCode;
            if (k == 32) return false;
        }
    </script>
</head>
<body style="background-color: lightgray">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="container">
            <div class="row">
                <div class="col-md-3">
                </div>
                <div class="col-md-6">
                    <br />
                    <asp:Label ID="lblErrorMsg" runat="server"
                        Style="color: #CC3300; font-weight: 700; font-family: Arial, Helvetica, sans-serif; font-size: small;"></asp:Label>
                </div>
                <div class="col-md-3">
                </div>
            </div>
            <div class="row" style="margin-top: 15%;">
                <div class="col-md-3">
                </div>
                <div class="col-md-6" style="background: #fff0d8; border-radius: 10px; box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);">
                    <h2 class="text-center"><b>Forgot Password
                    </b></h2>
                    <br />
                    <asp:HiddenField runat="server" ID="hdnActivity" Value="EMAILID" />
                    <div class="row" runat="server" id="divEmailID">
                        <div class="col-md-5">
                            <label>Please Enter Email Id:</label>
                        </div>
                        <div class="col-md-7">
                            <asp:TextBox runat="server" ID="txtEmailID" CssClass="form-control" Width="80%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="Req_EmailID" ValidationGroup="Submit" runat="server"
                                ControlToValidate="txtEmailID" ErrorMessage="Required" Display="Dynamic"
                                Style="font-family: Aharoni" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="Regex_EmailID" runat="server" ControlToValidate="txtEmailID"
                                Style="font-family: Aharoni" ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                Display="Dynamic" ErrorMessage="Invalid Email ID" />
                        </div>
                        <br />
                    </div>
                    <div class="row" runat="server" id="divUsers" visible="false">
                        <div class="col-md-5">
                            <label>Please Select User:</label>
                        </div>
                        <div class="col-md-7">
                            <asp:DropDownList runat="server" ID="ddlUsers" CssClass="form-control" Width="80%"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="Req_Users" ValidationGroup="Submit" runat="server"
                                ControlToValidate="ddlUsers" ErrorMessage="Required" Display="Dynamic" InitialValue="0"
                                Style="font-family: Aharoni" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                        <br />
                    </div>
                    <div class="row" runat="server" id="divSecurity" visible="false">
                        <div class="row">
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-11">
                                <b>Note: Please answer below security question.
                                </b>
                            </div>
                        </div>
                        <br />
                        <div class="col-md-5">
                            <label>
                                <asp:Label runat="server" ID="lblSECURITY_QUE"></asp:Label>
                            </label>
                        </div>
                        <div class="col-md-7">
                            <asp:TextBox runat="server" ID="txtSECURITY_ANS" CssClass="form-control" Width="80%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqSECURITY_ANS" ValidationGroup="Submit" runat="server"
                                ControlToValidate="txtSECURITY_ANS" ErrorMessage="Required" Display="Dynamic"
                                Style="font-family: Aharoni" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                        <br />
                    </div>
                    <br />
                    <center>
                        <asp:Button ID="btnSubmit" runat="server" ValidationGroup="Submit" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnback" runat="server"  CssClass="btn btn-danger" Text="Back" OnClick="btnback_Click" />
                    </center>
                    <br />
                </div>
                <div class="col-md-3">
                </div>
            </div>
        </div>
    </form>
</body>
</html>
