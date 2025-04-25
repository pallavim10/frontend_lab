<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NSAE_SDV_ALL.aspx.cs" Inherits="CTMS.NSAE_SDV_ALL" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <script src="CommonFunctionsJs/DivExpandCollapse.js"></script>
    <script src="CommonFunctionsJs/Datatable1.js"></script>
    <link href="CommonStyles/ModalPopup.css" rel="stylesheet" />
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <style type="text/css">
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #0000ff;
        }

        .select2-container--default .select2-selection--single {
            margin-top: 2px;
        }
    </style>
    <script type="text/javascript">
        function pageLoad() {
            $('.select').select2();

            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: false,
                fixedHeader: true
            });

            $(".Datatable").parent().parent().addClass('fixTableHead');
        }
    </script>
    <script type="text/javascript">

        function ShowOpenQuery_SAEID_RECID(element) {

            var SAEID = $("#MainContent_drpSAEID").find("option:selected").val();
            var RECID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var MODULEID = $(element).closest('tr').find('td:eq(1)').find('span').html();

            $.ajax({
                type: "POST",
                url: "AjaxFunction_SAE.aspx/SAE_ShowOpenQuery_SAEID_RECID",
                data: '{SAEID: "' + SAEID + '",RECID: "' + RECID + '",MODULEID:"' + MODULEID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        $('#SAE_divOpenAllQuery').html(data.d)

                        $("#SAE_OpenAllQuery").dialog({
                            title: "Open Query",
                            width: 1100,
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

        function ShowAnsQuery_SAEID_RECID(element) {

            var SAEID = $("#MainContent_drpSAEID").find("option:selected").val();
            var RECID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var MODULEID = $(element).closest('tr').find('td:eq(1)').find('span').html();

            $.ajax({
                type: "POST",
                url: "AjaxFunction_SAE.aspx/SAE_ShowAnsQuery_SAEID_RECID",
                data: '{SAEID: "' + SAEID + '",RECID: "' + RECID + '",MODULEID:"' + MODULEID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        $('#SAE_divAnsAllQuery').html(data.d)

                        $("#SAE_popup_AnsAllQuery").dialog({
                            title: "Answered Query",
                            width: 1100,
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

        function ShowClosedQuery_SAEID_RECID(element) {

            var SAEID = $("#MainContent_drpSAEID").find("option:selected").val();
            var RECID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var MODULEID = $(element).closest('tr').find('td:eq(1)').find('span').html();

            $.ajax({
                type: "POST",
                url: "AjaxFunction_SAE.aspx/SAE_ShowClosedQuery_SAEID_RECID",
                data: '{SAEID: "' + SAEID + '",RECID: "' + RECID + '",MODULEID:"' + MODULEID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        $('#SAE_DivClosedAllQuery').html(data.d)

                        $("#SAE_popup_ClosedAllQuery").dialog({
                            title: "Closed Query",
                            width: 1100,
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

        function SAE_ShowQueryComment(element) {

            var ID = $(element).closest('tr').find('td').eq(0).text().trim();

            $.ajax({
                type: "POST",
                url: "AjaxFunction_SAE.aspx/SAE_ShowQueryComment",
                data: '{ID: "' + ID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        $('#SAE_divShowQryComment').html(data.d)

                        $("#SAE_Popup_ShowQryComment").dialog({
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

        function SAE_showAuditTrail_All(element) {

            var SAEID = $("#MainContent_drpSAEID").find("option:selected").val();
            var RECID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var MODULEID = $(element).closest('tr').find('td:eq(1)').find('span').html();

            $.ajax({
                type: "POST",
                url: "AjaxFunction_SAE.aspx/Get_ALL_AUDIT_BY_SAEID_RECID",
                data: '{SAEID: "' + SAEID + '",RECID: "' + RECID + '",MODULEID:"' + MODULEID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        $('#SAE_DivAuditTrailALL').html(data.d);

                        $("#SAE_popup_AuditTrailALL").dialog({
                            title: "Audit Trail",
                            width: 1200,
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

        function MarkAsSDV(element) {

            var SAEID = $("#MainContent_drpSAEID").find("option:selected").val();
            var RECID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var MODULEID = $(element).closest('tr').find('td:eq(1)').find('span').html();
            var SUBJID = $("#MainContent_drpSubID option:selected").val();
            var INVID = $("#MainContent_drpInvID option:selected").val();
            var STATUS = $(element).closest('tr').find('td:eq(3)').find("span[id*='STATUS']").html();

            $.ajax({
                type: "POST",
                url: "AjaxFunction_SAE.aspx/SAE_MarkAsSDV",
                data: '{SAEID: "' + SAEID + '", RECID: "' + RECID + '" ,MODULEID: "' + MODULEID + '",SUBJID: "' + SUBJID + '",INVID:"' + INVID + '",STATUS:"' + STATUS + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {

                    var SDVSTATUS = res.d;

                    $('#' + $(element).closest('tr').find("a[id*='lbtnSDV']").attr('id')).addClass("disp-none");
                    $('#' + $(element).closest('tr').find("a[id*='lbtnSDVDONE']").attr('id')).addClass("disp-none");

                    if (SDVSTATUS == 'True') {

                        alert('SDV marked successfully.');
                        $('#' + $(element).closest('tr').find("a[id*='lbtnSDVDONE']").attr('id')).removeClass("disp-none");

                    }
                    else if (SDVSTATUS == 'False') {

                        alert('SDV marked voided successfully.');
                        $('#' + $(element).closest('tr').find("a[id*='lbtnSDV']").attr('id')).removeClass("disp-none");

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

            var SAEID = $("#MainContent_drpSAEID").find("option:selected").val();
            var RECID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var MODULEID = $(element).closest('tr').find('td:eq(1)').find('span').html();
            var SUBJID = $("#MainContent_drpSubID option:selected").val();
            var INVID = $("#MainContent_drpInvID option:selected").val();
            var STATUS = $(element).closest('tr').find('td:eq(3)').find("span[id*='STATUS']").html();

            $.ajax({
                type: "POST",
                url: "AjaxFunction_SAE.aspx/SAE_UnMarkAsSDV",
                data: '{SAEID: "' + SAEID + '", RECID: "' + RECID + '" ,MODULEID: "' + MODULEID + '",SUBJID: "' + SUBJID + '",INVID:"' + INVID + '",STATUS:"' + STATUS + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {

                    var SDVSTATUS = res.d;

                    $('#' + $(element).closest('tr').find("a[id*='lbtnSDV']").attr('id')).addClass("disp-none");
                    $('#' + $(element).closest('tr').find("a[id*='lbtnSDVDONE']").attr('id')).addClass("disp-none");

                    if (SDVSTATUS == 'True') {
                        alert('SDV marked successfully.');
                        $('#' + $(element).closest('tr').find("a[id*='lbtnSDVDONE']").attr('id')).removeClass("disp-none");

                    }
                    else if (SDVSTATUS == 'False') {
                        alert('SDV marked voided successfully.');
                        $('#' + $(element).closest('tr').find("a[id*='lbtnSDV']").attr('id')).removeClass("disp-none");

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

        function ViewData(element) {

            var SAEID = $("#MainContent_drpSAEID").find("option:selected").val();
            var RECID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var MODULEID = $(element).closest('tr').find('td:eq(1)').find('span').html();
            var SUBJID = $("#MainContent_drpSubID option:selected").val();
            var INVID = $("#MainContent_drpInvID option:selected").val();
            var STATUS = $(element).closest('tr').find('td:eq(3)').find("span[id*='STATUS']").html();
            var SAE = $(element).closest('tr').find('td:eq(3)').find("span[id*='SAE']").html();
            var MULTIPLEYN = $(element).closest('tr').find('td:eq(9)').find('span').html();

            var test;
            if (MULTIPLEYN == "True") {

                if ($(element).closest('tr').find('td:eq(3)').find("span[id*='DELETE']").html() == "True") {
                    test = "NSAE_MULTIPLE_DATA_ENTRY_SDV.aspx?INVID=" + INVID + "&SUBJID=" + SUBJID + "&SAE=" + SAE + "&STATUS=" + STATUS + "&SAEID=" + SAEID + "&RECID=" + RECID + "&MODULEID=" + MODULEID + "&MULTIPLEYN=1" + "&DELETED=1";
                }
                else {
                    test = "NSAE_MULTIPLE_DATA_ENTRY_SDV.aspx?INVID=" + INVID + "&SUBJID=" + SUBJID + "&SAE=" + SAE + "&STATUS=" + STATUS + "&SAEID=" + SAEID + "&RECID=" + RECID + "&MODULEID=" + MODULEID + "&MULTIPLEYN=1";
                }
            }
            else {
                if ($(element).closest('tr').find('td:eq(3)').find("span[id*='DELETE']").html() == "True") {
                    test = "NSAE_DATA_ENTRY_SDV.aspx?INVID=" + INVID + "&SUBJID=" + SUBJID + "&SAE=" + SAE + "&STATUS=" + STATUS + "&SAEID=" + SAEID + "&RECID=" + RECID + "&MODULEID=" + MODULEID + "&MULTIPLEYN=0" + "&DELETED=1";
                }
                else {
                    test = "NSAE_DATA_ENTRY_SDV.aspx?INVID=" + INVID + "&SUBJID=" + SUBJID + "&SAE=" + SAE + "&STATUS=" + STATUS + "&SAEID=" + SAEID + "&RECID=" + RECID + "&MODULEID=" + MODULEID + "&MULTIPLEYN=0";
                }
            }

            window.open(test);
            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%-- <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">SAE SDV
            </h3>
        </div>
        <div id="SAE_popup_AuditTrail" title="Audit Trail" class="disp-none">
            <div id="SAE_grdAuditTrail">
            </div>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
                <div class="form-group has-warning">
                    <asp:Label ID="Label1" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
                </div>
                <div runat="server" id="DivINV" class="form-group" style="display: inline-flex">
                    <div class="form-group" style="display: inline-flex">
                        <label class="label">
                            Site ID:
                        </label>
                        <div class="Control">
                            <asp:DropDownList ID="drpInvID" runat="server" AutoPostBack="True" CssClass="form-control "
                                OnSelectedIndexChanged="drpInvID_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="form-group" style="display: inline-flex">
                    <label class="label">
                        Subject ID:
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="drpSubID" runat="server" CssClass="form-control required select" AutoPostBack="true"
                            OnSelectedIndexChanged="drpSubID_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group" style="display: inline-flex">
                    <label class="label">
                        SAE ID:
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="drpSAEID" runat="server" CssClass="form-control required">
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
                <div class="box-header" style="display: inline-flex;">
                    <div runat="server" style="display: inline-flex; padding: 0px; margin: 4px 0px 0px 10px;"
                        id="anchor">
                        <a href="JavaScript:divexpandcollapse('_Pat_<%# Eval("MODULEID") %>');" style="color: #333333">
                            <i id="img_Pat_<%# Eval("MODULEID") %>" class="icon-plus-sign-alt"></i></a>
                    </div>
                    <h3 class="box-title" style="padding-top: 0px;">
                        <asp:Label ID="lblHeader" runat="server" Text='<%# Bind("MODULENAME") %>'></asp:Label>
                    </h3>
                </div>
                <div id="_Pat_<%# Eval("MODULEID") %>" style="display: none; position: relative; overflow: auto;">
                    <div class="box-body">
                        <div class="rows">
                            <div  class="fixTableHead">
                                <div>
                                    <asp:GridView ID="grd_Records" runat="server" CellPadding="3" AutoGenerateColumns="True"
                                        CssClass="table table-bordered table-striped Datatable" Width="100%" ShowHeader="True"
                                        EmptyDataText="No Records Available" CellSpacing="2" OnRowDataBound="grd_Records_RowDataBound"
                                        OnPreRender="grd_data_PreRender">
                                        <RowStyle HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="RECID" HeaderStyle-CssClass="disp-none"
                                                ItemStyle-CssClass="disp-none">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRECID" Text='<%# Bind("RECID") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MODULEID" HeaderStyle-CssClass="disp-none"
                                                ItemStyle-CssClass="disp-none">
                                                <ItemTemplate>
                                                    <asp:Label ID="MODULEID" Text='<%# Bind("MODULEID") %>' runat="server" ForeColor="Brown"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="align-center" ItemStyle-CssClass="align-center"
                                                HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                <ItemTemplate>
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
                                                    <asp:Label ID="SAE" CssClass="disp-none" Text='<%# Bind("SAE") %>' runat="server"></asp:Label>
                                                    <asp:Label ID="DELETE" CssClass="disp-none" Text='<%# Bind("DELETE") %>' runat="server"></asp:Label>
                                                    <asp:Label ID="STATUS" CssClass="disp-none" Text='<%# Bind("STATUS") %>' runat="server"></asp:Label>
                                                    <asp:LinkButton ID="lnkPAGENUM" runat="server" ToolTip="View" OnClientClick="return ViewData(this)"><i class="fa fa-search"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Query" HeaderStyle-CssClass="width100px align-center"
                                                ItemStyle-CssClass="width100px align-center">
                                                <ItemTemplate>
                                                    <div style="display: inline-flex;">
                                                        <asp:LinkButton ID="lnkQUERYSTATUS" ToolTip="Open Query" OnClientClick="return ShowOpenQuery_SAEID_RECID(this);" runat="server">
                                                            <i class="fa fa-question-circle" style="font-size: 17px; color: maroon;"></i>
                                                        </asp:LinkButton>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                   <asp:LinkButton ID="lnkQUERYANS" ToolTip="Answered Open Query" OnClientClick="return ShowAnsQuery_SAEID_RECID(this);" runat="server">
                                                       <i class="fa fa-question-circle" style="font-size: 17px; color: blue;"></i>
                                                   </asp:LinkButton>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lnkQUERYCLOSE" ToolTip="Closed Query" OnClientClick="return ShowClosedQuery_SAEID_RECID(this);" runat="server">
                                                            <i class="fa fa-question-circle" style="font-size: 17px; color: darkgreen;"></i>
                                                        </asp:LinkButton>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;

                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Audit Trail" HeaderStyle-CssClass="width100px align-center"
                                                ItemStyle-CssClass="width100px align-center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="AD" ToolTip="Audit trail" OnClientClick="return SAE_showAuditTrail_All(this);" class="disp-none" runat="server">
                                                        <i class="fa fa-history" id="ADICON" runat="server" style="font-size: 17px"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="align-left">
                                                <HeaderTemplate>
                                                    <label>Review Details</label><br />
                                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Review By]</label><br />
                                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div runat="server" id="divReviewedBy">
                                                        <div>
                                                            <asp:Label ID="REVIEWEDBYNAME" runat="server" Text='<%# Bind("REVIEWEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                        </div>
                                                        <div>
                                                            <asp:Label ID="REVIEWED_CAL_DAT" runat="server" Text='<%# Bind("REVIEWED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                        </div>
                                                        <div>
                                                            <asp:Label ID="REVIEWED_CAL_TZDAT" runat="server" Text='<%# Bind("REVIEWED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="align-left">
                                                <HeaderTemplate>
                                                    <label>SDV Details</label><br />
                                                    <label style="color: brown; font-weight: lighter; margin-bottom: 0px;">[SDV Status]</label><br />
                                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[SDV By]</label><br />
                                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div runat="server" id="divSDVBY">
                                                        <div>
                                                            <asp:Label ID="SDV_STATUSNAME" runat="server" Text='<%# Bind("SDV_STATUSNAME") %>' ForeColor="Brown"></asp:Label>
                                                        </div>
                                                        <div>
                                                            <asp:Label ID="lblSDVBYNAME" runat="server" Text='<%# Bind("SDVBYNAME") %>' ForeColor="Blue"></asp:Label>
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
                                                    <label>Medical Review Details</label><br />
                                                    <label style="color: brown; font-weight: lighter; margin-bottom: 0px;">[Medical Review Status]</label><br />
                                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Medical Review By]</label><br />
                                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div runat="server" id="divMedicakBy">
                                                        <div>
                                                            <asp:Label ID="MR_STATUSNAME" runat="server" Text='<%# Bind("MR_STATUSNAME") %>' ForeColor="Brown"></asp:Label>
                                                        </div>
                                                        <div>
                                                            <asp:Label ID="lblMRBYNAME" runat="server" Text='<%# Bind("MRBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                        </div>
                                                        <div>
                                                            <asp:Label ID="MR_CAL_DAT" runat="server" Text='<%# Bind("MR_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                        </div>
                                                        <div>
                                                            <asp:Label ID="MR_CAL_TZDAT" runat="server" Text='<%# Bind("MR_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                        </div>
                                                    </div>
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
            </div>
            <br />
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
