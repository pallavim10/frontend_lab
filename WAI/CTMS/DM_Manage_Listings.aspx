<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DM_Manage_Listings.aspx.cs" Inherits="CTMS.DM_Manage_Listings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="CommonFunctionsJs/DM/DM_ConfirmMsg.js"></script>
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <script type="text/javascript" src="CommonFunctionsJs/DB/DB_showAuditTrail.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/btnSave_Required.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/TabIndex.js"></script>
    <script runat="server"> 
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            // the following line is important 
            MasterPage master = this.Master;
        }
    </script>
    <script type="text/javascript" language="javascript">
        function pageLoad() {
            //CalculateRPN();
            $('.select').select2();


            $(".numeric").on("keypress keyup blur", function (event) {
                $(this).val($(this).val().replace(/[^\d].+/, ""));
                if ((event.which < 48 || event.which > 57)) {
                    event.preventDefault();
                }
            });

            //only for numeric value
            $('.numeric').keypress(function (event) {

                if (event.keyCode == 8 || event.keyCode == 9 || event.charCode == 48 || event.charCode == 49 || event.charCode == 50 || event.charCode == 51
                    || event.charCode == 52 || event.charCode == 52 || event.charCode == 53 || event.charCode == 54 || event.charCode == 55 || event.charCode == 56 || event.charCode == 57) {
                    // let it happen, don't do anything
                    return true;
                }
                else {
                    event.preventDefault();
                }
            });


            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: true,
                fixedHeader: true
            });

            $('.txtDate').each(function (index, element) {
                $(element).pikaday({
                    field: element,
                    // trigger: $(element).closest('div').find('.datepicker-button').get(0), // <<<<
                    // firstDay: 1,
                    //position: 'top right',
                    // minDate: new Date('2000-01-01'),
                    // maxDate: new Date('9999-12-31'),
                    format: 'DD-MMM-YYYY',
                    //  defaultDate: new Date(''),
                    //setDefaultDate: false,
                    yearRange: [1910, 2050]
                });
            });
        }

        function bindValues() {
            var avaTag = $("#<%=hfValues.ClientID%>").val().split(",");
            $('.Value').autocomplete({
                source: avaTag, minLength: 0
            }).on('focus', function () { $(this).keydown(); });
        }

        function bindColorValues() {
            var colorFields = $(".ColorValues").toArray();
            for (a = 0; a < colorFields.length; ++a) {
                var avaTag = $(colorFields[a]).closest('tr').find('td span:eq(1)').text().split(",");
                $(colorFields[a]).autocomplete({
                    source: avaTag, minLength: 0
                }).on('focus', function () { $(this).keydown(); });
            }

            var ColorBox = $("input[id*='ColorBoxPrimColor']").toArray();
            for (a = 0; a < ColorBox.length; ++a) {

                var color = $(ColorBox[a]).closest('td').find('input:eq(1)').val();

                $(ColorBox[a]).attr('value', color);
            }
        }

        $(function () {
            $('.Value').autocomplete({
                source: $("#<%=hfValues.ClientID%>").val(), minLength: 0
            }).on('focus', function () { $(this).keydown(); });
        });

        $(function () {
            var colorFields = $(".ColorValues").toArray();
            for (a = 0; a < colorFields.length; ++a) {
                var avaTag = $(colorFields[a]).closest('tr').find('td span:eq(1)').text().split(",");
                $(colorFields[a]).autocomplete({
                    source: avaTag, minLength: 0
                }).on('focus', function () { $(this).keydown(); });
            }
        });

        function bindColorBox() {
            var ColorBox = $("input[id*='ColorBoxPrimColor']").toArray();
            for (a = 0; a < ColorBox.length; ++a) {

                var color = $(ColorBox[a]).closest('td').find('input:eq(1)');

                $(ColorBox[a]).attr('value', color)
            }
        }

        function ChkAllField(element) {

            var gvID = $(element).closest('table').attr('id');

            $('#' + gvID + ' input[type=checkbox][id*=chkListing]').each(function () {

                if ($(element).prop('checked') == true) {
                    $(this).prop('checked', true);
                }
                else {
                    $(this).prop('checked', false);
                }
            });
        }

        function ValidateTXTSEQNO() {

            var count = 0;

            $('.chkList').each(function () {

                var checkListing = $(this).closest('tr').find('input').eq(0).is(':checked');
                var TXTSEQNO = $(this).closest('tr').find('input').eq(1).val();

                if (checkListing) {

                    count++;

                    if (TXTSEQNO.trim() == '' || TXTSEQNO.trim() == null) {
                        alert("Please Enter Seq. No");
                        event.preventDefault();
                        return false;
                    }
                    else {
                        return true;
                    }
                }
            });

            if (count < 1) {
                alert("Select at least one data to add listing fields.");
                event.preventDefault();
            }
        }

        function CheckOtherListingChk() {

            var count = 0;

            $('.chkOtherListing').each(function () {

                var checkListing = $(this).closest('tr').find('input').eq(0).is(':checked');
                var ddlFilter = $(this).closest('tr').find('select').val();

                if (checkListing) {

                    count++;

                    if (ddlFilter.trim() == '' || ddlFilter.trim() == null || ddlFilter.trim() == '0') {
                        alert("Please select filter option to add other listings.");
                        event.preventDefault();
                        return false;
                    }
                    else {
                        return true;
                    }
                }
            });

            if (count < 1) {
                alert("Select at least one data to set other listings criteria.");
                event.preventDefault();
            }
        }

        function CheckOtherListingChk_Remove() {

            var count = 0;

            $('.chkOtherListing_Remove').each(function () {

                var checkListing = $(this).closest('tr').find('input').eq(0).is(':checked');

                if (checkListing) {

                    count++;
                }
            });

            if (count < 1) {
                alert("Select at least one data to remove other listings criteria.");
                event.preventDefault();
            }
        }

        function CheckPrimaryDetailsChk() {

            var count = 0;

            $('.CheckPrimaryDetailsChk').each(function () {

                var checkListing = $(this).closest('tr').find('input').eq(0).is(':checked');

                if (checkListing) {
                    count++;
                }
            });

            if (count < 1) {
                alert("Select at least one data to set Primary Details.");
                event.preventDefault();
            }
        }

        function CheckPrimaryDetailsChk_Remove() {

            var count = 0;

            $('.CheckPrimaryDetailsChk_Remove').each(function () {

                var checkListing = $(this).closest('tr').find('input').eq(0).is(':checked');

                if (checkListing) {
                    count++;
                }
            });

            if (count < 1) {
                alert("Select at least one data to remove Primary Details.");
                event.preventDefault();
            }
        }
    </script>
    <script type="text/javascript" language="javascript">

        function Print() {
            var ProjectId = '<%# Session["PROJECTID"] %>'
            var MODULEID = $("#HDNMODULEID").val();
            var MODULENAME = $("#HDMODULENAME").val();
            var test = "ReportDM_Mappings.aspx?ProjectId=" + ProjectId + "&MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500px,width=1000px";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);

            if (div.style.display == "none") {
                div.style.display = "inline";
                document.getElementById('img' + divname).className = 'icon-minus-sign-alt';

            } else {
                div.style.display = "none";
                document.getElementById('img' + divname).className = 'icon-plus-sign-alt';
            }
        }

        function ManipulateAll(ID) {
            var img = document.getElementById('img' + ID);

            if (img.className == 'icon-plus-sign-alt') {
                img.className = 'icon-minus-sign-alt'
                $("div[id*='" + ID + "']").css("display", "inline");
                $("i[id*='" + ID + "']").removeClass('icon-plus-sign-alt');
                $("i[id*='" + ID + "']").addClass('icon-minus-sign-alt');
            } else {
                img.className = 'icon-plus-sign-alt'
                $("div[id*='" + ID + "']").css("display", "none");
                $("i[id*='" + ID + "']").removeClass('icon-minus-sign-alt');
                $("i[id*='" + ID + "']").addClass('icon-plus-sign-alt');
            }
        }

        function OnClickListingShow() {
            var Field = $('#MainContent_ddlOnClickField :selected').text();

            if (Field == 'Subject') {
                $(".divOnClickListing").removeClass('disp-none');
            }
            else {
                $(".divOnClickListing").addClass('disp-none');
            }
        }

        function set_Color(element) {
            var fcolor = element.value;
            $(element).closest('tr').find('td:eq(7)').find('input:eq(1)').attr('value', fcolor);
        }

        function ChangeDivQuery() {
            if ($('#MainContent_chkMM').prop('checked') == true) {
                $('#divQueryText').removeClass('disp-none');
                $('#divParent').removeClass('disp-none');
                $('#divAddFuncs').removeClass('disp-none');
            }
            else {

                if (!$('#divQueryText').hasClass('disp-none')) {
                    $('#divQueryText').addClass('disp-none');
                }

                if (!$('#divParent').hasClass('disp-none')) {
                    $('#divParent').addClass('disp-none');
                }

                if (!$('#divAddFuncs').hasClass('disp-none')) {
                    $('#divAddFuncs').addClass('disp-none');
                }
            }
        }

        function showDropdownList() {
            if ($('#MainContent_chkManCode').prop('checked') == true) {
                $('#MainContent_drpAutoCode').removeClass('disp-none');
            }
            else {
                if (!$('#MainContent_drpAutoCode').hasClass('disp-none')) {
                    $('#MainContent_drpAutoCode').addClass('disp-none');
                }
            }
        }
    </script>
    <style>
        .select2-container .select2-selection--multiple {
            min-height: 160px;
        }

        .inpputchkEditable {
            outline: 2px #c00 solid;
            outline-offset: -2px;
        }
    </style>
    <style type="text/css">
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #0000ff;
        }

        .select2-container--default .select2-selection--single {
            margin-top: 2px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager11" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">Manage Listings
                    </h3>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                            <asp:HiddenField runat="server" ID="hfValues" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">Manage Fields
                    </h3>
                </div>
                <div class="form-group" style="margin-bottom: 10px;">
                    <div class="col-md-12">
                        <div class="col-md-6">
                            <div class="col-md-3">
                                <label>
                                    Select Listing :</label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="drpList" runat="server" CssClass="form-control width300px"
                                    OnSelectedIndexChanged="drpList_SelectedIndexChanged" AutoPostBack="true" TabIndex="1">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <asp:Button ID="btnAddFieldsList" runat="server" Text="Add/Remove Fields" OnClick="btnAddFieldsList_Click"
                                CssClass="btn btn-primary btn-sm pull-right" Visible="false" OnClientClick="ValidateTXTSEQNO();" TabIndex="2" />
                        </div>
                    </div>
                    <br />
                    <div class="box-body">
                        <div class="row">
                            &nbsp;
                        </div>
                        <div class="row">
                            <asp:GridView ID="grd_Module" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                Style="width: 95%; border-collapse: collapse; margin-left: 20px; margin-bottom: 10px;"
                                CssClass="table table-bordered table-striped " OnRowDataBound="grd_Module_RowDataBound">
                                <Columns>
                                    <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                                        HeaderStyle-CssClass="txt_center">
                                        <HeaderTemplate>
                                            <a href="JavaScript:ManipulateAll('_Field_');" id="_Folder" style="color: #333333"><i
                                                id="img_Field_" class="icon-plus-sign-alt"></i></a>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div runat="server" id="anchor">
                                                <a href="JavaScript:divexpandcollapse('_Field_<%# Eval("ModuleID") %>');" style="color: #333333">
                                                    <i id="img_Field_<%# Eval("ModuleID") %>" class="icon-plus-sign-alt"></i></a>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="lblID" Text='<%# Eval("ModuleID") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Module Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblModule" Text='<%# Eval("MODULENAME") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <tr>
                                                <td colspan="100%" style="padding: 2px; padding-left: 3%;">
                                                    <div style="float: right; font-size: 13px;">
                                                    </div>
                                                    <div>
                                                        <div id="_Field_<%# Eval("ModuleID") %>" style="display: none; position: relative; overflow: auto;">
                                                            <asp:GridView ID="grd_Field" runat="server" CellPadding="3" AutoGenerateColumns="False" Style="width: 99%; border-collapse: collapse; margin-left: 8px; margin-bottom: 10px;"
                                                                CssClass="table table-bordered table-striped table-striped1" OnRowDataBound="grd_Field_RowDataBound">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="ID" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="txt_center disp-none"
                                                                        ItemStyle-CssClass="txt_center disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblID" Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderStyle-Width="10%" ItemStyle-Width="10%" HeaderStyle-CssClass="txt_center">
                                                                        <HeaderTemplate>
                                                                            <label>Select All</label><br />
                                                                            <asp:CheckBox ID="ChkAllField" runat="server" AutoPostBack="false" OnClick="ChkAllField(this)" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkListing" CssClass="chkList" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="SEQ NO" HeaderStyle-Width="5%" ItemStyle-Width="5%"
                                                                        HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtSEQNO" Width="51px" Text='<%# Eval("SEQNO") %>' Style="text-align: center;" MaxLength="3" ValidationGroup="section"
                                                                                runat="server" CssClass="form-control numeric"></asp:TextBox>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Field Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblField" Text='<%# Eval("FIELDNAME") %>' runat="server"></asp:Label>
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
                    <br />
                </div>
            </div>
            <div id="DIVMANAGEVISIVLE" runat="server" visible="false">
                <div class="row" runat="server" id="DivMngAdditional">
                    <div class="col-md-6">
                        <div class="box box-primary" style="min-height: 300px;">
                            <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                <h4 class="box-title" align="left">Manage Additional Fields
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    <label>
                                                        Enter Field Name :</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox Style="width: 90%;" ID="txtAddFieldName" runat="server" CssClass="form-control required1" TabIndex="3"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    <label>
                                                        Enter Formula :</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox runat="server" ID="txtFormula" Width="90%" Height="140px" TextMode="MultiLine"
                                                        CssClass="form-control required1" TabIndex="4"> 
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4" style="display: inline-flex">
                                                    <asp:Button ID="btnAddFormula" Text="Add" runat="server" CssClass="btn btn-primary btn-sm"
                                                        OnClick="btnAddFormula_Click" TabIndex="5" />
                                                    
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="drpFunction" Width="90%" runat="server" CssClass="form-control" TabIndex="6">
                                                        <asp:ListItem Selected="True" Text="-Select Function-" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="DATEDIFF" Value="DATEDIFF(<day/week/month/quarter/year>, <date1>, <date1>)"></asp:ListItem>
                                                        <asp:ListItem Text="DATEADD" Value="DATEADD(<day/week/month/quarter/year> , <Number>, <date>)"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <br />
                                                    <asp:DropDownList ID="drpFormulaField" Width="90%" runat="server" CssClass="form-control" TabIndex="7">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    &nbsp;
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:Button ID="btnAddField" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave1"
                                                        OnClick="btnAddField_Click" TabIndex="8" />
                                                    <asp:Button ID="btnUpdateField" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave1"
                                                        OnClick="btnUpdateField_Click" TabIndex="9" />
                                                    <asp:Button ID="btncancelField" Text="Cancel" runat="server" CssClass="btn btn-danger btn-sm"
                                                        OnClick="btncancelField_Click"  TabIndex="10"/>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-success" style="min-height: 284px;">
                            <div class="box-header with-border">
                                <h4 class="box-title" align="left">Additional Fields
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div>
                                        <div class="rows">
                                            <div style="width: 100%; height: 330px; overflow: auto;">
                                                <div>
                                                    <asp:GridView ID="grdAddFields" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                        Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdAddFields_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnEditField" CommandArgument='<%# Bind("ID") %>' CommandName="EditField"
                                                                        runat="server" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Field Name" ItemStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFIELDNAME" runat="server" Style="text-align: left" Text='<%# Bind("FIELDNAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Formula" ItemStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFORMULA" runat="server" Style="text-align: left" Text='<%# Bind("FORMULA") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('DM_LISTINGS_AddFields', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                <HeaderTemplate>
                                                                    <label>Entered By Details</label><br />
                                                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Entered By]</label><br />
                                                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <div>
                                                                            <asp:Label ID="ENTEREDBYNAME" runat="server" Text='<%# Bind("ENTEREDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                                        </div>
                                                                        <div>
                                                                            <asp:Label ID="ENTERED_CAL_DAT" runat="server" Text='<%# Bind("ENTERED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                                        </div>
                                                                        <div>
                                                                            <asp:Label ID="ENTERED_CAL_TZDAT" runat="server" Text='<%# Bind("ENTERED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                <HeaderTemplate>
                                                                    <label>Last Modified Details</label><br />
                                                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Modified By]</label><br />
                                                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <div>
                                                                            <asp:Label ID="UPDATEDBYNAME" runat="server" Text='<%# Bind("UPDATEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                                        </div>
                                                                        <div>
                                                                            <asp:Label ID="UPDATED_CAL_DAT" runat="server" Text='<%# Bind("UPDATED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                                        </div>
                                                                        <div>
                                                                            <asp:Label ID="UPDATED_CAL_TZDAT" runat="server" Text='<%# Bind("UPDATED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnDeleteField" CommandArgument='<%# Bind("ID") %>' CommandName="DeleteField"
                                                                        runat="server" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this field : ", Eval("FIELDNAME")) %>' ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>&nbsp;&nbsp;
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
                        </div>
                    </div>
                </div>
                <div class="row" runat="server" id="DivSetCriteria">
                    <div class="col-md-6">
                        <div class="box box-primary" style="min-height: 250px;">
                            <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                <h4 class="box-title" align="left">Set Criteria
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    <label>
                                                        Select Field :</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="drpField" runat="server" CssClass="form-control required" AutoPostBack="True"
                                                        OnSelectedIndexChanged="drpField_SelectedIndexChanged" Style="width: 100%;" TabIndex="11">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="drpCondition1" runat="server" CssClass="form-control required" TabIndex="12">
                                                        <asp:ListItem Selected="True" Text="-Select Condition-" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Is Blank" Value="IS NULL"></asp:ListItem>
                                                        <asp:ListItem Text="Is Not Blank" Value="IS NOT NULL"></asp:ListItem>
                                                        <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                        <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                        <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                        <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                        <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                        <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                        <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                                        <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                                        <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                                        <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox runat="server" CssClass="Value form-control" ID="txtValue1" TabIndex="13">
                                                    </asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="drpAndOr1" runat="server" CssClass="form-control" TabIndex="14">
                                                        <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                        <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                        <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="drpCondition2" runat="server" CssClass="form-control" TabIndex="15">
                                                        <asp:ListItem Selected="True" Text="-Select Condition-" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Is Blank" Value="IS NULL"></asp:ListItem>
                                                        <asp:ListItem Text="Is Not Blank" Value="IS NOT NULL"></asp:ListItem>
                                                        <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                        <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                        <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                        <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                        <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                        <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                        <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                                        <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                                        <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                                        <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox runat="server" CssClass="Value form-control" ID="txtValue2" TabIndex="16">
                                                    </asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="drpAndOr2" runat="server" CssClass="form-control" TabIndex="17">
                                                        <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                        <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                        <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="drpCondition3" runat="server" CssClass="form-control" TabIndex="18">
                                                        <asp:ListItem Selected="True" Text="-Select Condition-" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Is Blank" Value="IS NULL"></asp:ListItem>
                                                        <asp:ListItem Text="Is Not Blank" Value="IS NOT NULL"></asp:ListItem>
                                                        <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                        <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                        <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                        <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                        <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                        <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                        <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                                        <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                                        <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                                        <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox runat="server" CssClass="Value form-control" ID="txtValue3" TabIndex="19">
                                                    </asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="drpAndOr3" runat="server" CssClass="form-control" TabIndex="20">
                                                        <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                        <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                        <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="drpCondition4" runat="server" CssClass="form-control" TabIndex="21">
                                                        <asp:ListItem Selected="True" Text="-Select Condition-" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Is Blank" Value="IS NULL"></asp:ListItem>
                                                        <asp:ListItem Text="Is Not Blank" Value="IS NOT NULL"></asp:ListItem>
                                                        <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                        <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                        <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                        <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                        <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                        <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                        <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                                        <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                                        <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                                        <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox runat="server" CssClass="Value form-control" ID="txtValue4" TabIndex="22">
                                                    </asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="drpAndOr4" runat="server" CssClass="form-control" TabIndex="23">
                                                        <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                        <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                        <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="drpCondition5" runat="server" CssClass="form-control" TabIndex="24">
                                                        <asp:ListItem Selected="True" Text="-Select Condition-" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Is Blank" Value="IS NULL"></asp:ListItem>
                                                        <asp:ListItem Text="Is Not Blank" Value="IS NOT NULL"></asp:ListItem>
                                                        <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                        <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                        <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                        <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                        <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                        <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                        <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                                        <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                                        <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                                        <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox runat="server" CssClass="Value form-control" ID="txtValue5" TabIndex="25">
                                                    </asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    &nbsp;
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    &nbsp;
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:Button ID="btnSaveCrit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                        OnClick="btnSaveCrit_Click" TabIndex="26" />
                                                    <asp:Button ID="btnCancelCrit" Text="Cancel" runat="server" CssClass="btn btn-danger btn-sm"
                                                        OnClick="btnCancelCrit_Click" TabIndex="27" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-primary">
                            <div class="box-header with-border" style="float: left;">
                                <h4 class="box-title" align="left">Criterias
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div>
                                        <div class="rows">
                                            <div style="width: 100%; height: 215px; overflow: auto;">
                                                <div>
                                                    <asp:GridView ID="grdCriterias" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped "
                                                        Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdCriterias_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%"
                                                                ItemStyle-Width="15%">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnUpdate" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                        CommandName="EditCrit" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Field Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFIELDNAME" runat="server" Text='<%# Eval("FIELDNAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Criterias">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNAME" runat="server" Text='<%# Eval("Criteria") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('DM_Listings_Criteria', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                <HeaderTemplate>
                                                                    <label>Entered By Details</label><br />
                                                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Entered By]</label><br />
                                                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <div>
                                                                            <asp:Label ID="ENTEREDBYNAME" runat="server" Text='<%# Bind("ENTEREDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                                        </div>
                                                                        <div>
                                                                            <asp:Label ID="ENTERED_CAL_DAT" runat="server" Text='<%# Bind("ENTERED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                                        </div>
                                                                        <div>
                                                                            <asp:Label ID="ENTERED_CAL_TZDAT" runat="server" Text='<%# Bind("ENTERED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                <HeaderTemplate>
                                                                    <label>Last Modified Details</label><br />
                                                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Modified By]</label><br />
                                                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <div>
                                                                            <asp:Label ID="UPDATEDBYNAME" runat="server" Text='<%# Bind("UPDATEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                                        </div>
                                                                        <div>
                                                                            <asp:Label ID="UPDATED_CAL_DAT" runat="server" Text='<%# Bind("UPDATED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                                        </div>
                                                                        <div>
                                                                            <asp:Label ID="UPDATED_CAL_TZDAT" runat="server" Text='<%# Bind("UPDATED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%"
                                                                ItemStyle-Width="15%">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtndelete" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                        CommandName="DeleteCrit" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this field : ", Eval("FIELDNAME")) %>' ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                        </div>
                    </div>
                </div>
                <div class="row" runat="server" id="DivSetCriteriaAcrossModule">
                    <div class="col-md-6">
                        <div class="box box-primary" style="min-height: 250px;">
                            <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                <h4 class="box-title" align="left">Set Criteria Across Modules
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-3">
                                                    <label>
                                                        Select Module :</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlListModules1" runat="server" CssClass="form-control required4"
                                                        AutoPostBack="True" Style="width: 100%;" OnSelectedIndexChanged="ddlListModules1_SelectedIndexChanged" TabIndex="28">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-3">
                                                    <label>
                                                        Select Field :</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlListField1" runat="server" CssClass="form-control required4"
                                                        Style="width: 100%;" TabIndex="29">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-3">
                                                    <label>
                                                        Select Condition :</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlACM_Condition" runat="server" Style="width: 100%;" CssClass="form-control required4" TabIndex="30">
                                                        <asp:ListItem Selected="True" Text="-Select Condition-" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                        <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                        <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                        <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                        <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                        <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-3">
                                                    <label>
                                                        Select Module :</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlListModules2" runat="server" CssClass="form-control required4"
                                                        AutoPostBack="True" Style="width: 100%;" OnSelectedIndexChanged="ddlListModules2_SelectedIndexChanged" TabIndex="31">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-3">
                                                    <label>
                                                        Select Field :</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlListField2" runat="server" CssClass="form-control required4"
                                                        Style="width: 100%;" TabIndex="32">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    &nbsp;
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:Button ID="btnSubmitACM" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave4"
                                                        OnClick="btnSubmitACM_Click" TabIndex="33" />
                                                    <asp:Button ID="btnUpdateACM" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave4"
                                                        OnClick="btnUpdateACM_Click" TabIndex="34" />
                                                    <asp:Button ID="btnCancelACM" Text="Cancel" runat="server" CssClass="btn btn-danger btn-sm"
                                                        OnClick="btnCancelACM_Click" TabIndex="35" />
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-primary">
                            <div class="box-header with-border" style="float: left;">
                                <h4 class="box-title" align="left">Criterias
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div>
                                        <div class="rows">
                                            <div style="width: 100%; height: 257px; overflow: auto;">
                                                <div>
                                                    <asp:GridView ID="grdACM" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                        Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdACM_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%"
                                                                ItemStyle-Width="15%">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                        CommandName="EditCrit" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Criterias">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNAME" runat="server" Text='<%# Eval("Criteria") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('DM_Listing_CritModules', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                <HeaderTemplate>
                                                                    <label>Entered By Details</label><br />
                                                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Entered By]</label><br />
                                                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <div>
                                                                            <asp:Label ID="ENTEREDBYNAME" runat="server" Text='<%# Bind("ENTEREDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                                        </div>
                                                                        <div>
                                                                            <asp:Label ID="ENTERED_CAL_DAT" runat="server" Text='<%# Bind("ENTERED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                                        </div>
                                                                        <div>
                                                                            <asp:Label ID="ENTERED_CAL_TZDAT" runat="server" Text='<%# Bind("ENTERED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                <HeaderTemplate>
                                                                    <label>Last Modified Details</label><br />
                                                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Modified By]</label><br />
                                                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <div>
                                                                            <asp:Label ID="UPDATEDBYNAME" runat="server" Text='<%# Bind("UPDATEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                                        </div>
                                                                        <div>
                                                                            <asp:Label ID="UPDATED_CAL_DAT" runat="server" Text='<%# Bind("UPDATED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                                        </div>
                                                                        <div>
                                                                            <asp:Label ID="UPDATED_CAL_TZDAT" runat="server" Text='<%# Bind("UPDATED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%"
                                                                ItemStyle-Width="15%">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtndelete" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                        CommandName="DeleteCrit" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this criteria : ", Eval("Criteria")) %>' ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                        </div>
                    </div>
                </div>
                <div class="row" runat="server" id="DivSetCritAcrossListing">
                    <div class="col-md-6">
                        <div class="box box-primary" style="min-height: 250px;">
                            <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                <h4 class="box-title" align="left">Set Criteria Across Listings
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    <label>
                                                        Select Field :</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="drpACLFields" runat="server" CssClass="form-control required2"
                                                        AutoPostBack="True" Style="width: 100%;" OnSelectedIndexChanged="drpACLFields_SelectedIndexChanged" TabIndex="36">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    <label>
                                                        Select Condition :</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="drpACLCondition" runat="server" CssClass="form-control required2"
                                                        Style="width: 100%;" TabIndex="37">
                                                        <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Contains In" Value="Contains In"></asp:ListItem>
                                                        <asp:ListItem Text="Does not Contains In" Value="Does not Contains In"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    <label>
                                                        Select Target Listing :</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="drpACLTgList" AutoPostBack="true" runat="server" CssClass="form-control required2"
                                                        Style="width: 100%;" OnSelectedIndexChanged="drpACLTgList_SelectedIndexChanged" TabIndex="38">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    <label>
                                                        Select Target Field :</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="drpACLTgListField" runat="server" CssClass="form-control required2"
                                                        Style="width: 100%;" TabIndex="39">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    &nbsp;
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:Button ID="btnSubmitACL" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave2"
                                                        OnClick="btnSubmitACL_OnClick_Click" TabIndex="40" />
                                                    <asp:Button ID="btnCancelACL" Text="Cancel" runat="server" CssClass="btn btn-danger btn-sm"
                                                        OnClick="btnCancelACL_OnClick_Click" TabIndex="41" />
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-primary">
                            <div class="box-header with-border" style="float: left;">
                                <h4 class="box-title" align="left">Records
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div>
                                        <div class="rows">
                                            <div style="width: 100%; height: 215px; overflow: auto;">
                                                <div>
                                                    <asp:GridView ID="grdArcListing" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped "
                                                        Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdArcListing_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%"
                                                                ItemStyle-Width="15%">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                        CommandName="EditArcListing" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Criteria">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNAME" runat="server" Text='<%# Eval("Criteria") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('DM_Listing_CritLists', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                <HeaderTemplate>
                                                                    <label>Entered By Details</label><br />
                                                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Entered By]</label><br />
                                                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <div>
                                                                            <asp:Label ID="ENTEREDBYNAME" runat="server" Text='<%# Bind("ENTEREDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                                        </div>
                                                                        <div>
                                                                            <asp:Label ID="ENTERED_CAL_DAT" runat="server" Text='<%# Bind("ENTERED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                                        </div>
                                                                        <div>
                                                                            <asp:Label ID="ENTERED_CAL_TZDAT" runat="server" Text='<%# Bind("ENTERED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                <HeaderTemplate>
                                                                    <label>Last Modified Details</label><br />
                                                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Modified By]</label><br />
                                                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <div>
                                                                            <asp:Label ID="UPDATEDBYNAME" runat="server" Text='<%# Bind("UPDATEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                                        </div>
                                                                        <div>
                                                                            <asp:Label ID="UPDATED_CAL_DAT" runat="server" Text='<%# Bind("UPDATED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                                        </div>
                                                                        <div>
                                                                            <asp:Label ID="UPDATED_CAL_TZDAT" runat="server" Text='<%# Bind("UPDATED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%"
                                                                ItemStyle-Width="15%">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtndeleteACL" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                        CommandName="DeleteACL" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this field : ", Eval("FIELDNAME")) %>' ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                        </div>
                    </div>
                </div>
                <div class="row" runat="server" id="divOnClickEvent" visible="false">
                    <div class="col-md-6">
                        <div class="box box-primary" style="min-height: 250px;">
                            <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                <h4 class="box-title" align="left">Set OnClick Event </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    <label>
                                                        Select Field :</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlOnClickField" runat="server" CssClass="form-control required3"
                                                        AutoPostBack="True" Style="width: 100%;" OnSelectedIndexChanged="ddlOnClickField_SelectedIndexChanged" TabIndex="42">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    <label>
                                                        Select Listing :</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlOnClickListing" AutoPostBack="true" runat="server" CssClass="form-control required3"
                                                        Style="width: 100%;" OnSelectedIndexChanged="ddlOnClickListing_SelectedIndexChanged" TabIndex="43">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    <label>
                                                        Select Listing Filter :</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlOnClickFilter" runat="server" CssClass="form-control" Style="width: 100%;" TabIndex="44">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    &nbsp;
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:Button ID="btnSubmit_OnClick" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave3"
                                                        OnClick="btnSubmit_OnClick_Click" TabIndex="45" />
                                                    <asp:Button ID="btnUpdate_OnClick" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm" TabIndex="46" />
                                                    <asp:Button ID="btnCancel_OnClick" Text="Cancel" runat="server" CssClass="btn btn-danger btn-sm"
                                                        OnClick="btnCancel_OnClick_Click" TabIndex="47" />
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-primary">
                            <div class="box-header with-border" style="float: left;">
                                <h4 class="box-title" align="left">Records
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div>
                                        <div class="rows">
                                            <div style="width: 100%; height: 215px; overflow: auto;">
                                                <div>
                                                    <asp:GridView ID="grdonClickEvent" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                        Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdonClickEvent_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%"
                                                                ItemStyle-Width="15%">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Eval("FIELDNAME") %>'
                                                                        CommandName="EditOnClick" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Field">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFIELDNAME" runat="server" Text='<%# Eval("FIELDNAME") %>'></asp:Label>
                                                                    <asp:HiddenField ID="hdnID" runat="server" Value='<%# Eval("ID") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Listing">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLISTNAME" runat="server" Text='<%# Eval("LISTNAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Listing Filter">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFIELDNAME_FILTER" runat="server" Text='<%# Eval("FIELDNAME_FILTER") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%"
                                                                ItemStyle-Width="15%">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtndeleteOnClick" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                        CommandName="DeleteOnClick" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this field : ", Eval("FIELDNAME")) %>' ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                        </div>
                    </div>
                </div>
                <div runat="server" id="divOtherListings" visible="false" class="row">
                    <div class="col-md-6">
                        <div class="box box-primary" style="min-height: 300px;">
                            <div class="box-header with-border" style="float: left;">
                                <h4 class="box-title" align="left">Add Other Listings
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Select Field:</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:DropDownList Style="width: 200px;" ID="ddlOtherListingField" runat="server"
                                                    AutoPostBack="true" class="form-control drpControl required5" OnSelectedIndexChanged="ddlOtherListingField_SelectedIndexChanged" TabIndex="48">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div style="width: 100%; height: 301px; overflow: auto;">
                                            <div>
                                                <asp:GridView ID="gvNew" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowDataBound="gvNew_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chk_Sel_Fun" CssClass="chkOtherListing" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Listing">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                                <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Filter">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlFilter" runat="server" CssClass="form-control" Style="width: 90%;">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-1">
                        <div>
                            &nbsp;
                        </div>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <div class="box-body">
                            <div style="min-height: 300px;">
                                <div class="row txtCenter">
                                    <asp:LinkButton ID="lbtnAdd" ForeColor="White" Text="Add" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave5"
                                        OnClientClick="return CheckOtherListingChk();" OnClick="lbtnAdd_Click" TabIndex="49" />
                                </div>
                                <div class="row txtCenter">
                                    &nbsp;
                                </div>
                                <div class="row txtCenter">
                                    <asp:LinkButton ID="lbtnRemove" ForeColor="White" Text="Remove" runat="server"
                                        CssClass="btn btn-primary btn-sm" OnClientClick="return CheckOtherListingChk_Remove();" OnClick="lbtnRemove_Click" TabIndex="50" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div class="box box-primary" style="min-height: 300px;">
                            <div class="box-header with-border" style="float: left;">
                                <h4 class="box-title" align="left">Added Listings
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <div class="row">
                                        <div style="width: 100%; height: 318px; overflow: auto;">
                                            <div>
                                                <asp:GridView ID="gvAdded" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chk_Sel_Fun" CssClass="chkOtherListing_Remove" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Listing">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                                <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
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
                    </div>
                </div>
                <div runat="server" id="divSetPrim" visible="false" class="row">
                    <div class="col-md-6">
                        <div class="box box-primary" style="min-height: 300px;">
                            <div class="box-header with-border" style="float: left;">
                                <h4 class="box-title" align="left">Add Primary Details
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Select Module:</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlPrimModule" runat="server" AutoPostBack="true" class="form-control drpControl width200px"
                                                    OnSelectedIndexChanged="ddlPrimModule_SelectedIndexChanged" TabIndex="51">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div style="width: 96%; height: 301px; overflow: auto;">
                                            <div>
                                                <asp:GridView ID="grdPriamryFields" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chk_Sel_Fun" CssClass="CheckPrimaryDetailsChk" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Field Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                                <asp:Label ID="lblFIELDNAME" runat="server" Text='<%# Bind("FIELDNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-1">
                        <div>
                            &nbsp;
                        </div>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <div class="box-body">
                            <div style="min-height: 300px;">
                                <div class="row txtCenter">
                                    <asp:LinkButton ID="lbtnAddPrimaryFields" ForeColor="White" Text="Add" runat="server"
                                        CssClass="btn btn-primary btn-sm" OnClick="lbtnAddPrimaryFields_Click" OnClientClick="return CheckPrimaryDetailsChk();" TabIndex="52" />
                                </div>
                                <div class="row txtCenter">
                                    &nbsp;
                                </div>
                                <div class="row txtCenter">
                                    <asp:LinkButton ID="lbtnRemovePrimaryFields" ForeColor="White" Text="Remove" runat="server"
                                        CssClass="btn btn-primary btn-sm" OnClick="lbtnRemovePrimaryFields_Click" OnClientClick="return CheckPrimaryDetailsChk_Remove();" TabIndex="53" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div class="box box-primary" style="min-height: 300px;">
                            <div class="box-header with-border" style="float: left;">
                                <h4 class="box-title" align="left">Added Primary Details
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div class="row">
                                        <div style="width: 100%; height: 362px; overflow: auto;">
                                            <div>
                                                <asp:GridView ID="grdAddedPriamryDetails" runat="server" AutoGenerateColumns="False"
                                                    CssClass="table table-bordered table-striped" Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chk_Sel_Fun" CssClass="CheckPrimaryDetailsChk_Remove" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Module Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblModulename" runat="server" Text='<%# Bind("MODULENAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Field Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                                <asp:Label ID="lblName" runat="server" Text='<%# Bind("FIELDNAME") %>'></asp:Label>
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
                    </div>
                </div>
                <div runat="server" id="divColor" visible="false" style="margin-top: 10px;" class="box box-primary">
                    <div class="box-header with-border" style="float: left;">
                        <h4 class="box-title" align="left">Set Color Coding
                        </h4>
                    </div>
                    <div class="box-body">
                        <div align="left" style="margin-left: 5px">
                            <div class="row">
                                <div>
                                    <asp:GridView ID="gvPrimColor" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                        Style="width: 95%; border-collapse: collapse; margin-left: 20px;" OnRowDataBound="gvPrimColor_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Field Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("FIELDNAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOptions" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Condition">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="drpColorCondition1" runat="server" CssClass="form-control" TabIndex="54">
                                                        <asp:ListItem Selected="True" Text="-Select Condition-" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Is Blank" Value="IS NULL"></asp:ListItem>
                                                        <asp:ListItem Text="Is Not Blank" Value="IS NOT NULL"></asp:ListItem>
                                                        <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                        <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                        <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                        <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                        <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                        <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                        <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                                        <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                                        <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                                        <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Answer">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtAns1" runat="server" CssClass="ColorValues form-control" Text='<%# Bind("Ans1") %>' TabIndex="55"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="5%" HeaderText="Or">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="drpColorOr" runat="server" CssClass="form-control" TabIndex="56">
                                                        <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                        <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Condition">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="drpColorCondition2" runat="server" CssClass="form-control" TabIndex="57">
                                                        <asp:ListItem Selected="True" Text="-Select Condition-" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Is Blank" Value="IS NULL"></asp:ListItem>
                                                        <asp:ListItem Text="Is Not Blank" Value="IS NOT NULL"></asp:ListItem>
                                                        <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                        <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                        <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                        <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                        <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                        <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                        <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                                        <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                                        <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                                        <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Answer">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtAns2" runat="server" CssClass="ColorValues form-control" Text='<%# Bind("Ans2") %>' TabIndex="58"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Color">
                                                <ItemTemplate>
                                                    <input type="color" id="ColorBoxPrimColor" name="PrimColor" onchange="set_Color(this);" tabindex="59" />&nbsp;&nbsp;
                                                <asp:HiddenField ID="hfPrimColor" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Button ID="btnSetPrimColor" Text="Submit" Visible="false" runat="server" Style="margin-left: 21%"
                                            CssClass="btn btn-primary btn-sm" OnClick="btnSetPrimColor_Click" TabIndex="60" />
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAddFormula" />
            <asp:PostBackTrigger ControlID="btnAddField" />
            <asp:PostBackTrigger ControlID="btnUpdateField" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
