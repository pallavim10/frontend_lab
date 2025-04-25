<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Comm_MailBox_Config.aspx.cs" Inherits="CTMS.Comm_MailBox_Config" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Mailbox Configuration</h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <asp:Label ID="Label7" class="label" runat="server" Text="Username :"></asp:Label>
                        </div>
                        <div class="col-md-6">
                            <div class="control">
                                <asp:TextBox runat="server" ID="txtMailUsername" CssClass="form-control required width300px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <asp:Label ID="Label1" class="label" runat="server" Text="Password :"></asp:Label>
                        </div>
                        <div class="col-md-6">
                            <div class="control">
                                <asp:TextBox runat="server" ID="txtMailPassword" TextMode="Password" CssClass="form-control required width300px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <%--<div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="col-md-3">
                                        <asp:Label ID="Label2" runat="server" Text="Note: You will get a Test Mail as Mailbox is configured."></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />--%>
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-6">
                            <div class="col-md-3">
                                &nbsp;
                            </div>
                            <div class="col-md-3">
                                <div class="control">
                                    <asp:LinkButton ID="lbtnsubmit" Text="Submit" runat="server" Style="color: white;"
                                        CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnsubmit_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
