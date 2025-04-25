<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NSAE_REOPEN_REQ_LIST.aspx.cs" Inherits="CTMS.NSAE_REOPEN_REQ_LIST" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <script src="CommonFunctionsJs/DivExpandCollapse.js"></script>
    <link href="CommonStyles/ModalPopup.css" rel="stylesheet" />
    <script src="CommonFunctionsJs/Datatable1.js"></script>
    <style type="text/css">
        #Reason {
            background-color: #fff;
            padding: 100px;
        }

        .center {
            background-color: black;
            text-align: left;
            width: 100%;
            height: 30px;
            color: white;
            font-weight: 400;
            padding-top: 6px;
        }

        .close {
            position: absolute;
            right: 0px;
            top: 0px;
            width: 30px;
            height: 30px;
            opacity: 1;
            border: solid 1px black;
            background-color: black;
        }

            .close:hover {
                opacity: 1;
            }
    </style>
    <style type="text/css">
        .Popup2 {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            min-width: 900px;
            max-width: 950px;
        }

        .modal-body2 {
            position: relative;
            padding: 0px;
        }
    </style>
    <script type="text/javascript">

        function ShowOpenQuery_SAEID_RECID(element) {

            var SAEID = $("#MainContent_lblSAEID").text();
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

            var SAEID = $("#MainContent_lblSAEID").text();
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

            var SAEID = $("#MainContent_lblSAEID").text();
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

            var SAEID = $("#MainContent_lblSAEID").text();
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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-success">
        <div class="box-header">
            <h3 class="box-title">Re-Open SAE
            </h3>
            &nbsp&nbsp&nbsp<a href="JavaScript:ManipulateAll('_Pat_');" id="_Folder" style="color: #333333; font-size: x-large; margin-top: 5px;"><i id="img_Pat_" class="icon-plus-sign-alt"></i></a>
            <div class="pull-right">
                <asp:LinkButton ID="lbtnSupportingDocs" runat="server" Text="Supporting Document" ToolTip="Supporting Document" Height="27px" ForeColor="White" CssClass="btn btn-sm btn-success" OnClick="lbtnSupportingDocs_Click">
                    <i class="fa fa-files-o"></i>&nbsp;&nbsp;Supporting Document&nbsp;&nbsp;<asp:Label ID="lblCount" runat="server" Visible="false" CssClass="badge badge-info" Font-Bold="true" ForeColor="Black" Font-Size="13px" Text=""></asp:Label>
                </asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnApprove" runat="server" Text="Action" CssClass="btn btn-sm btn-DARKORANGE" />&nbsp&nbsp&nbsp
            </div>
        </div>
        <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700; font-size: 15px;"></asp:Label>
        <div class="box-body" runat="server">
            <div style="border-style: double; margin-bottom: 1px;">
                <br />
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="col-md-6">
                                <asp:Label Font-Bold="True" runat="server" ForeColor="Blue">INV ID :
                                </asp:Label>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="lblINVID" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="col-md-6">
                                <asp:Label Font-Bold="true" runat="server" ForeColor="Blue">Subject Id :
                                </asp:Label>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="lblSUBJID" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="col-md-6">
                                <asp:Label Font-Bold="true" runat="server" ForeColor="Blue">SAEID :
                                </asp:Label>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="lblSAEID" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="col-md-6">
                                <asp:Label Font-Bold="true" runat="server" ForeColor="Blue">SAE :
                                </asp:Label>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="lblSAE" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="col-md-6">
                                <asp:Label Font-Bold="true" runat="server" ForeColor="Blue">Event Id :
                                </asp:Label>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="lblEventId" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="col-md-6">
                                <asp:Label Font-Bold="true" runat="server" ForeColor="Blue">Event Term :
                                </asp:Label>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="lblEventTerm" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="col-md-6">
                                <asp:Label Font-Bold="true" runat="server" ForeColor="Blue">Status :
                                </asp:Label>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="col-md-6">
                                <asp:Label Font-Bold="true" runat="server" ForeColor="Blue">Closed By :
                                </asp:Label>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="lblClosedBy" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="col-md-6">
                                <asp:Label Font-Bold="true" runat="server" ForeColor="Blue">Closed Datetime(Server) :
                                </asp:Label>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="lblClosedDTServer" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="col-md-6">
                                <asp:Label Font-Bold="true" runat="server" ForeColor="Blue">Closed Datetime(User), (Timezone) :
                                </asp:Label>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="lblClosedDTUTC" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="col-md-6">
                                <asp:Label Font-Bold="true" runat="server" ForeColor="Blue">Closed Reason :
                                </asp:Label>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="lblClosedReason" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="box box-warning">
        <div class="box-header" style="display: inline-flex; width: 100%;">
            <h3 class="box-title" style="width: 100%;">
                <a href="JavaScript:divexpandcollapse('grdid');" id="_Folder1"><i id="imggrdid" class="ion-plus-circled" style="font-size: larger; color: #666666"></i></a>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label6" runat="server" Text="Request Logs"></asp:Label>
            </h3>
        </div>
        <div class="card-body" style="display: none;" id="grdid">
            <div class="box">
                <asp:GridView ID="gridActionLogs" HeaderStyle-CssClass="text_center" runat="server" OnPreRender="gridsigninfo_PreRender" CssClass="table table-bordered Datatable1 table-striped" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="STATUS" HeaderText="Status" />
                        <asp:BoundField DataField="REQ_ACTION" HeaderText="Action" />
                        <asp:BoundField DataField="REQ_REASON" HeaderText="Request Reason" />
                        <asp:BoundField DataField="REQ_CAL_DAT" HeaderText="Request Datetime(Server)" />
                        <asp:BoundField DataField="REQ_CAL_TZDAT" HeaderText="Request Datetime(User), (Timezone)" />
                        <asp:BoundField DataField="REQ_BYNAME" HeaderText="Request By" />

                        <asp:BoundField DataField="ACTION" HeaderText="Action" />
                        <asp:BoundField DataField="ACTION_COMMENT" HeaderText="Action Comment" />
                        <asp:BoundField DataField="ACTION_CAL_DAT" HeaderText="Action Datetime(Server)" />
                        <asp:BoundField DataField="ACTION_CAL_TZDAT" HeaderText="Action Datetime(User), (Timezone)" />
                        <asp:BoundField DataField="ACTION_BYNAME" HeaderText="Action By" />
                    </Columns>
                </asp:GridView>
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
                            <div style="width: 100%; overflow: auto;">
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
                                                    <div id="divReviewedBy">
                                                        <div>
                                                            <asp:Label ID="REVIEWEDBYNAME" runat="server" Enabled="false" Font-Bold="true" Text='<%# Bind("REVIEWEDBYNAME") %>' ForeColor="Blue"></asp:Label>
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
                                                            <asp:Label ID="SDV_STATUSNAME" runat="server" Enabled="false" Font-Bold="true" Text='<%# Bind("SDV_STATUSNAME") %>' ForeColor="Blue"></asp:Label>
                                                        </div>
                                                        <div>
                                                            <asp:Label ID="lblSDVBYNAME" runat="server" Enabled="false" Font-Bold="true" Text='<%# Bind("SDVBYNAME") %>' ForeColor="Blue"></asp:Label>
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
    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlReOpenSAE" TargetControlID="btnApprove"
        BackgroundCssClass="Background">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlReOpenSAE" runat="server" Style="display: none;" CssClass="Popup1">
        <h5 class="heading">Re-Open Request</h5>
        <div class="modal-body" runat="server">
            <div id="ModelPopup2">
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="txtReason" TextMode="MultiLine" placeholder="Enter Comment for approve or disapprove...." CssClass="form-control required" Style="width: 575px; height: 100px;"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-3">
                        &nbsp;
                    </div>
                    <div class="col-md-9">
                        <asp:Button ID="btnApproveReq" runat="server" CssClass="btn btn-DarkGreen btn-sm cls-btnSave"
                            Text="Approve" OnClick="btnApproveReq_Click" />
                        &nbsp;
                            &nbsp;
                            <asp:Button ID="btnDisApproveReq" runat="server" Text="Disapprove"
                                CssClass="btn btn-danger btn-sm cls-btnSave" OnClick="btnDisApproveReq_Click" />
                        &nbsp;
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" PopupControlID="pnlSAESupportdoc" TargetControlID="Button2"
        BackgroundCssClass="Background">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlSAESupportdoc" runat="server" Style="display: none;" CssClass="Popup2">
        <asp:Button runat="server" ID="Button2" Style="display: none" />
        <div>
            <h5 class="heading">SAE Supporting Document
                <div class="align-right" style="display: -webkit-inline-box; margin-left: 80%;">
                    <asp:LinkButton D="btnSuportDocCancel" runat="server" Text="Closed" OnClick="btnSuportDocCancel_Click" Style="font-size: 20px; color: white;"><i class="fa fa-times"></i></asp:LinkButton>
                </div>
            </h5>
        </div>
        <div class="modal-body2" runat="server" style="height: 264px;">
            <div class=" box box-primary" runat="server">
                <div class="col-md-12">
                    <div style="width: 100%; height: 264px; overflow: auto; margin-top: 10px;">
                        <asp:GridView ID="grdSupport_Doc" runat="server" AutoGenerateColumns="False"
                            CssClass="table table-bordered table-striped Datatable" OnPreRender="grdSupport_Doc_PreRender" OnRowCommand="grdSupport_Doc_RowCommand" OnRowDataBound="grdSupport_Doc_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                                    ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                        <asp:HiddenField ID="hdnDeleted" runat="server" Value='<%#Eval("DELETED") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblDownloadSupportDoc" runat="server" CommandArgument='<%# Bind("ID") %>' CssClass="btn"
                                            CommandName="DownloadSupportDoc" ToolTip="Download"><i class="fa fa-download"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFileName" runat="server" Text='<%# Bind("FILENAME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Notes">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNotes" runat="server" Text='<%# Bind("NOTES") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="align-left">
                                    <HeaderTemplate>
                                        <label>Uploaded By Details</label><br />
                                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Uploaded By]</label><br />
                                        <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                        <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div>
                                            <div>
                                                <asp:Label ID="lblUploaded" runat="server" Text='<%# Bind("UPLOADBYNAME") %>' ForeColor="Blue"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="lblUPLOAD_CAL_DAT" runat="server" Text='<%# Bind("UPLOAD_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="lblUPLOAD_CAL_TZDAT" runat="server" Text='<%# Bind("UPLOAD_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="align-left">
                                    <HeaderTemplate>
                                        <label>Deleted By Details</label><br />
                                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Deleted By]</label><br />
                                        <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                        <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div>
                                            <div>
                                                <asp:Label ID="lblDELETEDBYNAME" runat="server" Text='<%# Bind("DELETEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="lblDELETED_CAL_DAT" runat="server" Text='<%# Bind("DELETED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="lblDELETED_CAL_TZDAT" runat="server" Text='<%# Bind("DELETED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
