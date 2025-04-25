<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Sponsor_PatientReview.aspx.cs" Inherits="CTMS.Sponsor_PatientReview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .fontred
        {
            color: Red;
        }
        .fontBlue
        {
            color: Blue;
            cursor: pointer;
        }
        .circleQueryCountRed
        {
            width: 15px;
            height: 15px;
            border-radius: 50%;
            font-size: 10px;
            color: Yellow;
            text-align: center;
            background: Red;
        }
        .circleQueryCountGreen
        {
            width: 15px;
            height: 15px;
            border-radius: 50%;
            font-size: 10px;
            color: Yellow;
            text-align: center;
            background: Green;
        }
        .YellowIcon
        {
            color: Yellow;
        }
        .GreenIcon
        {
            color: Green;
        }
        .Datatable
        {
            width: 100%;
        }
    </style>
    <script type="text/javascript">

        $(document).on("click", ".cls-btnSave", function () {
            var test = "0";

            $('.required').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "0" || value == null || value == "Select") {
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

        function pageLoad() {

            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                stateSave: false,
                aaSorting: [[1, 'asc']],
                width: '100%'
            });

        }

        function ViewEventDetails(element, LISTING_ID, FIELDID) {
            var VALUE = $(element).text();
            var SUBJID = $('#MainContent_drpSubID').val();
            var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
            var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();
            var PREV_LISTID = $(element).closest('table').parents().eq(2).find('input').val();

            if ($(element).text().trim() != '') {
                var test = "SPONSAR_LISTING_DATA_DETAILS.aspx?VALUE=" + VALUE + "&LISTING_ID=" + LISTING_ID + "&SUBJID=" + SUBJID + "&PREV_LISTID=" + PREV_LISTID + "&FIELDID=" + FIELDID + "&PVID=" + PVID + "&RECID=" + RECID;
                var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=620,width=1300";
                window.open(test, '_blank', strWinProperty);
                return false;
            }
        }

        function ViewLabDetails(element, LISTING_ID, FIELDID) {
            var VALUE = $(element).text();
            var TEST = $(element).closest('tr').find('td:eq(6)').text().trim();
            var SUBJID = $('#MainContent_drpSubID').val();
            var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
            var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();
            var PREV_LISTID = $(element).closest('table').parents().eq(2).find('input').val();

            if ($(element).text().trim() != '') {
                var test = "MM_LISTING_LAB_DETAILS.aspx?VALUE=" + VALUE + "&LISTING_ID=" + LISTING_ID + "&SUBJID=" + SUBJID + "&PREV_LISTID=" + PREV_LISTID + "&FIELDID=" + FIELDID + "&PVID=" + PVID + "&RECID=" + RECID + "&TEST=" + TEST;
                var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=620,width=1300";
                window.open(test, '_blank', strWinProperty);
                return false;
            }
        }

        function ViewSubjectDetails(element, LISTING_ID) {
            var VALUE = $(element).text();
            var SUBJID = $('#MainContent_drpSubID').val();
            var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
            var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();
            var PREV_LISTID = $(element).closest('table').parents().eq(2).find('input').val();

            var test = "SPONSOR_LISTING_DATA_SUBJECT.aspx?VALUE=" + VALUE + "&LISTING_ID=" + LISTING_ID + "&PREV_LISTID=" + PREV_LISTID + "&SUBJID=" + SUBJID + "&PVID=" + PVID + "&RECID=" + RECID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=620,width=1300";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function ViewVisitDetails(element, LISTING_ID) {
            var VALUE = $(element).text();
            var SUBJID = $('#MainContent_drpSubID').val();
            var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
            var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();
            var PREV_LISTID = $(element).closest('table').parents().eq(2).find('input').val();

            //            var test = "MM_LISTING_DATA_VISIT.aspx?VALUE=" + VALUE + "&LISTING_ID=" + LISTING_ID + "&PREV_LISTID=" + PREV_LISTID + "&SUBJID=" + SUBJID + "&PVID=" + PVID + "&RECID=" + RECID; ;
            //            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=620,width=1300";
            //            window.open(test, '_blank', strWinProperty);
            return false;
        }

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


        function RaiseIssue(element) {

            var Subject = $(element).closest('tr').find('td:eq(3)').text().trim();
            var EventCode = $(element).closest('tr').find('td:eq(5)').text().trim();

            var Department = "Medical"
            var Rule = $(element).closest('table').parents().parents().eq(6).find('h3').text().trim();
            var Source = $(element).closest('table').parents().parents().eq(6).find('h3').text().trim();

            var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
            var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();

            if (Source == '' || Source == undefined) {
                Source = Rule;
            }

            var LISTING_ID = $(element).closest('table').parents().eq(2).find('input').val();

            var test = "NewIssuePopup.aspx?Subject=" + Subject + "&Department=" + Department + "&Source=" + Source + "&EventCode=" + EventCode + "&Rule=" + Rule + "&PVID=" + PVID + "&RECID=" + RECID + "&LISTING_ID=" + LISTING_ID;

            //var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=370,width=550,resizable=no,left=850";
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=370,width=550,resizable=no";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function ShowComments(element) {

            var Subject = $(element).closest('tr').find('td:eq(3)').text().trim();
            var Source = $(element).closest('table').parents().parents().eq(6).find('h3').text().trim();
            var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
            var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();

            var LISTING_ID = $(element).closest('table').parents().eq(2).find('input').val();

            var test = "MM_LISTING_COMMENTS_SPONSOR.aspx?SUBJID=" + Subject + "&PVID=" + PVID + "&RECID=" + RECID + "&LISTING_ID=" + LISTING_ID;

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

        function ShowComments_PEER(element) {

            var Subject = $(element).closest('tr').find('td:eq(3)').text().trim();
            var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
            var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();

            var LISTING_ID = $(element).closest('table').parents().eq(2).find('input').val();

            var test = "MM_LISTING_COMMENTS_SPONSOR.aspx?SUBJID=" + Subject + "&PVID=" + PVID + "&RECID=" + RECID + "&LISTING_ID=" + LISTING_ID + "&TYPE=PEER";

            //var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=370,width=550,resizable=no,left=850";
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=600,width=450,resizable=no";
            window.open(test, '_blank', strWinProperty);
            return false;
        }


        function RaiseQuery(element) {

            var Subject = $(element).closest('tr').find('td:eq(3)').text().trim();
            var EventCode = $(element).closest('tr').find('td:eq(5)').text().trim();

            var Department = "Medical"
            var Rule = $(element).closest('table').parents().parents().eq(6).find('h3').text().trim();
            var Source = $(element).closest('table').parents().parents().eq(6).find('h3').text().trim();

            var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
            var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();

            if (Source == '' || Source == undefined) {
                Source = Rule;
            }

            var LISTING_ID = $(element).closest('table').parents().eq(2).find('input').val();

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

            var test = "MM_PopUpQueryList.aspx?PVID=" + PVID + "&RECID=" + RECID + "&SOURCE=SPONSOR";
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=445,width=1200";
            window.open(test, '_blank', strWinProperty);

            return false;
        }

        function GenerateAutoQuery(element) {

            var SUBJID = $(element).closest('tr').find('td:eq(3)').text().trim();
            var SOURCE = $(element).closest('table').parents().parents().eq(6).find('h3').text().trim();
            var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
            var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();
            var LISTING_ID = $(element).closest('table').parents().eq(2).find('input').val();

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
            var SOURCE = $(element).closest('table').parents().parents().eq(6).find('h3').text().trim()
            var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
            var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();
            var LISTING_ID = $(element).closest('table').parents().eq(2).find('input').val();

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/MM_Reviewed_PatientRev",
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

                    if (Listingstatus != 'Not Reviewed') {

                        if (Listingstatus == 'Reviewed' || Listingstatus == 'Reviewed from Patient Review') {

                            $('#' + $(element).closest('tr').find('td:eq(0)').find("span[id*='lbtnReviewDone']").attr('id')).removeClass("disp-none");

                        }
                        else if (Listingstatus == 'Reviewed with Peer View') {

                            $('#' + $(element).closest('tr').find('td:eq(0)').find("span[id*='lbtnPeerReview']").attr('id')).removeClass("disp-none");

                        }
                        else if (Listingstatus == 'Query and Reviewed' || Listingstatus == 'Query and Reviewed from Patient Review') {

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">
                        Patient Review</h3>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        </div>
                        <div runat="server" id="DivINV" class="form-group" style="display: inline-flex">
                            <div class="form-group" style="display: inline-flex">
                                <label class="label">
                                    Site ID:
                                </label>
                                <div class="Control">
                                    <asp:DropDownList ID="drpInvID" runat="server" OnSelectedIndexChanged="drpInvID_SelectedIndexChanged"
                                        AutoPostBack="True" CssClass="form-control required">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                Indication:
                            </label>
                            <div class="Control">
                                <asp:DropDownList runat="server" ID="drpIndication" CssClass="form-control required"
                                    AutoPostBack="True" OnSelectedIndexChanged="drpIndication_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                Subject ID:
                            </label>
                            <div class="Control">
                                <asp:DropDownList ID="drpSubID" runat="server" CssClass="form-control required">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <asp:Button ID="btngetdata" runat="server" Text="Get Data" CssClass="btn btn-primary btn-sm cls-btnSave"
                            OnClick="btngetdata_Click" />&nbsp&nbsp&nbsp <a href="JavaScript:ManipulateAll('_Pat_');"
                                id="_Folder" style="color: #333333; font-size: x-large; margin-top: 5px;"><i id="img_Pat_"
                                    class="icon-plus-sign-alt"></i></a>
                    </div>
                </div>
            </div>
            <asp:Repeater runat="server" OnItemDataBound="repeatData_ItemDataBound" ID="repeatData"
                OnItemCommand="repeatData_ItemCommand">
                <ItemTemplate>
                    <div class="box box-primary">
                        <div class="box-header">
                            <div runat="server" style="display: inline-flex; padding-right: 20px; margin-top: 4px;
                                width: 100%; margin-left: 10px;" id="anchor">
                                <a href="JavaScript:divexpandcollapse('_Pat_<%# Eval("ID") %>');" style="color: #333333">
                                    <i id="img_Pat_<%# Eval("ID") %>" class="icon-plus-sign-alt"></i></a>
                                <h3 class="box-title" style="padding-top: 0px; width: 100%;">
                                    <asp:Label ID="lblHeader" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
                                    <asp:HiddenField ID="hdnListID" runat="server" Value='<%# Bind("ID") %>' />
                                    <div class="pull-right">
                                        <asp:LinkButton ID="lbtnITTChecked" runat="server" ToolTip="Intend To Treat" Text="ITT"
                                            Visible="false" CommandName="AnalytITT" CommandArgument='<%# Bind("PrimaryMODULE") %>'><i class="fa fa-check-square"></i></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnITTUncheck" runat="server" ToolTip="Intend To Treat" Text="ITT"
                                            Visible="false" CommandName="AnalytITT" CommandArgument='<%# Bind("PrimaryMODULE") %>'><i class="fa fa-square-o"></i></asp:LinkButton>
                                        <asp:Label ID="lblITT" runat="server" Text="ITT" ToolTip="Intend To Treat" Visible="false"></asp:Label>
                                        &nbsp&nbsp&nbsp&nbsp
                                        <asp:LinkButton ID="lbtnPPPChecked" runat="server" ToolTip="Per Protocol Population"
                                            Text="PP" Visible="false" CommandName="AnalytPPP" CommandArgument='<%# Bind("PrimaryMODULE") %>'><i class="fa fa-check-square"></i></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnPPPUncheck" runat="server" ToolTip="Per Protocol Population"
                                            Text="PP" Visible="false" CommandName="AnalytPPP" CommandArgument='<%# Bind("PrimaryMODULE") %>'><i class="fa fa-square-o"></i></asp:LinkButton>
                                        <asp:Label ID="lblPPP" runat="server" Text="PP" ToolTip="Per Protocol Population"
                                            Visible="false"></asp:Label>
                                    </div>
                                </h3>
                            </div>
                        </div>
                        <div id="_Pat_<%# Eval("ID") %>" style="display: none; position: relative; overflow: auto;">
                            <div class="box-body">
                                <div class="rows">
                                    <div style="width: 100%; overflow: auto;">
                                        <div>
                                            <asp:HiddenField ID="hfLISTID" runat="server" Value='<%# Bind("ID") %>'></asp:HiddenField>
                                            <asp:GridView ID="gridData" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true"
                                                OnPreRender="grd_data_PreRender" Width="100%" CssClass="table table-bordered Datatable table-striped notranslate"
                                                OnRowDataBound="gridData_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Options" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                                        <ItemTemplate>
                                                            <div class="txt_center" style="display: inline-flex;">
                                                                <asp:LinkButton ID="lbtnComments" OnClientClick="return ShowComments(this);" runat="server">
                                                                    <i title="Comments" id="iconComments" runat="server" class="fa fa-comment" style="color: #333333;"
                                                                        aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:LinkButton ID="lbtnReviewPatientRev" runat="server">
                                                        <i title="Reviewed from Patient Review" style="color: #37e537;" class="fa fa-thumbs-up"   aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                                <asp:LinkButton ID="lbtnAnotherReviewed" runat="server">
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
                    <br />
                </ItemTemplate>
            </asp:Repeater>
            <asp:HiddenField ID="hdntranspose" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
