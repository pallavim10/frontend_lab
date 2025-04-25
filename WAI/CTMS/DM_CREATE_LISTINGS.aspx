<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DM_CREATE_LISTINGS.aspx.cs" Inherits="CTMS.DM_CREATE_LISTINGS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="CommonFunctionsJs/DM/DM_ConfirmMsg.js"></script>
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <script type="text/javascript" src="CommonFunctionsJs/DB/DB_showAuditTrail.js"></script>
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

        $(document).on("click", ".cls-btnSave1", function () {
            var test = "0";

            $('.required1').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "-1" || value == null || value == "-Select-" || value == "--Select--") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                }
                else if (ctrl == "text" || ctrl == "textarea") {
                    if (value == "") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                }
            });

            if (test == "1") {
                return false;
            }
            return true;
        });

        $(document).on("click", ".cls-btnSave2", function () {
            var test = "0";

            $('.required2').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "-1" || value == null || value == "-Select-" || value == "--Select--") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                }
                else if (ctrl == "text" || ctrl == "textarea") {
                    if (value == "") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                }
            });

            if (test == "1") {
                return false;
            }
            return true;
        });

        $(document).on("click", ".cls-btnSave3", function () {
            var test = "0";

            $('.required3').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "-1" || value == "0" || value == null || value == "-Select-" || value == "--Select--") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                }
                else if (ctrl == "text" || ctrl == "textarea") {
                    if (value == "") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                }
            });

            if (test == "1") {
                return false;
            }
            return true;
        });

        $(document).on("click", ".cls-btnSave4", function () {
            var test = "0";

            $('.required4').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "-1" || value == "0" || value == null || value == "-Select-" || value == "--Select--") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                }
                else if (ctrl == "text" || ctrl == "textarea") {
                    if (value == "") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                }
            });

            if (test == "1") {
                return false;
            }
            return true;
        });

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

        function ChangeDIVDM() {
            if ($('#MainContent_chkDM').prop('checked') == true) {
                $('#DivPatientReview').removeClass('disp-none');
                $('#DivAdditional').removeClass('disp-none');
                if (!$('#DivStudyReview').hasClass('disp-none')) {
                    $('#DivStudyReview').removeClass('disp-none');
                }
            }
            else {
                if (!$('#DivPatientReview').hasClass('disp-none')) {
                    $('#DivPatientReview').addClass('disp-none');
                    $('#DivStudyReview').removeClass('disp-none');
                    $('#DivAdditional').hasClass('disp-none');
                    $('#DivAdditional').addClass('disp-none');
                    $("#MainContent_chkTiles").prop("checked", false);
                    $("#MainContent_chkGraphs").prop("checked", false);
                    ChangeDivQuery();
                }
            }
        }


        function ChangeDivQuery() {
            if ($('#MainContent_chkMM').prop('checked') == true) {
                $('#divQueryText').removeClass('disp-none');
                $('#divParent').removeClass('disp-none');
                $('#divAddFuncs').removeClass('disp-none');
                $('#divPivot').removeClass('disp-none');
                $('#DivPatientReview').removeClass('disp-none');
                $('#DivAdditional').removeClass('disp-none');
                $('#DivStudyReview').removeClass('disp-none');

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
                if (!$('#divPivot').hasClass('disp-none')) {
                    $('#divPivot').addClass('disp-none');
                }
                if (!$('#DivPatientReview').hasClass('disp-none')) {
                    $('#DivPatientReview').addClass('disp-none');

                }
                if (!$('#DivStudyReview').hasClass('disp-none')) {
                    $('#DivStudyReview').addClass('disp-none');

                }
                if (!$('#DivAdditional').hasClass('disp-none')) {
                    $('#DivAdditional').addClass('disp-none');
                    $("#MainContent_chkTiles").prop("checked", false);
                    $("#MainContent_chkGraphs").prop("checked", false);
                }
                ChangeDIVDM();
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
    <style type="text/css">
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
    <%--    <script type="text/javascript" src="js/MaxLength.min.js"></script>
    <script type="text/javascript" src="js/MaxLength_Limit.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager11" runat="server">
    </asp:ScriptManager>
    <contenttemplate>
        <div class="box box-warning">
            <div class="box-header">
                <h3 class="box-title">Create Listings
                </h3>
                <div class="pull-right" style="padding-top: 4px; margin-right: 10px;">
                    <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" Visible="false" Text="Export Listing" CssClass="btn btn-info btn-sm" ForeColor="White" Font-Bold="true"> Export Listing&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>

                </div>
            </div>
            <div class="box-body">
                <div class="form-group">
                    <div class="form-group has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        <asp:HiddenField runat="server" ID="hfValues" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="col-md-3" style="width: 133px;">
                                                    <label>Listing Name :</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox Style="width: 360px;" ID="txtListing" ValidationGroup="section" runat="server"
                                                        CssClass="form-control required" TabIndex="1"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="col-md-4">
                                                    <label>Sequence Number :</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtSeqNo" runat="server"
                                                        CssClass="form-control required" TabIndex="2"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="col-md-3" style="width: 133px;">
                                                    <label>
                                                        Same as another :</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="drpSameas" Style="width: 360px;" runat="server" CssClass="form-control" TabIndex="3">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            
                                            <div class="col-md-6">
                                                &nbsp;
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="col-md-3">
                                                    <label>
                                                        Publish To :</label>
                                                </div>
                                                <div class="col-md-5">
                                                    <asp:CheckBox runat="server" onclick="return ChangeDIVDM();" onchange="return ChangeDIVDM();" ToolTip="Select if 'YES'" ID="chkDM" TabIndex="4" />&nbsp;&nbsp;
                                                    <label>
                                                        Data Management</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkIWRS" TabIndex="5" />&nbsp;&nbsp;
                                                    <label>
                                                        IWRS</label>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="col-md-6">
                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkSaftey" TabIndex="6" />&nbsp;&nbsp;
                                                    <label>
                                                        Pharmacovigilance(Log New SAE)</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="col-md-3">
                                                    &nbsp;
                                                </div>
                                                <div class="col-md-5">
                                                    <asp:CheckBox runat="server" onclick="return ChangeDivQuery();" onchange="return ChangeDivQuery();"
                                                        ToolTip="Select if 'YES'" ID="chkMM" TabIndex="7" />&nbsp;&nbsp;
                                                    <label>Medical Monitoring</label>
                                                </div>
                                                <div class="col-md-4">
                                                    &nbsp;
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row disp-none" id="divPivot">
                                            <div class="col-md-6">
                                                <div class="col-md-3">
                                                    <label>Pivot :</label>
                                                </div>
                                                <div class="col-md-5">
                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkTranspose" TabIndex="8" />
                                                </div>
                                                <div class="col-md-4">
                                                    <label>Listing Dashboard :</label>&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkDashboard" TabIndex="9" />
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="col-md-4">
                                                    <label>Enable :</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkQueryReport" TabIndex="10" />&nbsp;&nbsp;
                                                    <label>Query Report</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkCommentReport" TabIndex="11" />&nbsp;&nbsp;
                                                    <label>Comment Report</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6 disp-none" id="DivPatientReview">
                                                <div class="col-md-3">
                                                    <label>Review :</label>
                                                </div>
                                                <div class="col-md-5">
                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkPatientReview" TabIndex="12" />&nbsp;&nbsp;
                                                    <label>
                                                        Subject/Participant Review
                                                    </label>

                                                </div>
                                                <div class="col-md-4 disp-none" id="DivStudyReview">
                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkStudyReview" TabIndex="13" />&nbsp;&nbsp;
                                                    <label>
                                                        Focus Data Review</label>
                                                </div>
                                            </div>
                                            <div class="col-md-6 disp-none" id="DivAdditional">
                                                <div class="col-md-4">
                                                    <label>
                                                        Additional Dashboard :</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkTiles"  TabIndex="14"/>&nbsp;&nbsp;
                                                    <label>
                                                        Tiles</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkGraphs" TabIndex="15" />&nbsp;&nbsp;
                                                    <label>
                                                        Graphs</label>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div id="divAddFuncs" class="disp-none">
                                            <div id="divQueryText" class="disp-none">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-3" style="width: 133px;">
                                                            <label>Query Text :</label>
                                                        </div>
                                                        <div class="col-md-9">
                                                            <asp:TextBox runat="server" ID="txtQueryText" MaxLength="500" Width="910px" Height="50px" TextMode="MultiLine" CssClass="form-control" TabIndex="16"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="col-md-3" style="width: 133px;">
                                                        <label>Select Parent :</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:DropDownList ID="ddlParent" runat="server" CssClass="form-control" TabIndex="17">
                                                            <asp:ListItem Value="Other Listings" Text="Other Listings"></asp:ListItem>
                                                            <asp:ListItem Value="Examinations" Text="Examinations"></asp:ListItem>
                                                            <asp:ListItem Value="Lab Data" Text="Lab Data"></asp:ListItem>
                                                            <asp:ListItem Value="Adverse Events" Text="Adverse Events"></asp:ListItem>
                                                            <asp:ListItem Value="Con-Meds" Text="Con-Meds"></asp:ListItem>
                                                            <asp:ListItem Value="Medical History" Text="Medical History"></asp:ListItem>
                                                            <asp:ListItem Value="Study Conclusion" Text="Study Conclusion"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 disp-none" runat="server" visible="false">
                                                    <div class="col-md-4">
                                                        <label>Additional Functions :</label>
                                                    </div>
                                                    <div class="col-md-8" style="display: inline-flex;">
                                                        <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkManCode" CssClass="disp-none" onclick="return showDropdownList();" TabIndex="18" />&nbsp;&nbsp;
                                                        <label>Manual Coding</label>&nbsp;&nbsp;
                                                    <asp:DropDownList runat="server" ID="drpAutoCode" CssClass="form-control required106 disp-none" TabIndex="19">
                                                        <asp:ListItem Text="--Select--" Value="0">
                                                        </asp:ListItem>
                                                        <asp:ListItem Text="Meddra" Value="Meddra">
                                                        </asp:ListItem>
                                                        <asp:ListItem Text="WHODData" Value="WHODData">
                                                        </asp:ListItem>
                                                    </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-5">
                                                    &nbsp;
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:Button ID="btnAddListing" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                        OnClick="btnAddListing_Click" TabIndex="20" />
                                                    <asp:Button ID="btnUpdateListing" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                        OnClick="btnUpdateListing_Click" TabIndex="21" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btncancel" Text="Cancel" runat="server" CssClass="btn btn-danger btn-sm"
                                                        OnClick="btncancel_Click" TabIndex="22" />
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h4 class="box-title" align="left">Listing Records
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left">
                                    <div>
                                        <div class="rows">
                                            <div class="fixTableHead">
                                                <asp:GridView ID="grdListing" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                                    Style="border-collapse: collapse; width: 99%;" OnRowCommand="grdListing_RowCommand" OnPreRender="grdListing_PreRender" OnRowDataBound="grdListing_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="ID" ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                    CommandName="EditList" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Listing Name" HeaderStyle-CssClass="width200px" ItemStyle-CssClass="width200px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNAME" runat="server" Text='<%# Eval("NAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sequence No" HeaderStyle-CssClass="width200px" ItemStyle-CssClass="width200px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSEQNO" runat="server" Text='<%# Eval("SEQNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Data Management" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDM" runat="server" CommandArgument='<%# Eval("DM") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconDM" runat="server" class="fa fa-check"></i></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="IWRS" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIWRS" runat="server" CommandArgument='<%# Eval("IWRS") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconIWRS" runat="server" class="fa fa-check"></i></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Pharmacovigilance(Log New SAE)" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSaftey" runat="server" CommandArgument='<%# Eval("Saftey") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconSaftey" runat="server" class="fa fa-check"></i></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Medical Monitoring" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMEDICAL" runat="server" CommandArgument='<%# Eval("MEDICAL") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconMEDICAL" runat="server" class="fa fa-check"></i></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('DM_LISTINGS', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none">
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
                                                        <asp:TemplateField ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none">
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
                                                                <asp:LinkButton ID="lbtndeleteSection" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                    CommandName="DeleteList" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this listing : ", Eval("NAME")) %>' ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>&nbsp;&nbsp;
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
        </div>
    </contenttemplate>
</asp:Content>
