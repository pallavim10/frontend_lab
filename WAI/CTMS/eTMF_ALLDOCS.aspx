<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="eTMF_ALLDOCS.aspx.cs" Inherits="CTMS.eTMF_ALLDOCS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- jvectormap -->
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <script src="js/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js" type="text/javascript"></script>
    <script src="js/plugins/jvectormap/jquery-jvectormap-world-mill-en.js" type="text/javascript"></script>
    <script src="Scripts/ClientValidation.js" type="text/javascript"></script>
    <!-- for Jquery Popup//-->
    <script src="https://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <link href="Styles/Jquery.ui.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/CommonFunction.js" type="text/javascript"></script>

    <link href="CommonStyles/eTMF/eTMF_GrdLayers.css" rel="stylesheet" />
    <script type="text/javascript" src="CommonFunctionsJs/eTMF/eTMF_OpenDoc.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/eTMF/eTMF_History.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/eTMF/eTMF_DivExpandCollapse.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/eTMF/eTMF_ChangeStatus.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/eTMF/eTMF_Event_Trail.js"></script>

    <script>
        function bindOptionValues() {
            var colorFields = $(".OptionValues").toArray();
            for (a = 0; a < colorFields.length; ++a) {

                var avaTag = "";
                if ($(colorFields[a]).attr('id') == 'MainContent_txtLISTValue1') {
                    avaTag = $('#MainContent_hfValue1').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'MainContent_txtLISTValue2') {
                    avaTag = $('#MainContent_hfValue2').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'MainContent_txtLISTValue3') {
                    avaTag = $('#MainContent_hfValue3').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'MainContent_txtLISTValue4') {
                    avaTag = $('#MainContent_hfValue4').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'MainContent_txtLISTValue5') {
                    avaTag = $('#MainContent_hfValue5').val().split(',');
                }

                $(colorFields[a]).autocomplete({
                    source: avaTag, minLength: 0
                }).on('focus', function () { $(this).keydown(); });
            }
        }

        $(function () {
            var colorFields = $(".OptionValues").toArray();
            for (a = 0; a < colorFields.length; ++a) {

                var avaTag = "";
                if ($(colorFields[a]).attr('id') == 'MainContent_txtLISTValue1') {
                    avaTag = $('#MainContent_hfValue1').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'MainContent_txtLISTValue2') {
                    avaTag = $('#MainContent_hfValue2').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'MainContent_txtLISTValue3') {
                    avaTag = $('#MainContent_hfValue3').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'MainContent_txtLISTValue4') {
                    avaTag = $('#MainContent_hfValue4').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'MainContent_txtLISTValue5') {
                    avaTag = $('#MainContent_hfValue5').val().split(',');
                }

                $(colorFields[a]).autocomplete({
                    source: avaTag, minLength: 0
                }).on('focus', function () { $(this).keydown(); });
            }
        });

        $(document).on("click", ".cls-btnSave", function () {
            var test = "0";

            var colorFields = $(".ANDORCONDITION").toArray();
            for (a = 0; a < colorFields.length; ++a) {

                var avaTag = "";
                if ($(colorFields[a]).attr('id') == 'MainContent_drpLISTAndOr1') {

                    if ($("#MainContent_drpLISTAndOr1").val() != "0") {

                        if ($("#MainContent_drpLISTField2").val() == "0") {
                            $("#MainContent_drpLISTField2").addClass("brd-1px-redimp");
                            test = "1";
                        }
                        else {
                            $("#MainContent_drpLISTField2").removeClass("brd-1px-redimp");
                            test = "0";
                        }

                        if ($("#MainContent_drpLISTCondition2").val() == "-1") {
                            $("#MainContent_drpLISTCondition2").addClass("brd-1px-redimp");
                            test = "1";
                        }
                        else {
                            $("#MainContent_drpLISTCondition2").removeClass("brd-1px-redimp");
                            test = "0";
                        }
                    }
                    else {

                        $("#MainContent_drpLISTField2").removeClass("brd-1px-redimp");
                        $("#MainContent_drpLISTCondition2").removeClass("brd-1px-redimp");
                        test = "0";

                    }
                }
                else if ($(colorFields[a]).attr('id') == 'MainContent_drpLISTAndOr2') {

                    if ($("#MainContent_drpLISTAndOr2").val() != "0") {

                        if ($("#MainContent_drpLISTField3").val() == "0") {
                            $("#MainContent_drpLISTField3").addClass("brd-1px-redimp");
                            test = "1";
                        }
                        else {
                            $("#MainContent_drpLISTField3").removeClass("brd-1px-redimp");
                            test = "0";
                        }

                        if ($("#MainContent_drpLISTCondition3").val() == "-1") {
                            $("#MainContent_drpLISTCondition3").addClass("brd-1px-redimp");
                            test = "1";
                        }
                        else {
                            $("#MainContent_drpLISTCondition3").removeClass("brd-1px-redimp");
                            test = "0";
                        }
                    }
                    else {

                        $("#MainContent_drpLISTField3").removeClass("brd-1px-redimp");
                        $("#MainContent_drpLISTCondition3").removeClass("brd-1px-redimp");
                        test = "0";

                    }
                }
                else if ($(colorFields[a]).attr('id') == 'MainContent_drpLISTAndOr3') {

                    if ($("#MainContent_drpLISTAndOr3").val() != "0") {

                        if ($("#MainContent_drpLISTField4").val() == "0") {
                            $("#MainContent_drpLISTField4").addClass("brd-1px-redimp");
                            test = "1";
                        }
                        else {
                            $("#MainContent_drpLISTField4").removeClass("brd-1px-redimp");
                            test = "0";
                        }

                        if ($("#MainContent_drpLISTCondition4").val() == "-1") {
                            $("#MainContent_drpLISTCondition4").addClass("brd-1px-redimp");
                            test = "1";
                        }
                        else {
                            $("#MainContent_drpLISTCondition4").removeClass("brd-1px-redimp");
                            test = "0";
                        }
                    }
                    else {

                        $("#MainContent_drpLISTField4").removeClass("brd-1px-redimp");
                        $("#MainContent_drpLISTCondition4").removeClass("brd-1px-redimp");
                        test = "0";

                    }
                }
                else if ($(colorFields[a]).attr('id') == 'MainContent_drpLISTAndOr4') {

                    if ($("#MainContent_drpLISTAndOr4").val() != "0") {

                        if ($("#MainContent_drpLISTField5").val() == "0") {
                            $("#MainContent_drpLISTField5").addClass("brd-1px-redimp");
                            test = "1";
                        }
                        else {
                            $("#MainContent_drpLISTField5").removeClass("brd-1px-redimp");
                            test = "0";
                        }

                        if ($("#MainContent_drpLISTCondition5").val() == "-1") {
                            $("#MainContent_drpLISTCondition5").addClass("brd-1px-redimp");
                            test = "1";
                        }
                        else {
                            $("#MainContent_drpLISTCondition5").removeClass("brd-1px-redimp");
                            test = "0";
                        }
                    }
                    else {

                        $("#MainContent_drpLISTField5").removeClass("brd-1px-redimp");
                        $("#MainContent_drpLISTCondition5").removeClass("brd-1px-redimp");
                        test = "0";

                    }
                }
            }

            if (test == "1") {
                return false;
            }
            return true;
        });

    </script>
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": true, "ordering": true,
                "bDestroy": true, stateSave: true
            });

            $(".Datatable").parent().parent().addClass('fixTableHead');
        }
    </script>
    <style type="text/css">
        .btn-info {
            background-repeat: repeat-x;
            border-color: #28a4c9;
            /*background-image: linear-gradient(to bottom, #5bc0de 0%, #2aabd2 100%);*/
        }

        .prevent-refresh-button {
            display: inline-block;
            padding: 5px 5px;
            margin-bottom: 0;
            font-size: 12px;
            font-weight: normal;
            line-height: 1.428571429;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            cursor: pointer;
            border: 1px solid transparent;
            border-radius: 4px;
            width: 70pt;
            height: 19pt;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                <asp:Label runat="server" ID="lblHeader" Text="Search Docs"></asp:Label>
            </h3>
        </div>
        <div class="row">
            <div class="lblError">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="row">
                    <div class="col-md-3">
                        <asp:DropDownList ID="drpLISTField1" runat="server" CssClass="form-control required"
                            Width="100%" AutoPostBack="true" OnSelectedIndexChanged="drpLISTField1_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.Country" Text="Country"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.SiteID" Text="Site"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.Zone" Text="Zones"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.Section" Text="Sections"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.Artifact" Text="Artifacts"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.UploadFileName" Text="File Name"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.[Status]" Text="Status"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.UPLOADBYNAME" Text="Uploaded By"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.SysVERSION" Text="System Version"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.DocVERSION" Text="Document Version"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.DocDATE" Text="Document Date"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.NOTE" Text="Notes"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.RECEIPTDAT" Text="Received Date"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.Unblind" Text="Blinded/Unblinded"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.UPLOAD_DAYS" Text="No. of Days to Upload"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.QC_DAYS" Text="Document QC Compliance"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.QCBYNAME" Text="QCed By"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.REJECTBYNAME" Text="Rejected By"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.REJECT_DOC_REASONE" Text="Reason for Reject"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.DELETEBYNAME" Text="Deleted By"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.DELETE_REASON" Text="Reason for Delete"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.QCACTBYNAME" Text="QC Rectified By"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.QCACT_DETAIL" Text="QC Rectification Details"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="drpLISTCondition1" runat="server" CssClass="form-control required"
                            Width="100%">
                            <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="Is Blank" Value="IS NULL"></asp:ListItem>
                            <asp:ListItem Text="Is Not Blank" Value="IS NOT NULL"></asp:ListItem>
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
                        <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue1"
                            Width="100%"> </asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="drpLISTAndOr1" runat="server" CssClass="form-control ANDORCONDITION"
                            Width="100%">
                            <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                            <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                            <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <asp:DropDownList ID="drpLISTField2" runat="server" CssClass="form-control" Width="100%"
                            AutoPostBack="true" OnSelectedIndexChanged="drpLISTField2_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.Country" Text="Country"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.SiteID" Text="Site"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.Zone" Text="Zones"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.Section" Text="Sections"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.Artifact" Text="Artifacts"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.UploadFileName" Text="File Name"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.[Status]" Text="Status"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.UPLOADBYNAME" Text="Uploaded By"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.SysVERSION" Text="System Version"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.DocVERSION" Text="Document Version"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.DocDATE" Text="Document Date"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.NOTE" Text="Notes"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.RECEIPTDAT" Text="Received Date"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.Unblind" Text="Blinded/Unblinded"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.UPLOAD_DAYS" Text="No. of Days to Upload"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.QC_DAYS" Text="Document QC Compliance"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.QCBYNAME" Text="QCed By"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.REJECTBYNAME" Text="Rejected By"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.REJECT_DOC_REASONE" Text="Reason for Reject"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.DELETEBYNAME" Text="Deleted By"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.DELETE_REASON" Text="Reason for Delete"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.QCACTBYNAME" Text="QC Rectified By"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.QCACT_DETAIL" Text="QC Rectification Details"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="drpLISTCondition2" runat="server" CssClass="form-control" Width="100%">
                            <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="Is Blank" Value="IS NULL"></asp:ListItem>
                            <asp:ListItem Text="Is Not Blank" Value="IS NOT NULL"></asp:ListItem>
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
                        <asp:DropDownList ID="drpLISTAndOr2" runat="server" CssClass="form-control ANDORCONDITION"
                            Width="100%">
                            <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                            <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                            <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <asp:DropDownList ID="drpLISTField3" runat="server" CssClass="form-control" Width="100%"
                            AutoPostBack="true" OnSelectedIndexChanged="drpLISTField3_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.Country" Text="Country"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.SiteID" Text="Site"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.Zone" Text="Zones"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.Section" Text="Sections"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.Artifact" Text="Artifacts"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.UploadFileName" Text="File Name"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.[Status]" Text="Status"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.UPLOADBYNAME" Text="Uploaded By"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.SysVERSION" Text="System Version"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.DocVERSION" Text="Document Version"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.DocDATE" Text="Document Date"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.NOTE" Text="Notes"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.RECEIPTDAT" Text="Received Date"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.Unblind" Text="Blinded/Unblinded"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.UPLOAD_DAYS" Text="No. of Days to Upload"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.QC_DAYS" Text="Document QC Compliance"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.QCBYNAME" Text="QCed By"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.REJECTBYNAME" Text="Rejected By"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.REJECT_DOC_REASONE" Text="Reason for Reject"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.DELETEBYNAME" Text="Deleted By"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.DELETE_REASON" Text="Reason for Delete"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.QCACTBYNAME" Text="QC Rectified By"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.QCACT_DETAIL" Text="QC Rectification Details"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="drpLISTCondition3" runat="server" CssClass="form-control" Width="100%">
                            <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="Is Blank" Value="IS NULL"></asp:ListItem>
                            <asp:ListItem Text="Is Not Blank" Value="IS NOT NULL"></asp:ListItem>
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
                        <asp:DropDownList ID="drpLISTAndOr3" runat="server" CssClass="form-control ANDORCONDITION"
                            Width="100%">
                            <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                            <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                            <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <asp:DropDownList ID="drpLISTField4" runat="server" CssClass="form-control" Width="100%"
                            AutoPostBack="true" OnSelectedIndexChanged="drpLISTField4_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.Country" Text="Country"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.SiteID" Text="Site"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.Zone" Text="Zones"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.Section" Text="Sections"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.Artifact" Text="Artifacts"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.UploadFileName" Text="File Name"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.[Status]" Text="Status"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.UPLOADBYNAME" Text="Uploaded By"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.SysVERSION" Text="System Version"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.DocVERSION" Text="Document Version"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.DocDATE" Text="Document Date"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.NOTE" Text="Notes"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.RECEIPTDAT" Text="Received Date"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.Unblind" Text="Blinded/Unblinded"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.UPLOAD_DAYS" Text="No. of Days to Upload"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.QC_DAYS" Text="Document QC Compliance"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.QCBYNAME" Text="QCed By"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.REJECTBYNAME" Text="Rejected By"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.REJECT_DOC_REASONE" Text="Reason for Reject"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.DELETEBYNAME" Text="Deleted By"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.DELETE_REASON" Text="Reason for Delete"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.QCACTBYNAME" Text="QC Rectified By"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.QCACT_DETAIL" Text="QC Rectification Details"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="drpLISTCondition4" runat="server" CssClass="form-control" Width="100%">
                            <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="Is Blank" Value="IS NULL"></asp:ListItem>
                            <asp:ListItem Text="Is Not Blank" Value="IS NOT NULL"></asp:ListItem>
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
                        <asp:DropDownList ID="drpLISTAndOr4" runat="server" CssClass="form-control ANDORCONDITION"
                            Width="100%">
                            <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                            <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                            <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <asp:DropDownList ID="drpLISTField5" runat="server" CssClass="form-control" Width="100%"
                            AutoPostBack="true" OnSelectedIndexChanged="drpLISTField5_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.Country" Text="Country"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.SiteID" Text="Site"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.Zone" Text="Zones"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.Section" Text="Sections"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.Artifact" Text="Artifacts"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.UploadFileName" Text="File Name"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.[Status]" Text="Status"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.UPLOADBYNAME" Text="Uploaded By"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.SysVERSION" Text="System Version"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.DocVERSION" Text="Document Version"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.DocDATE" Text="Document Date"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.NOTE" Text="Notes"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.RECEIPTDAT" Text="Received Date"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.Unblind" Text="Blinded/Unblinded"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.UPLOAD_DAYS" Text="No. of Days to Upload"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.QC_DAYS" Text="Document QC Compliance"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.QCBYNAME" Text="QCed By"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.REJECTBYNAME" Text="Rejected By"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.REJECT_DOC_REASONE" Text="Reason for Reject"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.DELETEBYNAME" Text="Deleted By"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.DELETE_REASON" Text="Reason for Delete"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.QCACTBYNAME" Text="QC Rectified By"></asp:ListItem>
                            <asp:ListItem Value="V_eTMF_Documents_MASTERDATA.QCACT_DETAIL" Text="QC Rectification Details"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="drpLISTCondition5" runat="server" CssClass="form-control" Width="100%">
                            <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="Is Blank" Value="IS NULL"></asp:ListItem>
                            <asp:ListItem Text="Is Not Blank" Value="IS NOT NULL"></asp:ListItem>
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
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-3">
                            &nbsp;
                        </div>
                        <div class="col-md-7">
                            <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                OnClick="btnSearch_Click" />
                            <asp:Button ID="btnAllDocs" Text="Show All Docs" runat="server" CssClass="btn btn-DarkGreen btn-sm"
                                OnClick="btnAllDocs_Click" />&nbsp&nbsp&nbsp
                                 <a href="#" class="dropdown-toggle btn-info prevent-refresh-button" data-toggle="dropdown" style="color: #FFFFFF">Export&nbsp;&nbsp;<span class="glyphicon glyphicon-download"></span></a>
                            <ul class="dropdown-menu dropdown-menu-sm">
                                <li>
                                    <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" CommandName="Excel" ToolTip="Excel"
                                        Text="Excel" CssClass="dropdown-item" Style="color: #333333;">
                                    </asp:LinkButton></li>
                                <hr style="margin: 5px;" />
                                <li>
                                    <asp:LinkButton runat="server" ID="btnExportPDF" CssClass="dropdown-item" OnClick="btnExportPDF_Click"
                                        ToolTip="PDF" Text="PDF" Style="color: #333333;">
                                    </asp:LinkButton>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="box box-primary">
            <div class="box-body">
                <div class="rows">
                    <div style="width: 100%; overflow: auto;">
                        <div>
                            <asp:GridView ID="gvFiles" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                Width="100%" CssClass="table table-bordered table-striped layerFiles Datatable"
                                OnPreRender="grd_data_PreRender"
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
                                    <asp:TemplateField HeaderText="Location">
                                        <ItemTemplate>
                                            <asp:Label ID="LOCATION" ToolTip='<%# Bind("LOCATION") %>' runat="server" Text='<%# Bind("LOCATION") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Zone">
                                        <ItemTemplate>
                                            <asp:Label ID="Zone" ToolTip='<%# Bind("Zone") %>' runat="server" Text='<%# Bind("Zone") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Section">
                                        <ItemTemplate>
                                            <asp:Label ID="Section" ToolTip='<%# Bind("Section") %>' runat="server" Text='<%# Bind("Section") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Artifact">
                                        <ItemTemplate>
                                            <asp:Label ID="Artifact" ToolTip='<%# Bind("Artifact") %>' runat="server" Text='<%# Bind("Artifact") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sub-Artifact">
                                        <ItemTemplate>
                                            <asp:Label ID="SubArtifact" ToolTip='<%# Bind("SubArtifact") %>' runat="server" Text='<%# Bind("SubArtifact") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="Country" ToolTip='<%# Bind("Country") %>' runat="server" Text='<%# Bind("Country") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Site ID">
                                        <ItemTemplate>
                                            <asp:Label ID="SiteID" ToolTip='<%# Bind("SiteID") %>' runat="server" Text='<%# Bind("SiteID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Subject ID">
                                        <ItemTemplate>
                                            <asp:Label ID="SUBJID" ToolTip='<%# Bind("SUBJID") %>' runat="server" Text='<%# Bind("SUBJID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Spec.">
                                        <ItemTemplate>
                                            <asp:Label ID="Spec" Width="100%" ToolTip='<%# Bind("SPEC_CONCAT") %>' runat="server" Text='<%# Bind("SPEC_CONCAT") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="File Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_UploadFileName" Width="100%" ToolTip='<%# Bind("UploadFileName") %>'
                                                CssClass="label" runat="server" Text='<%# Bind("UploadFileName") %>'></asp:Label>
                                            <asp:LinkButton ID="lbtn_UploadFileName" Width="100%" ToolTip='<%# Bind("UploadFileName") %>'
                                                runat="server" CssClass="label" OnClientClick="return OpenDoc(this);" Text='<%# Bind("UploadFileName") %>'></asp:LinkButton>
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
                                            <asp:Label ID="SysVERSION" Width="100%" ToolTip='<%# Bind("SysVERSION") %>' runat="server" Text='<%# Bind("SysVERSION") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="STATUS" Width="100%" ToolTip='<%# Bind("STATUS") %>' runat="server" Text='<%# Bind("STATUS") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Document Version" ItemStyle-CssClass="txt_center">
                                        <ItemTemplate>
                                            <asp:Label ID="DocVERSION" ToolTip='<%# Bind("DocVERSION") %>' runat="server" Text='<%# Bind("DocVERSION") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Document Date">
                                        <ItemTemplate>
                                            <asp:Label ID="DocDATE" ToolTip='<%# Bind("DocDATE") %>' runat="server" Text='<%# Bind("DocDATE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Note">
                                        <ItemTemplate>
                                            <asp:Label ID="NOTE" ToolTip='<%# Bind("NOTE") %>' runat="server" Text='<%# Bind("NOTE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Received Date">
                                        <ItemTemplate>
                                            <asp:Label ID="ReceivedDate" Width="100%" ToolTip='<%# Bind("RECEIPTDAT") %>' CssClass="label"
                                                runat="server" Text='<%# Bind("RECEIPTDAT") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Expiry Date">
                                        <ItemTemplate>
                                            <asp:Label ID="ExpiryDate" Width="100%" ToolTip='<%# Bind("ExpiryDate") %>' CssClass="label"
                                                runat="server" Text='<%# Bind("ExpiryDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Blinded / Unblinded">
                                        <ItemTemplate>
                                            <asp:Label ID="Unblind" ToolTip='<%# Bind("Unblind") %>' runat="server" Text='<%# Bind("Unblind") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No. of Days to Upload">
                                        <ItemTemplate>
                                            <asp:Label ID="UPLOAD_DAYS" ToolTip='<%# Bind("UPLOAD_DAYS") %>' runat="server" Text='<%# Bind("UPLOAD_DAYS") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <label>Uploading Details</label><br />
                                            <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Uploaded By]</label><br />
                                            <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                            <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <div>
                                                    <asp:Label ID="UPLOADBYNAME" runat="server" Text='<%# Bind("UPLOADBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="UPLOAD_CAL_DAT" runat="server" Text='<%# Bind("UPLOAD_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="UPLOAD_CAL_TZDAT" runat="server" Text='<%# Eval("UPLOAD_CAL_TZDAT") +" "+ Eval("UPLOAD_TZVAL") %>' ForeColor="Red"></asp:Label>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reason for Delete">
                                        <ItemTemplate>
                                            <asp:Label ID="DELETE_REASON" ToolTip='<%# Bind("DELETE_REASON") %>' runat="server" Text='<%# Bind("DELETE_REASON") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <label>Deletion Details</label><br />
                                            <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Deleted By]</label><br />
                                            <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                            <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <div>
                                                    <asp:Label ID="DELETEBYNAME" runat="server" Text='<%# Bind("DELETEBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="DELETE_CAL_DAT" runat="server" Text='<%# Bind("DELETE_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="DELETE_CAL_TZDAT" runat="server" Text='<%# Eval("DELETE_CAL_TZDAT") +" "+ Eval("DELETE_TZVAL") %>' ForeColor="Red"></asp:Label>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="QC Status">
                                        <ItemTemplate>
                                            <asp:Label ID="QC_STATUS" ToolTip='<%# Bind("QC_STATUS") %>' runat="server" Text='<%# Bind("QC_STATUS") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Correct Nomenclature">
                                        <ItemTemplate>
                                            <asp:Label ID="QCDOC_CORRECT_NOM" ToolTip='<%# Bind("QCDOC_CORRECT_NOM") %>' runat="server" Text='<%# Bind("QCDOC_CORRECT_NOM") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Legibility of Document">
                                        <ItemTemplate>
                                            <asp:Label ID="QCDocLegible" ToolTip='<%# Bind("QCDocLegible") %>' runat="server" Text='<%# Bind("QCDocLegible") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Correct Orientation">
                                        <ItemTemplate>
                                            <asp:Label ID="QCDOC_CORRECT_ORIEN" ToolTip='<%# Bind("QCDOC_CORRECT_ORIEN") %>' runat="server" Text='<%# Bind("QCDOC_CORRECT_ORIEN") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Filed in Correct Location">
                                        <ItemTemplate>
                                            <asp:Label ID="QCDocLocat" ToolTip='<%# Bind("QCDocLocat") %>' runat="server" Text='<%# Bind("QCDocLocat") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Is the document attributable">
                                        <ItemTemplate>
                                            <asp:Label ID="QCDOC_DOC_ATTRI" ToolTip='<%# Bind("QCDOC_DOC_ATTRI") %>' runat="server" Text='<%# Bind("QCDOC_DOC_ATTRI") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Is the document complete">
                                        <ItemTemplate>
                                            <asp:Label ID="QCDOC_COMPLETE" ToolTip='<%# Bind("QCDOC_COMPLETE") %>' runat="server" Text='<%# Bind("QCDOC_COMPLETE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:Label ID="QC_ACTION" ToolTip='<%# Bind("QC_ACTION") %>' runat="server" Text='<%# Bind("QC_ACTION") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Comment">
                                        <ItemTemplate>
                                            <asp:Label ID="QCcomments" ToolTip='<%# Bind("QCcomments") %>' runat="server" Text='<%# Bind("QCcomments") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Document QC Compliance (No of days)">
                                        <ItemTemplate>
                                            <asp:Label ID="QC_DAYS" ToolTip='<%# Bind("QC_DAYS") %>' runat="server" Text='<%# Bind("QC_DAYS") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <label>QC Details</label><br />
                                            <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[QCed By]</label><br />
                                            <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                            <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <div>
                                                    <asp:Label ID="QCBYNAME" runat="server" Text='<%# Bind("QCBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="QC_CAL_DAT" runat="server" Text='<%# Bind("QC_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="QC_CAL_TZDAT" runat="server" Text='<%# Eval("QC_CAL_TZDAT") +" "+ Eval("QC_TZVAL") %>' ForeColor="Red"></asp:Label>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <label>Rejection Details</label><br />
                                            <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Rejected By]</label><br />
                                            <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                            <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <div>
                                                    <asp:Label ID="REJECTBYNAME" runat="server" Text='<%# Bind("REJECTBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="REJECT_CAL_DAT" runat="server" Text='<%# Bind("REJECT_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="REJECT_CAL_TZDAT" runat="server" Text='<%# Eval("REJECT_CAL_TZDAT")+" "+ Eval("REJECT_TZVAL") %>' ForeColor="Red"></asp:Label>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rectification Action">
                                        <ItemTemplate>
                                            <asp:Label ID="QCACT_DETAIL" ToolTip='<%# Bind("QCACT_DETAIL") %>' runat="server" Text='<%# Bind("QCACT_DETAIL") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <label>Rectification Details</label><br />
                                            <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Rectified By]</label><br />
                                            <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                            <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <div>
                                                    <asp:Label ID="QCACTBYNAME" runat="server" Text='<%# Bind("QCACTBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="QCACT_CAL_DAT" runat="server" Text='<%# Bind("QCACT_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="QCACT_CAL_TZDAT" runat="server" Text='<%# Bind("QCACT_CAL_TZDAT") +" "+ Eval("QCACT_TZVAL")%>' ForeColor="Red"></asp:Label>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="1%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnDocumentHistory" runat="server" ToolTip="Document History"
                                                OnClientClick="return DOCUMENT_HISTORY(this);" CommandArgument='<%# Eval("ID") %>'>
                                                <i id="iconDochistory" runat="server" class="fa fa-history" style="color: #333333;"
                                                    aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="1%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnversionHistory" runat="server" ToolTip="Version History"
                                                OnClientClick="return VERSION_HISTORY(this);" CommandArgument='<%# Eval("ID") %>'>
                                                <i id="iconhistory" runat="server" class="fa fa-files-o" style="color: #333333;"
                                                    aria-hidden="true"></i>
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
                                    <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="1%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQC" runat="server" ToolTip="QC Document" CommandArgument='<%# Eval("ID") %>'
                                                Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconQC" runat="server"
                                                    class="fa fa-check"></i></asp:Label>
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
</asp:Content>

