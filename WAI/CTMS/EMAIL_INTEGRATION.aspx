<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="EMAIL_INTEGRATION.aspx.cs" Inherits="CTMS.EMAIL_INTEGRATION" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .highlight-red {
            border: 1px solid red;
        }
    </style>
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                stateSave: true
            });
        }
    </script>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box-body">
        <div class="form-group has-warning">
            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary" style="min-height: 300px;">
                    <div class="box-header with-border" style="float: left;">
                        <h3 class="box-title" align="left">Mailbox Configuration
                        </h3>
                    </div>
                    <div id="Div3" class="pull-right" runat="server">
                        <asp:LinkButton runat="server" ID="lbtnExport" OnClick="lbtnExport_Click" CssClass="btn btn-info" Style="color: white; margin-top: 3px;">
                                            Export Configuration &nbsp;&nbsp; <span class="glyphicon glyphicon-download 2x"></span>
                        </asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    </div>
                    <div class="box-body">
                        <div align="left" style="margin-left: 5px">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <label>
                                            Display name:
                                        </label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:TextBox ID="txtDisplayName" Style="width: 100%;" runat="server"
                                            Text="" CssClass="form-control required"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <label>
                                            SMTP Server IP Address / Host Name:
                                        </label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:TextBox ID="txtHostName" Style="width: 100%;" runat="server"
                                            Text="" CssClass="form-control required"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12" style="margin-top: 5px;">
                                    <div class="col-md-3">
                                        <label>
                                            Port Number.:</label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:TextBox ID="txtPortNo" Style="width: 100%;" runat="server"
                                            Text="" CssClass="form-control required"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12" style="margin-top: 5px;">
                                    <div class="col-md-3">
                                        <label>
                                            SSL Enable :</label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:CheckBox ID="chkSSL" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12" style="margin-top: 5px;">
                                    <div class="col-md-3">
                                        <label>
                                            User Name :</label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:TextBox ID="txtUserName" Style="width: 100%;" runat="server"
                                            Text="" CssClass="form-control required"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12" style="margin-top: 5px;">
                                    <div class="col-md-3">
                                        <label>
                                            Password :</label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:TextBox ID="txtPassword" Style="width: 100%;" runat="server" TextMode="Password"
                                            CssClass="form-control required"></asp:TextBox>
                                        
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="col-md-12" align="center" style="margin-top: 5px; margin-left: -36px;">
                                <asp:Button ID="btnSubmit" runat="server" OnClientClick="return validateForm();" CssClass="btn btn-primary btn-sm cls-btnSave"
                                    OnClick="btnSubmit_Click" Text="Submit" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />
    </div>
</asp:Content>
