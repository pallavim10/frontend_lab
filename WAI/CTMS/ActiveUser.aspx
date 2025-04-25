<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ActiveUser.aspx.cs" Inherits="CTMS.ActiveUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">

                Activate User</h3>
        </div>
   
        <!-- /.box-header -->
        <!-- text input -->
        <div class="box-body">
            <div class="row">
                <div class="lblError">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="lblError">
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </div>
                </div>
                <asp:UpdatePanel runat="server" ID="UpdatePanel">
                    <ContentTemplate>
                        <div class="box box-primary">
                            <div class="box-header">
                            </div>
                            <div class="box-body">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="label col-md-2" id="proj" runat="server">
                                                Select Project: &nbsp;
                                                <asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                            </div>
                                            <div class="col-md-3" id="projname" runat="server">
                                                <asp:DropDownList ID="Drp_Project_Name" class="form-control drpControl required width200px"
                                                    runat="server" AutoPostBack="True" OnSelectedIndexChanged="Drp_Project_Name_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="label col-md-2">
                                                Select User Group:&nbsp;
                                                <asp:Label ID="Label6" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:DropDownList ID="Drp_User_Group" class="form-control drpControl required width200px"
                                                    runat="server" AutoPostBack="True" OnSelectedIndexChanged="Drp_User_Group_SelectedIndexChanged"
                                                    Style="margin-bottom: 1px">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="label col-md-2">
                                                Select User: &nbsp;
                                                <asp:Label ID="Label8" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:DropDownList ID="Drp_User" class="form-control drpControl required width200px"
                                                    runat="server" AutoPostBack="True" OnSelectedIndexChanged="Drp_User_SelectedIndexChanged"
                                                    Style="margin-bottom: 1px">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="label col-md-2">
                                                Select User Name: &nbsp;
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txt_User_Name" runat="server" class="form-control required width200px"
                                                    ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="label col-md-2">
                                                Email ID :&nbsp;
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txt_EmailID" runat="server" class="form-control required width200px"
                                                    ReadOnly="true"></asp:TextBox>
                                            </div>
                                            <div class="label col-md-2">
                                                <asp:Button ID="Btn_Update" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                    Style="margin-left: 10px" OnClick="Btn_Update_Click" Text="Active" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hnfUserName" runat="server" />
            <asp:HiddenField ID="hnfEmailID" runat="server" />
            <asp:HiddenField ID="hnfUserType" runat="server" />
            <asp:HiddenField ID="hnfUserDispName" runat="server" />
            <asp:HiddenField ID="hnfSiteID" runat="server" />
            <asp:HiddenField ID="hnfUserID" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
