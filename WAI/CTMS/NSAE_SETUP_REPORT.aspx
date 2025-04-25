<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NSAE_SETUP_REPORT.aspx.cs" Inherits="CTMS.NSAE_SETUP_REPORT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="CommonFunctionsJs/SAE/SAE_ConfirmMsg.js"></script>
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <script src="CommonFunctionsJs/SAE/SAE_showAuditTrail.js"></script>
    <style type="text/css">
        .select2-container .select2-selection--multiple {
            min-height: 60px;
            width: 200px;
        }

        .fixTableHead {
            overflow-y: auto;
            max-height: 300px;
            min-height: 300px;
        }
    </style>
    <script language="javascript" type="text/javascript">

        function pageLoad() {
            $('.select').select2();

            $(".Datatable").parent().parent().addClass('fixTableHead');

        }
    </script>
    <script type="text/jscript">
        $(document).on("click", ".cls-btnSave1", function () {
            var test = "0";

            $('.required1').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "0" || value == null || value == "Select") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                }
                else if (ctrl == "text" || ctrl == "textarea") {
                    if (value == "") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                }
            });

            if (test == "1") {
                return false;
            }
            return true;
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="form-group">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <div class="box-body">
        <div class="row">
            <div class="col-md-6">
                <div class="box box-primary" id="div11" runat="server">
                    <div class="box-header">
                        <h3 class="box-title">Define Report </h3>
                    </div>
                    <div class="rows">
                        <div style="height: 264px;">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            Report Name:</label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:TextBox Style="width: 200px;" ID="txtReportName" runat="server"
                                            class="form-control numeric required1"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            Access To :</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkInternal" />&nbsp;&nbsp;
                                                        <label>
                                                            Interrnal
                                                        </label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkSite" />&nbsp;&nbsp;
                                                        <label>
                                                            Site
                                                        </label>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            Upload Template:</label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:FileUpload ID="DfnReportFile" runat="server" />
                                        <label style="color: red; font-weight: normal;">[Note : Please Select Word File Only]</label>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-7">
                                        <asp:Button ID="btnSubmitDefineReport" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave1" OnClick="btnSubmitDefineReport_Click" />
                                        <asp:Button ID="btnUpdateDefineReport" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnUpdateDefineReport_Click" />&nbsp;&nbsp;
                                                    <asp:Button ID="btnCancelDefineReport" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnCancelDefineReport_Click" />
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="box box-primary" id="div12" runat="server">
                    <div class="box-header">
                        <h3 class="box-title">Records</h3>
                    </div>
                    <div class="rows">
                        <div>
                            <asp:GridView ID="grddefineReport" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grddefineReport_RowCommand" OnRowDataBound="grddefineReport_RowDataBound" OnPreRender="grddefineReport_PreRender">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                                        HeaderText="ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbteditDefineReport" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                CommandName="EditDefineReport" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Report Name" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReportname" runat="server" Text='<%# Bind("REPORT_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Internal" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInternal" runat="server" CommandArgument='<%# Eval("INTERNAL") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconInternal" runat="server" class="fa fa-check"></i></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Site" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsite" runat="server" CommandArgument='<%# Eval("SITE") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconSite" runat="server" class="fa fa-check"></i></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="File Name" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileName" runat="server" Text='<%# Bind("FILENAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('SAE_SETUP_REPORT', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnDefineReport" runat="server" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this Report : ", Eval("REPORT_NAME")) %>' CommandArgument='<%# Eval("ID") %>'
                                                CommandName="DeleteDefineReport" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
    <br />
    <div class="box-body">
        <div class="row">
            <div class="col-md-6">
                <div class="box box-primary" id="div1" runat="server">
                    <div class="box-header">
                        <h3 class="box-title">Add Variables</h3>
                    </div>
                    <div class="rows">
                        <div style="height: 264px;">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            Select Report :</label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:DropDownList runat="server" ID="drpReport" CssClass="form-control required8 width200px" 
                                            AutoPostBack="true" OnSelectedIndexChanged="drpReport_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            Select Control Type :</label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:DropDownList ID="drpControlType" runat="server" CssClass="form-control width200px"
                                            AutoPostBack="true" OnSelectedIndexChanged="drpControlType_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                            <asp:ListItem Value="Plain Text Content Control" Text="Plain Text Content Control"></asp:ListItem>
                                            <asp:ListItem Value="Check Box Content Control" Text="Check Box Content Control"></asp:ListItem>
                                            <asp:ListItem Value="Repeating Section Content Control" Text="Repeating Section Content Control"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            Select Module :</label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:DropDownList runat="server" ID="drpModule" CssClass="form-control required8 width200px"
                                            AutoPostBack="true" OnSelectedIndexChanged="drpModule_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            Select Field/s :</label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:ListBox ID="lstFeilds" runat="server" CssClass="width300px select"></asp:ListBox>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-7">
                                        <asp:Button ID="btnSubmitVaribales" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm " OnClick="btnSubmitVaribales_Click" />
                                        <asp:Button ID="btnUpdateVaribales" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnUpdateVaribales_Click" />&nbsp;&nbsp;
                                        <asp:Button ID="btnCancelVaribales" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnCancelVaribales_Click" />
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="box box-primary" id="div2" runat="server">
                    <div class="box-header">
                        <h3 class="box-title">Records</h3>
                    </div>
                    <div class="rows">
                        <div style="width: 100%; height: 264px; overflow: auto;">
                            <div>
                                <div>
                                    <asp:GridView ID="grdReportVariavles" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                        Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowDataBound="grdReportVariavles_RowDataBound" OnRowCommand="grdReportVariavles_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                                                HeaderText="ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                                                HeaderStyle-CssClass="txt_center">
                                                <HeaderTemplate>
                                                    <a href="JavaScript:ManipulateAll('_Field_');" id="_Folder" style="color: #333333"><i
                                                        id="img_Field_" class="icon-plus-sign-alt"></i></a>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div runat="server" id="anchor">
                                                        <a href="JavaScript:divexpandcollapse('_Field_<%# Eval("ID") %>');" style="color: #333333">
                                                            <i id="img_Field_<%# Eval("ID") %>" class="icon-plus-sign-alt"></i></a>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                        CommandName="EditReportVariable" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID" ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("FIELD_IDS") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Control Type" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLISTNAME" runat="server" Text='<%# Bind("CONTROL_TYPE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Module Name" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtModuleName" runat="server" Text='<%# Bind("MODULENAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('SAE_SETUP_REPORT_VARIABLES', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnDelete" runat="server" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this Control Type : ", Eval("CONTROL_TYPE")) %>' CommandArgument='<%# Bind("ID") %>'
                                                        CommandName="DeleteReportVariable" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td colspan="100%" style="padding: 2px; padding-left: 4%;">
                                                            <div style="float: right; font-size: 13px;">
                                                            </div>
                                                            <div>
                                                                <div id="_Field_<%# Eval("ID") %>" style="display: none; position: relative; overflow: auto;">
                                                                    <asp:GridView ID="grdListReportVariable" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                                                        CssClass="table table-bordered table-striped table-striped1">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Variable Name" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblVARIABLENAME" Text='<%# Eval("VARIABLENAME") %>' Style="text-align: center;" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Field Name" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblField" Text='<%# Eval("FIELDNAME") %>' Style="text-align: center;" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
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
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
