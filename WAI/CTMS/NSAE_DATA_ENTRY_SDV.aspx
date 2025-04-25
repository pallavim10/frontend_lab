<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NSAE_DATA_ENTRY_SDV.aspx.cs" Inherits="CTMS.NSAE_DATA_ENTRY_SDV" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="js/Input%20Mask/jquery.inputmask.bundle.js"></script>
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <link href="js/plugins/datatables/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
    <script src="js/CKEditor/ckeditor.js"></script>
    <link href="CommonStyles/ButtonColor.css" rel="stylesheet" />
    <link href="CommonStyles/DataEntry_Table.css" rel="stylesheet" />
    <link href="CommonStyles/ModalPopup.css" rel="stylesheet" />
    <script src="CommonFunctionsJs/CKEDITOR_Limited.js"></script>
    <script src="CommonFunctionsJs/Datatable1.js"></script>
    <script src="CommonFunctionsJs/DisableButton.js"></script>
    <script src="CommonFunctionsJs/DivExpandCollapse.js"></script>
    <script src="CommonFunctionsJs/TextBox_Options.js"></script>
    <script src="CommonFunctionsJs/Button_Mandatory.js"></script>
    <script src="CommonFunctionsJs/btnSave_Required.js"></script>
    <script src="CommonFunctionsJs/ConfirmMsg.js"></script>
    <script src="CommonFunctionsJs/ControlJS.js"></script>
    <script src="CommonFunctionsJs/SAE/SAE_CallChange_ReadOnly.js"></script>
    <script src="CommonFunctionsJs/SAE/SAE_Grid_AuditTrail.js"></script>
    <script src="CommonFunctionsJs/SAE/SAE_Grid_Comments.js"></script>
    <script src="CommonFunctionsJs/SAE/SAE_Grid_Queries.js"></script>
    <script src="CommonFunctionsJs/SAE/SAE_OnChangeCrit.js"></script>
    <script src="CommonFunctionsJs/SAE/SAE_OpenModule.js"></script>
    <script src="CommonFunctionsJs/SAE/SAE_Query_HidwShow.js"></script>
    <script src="CommonFunctionsJs/SAE/SAE_Query_HighlightControl.js"></script>
    <script src="CommonFunctionsJs/SAE/SAE_QueryOveRide.js"></script>
    <script src="CommonFunctionsJs/SAE/SAE_ShowChild.js"></script>
    <script src="CommonFunctionsJs/SAE/SAE_SDV.js"></script>
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <script src="CommonFunctionsJs/SAE/DataChanged_Disabled_Buttons.js"></script>
    <script src="CommonFunctionsJs/SAE/SAE_PARENT_CHILD_LINKED.js"></script>
    <script language="javascript">
        $(document).ready(function () {

            if ($('#MainContent_hdn_PAGESTATUS').val() != '0') {

                $(".Comments").removeClass("disp-none");
                $(".ManualQuery").removeClass("disp-none");
            }

            FillAuditDetails();
            FillCommentsDetails();
            FillAnsQUERIES();
            FillOPENQUERIES();
            FillCLOSEQUERIES();
            FillSDVDetails();
            BindLinkedData();
            SetLinkedData();
        });

        $(function checkCritcalDp() {

            var chk;

            var CHKs = $(".chkCriticalDP").toArray();

            for (chk = 0; chk < CHKs.length; ++chk) {

                $('#' + $(CHKs[chk]).find('input').attr('id')).addClass('inpputchkCriticalDP');
            }

        });

        function countCheckboxes() {
            var inputElems = $("input[type=checkbox][id*=chkSDV]").toArray()

            var count = 0;

            for (var i = 0; i < inputElems.length; i++) {

                if (inputElems[i].type === "checkbox" && inputElems[i].checked === true) {
                    count++;
                }
            }

            if (count < 1) {
                alert("Select at least one data to SDV");
                event.preventDefault();
            }
        }

        function pageLoad() {

            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: false,
                fixedHeader: true
            });
        }
    </script>
    <style type="text/css">
        .Signs {
            display: block;
            padding: 0px 31px;
            font-size: 13px;
            width: 81%;
            border: 1px solid #cccccc;
            border-radius: 4px;
            margin-left: 4px;
            margin-top: -5px !important;
            margin-bottom: 14px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div runat="server" id="divHideShow" class="disp-none">
        <input id="hideshow" type="button" class="btn btn-primary btn-sm" value="SHOW QUERY"
            style="font-size: 9px;" />
    </div>
    <asp:GridView ID="SAE_grdOnPageSpecs" runat="server" AutoGenerateColumns="False" CellPadding="4"
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
    <asp:GridView ID="SAE_grdQUERYDETAILS" runat="server" AutoGenerateColumns="False" CellPadding="4"
        CssClass="table table-bordered table-striped disp-none" DataKeyNames="QUERYTYPE">
        <Columns>
            <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                <ItemTemplate>
                    <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="SAEID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                <ItemTemplate>
                    <asp:TextBox ID="SAEID" runat="server" Text='<%# Bind("SAEID") %>' Width="70px" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Query Text">
                <ItemTemplate>
                    <asp:Label ID="QUERYTEXT" ForeColor="Maroon" runat="server" Enabled="false" Text='<%# Bind("QUERYTEXT") %>'
                        Width="500" />
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
                    <asp:LinkButton ID="lnkOverride" runat="server" Text="Action" OnClientClick="return SAE_OpenOverideData_ReadOnly(this)"
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
    <asp:GridView ID="SAE_grdOpenQuers" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="4"
        CssClass="table table-bordered table-striped disp-none">
        <Columns>
            <asp:TemplateField HeaderText="VARIABLE NAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                <ItemTemplate>
                    <asp:TextBox ID="VARIABLENAME" runat="server" Text='<%# Bind("VARIABLENAME") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:GridView ID="SAE_grdAnsQueries" runat="server" Width="96%" AutoGenerateColumns="False" CellPadding="4"
        CssClass="table table-bordered table-striped disp-none">
        <Columns>
            <asp:TemplateField HeaderText="VARIABLE NAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                <ItemTemplate>
                    <asp:TextBox ID="VARIABLENAME" runat="server" Text='<%# Bind("VARIABLENAME") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:GridView ID="SAE_grdcloseQueries" runat="server" AutoGenerateColumns="False" CellPadding="4"
        CssClass="table table-bordered table-striped disp-none">
        <Columns>
            <asp:TemplateField HeaderText="VARIABLE NAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                <ItemTemplate>
                    <asp:TextBox ID="VARIABLENAME" runat="server" Text='<%# Bind("VARIABLENAME") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:GridView ID="SAE_grdAUDITTRAILDETAILS" runat="server" AutoGenerateColumns="False"
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
    <asp:GridView ID="SAE_grdComments" runat="server" AutoGenerateColumns="False" CellPadding="4"
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
    <asp:GridView ID="grSDVDETAILS" runat="server" AutoGenerateColumns="False" CellPadding="4"
        CssClass="table table-bordered table-striped disp-none">
        <Columns>
            <asp:TemplateField HeaderText="VARIABLE NAME">
                <ItemTemplate>
                    <asp:TextBox ID="VARIABLENAME" runat="server" font-family="Arial" Font-Size="X-Small"
                        Text='<%# Bind("VARIABLENAME") %>' Width="100px" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="SDVYN">
                <ItemTemplate>
                    <asp:TextBox ID="SDVYN" runat="server" font-family="Arial" Font-Size="X-Small"
                        Text='<%# Bind("SDVYN") %>' Width="100px" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div class="box box-warning">
        <div class="box-header" style="display: inline-flex; width: 100%;">
            <h3 class="box-title" style="width: 100%;">SAE SDV &nbsp;||&nbsp;
                <asp:Label runat="server" ID="lblSiteId" />&nbsp;||&nbsp;<asp:Label runat="server"
                    ID="lblSubjectId" />&nbsp;||&nbsp;<asp:Label runat="server" ID="lblSAE" />&nbsp;||&nbsp;<asp:Label
                        runat="server" ID="lblSAEID" />&nbsp;||&nbsp;<asp:Label runat="server" ID="lblStatus" />&nbsp;
            </h3>
            <div class="float-right" style="margin-right: 10px; margin-top: 3px;">
                <asp:Label ID="lblPageStatus" runat="server" Style="height: 34px; text-align: center; padding-top: 7px;" Visible="false" Text="Module Status" CssClass="btn btn-sm btn-danger">
                </asp:Label>
            </div>
            <div id="pop_PageStatus" class="disp-none">
                <div class="box-body" runat="server">
                    <table>
                        <tr>
                            <td style="border: 1px solid black;">
                                <div class="row">
                                    <div class="col-md-12" style="width: 518px; height: 95px;">
                                        <div class="col-md-6" style="margin-top: 10px;">
                                            <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">Reviewed By</label><br />
                                            <label style="color: green; font-weight: lighter; margin-bottom: 0px;">Datetime(Server)</label><br />
                                            <label style="color: red; font-weight: lighter; margin-bottom: 0px;">Datetime(User), (Timezone)</label>
                                        </div>
                                        <div class="col-md-6" style="margin-top: 10px;">
                                            <asp:Label ID="lblReviewedBy" runat="server" Style="min-width: 250px;"></asp:Label><br />
                                            <asp:Label ID="lblReviewedDatetimeServer" runat="server" Style="min-width: 250px;"></asp:Label><br />
                                            <asp:Label ID="lblReviewedDatetimeUser" runat="server" Style="min-width: 250px;"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </td>
                            <td style="border: 1px solid black;">
                                <div class="row">
                                    <div class="col-md-12" style="width: 518px;">
                                        <div class="col-md-6" style="margin-top: 10px;">
                                            <label style="color: brown; font-weight: lighter; margin-bottom: 0px;">SDV Status</label><br />
                                            <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">SDV By</label><br />
                                            <label style="color: green; font-weight: lighter; margin-bottom: 0px;">Datetime(Server)</label><br />
                                            <label style="color: red; font-weight: lighter; margin-bottom: 0px;">Datetime(User), (Timezone)</label>
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
                        </tr>
                        <tr>
                            <td style="border: 1px solid black;">
                                <div class="row">
                                    <div class="col-md-12" style="width: 518px; height: 95px;">
                                        <div class="col-md-6" style="margin-top: 10px;">
                                            <label style="color: brown; font-weight: lighter; margin-bottom: 0px;">Medical Reviewed Status</label><br />
                                            <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">Medical Reviewed By</label><br />
                                            <label style="color: green; font-weight: lighter; margin-bottom: 0px;">Datetime(Server)</label><br />
                                            <label style="color: red; font-weight: lighter; margin-bottom: 0px;">Datetime(User), (Timezone)</label>
                                        </div>
                                        <div class="col-md-6" style="margin-top: 10px;">
                                            <asp:Label ID="lblMRStatus" runat="server" Style="min-width: 250px;"></asp:Label><br />
                                            <asp:Label ID="lblMedicalReviewedBy" runat="server" Style="min-width: 250px;"></asp:Label><br />
                                            <asp:Label ID="lblMEdicalDatetimeServer" runat="server" Style="min-width: 250px;"></asp:Label><br />
                                            <asp:Label ID="lblMedicalDatetimeUser" runat="server" Style="min-width: 250px;"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </td>
                            <td style="border: 1px solid black;">
                                <div class="row">
                                    <div class="col-md-12" style="width: 518px; height: 95px;">
                                        <div class="col-md-6" style="margin-top: 10px;">
                                            <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">Sign By</label><br />
                                            <label style="color: green; font-weight: lighter; margin-bottom: 0px;">Datetime(Server)</label><br />
                                            <label style="color: red; font-weight: lighter; margin-bottom: 0px;">Datetime(User), (Timezone)</label>

                                        </div>
                                        <div class="col-md-6" style="margin-top: 10px;">
                                            <asp:Label ID="lblSignBy" runat="server" Style="min-width: 250px;"></asp:Label><br />
                                            <asp:Label ID="lblSignByDateTiemServer" runat="server" Style="min-width: 250px;"></asp:Label><br />
                                            <asp:Label ID="lblSignByDateTimeUser" runat="server" Style="min-width: 250px;"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="pull-right" style="padding-right: 5px; margin-top: 3px;">
                <asp:LinkButton ID="lbtnPageComment" ToolTip="Page Comments" OnClientClick="return SAE_Show_Page_Comments(this);" CssClass="btn btn-warning btn-sm" runat="server" class="disp-none Comments">
                    <i class="fa fa-comment fa-2x" style="color: white;"></i>
                    <asp:Label class="badge badge-info right" ID="lblComment_Count" runat="server" Text="0"
                        Style="margin-left: 5px;" Font-Bold="true"></asp:Label>
                </asp:LinkButton>&nbsp&nbsp&nbsp
            </div>
            <div class="navbar-custom-menu navbar-right" id="divhelp" runat="server">
                <ul class="nav navbar-nav">
                    <li class="dropdown notifications-menu"><a href="#" class="dropdown-toggle" data-toggle="dropdown"
                        style="padding-top: 0px; padding-bottom: 0px;" aria-expanded="false">
                        <asp:Label ID="Label1" runat="server" ToolTip="Help" CssClass="btn btn-success btn-sm"><i class="fa fa-info fa-2x"></i></asp:Label></a>
                        <ul class="dropdown-menu" style="width: 500px;">
                            <li class="box-header">
                                <h3 class="box-title" style="width: 100%;">
                                    <label>
                                        Form Filling Instructions</label></h3>
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
                <asp:HiddenField runat="server" ID="hdnStatus" />
                <asp:HiddenField runat="server" ID="hdnSAEID" />
                <asp:HiddenField runat="server" ID="hdnRECID" />
                <asp:HiddenField runat="server" ID="hdnSAE" />
                <asp:HiddenField runat="server" ID="hdn_PAGESTATUS" />
                <asp:HiddenField runat="server" ID="hdnIsComplete" />
                <asp:HiddenField runat="server" ID="hdnDM_PVID" />
                <asp:HiddenField runat="server" ID="hdnDM_RECID" />
                <asp:HiddenField runat="server" ID="hdnDM_TABLENAME" />
                <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700; font-size: 15px;"></asp:Label>
                <br />
                <div class="pull-left" style="display: inline-flex">
                    <label class="label" style="color: Maroon;">
                        Select Module:
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="drpModule" runat="server" ForeColor="Blue" AutoPostBack="True"
                            CssClass="form-control " Style="width: 100%" OnSelectedIndexChanged="drpModule_SelectedIndexChanged1">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="pull-right" id="divverifyall" runat="server">
                    <asp:Label ID="Label4" runat="server" Text="Verify All" Font-Size="15px"
                        Font-Bold="true" Font-Names="Arial"></asp:Label>
                    &nbsp&nbsp
                    <asp:CheckBox ID="Chk_Verify_All" runat="server" Style="font-size: x-small; margin-right: 6px;"
                        OnClick="checkVerifyAll(this)" />
                    &nbsp&nbsp
                </div>
                <br />
                <table class="style1">
                    <tr>
                        <td colspan="3"></td>
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
                                                <asp:DropDownList ID="DRP_FIELD" CssClass="rightClick" runat="server" onchange="showChild_ReadOnly(this); checkOnChangeCrit(this);" Visible="false">
                                                </asp:DropDownList>
                                                <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                    OnClientClick="OpenModule(this);"></asp:LinkButton>
                                                <asp:TextBox ID="TXT_FIELD" runat="server" autocomplete="off" Visible="false" CssClass="manualInput rightClick"
                                                    onchange="showChild_ReadOnly(this); checkOnChangeCrit(this);"></asp:TextBox>
                                                <asp:HiddenField runat="server" ID="HDN_FIELD" />
                                                <asp:HiddenField runat="server" ID="hfValue1" />
                                                <asp:Repeater runat="server" ID="repeat_CHK">
                                                    <ItemTemplate>
                                                        <div class="col-md-4">
                                                            <asp:CheckBox ID="CHK_FIELD" runat="server" CssClass="checkbox rightClick"
                                                                onchange="showChild_ReadOnly(this); checkOnChangeCrit(this);" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                Text='<%# Bind("TEXT") %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <asp:Repeater runat="server" ID="repeat_RAD">
                                                    <ItemTemplate>
                                                        <div class="col-md-4">
                                                            <asp:RadioButton ID="RAD_FIELD" runat="server" onchange="showChild_ReadOnly(this);" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                onclick="RadioCheck(this); checkOnChangeCrit(this);"
                                                                CssClass="radio rightClick" Text='<%# Bind("TEXT") %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <asp:HiddenField runat="server" ID="HDN_OLD_VALUE" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="MQ" ToolTip="Raise Manual Query" OnClientClick="return OpenManualQuery(this);" class="disp-none ManualQuery" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:deepskyblue;"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="AQ" ToolTip="Open Query" OnClientClick="return SAE_ShowOpenQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:maroon"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="AWSQ" ToolTip="Answered Open Query" OnClientClick="return SAE_ShowAnsQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:blue"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="CQ" ToolTip="Closed Query" OnClientClick="return SAE_ShowClosedQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:18px;color:darkgreen"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="CM" ToolTip="Field Comments" OnClientClick="return SAE_ShowComments(this);" runat="server" class="disp-none Comments">
                                                <i class="fa fa-comment" style="font-size:17px;color:darkmagenta;"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="" ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="AD" ToolTip="Audit trail" OnClientClick="return SAE_showAuditTrail(this);" class="disp-none" runat="server">
                                                <i class="fa fa-history" style="font-size:17px"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="" ItemStyle-Width="5%" ItemStyle-CssClass=" txt_center ">
                                        <ItemTemplate>
                                            <asp:HiddenField runat="server" ID="hdnSDV" />
                                            <asp:CheckBox ID="chkSDV" CssClass="sdvCheckbox" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="REQUIREDYN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="lblREQUIREDYN" Text='<%# Bind("REQUIREDYN") %>' runat="server"></asp:Label>
                                            <asp:Label ID="READYN" Text='<%# Bind("READYN") %>' runat="server"></asp:Label>
                                            <asp:Label ID="SYNC_COUNT" Text='<%# Bind("SYNC_COUNT") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PREFIXTEXT" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPREFIXTEXT" Text='<%# Bind("PREFIXTEXT") %>' runat="server"></asp:Label>
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
                                                                                <asp:DropDownList ID="DRP_FIELD" CssClass="rightClick" runat="server"
                                                                                    onchange="showChild_ReadOnly(this);  checkOnChangeCrit(this);" Visible="false">
                                                                                </asp:DropDownList>
                                                                                <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                                                    OnClientClick="OpenModule(this);"></asp:LinkButton>
                                                                                <asp:TextBox ID="TXT_FIELD" runat="server" autocomplete="off" Visible="false" CssClass="manualInput  rightClick"
                                                                                    onchange="showChild_ReadOnly(this); checkOnChangeCrit(this);"></asp:TextBox>
                                                                                <asp:HiddenField runat="server" ID="HDN_FIELD" />
                                                                                <asp:HiddenField runat="server" ID="hfValue1" />
                                                                                <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                    <ItemTemplate>
                                                                                        <div class="col-md-4">
                                                                                            <asp:CheckBox ID="CHK_FIELD" runat="server" CssClass="checkbox rightClick"
                                                                                                onchange="showChild_ReadOnly(this); checkOnChangeCrit(this);"
                                                                                                ForeColor='<%#System.Drawing.Color.FromName(Eval ("color").ToString()) %>'
                                                                                                Text='<%# Bind("TEXT") %>' />
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <asp:Repeater runat="server" ID="repeat_RAD">
                                                                                    <ItemTemplate>
                                                                                        <div class="col-md-4">
                                                                                            <asp:RadioButton ID="RAD_FIELD" runat="server" onchange="showChild_ReadOnly(this);" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                onclick="RadioCheck(this);  checkOnChangeCrit(this);"
                                                                                                CssClass="radio rightClick" Text='<%# Bind("TEXT") %>' />
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <asp:HiddenField runat="server" ID="HDN_OLD_VALUE" />
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="MQ" ToolTip="Raise Manual Query" OnClientClick="return OpenManualQuery(this);" class="disp-none ManualQuery" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:deepskyblue;"></i>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="AQ" ToolTip="Open Query" OnClientClick="return SAE_ShowOpenQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:maroon"></i>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="AWSQ" ToolTip="Answered Open Query" OnClientClick="return SAE_ShowAnsQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:blue"></i>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="CQ" ToolTip="Closed Query" OnClientClick="return SAE_ShowClosedQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:18px;color:darkgreen"></i>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="CM" ToolTip="Field Comments" OnClientClick="return SAE_ShowComments(this);" runat="server" class="disp-none Comments">
                                                <i class="fa fa-comment" style="font-size:17px;color:darkmagenta;"></i>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="" ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="AD" ToolTip="Audit trail" OnClientClick="return SAE_showAuditTrail(this);" class="disp-none" runat="server">
                                                <i class="fa fa-history" style="font-size:17px"></i>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="" ItemStyle-Width="5%" ItemStyle-CssClass=" txt_center ">
                                                                        <ItemTemplate>
                                                                            <asp:HiddenField runat="server" ID="hdnSDV" />
                                                                            <asp:CheckBox ID="chkSDV" CssClass="sdvCheckbox" runat="server" OnClick="chkVerifyHF(this);" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="REQUIREDYN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblREQUIREDYN" Text='<%# Bind("REQUIREDYN") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="READYN" Text='<%# Bind("READYN") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="SYNC_COUNT" Text='<%# Bind("SYNC_COUNT") %>' runat="server"></asp:Label>
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
                                                                                                                <asp:DropDownList ID="DRP_FIELD" CssClass="rightClick" runat="server" onchange="showChild_ReadOnly(this); checkOnChangeCrit(this);" Visible="false">
                                                                                                                </asp:DropDownList>
                                                                                                                <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                                                                                    OnClientClick="OpenModule(this);"></asp:LinkButton>
                                                                                                                <asp:TextBox ID="TXT_FIELD" runat="server" autocomplete="off" Visible="false" CssClass="manualInput  rightClick"
                                                                                                                    onchange="showChild_ReadOnly(this); checkOnChangeCrit(this);"></asp:TextBox>
                                                                                                                <asp:HiddenField runat="server" ID="HDN_FIELD" />
                                                                                                                <asp:HiddenField runat="server" ID="hfValue1" />
                                                                                                                <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                                                    <ItemTemplate>
                                                                                                                        <div class="col-md-4">
                                                                                                                            <asp:CheckBox ID="CHK_FIELD" runat="server"
                                                                                                                                CssClass="checkbox rightClick" onchange="showChild_ReadOnly(this); checkOnChangeCrit(this);"
                                                                                                                                ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                                                Text='<%# Bind("TEXT") %>' />
                                                                                                                        </div>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:Repeater>
                                                                                                                <asp:Repeater runat="server" ID="repeat_RAD">
                                                                                                                    <ItemTemplate>
                                                                                                                        <div class="col-md-4">
                                                                                                                            <asp:RadioButton ID="RAD_FIELD" runat="server" onchange="showChild_ReadOnly(this);" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                                                onclick="RadioCheck(this);checkOnChangeCrit(this);"
                                                                                                                                CssClass="radio rightClick" Text='<%# Bind("TEXT") %>' />
                                                                                                                        </div>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:Repeater>
                                                                                                                <asp:HiddenField runat="server" ID="HDN_OLD_VALUE" />
                                                                                                            </div>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="MQ" ToolTip="Raise Manual Query" OnClientClick="return OpenManualQuery(this);" class="disp-none ManualQuery" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:deepskyblue;"></i>
                                                                                                            </asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="AQ" ToolTip="Open Query" OnClientClick="return SAE_ShowOpenQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:maroon"></i>
                                                                                                            </asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="AWSQ" ToolTip="Answered Open Query" OnClientClick="return SAE_ShowAnsQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:blue"></i>
                                                                                                            </asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="CQ" ToolTip="Closed Query" OnClientClick="return SAE_ShowClosedQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:18px;color:darkgreen"></i>
                                                                                                            </asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="CM" ToolTip="Field Comments" OnClientClick="return SAE_ShowComments(this);" runat="server" class="disp-none Comments">
                                                <i class="fa fa-comment" style="font-size:17px;color:darkmagenta;"></i>
                                                                                                            </asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="" ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="AD" ToolTip="Audit trail" OnClientClick="return SAE_showAuditTrail(this);" class="disp-none" runat="server">
                                                <i class="fa fa-history" style="font-size:17px"></i>
                                                                                                            </asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="" ItemStyle-Width="5%" ItemStyle-CssClass=" txt_center ">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:HiddenField runat="server" ID="hdnSDV" />
                                                                                                            <asp:CheckBox ID="chkSDV" CssClass="sdvCheckbox" runat="server" OnClick="chkVerifyHF(this);" />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="REQUIREDYN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblREQUIREDYN" Text='<%# Bind("REQUIREDYN") %>' runat="server"></asp:Label>
                                                                                                            <asp:Label ID="READYN" Text='<%# Bind("READYN") %>' runat="server"></asp:Label>
                                                                                                            <asp:Label ID="SYNC_COUNT" Text='<%# Bind("SYNC_COUNT") %>' runat="server"></asp:Label>
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
                                                                                                                                                <asp:DropDownList ID="DRP_FIELD" CssClass="rightClick" runat="server" onchange="showChild_ReadOnly(this); checkOnChangeCrit(this);" Visible="false">
                                                                                                                                                </asp:DropDownList>
                                                                                                                                                <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                                                                                                                    OnClientClick="OpenModule(this);"></asp:LinkButton>
                                                                                                                                                <asp:TextBox ID="TXT_FIELD" runat="server" autocomplete="off" Visible="false" CssClass="manualInput rightClick"
                                                                                                                                                    onchange="showChild_ReadOnly(this); checkOnChangeCrit(this);"></asp:TextBox>
                                                                                                                                                <asp:HiddenField runat="server" ID="HDN_FIELD" />
                                                                                                                                                <asp:HiddenField runat="server" ID="hfValue1" />
                                                                                                                                                <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <div class="col-md-4">
                                                                                                                                                            <asp:CheckBox ID="CHK_FIELD" runat="server"
                                                                                                                                                                CssClass="checkbox rightClick" onchange="showChild_ReadOnly(this); checkOnChangeCrit(this);"
                                                                                                                                                                ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                                                                                Text='<%# Bind("TEXT") %>' />
                                                                                                                                                        </div>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:Repeater>
                                                                                                                                                <asp:Repeater runat="server" ID="repeat_RAD">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <div class="col-md-4">
                                                                                                                                                            <asp:RadioButton ID="RAD_FIELD" runat="server" onchange="showChild_ReadOnly(this);"
                                                                                                                                                                ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                                                                                onclick="RadioCheck(this);  checkOnChangeCrit(this);"
                                                                                                                                                                CssClass="radio rightClick" Text='<%# Bind("TEXT") %>' />
                                                                                                                                                        </div>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:Repeater>
                                                                                                                                                <asp:HiddenField runat="server" ID="HDN_OLD_VALUE" />
                                                                                                                                            </div>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:LinkButton ID="MQ" ToolTip="Raise Manual Query" OnClientClick="return OpenManualQuery(this);" class="disp-none ManualQuery" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:deepskyblue;"></i>
                                                                                                                                            </asp:LinkButton>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:LinkButton ID="AQ" ToolTip="Open Query" OnClientClick="return SAE_ShowOpenQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:maroon"></i>
                                                                                                                                            </asp:LinkButton>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:LinkButton ID="AWSQ" ToolTip="Answered Open Query" OnClientClick="return SAE_ShowAnsQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:blue"></i>
                                                                                                                                            </asp:LinkButton>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:LinkButton ID="CQ" ToolTip="Closed Query" OnClientClick="return SAE_ShowClosedQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:18px;color:darkgreen"></i>
                                                                                                                                            </asp:LinkButton>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:LinkButton ID="CM" ToolTip="Field Comments" OnClientClick="return SAE_ShowComments(this);" runat="server" class="disp-none Comments">
                                                <i class="fa fa-comment" style="font-size:17px;color:darkmagenta;"></i>
                                                                                                                                            </asp:LinkButton>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="" ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:LinkButton ID="AD" ToolTip="Audit trail" OnClientClick="return SAE_showAuditTrail(this);" class="disp-none" runat="server">
                                                <i class="fa fa-history" style="font-size:17px"></i>
                                                                                                                                            </asp:LinkButton>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="" ItemStyle-Width="5%" ItemStyle-CssClass=" txt_center ">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:HiddenField runat="server" ID="hdnSDV" />
                                                                                                                                            <asp:CheckBox ID="chkSDV" CssClass="sdvCheckbox" runat="server" OnClick="chkVerifyHF(this);" />
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="REQUIREDYN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblREQUIREDYN" Text='<%# Bind("REQUIREDYN") %>' runat="server"></asp:Label>
                                                                                                                                            <asp:Label ID="READYN" Text='<%# Bind("READYN") %>' runat="server"></asp:Label>
                                                                                                                                            <asp:Label ID="SYNC_COUNT" Text='<%# Bind("SYNC_COUNT") %>' runat="server"></asp:Label>
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
                        <td class="pull-left">
                            <asp:Button ID="btnBacktoHomePage" runat="server" Text="Back to Main Page" CssClass="btn btn-sm btn-deepskyblue"
                                OnClick="btnBacktoHomePage_Click"></asp:Button>
                        </td>
                        <td class="pull-right">
                            <asp:Button ID="btnCancle" Text="Cancle" runat="server" Width="105px" class="btn btn-primary btn-sm cls-btnSave" Visible="false" OnClick="btnCancle_Click" />
                            &nbsp;
                            <asp:Button ID="btnSAVESDV" runat="server" Text="Submit SDV" CssClass="btn btn-DarkGreen btn-sm"
                                OnClick="btnSAVESDV_Click" OnClientClick="return countCheckboxes();" />
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
                    <asp:GridView ID="grdDeletedRcords" runat="server" CellPadding="3" Name="DSAE" AutoGenerateColumns="True"
                        CssClass="table table-bordered table-striped Datatable" ShowHeader="True" CellSpacing="2" Width="100%"
                        EmptyDataText="No records found" OnRowDataBound="grdDeletedRcords_RowDataBound" OnPreRender="gridsigninfo_PreRender">
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
                                        <asp:LinkButton ID="lnkQUERYSTATUS" ToolTip="Open Query" OnClientClick="return SAE_ShowOpenQuery_PVID_RECID(this);" runat="server">
                                                                     <i class="fa fa-question-circle" style="font-size:17px;color:maroon;"></i></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                                                   <asp:LinkButton ID="lnkQUERYANS" ToolTip="Answered Open Query" OnClientClick="return SAE_ShowAnsQuery_PVID_RECID(this);" runat="server">
                                                                     <i class="fa fa-question-circle" style="font-size:17px;color:blue;"></i></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lnkQUERYCLOSE" ToolTip="Closed Query" OnClientClick="return SAE_ShowClosedQuery_PVID_RECID(this);" runat="server">
                                                                     <i class="fa fa-question-circle" style="font-size:17px;color:darkgreen;"></i></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
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
                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Reviewed By]</label><br />
                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div runat="server" id="divREVIEWED">
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
                                    <label>Medical Review Details</label><br />
                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Medical Reviewed By]</label><br />
                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div runat="server" id="divMR">
                                        <div>
                                            <asp:Label ID="MRBYNAME" runat="server" Text='<%# Bind("MRBYNAME") %>' ForeColor="Blue"></asp:Label>
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
                            <asp:TemplateField HeaderStyle-CssClass="align-left">
                                <HeaderTemplate>
                                    <label>Signed Off Details</label><br />
                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Signed By]</label><br />
                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div runat="server" id="divSIGN">
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
                                    <label>Delete</label><br />
                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Deleted By]</label><br />
                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div runat="server" id="divDELETE">
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
