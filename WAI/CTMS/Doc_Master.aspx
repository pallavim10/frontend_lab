<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Doc_Master.aspx.cs" Inherits="CTMS.Doc_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Document Master</h3>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-6">
                            <div class="box box-primary" style="min-height: 300px;">
                                <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                    <h4 class="box-title" align="left">
                                        Add Document
                                    </h4>
                                </div>
                                <div class="box-body">
                                    <div align="left" style="margin-left: 5px">
                                        <div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            Enter Document Name:</label>
                                                    </div>
                                                    <div class="col-md-1 requiredSign">
                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPlan" ID="reqDept"
                                                            ValidationGroup="Plan" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="col-md-7">
                                                        <asp:TextBox Style="width: 200px;" ID="txtPlan" ValidationGroup="Plan" runat="server"
                                                            CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-12" style="margin-top: 5px;">
                                                    <div class="col-md-4">
                                                        <label>
                                                            Enter Sequence Number:</label>
                                                    </div>
                                                    <div class="col-md-1 requiredSign">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSeq"
                                                            ValidationGroup="Plan" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="col-md-7">
                                                        <asp:TextBox Width="50px" ID="txtSeq" ValidationGroup="Plan" runat="server" CssClass="form-control"
                                                            MaxLength="4"></asp:TextBox>
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
                                                        <asp:Button ID="btnSubmitPlan" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                                            ValidationGroup="Plan" OnClick="btnSubmitPlan_Click" />
                                                        <asp:Button ID="btnupdatePlan" Text="Update" runat="server" CssClass="btn btn-primary btn-sm"
                                                            ValidationGroup="Plan" OnClick="btnupdatePlan_Click" />
                                                        <asp:Button ID="btncancelPlan" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                            OnClick="btncancelPlan_Click" />
                                                    </div>
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
                                                        <asp:GridView ID="gvPlan" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                            Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="gvPlan_RowCommand"
                                                            OnRowDataBound="gvPlan_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Seq. No." ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSEQNO" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Document Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPlan" runat="server" Text='<%# Bind("DocName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtnupdatePlan" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                            CommandName="Edit1" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;&nbsp;
                                                                        <asp:LinkButton ID="lbtndeletePlan" runat="server" CommandArgument='<%# Bind("ID") %>'
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
                        <div class="col-md-6">
                            <div class="box box-primary" style="min-height: 300px;">
                                <div class="box-header with-border" style="float: left;">
                                    <h4 class="box-title" align="left">
                                        Add Section
                                    </h4>
                                </div>
                                <div class="box-body">
                                    <div align="left" style="margin-left: 5px">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    <label>
                                                        Select Document:</label>
                                                </div>
                                                <div class="col-md-1 requiredSign">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlPlan"
                                                        ValidationGroup="Section" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:DropDownList Style="width: 200px;" ID="ddlPlan" runat="server" AutoPostBack="true"
                                                        class="form-control drpControl" ValidationGroup="Section" OnSelectedIndexChanged="ddlPlan_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="margin-top: 5px;">
                                                <div class="col-md-4">
                                                    <label>
                                                        Enter Section Name:</label>
                                                </div>
                                                <div class="col-md-1 requiredSign">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSection"
                                                        ValidationGroup="Section" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtSection" Style="width: 200px;" ValidationGroup="Section" runat="server"
                                                        Text="" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="margin-top: 5px;">
                                                <div class="col-md-4">
                                                    <label>
                                                        Enter Sequence Number:</label>
                                                </div>
                                                <div class="col-md-1 requiredSign">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtSubSeq"
                                                        ValidationGroup="Section" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:TextBox Width="50px" ID="txtSubSeq" ValidationGroup="Section" runat="server"
                                                        CssClass="form-control" MaxLength="4"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <br />
                                            <div class="col-md-12">
                                                <div class="col-md-6">
                                                    &nbsp;
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Button ID="btnsubmitSection" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                                        ValidationGroup="Section" OnClick="btnsubmitSection_Click" />
                                                    <asp:Button ID="btnupdateSection" Text="Update" runat="server" CssClass="btn btn-primary btn-sm"
                                                        ValidationGroup="Section" OnClick="btnupdateSection_Click" />
                                                    <asp:Button ID="btncancelSection" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                        OnClick="btncancelSection_Click" />
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
                                                        <asp:GridView ID="gvSection" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                            Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="gvSection_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="SEQ. No." ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSEQNO" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Section Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTask" runat="server" ToolTip='<%# Bind("SectionName") %>' Text='<%# Bind("SectionName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtnupdateSection" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                            CommandName="Edit1" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;&nbsp;
                                                                        <asp:LinkButton ID="lbtndeleteSection" runat="server" CommandArgument='<%# Bind("ID") %>'
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
