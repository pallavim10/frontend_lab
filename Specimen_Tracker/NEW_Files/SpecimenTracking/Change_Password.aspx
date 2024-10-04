<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Change_Password.aspx.cs" Inherits="SpecimenTracking.Change_Password" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
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
            <br />
            <div class="row">
                <div class="col-md-3">
                </div>
                <div class="col-md-6" style="background: #fff0d8; border-radius: 10px; box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);">
                    <h2 class="text-center"><b>Change Password
                    </b></h2>
                    <asp:Label ID="lblnote" runat="server" Text="Note :" Font-Size="Large" ForeColor="red" Font-Bold="true"></asp:Label>
                    <li>Password Length must be at least 8 characters.</li>
                    <li>Password must be combination of uppercase and lowercase letters, numbers, and special characters (e.g., !,@,#).).</li>
                    <li>Do not use the last five passwords stored in system.</li>
                    <br />
                    <br />
                    <div class="row">
                        <div class="col-md-5">
                            <label class="style13">Old password : </label>
                            <asp:Label ID="Lbl_Old_PWD" runat="server" Font-Size="Small" ForeColor="#FF3300"
                                Text="*"></asp:Label>
                        </div>
                        <div class="col-md-7">
                            <asp:TextBox ID="txt_Old_Pwd" runat="server" CssClass="form-control"
                                TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="Req_Old_Pwd" ValidationGroup="Update" runat="server"
                                ControlToValidate="txt_Old_Pwd" ErrorMessage="Required" Display="Dynamic" Style="font-family: Aharoni"
                                ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="Com_New_Pwd" ValidationGroup="Update" runat="server"
                                ControlToCompare="txt_Old_Pwd" ControlToValidate="txt_New_Pwd"
                                ErrorMessage="New Password must be diffrent from Old Password" Display="Dynamic" Style="font-family: Aharoni"
                                ForeColor="Red" Operator="NotEqual"></asp:CompareValidator>
                            <asp:Label ID="lblOldPwd" Visible="false" runat="server" Text="Old password entered is invalid" Style="font-family: Aharoni" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-5">
                            <label class="style13">Create new password :</label>
                            <asp:Label ID="Lbl_New_PWD" runat="server" Font-Size="Small" ForeColor="#FF3300"
                                Text="*"></asp:Label>
                        </div>
                        <div class="col-md-7">
                            <asp:TextBox ID="txt_New_Pwd" runat="server" CssClass="form-control"
                                TextMode="Password" onkeypress="return AvoidSpace(event)"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="Req_New_Pwd" ValidationGroup="Update" runat="server"
                                ControlToValidate="txt_New_Pwd" ErrorMessage="Required" Display="Dynamic"
                                ForeColor="Red" Style="font-family: Aharoni"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="Com_Con_Pwd" ValidationGroup="Update" runat="server" Style="font-family: Aharoni"
                                ControlToCompare="txt_New_Pwd" ControlToValidate="txt_Con_Pwd"
                                ErrorMessage="New Password and Confirm Password Does Not Match" Display="Dynamic"
                                ForeColor="Red"></asp:CompareValidator>
                            <asp:RegularExpressionValidator ID="Regex2" runat="server" ControlToValidate="txt_New_Pwd" Style="font-family: Aharoni"
                                ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}" Display="Dynamic" ValidationGroup="Update"
                                ErrorMessage="Password must contain: Minimum 8 characters at least 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character" ForeColor="Red" />
                            <asp:Label ID="lblNewPwd" Visible="false" runat="server" Text="New password must be other than last five passwords" Style="font-family: Aharoni" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-5">
                            <label class="style13">Confirm new password :</label>
                            <asp:Label ID="Lbl_Confirm_PWD" runat="server" Font-Size="Small" ForeColor="#FF3300"
                                Text="*"></asp:Label>
                        </div>
                        <div class="col-md-7">
                            <asp:TextBox ID="txt_Con_Pwd" runat="server" CssClass="form-control"
                                TextMode="Password" onkeypress="return AvoidSpace(event)"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="Req_Con_Pwd" ValidationGroup="Update" runat="server"
                                ControlToValidate="txt_Con_Pwd" ErrorMessage="Required" Display="Dynamic" Style="font-family: Aharoni"
                                ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <br />
                    <center>
                        <asp:Button ID="btnSubmit" runat="server" ValidationGroup="Update" CssClass="btn btn-primary" Text="Submit"
                            OnClick="btnSubmit_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnback" runat="server" CssClass="btn btn-danger" Text="Back" OnClick="btnback_Click" />
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
