<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DM_Freeze.aspx.cs" Inherits="CTMS.DM_Freeze" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="CommonFunctionsJs/DM/DivExpandCollapse.js"></script>

    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>

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
        }
    </script>

    <script type="text/javascript" language="javascript">

        function countCheckboxes() {
            var inputElems = document.getElementsByTagName("input");
            var count = 0;
            for (var i = 0; i < inputElems.length; i++) {
                if (inputElems[i].type === "checkbox" && inputElems[i].checked === true) {
                    count++;
                }
            }
            if (count < 1) {
                alert("Select at least one data to freeze");
                event.preventDefault();
            }
        }

        function Check_child(element) {

            $(element).closest('tr').next().find('td').find('table').find('input[type=checkbox][id*=Chkchild_FreezeYN]').each(function () {
                if ($(element).prop('checked') == true) {
                    $(this).prop('checked', true);
                }
                else {
                    $(this).prop('checked', false);
                }
            });
        }

        function Check_All(element) {
            $('input[type=checkbox][id*=Chk_FreezeYN]').each(function () {
                if ($(element).prop('checked') == true) {
                    $(this).prop('checked', true);
                }
                else {
                    $(this).prop('checked', false);
                }
            });
            $('input[type=checkbox][id*=Chkchild_FreezeYN]').each(function () {
                if ($(element).prop('checked') == true) {
                    $(this).prop('checked', true);
                }
                else {
                    $(this).prop('checked', false);
                }
            });
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

        function VIEW_DM_DATA(element) {

            var PVID = "", RECID = "", SUBJID = "", VISITID = "", MODULEID = "", TABLENAME = "", MODULENAME = "";

            PVID = $(element).closest('tr').find('td:eq(0)').find('span').text();
            RECID = $(element).closest('tr').find('td:eq(1)').find('span').text();

            if ((($(element).closest('table').attr('id')).indexOf('Gridchild') != -1)) {

                //find  value with parent  grid                
                SUBJID = $(element).closest('table').closest('tr').prev().find('td:eq(12)').find('span').text();
                VISITID = $(element).closest('table').closest('tr').prev().find('td:eq(2)').find('span').text();
                MODULEID = $(element).closest('table').closest('tr').prev().find('td:eq(3)').find('span').text();
                TABLENAME = $(element).closest('table').closest('tr').prev().find('td:eq(4)').find('span').text();
                MODULENAME = $(element).closest('table').closest('tr').prev().find('td:eq(14)').find('span').text();
            }
            else {

                SUBJID = $(element).closest('tr').find('td:eq(12)').find('span').text();
                VISITID = $(element).closest('tr').find('td:eq(2)').find('span').text();
                MODULEID = $(element).closest('tr').find('td:eq(3)').find('span').text();
                TABLENAME = $(element).closest('tr').find('td:eq(4)').find('span').text();
                MODULENAME = $(element).closest('tr').find('td:eq(14)').find('span').text();
            }

            var test = "DM_DATA.aspx?MODULEID=" + MODULEID + "&VISITID=" + VISITID + "&SUBJID=" + SUBJID + "&PVID=" + PVID + "&RECID=" + RECID + "&TABLENAME=" + TABLENAME + "&MODULENAME=" + MODULENAME;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=550,width=1050";
            window.open(test, '_blank', strWinProperty);

            return false;
        }

        function ShowOpenQuery_PVID_RECID(element) {

            var PVID = $(element).closest('tr').find('td:eq(0)').find('span').text();
            var RECID = $(element).closest('tr').find('td:eq(1)').find('span').text();

            $.ajax({
                type: "POST",
                url: "AjaxFunction_DM.aspx/ShowOpenQuery_PVID_RECID",
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
                        $('#divOpenAllQuery').html(data.d)

                        $("#popup_OpenAllQuery").dialog({
                            title: "Query Details",
                            width: 900,
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

            var PVID = $(element).closest('tr').find('td:eq(0)').find('span').text();
            var RECID = $(element).closest('tr').find('td:eq(1)').find('span').text();

            $.ajax({
                type: "POST",
                url: "AjaxFunction_DM.aspx/ShowAnsQuery_PVID_RECID",
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
                        $('#divOpenAllQuery').html(data.d)

                        $("#popup_OpenAllQuery").dialog({
                            title: "Query Details",
                            width: 900,
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="scrpt" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Data Freeze
            </h3>
        </div>

        <div class="lblError">
            <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700; font-size: small;"></asp:Label>
        </div>
        <div class="box-body">
            <div runat="server" id="Div1" class="form-group" style="display: inline-flex">
                <div class="form-group" style="display: inline-flex">
                    <label class="label" style="color: Maroon;">
                        Type:
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="drpType" ForeColor="Blue" runat="server" OnSelectedIndexChanged="drpType_SelectedIndexChanged" Width="200px"
                            AutoPostBack="True" CssClass="form-control ">
                            <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Site Wise Bulk Freezing" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Subject Wise Bulk Freezing" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Visit Wise Bulk Freezing" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Record Level Freezing" Value="4"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div runat="server" id="DivINV" class="form-group" style="display: inline-flex" visible="false">
                <div class="form-group" style="display: inline-flex">
                    <label class="label" style="color: Maroon;">
                        Site ID:
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="drpInvID" ForeColor="Blue" runat="server" OnSelectedIndexChanged="drpInvID_SelectedIndexChanged"
                            AutoPostBack="True" CssClass="form-control ">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="form-group" style="display: inline-flex" runat="server" id="DivSubject" visible="false">
                <label class="label" style="color: Maroon;">
                    Subject Id:
                </label>
                <div class="Control">
                    <asp:ListBox ID="lstSubjects" AutoPostBack="true" runat="server" CssClass="width300px select"
                        SelectionMode="Multiple" Width="300px" OnSelectedIndexChanged="lstSubjects_SelectedIndexChanged"></asp:ListBox>
                </div>
            </div>
            <div class="form-group" style="display: inline-flex" runat="server" id="DivVisits" visible="false">
                <label class="label" style="color: Maroon;">
                    Visit:
                </label>
                <div class="Control">
                    <asp:ListBox ID="lstVisits" AutoPostBack="true" runat="server" CssClass="width300px select"
                        SelectionMode="Multiple" Width="300px" OnSelectedIndexChanged="lstVisits_SelectedIndexChanged"></asp:ListBox>
                </div>
            </div>
            <asp:Button ID="Btn_Get_Data" runat="server" OnClick="Btn_Get_Data_Click" CssClass="btn btn-primary btn-sm cls-btnSave" Visible="false"
                Text="Get Data" />
            <asp:Button ID="btnBulkFreezing" runat="server" OnClick="btnBulkFreezing_Click" CssClass="btn btn-DarkGreen btn-sm" Visible="false"
                Text="Bulk Freezing" />
        </div>
        <div class="box-body">
            <div class="form-group ">
                <label id="selectchkall" visible="false" runat="server" class="label pull-right">
                    Select All:
                            <asp:CheckBox ID="Chk_Select_All" runat="server" AutoPostBack="True" OnCheckedChanged="Chk_Select_All_CheckedChanged" />
                </label>
                <div style="width: 100%; overflow: auto; padding-top: 10px;">
                    <div class="box">
                        <asp:GridView ID="Grd_Freeze" runat="server" AutoGenerateColumns="False" CellPadding="3" HeaderStyle-ForeColor="Blue"
                            CssClass="table table-bordered table-striped" CellSpacing="2" OnRowDataBound="Grd_Freeze_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="PVID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPVID" runat="server" Text='<%# Bind("PVID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RECID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRECID" runat="server" Text='<%# Bind("RECID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="VISITNUM" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVISITNUM" runat="server" Text='<%# Bind("VISITNUM") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MODULEID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMODULEID" runat="server" Text='<%# Bind("MODULEID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TableName" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTableName" runat="server" Text='<%# Bind("TABLENAME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Icon" HeaderStyle-CssClass="disp-inline" ItemStyle-CssClass="txt_center">
                                    <HeaderTemplate>
                                        <a href="JavaScript:ManipulateAll('PVID1');" id="_PVID" style="color: #333333">
                                            <i id="imgPVID1" class="icon-plus-sign-alt"></i></a>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div runat="server" id="anchor">
                                            <a href="JavaScript:divexpandcollapse('PVID1<%# Eval("PVID") %>');" style="color: #333333">
                                                <i id="imgPVID1<%# Eval("PVID") %>" class="icon-plus-sign-alt"></i>
                                            </a>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-CssClass="txt_center">
                                    <HeaderTemplate>
                                        <asp:Button ID="Btn_Update" runat="server" OnClick="Btn_Update_Click" CssClass="btn btn-primary btn-sm cls-btnSave"
                                            Text="Freeze YN" OnClientClick="return countCheckboxes();" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_FreezeYN" CssClass="freezcheck" runat="server" onclick="Check_child(this)" />
                                        <asp:Label ID="lblQuery" runat="server" Text="Open Queries" ForeColor="Red" Visible="false"> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View" ItemStyle-CssClass="txt_center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbview" runat="server" Class="fa fa-search" ToolTip="View Data" OnClientClick="return VIEW_DM_DATA(this);"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Query" ItemStyle-CssClass="txt_center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkQUERYSTATUS" ToolTip="Open Query" OnClientClick="return ShowOpenQuery_PVID_RECID(this);" Visible="false" runat="server">
                                                                     <i class="fa fa-question-circle" style="font-size:17px;color:maroon"></i></asp:LinkButton>
                                        &nbsp;
                                                   <asp:LinkButton ID="lnkQUERYANS" Visible="false" ToolTip="Answered Open Query" OnClientClick="return ShowAnsQuery_PVID_RECID(this);" runat="server">
                                                                     <i class="fa fa-question-circle" style="font-size:17px;color:blue;"></i></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Page Status" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Image ID="lnkPAGENUM" runat="server" Style="height: 20px;" CommandName="GOTOPAGE"
                                            ImageUrl="Images/New_Page.png"></asp:Image>
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
                                        <label>Signed Off Details</label><br />
                                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Signed Off By]</label><br />
                                        <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                        <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div runat="server" id="divSignOff">
                                            <div>
                                                <asp:Label ID="InvSignOffBYNAME" runat="server" Text='<%# Bind("InvSignOffBYNAME") %>' ForeColor="Blue"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="InvSignOff_CAL_DAT" runat="server" Text='<%# Bind("InvSignOff_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="InvSignOff_CAL_TZDAT" runat="server" Text='<%# Bind("InvSignOff_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Subject Id" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSUBJID" runat="server" Text='<%# Bind("SUBJID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Visit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVISIT" runat="server" Text='<%# Bind("VISIT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Module Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMODULENAME" runat="server" Text='<%# Bind("MODULENAME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Multiple YN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:Label ID="MULTIPLEYN" runat="server" Text='<%# Bind("MULTIPLEYN") %>'></asp:Label>
                                        <asp:Label ID="PAGESTATUS" runat="server" Text='<%# Bind("IsComplete") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <tr>
                                            <td colspan="100%" style="padding: 2px; padding-left: 1%;">
                                                <div style="float: right; font-size: 13px;">
                                                </div>
                                                <div>
                                                    <div id="PVID1<%# Eval("PVID") %>" style="display: none; position: relative; overflow: auto;">
                                                        <asp:GridView ID="Gridchild" runat="server" CellPadding="4" AutoGenerateColumns="false" CssClass="table table-bordered table-striped" OnRowDataBound="Gridchild_RowDataBound" HeaderStyle-ForeColor="Maroon">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="PVID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPVID" runat="server" Text='<%# Bind("PVID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="RECID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRECID" runat="server" Text='<%# Bind("RECID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderStyle-Width="50px">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="Chkchild_FreezeYN" CssClass="freezcheck" runat="server" />
                                                                        <asp:Label ID="lblQuery" runat="server" Text="Open Queries" ForeColor="Red" Visible="false"> </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="View" ItemStyle-CssClass="txt_center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="LBGOTOPAGE" runat="server" Class="fa fa-search" OnClientClick="return VIEW_DM_DATA(this);"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Query" ItemStyle-CssClass="txt_center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkQUERYSTATUS" ToolTip="Open Query" OnClientClick="return ShowOpenQuery_PVID_RECID(this);" Visible="false" runat="server">
                                                                     <i class="fa fa-question-circle" style="font-size:17px;color:maroon"></i></asp:LinkButton>
                                                                        &nbsp;
                                                   <asp:LinkButton ID="lnkQUERYANS" ToolTip="Answered Open Query" Visible="false" OnClientClick="return ShowAnsQuery_PVID_RECID(this);" runat="server">
                                                                     <i class="fa fa-question-circle" style="font-size:17px;color:blue;"></i></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Page Status" ItemStyle-CssClass="text-center">
                                                                    <ItemTemplate>
                                                                        <asp:Image ID="lnkPAGENUM" runat="server" Style="height: 20px;" CommandName="GOTOPAGE"
                                                                            ImageUrl="Images/New_Page.png"></asp:Image>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Record No." ItemStyle-CssClass="txt_center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="RECORDNO" runat="server" Text='<%# Bind("RECORDNO") %>'></asp:Label>
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
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
