<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SET_SecurityQue.aspx.cs" Inherits="SpecimenTracking.SET_SecurityQue" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <!-- Tell the browser to be responsive to screen width -->
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title></title>
    <!-- Font Awesome -->
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
<body style="background-color:lightgray">
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
            <br />
            <div class="row">
                <div class="col-md-3">
                </div>
                <div class="col-md-6" style="background: #fff0d8; border-radius: 10px; box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);">
                    <h2 class="text-center"><b>Set Security Question
                    </b></h2>
                    <asp:Label ID="lblnote" runat="server" Text="Note :" Font-Size="Large" ForeColor="red" Font-Bold="true"></asp:Label>
                    <li>Don't use information like your birthdate, family members' names, or other publicly available details.</li>
                    <li>Avoid generic questions like "What is your mother's maiden name?" as these are commonly used and can be easily guessed or researched.</li>
                    <li>Choose questions that are unique to you, but not so obscure that you'll forget the answer.</li>
                    <br />
                    <br />
                    <div class="row">
                        <div class="col-md-5">
                            <label class="style13">Enter Security Question : </label>
                            <asp:Label ID="lblQue" runat="server" Font-Size="Small" ForeColor="#FF3300"
                                Text="*"></asp:Label>
                        </div>
                        <div class="col-md-7">
                            <asp:TextBox ID="txtQue" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqQue" ValidationGroup="Submit" runat="server"
                                ControlToValidate="txtQue" ErrorMessage="Required" Display="Dynamic" Style="font-family: Aharoni"
                                ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-5">
                            <label class="style13">Enter Security Answer :</label>
                            <asp:Label ID="lblAns" runat="server" Font-Size="Small" ForeColor="#FF3300"
                                Text="*"></asp:Label>
                        </div>
                        <div class="col-md-7">
                            <asp:TextBox ID="txtAns" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqAns" ValidationGroup="Submit" runat="server"
                                ControlToValidate="txtAns" ErrorMessage="Required" Display="Dynamic" Style="font-family: Aharoni"
                                ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <br />
                    <center>
                        <asp:Button ID="btnSubmit" runat="server" ValidationGroup="Submit" CssClass="btn btn-primary" Text="Submit"
                            OnClick="btnSubmit_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" Text="Back" OnClick="btnCancel_Click" />
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

