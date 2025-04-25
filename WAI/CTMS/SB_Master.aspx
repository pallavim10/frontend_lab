<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SB_Master.aspx.cs" Inherits="CTMS.SB_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">
                        Manage Visit Master
                    </h3>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-5">
                        <div class="box box-primary" style="min-height: 300px;">
                            <div class="box-header with-border" style="float: left;">
                                <h4 class="box-title" align="left">
                                    Manage Tasks
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Select Department:</label>
                                            </div>
                                            <div class="col-md-1 requiredSign">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDept"
                                                    ValidationGroup="Task" ForeColor="Red">*</asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:DropDownList Style="width: 200px;" ID="ddlDept" runat="server" AutoPostBack="true"
                                                    class="form-control drpControl" ValidationGroup="Task" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Select Task:</label>
                                            </div>
                                            <div class="col-md-1 requiredSign">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTask"
                                                    ValidationGroup="Task" ForeColor="Red">*</asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:DropDownList Style="width: 200px;" ID="ddlTask" ValidationGroup="Task" runat="server"
                                                    class="form-control drpControl" AutoPostBack="true" OnSelectedIndexChanged="ddlTask_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div style="width: 100%; height: 264px; overflow: auto;">
                                            <div>
                                                <asp:GridView ID="gvNewActs" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sub-Tasks">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblActID" runat="server" Visible="false" Text='<%# Bind("Sub_Task_ID") %>'></asp:Label>
                                                                <asp:Label ID="lblAct" runat="server" Text='<%# Bind("Sub_Task_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-1">
                        <div class="box-body">
                            <div style="min-height: 300px;">
                                <div class="row txtCenter">
                                    <asp:LinkButton ID="lbtnAddToVisit" ForeColor="White" Text="Add" runat="server" CssClass="btn btn-primary btn-sm"
                                        ValidationGroup="Visit" OnClick="lbtnAddToVisit_Click"></asp:LinkButton>
                                </div>
                                <div class="row txtCenter">
                                    &nbsp;
                                </div>
                                <div class="row txtCenter">
                                    <asp:LinkButton ID="lbtnRemoveFromVisit" ForeColor="White" Text="Remove" runat="server"
                                        ValidationGroup="Visit" CssClass="btn btn-primary btn-sm" OnClick="lbtnRemoveFromVisit_Click"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-primary" style="min-height: 300px;">
                            <div class="box-header with-border" style="float: left;">
                                <h4 class="box-title" align="left">
                                    Manage Visits
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Enter Visit Name:</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:TextBox Style="width: 200px;" ID="txtGrp" ValidationGroup="AddGrp" runat="server"
                                                    CssClass="form-control required"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Button ID="btnAddVisit" Text="Add" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave "
                                                    ValidationGroup="AddVisit" OnClick="btnAddVisit_Click" />
                                                <asp:Button ID="btnUpdateVisit" Text="Update" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                    ValidationGroup="AddVisit" OnClick="btnUpdateVisit_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Select Visit:</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:DropDownList Style="width: 200px;" ID="ddlVisit" runat="server" AutoPostBack="true"
                                                    ValidationGroup="Visit" class="form-control drpControl" OnSelectedIndexChanged="ddlVisit_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfv_Addto" runat="server" ControlToValidate="ddlVisit"
                                                    ValidationGroup="Visit" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:LinkButton ID="lbtnUpdateVisit" ValidationGroup="Visit" runat="server" ToolTip="Edit"
                                                    OnClick="lbtnUpdateVisit_Click"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;&nbsp;
                                                <asp:LinkButton ID="lbtnDeleteVisit" ValidationGroup="Visit" runat="server" ToolTip="Delete"
                                                    OnClick="lbtnDeleteVisit_Click"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div style="width: 100%; height: 264px; overflow: auto;">
                                            <div>
                                                <asp:GridView ID="gvAddedActs" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sub-Tasks">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblActID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                                <asp:Label ID="lblAct" runat="server" Text='<%# Bind("Sub_Task_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
