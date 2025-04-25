<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IWRS_MNG_RANDOMIZATION.aspx.cs" Inherits="CTMS.IWRS_MNG_RANDOMIZATION" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <link href="CommonStyles/ModalPopup.css" rel="stylesheet" />
    <style type="text/css">
        .select2-container .select2-selection--multiple {
            min-height: 60px;
            width: 300px;
        }
    </style>
    <script type="text/javascript">

        function Check_All_Block(element) {

            $('input[type=checkbox][id*=Chek_Block]').each(function () {
                // >>this<< refers to specific checkbox

                if ($(element).prop('checked') == true) {
                    $(this).prop('checked', true);
                }
                else {
                    $(this).prop('checked', false);
                }

            });

        }

        function Check_BlockReasonEntered() {

            if ($('#MainContent_txtBlockComments').val().trim() == '') {
                alert('Please enter Comments');
                return false;
            }
        };

        function Check_All_UnBlock(element) {

            $('input[type=checkbox][id*=Chek_UnBlock]').each(function () {
                // >>this<< refers to specific checkbox

                if ($(element).prop('checked') == true) {
                    $(this).prop('checked', true);
                }
                else {
                    $(this).prop('checked', false);
                }
            });
        }

        function Check_UnBlockReasonEntered() {

            if ($('#MainContent_txtUnBlockComments').val().trim() == '') {
                alert('Please enter Comments');
                return false;
            }
        };

    </script>

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


    </script>
    <script type="text/javascript">
        function pageLoad() {
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
            width: 100pt;
            height: 20pt;
        }
    </style>

    <script type="text/javascript">

        function setActiveTab(tabId) {

            document.getElementById('<%= ActiveTabID.ClientID %>').value = tabId;

        }

        document.addEventListener('DOMContentLoaded', function () {
            const activeTabId = document.getElementById('<%= ActiveTabID.ClientID %>').value;

             if (activeTabId) {
                 document.querySelectorAll('.nav-tabs li').forEach(tab => tab.classList.remove('active'));
                 document.querySelectorAll('.tab-content').forEach(content => content.style.display = "none");

                 const activeTabLink = document.querySelector(`[href="#${activeTabId}"]`);
                 if (activeTabLink) {
                     activeTabLink.parentElement.classList.add('active');
                     document.getElementById(activeTabId).style.display = "block";
                 }
             }
         });

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Manage Randomization
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    <asp:HiddenField ID="hdnID" runat="server" />
                    <asp:Label runat="server" ID="lblHeader" Text="Export Sample Specs" Visible="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div id="tabscontainer" class="nav-tabs-custom" runat="server">
        <asp:HiddenField ID="ActiveTabID" runat="server" />
        <ul class="nav nav-tabs">
            <li id="li1" runat="server" class="active"><a href="#tab-1" data-toggle="tab" onclick="setActiveTab('tab-1')">Allocate Randomization Nos</a></li>
            <li id="li2" runat="server"><a href="#tab-2" data-toggle="tab" onclick="setActiveTab('tab-2')">Manage Randomization Nos</a></li>
            <li id="li3" runat="server"><a href="#tab-3" data-toggle="tab" onclick="setActiveTab('tab-3')">Randomization Nos Logs</a></li>
        </ul>
        <div class="tab">
            <div id="tab-1" class="tab-content">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box warningning">
                            <div class="box-header with-border" style="float: left;">
                                <h3 class="box-title">Allocate Randomization Number</h3>
                                <br />
                            </div>
                            <br />
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div runat="server" id="DivINV" class="form-group" style="display: inline-flex">
                                            <div class="form-group" style="display: inline-flex">
                                                <label class="label">
                                                    Site ID:
                                                </label>
                                                <div class="Control">
                                                    <asp:DropDownList ID="drpSITEID" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div runat="server" id="Div1" class="form-group" style="display: inline-flex">
                                            <div class="form-group" style="display: inline-flex">
                                                <label class="label">
                                                    From Block:
                                                </label>
                                                <div class="Control">
                                                    <asp:DropDownList ID="drpFrom" runat="server" CssClass="form-control" OnSelectedIndexChanged="drpFrom_SelectedIndexChanged"
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div runat="server" id="Div2" class="form-group" style="display: inline-flex">
                                            <div class="form-group" style="display: inline-flex">
                                                <label class="label">
                                                    To Block:
                                                </label>
                                                <div class="Control">
                                                    <asp:DropDownList ID="drpTo" runat="server" CssClass="form-control" AutoPostBack="True"
                                                        OnSelectedIndexChanged="drpTo_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div runat="server" id="Div3" class="form-group" style="display: inline-flex">
                                            <div class="form-group" style="display: inline-flex">
                                                <asp:LinkButton runat="server" ID="lbtnAllocate" Text="Allocate" ForeColor="White"
                                                    CssClass="btn btn-primary btn-sm" OnClick="lbtnAllocate_Click"></asp:LinkButton>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div style="width: 100%; overflow: auto;">
                                            <div>
                                                <asp:GridView ID="gvRands" runat="server" AllowSorting="True"
                                                    AutoGenerateColumns="False" CssClass="table table-bordered table-striped txt_center Datatable1">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Randomization Number" HeaderStyle-Width="50%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_RAND" Width="100%" CssClass="label" runat="server" Text='<%# Bind("RANDNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Block" HeaderStyle-Width="50%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Block" Width="100%" CssClass="label" runat="server" Text='<%# Bind("BLOCK") %>'></asp:Label>
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
                        </div>
                    </div>
                </div>
            </div>
            <div id="tab-2" class="tab-content">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box warningning">
                            <div class="box-header with-border" style="float: left;">
                                <h3 class="box-title">Manage Randomization Nos</h3>
                                <br />
                            </div>
                            <br />
                            <div class="box-body">
                                <div class="rows">
                                    <div class="col-md-12">
                                        <div style="display: inline-flex">
                                            <div style="display: inline-flex">
                                                <label class="label ">
                                                    From Block:
                                                </label>
                                                <div class="Control">
                                                    <asp:DropDownList runat="server" ID="ddlFromBlock" CssClass="form-control  width200px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="display: inline-flex">
                                            <div style="display: inline-flex">
                                                <label class="label ">
                                                    To Block:
                                                </label>
                                                <div class="Control">
                                                    <asp:DropDownList runat="server" ID="ddlToBlock" CssClass="form-control  width200px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="display: inline-flex">
                                            <div style="display: inline-flex">
                                                <asp:Button ID="btnGetData" runat="server" Text="Get Data"
                                                    CssClass="btn btn-primary " OnClick="btnGetData_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div style="width: 100%; overflow: auto;">
                                            <div>
                                                <asp:GridView ID="grdRandNos" runat="server" AllowSorting="True" Width="96%" AutoGenerateColumns="False"
                                                    CssClass="table table-bordered Datatable txt_center table-striped notranslate"
                                                    OnPreRender="grd_data_PreRender" OnRowDataBound="grdRandNos_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Randomization Number">
                                                            <ItemTemplate>
                                                                <asp:Label ID="RANDNO" runat="server" Text='<%# Bind("RANDNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Block Number">
                                                            <ItemTemplate>
                                                                <asp:Label ID="BLOCK" runat="server" Text='<%# Bind("BLOCK") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="STATUSNAME" runat="server" Text='<%# Bind("STATUSNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <label>Blocked Details</label><br />
                                                                <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Blocked By]</label><br />
                                                                <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                                <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                                <label style="color: red; font-weight: lighter; margin-bottom: 0px;">Comment</label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <div>
                                                                    <div>
                                                                        <asp:Label ID="BLOCKEDBYNAME" runat="server" Text='<%# Bind("BLOCKEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                                    </div>
                                                                    <div>
                                                                        <asp:Label ID="BLOCKED_CAL_DAT" runat="server" Text='<%# Bind("BLOCKED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                                    </div>
                                                                    <div>
                                                                        <asp:Label ID="BLOCKED_CAL_TZDAT" runat="server" Text='<%# Eval("BLOCKED_CAL_TZDAT")+" "+Eval("BLOCKED_TZVAL") %>' ForeColor="Red"></asp:Label>
                                                                    </div>
                                                                    <div>
                                                                        <asp:Label ID="BLOCKED_COMM" runat="server" Text='<%# Bind("BLOCKED_COMM") %>'></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ShowHeader="false">
                                                            <HeaderTemplate>
                                                                <asp:Button ID="Block" runat="server" CssClass="btn btn-primary"
                                                                    Text="Block" OnClick="btn_Block" />
                                                                <br />
                                                                <asp:CheckBox ID="ChekAll_Block" runat="server" onclick="Check_All_Block(this)" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chek_Block" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ShowHeader="false">
                                                            <HeaderTemplate>
                                                                <asp:Button ID="Unblock" runat="server" CssClass="btn btn-primary"
                                                                    Text="Unblock" OnClick="btn_UnBlock" />
                                                                <br />
                                                                <asp:CheckBox ID="ChekAll_UnBlock" runat="server" onclick="Check_All_UnBlock(this)" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chek_UnBlock" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="tab-3" class="tab-content">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">Randomization Number Logs</h3>
                                <br />
                                <div class="pull-right" style="margin-right: 5px;">
                                    <a href="#" class="dropdown-toggle btn-info prevent-refresh-button" data-toggle="dropdown" style="color: #FFFFFF">Export Logs&nbsp;<span class="glyphicon glyphicon-download"></span></a>
                                    <ul class="dropdown-menu dropdown-menu-sm">
                                        <li>
                                            <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExcel_Click" CommandName="Excel" ToolTip="Excel"
                                                Text="Excel" CssClass="dropdown-item" Style="color: #333333;">
                                            </asp:LinkButton></li>
                                        <hr style="margin: 5px;" />
                                        <li>
                                            <asp:LinkButton runat="server" ID="btnExportPDF" OnClick="btnExportPDF_Click" CssClass="dropdown-item"
                                                ToolTip="PDF" Text="PDF" Style="color: #333333;">
                                            </asp:LinkButton>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="form-group has-warning">
                                    <asp:Label ID="Label1" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
                                </div>
                                <div style="display: inline-flex">
                                    <label class="label ">
                                        From Date :
                                    </label>
                                    <div class="Control" style="display: inline-flex">
                                        <asp:TextBox ID="txtDateFrom" CssClass="form-control  txtDate" runat="server" autocomplete="off"
                                            Width="120px"></asp:TextBox>
                                    </div>
                                </div>
                                <div runat="server" id="Div4" style="display: inline-flex">
                                    <div style="display: inline-flex">
                                        <label class="label ">
                                            To Date :
                                        </label>
                                        <div class="Control" style="display: inline-flex">
                                            <asp:TextBox ID="txtDateTo" CssClass="form-control  txtDate" runat="server" autocomplete="off"
                                                Width="120px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div runat="server" id="Div5" style="display: inline-flex">
                                    <div style="display: inline-flex">
                                        <asp:Button ID="btnRanLogData" runat="server" Text="Get Data" CssClass="btn btn-primary btn-sm " OnClick="btnRanLogData_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="rows">
                                <div class="col_md-12">
                                    <div style="width: 100%; overflow: auto;">
                                        <div>
                                            <asp:GridView ID="GrdData" runat="server" AutoGenerateColumns="True"
                                                CssClass="table table-bordered table-striped Datatable" OnPreRender="GrdData_PreRender">
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <asp:ModalPopupExtender ID="modalpop_Block" runat="server" PopupControlID="pnl_Block" TargetControlID="Button_Block"
            BackgroundCssClass="Background">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnl_Block" runat="server" Style="display: none;" CssClass="Popup1">
            <asp:Button runat="server" ID="Button_Block" Style="display: none" />
            <h5 class="heading">Please Enter Comment</h5>
            <div class="modal-body" runat="server">
                <div id="ModelPopupBlock">
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="Label4" runat="server" CssClass="wrapperLable" Text="Comments" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtBlockComments" CssClass="form-control-model" ValidationGroup="BlockCommnet" TextMode="MultiLine"
                                runat="server" Style="width: 250px;"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-3">
                            &nbsp;
                        </div>
                        <div class="col-md-9">
                            <asp:Button ID="btnSubmitBlock" runat="server" Style="margin-left: 37px; height: 34px; width: 71px; font-size: 14px;" ValidationGroup="BlockCommnet" CssClass="btn btn-DarkGreen"
                                OnClientClick="return Check_BlockReasonEntered();" Text="Submit" OnClick="btnSubmitBlock_Click" />
                            &nbsp;
                            &nbsp;
                            <asp:Button ID="btnCancelBlock" runat="server" Text="Cancel" ValidationGroup="BlockCommnet"
                                CssClass="btn btn-DarkGreen" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="btnCancelBlock_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <br />
        <asp:ModalPopupExtender ID="modalpop_UnBlock" runat="server" PopupControlID="pnl_UnBlock" TargetControlID="Button_UnBlock"
            BackgroundCssClass="Background">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnl_UnBlock" runat="server" Style="display: none;" CssClass="Popup1">
            <asp:Button runat="server" ID="Button_UnBlock" Style="display: none" />
            <h5 class="heading">Please Enter Comment</h5>
            <div class="modal-body" runat="server">
                <div id="ModelPopupUnBlock">
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="Label2" runat="server" CssClass="wrapperLable" Text="Comments" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtUnBlockComments" CssClass="form-control-model" ValidationGroup="UnBlockCommnet" TextMode="MultiLine"
                                runat="server" Style="width: 250px;"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-3">
                            &nbsp;
                        </div>
                        <div class="col-md-9">
                            <asp:Button ID="btnSubmitUnBlock" runat="server" Style="margin-left: 37px; height: 34px; width: 71px; font-size: 14px;" ValidationGroup="UnBlockCommnet" CssClass="btn btn-DarkGreen"
                                OnClientClick="return Check_UnBlockReasonEntered();" Text="Submit" OnClick="btnSubmitUnBlock_Click" />
                            &nbsp;
                            &nbsp;
                            <asp:Button ID="btnCancelUnBlock" runat="server" Text="Cancel" ValidationGroup="UnBlockCommnet"
                                CssClass="btn btn-DarkGreen" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="btnCancelUnBlock_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>

