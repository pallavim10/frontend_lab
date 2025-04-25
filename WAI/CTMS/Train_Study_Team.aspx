<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Train_Study_Team.aspx.cs" Inherits="CTMS.Train_Study_Team" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);

            if (div.style.display == "none") {
                div.style.display = "inline";
                document.getElementById('img' + divname).className = 'icon-minus-sign-alt';

            } else {
                div.style.display = "none";
                document.getElementById('img' + divname).className = 'icon-plus-sign-alt';
            }
        }

        function ManipulateAll(ID) {
            var img = document.getElementById('img' + ID);

            if (img.className == 'icon-plus-sign-alt') {
                img.className = 'icon-minus-sign-alt'
                $("div[id*='" + ID + "']").css("display", "inline");
                $("i[id*='" + ID + "']").removeClass('icon-plus-sign-alt');
                $("i[id*='" + ID + "']").addClass('icon-minus-sign-alt');
            } else {
                img.className = 'icon-plus-sign-alt'
                $("div[id*='" + ID + "']").css("display", "none");
                $("i[id*='" + ID + "']").removeClass('icon-minus-sign-alt');
                $("i[id*='" + ID + "']").addClass('icon-plus-sign-alt');
            }
        }

        function Print() {
            var ProjectId = '<%= Session["PROJECTID"] %>'
            var test = "ReportTrain_Study_Team.aspx?ProjectId=" + ProjectId;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500px,width=1000px";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">
                        Manage Study Team
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
                                    Manage Roles
                                </h4>
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
                                                <label>
                                                    *</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:DropDownList Style="width: 200px;" ID="ddlRole" runat="server" AutoPostBack="true"
                                                    class="form-control drpControl  required" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Select Employee:</label>
                                            </div>
                                            <div class="col-md-1 requiredSign">
                                                <label>
                                                    *</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:DropDownList Style="width: 200px;" ID="ddlEmp" runat="server" AutoPostBack="true"
                                                    class="form-control drpControl required">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Enter Start Date:</label>
                                            </div>
                                            <div class="col-md-1 requiredSign">
                                                <label>
                                                    *</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:TextBox Style="text-align: center; width: 200px;" ID="txtSTDate" runat="server"
                                                    class=" txt_center form-control txtDate required"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Enter End Date:</label>
                                            </div>
                                            <div class="col-md-1 requiredSign">
                                                &nbsp;
                                            </div>
                                            <div class="col-md-7">
                                                <asp:TextBox Style="text-align: center; width: 200px;" ID="txtENDate" runat="server"
                                                    class=" txt_center form-control txtDate"></asp:TextBox>
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
                                                <asp:Button ID="btnsubmitTeam" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                    ValidationGroup="Dept" OnClick="btnsubmitTeam_Click" />
                                                <asp:Button ID="btnupdateTeam" Text="Update" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                    ValidationGroup="Dept" OnClick="btnupdateTeam_Click" />
                                                <asp:Button ID="btncancelTeam" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                    OnClick="btncancelTeam_Click" />
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
                                    Team Members
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div>
                                        <div class="rows">
                                            <div style="width: 100%; height: 264px; overflow: auto;">
                                                <div>
                                                    <asp:GridView ID="gvEmp" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                        Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="gvEmp_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblname" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="From" ItemStyle-CssClass="txt_center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFrom" runat="server" Text='<%# Bind("StartDate") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="To" ItemStyle-CssClass="txt_center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTo" runat="server" Text='<%# Bind("EndDate") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnupdateRole" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                        CommandName="Edit1" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;&nbsp;
                                                                    <asp:LinkButton ID="lbtndeleteRole" runat="server" CommandArgument='<%# Bind("ID") %>'
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
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">
                        Study Team View
                                                <asp:LinkButton ID="lbtnprint" runat="server" Text="Print" OnClientClick="return Print()" CssClass="btn-sm">
      <span class="glyphicon glyphicon-print"></span>Print</asp:LinkButton>
                    </h3>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="Label1" runat="server"></asp:Label>
                        </div>
                        <asp:GridView ID="gvRole" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            CssClass="table table-bordered table-striped" OnRowDataBound="gvRole_RowDataBound">
                            <Columns>
                                <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                                    HeaderStyle-CssClass="txt_center">
                                    <HeaderTemplate>
                                        <a href="JavaScript:ManipulateAll('_Role');" id="_Folder" style="color: #333333"><i
                                            id="img_Role" class="icon-plus-sign-alt"></i></a>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <a href="JavaScript:divexpandcollapse('_Role<%# Eval("Role_ID") %>');" style="color: #333333">
                                            <i id="img_Role<%# Eval("Role_ID") %>" class="icon-plus-sign-alt"></i></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false" HeaderText="ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_DeptID" runat="server" Text='<%# Bind("Role_ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Roles" ItemStyle-Width="100%">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Dept" Width="100%" ToolTip='<%# Bind("Role") %>' CssClass="label"
                                            runat="server" Text='<%# Bind("Role") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <tr>
                                            <td colspan="100%" style="padding: 2px;">
                                                <div style="float: right; font-size: 13px;">
                                                </div>
                                                <div>
                                                    <div class="rows">
                                                        <div class="col-md-12">
                                                            <div id="_Role<%# Eval("Role_ID") %>" style="display: none; position: relative; overflow: auto;">
                                                                <asp:GridView ID="gvEmpView" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                    CssClass="table table-bordered table-striped">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblname" CssClass="label" Width="100%" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="From" ItemStyle-CssClass="txt_center" ItemStyle-Width="20%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFrom" CssClass="label" runat="server" Text='<%# Bind("StartDate") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="To" ItemStyle-CssClass="txt_center" ItemStyle-Width="20%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTo" CssClass="label" runat="server" Text='<%# Bind("EndDate") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <RowStyle ForeColor="Blue" />
                                                                    <HeaderStyle ForeColor="Blue" />
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
