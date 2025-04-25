<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="RM_CategoryItems.aspx.cs" Inherits="CTMS.RM_CategoryItems" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Manage Masters
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
                                <div class="col-md-6">
                                    <div class="box box-primary" style="min-height: 300px;">
                                        <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                            <h4 class="box-title" align="left">
                                                Add Category
                                            </h4>
                                        </div>
                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div>
                                                    <div class="rows">
                                                        <div class="col-md-12">
                                                            <div class="col-md-3">
                                                                <label>
                                                                    Enter Category:</label>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtcategory" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:RequiredFieldValidator ID="reqcategory" runat="server" ControlToValidate="txtcategory"
                                                                    ValidationGroup="category" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12" align="center" style="margin-top: 5px; margin-left: -36px;">
                                                        <asp:Button ID="btnsubmitcate" Text="Submit" runat="server" OnClick="btnsubmitcate_Click"
                                                            CssClass="btn btn-primary btn-sm" ValidationGroup="category" />
                                                        <asp:Button ID="btnupdate" Text="Update" runat="server" CssClass="btn btn-primary btn-sm"
                                                            OnClick="btnupdate_Click" />
                                                        <asp:Button ID="btncancel" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                            OnClick="btncancel_Click" />
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
                                                                <asp:GridView ID="gridcategory" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="gridcategory_RowCommand"
                                                                    OnRowDataBound="gridcategory_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblcategory" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("id") %>'
                                                                                    CommandName="Edit1" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                                <asp:LinkButton ID="lbtndelete" runat="server" CommandArgument='<%# Bind("id") %>'
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
                                                Add Sub-Category
                                            </h4>
                                        </div>
                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div>
                                                    <div class="rows">
                                                        <div class="rows">
                                                            <div class="col-md-12">
                                                                <div class="col-md-4" style="padding-right: 0px">
                                                                    <label>
                                                                        Category:</label>
                                                                </div>
                                                                <div class="col-md-6" style="padding-left: 0px">
                                                                    <asp:DropDownList ID="ddlcategory" runat="server" AutoPostBack="true" class="form-control drpControl required"
                                                                        OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-md-1">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlcategory"
                                                                        ValidationGroup="subvate" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-12" style="margin-top: 5px;">
                                                            <div class="col-md-4" style="padding-right: 0px">
                                                                <label>
                                                                    Enter Sub-Category:</label>
                                                            </div>
                                                            <div class="col-md-6" style="padding-left: 0px">
                                                                <asp:TextBox ID="txtsubcategory" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-1">
                                                                <asp:RequiredFieldValidator ID="reqsubcategory" runat="server" ControlToValidate="txtsubcategory"
                                                                    ValidationGroup="subvate" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="rows">
                                                        <div class="col-md-12" align="center" style="margin-top: 5px; margin-left: -10px;">
                                                            <asp:Button ID="btnsubcategory" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                                                OnClick="btnsubcategory_Click" ValidationGroup="subvate" />
                                                            <asp:Button ID="btnupdatesubcat" Text="Update" runat="server" CssClass="btn btn-primary btn-sm"
                                                                ValidationGroup="subvate" OnClick="btnupdatesubcat_Click" />
                                                            <asp:Button ID="btncancelsubcategory" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                                OnClick="btncancelsubcategory_Click" />
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
                                                                <asp:GridView ID="gridsubcategory" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="gridsubcategory_RowCommand"
                                                                    OnRowDataBound="gridsubcategory_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sub-Category" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblsubcategory" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtnupdatesubcate" runat="server" CommandArgument='<%# Bind("id") %>'
                                                                                    CommandName="Edit1" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                                <asp:LinkButton ID="lbtndeletesubcate" runat="server" CommandArgument='<%# Bind("id") %>'
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
                                                Add Factors
                                            </h4>
                                        </div>
                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div>
                                                    <div class="rows">
                                                        <div class="col-md-12">
                                                            <div class="col-md-3">
                                                                <label>
                                                                    Sub-Category:</label>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlsubcategory" runat="server" AutoPostBack="true" class="form-control drpControl required"
                                                                    OnSelectedIndexChanged="ddlsubcategory_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlsubcategory"
                                                                    ValidationGroup="factor" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="rows">
                                                        <div class="col-md-12" style="margin-top: 5px;">
                                                            <div class="col-md-3">
                                                                <label>
                                                                    Enter Factors:</label>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtfactor" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtfactor"
                                                                    ValidationGroup="factor" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="rows">
                                                        <div class="col-md-12" align="center" style="margin-top: 5px; margin-left: -36px;">
                                                            <asp:Button ID="btnsubmitfactor" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                                                OnClick="btnsubmitfactor_Click" ValidationGroup="factor" />
                                                            <asp:Button ID="btnupdatefactor" Text="Update" runat="server" CssClass="btn btn-primary btn-sm"
                                                                ValidationGroup="factor" OnClick="btnupdatefactor_Click" />
                                                            <asp:Button ID="btncancelfactor" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                                OnClick="btncancelfactor_Click" />
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
                                                                <asp:GridView ID="gridfactor" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                    Style="width: 91%; border-collapse: collapse;" OnRowCommand="gridfactor_RowCommand">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Factors" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblsubcategory" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtnupdatesubcate" runat="server" CommandArgument='<%# Bind("id") %>'
                                                                                    CommandName="Edit1" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                                <asp:LinkButton ID="lbtndeletesubcate" runat="server" CommandArgument='<%# Bind("id") %>'
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
        </asp:UpdatePanel>
    </div>
</asp:Content>
