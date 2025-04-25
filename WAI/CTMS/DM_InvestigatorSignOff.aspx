<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DM_InvestigatorSignOff.aspx.cs" Inherits="CTMS.DM_InvestigatorSignOff" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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

            $('.noSpace').keyup(function () {
                this.value = this.value.replace(/\s/g, '');
            });
        }
    </script>
    <script src="CommonFunctionsJs/DivExpandCollapse.js"></script>
    <style type="text/css">
        .btn1:hover {
            background-color: #2e6da4 !important;
            color: white !important;
        }

        .input-group i {
            position: absolute !important;
        }

        .icon {
            padding: 10px;
            color: white;
            min-width: 50px;
            background-color: #000080;
            border-top-right-radius: 18px;
            border-bottom-right-radius: 18px;
        }

        #txt_Pwd, #txt_UserName {
            width: 100%;
            padding: 0px;
            text-align: center !important;
        }
    </style>
    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #fff;
            border: 3px solid #ccc;
            padding: 10px;
            width: 450px;
        }
    </style>
    <script type='text/javascript'>

        function countCheckboxes() {
            var inputElems = document.getElementsByTagName("input");
            var count = 0;
            for (var i = 0; i < inputElems.length; i++) {
                if (inputElems[i].type === "checkbox" && inputElems[i].checked === true) {
                    count++;
                }
            }
            if (count < 1) {
                alert("Select at least one data to Investigator Sign Off");
                event.preventDefault();
            }
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Investigator Sign Off
            </h3>
        </div>
        <div class="lblError">
            <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700; font-size: small;"></asp:Label>
        </div>
        <div class="box-body">
            <div runat="server" id="DivINV" class="form-group" style="display: inline-flex">
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
            <div class="form-group" style="display: inline-flex">
                <label class="label" style="color: Maroon;">
                    Subject Id:
                </label>
                <div class="Control">
                    <asp:DropDownList ID="drpSubID" ForeColor="Blue" runat="server" CssClass="form-control select"
                        AutoPostBack="True" OnSelectedIndexChanged="drpSubID_SelectedIndexChanged" SelectionMode="Single">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group" style="display: inline-flex">
                <label class="label" style="color: Maroon;">
                    Visit:
                </label>
                <div class="Control">
                    <asp:DropDownList ID="drpVisit" ForeColor="Blue" runat="server" OnSelectedIndexChanged="drpVisit_SelectedIndexChanged" AutoPostBack="true"
                        CssClass="form-control ">
                    </asp:DropDownList>
                </div>
            </div>
            <asp:Button ID="Btn_Get_Data" runat="server" OnClick="Btn_Get_Data_Click" CssClass="btn btn-primary btn-sm cls-btnSave"
                Text="Get Data" />
        </div>
        <div class="box-body">
            <div class="form-group ">
                <asp:GridView ID="Grd_InvestigatorSignOff" runat="server" AutoGenerateColumns="False"
                    CellPadding="3" CellSpacing="2" CssClass="table table-bordered table-striped" HeaderStyle-ForeColor="Blue"
                    OnRowDataBound="Grd_InvestigatorSignOff_RowDataBound">
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
                        <asp:TemplateField HeaderText="Investigator Sign Off" ItemStyle-CssClass="txt_center">
                            <HeaderTemplate>
                                <asp:Button ID="Btn_Update" runat="server" OnClick="Btn_Update_Click" CssClass="btn btn-primary btn-sm "
                                    Text="Investigator Sign Off" OnClientClick="return countCheckboxes();" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="Chk_InvSignOff" runat="server" />
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
                        <asp:TemplateField>
                            <ItemTemplate>
                                <tr>
                                    <td colspan="100%" style="padding: 2px; padding-left: 1%;">
                                        <div style="float: right; font-size: 13px;">
                                        </div>
                                        <div id="PVID1<%# Eval("PVID") %>" style="display: none; position: relative; overflow: scroll;">
                                            <div class="box">
                                                <div style="width: 100%; overflow: scroll;">
                                                    <asp:GridView ID="Gridchild" runat="server" CellPadding="4" AutoGenerateColumns="false" HeaderStyle-ForeColor="Maroon" CssClass="table table-bordered table-striped" OnRowDataBound="Gridchild_RowDataBound">
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
                                                                    <asp:Label ID="lblQuery" runat="server" ForeColor="Red" Visible="false" Text="Data Deleted"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View" ItemStyle-CssClass="txt_center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LBGOTOPAGE" runat="server" Class="fa fa-search" OnClientClick="return VIEW_DM_DATA(this);"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Query" ItemStyle-CssClass="txt_center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkQUERYSTATUS" ToolTip="Open Query" OnClientClick="return ShowOpenQuery_PVID_RECID(this);" runat="server">
                                                                     <i class="fa fa-question-circle" style="font-size:17px;color:maroon"></i></asp:LinkButton>
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
                    <EmptyDataTemplate>
                        <div align="center">
                            No records found.
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
        <asp:Label ID="Open_Request_temp" Style="display: none;" runat="server" Text="">.</asp:Label>
        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BehaviorID="mpe" PopupControlID="Panel11" TargetControlID="Open_Request_temp"
            CancelControlID="btnClose1" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="Panel11" runat="server" CssClass="modalPopup" align="center" Style="display: none; border: 5px solid #ccc">
            <div class="login-box">
                <div class="login-logo">
                    <h1 style="color: #00001a!important; font-size: 30px;">
                        <a href="#"></a>
                        <b>Investigator Electronic Sign off</b>
                    </h1>
                    <br />
                </div>
                <div></div>
                <div></div>
                <div class="box">
                    <div class="box-body login-box-body">
                        <asp:Label ID="Label2" runat="server" Style="color: #CC3300; font-size: 17px; font-weight: bold;"
                            ForeColor="Red"></asp:Label>
                        <div class="input-group mb-3" style="display: -ms-flexbox; display: flex; width: 100%; margin-bottom: 15px; border: 1px solid #008000; border-radius: 18px">
                            <asp:TextBox ID="txt_UserName" CssClass="noSpace" Style="width: 100%; height: 38px; padding: 10px; height: 38px; border: none; outline: none; border-radius: 0px; text-align: left; border-top-left-radius: 18px; border-bottom-left-radius: 18px;" runat="server" placeholder="User Id" autocomplete="off" class="numeric"></asp:TextBox>
                            <span class="fa fa-user icon"></span>
                        </div>
                        <div class="input-group mb-3" style="display: -ms-flexbox; display: flex; width: 100%; margin-bottom: 15px; border: 1px solid #008000; border-radius: 18px">
                            <asp:TextBox ID="txt_Pwd" CssClass="noSpace" Style="width: 100%; height: 38px; padding: 10px; height: 38px; border: none; outline: none; border-radius: 0px; text-align: left; border-top-left-radius: 18px; border-bottom-left-radius: 18px;" runat="server" TextMode="Password" type="password" placeholder="Password" data-type="password" autocomplete="off"></asp:TextBox>
                            <span class="fa fa-lock icon"></span>
                        </div>
                        <div class="input-group mb-3" style="display: -ms-flexbox; display: flex; width: 100%; margin-bottom: 15px;">
                            <div class="col-1 text-center">
                                <asp:CheckBox ID="chkINVSIGN" runat="server" class="required" AutoPostBack="False" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </div>
                            <div class="col-11 text-center">
                                <div style="text-align: justify; margin-right: 10px;">By my dated signature below, I, confirm that I have reviewed the subject visit data in electronic case report form for completeness and accuracy.</div>
                            </div>
                        </div>
                        <div class="input-group mb-3" style="display: -ms-flexbox; display: flex; width: 100%; margin-bottom: 25px;">
                            <div class="col-6 text-center" style="text-align: center">
                                <asp:Button ID="Btn_Login1" runat="server" class="btn1" type="submit" Style="height: 38px; width: 120px; color: #fff; background-color: #0026ff; border-color: #2e6da4; left: 100px; top: 150px; margin-left: 70px; border-radius: 18px;" value="Submit" Text="Submit" OnClick="Btn_Login_Click" />
                            </div>
                            <div class="col-6 text-center" style="display: flex; justify-content: center">
                                <asp:Button ID="btnClose1" class="btn1" Style="height: 38px; color: #fff; width: 120px; background-color: #0026ff; border-color: #0026ff; position: absolute; right: 0; margin-right: 70px; border-radius: 18px;" runat="server" Text="Cancel" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
