<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="eTMF_Change_Status.aspx.cs" Inherits="CTMS.eTMF_Change_Status" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script src="js/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>
    <script src="js/plugins/datatables/dataTables.bootstrap.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="Styles/datatables/dataTables.bootstrap.css" />
    <script language="javascript" type="text/javascript">

        function pageLoad() {

            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                stateSave: false,
                fixedHeader: true
            });

        }

        $(document).ready(function () {

            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                stateSave: false,
                fixedHeader: true
            });

        });

    </script>
    <style>
        .layer1
        {
            color: #0000ff;
        }
        .layer2
        {
            color: #800000;
        }
        .layer3
        {
            color: #008000;
        }
        .layer4
        {
            color: Black;
        }
        .layerFiles
        {
            color: #800000;
            font-style: italic;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Change Status</h3>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row" style="margin-top: 15px;">
            <div class="col-md-12">
                <div class="label col-md-2">
                    Current Status</div>
                <div class="col-md-4">
                    <asp:Label runat="server" ID="lblCurrentStatus" CssClass="form-control"></asp:Label>
                </div>
            </div>
        </div>
        <div class="row" style="margin-top: 15px;" id="divaddedusers" runat="server" visible="false">
            <div class="col-md-12">
                <div class="label col-md-2">
                    Selected Users</div>
                <div class="col-md-4">
                    <asp:GridView ID="gvUsers" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        CssClass="table table-bordered table-striped layerFiles" OnRowDataBound="gvUsers_RowDataBound">
                        <Columns>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCheck" Visible="false"><i class="fa fa-check-square-o"></i></asp:Label>
                                    <asp:Label runat="server" ID="lblUnCheck" Visible="false"><i class="fa fa-square-o"></i></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User">
                                <ItemTemplate>
                                    <asp:Label ID="User" Width="100%" ToolTip='<%# Bind("User") %>' CssClass="label"
                                        runat="server" Text='<%# Bind("User") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date-Time">
                                <ItemTemplate>
                                    <asp:Label ID="DoneDT" Width="100%" ToolTip='<%# Bind("DoneDT") %>' CssClass="label"
                                        runat="server" Text='<%# Bind("DoneDT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
        <div class="row" id="div7" runat="server" style="padding-top: 15px;">
            <div class="col-md-12">
                <div class="label col-md-2">
                    Change Status to
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="drpAction" AutoPostBack="true" CssClass="form-control required"
                        OnSelectedIndexChanged="drpAction_SelectedIndexChanged">
                        <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Edit" Value="Edit"></asp:ListItem>
                        <asp:ListItem Text="Collaborate" Value="Collaborate"></asp:ListItem>
                        <asp:ListItem Text="Review" Value="Review"></asp:ListItem>
                        <asp:ListItem Text="QC" Value="QC"></asp:ListItem>
                        <asp:ListItem Text="QA Review" Value="QA Review"></asp:ListItem>
                        <asp:ListItem Text="Internal Approval" Value="Internal Approval"></asp:ListItem>
                        <asp:ListItem Text="Sponsor Approval" Value="Sponsor Approval"></asp:ListItem>
                        <asp:ListItem Text="Final" Value="Final"></asp:ListItem>
                        <asp:ListItem Text="Obsolete" Value="Obsolete"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row" id="divDeadline" visible="false" runat="server" style="padding-top: 15px;">
            <div class="col-md-12">
                <div class="label col-md-2">
                    Enter Deadline Date (if Applicable)
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtDeadline" CssClass="form-control txtDate"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row" id="divUsers" visible="false" runat="server" style="padding-top: 15px;">
            <div class="col-md-12">
                <div class="label col-md-2">
                    Select Users : &nbsp;
                    <asp:Label ID="Label12" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:GridView ID="grd_Users" runat="server" CellPadding="3" AutoGenerateColumns="False"
                        CssClass="table table-bordered table-striped table-striped1">
                        <Columns>
                            <asp:TemplateField HeaderText="User_ID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                <ItemTemplate>
                                    <asp:Label ID="User_ID" Text='<%# Eval("User_ID") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User Name">
                                <ItemTemplate>
                                    <asp:Label ID="User_Name" Text='<%# Eval("User_Name") %>' ToolTip='<%# Eval("User_Name") %>'
                                        runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
        <div class="row" id="div12" runat="server" style="padding-top: 15px; padding-bottom: 15px;">
            <div class="col-md-12">
                <div class="label col-md-2">
                    <asp:Button ID="btnback" runat="server" Text="Back" CssClass="btn btn-primary btn-sm"
                        OnClick="btnback_Click" />
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm cls-btnSave"
                        OnClick="btnSubmit_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
