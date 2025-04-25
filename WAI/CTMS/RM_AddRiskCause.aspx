<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RM_AddRiskCause.aspx.cs"
    Inherits="CTMS.RM_AddRiskCause" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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


        // js prototype
        if (typeof (Number.prototype.isBetween) === "undefined") {
            Number.prototype.isBetween = function (min, max, notBoundaries) {
                var between = false;
                if (notBoundaries) {
                    if ((this < max) && (this > min)) between = true;
                } else {
                    if ((this <= max) && (this >= min)) between = true;
                }
                return between;
            }
        }


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
        function pageLoad() {
            //CalculateRPN();
            $('.select').select2();
            $('.txtDate').each(function (index, element) {
                $(element).pikaday({
                    field: element,
                    // trigger: $(element).closest('div').find('.datepicker-button').get(0), // <<<<
                    // firstDay: 1,
                    //position: 'top right',
                    // minDate: new Date('2000-01-01'),
                    // maxDate: new Date('9999-12-31'),
                    format: 'DD-MMM-YYYY',
                    //  defaultDate: new Date(''),
                    //setDefaultDate: false,
                    yearRange: [1910, 2050]
                });
            });
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
                Risk Cause</h3>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div class="form-horizontal">
            <div class="box-body">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <asp:Label ID="Label2" CssClass="label" runat="server" Text="Select Cause :"></asp:Label>
                            </div>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="ddlCause" CssClass="form-control required1"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlCause_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <asp:Label ID="Label3" CssClass="label" runat="server" Text="Select Sub-Cause :"></asp:Label>
                            </div>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="ddlSubCause" CssClass="form-control required1">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Enter Comment :"></asp:Label>
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtComment" runat="server" CssClass="form-control" Height="100px"
                                    Width="580px" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-3">
                                &nbsp;</div>
                            <div class="col-md-6">
                                <asp:Button ID="btnAdd" runat="server" Text="Add" Style="margin-left: 10px" CssClass="btn btn-primary btn-sm cls-btnSave1"
                                    OnClick="btnAdd_Click" />
                                <asp:Button ID="btnUpdate" runat="server" Text="Update" Style="margin-left: 10px"
                                    CssClass="btn btn-primary btn-sm cls-btnSave1" OnClick="btnUpdate_Click" />
                            </div>
                        </div>
                    </div>
                    <br />
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
