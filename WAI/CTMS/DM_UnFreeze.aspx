<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DM_UnFreeze.aspx.cs" Inherits="CTMS.DM_UnFreeze" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script src="CommonFunctionsJs/DivExpandCollapse.js"></script>
    <script src="CommonFunctionsJs/btnSave_Required.js"></script>

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
                alert("Select at least one data to Generate UnFreeze Request");
                event.preventDefault();
            }
        }

        function Check_child(element) {

            $(element).closest('tr').next().find('td').find('table').find('input[type=checkbox][id*=Chkchild_UnFreezeYN]').each(function () {
                if ($(element).prop('checked') == true) {
                    $(this).prop('checked', true);
                }
                else {
                    $(this).prop('checked', false);
                }
            });
        }

        function Check_All(element) {
            $('input[type=checkbox][id*=Chk_UnFreezeYN]').each(function () {
                if ($(element).prop('checked') == true) {
                    $(this).prop('checked', true);
                }
                else {
                    $(this).prop('checked', false);
                }
            });
            $('input[type=checkbox][id*=Chkchild_UnFreezeYN]').each(function () {
                if ($(element).prop('checked') == true) {
                    $(this).prop('checked', true);
                }
                else {
                    $(this).prop('checked', false);
                }
            });
        }
    </script>
    <script type="text/javascript">
        function VIEW_DM_DATA(element) {

            var PVID = "", RECID = "", SUBJID = "", VISITID = "", MODULEID = "", TABLENAME = "", MODULENAME = "";

            PVID = $(element).closest('tr').find('td:eq(0)').find('span').text();
            RECID = $(element).closest('tr').find('td:eq(1)').find('span').text();

            if ((($(element).closest('table').attr('id')).indexOf('Gridchild') != -1)) {

                //find  value with parent  grid
                SUBJID = $(element).closest('table').closest('tr').prev().find('td:eq(11)').find('span').text();
                VISITID = $(element).closest('table').closest('tr').prev().find('td:eq(2)').find('span').text();
                MODULEID = $(element).closest('table').closest('tr').prev().find('td:eq(3)').find('span').text();
                TABLENAME = $(element).closest('table').closest('tr').prev().find('td:eq(4)').find('span').text();
                MODULENAME = $(element).closest('table').closest('tr').prev().find('td:eq(14)').find('span').text();
            }
            else {

                SUBJID = $(element).closest('tr').find('td:eq(11)').find('span').text();
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Generate UnFreeze Request
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
                    <asp:DropDownList ID="drpSubID" ForeColor="Blue" runat="server" CssClass="form-control select" SelectionMode="Single"
                        AutoPostBack="True" OnSelectedIndexChanged="drpSubID_SelectedIndexChanged">
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
            <div class="row">
                <div class="">
                    <div class="col-md-2" style="width: 12%;">
                        <label class="label" style="color: Maroon;">
                            Enter Comment:
                        </label>
                    </div>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="txtComment" Width="96%" Height="60px" TextMode="MultiLine" MaxLength="500"
                            CssClass="form-control required1"> 
                        </asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="box-body">
            <div class="form-group ">
                <label id="selectchkall" visible="false" runat="server" class="label pull-right">
                    Select All:
                            <asp:CheckBox ID="Chk_Select_All" runat="server" AutoPostBack="True" OnCheckedChanged="Chk_Select_All_CheckedChanged" />
                </label>
                <div style="width: 100%; overflow: auto; padding-top: 10px;">
                    <asp:GridView ID="Grd_UNFreeze" runat="server" AutoGenerateColumns="False" CellPadding="3" OnRowDataBound="Grd_UNFreeze_RowDataBound"
                        CellSpacing="2" CssClass="table table-bordered table-striped" HeaderStyle-ForeColor="Blue">
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
                            <asp:TemplateField ItemStyle-CssClass="txtCenter width40px" ControlStyle-CssClass="txt_center"
                                HeaderStyle-CssClass="txt_center width20px">
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
                            <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                <HeaderTemplate>
                                    <asp:Button ID="Btn_Update" runat="server" OnClick="Btn_Update_Click" CssClass="btn btn-primary btn-sm cls-btnSave1"
                                        Text="UnFreeze YN" OnClientClick="return countCheckboxes();" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="Chk_UnFreezeYN" runat="server" onclick="Check_child(this)" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="View" ItemStyle-CssClass="txt_center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbview" runat="server" Class="fa fa-search" ToolTip="View Data" OnClientClick="return VIEW_DM_DATA(this);"></asp:LinkButton>
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
                                            <div id="PVID1<%# Eval("PVID") %>" style="display: none; position: relative; overflow: scroll;">
                                                <div class="box">
                                                    <div style="width: 100%; overflow: scroll;">
                                                        <asp:GridView ID="Gridchild" runat="server" CellPadding="3" AutoGenerateColumns="false" HeaderStyle-ForeColor="Maroon"
                                                            CssClass="table table-bordered table-striped" OnRowDataBound="Gridchild_RowDataBound">
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
                                                                        <asp:CheckBox ID="Chkchild_UnFreezeYN" CssClass="freezcheck" runat="server" />
                                                                        <asp:Label ID="lblQuery" runat="server" Text="Open Queries" ForeColor="Red" Visible="false"> </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="View" ItemStyle-CssClass="txt_center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="LBGOTOPAGE" runat="server" Class="fa fa-search" OnClientClick="return VIEW_DM_DATA(this);"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Record No." ItemStyle-CssClass="txt_center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="RECORDNO" runat="server" Text='<%# Bind("RECORDNO") %>'></asp:Label>
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
        </div>
    </div>
</asp:Content>
