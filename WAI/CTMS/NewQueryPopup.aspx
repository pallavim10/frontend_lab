<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewQueryPopup.aspx.cs"
    Inherits="CTMS.NewQueryPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="UTF-8">
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
    <!-- for pikaday datepicker//-->
    <link href="Styles/pikaday.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/moment.js" type="text/javascript"></script>
    <script src="Scripts/pikaday.js" type="text/javascript"></script>
    <script src="Scripts/pikaday.jquery.js" type="text/javascript"></script>
    <!-- for pikaday datepicker//-->
    <!-- for jquery Datatable.1.10.15//-->
    <script src="Scripts/Datatable.1.10.15.UI.js" type="text/javascript"></script>
    <script src="Scripts/Datatable1.10.15.js" type="text/javascript"></script>
    <!-- for jquery Datatable.1.10.15//-->
    <script src="js/AdminLTE/app.js" type="text/javascript"></script>
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        function opeDMQuery(PVID, RECID) {

            alert('Some eCRF queries are already exists.')

            var test = "MM_DMQueryList.aspx?PVID=" + PVID + "&RECID=" + RECID;
            var strWinProperty = "scrollbars=no,resizable=no,status=no,location=no,toolbar=no,menubar=no, width=0,height=0,left=-1000,top=-1000";
            window.open(test, '_blank', strWinProperty);

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
            $(".tabs-menu a").click(function (event) {
                event.preventDefault();
                $(this).parent().addClass("current");
                $(this).parent().siblings().removeClass("current");
                var tab = $(this).attr("href");
                $(".tab-content").not(tab).css("display", "none");
                $(tab).fadeIn();
            });
        });

        function pageLoad() {
            $(".Datatable").DataTable();
            $('.txtDate').each(function (index, element) {
                $(element).pikaday({
                    field: element,
                    format: 'DD-MMM-YYYY',
                    yearRange: [1910, 2050]
                });
            });
        }


    </script>
    <style>
        .txt_center {
            text-align: center;
        }

        .margin-right5 {
            margin-right: 5px;
        }

        .margin-right10 {
            margin-right: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="box box-warning">
            <div class="box-header">
                <h3 class="box-title">Raise New Query</h3>
            </div>
            <div class="row">
                <div class="lblError">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    <asp:HiddenField runat="server" ID="hdnLISTING_ID" />
                    <asp:HiddenField runat="server" ID="hdnPVID" />
                    <asp:HiddenField runat="server" ID="hdnRECID" />
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-xs-10">
                        <div class="col-xs-3">
                            <label>
                                Site ID :
                            </label>
                        </div>
                        <div class="col-xs-4">
                            <asp:Label runat="server" ID="lblSITEID" />
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-xs-10">
                        <div class="col-xs-3">
                            <label>
                                Subject ID :
                            </label>
                        </div>
                        <div class="col-xs-4">
                            <asp:Label runat="server" ID="lblSUBJID" />
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-xs-10">
                        <div class="col-xs-3">
                            <label>
                                Listing Name :
                            </label>
                        </div>
                        <div class="col-xs-4">
                            <asp:Label runat="server" ID="lblLISTING" />
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-xs-10">
                        <div class="col-xs-3">
                            <label>
                                Enter Query Text :
                            </label>
                        </div>
                        <div class="col-xs-7">
                            <asp:TextBox ID="txtQueryDetail" runat="server" CssClass=" form-control required"
                                TextMode="MultiLine" Width="100%" Rows="4"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-xs-10 text-center">
                        <asp:Button ID="bntSave" runat="server" Text="Save" CssClass="btn btn-primary btn-sm cls-btnSave"
                            OnClick="bntSave_Click" />
                    </div>
                </div>
                <br />
            </div>
        </div>
    </form>
</body>
</html>
