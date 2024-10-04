<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EMAIL_INTEGRATION.aspx.cs" Inherits="SpecimenTracking.EMAIL_INTEGRATION" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">Mailbox Configuration</h1>
                    </div>
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home </a></li>
                            <li class="breadcrumb-item active">UMT Setup</li>
                            <li class="breadcrumb-item active">Mailbox Configuration</li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
        <section class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Mailbox Configuration</h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="lbtnExport" runat="server" Font-Size="14px" Style="margin-top: 3px;" CssClass="btn btn-default" OnClick="lbtnExport_Click" ForeColor="Black">Export Configuration&nbsp;<span class="fas fa-download btn-xs"></span></asp:LinkButton>
                                    &nbsp;&nbsp;                       
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-3">
                                        <label>
                                            Display name:
                                        </label>
                                    </div>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="txtDisplayName" Style="width: 100%;" runat="server"
                                            Text="" CssClass="form-control required"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-3">
                                        <label>
                                            SMTP Server IP Address / Host Name:
                                        </label>
                                    </div>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="txtHostName" Style="width: 100%;" runat="server"
                                            Text="" CssClass="form-control required"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-3">
                                        <label>
                                            Port Number.:</label>
                                    </div>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="txtPortNo" Style="width: 100%;" runat="server"
                                            Text="" CssClass="form-control required"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-3">
                                        <label>
                                            SSL Enable :</label>
                                    </div>
                                    <div class="col-md-9">
                                        <asp:CheckBox ID="chkSSL" runat="server" />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-3">
                                        <label>
                                            User Name :</label>
                                    </div>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="txtUserName" Style="width: 100%;" runat="server"
                                            Text="" CssClass="form-control required"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-3">
                                        <label>
                                            Password :</label>
                                    </div>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="txtPassword" Style="width: 100%;" runat="server" TextMode="Password"
                                            CssClass="form-control required"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <center>
                                    <asp:Button ID="btnSubmit" runat="server" OnClientClick="return validateForm();" CssClass="btn btn-primary btn-sm cls-btnSave"
                                        OnClick="btnSubmit_Click" Text="Submit" />
                                </center>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </section>
    </div>

    <script type="text/javascript">

        function validateForm() {
            var inputValue = document.getElementById('<%= txtPassword.ClientID %>').value;

            if (inputValue.trim() === "") {

                document.getElementById('<%= txtPassword.ClientID %>').style.borderColor = "red";
                return false;
            }
            return true;
        }
    </script>
    <style>
        .highlight-red {
            border: 1px solid red;
        }
    </style>
</asp:Content>


