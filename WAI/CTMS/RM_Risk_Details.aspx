<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RM_Risk_Details.aspx.cs"
    Inherits="CTMS.RM_Risk_Details" %>

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
    <%-- <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.min.js"></script>--%>
    <%-- <style>
        .label
        {
    display: inline-block;
max-width: 100%;
margin-bottom: -2px;
font-weight: bold;
font-size: 12px;
margin-left: 9px;
width: 58px;
}
    
    </style>--%>
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
                Risk Details</h3>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <div class="form-horizontal">
        <div class="box-body">
            <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                <label class="col-lg-2 width100px label">
                    Risk ID</label>
                <div class="col-lg-2">
                    <asp:TextBox runat="server" ID="txtRiskID" Enabled="false" Width="100px" CssClass="form-control txtCenter"> 
                    </asp:TextBox>
                </div>
            </div>
            <br />
                    <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                        <label class="col-lg-3 width100px label">
                            Category</label>
                        <div class="col-lg-2">
                            <asp:DropDownList ID="ddlcategory" runat="server" Width="300px" AutoPostBack="true"
                                class="form-control drpControl required" 
                                onselectedindexchanged="ddlcategory_SelectedIndexChanged" >
                            </asp:DropDownList>
                        </div>
                    </div>
                    <br />
                    <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                        <label class="col-lg-3 width100px label">
                            Sub Category</label>
                        <div class="col-lg-2">
                            <asp:DropDownList ID="ddlsubcategory" runat="server" Width="300px" AutoPostBack="true"
                                class="form-control drpControl required" 
                                onselectedindexchanged="ddlsubcategory_SelectedIndexChanged" >
                            </asp:DropDownList>
                        </div>
                    </div>
                    <br />
                    <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                        <label class="col-lg-3 width100px label">
                            Factor</label>
                        <div class="col-lg-2">
                            <asp:DropDownList ID="ddlfactor" Width="300px" runat="server" AutoPostBack="true"
                                class="form-control drpControl required">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <br />
            <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                <label class="col-lg-3 width100px label">
                    Risk Description</label>
                <div class="col-lg-2">
                    <asp:TextBox runat="server" ID="txtRiskCons" Height="36px" Width="600px" TextMode="MultiLine"
                        CssClass="form-control "> 
                    </asp:TextBox>
                </div>
            </div>
            <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                <label class="col-lg-3 width100px label">
                   Risk Impacts</label>
                <div class="col-lg-2">
                    <asp:ListBox ID="lstRiskImpact" runat="server" CssClass="width300px select" SelectionMode="Multiple"
                        Width="600px"></asp:ListBox>
                </div>
            </div>
            <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                <label class="col-lg-3 width100px label">
                    Suggested Mitigation</label>
                <div class="col-lg-2">
                    <asp:TextBox runat="server" ID="txtSugMitig" Height="36px" Width="600px" TextMode="MultiLine"
                        CssClass="form-control "> 
                    </asp:TextBox>
                </div>
            </div>
            <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                <label class="col-lg-3 width100px label">
                    Suggested Risk category</label>
                <div class="col-lg-2">
                    <asp:TextBox runat="server" ID="txtSugRiskCat" Height="36px" Width="600px" TextMode="MultiLine"
                        CssClass="form-control "> 
                    </asp:TextBox>
                </div>
            </div>
            <div class="form-group" style="display: inline-flex; margin-bottom: 6px;width:800px;" ">
                <label class="col-lg-3 width100px label">
                    Risk Nature</label>
                <div class="col-lg-2" >
                <asp:RadioButton Text=" Static" runat="server" ID="radStatic" Checked="true"  GroupName="nature" />&nbsp;&nbsp;
                <asp:RadioButton Text=" Dynamic" runat="server" ID="radDynamic" GroupName="nature" />
                </div>
               <div class="label txtCenter" style="float:right">
                    Key</div>
                <div class="col-lg-2" >
                <asp:RadioButton Text="Yes" runat="server" ID="radyes" Checked="true"  GroupName="key" />&nbsp;&nbsp;
                <asp:RadioButton Text="No" runat="server" ID="radno" GroupName="key" />
                </div>
            </div>
            <br />
            <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                <label class="col-lg-2 width100px label">
                    Transcelerate Category
                </label>
                <div class="col-lg-3 txtCenter ">
                    <asp:TextBox ID="txtTransCat" runat="server" Height="36px" Width="600px" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                </div>
                <%--<div class="label txtCenter">
                    Transcelerate Sub Category</div>
                <div class="col-lg-3 ">
                    <asp:TextBox ID="txtTransSubCat" runat="server" Width="100%" CssClass="form-control txtCenter"></asp:TextBox>
                </div>--%>
            </div>
        </div>
    </div>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                <asp:Button ID="bntSave" runat="server" Text="Save" OnClick="bntSave_Click" Style="margin-left: 10px"
                    CssClass="btn btn-primary btn-sm cls-btnSave" />
            </h3>
        </div>
    </div>
    </form>
</body>
</html>
