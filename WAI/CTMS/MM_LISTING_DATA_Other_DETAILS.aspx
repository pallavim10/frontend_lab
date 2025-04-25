<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MM_LISTING_DATA_Other_DETAILS.aspx.cs"
    Inherits="CTMS.MM_LISTING_DATA_Other_DETAILS" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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
    <script src="js/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>
    <script src="js/plugins/datatables/dataTables.bootstrap.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="Styles/datatables/dataTables.bootstrap.css" />
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": false,
                "bDestroy": false,
                stateSave: true
            });
        }

        function ViewOtherDetails_Other(element, LISTING_ID, PREV_LISTID) {
            var VALUE = $('#hdnValue').val();
            var SUBJID = $('#hdnSUBJID').val();
            //            var PREV_LISTID = $('#hdnPREV_LISTID').val();
            var FIELDID = $('#hdnFIELDID').val();
            var TYPE = $('#hdnTYPE').val();

            var test = "MM_LISTING_DATA_DETAILS.aspx?VALUE=" + VALUE + "&LISTING_ID=" + LISTING_ID + "&SUBJID=" + SUBJID + "&PREV_LISTID=" + PREV_LISTID + "&FIELDID=" + FIELDID + "&TYPE=" + TYPE;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=445,width=1300";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function ViewEventDetails_Other(element, LISTING_ID, PREV_LISTID) {
            var VALUE = $('#hdnValue').val();
            var SUBJID = $('#hdnSUBJID').val();
            //            var PREV_LISTID = $('#hdnPREV_LISTID').val();
            var FIELDID = $('#hdnFIELDID').val();
            var TYPE = $('#hdnTYPE').val();

            var test = "MM_LISTING_DATA_DETAILS.aspx?VALUE=" + VALUE + "&LISTING_ID=" + LISTING_ID + "&SUBJID=" + SUBJID + "&PREV_LISTID=" + PREV_LISTID + "&FIELDID=" + FIELDID + "&TYPE=" + TYPE;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=445,width=1300";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function ViewLabDetails_Other(element, LISTING_ID, PREV_LISTID) {
            var VALUE = $('#hdnValue').val();
            var SUBJID = $('#hdnSUBJID').val();
            var FIELDID = $('#hdnFIELDID').val();
            var TYPE = $('#hdnTYPE').val();

            var test = "MM_LISTING_DATA_DETAILS.aspx?VALUE=" + VALUE + "&LISTING_ID=" + LISTING_ID + "&SUBJID=" + SUBJID + "&PREV_LISTID=" + PREV_LISTID + "&FIELDID=" + FIELDID + "&TYPE=" + TYPE;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=445,width=1300";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function ViewSubjectDetails_Other(element, LISTING_ID, PREV_LISTID) {
            var VALUE = $('#hdnValue').val();
            var SUBJID = $('#hdnSUBJID').val();

            var test = "MM_LISTING_DATA_SUBJECT.aspx?VALUE=" + VALUE + "&LISTING_ID=" + LISTING_ID + "&PREV_LISTID=" + PREV_LISTID + "&SUBJID=" + SUBJID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=445,width=1300";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function ViewEventDetails(element, LISTING_ID, FIELDID) {
            var VALUE = $(element).text();
            var SUBJID = $(element).closest('tr').find('td:eq(3)').text();
            var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
            var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();
            var PREV_LISTID = $('#hdnlistid').val();
            var TYPE = $('#hdnTYPE').val();

            if ($(element).text().trim() != '') {
                var test = "MM_LISTING_DATA_DETAILS.aspx?VALUE=" + VALUE + "&LISTING_ID=" + LISTING_ID + "&SUBJID=" + SUBJID + "&PREV_LISTID=" + PREV_LISTID + "&FIELDID=" + FIELDID + "&PVID=" + PVID + "&RECID=" + RECID + "&TYPE=" + TYPE;
                var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=620,width=1300";
                window.open(test, '_blank', strWinProperty);
                return false;
            }
        }

        function ViewLabDetails(element, LISTING_ID, FIELDID) {
            var VALUE = $(element).text();
            var TEST = $(element).closest('tr').find('td:eq(6)').text().trim();
            var SUBJID = $(element).closest('tr').find('td:eq(3)').text();
            var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
            var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();
            var PREV_LISTID = $('#hdnlistid').val();

            if ($(element).text().trim() != '') {
                var test = "MM_LISTING_LAB_DETAILS.aspx?VALUE=" + VALUE + "&LISTING_ID=" + LISTING_ID + "&SUBJID=" + SUBJID + "&PREV_LISTID=" + PREV_LISTID + "&FIELDID=" + FIELDID + "&PVID=" + PVID + "&RECID=" + RECID + "&TEST=" + TEST;
                var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=620,width=1300";
                window.open(test, '_blank', strWinProperty);
                return false;
            }
        }

        function ViewSubjectDetails(element, LISTING_ID) {
            var VALUE = $(element).text();
            var SUBJID = $(element).closest('tr').find('td:eq(3)').text();
            var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
            var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();
            var PREV_LISTID = $('#hdnlistid').val();

            var test = "MM_LISTING_DATA_SUBJECT.aspx?VALUE=" + VALUE + "&LISTING_ID=" + LISTING_ID + "&PREV_LISTID=" + PREV_LISTID + "&SUBJID=" + SUBJID + "&PVID=" + PVID + "&RECID=" + RECID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=620,width=1300";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function ViewVisitDetails(element, LISTING_ID) {
            var VALUE = $(element).text();
            var SUBJID = $(element).closest('tr').find('td:eq(3)').text();
            var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
            var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();
            var PREV_LISTID = $('#hdnlistid').val();

            var test = "MM_LISTING_DATA_VISIT.aspx?VALUE=" + VALUE + "&LISTING_ID=" + LISTING_ID + "&PREV_LISTID=" + PREV_LISTID + "&SUBJID=" + SUBJID + "&PVID=" + PVID + "&RECID=" + RECID;;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=620,width=1300";
            window.open(test, '_blank', strWinProperty);
            return false;
        }



        function ShowComments_btn(element) {

            var Subject = $('#hdnSUBJID').val().trim();
            var Source = $('#hdnPrimMODULENAME').val().trim();
            var PVID = $('#hdnPVID').val().trim();
            var RECID = $('#hdnRECID').val().trim();

            var LISTING_ID = $('#hdnPREV_LISTID').val();

            var test = "MM_LISTING_COMMENTS.aspx?SUBJID=" + Subject + "&PVID=" + PVID + "&RECID=" + RECID + "&LISTING_ID=" + LISTING_ID;

            //var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=370,width=550,resizable=no,left=850";
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=600,width=450,resizable=no";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function ShowComments(element) {

            var Subject = $(element).closest('tr').find('td:eq(3)').text().trim();
            var Source = $('#hdnPrimMODULENAME').val().trim();
            var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
            var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();

            var LISTING_ID = $('#hdnlistid').val();

            var test = "MM_LISTING_COMMENTS.aspx?SUBJID=" + Subject + "&PVID=" + PVID + "&RECID=" + RECID + "&LISTING_ID=" + LISTING_ID;

            //var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=370,width=550,resizable=no,left=850";
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=600,width=450,resizable=no";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function ShowHistory(element) {

            var Subject = $(element).closest('tr').find('td:eq(3)').text().trim();
            var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
            var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();

            var test = "MM_LISTING_HISTORY.aspx?SUBJID=" + Subject + "&PVID=" + PVID + "&RECID=" + RECID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=600,width=1000,resizable=no";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function ShowComments_PEER_btn(element) {

            var Subject = $('#hdnSUBJID').val();
            var PVID = $('#hdnPVID').val();
            var RECID = $('#hdnRECID').val();

            var LISTING_ID = $('#hdnPREV_LISTID').val();

            var test = "MM_LISTING_COMMENTS.aspx?SUBJID=" + Subject + "&PVID=" + PVID + "&RECID=" + RECID + "&LISTING_ID=" + LISTING_ID + "&TYPE=PEER";

            //var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=370,width=550,resizable=no,left=850";
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=600,width=450,resizable=no";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function ShowComments_PEER(element) {

            var Subject = $(element).closest('tr').find('td:eq(3)').text().trim();
            var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
            var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();

            var LISTING_ID = $('#hdnlistid').val();

            var test = "MM_LISTING_COMMENTS.aspx?SUBJID=" + Subject + "&PVID=" + PVID + "&RECID=" + RECID + "&LISTING_ID=" + LISTING_ID + "&TYPE=PEER";

            //var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=370,width=550,resizable=no,left=850";
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=600,width=450,resizable=no";
            window.open(test, '_blank', strWinProperty);
            return false;
        }


        function RaiseQuery(element) {

            var Subject = $(element).closest('tr').find('td:eq(3)').text().trim();
            var EventCode = $(element).closest('tr').find('td:eq(5)').text().trim();

            var Department = "Medical"
            var Rule = $('#lblHeader').text().trim();
            var Source = $('#hdnPrimMODULENAME').val().trim();

            var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
            var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();

            if (Source == '' || Source == undefined) {
                Source = Rule;
            }

            var LISTING_ID = $('#hdnlistid').val();

            var test = "NewQueryPopup.aspx?Subject=" + Subject + "&Department=" + Department + "&Source=" + Source + "&EventCode=" + EventCode + "&Rule=" + Rule + "&PVID=" + PVID + "&RECID=" + RECID + "&LISTING_ID=" + LISTING_ID + "&Refrence=" + EventCode;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=250,width=500,resizable=no";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function GetFilterPieChart() {
            var INVID = "";
            var SUBJID = "";
            var RULE = "Ongoing Events"
            var RULE1 = "Rule5"

            var test = "MM_FilterPieChart.aspx?SUBJID=" + SUBJID + "&INVID=" + INVID + "&RULE=" + RULE + "&RULE1=" + RULE1;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=250,width=700,resizable=no";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function ShowQueryList(element) {

            var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
            var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();

            var test = "MM_PopUpQueryList.aspx?PVID=" + PVID + "&RECID=" + RECID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=445,width=1200";
            window.open(test, '_blank', strWinProperty);

            return false;
        }

        function GenerateAutoQuery(element) {

            var SUBJID = $(element).closest('tr').find('td:eq(3)').text().trim();
            var SOURCE = $('#hdnPrimMODULENAME').val().trim();
            var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
            var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();
            var LISTING_ID = $('#hdnlistid').val();

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/MM_AutoQuery",
                data: '{SOURCE:"' + SOURCE + '",  PVID: "' + PVID + '", RECID: "' + RECID + '" ,LISTING_ID: "' + LISTING_ID + '",SUBJID: "' + SUBJID + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                failure: function (response) {
                    if (response.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        alert("Contact administrator not suceesfully updated")
                    }
                }
            });
        }

        function MarkAsReviewed(element) {

            var SUBJID = $(element).closest('tr').find('td:eq(3)').text().trim();
            var SOURCE = $('#lblHeader').text().trim()
            var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
            var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();
            var LISTING_ID = $('#hdnlistid').val();

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/MM_Reviewed",
                data: '{SOURCE:"' + SOURCE + '",  PVID: "' + PVID + '", RECID: "' + RECID + '" ,LISTING_ID: "' + LISTING_ID + '",SUBJID: "' + SUBJID + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {

                    var Listingstatus = res.d;

                    $('#' + $(element).closest('tr').find('td:eq(0)').find("a[id*='lbtnReview']").attr('id')).addClass("disp-none");
                    $('#' + $(element).closest('tr').find('td:eq(0)').find("a[id*='lbtnAnotherReviewed']").attr('id')).addClass("disp-none");
                    $('#' + $(element).closest('tr').find('td:eq(0)').find("span[id*='lbtnReviewDone']").attr('id')).addClass("disp-none");
                    $('#' + $(element).closest('tr').find('td:eq(0)').find("span[id*='lbtnPeerReview']").attr('id')).addClass("disp-none");
                    $('#' + $(element).closest('tr').find('td:eq(0)').find("span[id*='lbtnReviewQuery']").attr('id')).addClass("disp-none");
                    $('#' + $(element).closest('tr').find('td:eq(0)').find("a[id*='lbtnReviewPatientRev']").attr('id')).addClass("disp-none");

                    if (Listingstatus != 'Not Reviewed') {

                        if (Listingstatus == 'Reviewed') {

                            $('#' + $(element).closest('tr').find('td:eq(0)').find("span[id*='lbtnReviewDone']").attr('id')).removeClass("disp-none");

                        }
                        else if (Listingstatus == 'Reviewed with Peer View') {

                            $('#' + $(element).closest('tr').find('td:eq(0)').find("span[id*='lbtnPeerReview']").attr('id')).removeClass("disp-none");

                        }
                        else if (Listingstatus == 'Query and Reviewed') {

                            $('#' + $(element).closest('tr').find('td:eq(0)').find("span[id*='lbtnReviewQuery']").attr('id')).removeClass("disp-none");

                        }

                    }
                    else {

                        alert("This data can not be reviewed.");

                    }

                },
                failure: function (response) {
                    if (response.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        alert("Contact administrator not suceesfully updated")
                    }
                }
            });

            return false;
        }
    </script>
    <style type="text/css">
        .fontBlue {
            color: Blue;
            cursor: pointer;
        }

        .circleQueryCountRed {
            width: 15px;
            height: 15px;
            border-radius: 50%;
            font-size: 10px;
            color: Yellow;
            text-align: center;
            background: Red;
        }

        .circleQueryCountGreen {
            width: 15px;
            height: 15px;
            border-radius: 50%;
            font-size: 10px;
            color: Yellow;
            text-align: center;
            background: Green;
        }

        .YellowIcon {
            color: Yellow;
        }

        .GreenIcon {
            color: Green;
        }

        label {
            margin-bottom: 10px;
        }
    </style>
