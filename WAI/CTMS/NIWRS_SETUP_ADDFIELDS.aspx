<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NIWRS_SETUP_ADDFIELDS.aspx.cs"
    Inherits="CTMS.NIWRS_SETUP_ADDFIELDS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>WAI</title>
    <script src="Scripts/Jquery1.12.4.js" type="text/javascript"></script>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Common-Bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap-theme.css" rel="stylesheet" type="text/css" />
    <link href="Styles/AdminLTE.css" rel="stylesheet" type="text/css" />
    <link href="Styles/ionicons.css" rel="stylesheet" type="text/css" />
    <link href="Styles/font-awesome.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="img/favicon.ico" type="image/x-icon" />
    <link rel="shortcut icon" href="img/favicon.ico" type="image/x-icon" />
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="CommonFunctionsJs/IWRS/IWRS_ConfirmMsg.js"></script>
    <script src="Scripts/ClientValidation.js" type="text/javascript"></script>
    <!-- for Jquery Popup//-->
    <script src="https://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <link href="Styles/Jquery.ui.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/CommonFunction.js" type="text/javascript"></script>
    <!-- for Jquery Popup//-->
    <!-- Bootstrap -->
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <!-- Morris.js charts -->
    <%--  <script src="js/plugins/morris/morris.min.js" type="text/javascript"></script>--%>
    <!-- Sparkline -->
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <!-- jvectormap -->
    <script src="js/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js" type="text/javascript"></script>
    <script src="js/plugins/jvectormap/jquery-jvectormap-world-mill-en.js" type="text/javascript"></script>
    <!-- fullCalendar -->
    <script src="js/plugins/fullcalendar/fullcalendar.min.js" type="text/javascript"></script>
    <!-- jQuery Knob Chart -->
    <script src="js/plugins/jqueryKnob/jquery.knob.js" type="text/javascript"></script>
    <!-- daterangepicker -->
    <script src="js/plugins/daterangepicker/daterangepicker.js" type="text/javascript"></script>
    <!-- Bootstrap WYSIHTML5 -->
    <script src="js/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js" type="text/javascript"></script>
    <!-- iCheck -->
    <script src="js/plugins/iCheck/icheck.min.js" type="text/javascript"></script>
    <!-- AdminLTE App -->
    <script src="js/AdminLTE/app.js" type="text/javascript"></script>
    <script src="CommonFunctionsJs/IWRS/IWRS_showAuditTrail.js"></script>
    <!-- AdminLTE dashboard demo (This is only for demo purposes) -->
    <link href="Styles/graph.css" rel="stylesheet" type="text/css" />
    <script>

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

        function ConfigFrozen_MSG() {
            alert('Configuration has been Frozen');
            return false;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin: 5px">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:HiddenField runat="server" ID="hdnREVIEWSTATUS" />
            <div class="row">
                <div class="lblError">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-6">
                        <div class="box box-primary" style="min-height: 300px;">
                            <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                <h4 class="box-title" align="left">Add Additional Fields for : &nbsp;<asp:Label ID="lblList" runat="server"></asp:Label>
                                </h4>
                            </div>
                            <br />
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    <label>
                                                        Enter Field Name :</label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtFieldName" runat="server" CssClass="form-control width200px required"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    <label>
                                                        Enter Column Name :</label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtColName" runat="server" CssClass="form-control width200px required"></asp:TextBox>
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
                                                    <asp:Button ID="btnSubmitCol" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave4"
                                                        OnClick="btnSubmitCol_Click" />
                                                    <asp:Button ID="btnUpdateCol" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave4"
                                                        OnClick="btnUpdateCol_Click" />&nbsp;&nbsp;
                                                <asp:Button ID="btnCancelCol" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                    OnClick="btnCancelCol_Click" />
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
                                <h4 class="box-title" align="left">Records
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div>
                                        <div class="rows">
                                            <div style="width: 100%; height: 264px; overflow: auto;">
                                                <div>
                                                    <asp:GridView ID="grdCols" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                        Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdCols_RowCommand" OnRowDataBound="grdCols_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none"
                                                                ItemStyle-CssClass="disp-none">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                        CommandName="EditCol" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Field Name" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFIELDNAME" runat="server" Text='<%# Bind("FIELDNAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Column Name" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCOL_NAME" runat="server" Text='<%# Bind("COL_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('NIWRS_SETUP_LISTING_ADDFIELDS', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtndeleteSection" runat="server" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this field : ", Eval("FIELDNAME")) %>' CommandArgument='<%# Bind("ID") %>'
                                                                        CommandName="DeleteCol" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                        <div id="popup_AuditTrail" title="Audit Trail" class="disp-none">
                            <div id="DivAuditTrail" style="font-size: small;">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-6">
                        <div class="box box-primary" style="min-height: 300px;">
                            <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                <h4 class="box-title" align="left">Entered details module: &nbsp;<asp:Label ID="lblListing" runat="server"></asp:Label>
                                </h4>
                            </div>
                            <br />
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    <label>
                                                        Select Module :</label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:DropDownList ID="drpModule" runat="server" CssClass="form-control width200px"></asp:DropDownList>
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
                                                    <asp:Button ID="btnUpdate" Text="Update" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnUpdate_Click"/>
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
    </form>
</body>
</html>
