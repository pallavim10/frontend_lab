<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="eTMF_UPLOADDOCS_Site.aspx.cs" Inherits="CTMS.eTMF_UPLOADDOCS_Site" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function DisableButtons() {
            var inputs = document.getElementsByTagName("INPUT");
            for (var i in inputs) {
                if (inputs[i].type == "button" || inputs[i].type == "submit") {
                    inputs[i].disabled = true;
                }
            }
        }
        window.onbeforeunload = DisableButtons;
    </script>
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <style type="text/css">
        .select2-selection select2-selection--single
        {
            height: 20px;
        }
    </style>
    <script>
        function pageLoad() {
            //CalculateRPN();
            $('.select').select2();

        }

        $(function () {

            $('.select2').select2()
        });


        $(document).on("click", ".cls-btnSave", function () {
            var test = "0";

            $('.required').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "0" || value == "--Select--" || value == null) {
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
    <script>

        function bindSpecs() {
            var avaTag = $('#MainContent_hfSPECS').val().split(',');

            $('#MainContent_txtSPEC').autocomplete({
                source: avaTag, minLength: 0
            }).on('focus', function () { $(this).keydown(); });
        }

        $(function () {
            var avaTag = $('#MainContent_hfSPECS').val().split(',');

            $('#MainContent_txtSPEC').autocomplete({
                source: avaTag, minLength: 0
            }).on('focus', function () { $(this).keydown(); });
        });

        function ConfirmMsg() {
            var newLine = "\r\n"

            var error_msg = "This File is already exists";

            error_msg += newLine;
            error_msg += newLine;

            error_msg += "Note : Press OK to Proceed.";

            if (confirm(error_msg)) {

                $("#MainContent_btnUploadAgainDoc").click();

                return true;

            } else {

                return false;
            }

        }

        function SetEmailYes(element) {

            $('#MainContent_lbtnSendEmailYes').addClass("disp-none");
            $('#MainContent_lbtnSendEmailNO').removeClass("disp-none");
            $('#MainContent_divEmail').removeClass("disp-none");

            $('#MainContent_txtToEmailIds').val("");
            $('#MainContent_txtCcEmailIds').val("");

            return false;
        }

        function SetEmailNo(element) {

            $('#MainContent_lbtnSendEmailYes').removeClass("disp-none");
            $('#MainContent_lbtnSendEmailNO').addClass("disp-none");
            $('#MainContent_divEmail').addClass("disp-none");

            $('#MainContent_txtToEmailIds').val("");
            $('#MainContent_txtCcEmailIds').val("");

            return false;
        }

        function DOC_TRACKING(element) {

            var ID = $(element).closest('tr').find('td:eq(0)').find('span').text();

            var test = "eTMF_DOCUMENT_TRACKING.aspx?ID=" + ID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=300,width=600";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function VERSION_HISTORY(element) {

            var ID = $(element).closest('tr').find('td:eq(0)').find('span').text();

            var test = "eTMF_VERSION_HISTORY.aspx?ID=" + ID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=300,width=1200";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function ShowReminderDiv(element) {

            if ($('#divreminderdat').hasClass("disp-none")) {

                $('#divreminderdat').removeClass("disp-none");

            }
            else {

                $('#divreminderdat').addClass("disp-none");

            }

            $('#MainContent_txtReminderDat').val("");

            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Upload Documents</h3>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                <asp:HiddenField runat="server" ID="hfVerDATE" />
                <asp:HiddenField runat="server" ID="hfVerSPEC" />
                <asp:HiddenField runat="server" ID="hfSPECS" />
                <asp:HiddenField runat="server" ID="hfVerTYPE" />
                <asp:HiddenField runat="server" ID="hfOwnerEmailId" />
            </div>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="row" style="display: none;">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Upload Using : &nbsp;
                            <asp:Label ID="Label7" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlAction" runat="server" class="form-control drpControl required width250px"
                                OnSelectedIndexChanged="ddlAction_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                <asp:ListItem Value="1" Selected="True" Text="Unknown"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div id="divforeTMF" runat="server" visible="false">
                    <div class="row" id="div1" runat="server" style="padding-top: 15px;">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Select Structure :
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="drpDocType" runat="server" CssClass="form-control width250px"
                                    OnSelectedIndexChanged="drpDocType_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="label col-md-2">
                                Select Zones :
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-control width250px"
                                    OnSelectedIndexChanged="ddlZones_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="div3" runat="server" style="padding-top: 15px;">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Select Sections :
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlSections" runat="server" CssClass="form-control width250px"
                                    OnSelectedIndexChanged="ddlSections_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="label col-md-2">
                                Select Artifacts :
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlArtifacts" runat="server" CssClass="form-control width250px"
                                    OnSelectedIndexChanged="ddlArtifacts_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divfortask" runat="server" visible="false">
                    <div class="row" id="divdepartment" runat="server" style="padding-top: 15px;">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Select Department :
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddldepartment" runat="server" CssClass="form-control width250px"
                                    OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="label col-md-2">
                                Select Task :
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlTask" runat="server" CssClass="form-control width250px"
                                    OnSelectedIndexChanged="ddlTask_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="divsubtask" runat="server" style="padding-top: 15px;">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Select Sub Task :
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlSubTask" runat="server" CssClass="form-control width250px"
                                    OnSelectedIndexChanged="ddlSubTask_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" id="div5" runat="server" style="padding-top: 15px;">
                    <div class="col-md-12">
                        <div id="divdocument" runat="server" visible="false">
                            <div class="label col-md-2">
                                Select Documents :
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList runat="server" ID="drpDocument" CssClass="form-control width250px required"
                                    OnSelectedIndexChanged="drpDocument_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div id="divstatus" runat="server" visible="false">
                            <div class="label col-md-2">
                                Select Status : &nbsp;
                                <asp:Label ID="Label10" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList runat="server" ID="drpAction" AutoPostBack="true" CssClass="form-control required width250px"
                                    OnSelectedIndexChanged="drpAction_SelectedIndexChanged">
                                    <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                    <%--<asp:ListItem Text="Draft" Value="Draft"></asp:ListItem>--%>
                                    <asp:ListItem Text="Final" Value="Final"></asp:ListItem>
                                    <asp:ListItem Text="Obsolete" Value="Obsolete"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" id="divUploaded" visible="false" runat="server" style="padding-top: 15px;">
                    <div class="col-md-12">
                        <div id="div6" runat="server">
                            <div class="label col-md-2">
                            </div>
                            <div class="col-md-5" align="right">
                                <a href="#MainContent_gvFilesUploaded">
                                    <asp:Label runat="server" ID="lblUploaded"></asp:Label>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" visible="false" id="divInstruction" runat="server" style="padding-top: 15px;">
                    <div class="col-md-12">
                        <div id="div9" runat="server">
                            <div class="label col-md-2">
                                Instructions :
                            </div>
                            <div class="col-md-8">
                                <asp:Label runat="server" ID="lblInstruction" TextMode="MultiLine" Width="721px"
                                    Font-Bold="true" Style="height: auto; max-height: 100px; color: Blue;">
                                </asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" style="padding-top: 15px;">
                    <div class="col-md-12">
                        <div id="divCountry" runat="server" visible="false">
                            <div class="label col-md-2">
                                Select Country : &nbsp;
                                <asp:Label ID="Label13" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList runat="server" ID="drpCountry" AutoPostBack="true" CssClass="form-control width250px"
                                    OnSelectedIndexChanged="drpCountry_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div id="divINVID" runat="server" visible="false">
                            <div class="label col-md-2">
                                Site ID : &nbsp;
                                <asp:Label ID="Label14" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList runat="server" ID="drpSites" CssClass="form-control width250px"
                                    AutoPostBack="true" OnSelectedIndexChanged="drpSites_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" style="padding-top: 15px;">
                    <div class="col-md-12">
                        <div id="divIndividual" runat="server" visible="false">
                            <div class="label col-md-2">
                                Individual : &nbsp;
                                <asp:Label ID="lbl44" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList runat="server" ID="ddlIndividual" CssClass="form-control required width250px select2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div id="divSubject" runat="server" visible="false">
                            <div class="label col-md-2">
                                Subject : &nbsp;
                                <asp:Label ID="Label9" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox runat="server" ID="txtSubject" CssClass="form-control required width250px">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div id="divSPEC" runat="server" visible="false">
                            <div class="label col-md-2">
                                <asp:Label runat="server" ID="lblSPECtitle"></asp:Label>
                                &nbsp;: &nbsp;
                                <asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox runat="server" ID="txtSPEC" CssClass="form-control required width250px">
                                </asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divDefaultView" runat="server" visible="false">
                    <div class="row" id="div7" runat="server" style="padding-top: 15px;">
                        <div class="col-md-12">
                            <div id="divStatus2" visible="false" runat="server">
                                <div class="label col-md-2">
                                    Select Sub-Status :&nbsp;
                                    <asp:Label ID="Label6" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList runat="server" ID="ddlFinalStatus" CssClass="form-control width250px"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlFinalStatus_SelectedIndexChanged">
                                        <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="None" Value="None"></asp:ListItem>
                                        <asp:ListItem Text="Collaborate" Value="Collaborate"></asp:ListItem>
                                        <asp:ListItem Text="Review" Value="Review"></asp:ListItem>
                                        <asp:ListItem Text="QA Review" Value="QA Review"></asp:ListItem>
                                        <asp:ListItem Text="Internal Approval" Value="Internal Approval"></asp:ListItem>
                                        <asp:ListItem Text="Sponsor Approval" Value="Sponsor Approval"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div id="divStatus3" visible="false" runat="server">
                                <div class="label col-md-2">
                                    Select Action : &nbsp;
                                    <asp:Label ID="Label5" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList runat="server" ID="ddlStatus3" CssClass="form-control width250px"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlStatus3_SelectedIndexChanged">
                                        <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Current" Value="Current"></asp:ListItem>
                                        <asp:ListItem Text="Replace" Value="Replace"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="padding-top: 15px;">
                        <div class="col-md-12">
                            <div id="divUsers" visible="false" runat="server">
                                <div class="label col-md-2">
                                    Select Users : &nbsp;
                                    <asp:Label ID="Label12" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:GridView ID="grd_Users" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                        CssClass="table table-bordered table-striped table-striped1 width250px">
                                        <Columns>
                                            <asp:TemplateField HeaderText="User_ID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                <ItemTemplate>
                                                    <asp:Label ID="User_ID" Text='<%# Eval("User_ID") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EMAILID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                <ItemTemplate>
                                                    <asp:Label ID="EMAILID" Text='<%# Eval("EMAILID") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="User_Name" Text='<%# Eval("User_Name") %>' ToolTip='<%# Eval("User_Name") %>'
                                                        runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div id="divDeadline" visible="false" runat="server">
                                <div class="label col-md-2">
                                    Enter Deadline Date (if Applicable) : &nbsp;
                                    <asp:Label ID="Label11" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox runat="server" ID="txtDeadline" CssClass="form-control txtDate width250px"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="divFileReplace" visible="false" runat="server" style="padding-top: 15px;">
                        <div class="col-md-6">
                            <div class="label col-md-2">
                                Select File : &nbsp;
                                <asp:Label ID="Label4" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:GridView ID="gvFiles" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                    CssClass="table table-bordered table-striped table-striped1">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                            <ItemTemplate>
                                                <asp:Label ID="ID" Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="File Name">
                                            <ItemTemplate>
                                                <asp:Label ID="UploadFileName" Text='<%# Eval("UploadFileName") %>' ToolTip='<%# Eval("UploadFileName") %>'
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Version No.">
                                            <ItemTemplate>
                                                <asp:Label ID="DOC_VERSIONNO" Text='<%# Eval("DOC_VERSIONNO") %>' ToolTip='<%# Eval("DOC_VERSIONNO") %>'
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Document Date">
                                            <ItemTemplate>
                                                <asp:Label ID="DOC_DATETIME" Text='<%# Eval("DOC_DATETIME") %>' ToolTip='<%# Eval("DOC_DATETIME") %>'
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Note">
                                            <ItemTemplate>
                                                <asp:Label ID="NOTE" Text='<%# Eval("NOTE") %>' ToolTip='<%# Eval("NOTE") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="div8" runat="server" style="padding-top: 15px;">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Document Version Number: &nbsp;
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox runat="server" ID="txtDovVersionNo" CssClass="form-control width250px"></asp:TextBox>
                            </div>
                            <div class="label col-md-2">
                                <asp:Label runat="server" ID="lblDateTitle" Text="Select Document Date"></asp:Label>
                                &nbsp; : &nbsp;
                                <asp:Label ID="lblRequiredDocDate" runat="server" Font-Size="Small" ForeColor="#FF3300"
                                    Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox runat="server" ID="txtDocDateTime" CssClass="form-control txtDateNoFuture width250px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="div13" runat="server" style="padding-top: 15px;">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Note : &nbsp;
                                <asp:Label ID="Label8" runat="server" Font-Size="Small" ForeColor="#FF3300" Text=""></asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtNote" TextMode="MultiLine" CssClass="form-control"
                                    Height="50" Width="721"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="divFunction" runat="server" style="padding-top: 15px;">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Function : &nbsp;
                                <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList runat="server" ID="ddlFunction" CssClass="form-control required width250px">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="div11" runat="server" style="padding-top: 15px;">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Enter Expiry Date (if Applicable) : &nbsp;
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox runat="server" ID="txtExpiryDate" CssClass="form-control txtDate width250px"
                                    onchange="return ShowReminderDiv(this);"></asp:TextBox>
                            </div>
                            <div id="divreminderdat" class="disp-none">
                                <div class="label col-md-2">
                                    Set Reimnder Date : &nbsp;
                                    <asp:Label ID="Label15" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox runat="server" ID="txtReminderDat" CssClass="form-control txtDateNoFuture width250px"></asp:TextBox>
                                </div>
                            </div>
                            <asp:HiddenField ID="hdnEmailEnable" runat="server" />
                        </div>
                    </div>
                    <div class="row" id="div2" runat="server" style="padding-top: 15px;">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Select File : &nbsp;
                                <asp:Label ID="Label16" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="width250px" Font-Size="X-Small" />
                                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="filePDF"
                                    ErrorMessage="Only PDF files are allowed!" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF)$"
                                    ControlToValidate="FileUpload1" CssClass="text-red"></asp:RegularExpressionValidator>--%>
                            </div>
                            <div class="disp-none label col-md-2">
                                Select File : &nbsp;
                                <asp:Label ID="Label3" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="disp-none col-md-3">
                                <asp:FileUpload ID="FileUpload2" runat="server" CssClass="width250px" Font-Size="X-Small" />
                                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationGroup="filePDF"
                                    ErrorMessage="Only Word,Excel,Image and Power-Point files are allowed!" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.doc|.docx|.png|.jpg|.txt|.xls|.xlsx|.ppt|.pptx)$"
                                    ControlToValidate="FileUpload2" CssClass="text-red"></asp:RegularExpressionValidator>--%>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="div4" runat="server" style="padding-top: 15px;">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Send Email : &nbsp;
                                <asp:Label ID="Label17" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:LinkButton ID="lbtnSendEmailYes" OnClientClick="return SetEmailYes(this);" runat="server">
                                    <i title="Mark as Yes" class="icon-check-empty" style="color: #333333;" aria-hidden="true"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="lbtnSendEmailNO" OnClientClick="return SetEmailNo(this);" runat="server"
                                    CssClass="disp-none">
                                    <i title="Mark as No" class="icon-check" style="color: #333333;" aria-hidden="true"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="row disp-none" id="divEmail" runat="server" style="padding-top: 15px;">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                To Email IDs (Use ',' in case of multiple) : &nbsp;
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox runat="server" ID="txtToEmailIds" TextMode="MultiLine" CssClass="form-control width250px"></asp:TextBox>
                            </div>
                            <div class="label col-md-2">
                                Cc Email IDs (Use ',' in case of multiple) : &nbsp;
                                <asp:Label ID="Label18" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox runat="server" ID="txtCcEmailIds" TextMode="MultiLine" CssClass="form-control width250px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row" id="div12" runat="server" style="padding-top: 15px;">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                            </div>
                            <div class="col-md-3" align="right">
                                <asp:Button ID="btnUpload" runat="server" Text="Upload" ValidationGroup="filePDF"
                                    CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnUpload_Click" />
                                <asp:Button ID="btnUploadAgainDoc" runat="server" CssClass="disp-none" OnClick="btnUploadAgainDoc_Click">
                                </asp:Button>
                            </div>
                        </div>
                    </div>
                    <br />
                </div>
            </div>
        </div>
        <div class="box box-warning" runat="server" id="divUpoadedDocs" visible="false">
            <div class="box-header">
                <h3 class="box-title">
                    Uploaded Document</h3>
            </div>
            <asp:GridView ID="gvFilesUploaded" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                EmptyDataText="No Record Available." CssClass="table table-bordered table-striped layerFiles Datatable"
                OnPreRender="grd_data_PreRender" OnRowDataBound="gvFilesUploaded_RowDataBound"
                OnRowCommand="gvFilesUploaded_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                        ItemStyle-CssClass="disp-none" HeaderText="ID">
                        <ItemTemplate>
                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                        ItemStyle-CssClass="disp-none" HeaderText="SysFileName">
                        <ItemTemplate>
                            <asp:Label ID="SysFileName" runat="server" Text='<%# Bind("SysFileName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Country">
                        <ItemTemplate>
                            <asp:Label ID="COUNTRYNAME" Width="100%" ToolTip='<%# Bind("COUNTRYNAME") %>' CssClass="label"
                                runat="server" Text='<%# Bind("COUNTRYNAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SIte Id">
                        <ItemTemplate>
                            <asp:Label ID="SiteID" Width="100%" ToolTip='<%# Bind("SiteID") %>' CssClass="label"
                                runat="server" Text='<%# Bind("SiteID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Spec.">
                        <ItemTemplate>
                            <asp:Label ID="Spec" Width="100%" ToolTip='<%# Bind("SPEC_CONCAT") %>' CssClass="label"
                                runat="server" Text='<%# Bind("SPEC_CONCAT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                        ItemStyle-CssClass="disp-none">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Name" Width="100%" ToolTip='<%# Bind("AutoNomenclature") %>' CssClass="label"
                                runat="server" Text='<%# Bind("AutoNomenclature") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="File Name">
                        <ItemTemplate>
                            <asp:Label ID="lbl_UploadFileName" Width="100%" ToolTip='<%# Bind("UploadFileName") %>'
                                CssClass="label" runat="server" Text='<%# Bind("UploadFileName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="File Type" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="lbtnFileType" runat="server" Font-Size="Larger"><i id="ICONCLASS"
                                runat="server" class="fas fa-file-text"></i></asp:Label>
                            <asp:Label ID="lbl_FileSize" Width="100%" CssClass="label" Font-Size="X-Small" runat="server" Text='<%# Bind("CAL_Size") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="System Version">
                        <ItemTemplate>
                            <asp:Label ID="lbl_VersionID" Width="100%" ToolTip='<%# Bind("VersionID") %>' CssClass="label"
                                runat="server" Text='<%# Bind("VersionID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Status" Width="100%" ToolTip='<%# Bind("Status") %>' CssClass="label"
                                runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Document Version" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="DOC_VERSIONNO" Width="100%" ToolTip='<%# Bind("DOC_VERSIONNO") %>'
                                CssClass="label" runat="server" Text='<%# Bind("DOC_VERSIONNO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Document Date" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="DOC_DATETIME" Width="100%" ToolTip='<%# Bind("DOC_DATETIME") %>' CssClass="label"
                                runat="server" Text='<%# Bind("DOC_DATETIME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Note" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="NOTE" Width="100%" ToolTip='<%# Bind("NOTE") %>' CssClass="label"
                                runat="server" Text='<%# Bind("NOTE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Uploaded By" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="lbl_User" Width="100%" ToolTip='<%# Bind("User") %>' CssClass="label"
                                runat="server" Text='<%# Bind("User") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Uploaded DateTime" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="lbl_UploadDateTime" Width="100%" ToolTip='<%# Bind("UploadDateTime") %>'
                                CssClass="label" runat="server" Text='<%# Bind("UploadDateTime") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="1%">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnversionHistory" runat="server" ToolTip="Version History"
                                OnClientClick="return VERSION_HISTORY(this);" CommandArgument='<%# Eval("ID") %>'>
                                <i id="iconhistory" runat="server" class="fa fa-history" style="color: #333333;"
                                    aria-hidden="true"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="1%">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnDocumentTrack" runat="server" ToolTip="Document Track" CommandArgument='<%# Eval("ID") %>'
                                OnClientClick="return DOC_TRACKING(this);"><i class="fa fa-map-marked-alt" style="color:#333333;" aria-hidden="true"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="1%">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnDownloadDoc" runat="server" ToolTip="Download" CommandName="Download" CssClass="btn"
                                CommandArgument='<%# Eval("ID") %>'><i class="fa fa-download" style="color:#333333;" aria-hidden="true"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
