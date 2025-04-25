<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="LabData_Default.aspx.cs" Inherits="CTMS.LabData_Default" %>

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
                        Default Data
                    </h3>
                </div>
            </div>
            <div class="box-body">
                <div class="form-group">
                    <div class="form-group has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-6">
                        <div class="box box-primary" style="min-height: 300px;">
                            <div class="box-header with-border" style="float: left;">
                                <h4 class="box-title" align="left">
                                    Add Items
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Enter Item Name:</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtItems"
                                                    ValidationGroup="Items" InitialValue="0"></asp:RequiredFieldValidator>
                                                <asp:TextBox Style="width: 200px;" ID="txtItems" runat="server" class="form-control drpControl"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                &nbsp;
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Button ID="btnSubmitItems" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                                    ValidationGroup="Items" OnClick="btnSubmitItems_Click" />
                                                <asp:Button ID="btnUpdateItems" Text="Update" runat="server" CssClass="btn btn-primary btn-sm"
                                                    ValidationGroup="Items" OnClick="btnUpdateMat_Click" />
                                                <asp:Button ID="btnancelItems" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                    OnClick="btnancelItems_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-primary">
                            <div class="box-header with-border" style="float: left;">
                                <h4 class="box-title" align="left">
                                    Records
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div>
                                        <div class="rows">
                                            <div style="width: 100%; height: 264px; overflow: auto;">
                                                <div>
                                                    <asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                        Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="gvItems_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Item Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItems" runat="server" Text='<%# Bind("ITEM") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnupdateMat" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                        CommandName="Edit1" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;&nbsp;
                                                                    <asp:LinkButton ID="lbtndeleteMat" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                        CommandName="Delete1" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-5">
                        <div class="box box-primary" style="min-height: 300px;">
                            <div class="box-header with-border" style="float: left;">
                                <h4 class="box-title" align="left">
                                    Add Tests
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div class="row">
                                        <div style="width: 100%; height: 264px; overflow: auto;">
                                            <div>
                                                <asp:GridView ID="gvNewItems" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Items Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblItemsID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                                <asp:Label ID="lblItems" runat="server" Text='<%# Bind("ITEM") %>'></asp:Label>
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
                                    <asp:LinkButton ID="lbtnAddToTest" ForeColor="White" Text="Add" runat="server" CssClass="btn btn-primary btn-sm"
                                        ValidationGroup="Test" OnClick="lbtnAddToTest_Click" />
                                </div>
                                <div class="row txtCenter">
                                    &nbsp;
                                </div>
                                <div class="row txtCenter">
                                    <asp:LinkButton ID="lbtnRemoveFromTest" ForeColor="White" Text="Remove" runat="server"
                                        ValidationGroup="Test" CssClass="btn btn-primary btn-sm" OnClick="lbtnRemoveFromTest_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-primary" style="min-height: 300px;">
                            <div class="box-header with-border" style="float: left;">
                                <h4 class="box-title" align="left">
                                    Manage Tests
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Test Name:</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:TextBox Style="width: 200px;" ID="txtGrp" ValidationGroup="AddGrp" runat="server"
                                                    CssClass="form-control required"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Button ID="btnAddTest" Text="Add" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave "
                                                    ValidationGroup="AddTest" OnClick="btnAddTest_Click" />
                                                <asp:Button ID="btnUpdateTest" Text="Update" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                    ValidationGroup="AddTest" OnClick="btnUpdateTest_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Select Test:</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:DropDownList Style="width: 200px;" ID="ddlGroup" runat="server" AutoPostBack="true"
                                                    ValidationGroup="Test" class="form-control drpControl" OnSelectedIndexChanged="ddlTest_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfv_Addto" runat="server" ControlToValidate="ddlGroup"
                                                    ValidationGroup="Test" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:LinkButton ID="lbtnUpdateTest" ValidationGroup="Test" runat="server" ToolTip="Edit"
                                                    OnClick="lbtnUpdateTest_Click"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;&nbsp;
                                                <asp:LinkButton ID="lbtnDeleteTest" ValidationGroup="Test" runat="server" ToolTip="Delete"
                                                    OnClick="lbtnDeleteTest_Click"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div style="width: 100%; overflow: auto;">
                                            <div>
                                                <asp:GridView ID="gvAddedItems" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Items Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblItemsID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                                <asp:Label ID="lblSubTask" runat="server" Text='<%# Bind("ITEM") %>'></asp:Label>
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
