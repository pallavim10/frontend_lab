<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Comm_AddEvent.aspx.cs"
    Inherits="CTMS.Comm_AddEvent" %>

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
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <script src="js/Input%20Mask/jquery.inputmask.bundle.js" type="text/javascript"></script>
    <script type="text/javascript">
        function pageLoad() {
            $('.select').select2();
            $('.txtDate').each(function (index, element) {
                $(element).pikaday({
                    field: element,
                    format: 'DD-MMM-YYYY',
                    yearRange: [1910, 2050]
                });
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
        }
      );
            });
        }
    </script>
    <script language="javascript" type="text/javascript">
        $(function () {

            $('.txtDateMask').inputmask(
                    "dd/mm/yyyy", {
                        placeholder: "dd/mm/yyyy"
                    }
                                      );
        });

        $(function () {

            $('.txtTime').inputmask(
        "hh:mm", {
            placeholder: "HH:MM",
            insertMode: false,
            showMaskOnHover: false,
            hourFormat: "24"
        }
      );

        });
    </script>
    <script type="text/javascript">

        function DecodeUrl(url) {

            var dec = decodeURI(url);

            window.location.href = dec;
        }


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

        function CloseCurrent() {

            window.close();

        }
    </script>
</head>
<body>
    <form id="form1" method="post" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                New Event</h3>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div class="form-horizontal">
            <div class="col-md-12" style="display: inline-flex; margin-bottom: 6px;">
                <label class="col-md-3 width100px label">
                    Project ID</label>
                <div class="col-md-9">
                    <asp:DropDownList ID="Drp_Project" Width="200px" class="form-control required" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="form-horizontal">
            <div class="col-md-12" style="display: inline-flex; margin-bottom: 6px;">
                <label class="col-md-3 width100px label">
                    Type</label>
                <div class="col-md-9">
                    <asp:DropDownList ID="Drp_Type" Width="200px" class="form-control required" runat="server">
                        <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Internal" Value="Internal"></asp:ListItem>
                        <asp:ListItem Text="External" Value="External"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="form-horizontal">
            <div class="col-md-12" style="display: inline-flex; margin-bottom: 6px;">
                <label class="col-md-3 width100px label">
                    Nature</label>
                <div class="col-md-9">
                    <asp:DropDownList ID="Drp_Nature" Width="200px" class="form-control required" runat="server">
                        <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approval" Value="Approval"></asp:ListItem>
                        <asp:ListItem Text="Decision" Value="Decision"></asp:ListItem>
                        <asp:ListItem Text="Deviation" Value="Deviation"></asp:ListItem>
                        <asp:ListItem Text="Document Review" Value="Document Review"></asp:ListItem>
                        <asp:ListItem Text="General Communication" Value="General Communication"></asp:ListItem>
                        <asp:ListItem Text="Risk" Value="Risk"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="form-horizontal">
                    <div class="col-md-12" style="display: inline-flex; margin-bottom: 6px;">
                        <label class="col-md-3 width100px label">
                            Department</label>
                        <div class="col-md-9">
                            <asp:ListBox ID="list_Dept" runat="server" CssClass="select required" AutoPostBack="true"
                                SelectionMode="Multiple" Width="500px" OnSelectedIndexChanged="list_Dept_SelectedIndexChanged">
                            </asp:ListBox>
                        </div>
                    </div>
                </div>
                <div class="form-horizontal">
                    <div class="col-md-12" style="display: inline-flex; margin-bottom: 6px;">
                        <label class="col-md-3 width100px label">
                            Reference</label>
                        <div class="col-md-9">
                            <asp:DropDownList ID="Drp_Refer" Width="200px" class="form-control required" runat="server">
                                <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="CRO Personnel" Value="CRO Personnel"></asp:ListItem>
                                <asp:ListItem Text="Data Integrity" Value="Data Integrity"></asp:ListItem>
                                <asp:ListItem Text="Data Quality" Value="Data Quality"></asp:ListItem>
                                <asp:ListItem Text="Data Review" Value="Data Review"></asp:ListItem>
                                <asp:ListItem Text="Document Management" Value="Document Management"></asp:ListItem>
                                <asp:ListItem Text="Ethics Committee" Value="Ethics Committee"></asp:ListItem>
                                <asp:ListItem Text="Medical Management" Value="Medical Management"></asp:ListItem>
                                <asp:ListItem Text="Process Deviation" Value="Process Deviation"></asp:ListItem>
                                <asp:ListItem Text="Protocol Deviation" Value="Protocol Deviation"></asp:ListItem>
                                <asp:ListItem Text="Regulatory" Value="Regulatory"></asp:ListItem>
                                <asp:ListItem Text="Risk Management" Value="Risk Management"></asp:ListItem>
                                <asp:ListItem Text="Safety-AE" Value="Safety-AE"></asp:ListItem>
                                <asp:ListItem Text="Safety-SAE" Value="Safety-SAE"></asp:ListItem>
                                <asp:ListItem Text="Site Personnel" Value="Site Personnel"></asp:ListItem>
                                <asp:ListItem Text="SOP" Value="SOP"></asp:ListItem>
                                <asp:ListItem Text="Sponsor Decision" Value="Sponsor Decision"></asp:ListItem>
                                <asp:ListItem Text="Training" Value="Training"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="form-horizontal">
                    <div class="col-md-12" style="display: inline-flex; margin-bottom: 6px;">
                        <label class="col-md-3 width100px label">
                            Event</label>
                        <div class="col-md-9">
                            <asp:DropDownList ID="ddlEvent" Width="500px" class="form-control required" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="form-horizontal">
                    <div class="col-md-12" style="display: inline-flex; margin-bottom: 6px;">
                        <label class="col-md-3 width100px label">
                            DateTime of Event</label>
                        <div class="col-md-9" style="display: inline-flex;">
                            <asp:TextBox ID="txtEventDate" runat="server" autocomplete="off" Width="500px" CssClass="form-control required txtDate"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="form-horizontal">
            <div class="col-md-12" style="display: inline-flex; margin-bottom: 6px;">
                <label class="col-md-3 width100px label">
                    Notes</label>
                <div class="col-md-9">
                    <asp:TextBox runat="server" ID="txtNote" CssClass="form-control" Height="50px" TextMode="MultiLine"
                        Width="500px"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="form-horizontal">
            <div class="col-md-12" style="margin-bottom: 6px;">
                <div style="margin-left: 25%;">
                    <asp:LinkButton runat="server" ID="lbtnSend" Text="Submit" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave"
                        OnClick="lbtnSubmit_Click"></asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton runat="server" ID="lbtnCancel" Text="Cancel" ForeColor="White" CssClass="btn btn-primary btn-sm"
                        OnClientClick="CloseCurrent();"></asp:LinkButton>
                </div>
            </div>
        </div>
        <br />
    </div>
    </form>
</body>
</html>
