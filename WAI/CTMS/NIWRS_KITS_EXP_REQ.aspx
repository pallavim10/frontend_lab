<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NIWRS_KITS_EXP_REQ.aspx.cs" Inherits="CTMS.NIWRS_KITS_EXP_REQ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="CommonFunctionsJs/SAE/SAE_ConfirmMsg.js"></script>
    <script src="Scripts/Select2.js" type="text/javascript"></script>

    <style type="text/css">
        .select2-container .select2-selection--multiple {
            min-height: 60px;
            width: 200px;
        }
    </style>
    <script language="javascript" type="text/javascript">

        function pageLoad() {
            $('.select').select2();

            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: true
            });

            $(".Datatable").parent().parent().addClass('fixTableHead');
        }
    </script>
  <%--  <script type="text/javascript" src="js/MaxLength.min.js"></script>
    <script type="text/javascript" src="js/MaxLength_Limit.js"></script>--%>
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            $(".nav-tabs a").click(function (event) {
                event.preventDefault();
                $(this).parent().addClass("active");
                $(this).parent().siblings().removeClass("active");
                var tab = $(this).attr("href");
                $(this).parent().parent().parent().find('.tab-content').not(tab).css("display", "none");
                $(tab).fadeIn();
            });
        });

        function bindOptionValues() {
            var optionFields = $(".OptionValues").toArray();
            for (a = 0; a < optionFields.length; ++a) {

                var avaTag = "";
                if ($(optionFields[a]).attr('id') == 'MainContent_txtLISTValue1') {
                    avaTag = $('#MainContent_hfValue1').val().split(',');
                }
                else if ($(optionFields[a]).attr('id') == 'MainContent_txtLISTValue2') {
                    avaTag = $('#MainContent_hfValue2').val().split(',');
                }
                else if ($(optionFields[a]).attr('id') == 'MainContent_txtLISTValue3') {
                    avaTag = $('#MainContent_hfValue3').val().split(',');
                }
                else if ($(optionFields[a]).attr('id') == 'MainContent_txtLISTValue4') {
                    avaTag = $('#MainContent_hfValue4').val().split(',');
                }
                else if ($(optionFields[a]).attr('id') == 'MainContent_txtLISTValue5') {
                    avaTag = $('#MainContent_hfValue5').val().split(',');
                }

                $(optionFields[a]).autocomplete({
                    source: avaTag, minLength: 0
                }).on('focus', function () { $(this).keydown(); });
            }
        }

        $(function () {
            var optionFields = $(".OptionValues").toArray();
            for (a = 0; a < optionFields.length; ++a) {

                var avaTag = "";
                if ($(optionFields[a]).attr('id') == 'MainContent_txtLISTValue1') {
                    avaTag = $('#MainContent_hfValue1').val().split(',');
                }
                else if ($(optionFields[a]).attr('id') == 'MainContent_txtLISTValue2') {
                    avaTag = $('#MainContent_hfValue2').val().split(',');
                }
                else if ($(optionFields[a]).attr('id') == 'MainContent_txtLISTValue3') {
                    avaTag = $('#MainContent_hfValue3').val().split(',');
                }
                else if ($(optionFields[a]).attr('id') == 'MainContent_txtLISTValue4') {
                    avaTag = $('#MainContent_hfValue4').val().split(',');
                }
                else if ($(optionFields[a]).attr('id') == 'MainContent_txtLISTValue5') {
                    avaTag = $('#MainContent_hfValue5').val().split(',');
                }

                $(optionFields[a]).autocomplete({
                    source: avaTag, minLength: 0
                }).on('focus', function () { $(this).keydown(); });
            }
        });

        $(document).on("click", ".cls-btnSave1", function () {
            var test = "0";

            $('.required1').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "0" || value == null) {
                        if ($(this).hasClass("select") == true) {
                            $(this).next("span").addClass("brd-1px-redimp");
                            test = "1";
                        }
                        else {
                            $(this).addClass("brd-1px-redimp");
                            test = "1";
                        }
                    }
                }
                else if (ctrl == "text" || ctrl == "textarea" || ctrl == "password") {
                    if (value == "") {
                        if ($(this).hasClass("ckeditor")) {
                            $(this).next('div').addClass("brd-1px-redimp");
                        }
                        else {
                            $(this).addClass("brd-1px-redimp");
                        }
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
    <style type="text/css">
        .circleCountGreen {
            width: 35px;
            height: 19px;
            border-radius: 50%;
            font-size: 12px;
            color: black;
            text-align: center;
            background: #41a7f1;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-success">
        <div class="box-header">
            <h3 class="box-title">Generate Kit Expiry Request
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
            </div>
            <div class="box-body">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-1">
                                <label>
                                    Search Kits :</label>
                            </div>
                            <div class="col-md-10">
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="drpLISTField1" runat="server" CssClass="form-control required1 width200px"
                                            AutoPostBack="true" OnSelectedIndexChanged="drpLISTField1_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Kit Number" Value="KITNO"></asp:ListItem>
                                            <asp:ListItem Text="Block Number" Value="LOTNO"></asp:ListItem>
                                            <asp:ListItem Text="Expiry Date" Value="EXPIRY_DATE"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="drpLISTCondition1" runat="server" CssClass="form-control required1" Width="100%">
                                            <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                            <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                            <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                            <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                            <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                            <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                            <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                            <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                            <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                            <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:HiddenField runat="server" ID="hfValue1" />
                                        <asp:TextBox runat="server" CssClass="OptionValues  form-control" ID="txtLISTValue1"
                                            Width="100%"> </asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="drpLISTAndOr1" runat="server" CssClass="form-control" Width="100%">
                                            <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                            <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                            <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="drpLISTField2" runat="server" CssClass="form-control width200px"
                                            AutoPostBack="true" OnSelectedIndexChanged="drpLISTField2_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Kit Number" Value="KITNO"></asp:ListItem>
                                            <asp:ListItem Text="Block Number" Value="LOTNO"></asp:ListItem>
                                            <asp:ListItem Text="Expiry Date" Value="EXPIRY_DATE"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="drpLISTCondition2" runat="server" CssClass="form-control" Width="100%">
                                            <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                            <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                            <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                            <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                            <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                            <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                            <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                            <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                            <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                            <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:HiddenField runat="server" ID="hfValue2" />
                                        <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue2"
                                            Width="100%"> </asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="drpLISTAndOr2" runat="server" CssClass="form-control" Width="100%">
                                            <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                            <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                            <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="drpLISTField3" runat="server" CssClass="form-control width200px"
                                            AutoPostBack="true" OnSelectedIndexChanged="drpLISTField3_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Kit Number" Value="KITNO"></asp:ListItem>
                                            <asp:ListItem Text="Block Number" Value="LOTNO"></asp:ListItem>
                                            <asp:ListItem Text="Expiry Date" Value="EXPIRY_DATE"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="drpLISTCondition3" runat="server" CssClass="form-control" Width="100%">
                                            <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                            <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                            <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                            <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                            <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                            <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                            <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                            <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                            <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                            <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:HiddenField runat="server" ID="hfValue3" />
                                        <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue3"
                                            Width="100%"> </asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="drpLISTAndOr3" runat="server" CssClass="form-control" Width="100%">
                                            <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                            <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                            <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="drpLISTField4" runat="server" CssClass="form-control width200px"
                                            AutoPostBack="true" OnSelectedIndexChanged="drpLISTField4_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Kit Number" Value="KITNO"></asp:ListItem>
                                            <asp:ListItem Text="Block Number" Value="LOTNO"></asp:ListItem>
                                            <asp:ListItem Text="Expiry Date" Value="EXPIRY_DATE"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="drpLISTCondition4" runat="server" CssClass="form-control" Width="100%">
                                            <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                            <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                            <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                            <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                            <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                            <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                            <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                            <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                            <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                            <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:HiddenField runat="server" ID="hfValue4" />
                                        <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue4"
                                            Width="100%"> </asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="drpLISTAndOr4" runat="server" CssClass="form-control" Width="100%">
                                            <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                            <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                            <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="drpLISTField5" runat="server" CssClass="form-control width200px"
                                            AutoPostBack="true" OnSelectedIndexChanged="drpLISTField5_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Kit Number" Value="KITNO"></asp:ListItem>
                                            <asp:ListItem Text="Block Number" Value="LOTNO"></asp:ListItem>
                                            <asp:ListItem Text="Expiry Date" Value="EXPIRY_DATE"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="drpLISTCondition5" runat="server" CssClass="form-control" Width="100%">
                                            <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                            <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                            <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                            <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                            <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                            <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                            <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                            <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                            <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                            <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:HiddenField runat="server" ID="hfValue5" />
                                        <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue5"
                                            Width="100%"> </asp:TextBox>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-1">
                                <label>
                                    Select Level :</label>
                            </div>
                            <div class="col-md-11">
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:ListBox ID="drplevel" runat="server" SelectionMode="Multiple" CssClass="form-control required1 select width200px" AutoPostBack="true" OnSelectedIndexChanged="drplevel_SelectedIndexChanged">
                                            <asp:ListItem Text="Central" Value="Central"></asp:ListItem>
                                            <asp:ListItem Text="Country" Value="Country"></asp:ListItem>
                                            <asp:ListItem Text="Site" Value="Site"></asp:ListItem>
                                        </asp:ListBox>
                                    </div>
                                    <div id="divCountry" runat="server" visible="false">
                                        <div class="col-md-1">
                                            <label>
                                                Select Country :</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:ListBox ID="lstcountry" runat="server" SelectionMode="Multiple" CssClass="form-control select width200px" AutoPostBack="true" OnSelectedIndexChanged="lstcountry_SelectedIndexChanged"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div id="divSite" runat="server" visible="false">
                                        <div class="col-md-1">
                                            <label>
                                                Select Site :</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:ListBox ID="lstsite" runat="server" CssClass="form-control select " Width="100%"></asp:ListBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                        <div class="col-md-12 txtCenter">
                            <asp:Button ID="btnSearch" Text="Show Kits" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave1"
                                OnClick="btnSearch_Click" />
                        </div>
                    </div>
                    <br />
                    <div runat="server" visible="false" id="divREQ">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <label>
                                        Please enter reason for expiry update :</label>
                                </div>
                                <div class="col-md-9">
                                    <asp:TextBox runat="server" ID="txtREQUEST_COMMENT" MaxLength="200" TextMode="MultiLine" CssClass="form-control required width300px" Rows="4"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <label>
                                        Please enter new Expiry Date:</label>
                                </div>
                                <div class="col-md-9">
                                    <asp:TextBox runat="server" ID="txtREQUEST_EXPDAT" CssClass="form-control required txtDate"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    &nbsp;
                                </div>
                                <div class="col-md-6">
                                    <asp:Button ID="btnSubmit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                        OnClick="btnSubmit_Click" />
                                </div>
                                <div class="col-md-3">
                                </div>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />
    <div runat="server" visible="false" id="divKITS">
        <div id="tabscontainer1" class="nav-tabs-custom" runat="server">
            <div class="nav-tabs-custom">
                <ul class="nav nav-tabs">
                    <li id="li1_1" runat="server" class="active"><a href="#tab1-1" data-toggle="tab">Central Inventory &nbsp;&nbsp;:&nbsp;&nbsp;
                                                        <asp:Label runat="server" ID="lbl_NIWRS_KITS_POOL"></asp:Label>
                    </a></li>
                    <li id="li1_2" runat="server"><a href="#tab1-2" data-toggle="tab">Central to Country Order (In Transit) &nbsp;&nbsp;:&nbsp;&nbsp;
                                                        <asp:Label runat="server" ID="lbl_NIWRS_KITS_COUNTRY_ORDERS"></asp:Label>
                    </a></li>
                </ul>
                <div class="tab">
                    <div id="tab1-1" class="tab-content current">
                        <div class="form-group">
                            <asp:GridView ID="grd_NIWRS_KITS_POOL" runat="server" AutoGenerateColumns="true"
                                CssClass="table table-bordered table-striped Datatable txtCenter" OnPreRender="GridView_PreRender">
                            </asp:GridView>
                        </div>
                    </div>
                    <div id="tab1-2" class="tab-content">
                        <div class="form-group">
                            <asp:GridView ID="grd_NIWRS_KITS_COUNTRY_ORDERS" runat="server" AutoGenerateColumns="true"
                                CssClass="table table-bordered table-striped Datatable txtCenter" OnPreRender="GridView_PreRender">
                            </asp:GridView>
                        </div>
                    </div>
                    <br />
                </div>
            </div>
        </div>
        <br />
        <div id="tabscontainer2" class="nav-tabs-custom" runat="server">
            <div class="nav-tabs-custom">
                <ul class="nav nav-tabs">
                    <li id="li2_1" runat="server" class="active"><a href="#tab2-1" data-toggle="tab">Country Inventory &nbsp;&nbsp;:&nbsp;&nbsp;
                                                        <asp:Label runat="server" ID="lbl_NIWRS_KITS_COUNTRY"></asp:Label>
                    </a></li>
                    <li id="li2_2" runat="server"><a href="#tab2-2" data-toggle="tab">Country to Site Order (In Transit) &nbsp;&nbsp;:&nbsp;&nbsp;
                                                        <asp:Label runat="server" ID="lbl_NIWRS_KITS_SITE_ORDERS"></asp:Label>
                    </a></li>
                    <li id="li2_3" runat="server"><a href="#tab2-3" data-toggle="tab">Country to Country Order (In Transit) &nbsp;&nbsp;:&nbsp;&nbsp;
                                                        <asp:Label runat="server" ID="lbl_NIWRS_KITS_COUNTRY_TRANSF_ORDERS"></asp:Label>
                    </a></li>
                    <li id="li2_4" runat="server"><a href="#tab2-4" data-toggle="tab">Country to Central Order (In Transit) &nbsp;&nbsp;:&nbsp;&nbsp;
                                                        <asp:Label runat="server" ID="lbl_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS"></asp:Label>
                    </a></li>
                </ul>
                <div class="tab">
                    <div id="tab2-1" class="tab-content current">
                        <div class="form-group">
                            <asp:GridView ID="grd_NIWRS_KITS_COUNTRY" runat="server" AutoGenerateColumns="true"
                                CssClass="table table-bordered table-striped Datatable txtCenter" OnPreRender="GridView_PreRender">
                            </asp:GridView>
                        </div>
                    </div>
                    <div id="tab2-2" class="tab-content">
                        <div class="form-group">
                            <asp:GridView ID="grd_NIWRS_KITS_SITE_ORDERS" runat="server" AutoGenerateColumns="true"
                                CssClass="table table-bordered table-striped Datatable txtCenter" OnPreRender="GridView_PreRender">
                            </asp:GridView>
                        </div>
                    </div>
                    <div id="tab2-3" class="tab-content">
                        <div class="form-group">
                            <asp:GridView ID="grd_NIWRS_KITS_COUNTRY_TRANSF_ORDERS" runat="server" AutoGenerateColumns="true"
                                CssClass="table table-bordered table-striped Datatable txtCenter" OnPreRender="GridView_PreRender">
                            </asp:GridView>
                        </div>
                    </div>
                    <div id="tab2-4" class="tab-content">
                        <div class="form-group">
                            <asp:GridView ID="grd_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS" runat="server" AutoGenerateColumns="true"
                                CssClass="table table-bordered table-striped Datatable txtCenter" OnPreRender="GridView_PreRender">
                            </asp:GridView>
                        </div>
                    </div>
                    <br />
                </div>
            </div>
        </div>
        <br />
        <div id="tabscontainer3" class="nav-tabs-custom" runat="server">
            <div class="nav-tabs-custom">
                <ul class="nav nav-tabs">
                    <li id="li3_1" runat="server" class="active"><a href="#tab3-1" data-toggle="tab">Site Inventory &nbsp;&nbsp;:&nbsp;&nbsp;
                                                        <asp:Label runat="server" ID="lbl_NIWRS_KITS_SITE"></asp:Label>
                    </a></li>
                    <li id="li3_2" runat="server"><a href="#tab3-2" data-toggle="tab">Site to Site Order (In Transit) &nbsp;&nbsp;:&nbsp;&nbsp;
                                                        <asp:Label runat="server" ID="lbl_NIWRS_KITS_SITE_TRANSF_ORDERS"></asp:Label>
                    </a></li>
                    <li id="li3_3" runat="server"><a href="#tab3-3" data-toggle="tab">Site to Country Order (In Transit) &nbsp;&nbsp;:&nbsp;&nbsp;
                                                        <asp:Label runat="server" ID="lbl_NIWRS_KITS_SITE_COUNTRY_ORDERS"></asp:Label>
                    </a></li>
                </ul>
                <div class="tab">
                    <div id="tab3-1" class="tab-content current">
                        <div class="form-group">
                            <asp:GridView ID="grd_NIWRS_KITS_SITE" runat="server" AutoGenerateColumns="true"
                                CssClass="table table-bordered table-striped Datatable txtCenter" OnPreRender="GridView_PreRender">
                            </asp:GridView>
                        </div>
                    </div>
                    <div id="tab3-2" class="tab-content">
                        <div class="form-group">
                            <asp:GridView ID="grd_NIWRS_KITS_SITE_TRANSF_ORDERS" runat="server" AutoGenerateColumns="true"
                                CssClass="table table-bordered table-striped Datatable txtCenter" OnPreRender="GridView_PreRender">
                            </asp:GridView>
                        </div>
                    </div>
                    <div id="tab3-3" class="tab-content">
                        <div class="form-group">
                            <asp:GridView ID="grd_NIWRS_KITS_SITE_COUNTRY_ORDERS" runat="server" AutoGenerateColumns="true"
                                CssClass="table table-bordered table-striped Datatable txtCenter" OnPreRender="GridView_PreRender">
                            </asp:GridView>
                        </div>
                    </div>
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
