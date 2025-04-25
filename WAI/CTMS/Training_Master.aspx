<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Training_Master.aspx.cs" Inherits="CTMS.Training_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Manage Training Masters
            </h3>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="box-body">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-5">
                                    <div class="box box-primary" style="min-height: 300px;">
                                        <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                            <h4 class="box-title" align="left">
                                                Add Section
                                            </h4>
                                        </div>
                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="col-md-4">
                                                                <label>
                                                                    Enter Section Name:</label>
                                                            </div>
                                                            <div class="col-md-1 requiredSign">
                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtSec" ID="reqSec"
                                                                    ValidationGroup="Sec" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="col-md-7">
                                                                <asp:TextBox Style="width: 200px;" ID="txtSec" ValidationGroup="Sec" runat="server"
                                                                    CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="col-md-4">
                                                                <label>
                                                                    Attach Documents:</label>
                                                            </div>
                                                            <div class="col-md-1 requiredSign">
                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtSec" ID="RequiredFieldValidator3"
                                                                    ValidationGroup="Sec" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="col-md-7">
                                                                <asp:FileUpload ID="FileUpload1" runat="server" Font-Size="X-Small" />
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
                                                                <asp:Button ID="btnsubmitSec" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                                                    ValidationGroup="Sec" OnClick="btnsubmitSec_Click" />
                                                                <asp:Button ID="btnupdateSec" Text="Update" runat="server" CssClass="btn btn-primary btn-sm"
                                                                    ValidationGroup="Sec" OnClick="btnupdateSec_Click" />
                                                                <asp:Button ID="btncancelSec" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                                    OnClick="btncancelSec_Click" />
                                                            </div>
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
                                                                <asp:GridView ID="gvSec" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="gvSec_RowCommand"
                                                                    OnRowDataBound="gvSec_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Section Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSec" runat="server" Text='<%# Bind("Section") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtnupdateSec" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                                    CommandName="Edit1" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;&nbsp;
                                                                                <asp:LinkButton ID="lbtndeleteSec" runat="server" CommandArgument='<%# Bind("ID") %>'
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
                                                Add Sub-Section
                                            </h4>
                                        </div>
                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            <label>
                                                                Select Section:</label>
                                                        </div>
                                                        <div class="col-md-1 requiredSign">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSec"
                                                                ValidationGroup="SubSec" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:DropDownList Style="width: 200px;" ID="ddlSec" runat="server" AutoPostBack="true"
                                                                class="form-control drpControl" ValidationGroup="SubSec" OnSelectedIndexChanged="ddlSec_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12" style="margin-top: 5px;">
                                                        <div class="col-md-4">
                                                            <label>
                                                                Enter Sub-Section Name:</label>
                                                        </div>
                                                        <div class="col-md-1 requiredSign">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSubSec"
                                                                ValidationGroup="SubSec" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:TextBox ID="txtSubSec" Style="width: 200px;" ValidationGroup="SubSec" runat="server"
                                                                Text="" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <br />
                                                    <div class="col-md-12">
                                                        <div class="col-md-6">
                                                            &nbsp;
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Button ID="btnsubmitSubSec" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                                                ValidationGroup="SubSec" OnClick="btnsubmitSubSec_Click" />
                                                            <asp:Button ID="btnupdateSubSec" Text="Update" runat="server" CssClass="btn btn-primary btn-sm"
                                                                ValidationGroup="SubSec" OnClick="btnupdateSubSec_Click" />
                                                            <asp:Button ID="btncancelSubSec" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                                OnClick="btncancelSubSec_Click" />
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
                                                                <asp:GridView ID="gvSubSec" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="gvSubSec_RowCommand"
                                                                    OnRowDataBound="gvSubSec_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sub-Section Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSubSec" runat="server" Text='<%# Bind("SubSection") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtnupdateSubSec" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                                    CommandName="Edit1" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;&nbsp;
                                                                                <asp:LinkButton ID="lbtndeleteSubSec" runat="server" CommandArgument='<%# Bind("ID") %>'
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
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnsubmitSec" />
                <asp:PostBackTrigger ControlID="btnupdateSec" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
