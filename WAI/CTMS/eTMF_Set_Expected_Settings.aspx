<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="eTMF_Set_Expected_Settings.aspx.cs"
    Inherits="CTMS.eTMF_Set_Expected_Settings" %>

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

        $(document).on("click", ".cls-btnSave1", function () {
            var test = "0";

            $('.required1').each(function (index, element) {
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


        $(document).on("click", ".cls-btnSave2", function () {
            var test = "0";

            $('.required2').each(function (index, element) {
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


        $(document).on("click", ".cls-btnSave4", function () {
            var test = "0";

            $('.required4').each(function (index, element) {
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
    </script>
    <script>
        $(function () {
            $("#btnSubmitSPECtitle").click(function () {
                var txtName = $("#txtSPECtitle");
                var result = $.trim(txtName.val());
                txtName.val(result);
            });

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="box box-warning">
            <div class="box-header">
                <h3 class="box-title" style="color: Blue;">Additional settings &nbsp&nbsp||&nbsp&nbsp<asp:Label ID="lblDocName" ForeColor="Blue" runat="server"></asp:Label>
                </h3>
            </div>
            <div class="row">
                <div class="lblError">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-6" runat="server" id="divExpComment">
                        <div class="box box-primary" style="min-height: 300px;">
                            <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                <h4 class="box-title" align="left">Add Expected Document Comments
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-3">
                                                    <label>
                                                        Comments :</label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtExpComment" runat="server" TextMode="MultiLine" CssClass="form-control"
                                                        Width="300px" Height="150px"></asp:TextBox>
                                                    <%--CssClass="form-control required--%>
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
                                                    <asp:Button ID="btnSubmitComm" Text="Save" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                        OnClick="btnSubmitComm_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6" runat="server" id="divInstructions">
                        <div class="box box-primary" style="min-height: 300px;">
                            <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                <h4 class="box-title" align="left">Add Instructions
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-3">
                                                    <label>
                                                        Instruction :</label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtInstruction" runat="server" TextMode="MultiLine" CssClass="form-control"
                                                        Width="300px" Height="150px"></asp:TextBox>
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
                                                    <asp:Button ID="btnsubmitInst" Text="Save" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave1"
                                                        OnClick="btnsubmitInst_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-primary" style="min-height: 300px;">
                            <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                <h4 class="box-title" align="left">Define Titles
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-3">
                                                    <label>
                                                        Enter Date Convention :</label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtDateTitle" runat="server" CssClass="form-control width300px required5"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    &nbsp;
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:Button ID="btnSubmitDateTitle" Text="Save" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave5"
                                                        OnClick="btnSubmitDateTitle_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div align="left" style="margin-left: 5px">
                                    <div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-3">
                                                    <label>
                                                        Enter Spec. Convention :</label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtSPECtitle" runat="server" CssClass="form-control width300px required4"></asp:TextBox>
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
                                                    <asp:Button ID="btnSubmitSPECtitle" Text="Save" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave4"
                                                        OnClick="btnSubmitSPECtitle_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6" runat="server" id="divEmail" visible="false">
                        <div class="box box-primary" style="min-height: 300px;">
                            <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                <h4 class="box-title" align="left">Define Email
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-3">
                                                    <label>
                                                        Email To :</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkUpload" />&nbsp;&nbsp;
                                                <label>
                                                    Upload</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkdownload" />&nbsp;&nbsp;
                                                <label>
                                                    Download</label>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-3">
                                                    &nbsp;
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:Button ID="btnSubmitEmail" Text="Save" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                        OnClick="btnSubmitEmail_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="col-md-6" runat="server" id="divSPEC" visible="false">
                        <div class="box box-primary">
                            <div class="box-header with-border" style="float: left;">
                                <h4 class="box-title" align="left">Define Spec. Options
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div>
                                        <div class="rows">
                                            <div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-3">
                                                            Sequence No.
                                                        </div>
                                                        <div class="col-md-4">
                                                            Option
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtSeqNo" runat="server" CssClass="form-control width90px numeric required2"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:TextBox ID="txtText" runat="server" CssClass="form-control width180px required2"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4" style="display: inline-flex">
                                                            <asp:Button ID="btnsubmitSPEC" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave2"
                                                                OnClick="btnsubmitSPEC_Click" />
                                                            <asp:Button ID="btnupdateSPEC" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave2"
                                                                OnClick="btnupdateSPEC_Click" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btncancelSPEC" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                                OnClick="btncancelSPEC_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                            </div>
                                            <br />
                                            <div style="width: 100%; height: 264px; overflow: auto;">
                                                <div>
                                                    <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                        Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdData_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sequence No." ItemStyle-Width="2%" ControlStyle-Width="2%"
                                                                ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="SEQNO" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="TEXT" ItemStyle-Width="100%" ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtTEXT" runat="server" Text='<%# Bind("TEXT") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Options" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                        CommandName="EditModule" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lbtndeleteSection" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                        CommandName="DeleteModule" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
    </form>
</body>
</html>
