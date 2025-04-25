<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="DM_DataEntry.aspx.cs" Inherits="CTMS.DM_DataEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="js/Input%20Mask/jquery.inputmask.bundle.js"></script>
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <script src="js/CKEditor/ckeditor.js"></script>
    <link href="CommonStyles/ButtonColor.css" rel="stylesheet" />
    <link href="CommonStyles/DataEntry_Table.css" rel="stylesheet" />
    <link href="CommonStyles/ModalPopup.css" rel="stylesheet" />
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <script src="CommonFunctionsJs/DM/DataChanged_Disabled_Buttons.js"></script>
    <style type="text/css">
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #0000ff;
        }

        .select2-container--default .select2-selection--single {
            margin-top: 2px;
        }

        .disp-none1 {
            opacity: 0;
            width: 1px;
            height: 1px;
            position: absolute;
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

    <script src="CommonFunctionsJs/CKEDITOR_Limited.js"></script>
    <script src="CommonFunctionsJs/DisableButton.js"></script>
    <script src="CommonFunctionsJs/DivExpandCollapse.js"></script>
    <script src="CommonFunctionsJs/TextBox_Options.js"></script>
    <script src="CommonFunctionsJs/Button_Mandatory.js"></script>
    <script src="CommonFunctionsJs/btnSave_Required.js"></script>
    <script src="CommonFunctionsJs/ConfirmMsg.js"></script>
    <script src="CommonFunctionsJs/ControlJS.js"></script>
    <script src="CommonFunctionsJs/OpenPopUp.js"></script>
    <script src="CommonFunctionsJs/DM/DataChange.js"></script>
    <script src="CommonFunctionsJs/DM/Grid_AuditTrail.js"></script>
    <script src="CommonFunctionsJs/DM/Grid_Comments.js"></script>
    <script src="CommonFunctionsJs/DM/Grid_Queries.js"></script>
    <%--<script src="CommonFunctionsJs/DM/OnChangeCrit.js"></script>--%>
    <script src="CommonFunctionsJs/DM/OnChangeCritEditable.js"></script>
    <script src="CommonFunctionsJs/DM/OpenModule.js"></script>
    <script src="CommonFunctionsJs/DM/Query_HidwShow.js"></script>
    <script src="CommonFunctionsJs/DM/Query_HighlightControl.js"></script>
    <script src="CommonFunctionsJs/DM/PARENT_CHILD_LINKED.js"></script>
    <script src="CommonFunctionsJs/DM/QueryOveRide.js"></script>
    <script src="CommonFunctionsJs/DM/ShowChild.js"></script>
    <script src="CommonFunctionsJs/DM/CallChange.js"></script>
    <script language="javascript">
        $(document).ready(function () {

            if ($('#MainContent_hdnRECID').val() != '-1' && $('#MainContent_hdnRECID').val() != '') {

                $(".Comments").removeClass("disp-none");
                $("#MainContent_lblApplicableStatus").addClass("disp-none");
                $("#MainContent_btnNotApplicable").addClass("disp-none");
                $("#MainContent_btnDeleteData").removeClass("disp-none");
                $("#MainContent_btnDeleteData").addClass("btn btn-danger btn-sm");
            }
            else {
                $("#MainContent_lblApplicableStatus").addClass("disp-none");
                $("#MainContent_btnDeleteData").addClass("disp-none");
            }

            if ($('#MainContent_hdnIsComplete').val() == '2') {

                $("#MainContent_btnNotApplicable").removeClass("disp-none");
                $("#MainContent_grd_Data").addClass("disp-none");
                $("#MainContent_btnDeleteData").addClass("disp-none");
                $("#MainContent_bntSaveComplete").addClass("disp-none");
                $("#MainContent_btnSaveIncomplete").addClass("disp-none");
                $("#MainContent_lblApplicableStatus").removeClass("disp-none");
                $("#MainContent_btnCancle").addClass("disp-none");
            }

            if ($('#MainContent_hdnFreezeStatus').val() == '1' || $('#MainContent_hdnLockStatus').val() == '1') {

                $("#MainContent_btnNotApplicable").addClass("disp-none");
                $("#MainContent_btnDeleteData").addClass("disp-none");
                $("#MainContent_btnCancle").addClass("disp-none");
                $("#MainContent_lblApplicableStatus").addClass("disp-none");
                $(".Disabled_Text").addClass("disp-none");
            }

            if ('<%=Session["LOCK_STATUS"] %>' != "" && $('#MainContent_hdnRECID').val() == '-1') {

                $("#MainContent_lblApplicableStatus").removeClass("disp-none");
                $("#MainContent_lblApplicableStatus").val("This module is locked.");

            }

            //Get Query String Value
            const params = new Proxy(new URLSearchParams(window.location.search), {
                get: (searchParams, prop) => searchParams.get(prop),
            });

            let DELETED = params.DELETED;

            if (DELETED == "1") {
                $("#MainContent_btnDeleteData").addClass("disp-none");
                $(".Disabled_Text").addClass("disp-none");
            }

            FillAuditDetails();
            FillCommentsDetails();
            FillAnsQUERIES();
            FillOPENQUERIES();
            FillCLOSEQUERIES();
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--<div runat="server" id="divHideShow" class="disp-none">
        <input id="hideshow" type="button" class="btn btn-primary btn-sm" value="SHOW QUERY"
            style="font-size: 9px;" />
    </div>--%>
    <asp:GridView ID="grdOnPageSpecs" runat="server" AutoGenerateColumns="False" CellPadding="4"
        font-family="Arial" Font-Size="X-Small" ForeColor="#333333" GridLines="None"
        Style="text-align: center" Width="228px" CssClass="table table-bordered table-striped disp-none">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="CritCode">
                <ItemTemplate>
                    <asp:TextBox ID="CritCode" runat="server" font-family="Arial" Font-Size="X-Small"
                        Text='<%# Bind("CritCode") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ERR_MSG">
                <ItemTemplate>
                    <asp:TextBox ID="ERR_MSG" runat="server" font-family="Arial" Font-Size="X-Small"
                        Text='<%# Bind("ERR_MSG") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Restricted">
                <ItemTemplate>
                    <asp:TextBox ID="Restricted" runat="server" font-family="Arial" Font-Size="X-Small"
                        Text='<%# Bind("Restricted") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ISDERIVED">
                <ItemTemplate>
                    <asp:TextBox ID="ISDERIVED" runat="server" font-family="Arial" Font-Size="X-Small"
                        Text='<%# Bind("ISDERIVED") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ISDERIVED_VALUE">
                <ItemTemplate>
                    <asp:TextBox ID="ISDERIVED_VALUE" runat="server" font-family="Arial" Font-Size="X-Small"
                        Text='<%# Bind("ISDERIVED_VALUE") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="SETFIELDID">
                <ItemTemplate>
                    <asp:TextBox ID="SETFIELDID" runat="server" font-family="Arial" Font-Size="X-Small"
                        Text='<%# Bind("SETFIELDID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="SETVALUEDATA">
                <ItemTemplate>
                    <asp:TextBox ID="SETVALUEDATA" runat="server" font-family="Arial" Font-Size="X-Small"
                        Text='<%# Bind("SETVALUEDATA") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="VARIABLENAME">
                <ItemTemplate>
                    <asp:TextBox ID="VARIABLENAME" runat="server" font-family="Arial" Font-Size="X-Small"
                        Text='<%# Bind("VARIABLENAME") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
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
        <div class="box-body">
            <div class="form-group">
                <asp:HiddenField runat="server" ID="hfTablename" />
                <asp:HiddenField runat="server" ID="hdnPVID" />
                <asp:HiddenField runat="server" ID="hdn_PAGESTATUS" />
                <asp:HiddenField runat="server" ID="hdnIsComplete" />
                <asp:HiddenField runat="server" ID="hdnRECID" />
                <asp:HiddenField runat="server" ID="hdnVISITID" />
                <asp:HiddenField runat="server" ID="hdnError_Msg" />
                <asp:HiddenField runat="server" ID="hdnAllowable" />
                <asp:HiddenField runat="server" ID="hdnFreezeStatus" />
                <asp:HiddenField runat="server" ID="hdnLockStatus" />
                <div style="display: inline-flex;">
                    <div runat="server" id="divDDLS">
                        <div style="display: inline-flex">
                            <label class="label" style="color: Maroon;">
                                Select Subject Id:
                            </label>
                            <div class="Control">
                                <asp:DropDownList ID="drpSubID" runat="server" ForeColor="Blue" AutoPostBack="True" OnSelectedIndexChanged="drpSubID_SelectedIndexChanged"
                                    CssClass="form-control select">
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
                        <div style="display: inline-flex">
                            <a href="#MainContent_Panel1">
                                <asp:Label runat="server" ID="lblTotalRecords" CssClass="label" Style="color: Maroon;"></asp:Label>
                            </a>
                        </div>
                    </div>
                </div>
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
                        <td colspan="3">
                            <asp:Label ID="lblModuleName" runat="server" Text="" Font-Size="12px" Font-Bold="true"
                                Font-Names="Arial" CssClass="list-group-item"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="grd_Data" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                ShowHeader="False" CssClass="table table-bordered table-striped ShowChild1" OnRowDataBound="grd_Data_RowDataBound">
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
                                                <asp:Label ID="lblFieldNameFrench" Style="text-align: LEFT" runat="server"></asp:Label>

                                                <asp:DropDownList ID="DRP_FIELD" CssClass="rightClick" onmousedown="myFocus();" runat="server" onchange="showChild(this); DATA_Changed(this);" Visible="false">
                                                </asp:DropDownList>

                                                <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                    OnClientClick="OpenModule(this);"></asp:LinkButton>

                                                <asp:TextBox ID="TXT_FIELD" runat="server" autocomplete="off" Visible="false" CssClass="rightClick" onmousedown="myFocus();"
                                                    onchange="showChild(this); DATA_Changed(this);"></asp:TextBox>

                                                <asp:HiddenField runat="server" ID="HDN_FIELD" />
                                                <asp:HiddenField runat="server" ID="hfValue1" />
                                                <asp:HiddenField runat="server" ID="hdn_PGL_TYPE" Value='<%# Eval("PGL_TYPE") %>' />
                                                <asp:HiddenField runat="server" ID="hdn_DELETEDBY" Value='<%# Eval("DELETEDBY") %>' />
                                                <asp:Repeater runat="server" ID="repeat_CHK">
                                                    <ItemTemplate>
                                                        <div runat="server" id="divWrapper1" class="col-md-4">
                                                            <asp:CheckBox ID="CHK_FIELD" runat="server" CssClass="checkbox rightClick" onmousedown="myFocus();"
                                                                onchange="showChild(this); DATA_Changed(this);" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
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
                                                                onclick="RadioCheck(this);  DATA_Changed(this);"
                                                                CssClass="radio rightClick" onmousedown="myFocus();" Text='<%# Bind("TEXT") %>' />
                                                            <asp:HiddenField runat="server" ID="hdn_PGL_TYPE_RADIO" Value='<%# Eval("PGL_TYPE") %>' />
                                                            <asp:HiddenField runat="server" ID="hdn_DELETEDBY_RADIO" Value='<%# Eval("DELETEDBY") %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <asp:HiddenField runat="server" ID="HDN_OLD_VALUE" />
                                                <asp:UpdatePanel runat="server" ID="upnlBtn" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                    <ContentTemplate>
                                                        <asp:Button runat="server" ID="btnDATA_Changed" CssClass="btnDATA_Changed disp-none1" OnClick="DATA_Changed"></asp:Button>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnDATA_Changed" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                        <ItemTemplate>
                                            <asp:UpdatePanel runat="server" ID="upnlBtn_refresh" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="lbtnclear" ToolTip="Erase Data" CssClass="Disabled_Text" OnClientClick="return DATA_Changed_RightClick(this);" runat="server">
                        <i class="fa fa-eraser" style="font-size:17px;"></i>
                                                    </asp:LinkButton>
                                                    <asp:Button runat="server" ID="btnRightClick_Changed" CssClass="disp-none btnRightClick_Changed" OnClick="btnRightClick"></asp:Button>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnRightClick_Changed" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="AQ" ToolTip="Open Query" OnClientClick="return ShowOpenQuery(this);" class="disp-none Disabled_Text" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:maroon"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="AWSQ" ToolTip="Answered Open Query" OnClientClick="return ShowAnsQuery(this);" class="disp-none Disabled_Text" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:blue"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="CQ" ToolTip="Closed Query" OnClientClick="return ShowClosedQuery(this);" class="disp-none Disabled_Text" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:18px;color:darkgreen"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="CM" ToolTip="Field Comments" OnClientClick="return ShowComments(this);" runat="server" class="disp-none Comments Disabled_Text">
                                                <i class="fa fa-comment" style="font-size:17px;color:darkmagenta;"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="" ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="AD" ToolTip="Audit trail" OnClientClick="return showAuditTrail(this);" class="disp-none Disabled_Text" runat="server">
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
                                                                ShowHeader="False" CssClass="table table-bordered table-striped table-striped1 ShowChild2"
                                                                OnRowDataBound="grd_Data1_RowDataBound">
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
                                                                                <asp:Label ID="lblFieldNameFrench" Style="text-align: LEFT" runat="server"></asp:Label>

                                                                                <asp:DropDownList ID="DRP_FIELD" CssClass="rightClick" onmousedown="myFocus();" runat="server"
                                                                                    onchange="showChild(this);  DATA_Changed(this);" Visible="false">
                                                                                </asp:DropDownList>

                                                                                <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                                                    OnClientClick="OpenModule(this);"></asp:LinkButton>
                                                                                <asp:TextBox ID="TXT_FIELD" runat="server" autocomplete="off" Visible="false" CssClass="rightClick" onmousedown="myFocus();"
                                                                                    onchange="showChild(this); DATA_Changed(this);"></asp:TextBox>
                                                                                <asp:HiddenField runat="server" ID="HDN_FIELD" />
                                                                                <asp:HiddenField runat="server" ID="hfValue1" />
                                                                                <asp:HiddenField runat="server" ID="hdn_PGL_TYPE" Value='<%# Eval("PGL_TYPE") %>' />
                                                                                <asp:HiddenField runat="server" ID="hdn_DELETEDBY" Value='<%# Eval("DELETEDBY") %>' />
                                                                                <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                    <ItemTemplate>
                                                                                        <div runat="server" id="divWrapper1" class="col-md-4">
                                                                                            <asp:CheckBox ID="CHK_FIELD" runat="server" CssClass="checkbox rightClick" onmousedown="myFocus();"
                                                                                                onchange="showChild(this); DATA_Changed(this);"
                                                                                                ForeColor='<%#System.Drawing.Color.FromName(Eval ("color").ToString()) %>'
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
                                                                                                onclick="RadioCheck(this);  DATA_Changed(this);"
                                                                                                CssClass="radio rightClick" onmousedown="myFocus();" Text='<%# Bind("TEXT") %>' />
                                                                                            <asp:HiddenField runat="server" ID="hdn_PGL_TYPE_RADIO" Value='<%# Eval("PGL_TYPE") %>' />
                                                                                            <asp:HiddenField runat="server" ID="hdn_DELETEDBY_RADIO" Value='<%# Eval("DELETEDBY") %>' />
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <asp:HiddenField runat="server" ID="HDN_OLD_VALUE" />
                                                                                <asp:UpdatePanel runat="server" ID="upnlBtn" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                                                    <ContentTemplate>
                                                                                        <asp:Button runat="server" ID="btnDATA_Changed1" CssClass="disp-none btnDATA_Changed" OnClick="DATA_Changed"></asp:Button>
                                                                                    </ContentTemplate>
                                                                                    <Triggers>
                                                                                        <asp:AsyncPostBackTrigger ControlID="btnDATA_Changed1" EventName="Click" />
                                                                                    </Triggers>
                                                                                </asp:UpdatePanel>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                        <ItemTemplate>
                                                                            <asp:UpdatePanel runat="server" ID="upnlBtn_refresh" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                                                <ContentTemplate>
                                                                                    <asp:LinkButton ID="lbtnclear1" ToolTip="Erase Data" OnClientClick="return DATA_Changed_RightClick(this);" CssClass="Disabled_Text" runat="server">
                                                                                           <i class="fa fa-eraser" style="font-size:17px;"></i>
                                                                                    </asp:LinkButton>
                                                                                    <asp:Button runat="server" ID="btnRightClick_Changed1" CssClass="disp-none btnRightClick_Changed" OnClick="btnRightClick"></asp:Button>
                                                                                </ContentTemplate>
                                                                                <Triggers>
                                                                                    <asp:PostBackTrigger ControlID="btnRightClick_Changed1" />
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="AQ" ToolTip="Open Query" OnClientClick="return ShowOpenQuery(this);" class="disp-none Disabled_Text" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:maroon"></i>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="AWSQ" ToolTip="Answered Open Query" OnClientClick="return ShowAnsQuery(this);" class="disp-none Disabled_Text" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:blue"></i>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="CQ" ToolTip="Closed Query" OnClientClick="return ShowClosedQuery(this);" class="disp-none Disabled_Text" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:18px;color:darkgreen"></i>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="CM" ToolTip="Field Comments" OnClientClick="return ShowComments(this);" runat="server" class="disp-none Comments Disabled_Text">
                                                <i class="fa fa-comment" style="font-size:17px;color:darkmagenta;"></i>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="" ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="AD" ToolTip="Audit trail" OnClientClick="return showAuditTrail(this);" class="disp-none Disabled_Text" runat="server">
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
                                                                                                ShowHeader="False" CssClass="table table-bordered table-striped table-striped2 ShowChild3"
                                                                                                OnRowDataBound="grd_Data2_RowDataBound">
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
                                                                                                                <asp:Label ID="lblFieldNameFrench" Style="text-align: LEFT" runat="server"></asp:Label>
                                                                                                                <asp:DropDownList ID="DRP_FIELD" CssClass="rightClick" onmousedown="myFocus();" runat="server" onchange="showChild(this); DATA_Changed(this);" Visible="false">
                                                                                                                </asp:DropDownList>
                                                                                                                <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                                                                                    OnClientClick="OpenModule(this);"></asp:LinkButton>
                                                                                                                <asp:TextBox ID="TXT_FIELD" runat="server" autocomplete="off" Visible="false" CssClass="rightClick" onmousedown="myFocus();"
                                                                                                                    onchange="showChild(this); DATA_Changed(this);"></asp:TextBox>

                                                                                                                <asp:HiddenField runat="server" ID="HDN_FIELD" />
                                                                                                                <asp:HiddenField runat="server" ID="hfValue1" />
                                                                                                                <asp:HiddenField runat="server" ID="hdn_PGL_TYPE" Value='<%# Eval("PGL_TYPE") %>' />
                                                                                                                <asp:HiddenField runat="server" ID="hdn_DELETEDBY" Value='<%# Eval("DELETEDBY") %>' />
                                                                                                                <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                                                    <ItemTemplate>
                                                                                                                        <div runat="server" id="divWrapper1" class="col-md-4">
                                                                                                                            <asp:CheckBox ID="CHK_FIELD" runat="server"
                                                                                                                                CssClass="checkbox rightClick" onmousedown="myFocus();" onchange="showChild(this); DATA_Changed(this);"
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
                                                                                                                                onclick="RadioCheck(this);DATA_Changed(this);"
                                                                                                                                CssClass="radio rightClick" onmousedown="myFocus();" Text='<%# Bind("TEXT") %>' />
                                                                                                                            <asp:HiddenField runat="server" ID="hdn_PGL_TYPE_RADIO" Value='<%# Eval("PGL_TYPE") %>' />
                                                                                                                            <asp:HiddenField runat="server" ID="hdn_DELETEDBY_RADIO" Value='<%# Eval("DELETEDBY") %>' />
                                                                                                                        </div>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:Repeater>
                                                                                                                <asp:HiddenField runat="server" ID="HDN_OLD_VALUE" />
                                                                                                                <asp:UpdatePanel runat="server" ID="upnlBtn" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                                                                                    <ContentTemplate>
                                                                                                                        <asp:Button runat="server" ID="btnDATA_Changed2" CssClass="disp-none btnDATA_Changed" OnClick="DATA_Changed"></asp:Button>
                                                                                                                    </ContentTemplate>
                                                                                                                    <Triggers>
                                                                                                                        <asp:AsyncPostBackTrigger ControlID="btnDATA_Changed2" EventName="Click" />
                                                                                                                    </Triggers>
                                                                                                                </asp:UpdatePanel>
                                                                                                            </div>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:UpdatePanel runat="server" ID="upnlBtn_refresh" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                                                                                <ContentTemplate>
                                                                                                                    <asp:LinkButton ID="lbtnclear2" ToolTip="Erase Data" OnClientClick="return DATA_Changed_RightClick(this);" CssClass="Disabled_Text" runat="server">
                                                                        <i class="fa fa-eraser" style="font-size:17px;"></i>
                                                                                                                    </asp:LinkButton>
                                                                                                                    <asp:Button runat="server" ID="btnRightClick_Changed2" CssClass="disp-none btnRightClick_Changed" OnClick="btnRightClick"></asp:Button>
                                                                                                                </ContentTemplate>
                                                                                                                <Triggers>
                                                                                                                    <asp:PostBackTrigger ControlID="btnRightClick_Changed2" />
                                                                                                                </Triggers>
                                                                                                            </asp:UpdatePanel>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="AQ" ToolTip="Open Query" OnClientClick="return ShowOpenQuery(this);" class="disp-none Disabled_Text" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:maroon"></i>
                                                                                                            </asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="AWSQ" ToolTip="Answered Open Query" OnClientClick="return ShowAnsQuery(this);" class="disp-none Disabled_Text" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:blue"></i>
                                                                                                            </asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="CQ" ToolTip="Closed Query" OnClientClick="return ShowClosedQuery(this);" class="disp-none Disabled_Text" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:18px;color:darkgreen"></i>
                                                                                                            </asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="CM" ToolTip="Field Comments" OnClientClick="return ShowComments(this);" runat="server" class="disp-none Comments Disabled_Text">
                                                <i class="fa fa-comment" style="font-size:17px;color:darkmagenta;"></i>
                                                                                                            </asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="" ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="AD" ToolTip="Audit trail" OnClientClick="return showAuditTrail(this);" class="disp-none Disabled_Text" runat="server">
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
                                                                                                                                ShowHeader="False" CssClass="table table-bordered table-striped table-striped3 ShowChild4"
                                                                                                                                OnRowDataBound="grd_Data3_RowDataBound">
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
                                                                                                                                                <asp:Label ID="lblFieldNameFrench" Style="text-align: LEFT" runat="server"></asp:Label>
                                                                                                                                                <asp:DropDownList ID="DRP_FIELD" CssClass="rightClick" onmousedown="myFocus();" runat="server" onchange="showChild(this); DATA_Changed(this);" Visible="false">
                                                                                                                                                </asp:DropDownList>
                                                                                                                                                <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                                                                                                                    OnClientClick="OpenModule(this);"></asp:LinkButton>
                                                                                                                                                <asp:TextBox ID="TXT_FIELD" runat="server" autocomplete="off" Visible="false" CssClass="rightClick" onmousedown="myFocus();"
                                                                                                                                                    onchange="showChild(this); DATA_Changed(this);"></asp:TextBox>

                                                                                                                                                <asp:HiddenField runat="server" ID="HDN_FIELD" />
                                                                                                                                                <asp:HiddenField runat="server" ID="hfValue1" />
                                                                                                                                                <asp:HiddenField runat="server" ID="hdn_PGL_TYPE" Value='<%# Eval("PGL_TYPE") %>' />
                                                                                                                                                <asp:HiddenField runat="server" ID="hdn_DELETEDBY" Value='<%# Eval("DELETEDBY") %>' />
                                                                                                                                                <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <div runat="server" id="divWrapper1" class="col-md-4">
                                                                                                                                                            <asp:CheckBox ID="CHK_FIELD" runat="server"
                                                                                                                                                                CssClass="checkbox rightClick" onmousedown="myFocus();" onchange="showChild(this); DATA_Changed(this);"
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
                                                                                                                                                            <asp:RadioButton ID="RAD_FIELD" runat="server" onchange="showChild(this);"
                                                                                                                                                                ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                                                                                onclick="RadioCheck(this);  DATA_Changed(this);"
                                                                                                                                                                CssClass="radio rightClick" onmousedown="myFocus();" Text='<%# Bind("TEXT") %>' />
                                                                                                                                                            <asp:HiddenField runat="server" ID="hdn_PGL_TYPE_RADIO" Value='<%# Eval("PGL_TYPE") %>' />
                                                                                                                                                            <asp:HiddenField runat="server" ID="hdn_DELETEDBY_RADIO" Value='<%# Eval("DELETEDBY") %>' />
                                                                                                                                                        </div>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:Repeater>
                                                                                                                                                <asp:HiddenField runat="server" ID="HDN_OLD_VALUE" />
                                                                                                                                                <asp:UpdatePanel runat="server" ID="upnlBtn" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                                                                                                                    <ContentTemplate>
                                                                                                                                                        <asp:Button runat="server" ID="btnDATA_Changed3" CssClass="disp-none btnDATA_Changed" OnClick="DATA_Changed"></asp:Button>
                                                                                                                                                    </ContentTemplate>
                                                                                                                                                    <Triggers>
                                                                                                                                                        <asp:AsyncPostBackTrigger ControlID="btnDATA_Changed3" EventName="Click" />
                                                                                                                                                    </Triggers>
                                                                                                                                                </asp:UpdatePanel>
                                                                                                                                            </div>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:UpdatePanel runat="server" ID="upnlBtn_refresh" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                                                                                                                <ContentTemplate>
                                                                                                                                                    <asp:LinkButton ID="lbtnclear3" ToolTip="Erase Data" OnClientClick="return DATA_Changed_RightClick(this);" CssClass="Disabled_Text" runat="server">
                        <i class="fa fa-eraser" style="font-size:17px;"></i>
                                                                                                                                                    </asp:LinkButton>
                                                                                                                                                    <asp:Button runat="server" ID="btnRightClick_Changed3" CssClass="disp-none btnRightClick_Changed" OnClick="btnRightClick"></asp:Button>
                                                                                                                                                </ContentTemplate>
                                                                                                                                                <Triggers>
                                                                                                                                                    <asp:PostBackTrigger ControlID="btnRightClick_Changed3" />
                                                                                                                                                </Triggers>
                                                                                                                                            </asp:UpdatePanel>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:LinkButton ID="AQ" ToolTip="Open Query" OnClientClick="return ShowOpenQuery(this);" class="disp-none Disabled_Text" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:maroon"></i>
                                                                                                                                            </asp:LinkButton>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:LinkButton ID="AWSQ" ToolTip="Answered Open Query" OnClientClick="return ShowAnsQuery(this);" class="disp-none Disabled_Text" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:blue"></i>
                                                                                                                                            </asp:LinkButton>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:LinkButton ID="CQ" ToolTip="Closed Query" OnClientClick="return ShowClosedQuery(this);" class="disp-none Disabled_Text" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:18px;color:darkgreen"></i>
                                                                                                                                            </asp:LinkButton>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:LinkButton ID="CM" ToolTip="Field Comments" OnClientClick="return ShowComments(this);" runat="server" class="disp-none Comments Disabled_Text">
                                                <i class="fa fa-comment" style="font-size:17px;color:darkmagenta;"></i>
                                                                                                                                            </asp:LinkButton>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="" ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:LinkButton ID="AD" ToolTip="Audit trail" OnClientClick="return showAuditTrail(this);" class="disp-none Disabled_Text" runat="server">
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
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </asp:GridView>
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
                            <asp:Button ID="btnSubmitOnsubmitData" runat="server" CssClass="disp-none" OnClick="btnSubmitOnsubmitData_Click"></asp:Button>
                        </td>
                        <td style="text-align: RIGHT">
                            <asp:Button ID="btnNotApplicable" runat="server" Text="Not Applicable" ForeColor="White" Width="105px" CssClass="btn btn-rebeccapurple btn-sm" OnClick="btnNotApplicable_Click" />
                            &nbsp;
                            <asp:Button ID="btnDeleteData" Text="Delete" runat="server" Width="105px" OnClick="btnDeleteData_Click" CssClass="btn btn-danger btn-sm" />
                            &nbsp;
                            <asp:Button ID="btnCancle" Text="Cancel" runat="server" Width="105px" class="btn btn-danger btn-sm" Visible="false" OnClick="btnCancle_Click" />
                            &nbsp;
                            <asp:Button ID="btnSaveIncomplete" Text="Save Incomplete" runat="server" CssClass="btn btn-DARKORANGE btn-sm cls-btnSave"
                                OnClick="btnSaveIncomplete_Click" />
                            &nbsp;
                            <asp:Button ID="bntSaveComplete" runat="server" Text="Save Complete" OnClick="bntSaveComplete_Click"
                                CssClass="btn btn-DarkGreen btn-sm cls-btnSave" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <asp:Button runat="server" ID="Button_Popup" Style="display: none" />
        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1" TargetControlID="Button_Popup"
            BackgroundCssClass="Background">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="Panel1" runat="server" Style="display: none;" CssClass="Popup1">

            <asp:UpdatePanel ID="updPnlIDDetail" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <h5 class="heading">Reason for Change</h5>
                    <div class="disp-none">
                        <asp:Label ID="txt_TableName" runat="server"></asp:Label>
                        <asp:Label ID="txt_VariableName" runat="server"></asp:Label>
                    </div>
                    <div class="modal-body" runat="server">
                        <div id="ModelPopup">
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="Label4" runat="server" Text="Module Name" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="txt_ModuleName" CssClass="form-control-model" runat="server" Style="overflow-y: auto; max-height: 100px; min-height: 21px; min-width: 250px;"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="Label8" runat="server" Text="Field Name" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="txt_FieldName" CssClass="form-control-model" runat="server" Style="overflow-y: auto; max-height: 100px; min-height: 21px; min-width: 250px;"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="Label10" runat="server" Text="Old value" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="txt_OldValue" Enabled="false" CssClass="form-control-model" runat="server" Style="overflow-y: auto; max-height: 100px; min-height: 21px; min-width: 250px;"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="Label11" runat="server" Text="New value" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="txt_NewValue" CssClass="form-control-model" runat="server" Style="overflow-y: auto; max-height: 100px; min-height: 21px; min-width: 250px;"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="Label44" runat="server" Text="Reason" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:DropDownList ID="drp_Reason" CssClass="form-control-model" runat="server" Style="min-width: 250px;">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="Data entry error">Data entry error</asp:ListItem>
                                    <asp:ListItem Value="Updated data available">Updated data available</asp:ListItem>
                                    <asp:ListItem Value="Other">Other</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="Label2" runat="server" Text="Comments" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:TextBox ID="txt_Comments" CssClass="form-control-model" TextMode="MultiLine"
                                    runat="server" Style="min-width: 250px;"></asp:TextBox>
                            </div>
                        </div>
                        <div id="DivAction" runat="server" class="row">
                            <div class="col-md-4">
                                <asp:Label ID="Label7" runat="server" Text="Reason For Manual Query" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:DropDownList ID="drpAction" CssClass="form-control-model" runat="server" Style="min-width: 250px;" onchange="drpAction_Change_DataEntry();">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="Transcription error">Transcription error</asp:ListItem>
                                    <asp:ListItem Value="Data as per source">Data as per source</asp:ListItem>
                                    <asp:ListItem Value="New data available">New data available</asp:ListItem>
                                    <asp:ListItem Value="Other">Other</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row disp-none" id="OverrideComments" runat="server">
                            <div class="col-md-4">
                                <asp:Label ID="Label9" runat="server" Text="Query Comments" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:TextBox ID="txt_OverrideComm" CssClass="form-control-model" TextMode="MultiLine"
                                    runat="server" Style="min-width: 250px;"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-4">
                                &nbsp;
                            </div>
                            <div class="col-md-8">
                                <asp:Button ID="btn_Update" runat="server" Style="height: 34px; font-size: 14px;" CssClass="btn btn-DarkGreen"
                                    OnClientClick="return Check_ReasonEntered();" Text="Update Data" OnClick="btn_Update_Click" />
                                &nbsp;
                            &nbsp;
                            <asp:Button ID="btn_Cancel" runat="server" Text="Cancel"
                                CssClass="btn btn-danger" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <asp:Label ID="Label6" Style="display: none;" runat="server" Text=""></asp:Label>
        <cc2:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="Popup3" TargetControlID="Label6"
            BackgroundCssClass="Background">
        </cc2:ModalPopupExtender>
        <asp:Panel ID="Popup3" runat="server" Style="display: none;" CssClass="Popup1">
            <div class="modal-body" runat="server">
                <div id="ModelPopup3">
                    <div class="row">
                        <asp:Label ID="Label12" runat="server" Style="color: black; font-weight: 600; font-size: 17px; margin-left: 127px;">Are you sure you want to delete this record ?</asp:Label>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtDeletedReason" placeholder="Please enter comment to delete record...."
                                    TextMode="MultiLine" MaxLength="500" CssClass="form-control required2" Style="width: 575px; height: 131px;"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-3">
                            &nbsp;
                        </div>
                        <div class="col-md-9">
                            <asp:Button ID="btnDELETESubmit" runat="server" Style="margin-left: 37px; height: 34px; width: 71px; font-size: 14px;" CssClass="btn btn-DarkGreen btn-sm cls-btnSave2"
                                Text="Yes" OnClick="btnDELETESubmit_Click" />
                            &nbsp;
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="No"
                                CssClass="btn btn-danger btn-sm" Style="height: 34px; width: 71px; font-size: 14px;" />
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
    <asp:Label ID="Open_Request" Style="display: none;" runat="server" Text="">.</asp:Label>
    <cc2:ModalPopupExtender ID="ModalPopupExtender3" runat="server" PopupControlID="PopupDataImpact" TargetControlID="Open_Request"
        BackgroundCssClass="Background">
    </cc2:ModalPopupExtender>
    <asp:Panel ID="PopupDataImpact" runat="server" CssClass="Popup1" Style="display: none; min-width: 250px; max-width: 100%; min-height: auto; max-height: 450px; overflow: auto;">
        <asp:UpdatePanel ID="updatepanl4" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <h5 class="heading">Data impact warning</h5>
                <div class="modal-body" runat="server">
                    <div id="ModelPopup5">
                        <div class="row" style="margin-left: 10px; margin-right: 10px;">
                            <asp:Label ID="lblNotes" runat="server" Style="color: #0000ff; font-weight: 500;">Following is the list of Visits and Modules impacting with respect to this change.
<br />Please delete data entered for below Visit and Modules to proceed.</asp:Label>
                        </div>
                        <br />
                        <div class="row">
                            <div style="width: 95%; min-height: auto; max-height: 170px; overflow: auto;">
                                <asp:GridView ID="grdGetModules" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    Font-Size="Small" Style="width: 90%; margin-left: 25px;" CssClass="table table-bordered table-striped">
                                    <Columns>
                                        <asp:BoundField DataField="VISITNAME" HeaderText="Visit Name" />
                                        <asp:BoundField DataField="MODULENAME" HeaderText="Module Name" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-5">
                            &nbsp;
                        </div>
                        <div class="col-md-5">
                            <asp:Button ID="btnOk" runat="server" Style="height: 34px; width: 71px; font-size: 14px;" CssClass="btn btn-danger"
                                Text="OK" OnClick="btnCancel_Click1" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
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
    <div class="box box-warning" id="DivDeletedRecords" runat="server" visible="false">
        <div class="box-header" style="display: inline-flex; width: 100%;">
            <h3 class="box-title" style="width: 100%;">
                <a href="JavaScript:divexpandcollapse('grdDeleted');" id="_Folder1"><i id="imggrdDeleted" class="ion-plus-circled" style="font-size: larger; color: #666666"></i></a>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label3" runat="server" ForeColor="Blue" Text="Deleted Records"></asp:Label>
                <asp:Label runat="server" ID="lblTotalDeletedRecords" Visible="false" CssClass="label" Style="color: Maroon; font-weight: bold;"></asp:Label>
            </h3>
        </div>
        <div class="box-body" id="grdDeleted" style="display: none">
            <div class="box">
                <div class="fixTableHead">
                    <asp:GridView ID="grdDeletedRcords" runat="server" Width="100%" CellPadding="3" Name="DSAE" AutoGenerateColumns="True"
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
            </div>
        </div>
    </div>
</asp:Content>



