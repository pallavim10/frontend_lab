<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="eTMF_DiSAPP_MODIFY.aspx.cs" Inherits="CTMS.eTMF_DiSAPP_MODIFY" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <style type="text/css">
        .select2-selection select2-selection--single {
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

        function ShowReminderDiv() {

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
    <script type="text/javascript">
        function setFileName() {
            var fileUpload = document.getElementById('<%= FileUpload1.ClientID %>');
            var txtFileName = document.getElementById('<%= txtFileName1.ClientID %>');

            if (fileUpload.files.length > 0) {
                txtFileName.value = fileUpload.files[0].name;
            }
            //else {
            //    txtFileName.value = '';
            //}
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Action on Rejected Document </h3>
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
                <div class="row" id="div43" runat="server" style="padding-top: 15px;">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Select Action :
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddltakeaction" runat="server" CssClass="form-control width250px"
                                OnSelectedIndexChanged="ddltakeaction_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Modify/Reupload"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Delete"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <div id="DivDELETEDOC" runat="server" visible="false">
                    <div class="row" id="div2" runat="server">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                <label>Zone:</label>
                            </div>
                            <div class="col-md-8">
                                <asp:Label ID="lblZone" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row" id="div4" runat="server">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                <label>Section:</label>
                            </div>
                            <div class="col-md-8">
                                <asp:Label ID="lblSection" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row" id="div15" runat="server">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                <label>Artifact: </label>
                            </div>
                            <div class="col-md-8">
                                <asp:Label ID="lblArtifact" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row" id="div16" runat="server">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                <label>Sub-Artifact:</label>
                            </div>
                            <div class="col-md-8">
                                <asp:Label ID="lblSubArtifact" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row" id="div17" runat="server">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                <label>File Name:</label>
                            </div>
                            <div class="col-md-8">
                                <asp:Label ID="lblFilename" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row" id="div18" runat="server">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                <label>Reason:</label>
                            </div>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtReason" runat="server" CssClass="form-control required" TextMode="MultiLine"
                                    Height="60" Width="300"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row" id="div14" runat="server" style="padding-top: 15px; padding-bottom: 15px;">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                <asp:Button ID="Button1" runat="server" Text="Back" CssClass="btn btn-DARKORANGE btn-sm"
                                    OnClick="btnback_Click" />
                            </div>
                            <div class="col-md-3">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm cls-btnSave"
                                    OnClick="btnSubmit_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div id="DivModifyDoc" runat="server" visible="false">
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
                                        <asp:ListItem Text="Current" Value="Current"></asp:ListItem>
                                        <asp:ListItem Text="Superseded" Value="Superseded"></asp:ListItem>
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
                                            <asp:TemplateField HeaderText="Received Date " ItemStyle-CssClass="txt_center">
                                                <ItemTemplate>
                                                    <asp:Label ID="ReceivedDate" Width="100%" ToolTip='<%# Bind("RECEIPTDAT") %>' CssClass="label"
                                                        runat="server" Text='<%# Bind("RECEIPTDAT") %>'></asp:Label>
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
                                    <asp:Label runat="server" ID="lblDateTitle"></asp:Label>
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
                        <div class="row" id="div19" runat="server" style="padding-top: 15px;">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Received Date : &nbsp;
                                <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox runat="server" ID="txtReceiptdate" CssClass="form-control txtDate width250px required"></asp:TextBox>
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
                                        onchange="return ShowReminderDiv();"></asp:TextBox>
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
                        <div class="row" id="div10" runat="server" style="padding-top: 15px;">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    File Name : &nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox runat="server" ID="txtFileName1" CssClass="form-control width250px"></asp:TextBox>
                                </div>
                                <div class="label col-md-2">
                                    <asp:FileUpload ID="FileUpload1" runat="server" onchange="setFileName();" />
                                </div>
                                <div class="col-md-3">
                                    &nbsp
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row" id="div12" runat="server">
                            <div class="col-md-12">
                                <div class="label col-md-3">
                                    <asp:Button ID="btnBack" runat="server" Text="Back "
                                        CssClass="btn btn-DARKORANGE btn-sm" OnClick="btnBack_Click" />
                                </div>
                                &nbsp&nbsp
                                <div class="col-md-4">
                                    <asp:Button ID="btnUpload" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm cls-btnSave"
                                        OnClick="btnUpload_Click" />
                                </div>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
