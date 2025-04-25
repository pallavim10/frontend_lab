<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DM_SDV_ALL.aspx.cs" Inherits="CTMS.DM_SDV_ALL" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />

    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <link href="CommonStyles/Searchable_DropDown.css" rel="stylesheet" />
    <script src="CommonFunctionsJs/DM/QueryOveRide.js"></script>
    <script type="text/javascript">
        function pageLoad() {
            $('.select').select2();

            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: true,
                fixedHeader: true
            });
        }
    </script>
    <script type="text/javascript">

        function MarkAsSDV(element) {

            var PVID = $(element).closest('tr').find('td:eq(7)').find('span').html();
            var RECID = $(element).closest('tr').find('td:eq(8)').find('span').html();
            var TABLENAME = $(element).closest('tr').find('td:eq(9)').find('span').html();
            var VISITNUM = $("#MainContent_drpVisit option:selected").val();
            var VISIT = $("#MainContent_drpVisit option:selected").text();
            var MODULEID = $(element).closest('tr').find('td:eq(10)').find('span').html();
            var MODULENAME = $(element).closest('tr').find('td:eq(11)').find('span').html();
            var SUBJID = $("#MainContent_drpSubID option:selected").val();
            var INVID = $("#MainContent_drpInvID option:selected").val();

            $.ajax({
                type: "POST",
                url: "AjaxFunction_DM.aspx/DM_MarkAsSDV",

                data: '{PVID: "' + PVID + '", RECID: "' + RECID + '" ,VISITNUM: "' + VISITNUM + '",MODULEID: "' + MODULEID + '",SUBJID: "' + SUBJID + '",INVID:"' + INVID + '",TABLENAME:"' + TABLENAME + '",VISIT:"' + VISIT + '",MODULENAME:"' + MODULENAME + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {

                    var SDVSTATUS = res.d;

                    $('#' + $(element).closest('tr').find('td:eq(0)').find("a[id*='lbtnSDV']").attr('id')).addClass("disp-none");
                    $('#' + $(element).closest('tr').find('td:eq(0)').find("a[id*='lbtnSDVDONE']").attr('id')).addClass("disp-none");

                    if (SDVSTATUS == 'True') {

                        alert(MODULENAME + ' marked as SDV.');
                        $('#' + $(element).closest('tr').find('td:eq(0)').find("a[id*='lbtnSDVDONE']").attr('id')).removeClass("disp-none");

                    }
                    else if (SDVSTATUS == 'False') {

                        alert(MODULENAME + ' SDV voided.');
                        $('#' + $(element).closest('tr').find('td:eq(0)').find("a[id*='lbtnSDV']").attr('id')).removeClass("disp-none");

                    }
                    else {

                        alert("This data can not be SDV.");

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

        function UnMarkAsSDV(element) {

            var PVID = $(element).closest('tr').find('td:eq(7)').find('span').html();
            var RECID = $(element).closest('tr').find('td:eq(8)').find('span').html();
            var TABLENAME = $(element).closest('tr').find('td:eq(9)').find('span').html();
            var VISITNUM = $("#MainContent_drpVisit option:selected").val();
            var VISIT = $("#MainContent_drpVisit option:selected").text();
            var MODULEID = $(element).closest('tr').find('td:eq(10)').find('span').html();
            var MODULENAME = $(element).closest('tr').find('td:eq(11)').find('span').html();
            var SUBJID = $("#MainContent_drpSubID option:selected").val();
            var INVID = $("#MainContent_drpInvID option:selected").val();

            $.ajax({
                type: "POST",
                url: "AjaxFunction_DM.aspx/DM_UnMarkAsSDV",
                data: '{PVID: "' + PVID + '", RECID: "' + RECID + '", VISITNUM: "' + VISITNUM + '",MODULEID: "' + MODULEID + '", SUBJID: "' + SUBJID + '",INVID:"' + INVID + '",TABLENAME:"' + TABLENAME + '",VISIT:"' + VISIT + '",MODULENAME:"' + MODULENAME + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {

                    var SDVSTATUS = res.d;

                    $('#' + $(element).closest('tr').find('td:eq(0)').find("a[id*='lbtnSDV']").attr('id')).addClass("disp-none");
                    $('#' + $(element).closest('tr').find('td:eq(0)').find("a[id*='lbtnSDVDONE']").attr('id')).addClass("disp-none");

                    if (SDVSTATUS == 'True') {
                        alert(MODULENAME + ' marked as SDV.');
                        $('#' + $(element).closest('tr').find('td:eq(0)').find("a[id*='lbtnSDVDONE']").attr('id')).removeClass("disp-none");

                    }
                    else if (SDVSTATUS == 'False') {
                        alert(MODULENAME + ' SDV voided.');
                        $('#' + $(element).closest('tr').find('td:eq(0)').find("a[id*='lbtnSDV']").attr('id')).removeClass("disp-none");

                    }
                    else {

                        alert("This data can not be SDV.");

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
    <script type="text/javascript">

        function ShowOpenQuery_PVID_RECID(element) {

            var PVID = $(element).closest('tr').find('td:eq(7)').find('span').text();
            var RECID = $(element).closest('tr').find('td:eq(8)').find('span').html();

            // Get the full path of the URL
            let path = window.location.pathname;

            // Extract the page name
            let pageName = path.substring(path.lastIndexOf('/') + 1);

            $.ajax({
                type: "POST",
                url: "AjaxFunction_DM.aspx/ShowOpenQuery_PVID_RECID",
                data: '{PVID: "' + PVID + '",RECID:"' + RECID + '",PAGENAME:"' + pageName + '"}',

                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        $('#divOpenAllQuery').html(data.d)

                        $("#popup_OpenAllQuery").dialog({
                            title: "Query Details",
                            width: 1200,
                            height: 500,
                            modal: true,
                            buttons: {
                                "Close": function () { $(this).dialog("close"); }
                            }
                        });
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

        function ShowAnsQuery_PVID_RECID(element) {

            var PVID = $(element).closest('tr').find('td:eq(7)').find('span').text();
            var RECID = $(element).closest('tr').find('td:eq(8)').find('span').html();

            // Get the full path of the URL
            let path = window.location.pathname;

            // Extract the page name
            let pageName = path.substring(path.lastIndexOf('/') + 1);

            $.ajax({
                type: "POST",
                url: "AjaxFunction_DM.aspx/ShowAnsQuery_PVID_RECID",
                data: '{PVID: "' + PVID + '",RECID:"' + RECID + '",PAGENAME:"' + pageName + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        $('#divAnsAllQuery').html(data.d)

                        $("#popup_AnsAllQuery").dialog({
                            title: "Answered Query",
                            width: 1200,
                            height: 500,
                            modal: true,
                            buttons: {
                                "Close": function () { $(this).dialog("close"); }
                            }
                        });
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

        function ShowClosedQuery_PVID_RECID(element) {

            var PVID = $(element).closest('tr').find('td:eq(7)').find('span').text();
            var RECID = $(element).closest('tr').find('td:eq(8)').find('span').html();

            $.ajax({
                type: "POST",
                url: "AjaxFunction_DM.aspx/ShowClosedQuery_PVID_RECID",
                data: '{PVID: "' + PVID + '",RECID:"' + RECID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        $('#DivClosedAllQuery').html(data.d)

                        $("#popup_ClosedAllQuery").dialog({
                            title: "Closed Query",
                            width: 1200,
                            height: 500,
                            modal: true,
                            buttons: {
                                "Close": function () { $(this).dialog("close"); }
                            }
                        });
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

        function ShowQueryComment(element) {

            var ID = $(element).closest('tr').find('td').eq(0).text().trim();

            $.ajax({
                type: "POST",
                url: "AjaxFunction_DM.aspx/ShowQueryComment",
                data: '{ID: "' + element + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        $('#divShowQryComment').html(data.d)

                        $("#Popup_ShowQryComment").dialog({
                            title: "Query Comment",
                            width: 700,
                            height: 350,
                            modal: true,
                            buttons: {
                                "Close": function () { $(this).dialog("close"); }
                            }
                        });
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

        function Show_MM_QueryHistory(element) {

            $.ajax({
                type: "POST",
                url: "AjaxFunction_DM.aspx/Show_MM_QueryHistory",
                data: '{ID: "' + element + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        $('#divShow_MM_QryHistory').html(data.d)

                        $("#Popup_Show_MM_QryHistory").dialog({
                            title: "MM Query History",
                            width: 850,
                            height: 450,
                            modal: true,
                            buttons: {
                                "Close": function () { $(this).dialog("close"); }
                            }
                        });
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

        function showAuditTrail_All(element) {

            var PVID = $(element).closest('tr').find('td:eq(7)').find('span').text();
            var RECID = $(element).closest('tr').find('td:eq(8)').find('span').html();

            $.ajax({
                type: "POST",
                url: "AjaxFunction_DM.aspx/Get_ALL_AUDIT_BY_PVID_RECID",
                data: '{PVID: "' + PVID + '",RECID:"' + RECID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        $('#DivAuditTrailALL').html(data.d)

                        $("#popup_AuditTrailALL").dialog({
                            title: "Audit Trail",
                            width: 950,
                            height: 450,
                            modal: true,
                            buttons: {
                                "Close": function () { $(this).dialog("close"); }
                            }
                        });
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

        function ViewData(element) {

            var RECID = $(element).closest('tr').find('td:eq(8)').find('span').html();

            var VISITNUM = $("#MainContent_drpVisit option:selected").val();
            var VISIT = $("#MainContent_drpVisit option:selected").text();
            var MODULEID = $(element).closest('tr').find('td:eq(10)').find('span').html();
            var MODULENAME = $(element).closest('tr').find('td:eq(11)').find('span').html();
            var SUBJID = $("#MainContent_drpSubID option:selected").val();
            var INVID = $("#MainContent_drpInvID option:selected").val();
            var MULTIPLEYN = $(element).closest('tr').find('td:eq(12)').find('span').html();

            var test;
            if (MULTIPLEYN == "True") {

                if ($(element).closest('tr').find('td:eq(1)').find('span').html() == "True") {
                    test = "DM_DataSDV.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITNUM + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&RECID=" + RECID + "&MULTIPLEYN=1" + "&DELETED=1" + "&VISITSDV=1";
                }
                else {
                    test = "DM_DataSDV.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITNUM + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&RECID=" + RECID + "&MULTIPLEYN=1" + "&VISITSDV=1";
                }
            }
            else {
                if ($(element).closest('tr').find('td:eq(1)').find('span').html() == "True") {
                    test = "DM_DataSDV.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITNUM + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&RECID=" + RECID + "&MULTIPLEYN=0" + "&DELETED=1" + "&VISITSDV=1";
                }
                else {
                    test = "DM_DataSDV.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITNUM + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&RECID=" + RECID + "&MULTIPLEYN=0" + "&VISITSDV=1";
                }
            }

            window.open(test);
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Visit SDV
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
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
                        Subject ID:
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="drpSubID" runat="server" CssClass="form-control required select" AutoPostBack="True"
                            OnSelectedIndexChanged="drpSubID_SelectedIndexChanged" SelectionMode="Single">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group" style="display: inline-flex">
                    <label class="label" style="color: Maroon;">
                        Visit:
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="drpVisit" runat="server" CssClass="form-control required select">
                        </asp:DropDownList>
                    </div>
                </div>
                <asp:Button ID="btngetdata" runat="server" Text="Get Data" CssClass="btn btn-primary btn-sm cls-btnSave"
                    OnClick="btngetdata_Click" />
                &nbsp&nbsp&nbsp <a href="JavaScript:ManipulateAll('_Pat_');" id="_Folder" style="color: #333333; font-size: x-large; margin-top: 5px;"><i id="img_Pat_" class="icon-plus-sign-alt"></i></a>
            </div>
        </div>
    </div>
    <asp:Repeater runat="server" OnItemDataBound="repeatData_ItemDataBound" ID="repeatData">
        <ItemTemplate>
            <div class="box box-primary">
                <div class="box-header">
                    <div runat="server" style="display: inline-flex; padding: 0px; margin: 4px 0px 0px 10px;"
                        id="anchor">
                        <a href="JavaScript:divexpandcollapse('_Pat_<%# Eval("MODULEID") %>');" style="color: #333333">
                            <i id="img_Pat_<%# Eval("MODULEID") %>" class="icon-plus-sign-alt"></i></a>
                        <h3 class="box-title" style="padding-top: 0px;">
                            <asp:Label ID="lblHeader" runat="server" Text='<%# Bind("MODULENAME") %>'></asp:Label>
                        </h3>
                    </div>
                    <div class="pull-right disp-none">
                        <asp:Label ID="lblMODULEID" CssClass="disp-none" runat="server" Text='<%# Bind("MODULEID") %>'></asp:Label>&nbsp&nbsp&nbsp&nbsp
                    </div>
                </div>
                <div id="_Pat_<%# Eval("MODULEID") %>" style="display: none; overflow: auto">
                    <div class="box-body">
                        <div class="rows">
                            <div class="fixTableHead">
                                <asp:GridView ID="grd_Records" runat="server" AutoGenerateColumns="True" Width="100%"
                                    CssClass="table table-bordered table-striped Datatable" EmptyDataText="No Records Available"
                                    OnRowDataBound="grd_Records_RowDataBound" OnPreRender="grd_data_PreRender">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="align-center" ItemStyle-CssClass="align-center"
                                            HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDeletedStatus" Visible="false" Text="Record Deleted" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                                                <asp:LinkButton ID="lbtnSDV" runat="server" ToolTip="SDV" OnClientClick="return MarkAsSDV(this)">
                                                    <i id="iconSDV" runat="server" class="fa fa-square"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnSDVDONE" runat="server" ToolTip="SDV" OnClientClick="return UnMarkAsSDV(this)">
                                                    <i id="i1" runat="server" class="fa fa-check-square"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="align-center" ItemStyle-CssClass="align-center"
                                            HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="DELETE" CssClass="disp-none" Text='<%# Bind("DELETE") %>' runat="server"></asp:Label>
                                                <asp:LinkButton ID="lnkPAGENUM" runat="server" ToolTip="View" OnClientClick="return ViewData(this)"><i class="fa fa-search"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Query" HeaderStyle-CssClass="align-center"
                                            ItemStyle-CssClass="align-center">
                                            <ItemTemplate>
                                                <div style="display: inline-flex;">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lnkQUERYSTATUS" ToolTip="Open Query" OnClientClick="return ShowOpenQuery_PVID_RECID(this);" runat="server">
                                                                     <i class="fa fa-question-circle" style="font-size:17px;color:maroon;"></i></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lnkQUERYANS" ToolTip="Answered Open Query" OnClientClick="return ShowAnsQuery_PVID_RECID(this);" runat="server">
                                                                     <i class="fa fa-question-circle" style="font-size:17px;color:blue;"></i></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lnkQUERYCLOSE" ToolTip="Closed Query" OnClientClick="return ShowClosedQuery_PVID_RECID(this);" runat="server">
                                                                     <i class="fa fa-question-circle" style="font-size:17px;color:darkgreen;"></i></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Audit Trail" HeaderStyle-CssClass="width100px align-center"
                                            ItemStyle-CssClass="width100px align-center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="AD" ToolTip="Audit trail" OnClientClick="return showAuditTrail_All(this);" class="disp-none" runat="server">
                                                    <i class="fa fa-history" id="ADICON" runat="server" style="font-size: 17px"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="align-left">
                                            <HeaderTemplate>
                                                <label>SDV Details</label><br />
                                                <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[SDV Status]</label><br />
                                                <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[SDV By]</label><br />
                                                <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div runat="server" id="divSDV">
                                                    <div>
                                                        <asp:Label ID="SDV_STATUSNAME" runat="server" Text='<%# Bind("SDV_STATUSNAME") %>' ForeColor="Blue"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="SDVBYNAME" runat="server" Text='<%# Bind("SDVBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="SDV_CAL_DAT" runat="server" Text='<%# Bind("SDV_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="SDV_CAL_TZDAT" runat="server" Text='<%# Bind("SDV_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="align-left">
                                            <HeaderTemplate>
                                                <label>Frozen Details</label><br />
                                                <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Frozen By]</label><br />
                                                <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div runat="server" id="divFreeze">
                                                    <div>
                                                        <asp:Label ID="FREEZEBYNAME" runat="server" Text='<%# Bind("FREEZEBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="FREEZE_CAL_DAT" runat="server" Text='<%# Bind("FREEZE_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="FREEZE_CAL_TZDAT" runat="server" Text='<%# Bind("FREEZE_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="align-left">
                                            <HeaderTemplate>
                                                <label>Locked Details</label><br />
                                                <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Locked By]</label><br />
                                                <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div runat="server" id="divLock">
                                                    <div>
                                                        <asp:Label ID="LOCKBYNAME" runat="server" Text='<%# Bind("LOCKBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="LOCK_CAL_DAT" runat="server" Text='<%# Bind("LOCK_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="LOCK_CAL_TZDAT" runat="server" Text='<%# Bind("LOCK_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="disp-none"
                                            ItemStyle-CssClass="disp-none">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPVID" Font-Size="Small" Text='<%# Bind("PVID") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Record No." HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRECID" Font-Size="Small" Text='<%# Bind("RECID") %>' CssClass="disp-none" runat="server"></asp:Label>
                                                <asp:Label ID="Label5" Font-Size="Small" Text='<%# Bind("RECORDNO") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="disp-none"
                                            ItemStyle-CssClass="disp-none">
                                            <ItemTemplate>
                                                <asp:Label ID="TABLENAME" Font-Size="Small" Text='<%# Bind("TABLENAME") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="disp-none"
                                            ItemStyle-CssClass="disp-none">
                                            <ItemTemplate>
                                                <asp:Label ID="MODULEID" Font-Size="Small" Text='<%# Bind("MODULEID") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="disp-none"
                                            ItemStyle-CssClass="disp-none">
                                            <ItemTemplate>
                                                <asp:Label ID="MODULENAME" Font-Size="Small" Text='<%# Bind("MODULENAME") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="disp-none"
                                            ItemStyle-CssClass="disp-none">
                                            <ItemTemplate>
                                                <asp:Label ID="MULTIPLEYN" Font-Size="Small" Text='<%# Bind("MULTIPLEYN") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
