<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="DM_DataEntry_MultipleData_INV_Read_Only.aspx.cs"
    Inherits="CTMS.DM_DataEntry_MultipleData_INV_Read_Only" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="js/CKEditor/ckeditor.js" type="text/javascript"></script>
    <script src="js/Input%20Mask/jquery.inputmask.bundle.js" type="text/javascript"></script>
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />


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
    <style type="text/css">
        .custom-tooltip {
            display: none;
            position: absolute;
            background: rgb(255,255,255);
            color: #fff;
            padding: 10px;
            font-weight: bold;
            border-radius: 5px;
            font-size: 14px;
            white-space: pre-line;
            z-index: 100000;
            max-width: 350px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".tooltip-label").hover(function (event) {
                var tooltipText = $(this).attr("data-tooltip");
                $("body").append('<div class="custom-tooltip">' + tooltipText + '</div>');
                $(".custom-tooltip").css({
                    left: event.pageX + 10 + "px",
                    top: event.pageY + 10 + "px"
                }).fadeIn();
            }, function () {
                $(".custom-tooltip").remove();
            });

            $(document).mousemove(function (event) {
                $(".custom-tooltip").css({
                    left: event.pageX + 10 + "px",
                    top: event.pageY + 10 + "px"
                });
            });
        });

    </script>
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

    <link href="CommonStyles/ButtonColor.css" rel="stylesheet" />
    <link href="CommonStyles/DataEntry_Table.css" rel="stylesheet" />
    <link href="CommonStyles/ModalPopup.css" rel="stylesheet" />
    <script src="CommonFunctionsJs/DM/CallChange.js"></script>
    <script src="CommonFunctionsJs/DM/Grid_AuditTrail.js"></script>
    <script src="CommonFunctionsJs/DM/Grid_Comments.js"></script>
    <script src="CommonFunctionsJs/DM/Grid_Queries.js"></script>
    <script src="CommonFunctionsJs/DM/OpenModule.js"></script>
    <script src="CommonFunctionsJs/DM/Query_HidwShow.js"></script>
    <script src="CommonFunctionsJs/DM/Query_HighlightControl.js"></script>
    <script src="CommonFunctionsJs/DM/QueryOveRide.js"></script>
    <script src="CommonFunctionsJs/DM/ShowChild.js"></script>


    <script src="CommonFunctionsJs/btnSave_Required.js"></script>
    <script src="CommonFunctionsJs/CKEDITOR_Limited.js"></script>
    <script src="CommonFunctionsJs/ControlJS.js"></script>
    <script src="CommonFunctionsJs/Datatable1.js" type="javascript"></script>
    <script src="CommonFunctionsJs/DisableButton.js"></script>
    <script src="CommonFunctionsJs/DivExpandCollapse.js"></script>
    <script src="CommonFunctionsJs/OpenPopUp.js"></script>
    <script src="CommonFunctionsJs/TextBox_Options.js"></script>
    <script src="CommonFunctionsJs/DM/LAB_REFRENCE_RANGE.js"></script>
    <script src="CommonFunctionsJs/DM/PARENT_CHILD_LINKED.js"></script>

    <script language="javascript" type="text/javascript">

        $(document).ready(function () {

            if ($('#MainContent_hdnRECID').val() != '-1' && $('#MainContent_hdnRECID').val() != '') {

                $(".Comments").removeClass("disp-none");
                $("#MainContent_lblApplicableStatus").addClass("disp-none");
            }
            else {
                $("#MainContent_lblApplicableStatus").addClass("disp-none");
                $("#MainContent_btnCancle").addClass("disp-none");
            }

            if ($('#MainContent_hdnRECID').val() == '-1' && $("#MainContent_grd_Records tr").length == 0) {

                $("#MainContent_lblApplicableStatus").addClass("disp-none");
            }
            else {
                $("#MainContent_lblApplicableStatus").addClass("disp-none");
            }

            if ($('#MainContent_hdnIsComplete').val() == '2') {

                $("#MainContent_grd_Data").addClass("disp-none");
                $("#MainContent_lblApplicableStatus").removeClass("disp-none");
                $("#MainContent_btnCancle").addClass("disp-none");
            }

            if ('<%=Session["LOCK_STATUS"] %>' != "" && $('#MainContent_hdnRECID').val() == '-1') {

                $("#MainContent_lblApplicableStatus").removeClass("disp-none");
                $("#MainContent_lblApplicableStatus").val("This module is locked.");

            }

            FillAuditDetails();
            FillCommentsDetails();
            FillAnsQUERIES();
            FillOPENQUERIES();
            FillCLOSEQUERIES();
            BindLinkedData();
            SetLinkedData();
            GET_LAB_DATA();
        });

        function OpenModuleStatus() {

            if ($('#MainContent_hdnRECID').val() != '-1') {

                title = "Module Status";
            }
            else {
                title = "Record Status";
            }

            $("#POP_ModuleStatus").removeClass("disp-none");

            $("#POP_ModuleStatus").dialog({
                title: title,
                width: 990,
                height: 370,
                modal: true,
                buttons: {
                    "Close": function () { $(this).dialog("close"); }
                }
            });

            return false;
        }

    </script>

    <script type="text/jscript">

        function pageLoad() {

            $(".nav-tabs a").click(function (event) {
                event.preventDefault();
                $(this).parent().addClass("active");
                $(this).parent().siblings().removeClass("active");
                var tab = $(this).attr("href");
                $(".tab-content").not(tab).css("display", "none");
                $(tab).fadeIn();
            });
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--<div runat="server" id="divHideShow" class="disp-none">
        <input id="hideshow" type="button" class="btn btn-primary btn-sm" value="SHOW QUERY"
            style="font-size: 9px;" />
    </div>--%>
    <asp:GridView ID="grdQUERYDETAILS" runat="server" AutoGenerateColumns="False" CellPadding="4"
        CssClass="table table-bordered table-striped disp-none" DataKeyNames="QUERYTYPE">
        <Columns>
            <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                <ItemTemplate>
                    <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PVID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                <ItemTemplate>
                    <asp:TextBox ID="PVID" runat="server" Text='<%# Bind("PVID") %>' Width="70px" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Query Text">
                <ItemTemplate>
                    <asp:Label ID="QUERYTEXT" ForeColor="Maroon" runat="server" Enabled="false" Text='<%# Bind("QUERYTEXT") %>'
                        Width="500" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="MM_QUERYID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                <ItemTemplate>
                    <asp:Label ID="MM_QUERYID" runat="server" Text='<%# Bind("MM_QUERYID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Module Name">
                <ItemTemplate>
                    <asp:Label ID="MODULENAME" ForeColor="Maroon" runat="server" Text='<%# Bind("MODULENAME") %>' Width="120px"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Field Name">
                <ItemTemplate>
                    <asp:Label ID="FIELDNAME" runat="server" ForeColor="Maroon" Text='<%# Bind("FIELDNAME") %>' Width="100px"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="VARIABLE NAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                <ItemTemplate>
                    <asp:Label ID="VARIABLENAME" runat="server" Text='<%# Bind("VARIABLENAME") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Query Status">
                <ItemTemplate>
                    <asp:Label ID="STATUSNAME" runat="server" ForeColor="Red" Text='<%# Bind("STATUSNAME") %>' Width="100px"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Query Type">
                <ItemTemplate>
                    <asp:Label ID="QUERYTYPE" runat="server" ForeColor="Red" Text='<%# Bind("QUERYTYPE") %>' Width="100px"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lnkOverride" runat="server" Text="Answer" OnClientClick="return OpenOverideData(this)"
                        CommandArgument='<%# Bind("ID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lnkSelect" runat="server" Text="Show" CommandName="Select" OnClientClick="return ResolveHighlightControl(this)" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:GridView ID="grdOpenQuers" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="4"
        CssClass="table table-bordered table-striped disp-none">
        <Columns>
            <asp:TemplateField HeaderText="VARIABLE NAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                <ItemTemplate>
                    <asp:TextBox ID="VARIABLENAME" runat="server" Text='<%# Bind("VARIABLENAME") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:GridView ID="grdAnsQueries" runat="server" Width="96%" AutoGenerateColumns="False" CellPadding="4"
        CssClass="table table-bordered table-striped disp-none">
        <Columns>
            <asp:TemplateField HeaderText="VARIABLE NAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                <ItemTemplate>
                    <asp:TextBox ID="VARIABLENAME" runat="server" Text='<%# Bind("VARIABLENAME") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:GridView ID="grdcloseQueries" runat="server" AutoGenerateColumns="False" CellPadding="4"
        CssClass="table table-bordered table-striped disp-none">
        <Columns>
            <asp:TemplateField HeaderText="VARIABLE NAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                <ItemTemplate>
                    <asp:TextBox ID="VARIABLENAME" runat="server" Text='<%# Bind("VARIABLENAME") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:GridView ID="grdAUDITTRAILDETAILS" runat="server" AutoGenerateColumns="False"
        CellPadding="4" font-family="Arial" Font-Size="X-Small" ForeColor="#333333" GridLines="None"
        Style="text-align: center" Width="228px" CssClass="table table-bordered table-striped disp-none">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="VARIABLE NAME">
                <ItemTemplate>
                    <asp:TextBox ID="VARIABLENAME" runat="server" font-family="Arial" Font-Size="X-Small"
                        Text='<%# Bind("VARIABLENAME") %>' Width="100px" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="REASON">
                <ItemTemplate>
                    <asp:TextBox ID="REASON" runat="server" font-family="Arial" Font-Size="X-Small" Text='<%# Bind("REASON") %>'
                        Width="100px" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:GridView ID="grdComments" runat="server" AutoGenerateColumns="False" CellPadding="4"
        font-family="Arial" Font-Size="X-Small" ForeColor="#333333" GridLines="None"
        Style="text-align: center" Width="228px" CssClass="table table-bordered table-striped disp-none">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="VARIABLE NAME">
                <ItemTemplate>
                    <asp:TextBox ID="VARIABLENAME" runat="server" font-family="Arial" Font-Size="X-Small"
                        Text='<%# Bind("VARIABLENAME") %>' Width="100px" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div class="box box-warning">
        <div class="box-header" style="display: inline-flex; width: 100%;">
            <h3 class="box-title" style="width: 100%;">DATA ENTRY &nbsp;||&nbsp;
                <asp:Label runat="server" ID="lblSiteId" />&nbsp;||&nbsp;<asp:Label runat="server"
                    ID="lblSubjectId" />&nbsp;||&nbsp;<asp:Label runat="server" ID="lblVisit" />
                <asp:Label runat="server" ID="lblPVID" Visible="false" />&nbsp;                
                <asp:Label ID="lblApplicableStatus" runat="server" Text="|| marked as not applicable" Font-Size="15px" ForeColor="Maroon"
                    Font-Bold="true" Font-Names="Arial"></asp:Label>
                <asp:Label ID="lblPageStatus" runat="server" ToolTip="" Visible="false"></asp:Label>
                <asp:Label ID="lblsdvcom" runat="server" CssClass="tooltip-label" ToolTip="" Visible="false"></asp:Label>
                <asp:Label ID="lblFreeze" runat="server" CssClass="tooltip-label" ToolTip="" Visible="false"></asp:Label>
                <asp:Label ID="lbldatalock" runat="server" CssClass="tooltip-label" ToolTip="" Visible="false"></asp:Label>
                <asp:Label ID="lblInvestigatorSign" runat="server" CssClass="tooltip-label" ToolTip="" Visible="false"></asp:Label>
            </h3>
            <div class="float-right" style="margin-right: 10px; padding-top: 3px;">
                <asp:Button ID="btnModuleStatus" runat="server" Text="Module Status" CssClass="btn btn-sm btn-info"
                    OnClientClick="return OpenModuleStatus();"></asp:Button>
            </div>
            <div id="POP_ModuleStatus" class="disp-none">
                <div class="box-body" runat="server">
                    <table>
                        <tr>
                            <td style="border: 1px solid black;">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-6" style="margin-top: 10px;">
                                            <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">Status Details:</label>
                                        </div>
                                        <div class="col-md-6" style="margin-top: 10px;">
                                            <asp:Label ID="lblModuleStatus" runat="server" Style="min-width: 250px;"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid black;">
                                <div class="row">
                                    <div class="col-md-12" style="width: 500px;">
                                        <div class="col-md-6" style="margin-top: 10px;">
                                            <label style="color: brown; font-weight: lighter; margin-bottom: 0px;">SDV Status :</label><br />
                                            <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">SDV By :</label><br />
                                            <label style="color: green; font-weight: lighter; margin-bottom: 0px;">Datetime(Server) :</label><br />
                                            <label style="color: red; font-weight: lighter; margin-bottom: 0px;">Datetime(User), (Timezone) :</label>
                                        </div>
                                        <div class="col-md-6" style="margin-top: 10px;">
                                            <asp:Label ID="lblSDVStatus" runat="server" Style="min-width: 250px;"></asp:Label><br />
                                            <asp:Label ID="lblSDVBy" runat="server" Style="min-width: 250px;"></asp:Label><br />
                                            <asp:Label ID="lblSDVDatetimeServer" runat="server" Style="min-width: 250px;"></asp:Label><br />
                                            <asp:Label ID="lblSDVDatetimeUser" runat="server" Style="min-width: 250px;"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </td>
                            <td style="border: 1px solid black;">
                                <div class="row">
                                    <div class="col-md-12" style="width: 500px; height: 95px;">
                                        <div class="col-md-6" style="margin-top: 10px;">
                                            <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">Signed Off By :</label><br />
                                            <label style="color: green; font-weight: lighter; margin-bottom: 0px;">Datetime(Server) :</label><br />
                                            <label style="color: red; font-weight: lighter; margin-bottom: 0px;">Datetime(User), (Timezone) :</label>
                                        </div>
                                        <div class="col-md-6" style="margin-top: 10px;">
                                            <asp:Label ID="lblInvSignOffBy" runat="server" Style="min-width: 250px;"></asp:Label><br />
                                            <asp:Label ID="lblInvSignOffDatetimeServer" runat="server" Style="min-width: 250px;"></asp:Label><br />
                                            <asp:Label ID="lblInvSignOffDatetimeUser" runat="server" Style="min-width: 250px;"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid black;">
                                <div class="row">
                                    <div class="col-md-12" style="width: 500px; height: 95px;">
                                        <div class="col-md-6" style="margin-top: 10px;">
                                            <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">Frozen By :</label><br />
                                            <label style="color: green; font-weight: lighter; margin-bottom: 0px;">Datetime(Server) :</label><br />
                                            <label style="color: red; font-weight: lighter; margin-bottom: 0px;">Datetime(User), (Timezone) :</label>
                                        </div>
                                        <div class="col-md-6" style="margin-top: 10px;">
                                            <asp:Label ID="lblFreezeBy" runat="server" Style="min-width: 250px;"></asp:Label><br />
                                            <asp:Label ID="lblFreezeDatetimeServer" runat="server" Style="min-width: 250px;"></asp:Label><br />
                                            <asp:Label ID="lblFreezeDatetimeUser" runat="server" Style="min-width: 250px;"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </td>
                            <td style="border: 1px solid black;">
                                <div class="row">
                                    <div class="col-md-12" style="width: 500px; height: 95px;">
                                        <div class="col-md-6" style="margin-top: 10px;">
                                            <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">Locked By :</label><br />
                                            <label style="color: green; font-weight: lighter; margin-bottom: 0px;">Datetime(Server) :</label><br />
                                            <label style="color: red; font-weight: lighter; margin-bottom: 0px;">Datetime(User), (Timezone) :</label>

                                        </div>
                                        <div class="col-md-6" style="margin-top: 10px;">
                                            <asp:Label ID="lblLockBy" runat="server" Style="min-width: 250px;"></asp:Label><br />
                                            <asp:Label ID="lblLockDateTiemServer" runat="server" Style="min-width: 250px;"></asp:Label><br />
                                            <asp:Label ID="lblLockDateTimeUser" runat="server" Style="min-width: 250px;"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="navbar-custom-menu navbar-right" id="divhelp" runat="server">
                <ul class="nav navbar-nav">
                    <li class="dropdown notifications-menu"><a href="#" class="dropdown-toggle" data-toggle="dropdown"
                        style="padding-top: 0px; padding-bottom: 0px; margin-top: 3px;" aria-expanded="false">
                        <asp:Label ID="Label1" runat="server" ToolTip="Help"><i class="fa fa-info-circle"
                                style="font-size: 20px;"></i></asp:Label></a>
                        <ul class="dropdown-menu" style="width: 500px;">
                            <li class="box-header">
                                <h3 class="box-title" style="width: 100%;">
                                    <label>
                                        CRF Filling Instructions</label></h3>
                            </li>
                            <li>
                                <!-- inner menu: contains the messages -->
                                <div style="width: 100%; height: 400px; overflow: auto;">
                                    <ul class="menu" style="overflow: hidden; width: 100%; height: auto;">
                                        <div class="box-body" style="text-align: left; padding-left: 10px; padding-right: 10px;">
                                            <asp:Literal ID="LiteralText" runat="server"></asp:Literal>
                                        </div>
                                    </ul>
                                </div>
                            </li>
                        </ul>
                    </li>
                </ul>
            </div>
        </div>
        <br />
        <div runat="server" id="divDDLS">
            <div style="display: inline-flex">
                <label class="label" style="color: Maroon;">
                    Select Subject Id:
                </label>
                <div class="Control">
                    <asp:DropDownList ID="drpSubID" runat="server" ForeColor="Blue" AutoPostBack="True" OnSelectedIndexChanged="drpSubID_SelectedIndexChanged"
                        CssClass="form-control required select" SelectionMode="Single" Style="width: 100%">
                    </asp:DropDownList>
                </div>
            </div>
            <div style="display: inline-flex">
                <label class="label" style="color: Maroon;">
                    Select Visit:
                </label>
                <div class="Control">
                    <asp:DropDownList ID="drpVisit" runat="server" ForeColor="Blue" AutoPostBack="True" OnSelectedIndexChanged="drpVisit_SelectedIndexChanged"
                        CssClass="form-control select">
                    </asp:DropDownList>
                </div>
            </div>
            <div style="display: inline-flex">
                <label class="label" style="color: Maroon;">
                    Select Module:
                </label>
                <div class="Control">
                    <asp:DropDownList ID="drpModule" runat="server" ForeColor="Blue" AutoPostBack="True" OnSelectedIndexChanged="drpModule_SelectedIndexChanged"
                        CssClass="form-control select" Style="width: auto;">
                    </asp:DropDownList>
                </div>
            </div>
            <div runat="server" id="divLabID" visible="false" class="form-group" style="display: inline-flex">
                <label class="label" style="color: Maroon;">
                    Select Lab:
                </label>
                <div class="Control">
                    <asp:DropDownList ID="drpLab" runat="server" ForeColor="Blue" CssClass="form-control"
                        onchange="return GET_LAB_DATA();" Style="width: 100%">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="box-body">
            <div class="form-group">
                <asp:HiddenField runat="server" ID="hfTablename" />
                <asp:HiddenField runat="server" ID="hdnPVID" />
                <asp:HiddenField runat="server" ID="hfModuleLimit" />
                <asp:HiddenField runat="server" ID="hfModuleData" />
                <asp:HiddenField runat="server" ID="hdn_PAGESTATUS" />
                <asp:HiddenField runat="server" ID="hdnIsComplete" />
                <asp:HiddenField runat="server" ID="hdnRECID" />
                <asp:HiddenField runat="server" ID="hdnVISITID" />
                <asp:HiddenField runat="server" ID="hdnError_Msg" />
                <asp:HiddenField runat="server" ID="hdnAllowable" />
                <asp:HiddenField runat="server" ID="hdnFreezeStatus" />
                <asp:HiddenField runat="server" ID="hdnLockStatus" />
                <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700; font-size: 15px;"></asp:Label>
                <table class="style1">
                    <tr>
                        <td colspan="3">
                            <div style="border-style: double; margin-bottom: 1px;" id="divNote" runat="server" visible="false">
                                &nbsp;&nbsp;
                                <span style="color: teal; font-weight: bold; text-decoration-line: underline;">Reference Note :</span>
                                <asp:Repeater runat="server" ID="repeat_AllModule">
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <ul>
                                                        <li>
                                                            <asp:Label ID="LiteralText" ForeColor="#6600ff" Font-Bold="true" runat="server" Text='<%# Bind("Note") %>'></asp:Label>
                                                        </li>
                                                    </ul>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="padding-left: 1%;">
                            <asp:Label ID="lblRemaining" runat="server" Style="color: #CC3300; font-weight: 700; font-size: 15px;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="padding-left: 1%;">
                            <asp:Label ID="lblLimit" runat="server" Style="color: #e513db; font-weight: 700; font-size: 15px;" Visible="false"></asp:Label>
                            <asp:Label ID="lblLimitReached" runat="server" Style="color: #e513db; font-weight: 700; font-size: 15px;" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblModuleName" runat="server" Text="" Font-Size="12px" Font-Bold="true"
                                Font-Names="Arial" CssClass="list-group-item"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="grd_Data" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                CssClass="table table-bordered table-striped ShowChild1" ShowHeader="false" CellSpacing="2"
                                OnRowDataBound="grd_Data_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="VARIABLENAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVARIABLENAME" Text='<%# Bind("VARIABLENAME") %>'
                                                runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CONTROLTYPE" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCONTROLTYPE" Text='<%# Bind("CONTROLTYPE") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FIELD NAME" HeaderStyle-CssClass="align-center" ItemStyle-CssClass="align-left"
                                        ItemStyle-Width="33%" HeaderStyle-Width="33%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFieldName" Font-Size="Small" ToolTip='<%# Bind("Descrip") %>'
                                                Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CLASSNAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCLASS" Text='<%# Bind("CLASS") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="align-left" ItemStyle-CssClass="align-left"
                                        ItemStyle-Width="100%" HeaderStyle-Width="100%">
                                        <ItemTemplate>
                                            <div class="col-md-12" id="divcontrol" runat="server">
                                                <asp:Label ID="lblFieldName1" Text='<%# Bind("FIELDNAME") %>' Visible="false" runat="server"></asp:Label>
                                                <asp:DropDownList ID="DRP_FIELD" runat="server" onchange="showChild(this);" Visible="false">
                                                </asp:DropDownList>
                                                <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                    OnClientClick="OpenModule_Inv_ReadOnly(this);"></asp:LinkButton>

                                                <asp:TextBox ID="TXT_FIELD" runat="server" autocomplete="off" Visible="false"
                                                    onchange="showChild(this);"></asp:TextBox>
                                                <asp:HiddenField runat="server" ID="HDN_FIELD" />
                                                <asp:HiddenField runat="server" ID="hdn_PGL_TYPE" Value='<%# Eval("PGL_TYPE") %>' />
                                                <asp:HiddenField runat="server" ID="hdn_DELETEDBY" Value='<%# Eval("DELETEDBY") %>' />

                                                <asp:HiddenField runat="server" ID="hfValue1" />
                                                <asp:Repeater runat="server" ID="repeat_CHK">
                                                    <ItemTemplate>
                                                        <div runat="server" id="divWrapper1" class="col-md-4">
                                                            <asp:CheckBox ID="CHK_FIELD" runat="server" CssClass="checkbox"
                                                                onchange="showChild(this);" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                Text='<%# Bind("TEXT") %>' />
                                                             <asp:HiddenField runat="server" ID="hdn_PGL_TYPE_CHECK" Value='<%# Eval("PGL_TYPE") %>' />
                                                            <asp:HiddenField runat="server" ID="hdn_DELETEDBY_CHECK" Value='<%# Eval("DELETEDBY") %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <asp:Repeater runat="server" ID="repeat_RAD">
                                                    <ItemTemplate>
                                                        <div runat="server" id="divWrapper" class="col-md-4">
                                                            <asp:RadioButton ID="RAD_FIELD" runat="server" onchange="showChild(this);" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                onclick="RadioCheck(this); "
                                                                CssClass="radio" Text='<%# Bind("TEXT") %>' />
                                                             <asp:HiddenField runat="server" ID="hdn_PGL_TYPE_RADIO" Value='<%# Eval("PGL_TYPE") %>' />
                                                            <asp:HiddenField runat="server" ID="hdn_DELETEDBY_RADIO" Value='<%# Eval("DELETEDBY") %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <asp:HiddenField runat="server" ID="HDN_OLD_VALUE" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="AQ" ToolTip="Open Query" OnClientClick="return ShowOpenQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:maroon"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="AWSQ" ToolTip="Answered Open Query" OnClientClick="return ShowAnsQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:blue"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="CQ" ToolTip="Closed Query" OnClientClick="return ShowClosedQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:18px;color:darkgreen"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="CM" ToolTip="Field Comments" OnClientClick="return ShowComments(this);" runat="server" class="disp-none Comments">
                                                <i class="fa fa-comment" style="font-size:17px;color:darkmagenta;"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="" ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="AD" ToolTip="Audit trail" OnClientClick="return showAuditTrail(this);" class="disp-none" runat="server">
                                                <i class="fa fa-history" style="font-size:17px"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="REQUIREDYN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="lblREQUIREDYN" Text='<%# Bind("REQUIREDYN") %>' runat="server"></asp:Label>
                                            <asp:Label ID="READYN" Text='<%# Bind("READYN") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PREFIXTEXT" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPREFIXTEXT" Text='<%# Bind("PREFIXTEXT") %>' runat="server"></asp:Label>
                                            <asp:Label ID="AutoNum" Text='<%# Bind("AutoNum") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMODULEID" Text='<%# Bind("MODULEID") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMODULENAME" Text='<%# Bind("MODULENAME") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMultiple" Text='<%# Bind("Multiple") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="STRATA" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSTRATA" Text='<%# Bind("STRATA") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <tr>
                                                <td colspan="100%" style="padding: 2px; padding-left: 4%;">
                                                    <div style="float: right; font-size: 13px;">
                                                    </div>
                                                    <div>
                                                        <div id="_CHILD" style="display: block; position: relative;">
                                                            <asp:GridView ID="grd_Data1" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                                                CssClass="table table-bordered table-striped table-striped1 ShowChild2" ShowHeader="false"
                                                                CellSpacing="2" OnRowDataBound="grd_Data1_RowDataBound">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="VARIABLENAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblVARIABLENAME" Text='<%# Bind("VARIABLENAME") %>'
                                                                                runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="CONTROLTYPE" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCONTROLTYPE" Text='<%# Bind("CONTROLTYPE") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="FIELD NAME" HeaderStyle-CssClass="align-center" ItemStyle-CssClass="align-left"
                                                                        ItemStyle-Width="33%" HeaderStyle-Width="33%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFieldName" Font-Size="Small" ToolTip='<%# Bind("Descrip") %>'
                                                                                Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="CLASSNAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCLASS" Text='<%# Bind("CLASS") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="align-left" ItemStyle-CssClass="align-left"
                                                                        ItemStyle-Width="100%" HeaderStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <div class="col-md-12" id="divcontrol" runat="server">
                                                                                <asp:Label ID="lblFieldName1" Text='<%# Bind("FIELDNAME") %>' Visible="false" runat="server"></asp:Label>
                                                                                <asp:DropDownList ID="DRP_FIELD" runat="server" onchange="showChild(this);" Visible="false">
                                                                                </asp:DropDownList>
                                                                                <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                                                    OnClientClick="OpenModule_Inv_ReadOnly(this);"></asp:LinkButton>
                                                                                <asp:TextBox ID="TXT_FIELD" runat="server" autocomplete="off" Visible="false"
                                                                                    onchange="showChild(this);"></asp:TextBox>
                                                                                <asp:HiddenField runat="server" ID="HDN_FIELD" />
                                                                                <asp:HiddenField runat="server" ID="hdn_PGL_TYPE" Value='<%# Eval("PGL_TYPE") %>' />
                                                                                <asp:HiddenField runat="server" ID="hdn_DELETEDBY" Value='<%# Eval("DELETEDBY") %>' />

                                                                                <asp:HiddenField runat="server" ID="hfValue1" />
                                                                                <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                    <ItemTemplate>
                                                                                        <div  runat="server" id="divWrapper1" class="col-md-4">
                                                                                            <asp:CheckBox ID="CHK_FIELD" runat="server" CssClass="checkbox"
                                                                                                onchange="showChild(this);" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                Text='<%# Bind("TEXT") %>' />
                                                                                            <asp:HiddenField runat="server" ID="hdn_PGL_TYPE_CHECK" Value='<%# Eval("PGL_TYPE") %>' />
                                                                                            <asp:HiddenField runat="server" ID="hdn_DELETEDBY_CHECK" Value='<%# Eval("DELETEDBY") %>' />

                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <asp:Repeater runat="server" ID="repeat_RAD">
                                                                                    <ItemTemplate>
                                                                                        <div runat="server" id="divWrapper" class="col-md-4">
                                                                                            <asp:RadioButton ID="RAD_FIELD" runat="server" onchange="showChild(this);"
                                                                                                ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                onclick="RadioCheck(this); "
                                                                                                CssClass="radio" Text='<%# Bind("TEXT") %>' />
                                                                                             <asp:HiddenField runat="server" ID="hdn_PGL_TYPE_RADIO" Value='<%# Eval("PGL_TYPE") %>' />
                                                                                            <asp:HiddenField runat="server" ID="hdn_DELETEDBY_RADIO" Value='<%# Eval("DELETEDBY") %>' />
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <asp:HiddenField runat="server" ID="HDN_OLD_VALUE" />
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="AQ" ToolTip="Open Query" OnClientClick="return ShowOpenQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:maroon"></i>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="AWSQ" ToolTip="Answered Open Query" OnClientClick="return ShowAnsQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:blue"></i>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="CQ" ToolTip="Closed Query" OnClientClick="return ShowClosedQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:18px;color:darkgreen"></i>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="CM" ToolTip="Field Comments" OnClientClick="return ShowComments(this);" runat="server" class="disp-none Comments">
                                                <i class="fa fa-comment" style="font-size:17px;color:darkmagenta;"></i>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="" ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="AD" ToolTip="Audit trail" OnClientClick="return showAuditTrail(this);" class="disp-none" runat="server">
                                                <i class="fa fa-history" style="font-size:17px"></i>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="REQUIREDYN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblREQUIREDYN" Text='<%# Bind("REQUIREDYN") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="READYN" Text='<%# Bind("READYN") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblVal_Child" Text='<%# Bind("VAL_Child") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMODULEID" Text='<%# Bind("MODULEID") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMODULENAME" Text='<%# Bind("MODULENAME") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMultiple" Text='<%# Bind("Multiple") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PREFIXTEXT" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPREFIXTEXT" Text='<%# Bind("PREFIXTEXT") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="AutoNum" Text='<%# Bind("AutoNum") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="STRATA" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSTRATA" Text='<%# Bind("STRATA") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td colspan="100%" style="padding: 2px; padding-left: 4%;">
                                                                                    <div style="float: right; font-size: 13px;">
                                                                                    </div>
                                                                                    <div>
                                                                                        <div id="_CHILD" style="display: block; position: relative;">
                                                                                            <asp:GridView ID="grd_Data2" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                                                                                CssClass="table table-bordered table-striped table-striped2 ShowChild3" ShowHeader="false"
                                                                                                CellSpacing="2" OnRowDataBound="grd_Data2_RowDataBound">
                                                                                                <Columns>
                                                                                                    <asp:TemplateField HeaderText="VARIABLENAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblVARIABLENAME" Text='<%# Bind("VARIABLENAME") %>'
                                                                                                                runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="CONTROLTYPE" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblCONTROLTYPE" Text='<%# Bind("CONTROLTYPE") %>' runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="FIELD NAME" HeaderStyle-CssClass="align-center" ItemStyle-CssClass="align-left"
                                                                                                        ItemStyle-Width="33%" HeaderStyle-Width="33%">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblFieldName" Font-Size="Small" ToolTip='<%# Bind("Descrip") %>'
                                                                                                                Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT" runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="CLASSNAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblCLASS" Text='<%# Bind("CLASS") %>' runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="align-left" ItemStyle-CssClass="align-left"
                                                                                                        ItemStyle-Width="100%" HeaderStyle-Width="100%">
                                                                                                        <ItemTemplate>
                                                                                                            <div class="col-md-12" id="divcontrol" runat="server">
                                                                                                                <asp:Label ID="lblFieldName1" Text='<%# Bind("FIELDNAME") %>' Visible="false" runat="server"></asp:Label>
                                                                                                                <asp:DropDownList ID="DRP_FIELD" runat="server"
                                                                                                                    onchange="showChild(this);" Visible="false">
                                                                                                                </asp:DropDownList>

                                                                                                                <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                                                                                    OnClientClick="OpenModule_Inv_ReadOnly(this);"></asp:LinkButton>
                                                                                                                <asp:TextBox ID="TXT_FIELD" runat="server" autocomplete="off" Visible="false"
                                                                                                                    onchange="showChild(this);"></asp:TextBox>
                                                                                                                <asp:HiddenField runat="server" ID="HDN_FIELD" />
                                                                                                                 <asp:HiddenField runat="server" ID="hdn_PGL_TYPE" Value='<%# Eval("PGL_TYPE") %>' />
                                                                                                                <asp:HiddenField runat="server" ID="hdn_DELETEDBY" Value='<%# Eval("DELETEDBY") %>' />

                                                                                                                <asp:HiddenField runat="server" ID="hfValue1" />
                                                                                                                <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                                                    <ItemTemplate>
                                                                                                                        <div  runat="server" id="divWrapper1" class="col-md-4">
                                                                                                                            <asp:CheckBox ID="CHK_FIELD" runat="server" CssClass="checkbox"
                                                                                                                                onchange="showChild(this);"
                                                                                                                                ForeColor='<%#System.Drawing.Color.FromName(Eval ("color").ToString()) %>'
                                                                                                                                Text='<%# Bind("TEXT") %>' />
                                                                                                                               <asp:HiddenField runat="server" ID="hdn_PGL_TYPE_CHECK" Value='<%# Eval("PGL_TYPE") %>' />
                                                                                                                            <asp:HiddenField runat="server" ID="hdn_DELETEDBY_CHECK" Value='<%# Eval("DELETEDBY") %>' />
                                                                                                                        </div>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:Repeater>
                                                                                                                <asp:Repeater runat="server" ID="repeat_RAD">
                                                                                                                    <ItemTemplate>
                                                                                                                        <div  runat="server" id="divWrapper" class="col-md-4">
                                                                                                                            <asp:RadioButton ID="RAD_FIELD" runat="server" onchange="showChild(this);"
                                                                                                                                ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                                                onclick="RadioCheck(this); "
                                                                                                                                CssClass="radio" Text='<%# Bind("TEXT") %>' />
                                                                                                                             <asp:HiddenField runat="server" ID="hdn_PGL_TYPE_RADIO" Value='<%# Eval("PGL_TYPE") %>' />
                                                                                                                            <asp:HiddenField runat="server" ID="hdn_DELETEDBY_RADIO" Value='<%# Eval("DELETEDBY") %>' />
                                                                                                                        </div>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:Repeater>
                                                                                                                <asp:HiddenField runat="server" ID="HDN_OLD_VALUE" />
                                                                                                            </div>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="AQ" ToolTip="Open Query" OnClientClick="return ShowOpenQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:maroon"></i>
                                                                                                            </asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="AWSQ" ToolTip="Answered Open Query" OnClientClick="return ShowAnsQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:blue"></i>
                                                                                                            </asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="CQ" ToolTip="Closed Query" OnClientClick="return ShowClosedQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:18px;color:darkgreen"></i>
                                                                                                            </asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="CM" ToolTip="Field Comments" OnClientClick="return ShowComments(this);" runat="server" class="disp-none Comments">
                                                <i class="fa fa-comment" style="font-size:17px;color:darkmagenta;"></i>
                                                                                                            </asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="" ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="AD" ToolTip="Audit trail" OnClientClick="return showAuditTrail(this);" class="disp-none" runat="server">
                                                <i class="fa fa-history" style="font-size:17px"></i>
                                                                                                            </asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="REQUIREDYN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblREQUIREDYN" Text='<%# Bind("REQUIREDYN") %>' runat="server"></asp:Label>
                                                                                                            <asp:Label ID="READYN" Text='<%# Bind("READYN") %>' runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblVal_Child" Text='<%# Bind("VAL_Child") %>' runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblMODULEID" Text='<%# Bind("MODULEID") %>' runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblMODULENAME" Text='<%# Bind("MODULENAME") %>' runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblMultiple" Text='<%# Bind("Multiple") %>' runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="PREFIXTEXT" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblPREFIXTEXT" Text='<%# Bind("PREFIXTEXT") %>' runat="server"></asp:Label>
                                                                                                            <asp:Label ID="AutoNum" Text='<%# Bind("AutoNum") %>' runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="STRATA" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblSTRATA" Text='<%# Bind("STRATA") %>' runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <tr>
                                                                                                                <td colspan="100%" style="padding: 2px; padding-left: 4%;">
                                                                                                                    <div style="float: right; font-size: 13px;">
                                                                                                                    </div>
                                                                                                                    <div>
                                                                                                                        <div id="_CHILD" style="display: block; position: relative;">
                                                                                                                            <asp:GridView ID="grd_Data3" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                                                                                                                CssClass="table table-bordered table-striped table-striped3 ShowChild4" ShowHeader="false"
                                                                                                                                CellSpacing="2" OnRowDataBound="grd_Data3_RowDataBound">
                                                                                                                                <Columns>
                                                                                                                                    <asp:TemplateField HeaderText="VARIABLENAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblVARIABLENAME" Text='<%# Bind("VARIABLENAME") %>'
                                                                                                                                                runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="CONTROLTYPE" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblCONTROLTYPE" Text='<%# Bind("CONTROLTYPE") %>' runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="FIELD NAME" HeaderStyle-CssClass="align-center" ItemStyle-CssClass="align-left"
                                                                                                                                        ItemStyle-Width="33%" HeaderStyle-Width="33%">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblFieldName" Font-Size="Small" ToolTip='<%# Bind("Descrip") %>'
                                                                                                                                                Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT" runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="CLASSNAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblCLASS" Text='<%# Bind("CLASS") %>' runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="align-left" ItemStyle-CssClass="align-left"
                                                                                                                                        ItemStyle-Width="100%" HeaderStyle-Width="100%">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <div class="col-md-12" id="divcontrol" runat="server">
                                                                                                                                                <asp:Label ID="lblFieldName1" Text='<%# Bind("FIELDNAME") %>' Visible="false" runat="server"></asp:Label>
                                                                                                                                                <asp:DropDownList ID="DRP_FIELD" runat="server" onchange="showChild(this);" Visible="false">
                                                                                                                                                </asp:DropDownList>

                                                                                                                                                <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                                                                                                                    OnClientClick="OpenModule_Inv_ReadOnly(this);"></asp:LinkButton>
                                                                                                                                                <asp:TextBox ID="TXT_FIELD" runat="server" autocomplete="off" Visible="false"
                                                                                                                                                    onchange="showChild(this);">
                                                                                                                                                </asp:TextBox>
                                                                                                                                                <asp:HiddenField runat="server" ID="HDN_FIELD" />
                                                                                                                                                     <asp:HiddenField runat="server" ID="hdn_PGL_TYPE" Value='<%# Eval("PGL_TYPE") %>' />
                                                                                                                                                <asp:HiddenField runat="server" ID="hdn_DELETEDBY" Value='<%# Eval("DELETEDBY") %>' />

                                                                                                                                                <asp:HiddenField runat="server" ID="hfValue1" />
                                                                                                                                                <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <div  runat="server" id="divWrapper1" class="col-md-4">
                                                                                                                                                            <asp:CheckBox ID="CHK_FIELD" runat="server"
                                                                                                                                                                CssClass="checkbox"
                                                                                                                                                                onchange="showChild(this);"
                                                                                                                                                                ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                                                                                Text='<%# Bind("TEXT") %>' />
                                                                                                                                                            <asp:HiddenField runat="server" ID="hdn_PGL_TYPE_CHECK" Value='<%# Eval("PGL_TYPE") %>' />
                                                                                                                                                            <asp:HiddenField runat="server" ID="hdn_DELETEDBY_CHECK" Value='<%# Eval("DELETEDBY") %>' />
                                                                                                                                                        </div>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:Repeater>
                                                                                                                                                <asp:Repeater runat="server" ID="repeat_RAD">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <div runat="server" id="divWrapper" class="col-md-4">
                                                                                                                                                            <asp:RadioButton ID="RAD_FIELD" runat="server" onchange="showChild(this);" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                                                                                onclick="RadioCheck(this);"
                                                                                                                                                                CssClass="radio" Text='<%# Bind("TEXT") %>' />

                                                                                                                                                              <asp:HiddenField runat="server" ID="hdn_PGL_TYPE_RADIO" Value='<%# Eval("PGL_TYPE") %>' />
                                                                                                                                                            <asp:HiddenField runat="server" ID="hdn_DELETEDBY_RADIO" Value='<%# Eval("DELETEDBY") %>' />
                                                                                                                                                        </div>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:Repeater>
                                                                                                                                                <asp:HiddenField runat="server" ID="HDN_OLD_VALUE" />
                                                                                                                                            </div>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:LinkButton ID="AQ" ToolTip="Open Query" OnClientClick="return ShowOpenQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:maroon"></i>
                                                                                                                                            </asp:LinkButton>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:LinkButton ID="AWSQ" ToolTip="Answered Open Query" OnClientClick="return ShowAnsQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:blue"></i>
                                                                                                                                            </asp:LinkButton>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:LinkButton ID="CQ" ToolTip="Closed Query" OnClientClick="return ShowClosedQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:18px;color:darkgreen"></i>
                                                                                                                                            </asp:LinkButton>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:LinkButton ID="CM" ToolTip="Field Comments" OnClientClick="return ShowComments(this);" runat="server" class="disp-none Comments">
                                                <i class="fa fa-comment" style="font-size:17px;color:darkmagenta;"></i>
                                                                                                                                            </asp:LinkButton>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="" ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:LinkButton ID="AD" ToolTip="Audit trail" OnClientClick="return showAuditTrail(this);" class="disp-none" runat="server">
                                                <i class="fa fa-history" style="font-size:17px"></i>
                                                                                                                                            </asp:LinkButton>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="REQUIREDYN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblREQUIREDYN" Text='<%# Bind("REQUIREDYN") %>' runat="server"></asp:Label>
                                                                                                                                            <asp:Label ID="READYN" Text='<%# Bind("READYN") %>' runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblVal_Child" Text='<%# Bind("VAL_Child") %>' runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblMODULEID" Text='<%# Bind("MODULEID") %>' runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblMODULENAME" Text='<%# Bind("MODULENAME") %>' runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblMultiple" Text='<%# Bind("Multiple") %>' runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="PREFIXTEXT" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblPREFIXTEXT" Text='<%# Bind("PREFIXTEXT") %>' runat="server"></asp:Label>
                                                                                                                                            <asp:Label ID="AutoNum" Text='<%# Bind("AutoNum") %>' runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="STRATA" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblSTRATA" Text='<%# Bind("STRATA") %>' runat="server"></asp:Label>
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
                                                                                </td>
                                                                            </tr>
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
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">&nbsp;&nbsp;<asp:Button ID="btnBacktoHomePage" runat="server" Text="Back to Main Page" CssClass="btn btn-sm btn-deepskyblue"
                            OnClick="btnBacktoHomePage_Click"></asp:Button>
                        </td>
                        <td style="text-align: RIGHT">
                            <asp:Button ID="btnCancle" Text="Cancel" runat="server" Width="105px" class="btn btn-danger btn-sm" OnClick="btnCancle_Click" />
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box box-cyan" id="divSignOff" runat="server">
        <div class="box-header" style="display: inline-flex; width: 100%;">
            <h3 class="box-title" style="width: 100%;">
                <a href="JavaScript:divexpandcollapse('grdid');" id="_Folder"><i id="imggrdid" class="ion-plus-circled" style="font-size: larger; color: #666666"></i></a>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblHeader" runat="server" ForeColor="Blue" Text="Event Logs"></asp:Label>
            </h3>
        </div>
        <div class="box-body" id="grdid" style="display: none">
            <div class="box">
                <div class="fixTableHead">
                    <asp:GridView ID="gridsigninfo" HeaderStyle-CssClass="text_center" HeaderStyle-ForeColor="Maroon" runat="server" OnPreRender="gridsigninfo_PreRender" CssClass="table table-bordered Datatable table-striped" AutoGenerateColumns="true">
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <div class="box box-warning" id="divExistingRecord" runat="server">
        <div class="box-header">
            <div id="Div1" class="nav-tabs-custom" runat="server">
                <ul class="nav nav-tabs">
                    <li id="li1" runat="server" class="active"><a href="#tab-1" data-toggle="tab">Records
                        <asp:Label runat="server" ID="lblTotalRecords" Visible="false" CssClass="label" Style="color: Maroon; font-weight: bold;"></asp:Label></a></li>
                    <li id="li2" runat="server"><a href="#tab-2" data-toggle="tab">Deleted Records
                <asp:Label runat="server" ID="lblTotalDeletedRecords" Visible="false" CssClass="label" Style="color: Maroon; font-weight: bold;"></asp:Label></a></li>
                </ul>
                <div class="tab">
                    <div id="tab-1" class="tab-content current">
                        <asp:Panel ID="Panel1" runat="server">
                            <div class="fixTableHead">
                                <asp:GridView ID="grd_Records" runat="server" CellPadding="3" Name="DSAE" AutoGenerateColumns="True" OnPreRender="gridsigninfo_PreRender"
                                    CssClass="table table-bordered table-striped Datatable" ShowHeader="True" CellSpacing="2" Width="100%"
                                    OnRowDataBound="grd_Records_RowDataBound" EmptyDataText="No records found">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="align-center" ItemStyle-CssClass="align-center"
                                            HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <div style="display: inline-flex;">
                                                    <asp:LinkButton ID="lnkPAGENUM" runat="server" ToolTip="View" CommandArgument="RECID"
                                                        CommandName="Select" OnClick="lnkPAGENUM_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Query" HeaderStyle-CssClass="width100px align-center"
                                            ItemStyle-CssClass="width100px align-center">
                                            <ItemTemplate>
                                                <div style="display: inline-flex;">
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
                                                <asp:LinkButton ID="AD" ToolTip="Audit trail" OnClientClick="return showAuditTrail_All_LIST_ENTRY(this);" class="disp-none" runat="server">
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
                                                <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Frozen By]</label><br />
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
                                        <asp:TemplateField HeaderText="Record No." HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRECID" Font-Size="Small" Text='<%# Bind("RECID") %>' CssClass="disp-none" runat="server"></asp:Label>
                                                <asp:Label ID="Label5" Font-Size="Small" Text='<%# Bind("RECORDNO") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AutoCode" HeaderStyle-CssClass="width100px align-center disp-none"
                                            ItemStyle-CssClass="width100px align-center disp-none">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAUTOCODE" Font-Size="Small" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="width100px align-center disp-none"
                                            ItemStyle-CssClass="width100px align-center disp-none">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDELETE" Font-Size="Small" Text='<%# Bind("DELETE") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IsComplete" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIsComplete" Font-Size="Small" Text='<%# Bind("IsComplete") %>'
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </div>
                    <div id="tab-2" class="tab-content current">
                        <div class="form-group" style="display: inline-flex" runat="server" visible="false" id="lblDeletedRecords">
                            <a href="#MainContent_Panel1"></a>
                        </div>
                        <asp:Panel ID="Panel2" runat="server">
                            <div class="fixTableHead">
                                <asp:GridView ID="grdDeletedRcords" runat="server" CellPadding="3" Name="DSAE" AutoGenerateColumns="True" Width="100%"
                                    CssClass="table table-bordered table-striped Datatable" OnPreRender="gridsigninfo_PreRender" ShowHeader="True" CellSpacing="2"
                                    EmptyDataText="No records found" OnRowDataBound="grdDeletedRcords_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="align-center" ItemStyle-CssClass="align-center"
                                            HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <div style="display: inline-flex;">
                                                    <asp:LinkButton ID="lnkPAGENUMDeleted" runat="server" ToolTip="View" CommandArgument="RECID"
                                                        CommandName="Select" OnClick="lnkPAGENUMDeleted_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Query" HeaderStyle-CssClass="width100px align-center"
                                            ItemStyle-CssClass="width100px align-center">
                                            <ItemTemplate>
                                                <div style="display: inline-flex;">
                                                    <asp:LinkButton ID="lnkQUERYSTATUS" ToolTip="Open Query" OnClientClick="return ShowOpenQuery_PVID_RECID_DELETED(this);" runat="server">
                                                                     <i class="fa fa-question-circle" style="font-size:17px;color:maroon;"></i></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                                                   <asp:LinkButton ID="lnkQUERYANS" ToolTip="Answered Open Query" OnClientClick="return ShowAnsQuery_PVID_RECID_DELETED(this);" runat="server">
                                                                     <i class="fa fa-question-circle" style="font-size:17px;color:blue;"></i></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lnkQUERYCLOSE" ToolTip="Closed Query" OnClientClick="return ShowClosedQuery_PVID_RECID_DELETED(this);" runat="server">
                                                                     <i class="fa fa-question-circle" style="font-size:17px;color:darkgreen;"></i></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Audit Trail" HeaderStyle-CssClass="width100px align-center"
                                            ItemStyle-CssClass="width100px align-center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="AD" ToolTip="Audit trail" OnClientClick="return showAuditTrail_All_LIST_ENTRY_DELETED(this);" class="disp-none" runat="server">
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
                                                <label>Deleted</label><br />
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
                                                        <asp:Label ID="DELETE_CAL_TZDAT" runat="server" Text='<%# Bind("DELETE_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Deleted Reason" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                <asp:Label ID="DELETED_REASON" Text='<%# Bind("DELETED_REASON") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Record No." HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRECID" Font-Size="Small" Text='<%# Bind("RECID") %>' CssClass="disp-none" runat="server"></asp:Label>
                                                <asp:Label ID="Label5" Font-Size="Small" Text='<%# Bind("RECORDNO") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AutoCode" HeaderStyle-CssClass="width100px align-center disp-none"
                                            ItemStyle-CssClass="width100px align-center disp-none">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAUTOCODE" Font-Size="Small" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="width100px align-center disp-none"
                                            ItemStyle-CssClass="width100px align-center disp-none">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDELETE" Font-Size="Small" Text='<%# Bind("DELETE") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IsComplete" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIsComplete" Font-Size="Small" Text='<%# Bind("IsComplete") %>'
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
