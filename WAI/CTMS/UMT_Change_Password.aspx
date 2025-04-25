<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UMT_Change_Password.aspx.cs" Inherits="CTMS.UMT_Change_Password" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />

    <style type="text/css">
        .style1 {
            width: 100%;
            font-family: Arial;
            font-size: x-small;
        }

        .style2 {
            text-align: center;
            font-weight: bold;
        }

        .style5 {
            width: 100%;
        }

        .style11 {
            width: 9px;
        }

        .style12 {
            width: 205px;
        }

        .style13 {
            width: 134px;
            font-weight: 700;
        }

        .style14 {
            width: 435px;
            text-align: center;
        }

        .style15 {
            width: 314px;
        }

        .style16 {
            width: 312px;
        }

        .style17 {
            width: 250px;
            height: 200px;
        }

        .style18 {
            width: 100%;
            //font-family: Arial;
            font-size: small;
            font-weight: bold;
        }
    </style>
    <script type="text/jscript">
        function AvoidSpace(event) {
            var k = event ? event.which : window.event.keyCode;
            if (k == 32) return false;
        }
    </script>


</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-md-4">
                </div>
                <div class="col-md-4">
                    <img alt="" class="style17" src="image/DS%20259%20x%20235.jpg" style="margin-left: 56px; margin-top: 40px;" />

                    <br />
                    <asp:Label ID="lblErrorMsg" runat="server"
                        Style="color: #CC3300; font-weight: 700; font-family: Arial, Helvetica, sans-serif; font-size: small;"></asp:Label>
                </div>
                <div class="col-md-4">
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-4">
                </div>
                <div class="col-md-4" style="width: 50%;">
                    <asp:Label ID="lblnote" runat="server" Text="Note :" ForeColor="red" Font-Bold="true"></asp:Label>
                    <h6 style="font-weight: bold;">Password Length must be at least 8 characters.</h6>
                    <h6 style="font-weight: bold;">Password must be combination of alpha, numeric and symbolic characters.</h6>
                    <h6 style="font-weight: bold;">Do not use the last five passwords stored in system.</h6>
                </div>
                <div class="col-md-4">
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-4">
                    &nbsp  &nbsp
                </div>
                <div class="col-md-4 ">
                    <div class="style19">
                        <lable class="style13" style="margin-right: 20px;">Enter Old Password : </lable>
                        <asp:Label ID="Lbl_Old_PWD" runat="server" Font-Size="Small" ForeColor="#FF3300"
                            Text="*"></asp:Label>
                        <asp:TextBox ID="txt_Old_Pwd" runat="server" Font-Size="X-Small"
                            TextMode="Password" Width="155px"></asp:TextBox>

                    </div>
                    <div class="style19">
                        <lable class="style13" style="margin-right: 14px;">Enter New Password :</lable>
                        <asp:Label ID="Lbl_New_PWD" runat="server" Font-Size="Small" ForeColor="#FF3300"
                            Text="*"></asp:Label>
                        <asp:TextBox ID="txt_New_Pwd" runat="server" Font-Size="X-Small"
                            TextMode="Password" Width="155px" onkeypress="return AvoidSpace(event)"></asp:TextBox>


                    </div>
                    <div class="style19">
                        <lable class="style13" style="margin-right: 28px;">Confirm Password :</lable>
                        <asp:Label ID="Lbl_Confirm_PWD" runat="server" Font-Size="Small" ForeColor="#FF3300"
                            Text="*"></asp:Label>
                        <asp:TextBox ID="txt_Con_Pwd" runat="server" CssClass="style19" Font-Size="X-Small"
                            TextMode="Password" Width="155px" onkeypress="return AvoidSpace(event)"></asp:TextBox>
                    </div>
                    <div class="style19">
                        <lable class="style13" style="margin-right: 32px;">Security Question :</lable>
                        <asp:Label ID="LblSecurityQuestion" runat="server" Font-Size="Small" ForeColor="#FF3300"
                            Text="*"></asp:Label>
                        <asp:TextBox ID="TxtSecurityQuestion" runat="server" CssClass="style19" Font-Size="X-Small"
                             Width="155px" onkeypress="return AvoidSpace(event)"></asp:TextBox>
                    </div>
                    <div class="style19">
                        <lable class="style13" style="margin-right: 42px;"> Security Answer :</lable>
                        <asp:Label ID="LblSecurityAnswer" runat="server" Font-Size="Small" ForeColor="#FF3300"
                            Text="*"></asp:Label>
                        <asp:TextBox ID="TxtSecurityAnswer" runat="server" CssClass="style19" Font-Size="X-Small"
                             Width="155px" onkeypress="return AvoidSpace(event)"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <asp:RequiredFieldValidator ID="Req_Old_Pwd" ValidationGroup="Update" runat="server"
                        ControlToValidate="txt_Old_Pwd" ErrorMessage="Required" Font-Size="X-Small"
                        ForeColor="Red" Style="font-family: Aharoni;"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="Com_New_Pwd" ValidationGroup="Update" runat="server"
                        ControlToCompare="txt_Old_Pwd" ControlToValidate="txt_New_Pwd"
                        ErrorMessage="New Password must be diffrent from Old Password"
                        Font-Size="X-Small" ForeColor="Red" Operator="NotEqual"
                        Style="font-weight: 700;"></asp:CompareValidator>
                    <br />
                    <asp:RequiredFieldValidator ID="Req_New_Pwd" ValidationGroup="Update" runat="server"
                        ControlToValidate="txt_New_Pwd" ErrorMessage="Required" Font-Size="X-Small"
                        ForeColor="Red" Style="font-family: Aharoni"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="Com_Con_Pwd" ValidationGroup="Update" runat="server"
                        ControlToCompare="txt_New_Pwd" ControlToValidate="txt_Con_Pwd"
                        ErrorMessage="New Password and Confirm Password Does Not Match"
                        Font-Size="X-Small" ForeColor="Red" Style="font-weight: 700;"></asp:CompareValidator>
                    <br />
                    <asp:RequiredFieldValidator ID="Req_Con_Pwd" ValidationGroup="Update" runat="server"
                        ControlToValidate="txt_Con_Pwd" ErrorMessage="Required" Font-Size="X-Small"
                        ForeColor="Red" Style="font-family: Aharoni;"></asp:RequiredFieldValidator>
                </div>
            </div>
            <br />
            <center style="margin-right: -91px;">
                <asp:Button ID="Btn_Update" runat="server" ValidationGroup="Update" Font-Size="Small" Text="Submit" CssClass="btn-group-sm"
                     />
                <asp:Button ID="btnback" runat="server" Font-Size="Small" Text="Back" />
            </center>

            <br />
            <div class="row">

                <div class="col-md-12" style="padding-top: 100px;">
                    <%--<h5 class="style2" style="width:100%;">Powered by DiagnoSearch Life Sciences Pvt. Ltd</h5>--%>
                    <a class="style2" style="width: 100%; margin-left: 393px;">Powered by DiagnoSearch Life Sciences Pvt. Ltd.</a>
                </div>
            </div>
        </div>




    </form>
</body>
</html>
