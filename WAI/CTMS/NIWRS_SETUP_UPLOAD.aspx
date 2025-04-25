<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NIWRS_SETUP_UPLOAD.aspx.cs" Inherits="CTMS.NIWRS_SETUP_UPLOAD" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="CommonFunctionsJs/IWRS/IWRS_ConfirmMsg.js" type="text/javascript"></script>
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <style type="text/css">
        .select2-container .select2-selection--multiple {
            min-height: 60px;
            width: 300px;
        }
    </style>
    <script type="text/javascript">

        function ConfirmScreening() {
            var checkboxes = document.querySelectorAll('input[type="checkbox"][id*="SCR_Delete"]'); 
            var atLeastOneChecked = false;

            checkboxes.forEach(function (cb) {
                if (cb.checked) {
                    atLeastOneChecked = true;
                }
            });

            if (atLeastOneChecked) {
                return confirm("Are you sure you want to delete this Screening IDs?");
            } else {
                alert("Please select at least one.");
                return false;
            }
        }

        function ConfirmRand() {
            var checkboxes = document.querySelectorAll('input[type="checkbox"][id*="RAND_Delete"]');
            var atLeastOneChecked = false;

            checkboxes.forEach(function (cb) {
                if (cb.checked) {
                    atLeastOneChecked = true;
                }
            });

            if (atLeastOneChecked) {
                return confirm("Are you sure you want to delete this Randomization Lists?");
            } else {
                alert("Please select at least one.");
                return false;
            }
        }

        function ConfirmKits() {
            var checkboxes = document.querySelectorAll('input[type="checkbox"][id*="KIT_Delete"]');
            var atLeastOneChecked = false;

            checkboxes.forEach(function (cb) {
                if (cb.checked) {
                    atLeastOneChecked = true;
                }
            });

            if (atLeastOneChecked) {
                return confirm("Are you sure you want to delete this Kit Lists?");
            } else {
                alert("Please select at least one.");
                return false;
            }
        }
    </script>
    <script type="text/javascript">


        function Check_SCR_All_Delete(element) {

            $('input[type=checkbox][id*=Chek_SCR_Delete]').each(function () {
                // >>this<< refers to specific checkbox

                if ($(element).prop('checked') == true) {
                    $(this).prop('checked', true);
                }
                else {
                    $(this).prop('checked', false);
                }

            });
        }

        function Check_RAND_All_Delete(element) {

            $('input[type=checkbox][id*=Chek_RAND_Delete]').each(function () {
                // >>this<< refers to specific checkbox

                if ($(element).prop('checked') == true) {
                    $(this).prop('checked', true);
                }
                else {
                    $(this).prop('checked', false);
                }

            });
        }

        function Check_KIT_All_Delete(element) {

            $('input[type=checkbox][id*=Chek_KIT_Delete]').each(function () {
                // >>this<< refers to specific checkbox

                if ($(element).prop('checked') == true) {
                    $(this).prop('checked', true);
                }
                else {
                    $(this).prop('checked', false);
                }

            });
        }


    </script>
    <script language="javascript" type="text/javascript">

        function pageLoad() {
            //CalculateRPN();
            $('.select').select2();
            $('.txtDate').each(function (index, element) {
                $(element).pikaday({
                    field: element,
                    
                    format: 'DD-MMM-YYYY',
                   
                    yearRange: [1910, 2050]
                });
            });
        }

       
        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {

                if ($(fileUpload).attr('id') == 'MainContent_fileScreen') {
                    document.getElementById("<%=btnScrCols.ClientID %>").click();
                }
                else if ($(fileUpload).attr('id') == 'MainContent_fileRandom') {
                    document.getElementById("<%=btnRandCols.ClientID %>").click();
                }
                else if ($(fileUpload).attr('id') == 'MainContent_fileKit') {
                    document.getElementById("<%=btnKitCols.ClientID %>").click();
                }
            }
        }

        $(document).on("click", ".cls-btnSave", function () {
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

        $(document).on("click", ".cls-btnSave1", function () {
            var test = "0";

            $('.required1').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "-1" || value == null || value == "-Select-" || value == "--Select--" || value == "0") {
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
                    if (value == "-1" || value == null || value == "-Select-" || value == "--Select--" || value == "0") {
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
            <h3 class="box-title">Master Uploads</h3>
        </div>
        <div class="form-group">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                <asp:HiddenField ID="ActiveTabID" runat="server" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="box" style="min-height: 300px;">
                <div id="tabscontainer1" class="nav-tabs-custom" runat="server">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs">
                            <li id="li1_1" runat="server" class="active"><a href="#tab1-1"  data-toggle="tab" onclick="setActiveTab('tab1-1')">Upload Screening IDs</a></li>
                            <li id="li1_2" runat="server"><a href="#tab1-2" data-toggle="tab" onclick="setActiveTab('tab1-2')">View Screening IDs</a></li>
                        </ul>

                        <div class="tab">
                            <div id="tab1-1" class="tab-content active" style="display: block;">
                                <div class="form-group">
                                    <div class="box-body">
                                        <div align="left" style="margin-left: 5px">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="col-md-6">
                                                        <label>
                                                            Select File :</label>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <asp:FileUpload runat="server" ID="fileScreen" />
                                                        <asp:RegularExpressionValidator ID="RegexValidator" runat="server"
                                                            ControlToValidate="fileScreen"
                                                            ValidationExpression="^.*\.(xlsx|xls|csv)$"
                                                            ErrorMessage="Please upload a valid Excel file (.xlsx|.xls|.csv)"
                                                            Display="Dynamic"
                                                            ForeColor="Red" />
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Button ID="btnScrCols" runat="server" CssClass="disp-none" OnClick="btnScrCols_Click" />
                                                </div>
                                                <div class="pull-right" style="padding-top: 4px;">
                                                    <asp:LinkButton ID="lbtnExportUploadScreening" ForeColor="White" runat="server" Text="Export Screening IDs" CssClass="btn btn-info" OnClick="lbtnExportUploadScreening_Click">Export Screening IDs &nbsp;&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="col-md-6">
                                                        <label>
                                                            Select Country Column :</label>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <asp:DropDownList runat="server" ID="drpScrCountryID" CssClass="form-control width200px required">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="col-md-6">
                                                        <label>
                                                            Select Site ID Column :</label>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <asp:DropDownList runat="server" ID="drpScrSiteID" CssClass="form-control width200px required">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="col-md-6">
                                                        <label>
                                                            Select Sub-Site ID Column :</label>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <asp:DropDownList runat="server" ID="drpScrSubSiteID" CssClass="form-control width200px">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="col-md-6">
                                                        <label>
                                                            Select Screening ID Column :</label>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <asp:DropDownList runat="server" ID="drpScrID" CssClass="form-control width200px required">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="col-md-6">
                                                        <label>
                                                            Select Parent ID Column :</label>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <asp:DropDownList runat="server" ID="ddlScrIDENT" CssClass="form-control width200px">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="col-md-6">
                                                        &nbsp;
                                                    </div>
                                                    <div class="col-md-5">
                                                        &nbsp;
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <br />
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        &nbsp;
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Button ID="btnScrUpload" Text="Upload" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                            OnClick="btnScrUpload_Click" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Button ID="btnScrCancel" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                            OnClick="btnScrCancel_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="tab1-2" class="tab-content">
                                <div class="form-group">
                                    <div class="rows" style="margin-top: 5px;">
                                        <div>
                                            <asp:GridView ID="grd_Data" runat="server" AutoGenerateColumns="false" Width="100%"
                                                CssClass="table table-bordered table-striped Datatable txtCenter" OnPreRender="grd_Data_PreRender">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="false" ItemStyle-CssClass="width50px">
                                                        <HeaderTemplate>
                                                            <asp:Button ID="lblDelete" runat="server" CssClass="btn btn-primary btn-sm" OnClientClick="return ConfirmScreening();"
                                                                Text="Delete" OnClick="lblDelete_Click" />
                                                            <br />
                                                            <asp:CheckBox ID="ChekAll_Delete" runat="server" onclick="Check_SCR_All_Delete(this)" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="Chek_SCR_Delete" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Country">
                                                        <ItemTemplate>
                                                            <asp:Label ID="COUNTRY" runat="server" Text='<%# Bind("COUNTRY") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Site ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SITEID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sub-Site ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SUBSITEID" runat="server" Text='<%# Bind("SUBSITEID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Subject ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SUBJID" runat="server" Text='<%# Bind("SUBJID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Parent ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Parent_ID" runat="server" Text='<%# Bind("Parent_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <label>Entered Details</label><br />
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
                                                                    <asp:Label ID="ENTERED_CAL_TZDAT" runat="server" Text='<%# Eval("ENTERED_CAL_TZDAT") + " "+ Eval("ENTERED_TZVAL") %>' ForeColor="Red"></asp:Label>
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
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="box" style="min-height: 300px;">
                <div id="Div1" class="nav-tabs-custom" runat="server">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs">
                            <li id="li1" runat="server" class="active"><a href="#tab2-1" data-toggle="tab" onclick="setActiveTab('tab2-1')">Upload Randomization List </a></li>
                            <li id="li2" runat="server"><a href="#tab2-2" data-toggle="tab" onclick="setActiveTab('tab2-2')">View Randomization List</a></li>
                        </ul>
                        <div class="tab">
                            <div id="tab2-1" class="tab-content active" style="display: block;">
                                <div class="box-body">
                                    <div align="left" style="margin-left: 5px">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="col-md-6">
                                                    <label>
                                                        Select File :</label>
                                                </div>
                                                <div class="col-md-5">
                                                    <asp:FileUpload runat="server" ID="fileRandom" />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                        ControlToValidate="fileRandom"
                                                        ValidationExpression="^.*\.(xlsx|xls|csv)$"
                                                        ErrorMessage="Please upload a valid Excel file (.xlsx|.xls|.csv)"
                                                        Display="Dynamic"
                                                        ForeColor="Red" />
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Button ID="btnRandCols" runat="server" CssClass="disp-none" OnClick="btnRandCols_Click" />
                                            </div>
                                            <div class="pull-right" style="padding-top: 4px;">
                                                <asp:LinkButton ID="lnkRandomizationList" runat="server" Text="Export Randomization List" CssClass="btn btn-info" ForeColor="White" OnClick="lnkRandomizationList_Click">Export Randomization List&nbsp;&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                                            </div>
                                        </div>
                                        <br />
                                        <br />
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="col-md-6">
                                                    <label>
                                                        Select Sequence No. Column :</label>
                                                </div>
                                                <div class="col-md-5">
                                                    <asp:DropDownList runat="server" ID="drpRandSEQNO" CssClass="form-control width200px required1">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="col-md-6">
                                                    <label>
                                                        Select Block Column :</label>
                                                </div>
                                                <div class="col-md-5">
                                                    <asp:DropDownList runat="server" ID="drpRandBlock" CssClass="form-control width200px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="col-md-6">
                                                    <label>
                                                        Select Treatment Code Column :</label>
                                                </div>
                                                <div class="col-md-5">
                                                    <asp:DropDownList runat="server" ID="drpRandTrtGrp" CssClass="form-control width200px required1">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="col-md-6">
                                                    <label>
                                                        Select Treatment Arm Column :</label>
                                                </div>
                                                <div class="col-md-5">
                                                    <asp:DropDownList runat="server" ID="drpRandTrtGrpNm" CssClass="form-control width200px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="col-md-6">
                                                    <label>
                                                        Select STRATA Code Column :</label>
                                                </div>
                                                <div class="col-md-5">
                                                    <asp:DropDownList runat="server" ID="drpRandStrata" CssClass="form-control width200px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="col-md-6">
                                                    <label>
                                                        Select STRATA Value Column :</label>
                                                </div>
                                                <div class="col-md-5">
                                                    <asp:DropDownList runat="server" ID="drpRandStrataL" CssClass="form-control width200px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="col-md-6">
                                                    <label>
                                                        Select Randmization No. Column :</label>
                                                </div>
                                                <div class="col-md-5">
                                                    <asp:DropDownList runat="server" ID="drpRandNo" CssClass="form-control width200px required1">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                &nbsp;
                                            </div>
                                        </div>
                                        <br />
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    &nbsp;
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Button ID="btnRandUpload" Text="Upload" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave1"
                                                        OnClick="btnRandUpload_Click" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnRandCancel" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                        OnClick="btnRandCancel_Click" />
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <div id="tab2-2" class="tab-content">
                                <div class="form-group">
                                    <div class="rows" style="margin-top: 5px;">
                                        <div>
                                            <asp:GridView ID="grd_data_rand" runat="server" AutoGenerateColumns="false" Width="100%"
                                                CssClass="table table-bordered table-striped Datatable txtCenter" OnPreRender="grd_Data_PreRender">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="false" ItemStyle-CssClass="width50px">
                                                        <HeaderTemplate>
                                                            <asp:Button ID="lblRandDelete" runat="server" CssClass="btn btn-primary btn-sm" OnClientClick="return ConfirmRand();"
                                                                Text="Delete" OnClick="lblRandDelete_Click"/>
                                                            <br />
                                                            <asp:CheckBox ID="ChekAll_Delete" runat="server" onclick="Check_RAND_All_Delete(this)" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="Chek_RAND_Delete" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sequence No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SEQNO" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Block">
                                                        <ItemTemplate>
                                                            <asp:Label ID="BLOCK" runat="server" Text='<%# Bind("BLOCK") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Treatment Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="TREAT_GRP" runat="server" Text='<%# Bind("TREAT_GRP") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Treatment Arm">
                                                        <ItemTemplate>
                                                            <asp:Label ID="TREAT_GRP_NAME" runat="server" Text='<%# Bind("TREAT_GRP_NAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="STRATA Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="STRATA" runat="server" Text='<%# Bind("STRATA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="STRATA Value">
                                                        <ItemTemplate>
                                                            <asp:Label ID="STRATAL" runat="server" Text='<%# Bind("STRATAL") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Randmization No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="RANDNO" runat="server" Text='<%# Bind("RANDNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <label>Entered Details</label><br />
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
                                                                    <asp:Label ID="ENTERED_CAL_TZDAT" runat="server" Text='<%# Eval("ENTERED_CAL_TZDAT") + " "+ Eval("ENTERED_TZVAL") %>' ForeColor="Red"></asp:Label>
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
                </div>
                <br />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="box" style="min-height: 300px;">
                <div id="Div2" class="nav-tabs-custom" runat="server">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs">
                            <li id="li3" runat="server" class="active"><a href="#tab3-1" data-toggle="tab" onclick="setActiveTab('tab3-1')">Upload Kits List </a></li>
                            <li id="li4" runat="server"><a href="#tab3-2" data-toggle="tab" onclick="setActiveTab('tab3-2')">View Kits List</a></li>
                        </ul>
                        <div class="tab">
                            <div id="tab3-1" class="tab-content active" style="display: block;">
                                <div class="box-body">
                                    <div align="left" style="margin-left: 5px">
                                        <div class="row">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="col-md-6">
                                                        <label>
                                                            Select File :</label>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <asp:FileUpload runat="server" ID="fileKit" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                            ControlToValidate="fileKit"
                                                            ValidationExpression="^.*\.(xlsx|xls|csv)$"
                                                            ErrorMessage="Please upload a valid Excel file (.xlsx|.xls|.csv)"
                                                            Display="Dynamic"
                                                            ForeColor="Red" />
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Button ID="btnKitCols" runat="server" CssClass="disp-none" OnClick="btnKitCols_Click" />
                                                    <div class="pull-right" style="padding-top: 4px;">
                                                    <asp:LinkButton ID="lnkKitList" runat="server" Text="Export Kit List" CssClass="btn btn-info" ForeColor="White" OnClick="lnkKitList_Click">Export Kit List&nbsp;&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                                                </div>
                                                </div>
                                                
                                            </div>
                                            <br />
                                            <br />
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="col-md-6">
                                                        <label>
                                                            Select Sequence No. Column :</label>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <asp:DropDownList runat="server" ID="drpKitSeq" CssClass="form-control width200px required2">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="col-md-6">
                                                        <label>
                                                            Select Kit No. Column :</label>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <asp:DropDownList runat="server" ID="drpKitNo" CssClass="form-control width200px required2">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="col-md-6">
                                                        <label>
                                                            Select Treatment Code Column :</label>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <asp:DropDownList runat="server" ID="drpKitTrtGrp" CssClass="form-control width200px required2">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="col-md-6">
                                                        <label>
                                                            Select Treatment Arm Column :</label>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <asp:DropDownList runat="server" ID="drpKitTrtGrpNm" CssClass="form-control width200px required2">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="col-md-6">
                                                        <label>
                                                            Select Treatment Strength Column :</label>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <asp:DropDownList runat="server" ID="drpKitTrtStr" CssClass="form-control width200px">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="col-md-6">
                                                        <label>
                                                            Select Expiry Date Column :</label>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <asp:DropDownList runat="server" ID="drpKitExpDt" CssClass="form-control width200px required2">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="col-md-6">
                                                        <label>
                                                            Select Lot No. :</label>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <asp:DropDownList runat="server" ID="drpKitLotNo" CssClass="form-control width200px">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 disp-none" >
                                                    <div class="col-md-6 disp-none">
                                                        <label>
                                                            Select Blinded Expiry Date Column :</label>
                                                    </div>
                                                    <div class="col-md-5 disp-none">
                                                        <asp:DropDownList runat="server" ID="drpblExpDt" CssClass="form-control width200px">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-6">
                                                    &nbsp;
                                                </div>
                                            </div>
                                            <br />
                                            <br />
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        &nbsp;
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Button ID="btnKitUpload" Text="Upload" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave2"
                                                            OnClick="btnKitUpload_Click" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Button ID="btnKitCancel" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                        OnClick="btnKitCancel_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="tab3-2" class="tab-content">
                                <div class="form-group">
                                    <div class="rows" style="margin-top: 5px;">
                                        <div>
                                            <asp:GridView ID="grd_data_kits" runat="server" AutoGenerateColumns="false" Width="100%"
                                                CssClass="table table-bordered table-striped Datatable txtCenter" OnPreRender="grd_Data_PreRender">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="false" ItemStyle-CssClass="width50px">
                                                        <HeaderTemplate> 
                                                            <asp:Button ID="lblKITDelete" runat="server" CssClass="btn btn-primary btn-sm" OnClientClick="return ConfirmKits();"
                                                                Text="Delete" OnClick="lblKITDelete_Click" />
                                                            <br />
                                                            <asp:CheckBox ID="ChekAll_Delete" runat="server" onclick="Check_KIT_All_Delete(this)" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="Chek_KIT_Delete" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sequence No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SEQNO" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kit No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="KITNO" runat="server" Text='<%# Bind("KITNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Treatment Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="TREAT_GRP" runat="server" Text='<%# Bind("TREAT_GRP") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Treatment Arm">
                                                        <ItemTemplate>
                                                            <asp:Label ID="TREAT_GRP_NAME" runat="server" Text='<%# Bind("TREAT_GRP_NAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Treatment Strength">
                                                        <ItemTemplate>
                                                            <asp:Label ID="TREAT_STRENGTH" runat="server" Text='<%# Bind("TREAT_STRENGTH") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Expiry Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="EXPIRY_DATE" runat="server" Text='<%# Bind("EXPIRY_DATE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Lot No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LOTNO" runat="server" Text='<%# Bind("LOTNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <label>Entered Details</label><br />
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
                                                                    <asp:Label ID="ENTERED_CAL_TZDAT" runat="server" Text='<%# Eval("ENTERED_CAL_TZDAT") + " "+ Eval("ENTERED_TZVAL") %>' ForeColor="Red"></asp:Label>
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
                </div>
            </div>
        </div>
        <br />
    </div>
</asp:Content>
