<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CTMS_Matrix.aspx.cs" Inherits="CTMS.CTMS_Matrix" %>

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
                        Manage Metrics
                    </h3>
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
                        <div class="col-md-5">
                            <div class="box box-primary" style="min-height: 300px;">
                                <div class="box-header with-border" style="float: left;">
                                    <h4 class="box-title" align="left">
                                        Add Groups
                                    </h4>
                                </div>
                                <div class="box-body">
                                    <div align="left" style="margin-left: 5px">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    <label>
                                                        Select Task:</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:DropDownList Style="width: 200px;" ID="ddlTask" runat="server" AutoPostBack="true"
                                                        class="form-control drpControl" OnSelectedIndexChanged="ddlTask_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <br />
                                        <br />
                                        <div class="row">
                                            <div style="width: 100%; height: 264px; overflow: auto;">
                                                <div>
                                                    <asp:GridView ID="gvNewSubTask" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                        Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sub-Task Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSubTaskID" runat="server" Visible="false" Text='<%# Bind("Sub_Task_ID") %>'></asp:Label>
                                                                    <asp:Label ID="lblSubTask" runat="server" Text='<%# Bind("Sub_Task_Name") %>'></asp:Label>
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
                                        <asp:LinkButton ID="lbtnAddToGrp" ForeColor="White" Text="Add" runat="server" CssClass="btn btn-primary btn-sm"
                                            ValidationGroup="Grp" OnClick="lbtnAddToGrp_Click" />
                                    </div>
                                    <div class="row txtCenter">
                                        &nbsp;
                                    </div>
                                    <div class="row txtCenter">
                                        <asp:LinkButton ID="lbtnRemoveFromGrp" ForeColor="White" Text="Remove" runat="server"
                                            ValidationGroup="Grp" CssClass="btn btn-primary btn-sm" OnClick="lbtnRemoveFromGrp_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="box box-primary" style="min-height: 300px;">
                                <div class="box-header with-border" style="float: left;">
                                    <h4 class="box-title" align="left">
                                        Manage Groups
                                    </h4>
                                </div>
                                <div class="box-body">
                                    <div align="left" style="margin-left: 5px">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    <label>
                                                        Group Name:</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:TextBox Style="width: 200px;" ID="txtGrp" ValidationGroup="AddGrp" runat="server"
                                                        CssClass="form-control required"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Button ID="btnAddGrp" Text="Add" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave "
                                                        ValidationGroup="AddGrp" OnClick="btnAddGrp_Click" />
                                                    <asp:Button ID="btnUpdateGrp" Text="Update" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                        ValidationGroup="AddGrp" OnClick="btnUpdateGrp_Click" />
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    <label>
                                                        Select Group:</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:DropDownList Style="width: 200px;" ID="ddlGroup" runat="server" AutoPostBack="true"
                                                        ValidationGroup="Grp" class="form-control drpControl" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfv_Addto" runat="server" ControlToValidate="ddlGroup"
                                                        ValidationGroup="Grp" InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:LinkButton ID="lbtnUpdateGrp" ValidationGroup="Grp" runat="server" ToolTip="Edit"
                                                        OnClick="lbtnUpdateGrp_Click"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;&nbsp;
                                                    <asp:LinkButton ID="lbtnDeleteGrp" ValidationGroup="Grp" runat="server" ToolTip="Delete"
                                                        OnClick="lbtnDeleteGrp_Click"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div style="width: 100%; height: 264px; overflow: auto;">
                                                <div>
                                                    <asp:GridView ID="gvAddedTasks" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                        Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sub-Task Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSubTaskID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                                    <asp:Label ID="lblSubTask" runat="server" Text='<%# Bind("Sub_Task_Name") %>'></asp:Label>
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
                <%--Matrix--%>
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-5">
                            <div class="box box-primary" style="min-height: 300px;">
                                <div class="box-header with-border" style="float: left;">
                                    <h4 class="box-title" align="left">
                                        Add Metrics
                                    </h4>
                                </div>
                                <div class="box-body">
                                    <div align="left" style="margin-left: 5px">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    <label>
                                                        Select Task:</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddl_Mat_PTask"
                                                        ValidationGroup="Mat" InitialValue="0"></asp:RequiredFieldValidator>
                                                    <asp:DropDownList Style="width: 200px;" ID="ddl_Mat_PTask" runat="server" AutoPostBack="true"
                                                        class="form-control drpControl" OnSelectedIndexChanged="ddl_Mat_PTask_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    <label>
                                                        Select Sub-Task:</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddl_Mat_PSubTask"
                                                        ValidationGroup="Mat" InitialValue="0"></asp:RequiredFieldValidator>
                                                    <asp:DropDownList Style="width: 200px;" ID="ddl_Mat_PSubTask" runat="server" class="form-control drpControl">
                                                    </asp:DropDownList>
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
                                                    <asp:Button ID="btnSubmitMat" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                                        ValidationGroup="Mat" OnClick="btnSubmitMat_Click" />
                                                    <asp:Button ID="btnUpdateMat" Text="Update" runat="server" CssClass="btn btn-primary btn-sm"
                                                        ValidationGroup="Mat" OnClick="btnUpdateMat_Click" />
                                                    <asp:Button ID="btnancelMat" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                        OnClick="btnancelMat_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-7">
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
                                                        <asp:GridView ID="gvMat" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                            Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="gvMat_RowCommand"
                                                            OnRowDataBound="gvMat_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sub-Tasks">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDept" runat="server" Text='<%# Bind("Sub_Task_Name") %>'></asp:Label>
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
                <%--Comparison--%>
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-5">
                            <div class="box box-primary" style="min-height: 300px;">
                                <div class="box-header with-border" style="float: left;">
                                    <h4 class="box-title" align="left">
                                        Add To Compare
                                    </h4>
                                </div>
                                <div class="box-body">
                                    <div align="left" style="margin-left: 5px">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    <label>
                                                        Select Task:</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddl_Comp_PTask"
                                                        ValidationGroup="Comp" InitialValue="0"></asp:RequiredFieldValidator>
                                                    <asp:DropDownList Style="width: 200px;" ID="ddl_Comp_PTask" runat="server" AutoPostBack="true"
                                                        class="form-control drpControl" OnSelectedIndexChanged="ddl_Comp_PTask_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    <label>
                                                        Select Sub-Task:</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddl_Comp_PSubTask"
                                                        ValidationGroup="Comp" InitialValue="0"></asp:RequiredFieldValidator>
                                                    <asp:DropDownList Style="width: 200px;" ID="ddl_Comp_PSubTask" runat="server" class="form-control drpControl">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row txt_center">
                                            <asp:Label runat="server" ID="lbl" Font-Bold="true" Text="(Compare With)"></asp:Label>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    <label>
                                                        Select Task:</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddl_Comp_CTask"
                                                        ValidationGroup="Comp" InitialValue="0"></asp:RequiredFieldValidator>
                                                    <asp:DropDownList Style="width: 200px;" ID="ddl_Comp_CTask" runat="server" AutoPostBack="true"
                                                        class="form-control drpControl" OnSelectedIndexChanged="ddl_Comp_CTask_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    <label>
                                                        Select Sub-Task:</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddl_Comp_CSubTask"
                                                        ValidationGroup="Comp" InitialValue="0"></asp:RequiredFieldValidator>
                                                    <asp:DropDownList Style="width: 200px;" ID="ddl_Comp_CSubTask" runat="server" class="form-control drpControl">
                                                    </asp:DropDownList>
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
                                                    <asp:Button ID="btnSubmitComp" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                                        ValidationGroup="Comp" OnClick="btnSubmitComp_Click" />
                                                    <asp:Button ID="btnUpdateComp" Text="Update" runat="server" CssClass="btn btn-primary btn-sm"
                                                        ValidationGroup="Comp" OnClick="btnUpdateComp_Click" />
                                                    <asp:Button ID="btnCancelComp" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                        OnClick="btnCancelComp_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-7">
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
                                                        <asp:GridView ID="gvComp" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                            Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="gvComp_RowCommand"
                                                            OnRowDataBound="gvComp_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sub-Tasks">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSubTask" runat="server" Text='<%# Bind("Sub_Task_Name") %>'></asp:Label>
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
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAddGrp" />
            <asp:PostBackTrigger ControlID="btnUpdateGrp" />
            <asp:PostBackTrigger ControlID="lbtnDeleteGrp" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
