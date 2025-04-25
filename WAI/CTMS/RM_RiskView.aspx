<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RM_RiskView.aspx.cs" Inherits="CTMS.RM_RiskView" %>

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
        
        
    </script>
</head>
<body>
    <form id="form1" method="post" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <div class="box box-warning">
            <div style="text-align: center;">
                <div class="box-header">
                    <h3 class="box-title">
                        Risk Details</h3>
                </div>
                <div class="row">
                    <div class="form-group has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div style="margin-left: 100px;">
        <div class="form-horizontal">
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-2 width100px label">
                        Risk ID</label>
                    <div class="col-lg-2">
                        <asp:Label runat="server" ID="txtRiskID" Enabled="false" CssClass="form-control"
                            Width="550px"> 
                        </asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-horizontal">
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-3 width100px label">
                        Category</label>
                    <div class="col-lg-2">
                        <asp:Label runat="server" ID="lblCateg" CssClass="form-control" Height="36px" Width="550px"> 
                        </asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-horizontal">
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-3 width100px label">
                        Sub Category</label>
                    <div class="col-lg-2">
                        <asp:Label runat="server" CssClass="form-control" ID="lblSubCat" Height="36px" Width="550px"> 
                        </asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-horizontal">
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-3 width100px label">
                        Factor :
                    </label>
                    <div class="col-lg-2">
                        <asp:Label runat="server" CssClass="form-control" ID="lblfactor" Height="36px" Width="550px"> 
                        </asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-horizontal">
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-3 width100px label">
                        Risk Description :
                    </label>
                    <div class="col-lg-2">
                        <asp:Label runat="server" CssClass="form-control" ID="txtRiskCons" Height="36px"
                            Width="550px"> 
                        </asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-horizontal">
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-3 width100px label">
                        Risk Impacts :
                    </label>
                    <div class="col-lg-2">
                        <asp:Label runat="server" ID="txtRiskImpact" CssClass="form-control" Height="36px"
                            Width="550px"> 
                        </asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-horizontal">
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-3 width100px label">
                        Suggested Mitigation :
                    </label>
                    <div class="col-lg-2">
                        <asp:Label runat="server" ID="txtSugMitig" CssClass="form-control" Height="36px"
                            Width="550px"> 
                        </asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-horizontal">
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-3 width100px label">
                        Suggested Risk category :
                    </label>
                    <div class="col-lg-2">
                        <asp:Label runat="server" ID="txtSugRiskCat" CssClass="form-control" Height="36px"
                            Width="550px"> 
                        </asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-horizontal">
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-3 width100px label">
                        Risk Nature :
                    </label>
                    <div class="col-lg-2">
                        <asp:Label ID="lblnature" runat="server" CssClass="form-control" Width="550px" Height="36px"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-horizontal">
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-2 width100px label">
                        Transcelerate Category :
                    </label>
                    <div class="col-lg-3">
                        <asp:Label ID="txtTransCat" runat="server" CssClass="form-control" Width="550px"
                            Height="36px"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-horizontal">
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-2 width100px label">
                        Transcelerate Sub Category :
                    </label>
                    <div class="col-lg-3">
                        <asp:Label ID="txtTransSubCat" runat="server" CssClass="form-control" Width="550px"
                            Height="36px"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>
    </form>
</body>
</html>