</head>
<body>
    <form id="Form1" runat="server">
        <div class="page">
            <asp:ScriptManager ID="script1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hdnValue" runat="server" />
                    <asp:HiddenField ID="hdnlistid" runat="server" />
                    <asp:HiddenField ID="hdnSUBJID" runat="server" />
                    <asp:HiddenField ID="hdnPREV_LISTID" runat="server" />
                    <asp:HiddenField ID="hdnFIELDID" runat="server" />
                    <asp:HiddenField ID="hdnPVID" runat="server" />
                    <asp:HiddenField ID="hdnRECID" runat="server" />
                    <asp:HiddenField ID="hdntranspose" runat="server" />
                    <asp:HiddenField ID="hdnPrimMODULENAME" runat="server" />
                    <asp:HiddenField ID="hdnUNEXP" runat="server" />
                    <asp:HiddenField ID="hdnPrimMODULEID" runat="server" />
                    <asp:HiddenField ID="hdnTYPE" runat="server" />
                    <asp:HiddenField ID="hdnAutoQueryText" runat="server" />
                    <div class="box box-warning">
                        <div class="box-header">
                            <h3 class="box-title">Additional Listings
                            </h3>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <div class="rows">
                                    <asp:Repeater runat="server" ID="repeatOtherListings">
                                        <ItemTemplate>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton runat="server" ID="lbtnOtherListings" Text='<%# Bind("ListingName") %>'
                                            OnClientClick='<%# Bind("OnClickEvent") %>' CssClass="btn btn-primary btn-sm cls-btnSave"
                                            Style="margin-bottom: 1%;" ForeColor="White"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div runat="server" id="divDetails" visible="false" class="box box-warning">
                        <div class="box-header">
                            <h3 class="box-title">Details
                            </h3>
                            <div class="pull-right" style="margin-bottom: 10px; margin-right: 10px;">
                                &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton runat="server" ID="lbtnComments" Text="Comments" OnClientClick="return ShowComments_btn();"
                                CssClass="btn btn-primary btn-sm " ForeColor="White"></asp:LinkButton>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton runat="server" ID="lbtnPeer_Review" Text="Peer-Review" OnClientClick="return ShowComments_PEER_btn();"
                                CssClass="btn btn-primary btn-sm " ForeColor="White"></asp:LinkButton>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <div align="left" style="margin-left: 0px">
                                    <div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Repeater runat="server" ID="repeatData" OnItemDataBound="repeatData_ItemDataBound">
                                                    <ItemTemplate>
                                                        <div class="col-md-6">
                                                            <div class="col-md-6" style="padding-right: 0px;">
                                                                <label>
                                                                    <%# Eval("FIELDNAME")%>
                                                                :</label>
                                                            </div>
                                                            <div class="col-md-6" style="padding-left: 0px;">
                                                                <asp:Label runat="server" ID="lblData" Style="width: auto;" CssClass="form-control">
                                                                <%# Eval("DATA")%>
                                                                </asp:Label>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">
                        <asp:Label ID="lblHeader" runat="server"></asp:Label>
                    </h3>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                <div id="Div1" class="dropdown" runat="server" style="display: inline-flex">
                    <a href="#" class="dropdown-toggle glyphicon glyphicon-download-alt" data-toggle="dropdown"
                        style="color: #333333" title="Export"></a>
                    <ul class="dropdown-menu dropdown-menu-sm">
                        <li>
                            <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" ToolTip="Export to Excel"
                                Text="Export to Excel" CssClass="dropdown-item" Style="color: #333333;">
                            </asp:LinkButton></li>
                        <hr style="margin: 5px;" />
                        <li>
                            <asp:LinkButton runat="server" CssClass="dropdown-item" ID="btnPDF" OnClick="btnPDF_Click"
                                ToolTip="Export to PDF" Text="Export to PDF" Style="color: #333333;">
                            </asp:LinkButton></li>
                        <hr style="margin: 5px;" />
                        <li>
                            <asp:LinkButton runat="server" CssClass="dropdown-item" ID="btnRTF" OnClick="btnRTF_Click"
                                ToolTip="Export to RTF" Text="Export to RTF" Style="color: #333333;">
                            </asp:LinkButton></li>
                    </ul>
                </div>
                </div>
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
                </div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="box-body">
                            <div class="form-group">
                                <div class="box-body">
                                    <div class="rows">
                                        <div style="width: 100%; overflow: auto;">
                                            <div>
                                                <asp:GridView ID="gridData" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true"
                                                    OnPreRender="grd_data_PreRender" CssClass="table table-bordered Datatable table-striped"
                                                    OnRowDataBound="gridData_RowDataBound" OnRowCommand="gridData_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Options" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                                            <ItemTemplate>
                                                                <div class="txt_center" style="display: inline-flex;">
                                                                    <asp:LinkButton ID="lbtnHistory" OnClientClick="return ShowHistory(this);" runat="server">
                                                            <i title="History" class="fa fa-clock-o" style="color:#333333;font-size:14px" aria-hidden="true"></i>
                                                                    </asp:LinkButton>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:LinkButton ID="lbtnComments" OnClientClick="return ShowComments(this);" runat="server">
                                                                    <i title="Comments" id="iconComments" runat="server" class="fa fa-comment" style="color: #333333;"
                                                                        aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:LinkButton ID="lbtnForPeerReview" Visible="false" OnClientClick="return ShowComments_PEER(this);"
                                                                    runat="server">
                                                            <i title="For Peer-Review" class="fa fa-comments-o" style="color:#333333;" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                                    <asp:LinkButton ID="lbtnReview" CommandName="Review" CommandArgument='<%# Container.DataItemIndex %>'
                                                                        runat="server">
                                                        <i title="Review" class="fa fa-thumbs-o-up" style="color: #333333;" aria-hidden="true"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton ID="lbtnReviewPatientRev" OnClientClick="return MarkAsReviewed(this);"
                                                                        runat="server">
                                                        <i title="Reviewed from Patient Review" style="color: #37e537;" class="fa fa-thumbs-up"   aria-hidden="true"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton ID="lbtnAnotherReviewed" CommandName="Review" CommandArgument='<%# Container.DataItemIndex %>'
                                                                        runat="server">
                                                        <i title="Reviewed from Another Listings" style="color: #90ee90;" class="fa fa-thumbs-up"   aria-hidden="true"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:Label ID="lbtnReviewDone" runat="server">
                                                        <i title="Reviewed" class="fa fa-thumbs-up"  style="color: #008000;" aria-hidden="true"></i>
                                                                    </asp:Label>
                                                                    <asp:Label ID="lbtnPeerReview" CssClass="disp-none" runat="server">
                                                        <i title="Reviewed with Peer-Review" class="fa fa-thumbs-up"  style="color: #20b2aa;" aria-hidden="true"></i>
                                                                    </asp:Label>
                                                                    <asp:Label ID="lbtnReviewQuery" runat="server">
                                                        <i title="Reviewed with Query" class="fa fa-thumbs-up"  style="color: #6495ed;" aria-hidden="true"></i>
                                                                    </asp:Label>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:LinkButton ID="lbtnAutoQuery" CommandName="AutoQuery" CommandArgument='<%# Container.DataItemIndex %>'
                                                                    runat="server">
                                                        <i title="Raise Auto Query" class="fa fa-question-circle" style="color: #333333;" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:LinkButton ID="lbtnManualQuery" OnClientClick="return RaiseQuery(this);" runat="server">
                                                        <i title="Raise Manual Query" class="fa fa-question" style="color: #333333;" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <div runat="server" id="divQueryCount" visible="false" class="circleQueryCountGreen">
                                                                    <asp:LinkButton ID="lbtnQueryCount" OnClientClick="return ShowQueryList(this);" runat="server"
                                                                        ForeColor="Yellow" Font-Bold="true">
                                                                    </asp:LinkButton>
                                                                </div>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPVID" Font-Size="Small" Text='<%# Bind("PVID") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRECID" Font-Size="Small" Text='<%# Bind("RECID") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:GridView ID="gridData_Tran" Visible="false" HeaderStyle-CssClass="txt_center"
                                                    runat="server" AutoGenerateColumns="true" OnPreRender="grd_data_PreRender" CssClass="table table-bordered Datatable table-striped">
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnExport" />
                        <asp:PostBackTrigger ControlID="btnPDF" />
                        <asp:PostBackTrigger ControlID="btnRTF" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
</body>
</html>
