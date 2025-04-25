<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DM_RunRules.aspx.cs" Inherits="CTMS.DM_RunRules" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box box-info">
        <div class="box-header" runat="server" id="expandHeader">
            <h3 class="box-title">
                Run Rules</h3>
        </div>
        <asp:HiddenField ID="hdnRuleID" runat="server" />
        <asp:HiddenField ID="hdnConcatRuleID" runat="server" />
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <asp:Label ID="Label4" class="label" runat="server" Text="Select Site :"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <div class="control">
                                <asp:DropDownList ID="ddlInvid" class="form-control width300px" runat="server"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlInvid_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2" style="width: 14%;">
                            <asp:Label ID="Label5" class="label" runat="server" Text="Select Subject ID :"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <div class="control">
                                <asp:DropDownList ID="ddlSUBJID" class="form-control width300px" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlSUBJID_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <asp:Label ID="Label3" class="label" runat="server" Text="Select Visit :"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <div class="control">
                                <asp:DropDownList ID="ddlVisit" class="form-control width300px" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlVisit_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2" style="width: 14%;">
                            <asp:Label ID="Label1" class="label" runat="server" Text="Select Module :"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <div class="control">
                                <asp:DropDownList ID="ddlModule" class="form-control width300px" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlModule_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <asp:Label ID="Label2" class="label" runat="server" Text="Select Field :"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <div class="control">
                                <asp:DropDownList ID="ddlField" class="form-control width300px" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2" style="width: 14%;">
                            &nbsp;
                        </div>
                        <div class="col-md-4">
                            &nbsp;
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-2" style="width: 14%;">
                        </div>
                        <div class="col-md-4">
                            <asp:Button ID="btnRunRules" runat="server" Text="Run Rules" OnClick="btnRunRules_Click"
                                CssClass="btn btn-primary btn-sm cls-btnSave" />
                        </div>
                    </div>
                </div>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
