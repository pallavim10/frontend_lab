<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" ValidateRequest="false"
    CodeBehind="Budget_RolesComplexity_Master.aspx.cs" Inherits="CTMS.Budget_RolesComplexity_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function PopupComplexity() {
            var dualScreenLeft = window.screenLeft != undefined ? window.screenLeft : window.screenX;
            var dualScreenTop = window.screenTop != undefined ? window.screenTop : window.screenY;

            var width = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
            var height = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;

            var left = ((width / 2) - (600 / 2)) + dualScreenLeft;
            var top = ((height / 2) - (900 / 2)) + dualScreenTop;
            var test = "Budget_ComplexityMaster.aspx";
            window.open(test, '_blank', 'scrollbars = yes, width = 1000, height = 400, top = ' + top + ', left = ' + left);
            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Manage Roles & Complexity
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
                                                Add Roles
                                            </h4>
                                        </div>
                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="col-md-4">
                                                                <label>
                                                                    Enter Role Name:</label>
                                                            </div>
                                                            <div class="col-md-1 requiredSign">
                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtRole" ID="reqRole"
                                                                    ValidationGroup="Role" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="col-md-7">
                                                                <asp:TextBox Style="width: 250px;" ID="txtRole" ValidationGroup="Role" runat="server"
                                                                    CssClass="form-control"></asp:TextBox>
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
                                                                <asp:Button ID="btnsubmitRole" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                                                    ValidationGroup="Role" OnClick="btnsubmitRole_Click" />
                                                                <asp:Button ID="btnupdateRole" Text="Update" runat="server" CssClass="btn btn-primary btn-sm"
                                                                    ValidationGroup="Role" OnClick="btnupdateRole_Click" />
                                                                <asp:Button ID="btncancelRole" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                                    OnClick="btncancelRole_Click" />
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
                                                                <asp:GridView ID="gvRole" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="gvRole_RowCommand"
                                                                    OnRowDataBound="gvRole_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Role Name" >
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDept" runat="server" Text='<%# Bind("Role") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtnupdateRole" runat="server" CommandArgument='<%# Bind("Role_ID") %>'
                                                                                    CommandName="Edit1" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                                <asp:LinkButton ID="lbtndeleteRole" runat="server" CommandArgument='<%# Bind("Role_ID") %>'
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
                                        <div class="box-header with-border">
                                            <h4 class="box-title pull-left">
                                                Assign Complexity
                                            </h4>
                                            <%--<div class="pull-right">
                                                <asp:LinkButton ID="lbtnComplexityPopup" ForeColor="White" CssClass="btn btn-info btn-sm"
                                                    Text="Manage Complexity" runat="server" OnClientClick="return PopupComplexity();"></asp:LinkButton>
                                            </div>--%>
                                        </div>
                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            <label>
                                                                Select Role:</label>
                                                        </div>
                                                        <div class="col-md-1 requiredSign">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlRole"
                                                                InitialValue="0" ValidationGroup="Compl" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:DropDownList Style="width: 250px;" ID="ddlRole" runat="server" class="form-control drpControl"
                                                                ValidationGroup="Compl" AutoPostBack="true"
                                                                onselectedindexchanged="ddlRole_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12" style="margin-top: 5px;">
                                                        <div class="col-md-4">
                                                            <label>
                                                                Select Complexity:</label>
                                                        </div>
                                                        <div class="col-md-1 requiredSign">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCompl"
                                                                InitialValue="0" ValidationGroup="Compl" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:DropDownList Style="width: 250px;" ID="ddlCompl" runat="server" class="form-control drpControl"
                                                                ValidationGroup="Compl">
                                                            </asp:DropDownList>
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
                                                            <asp:Button ID="btnsubmitCompl" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                                                ValidationGroup="Compl" OnClick="btnsubmitCompl_Click" />
                                                            <asp:Button ID="btnupdateCompl" Text="Update" runat="server" CssClass="btn btn-primary btn-sm"
                                                                ValidationGroup="Compl" OnClick="btnupdateCompl_Click" />
                                                            <asp:Button ID="btncancelCompl" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                                OnClick="btncancelCompl_Click" />
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
                                                                <asp:GridView ID="gvCompl" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="gvCompl_RowCommand">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Role" >
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRole" runat="server" Text='<%# Bind("Role") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Complexity" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblCompl" runat="server" Text='<%# Bind("Complexity") %>' ></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtnupdateCompl" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                                    CommandName="Edit1" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                                <asp:LinkButton ID="lbtndeleteComl" runat="server" CommandArgument='<%# Bind("ID") %>'
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
