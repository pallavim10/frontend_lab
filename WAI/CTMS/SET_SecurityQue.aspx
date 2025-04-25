<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SET_SecurityQue.aspx.cs" Inherits="CTMS.SET_SecurityQue" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
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
            <div class="row" style="margin-top: 10%;">
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
                    <div class="row text-center">
                        <asp:Button ID="btnSubmit" runat="server" ValidationGroup="Submit" CssClass="btn btn-primary" Text="Submit"
                            OnClick="btnSubmit_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" Text="Back" OnClick="btnCancel_Click" />
                    </div>
                    <br />
                </div>
                <div class="col-md-3">
                </div>
            </div>
        </div>
    </form>
</body>
</html>
