<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="eTMF_UploadDocument.aspx.cs"
    Inherits="CTMS.eTMF_UploadDocument" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<title>Diagonsearch</title>
<script src="Scripts/Jquery1.12.4.js" type="text/javascript"></script>
<link href="Styles/Site.css" rel="stylesheet" type="text/css" />
<link href="Styles/Common-Bootstrap.css" rel="stylesheet" type="text/css" />
<link href="Styles/bootstrap-theme.css" rel="stylesheet" type="text/css" />
<link href="Styles/AdminLTE.css" rel="stylesheet" type="text/css" />
<link href="Styles/ionicons.css" rel="stylesheet" type="text/css" />
<link href="Styles/font-awesome.css" rel="stylesheet" type="text/css" />
<link rel="icon" href="img/favicon.ico" type="image/x-icon">
<link rel="shortcut icon" href="img/favicon.ico" type="image/x-icon" />
<%-- <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap-theme.css" rel="stylesheet" type="text/css" />--%>
<script src="Scripts/bootstrap.js" type="text/javascript"></script>
<script src="Scripts/ClientValidation.js" type="text/javascript"></script>
<!-- for pikaday datepicker//-->
<link href="Styles/pikaday.css" rel="stylesheet" type="text/css" />
<script src="Scripts/moment.js" type="text/javascript"></script>
<script src="Scripts/pikaday.js" type="text/javascript"></script>
<script src="Scripts/pikaday.jquery.js" type="text/javascript"></script>
<!-- for pikaday datepicker//-->
<link href="Styles/jquery.alerts.css" rel="stylesheet" type="text/css" />
<script src="Scripts/jquery.alerts.js" type="text/javascript"></script>
<link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
<script src="Scripts/Select2.js" type="text/javascript"></script>
<link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
<head>
    <script>

        $(document).ready(function () {


            $('.txtDate').each(function (index, element) {
                $(element).pikaday({
                    field: element,
                    format: 'DD-MMM-YYYY',
                    yearRange: [1910, 2050]
                });
            });

            $('.txtDateMask').each(function (index, element) {
                $(element).inputmask("dd/mm/yyyy", { placeholder: "dd/mm/yyyy" });
            });

            $('.txtDateNoFuture').each(function (index, element) {
                $(element).pikaday({
                    field: element,
                    format: 'DD-MMM-YYYY',
                    yearRange: [1910, 2050],
                    maxDate: new Date()
                });
            });

            $('.txtTime').each(function (index, element) {
                $(element).inputmask(
                    "hh:mm", {
                        placeholder: "HH:MM",
                        insertMode: false,
                        showMaskOnHover: false,
                        hourFormat: "24"
                    });
            });


            $('.txtuppercase').keyup(function () {
                $(this).val($(this).val().toUpperCase());
            });

            $('.txtuppercase').keydown(function (e) {

                var key = e.keyCode;
                if (key === 189 && e.shiftKey === true) {
                    return true;
                }
                else if ((key == 189) || (key == 109)) {
                    return true;
                }
                else if (e.ctrlKey || e.altKey) {
                    e.preventDefault();
                }
                else {
                    if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
                        e.preventDefault();
                    }
                }

            });
        });

        $(document).on("click", ".cls-btnSave", function () {
            var test = "0";

            $('.required').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "0" || value == null) {
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

        function DOC_TRACKING(element) {

            var ID = $(element).closest('tr').find('td:eq(0)').find('span').text();

            var test = "eTMF_DOCUMENT_TRACKING.aspx?ID=" + ID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=300,width=600";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function VERSION_HISTORY(element) {

            var ID = $(element).closest('tr').find('td:eq(0)').find('span').text();

            var test = "eTMF_VERSION_HISTORY.aspx?ID=" + ID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=300,width=1200";
            window.open(test, '_blank', strWinProperty);
            return false;
        }
    </script>
    <style>
        .label
        {
            display: inline-block;
            max-width: 100%;
            margin-bottom: -2px;
            font-weight: bold;
            font-size: 13px;
            margin-left: 0px;
        }
    </style>
    <script>

        function ConfirmMsg() {
            var newLine = "\r\n"

            var error_msg = "This File is already exists";

            error_msg += newLine;
            error_msg += newLine;

            error_msg += "Note : Press OK to Proceed.";

            if (confirm(error_msg)) {

                $("#btnUploadAgainDoc").click();

                return true;

            } else {

                return false;
            }

        }

    </script>
</head>
<body>
    <form id="form1" method="post" class="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField runat="server" ID="hfID" />
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Upload Document</h3>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div class="form-horizontal">
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-6 width150px label">
                        <asp:Label runat="server" ID="lbl1" Text="Task"></asp:Label>
                    </label>
                    <div class="col-lg-3">
                        :</div>
                    <div class="col-lg-3">
                        <asp:Label ID="lbl_Task" runat="server"></asp:Label>
                        <asp:Label ID="lbl_Task_ID" Visible="false" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="form-horizontal">
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-6 width150px label">
                        <asp:Label runat="server" ID="lbl2" Text="Sub Task Name"></asp:Label>
                    </label>
                    <div class="col-lg-3">
                        :</div>
                    <div class="col-lg-3">
                        <asp:Label ID="lbl_Sub_Task" runat="server"></asp:Label>
                        <asp:Label ID="lbl_Sub_Task_ID" Visible="false" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="form-horizontal" id="div1" runat="server">
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-6 width150px label">
                        Select Document</label>
                    <div class="col-lg-3">
                        :</div>
                    <div class="col-lg-3">
                        <asp:DropDownList runat="server" ID="drpDocument" AutoPostBack="true" CssClass="form-control required"
                            OnSelectedIndexChanged="drpDocument_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-horizontal" id="divUploaded" visible="false" runat="server">
            <br />
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-6 width150px label">
                    </label>
                    <div class="col-lg-3">
                    </div>
                    <div class="col-lg-3">
                        <a href="#gvFiles">
                            <asp:Label runat="server" ID="lblUploaded"></asp:Label>
                        </a>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="form-horizontal" id="div3" runat="server">
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-6 width150px label">
                        Select Action/Mode</label>
                    <div class="col-lg-3">
                        :</div>
                    <div class="col-lg-3">
                        <asp:DropDownList runat="server" ID="drpAction" AutoPostBack="true" CssClass="form-control required"
                            OnSelectedIndexChanged="drpAction_SelectedIndexChanged">
                            <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Edit" Value="Edit"></asp:ListItem>
                            <asp:ListItem Text="Collaborate" Value="Collaborate"></asp:ListItem>
                            <asp:ListItem Text="Review" Value="Review"></asp:ListItem>
                            <asp:ListItem Text="QA Review" Value="QA Review"></asp:ListItem>
                            <asp:ListItem Text="Internal Approval" Value="Internal Approval"></asp:ListItem>
                            <asp:ListItem Text="Sponsor Approval" Value="Sponsor Approval"></asp:ListItem>
                            <asp:ListItem Text="Final" Value="Final"></asp:ListItem>
                            <asp:ListItem Text="Obsolete" Value="Obsolete"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="form-horizontal" id="divDeadline" visible="false" runat="server">
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-6 width150px label">
                        Enter Deadline Date (if Applicable)</label>
                    <div class="col-lg-3">
                        :</div>
                    <div class="col-lg-3">
                        <asp:TextBox runat="server" ID="txtDeadline" CssClass="form-control txtDate"></asp:TextBox>
                    </div>
                </div>
            </div>
            <br />
        </div>
        <div class="form-horizontal" id="divUsers" visible="false" runat="server">
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-6 width150px label">
                        Select Users</label>
                    <div class="col-lg-3">
                        :</div>
                    <div class="col-lg-3">
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
        </div>
        <br />
        <div class="form-horizontal" id="div4" runat="server">
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-6 width150px label">
                        Document Version
                    </label>
                    <div class="col-lg-3">
                        :</div>
                    <div class="col-lg-3">
                        <asp:TextBox runat="server" ID="txtDovVersionNo" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="form-horizontal" id="div5" runat="server">
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-6 width150px label">
                        Document Date
                    </label>
                    <div class="col-lg-3">
                        :</div>
                    <div class="col-lg-3">
                        <asp:TextBox runat="server" ID="txtDocDateTime" CssClass="form-control txtDateNoFuture"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="form-horizontal" id="div6" runat="server">
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-6 width150px label">
                        Note</label>
                    <div class="col-lg-3">
                        :</div>
                    <div class="col-lg-3">
                        <asp:TextBox runat="server" ID="txtNote" TextMode="MultiLine" CssClass="form-control width300px"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="form-horizontal" id="divCOUNTRY" runat="server">
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-6 width150px label">
                        Select Country</label>
                    <div class="col-lg-3">
                        :</div>
                    <div class="col-lg-3">
                        <asp:DropDownList runat="server" ID="drpCountry" AutoPostBack="true" CssClass="form-control"
                            OnSelectedIndexChanged="drpCountry_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="form-horizontal" id="divINVID" runat="server">
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-6 width150px label">
                        Site ID</label>
                    <div class="col-lg-3">
                        :</div>
                    <div class="col-lg-3">
                        <asp:DropDownList runat="server" ID="drpSites" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <br />
        </div>
        <div class="row" id="divFunction" runat="server" style="padding-top: 15px;">
            <div class="col-md-12">
                <div class="label col-md-2">
                    Function : &nbsp;
                    <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlFunction" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="form-horizontal" id="div2" runat="server">
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-6 width150px label">
                        Enter Expiry Date (if Applicable)</label>
                    <div class="col-lg-3">
                        :</div>
                    <div class="col-lg-3">
                        <asp:TextBox runat="server" ID="txtExpiryDate" CssClass="form-control txtDate"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="form-horizontal">
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-6 width150px label">
                        Select File</label>
                    <div class="col-lg-3">
                        :</div>
                    <div class="col-lg-3">
                        <asp:FileUpload ID="FileUpload1" runat="server" Font-Size="X-Small" />
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="form-horizontal">
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-6 width150px label">
                        &nbsp;</label>
                    <div class="col-lg-3">
                        &nbsp;</div>
                    <div class="col-lg-3">
                        <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-primary btn-sm cls-btnSave"
                            OnClick="btnUpload_Click" />
                        <asp:Button ID="btnUploadAgainDoc" runat="server" CssClass="disp-none" OnClick="btnUploadAgainDoc_Click">
                        </asp:Button>
                    </div>
                </div>
            </div>
        </div>
        <br />
    </div>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Uploaded Document</h3>
        </div>
        <asp:GridView ID="gvFiles" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="table table-bordered table-striped layerFiles Datatable" OnPreRender="grd_data_PreRender"
            OnRowDataBound="gvFiles_RowDataBound" OnRowCommand="gvFiles_RowCommand">
            <Columns>
                <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                    ItemStyle-CssClass="disp-none" HeaderText="ID">
                    <ItemTemplate>
                        <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                    ItemStyle-CssClass="disp-none" HeaderText="SysFileName">
                    <ItemTemplate>
                        <asp:Label ID="SysFileName" runat="server" Text='<%# Bind("SysFileName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Country">
                    <ItemTemplate>
                        <asp:Label ID="COUNTRYNAME" Width="100%" ToolTip='<%# Bind("COUNTRYNAME") %>' CssClass="label"
                            runat="server" Text='<%# Bind("COUNTRYNAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SIte Id">
                    <ItemTemplate>
                        <asp:Label ID="SiteID" Width="100%" ToolTip='<%# Bind("SiteID") %>' CssClass="label"
                            runat="server" Text='<%# Bind("SiteID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                    ItemStyle-CssClass="disp-none">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Name" Width="100%" ToolTip='<%# Bind("AutoNomenclature") %>' CssClass="label"
                            runat="server" Text='<%# Bind("AutoNomenclature") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="File Name">
                    <ItemTemplate>
                        <asp:Label ID="lbl_UploadFileName" Width="100%" ToolTip='<%# Bind("UploadFileName") %>'
                            CssClass="label" runat="server" Text='<%# Bind("UploadFileName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="File Type" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label ID="lbtnFileType" runat="server" Font-Size="Larger"><i id="ICONCLASS"
                            runat="server" class="fas fa-file-text"></i></asp:Label>
                            <asp:Label ID="lbl_FileSize" Width="100%" CssClass="label" Font-Size="X-Small" runat="server" Text='<%# Bind("CAL_Size") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="System Version">
                    <ItemTemplate>
                        <asp:Label ID="lbl_VersionID" Width="100%" ToolTip='<%# Bind("VersionID") %>' CssClass="label"
                            runat="server" Text='<%# Bind("VersionID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Status" Width="100%" ToolTip='<%# Bind("Status") %>' CssClass="label"
                            runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Document Version" ItemStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label ID="DOC_VERSIONNO" Width="100%" ToolTip='<%# Bind("DOC_VERSIONNO") %>'
                            CssClass="label" runat="server" Text='<%# Bind("DOC_VERSIONNO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Document Date" ItemStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label ID="DOC_DATETIME" Width="100%" ToolTip='<%# Bind("DOC_DATETIME") %>' CssClass="label"
                            runat="server" Text='<%# Bind("DOC_DATETIME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Note" ItemStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label ID="NOTE" Width="100%" ToolTip='<%# Bind("NOTE") %>' CssClass="label"
                            runat="server" Text='<%# Bind("NOTE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Uploaded By" ItemStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label ID="lbl_User" Width="100%" ToolTip='<%# Bind("User") %>' CssClass="label"
                            runat="server" Text='<%# Bind("User") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Uploaded DateTime" ItemStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label ID="lbl_UploadDateTime" Width="100%" ToolTip='<%# Bind("UploadDateTime") %>'
                            CssClass="label" runat="server" Text='<%# Bind("UploadDateTime") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="1%">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnversionHistory" runat="server" ToolTip="Version History"
                            OnClientClick="return VERSION_HISTORY(this);" CommandArgument='<%# Eval("ID") %>'><i class="fa fa-history" style="color:#333333;" aria-hidden="true"></i>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="1%">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDocumentTrack" runat="server" ToolTip="Document Track" CommandArgument='<%# Eval("ID") %>'
                            OnClientClick="return DOC_TRACKING(this);"><i class="fa fa-map-marked-alt" style="color:#333333;" aria-hidden="true"></i>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="1%">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDownloadDoc" runat="server" ToolTip="Download" CommandName="Download" CssClass="btn"
                            CommandArgument='<%# Eval("ID") %>'><i class="fa fa-download" style="color:#333333;" aria-hidden="true"></i>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
