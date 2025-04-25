<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NCTMS_AddComments_SPONSOR.aspx.cs"
    Inherits="CTMS.NCTMS_AddComments_SPONSOR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="UTF-8">
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
    <!-- for pikaday datepicker//-->
    <link href="Styles/pikaday.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/moment.js" type="text/javascript"></script>
    <script src="Scripts/pikaday.js" type="text/javascript"></script>
    <script src="Scripts/pikaday.jquery.js" type="text/javascript"></script>
    <!-- for pikaday datepicker//-->
    <script src="js/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>
    <script src="js/plugins/datatables/dataTables.bootstrap.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="Styles/datatables/dataTables.bootstrap.css" />
    <script src="js/plugins/moment/moment.min.js" type="text/javascript"></script>
    <script src="js/plugins/moment/datetime-moment.js" type="text/javascript"></script>
    <link href="js/plugins/datatables/jquery.dataTables.css" rel="stylesheet" type="text/css" />
    <!-- Bootstrap -->
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <!-- Morris.js charts -->
    <script src="//cdnjs.cloudflare.com/ajax/libs/raphael/2.1.0/raphael-min.js"></script>
    <%--  <script src="js/plugins/morris/morris.min.js" type="text/javascript"></script>--%>
    <!-- Sparkline -->
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <!-- jvectormap -->
    <script src="js/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js" type="text/javascript"></script>
    <script src="js/plugins/jvectormap/jquery-jvectormap-world-mill-en.js" type="text/javascript"></script>
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
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script src="js/CKEditor/ckeditor.js" type="text/javascript"></script>
    <script type="text/javascript">
        CKEDITOR.config.toolbar = [
           ['Bold', 'Italic', 'Underline', 'StrikeThrough', '-', 'Undo', 'Redo', '-', 'Outdent', 'Indent', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
           ['Styles', 'Format', 'Font', 'FontSize', 'Table']
           ];

        CKEDITOR.config.height = 150;

        function CallCkedit() {

            CKEDITOR.replace("MainContent_txtEmailBody");

        }
    </script>
    <!-- AdminLTE dashboard demo (This is only for demo purposes) -->
    <link href="Styles/graph.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

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
                    else {
                        $(this).removeClass("brd-1px-redimp");
                    }
                }
                else if (ctrl == "text" || ctrl == "textarea") {
                    if (value == "") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                    else {
                        $(this).removeClass("brd-1px-redimp");
                    }
                }
            });

            if (test == "1") {
                return false;
            }
            return true;
        });



        $(document).ready(function () {
            $(".nav-tabs a").click(function (event) {
                event.preventDefault();
                $(this).parent().addClass("active");
                $(this).parent().siblings().removeClass("active");
                var tab = $(this).attr("href");
                $(".tab-content").not(tab).css("display", "none");
                $(tab).fadeIn();
            });
        });

        $(document).ready(function () {

            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: false,
                fixedHeader: true
            });

            if ($(location).attr('href').indexOf('NIWRS_FORM') != -1) {

                $('.txtDate').each(function (index, element) {
                    $(element).pikaday({
                        field: element,
                        format: 'DD-MMM-YYYY',
                        yearRange: [1910, 2050],
                        maxDate: new Date()
                    });
                });

            }
            else {

                $('.txtDate').each(function (index, element) {
                    $(element).pikaday({
                        field: element,
                        format: 'DD-MMM-YYYY',
                        yearRange: [1910, 2050]
                    });
                });

            }

            $('.txtDateNoFuture').each(function (index, element) {
                $(element).pikaday({
                    field: element,
                    format: 'DD-MMM-YYYY',
                    yearRange: [1910, 2050],
                    maxDate: new Date()
                });
            });

            $('.txtDateMask').each(function (index, element) {
                $(element).inputmask("dd/mm/yyyy", { placeholder: "dd/mm/yyyy" });
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
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="page">
        <div class="box box-warning">
            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            <div class="box-header" style="display: inline-flex; width: 100%;">
                <h3 class="box-title" style="width: 100%; font-weight: bold;">
                    Section Comments &nbsp;||&nbsp; Site Id :<asp:Label runat="server" ForeColor="Blue"
                        ID="lblSiteId" />&nbsp;||&nbsp;Visit :<asp:Label runat="server" ForeColor="Blue"
                            ID="lblVisit" />&nbsp;||&nbsp; Section :<asp:Label runat="server" ForeColor="Blue"
                                ID="lblModuleName" />&nbsp;||&nbsp;Visit Id :<asp:Label runat="server" ForeColor="Blue"
                                    ID="lblSVID" />&nbsp;&nbsp;
                </h3>
            </div>
        </div>
        <div class="box-body" id="divAddUpdateComment" runat="server">
            <div id="accordion">
                <div class="box box-danger">
                    <div class="box-header">
                        <h4 class="box-title">
                            <asp:Label runat="server" ID="lblNewComment" Text="Add New Comment">
                            </asp:Label>
                        </h4>
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Select Screening ID :&nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="drp_SUBJID" runat="server" class="form-control drpControl width260pximp">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row" id="divCRACOMMENT" runat="server" visible="false">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Comments :&nbsp;
                                    <asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox runat="server" ID="txtCRAComments" CssClass="ckeditor required" Enabled="false"
                                        Height="10%" TextMode="MultiLine" Width="99%"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row" id="divPMCOMMENT" runat="server" visible="false">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    PM Comments :&nbsp;
                                    <asp:Label ID="Label3" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox runat="server" ID="txtPMComment" CssClass="ckeditor required" Height="10%"
                                        Enabled="false" TextMode="MultiLine" Width="99%"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row" id="divSPONSORCOMMENT" runat="server">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Sponsor Comments :&nbsp;
                                    <asp:Label ID="Label5" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox runat="server" ID="txtSponsorComment" CssClass="ckeditor required" Height="10%"
                                        TextMode="MultiLine" Width="99%"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row" id="divTypeOfComment" runat="server" visible="false">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Type of Comments : &nbsp;
                                    <asp:Label ID="Label6" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <asp:CheckBox ID="chk_Internal" runat="server"></asp:CheckBox>
                                    <asp:Label ID="Label12" runat="server" Text="For Internal Use"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <asp:CheckBox ID="chk_FollowUp" runat="server"></asp:CheckBox>
                                    <asp:Label ID="Label13" runat="server" Text="Follow Up"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <asp:CheckBox ID="chkReport" runat="server"></asp:CheckBox>
                                    <asp:Label ID="Label14" runat="server" Text="In Report"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <asp:CheckBox ID="chkPD" runat="server"></asp:CheckBox>
                                    <asp:Label ID="Label4" runat="server" Text="Protocol deviation"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div style="margin-left: 20%;">
                                <asp:Button ID="bntSave" runat="server" Text="Save" CssClass="btn btn-primary btn-sm cls-btnSave"
                                    OnClick="bntSave_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-sm"
                                    OnClick="btnCancel_Click" />
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </div>
        <div class="box box-success">
            <div class="box-header">
                <h4 class="box-title">
                    Records&nbsp&nbsp <a href="JavaScript:ManipulateAll('_Pat_');" id="_Folder" style="color: #333333;
                        font-size: x-large; margin-top: 5px;"><i id="img_Pat_" class="icon-plus-sign-alt">
                        </i></a>
                </h4>
            </div>
            <asp:Repeater runat="server" OnItemDataBound="repeatData_ItemDataBound" ID="repeatData"
                OnItemCommand="repeatData_ItemCommand">
                <ItemTemplate>
                    <div class="box box-primary">
                        <div class="box-header">
                            <div runat="server" style="display: inline-flex; padding: 0px; margin: 4px 0px 0px 10px;"
                                id="anchor">
                                <a href="JavaScript:divexpandcollapse('_Pat_<%# Eval("ID") %>');" style="color: #333333">
                                    <i id="img_Pat_<%# Eval("ID") %>" class="icon-plus-sign-alt"></i></a>
                                <h3 class="box-title" style="padding-top: 0px;">
                                    <asp:Label ID="lblHeader" runat="server" Text="Section Comment"></asp:Label>
                                </h3>
                            </div>
                        </div>
                        <div id="_Pat_<%# Eval("ID") %>" style="display: none; position: relative; overflow: auto;">
                            <div class="box box-danger">
                                <div class="box-body">
                                    <div class="rows">
                                        <div style="width: 100%; overflow: auto;">
                                            <div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="label col-md-2" style="color: Blue">
                                                            Subject ID :&nbsp;
                                                        </div>
                                                        <div class="col-md-8">
                                                            <asp:Label ID="SUBJID" runat="server" Text='<%# Bind("SUBJID") %>'></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="label col-md-2" style="color: Blue">
                                                            Type of Comments : &nbsp;
                                                        </div>
                                                        <div class="col-md-2" id="divInternal" runat="server">
                                                            <asp:CheckBox ID="chk_Internal" Enabled="false" runat="server"></asp:CheckBox>
                                                            <asp:Label ID="Label12" runat="server" Text="For Internal Use"></asp:Label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:CheckBox ID="chk_FollowUp" Enabled="false" runat="server"></asp:CheckBox>
                                                            <asp:Label ID="Label13" runat="server" Text="Follow Up"></asp:Label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:CheckBox ID="chkReport" Enabled="false" runat="server"></asp:CheckBox>
                                                            <asp:Label ID="Label14" runat="server" Text="In Report"></asp:Label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:CheckBox ID="chkPD" Enabled="false" runat="server"></asp:CheckBox>
                                                            <asp:Label ID="Label4" runat="server" Text="Protocol deviation"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row" id="divCRACOMMENT" runat="server">
                                                    <div class="col-md-12">
                                                        <div class="label col-md-2" style="color: Blue">
                                                            CRA Comments:&nbsp;
                                                        </div>
                                                        <div class="col-md-9" style="border-color: LightGrey; border-style: solid; border-width: 1pt;">
                                                            <asp:Literal ID="ltrCRAComment" runat="server" Text='<%# Bind("Comments") %>'></asp:Literal><br />
                                                            <div class="pull-right">
                                                                <asp:Label ID="DTENTERED" ForeColor="darkgreen" Font-Bold="true" runat="server" Text='<%# Bind("DTENTERED") %>'></asp:Label>&nbsp/
                                                                <asp:Label ID="ENTEREDBY" ForeColor="darkgreen" Font-Bold="true" runat="server" Text='<%# Bind("ENTEREDBY") %>'></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row" id="divPMComment" runat="server">
                                                    <div class="col-md-12">
                                                        <div class="label col-md-2" style="color: Blue">
                                                            PM Comments:&nbsp;
                                                        </div>
                                                        <div class="col-md-9" style="border-color: LightGrey; border-style: solid; border-width: 1pt;">
                                                            <asp:Literal ID="ltrPM_COMMENTS" runat="server" Text='<%# Bind("PM_COMMENTS") %>'></asp:Literal><br />
                                                            <div class="pull-right">
                                                                <asp:Label ID="PM_COMMENTS_DAT" ForeColor="darkgreen" Font-Bold="true" runat="server"
                                                                    Text='<%# Bind("PM_COMMENTS_DAT") %>'></asp:Label>&nbsp/
                                                                <asp:Label ID="PM_COMMENTS_BY" ForeColor="darkgreen" Font-Bold="true" runat="server"
                                                                    Text='<%# Bind("PM_COMMENTS_BY") %>'></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row" id="divSponsorComment" runat="server">
                                                    <div class="col-md-12">
                                                        <div class="label col-md-2" style="color: Blue">
                                                            Sponsor Comments:&nbsp;
                                                        </div>
                                                        <div class="col-md-9" style="border-color: LightGrey; border-style: solid; border-width: 1pt;">
                                                            <asp:Literal ID="ltrSPONSOR_COMMENTS" runat="server" Text='<%# Bind("SPONSOR_COMMENTS") %>'></asp:Literal><br />
                                                            <div class="pull-right">
                                                                <asp:Label ID="SPONSOR_COMMENTS_DAT" ForeColor="darkgreen" Font-Bold="true" runat="server"
                                                                    Text='<%# Bind("SPONSOR_COMMENTS_DAT") %>'></asp:Label>&nbsp/
                                                                <asp:Label ID="SPONSOR_COMMENTS_BY" ForeColor="darkgreen" Font-Bold="true" runat="server"
                                                                    Text='<%# Bind("SPONSOR_COMMENTS_BY") %>'></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="label col-md-2" style="color: Blue">
                                                        </div>
                                                        <div class="col-md-11">
                                                            <div class="pull-right" style="display: inline-flex;">
                                                                <asp:Button ID="lbtnEdit" CommandArgument='<%# Bind("ID") %>' CommandName="EDITCOMMENT"
                                                                    CssClass="btn btn-primary btn-sm" runat="server" ToolTip="Edit Comment" Text="Edit Comment" />&nbsp;&nbsp;
                                                                <asp:Button ID="lbtnDelete" CommandArgument='<%# Bind("ID") %>' CommandName="DELETECOMMENT"
                                                                    CssClass="btn btn-danger btn-sm" runat="server" ToolTip="Delete Comment" Text="Delete Comment" />&nbsp;&nbsp;
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                </ItemTemplate>
            </asp:Repeater>
            <asp:HiddenField ID="hdnSectionId" runat="server" />
            <asp:HiddenField ID="hdnSubSectionId" runat="server" />
            <asp:HiddenField ID="hdnAction" runat="server" />
            <asp:HiddenField ID="hdnRECID" runat="server" />
        </div>
    </div>
    </form>
</body>
</html>
