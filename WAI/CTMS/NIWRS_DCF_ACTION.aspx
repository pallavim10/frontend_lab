<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NIWRS_DCF_ACTION.aspx.cs" Inherits="CTMS.NIWRS_DCF_ACTION" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="js/Input%20Mask/jquery.inputmask.bundle.js" type="text/javascript"></script>
    <script src="CommonFunctionsJs/IWRS/DataChange.js"></script>
    <script src="CommonFunctionsJs/IWRS/ShowChild.js"></script>
    <script src="CommonFunctionsJs/IWRS/CallChange.js"></script>
    <script src="CommonFunctionsJs/RadioCheck.js"></script>
    <script src="CommonFunctionsJs/ControlJS.js"></script>
    <script src="CommonFunctionsJs/Button_Mandatory.js"></script>
    <link href="CommonStyles/EntryGrid.css" rel="stylesheet" />
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <style type="text/css">
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

        $(document).on("click", ".cls-btnSave1", function () {
            var test = "0";

            $('.required1').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "0" || value == null) {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                    else {
                        $(this).removeClass("brd-1px-redimp");
                    }
                }
                else if (ctrl == "text" || ctrl == "textarea") {
                    if (value == "") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                    else {
                        $(this).removeClass("brd-1px-redimp");
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
                    if (value == "0" || value == null) {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                    else {
                        $(this).removeClass("brd-1px-redimp");
                    }
                }
                else if (ctrl == "text" || ctrl == "textarea") {
                    if (value == "") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                    else {
                        $(this).removeClass("brd-1px-redimp");
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
                    if (value == "0" || value == null) {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                    else {
                        $(this).removeClass("brd-1px-redimp");
                    }
                }
                else if (ctrl == "text" || ctrl == "textarea") {
                    if (value == "") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                    else {
                        $(this).removeClass("brd-1px-redimp");
                    }
                }
            });

            if (test == "1") {
                return false;
            }
            return true;
        });
    </script>
<%--    <script type="text/javascript" src="js/MaxLength.min.js"></script>
    <script type="text/javascript" src="js/MaxLength_Limit.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Raise Data Correction Request
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    <asp:HiddenField ID="hfControlType" runat="server" />
                </div>
            </div>
            <div class="box-body">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                DCF Id :
                            </div>
                            <div class="col-md-3">
                                <asp:Label runat="server" ID="lblID" CssClass="form-control" Width="80%"></asp:Label>
                            </div>
                            <div class="label col-md-2">
                                &nbsp;
                            </div>
                            <div class="col-md-3">
                                &nbsp;
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Site :
                            </div>
                            <div class="col-md-3">
                                <asp:Label runat="server" ID="lblSite" CssClass="form-control" Width="80%"></asp:Label>
                            </div>
                            <div class="label col-md-2">
                                Sub-Site :
                            </div>
                            <div class="col-md-3">
                                <asp:Label runat="server" ID="lblSubSite" CssClass="form-control" Width="80%"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                <asp:Label runat="server" ID="SUBJECTTEXT"></asp:Label>
                                :
                            </div>
                            <div class="col-md-3">
                                <asp:Label runat="server" ID="lblSubject" CssClass="form-control" Width="80%"></asp:Label>
                            </div>
                            <div class="label col-md-2">
                                Visit
                            </div>
                            <div class="col-md-3">
                                <asp:Label runat="server" ID="lblVisit" CssClass="form-control" Width="80%"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Form :
                            </div>
                            <div class="col-md-3">
                                <asp:Label runat="server" ID="lblForm" CssClass="form-control" Width="80%"></asp:Label>
                            </div>
                            <div class="label col-md-2">
                                Field :
                            </div>
                            <div class="col-md-3">
                                <asp:Label runat="server" ID="lblField" CssClass="form-control" Width="80%"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div runat="server" id="divData" visible="false" class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Old Value :
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="DRP_FIELD_old" runat="server" onchange="checkSpecs(this);"
                                    Visible="false" Width="80%">
                                </asp:DropDownList>
                                <asp:TextBox ID="TXT_FIELD_old" runat="server" Width="80%" autocomplete="off" Visible="false"
                                    onchange="checkSpecs(this);"></asp:TextBox>
                                <asp:Repeater runat="server" ID="repeat_CHK_old">
                                    <ItemTemplate>
                                        <div class="col-md-4">
                                            <asp:CheckBox ID="CHK_FIELD_old" runat="server" onchange="checkSpecs(this);" CssClass="checkbox"
                                                ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>' Text='<%# Bind("TEXT") %>' />
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Repeater runat="server" ID="repeat_RAD_old">
                                    <ItemTemplate>
                                        <div class="col-md-4">
                                            <asp:RadioButton ID="RAD_FIELD_old" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                onchange="checkSpecs(this);" onclick="return RadioCheck(this);" CssClass="radio"
                                                Text='<%# Bind("TEXT") %>' />
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="label col-md-2">
                                Select/Enter New Value :
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="DRP_FIELD_new" runat="server" onchange="checkSpecs(this);"
                                    Visible="false" Width="80%">
                                </asp:DropDownList>
                                <asp:TextBox ID="TXT_FIELD_new" runat="server" Width="80%" autocomplete="off" Visible="false"
                                    onchange="checkSpecs(this);"></asp:TextBox>
                                <asp:Repeater runat="server" ID="repeat_CHK_new">
                                    <ItemTemplate>
                                        <div class="col-md-4">
                                            <asp:CheckBox ID="CHK_FIELD_new" runat="server" onchange="checkSpecs(this);" CssClass="checkbox"
                                                ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>' Text='<%# Bind("TEXT") %>' />
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Repeater runat="server" ID="repeat_RAD_new">
                                    <ItemTemplate>
                                        <div class="col-md-4">
                                            <asp:RadioButton ID="RAD_FIELD_new" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                onchange="checkSpecs(this);" onclick="return RadioCheck(this);" CssClass="radio"
                                                Text='<%# Bind("TEXT") %>' />
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Enter Description :
                            </div>
                            <div class="col-md-3">
                                <asp:Label runat="server" ID="lblDesc" CssClass="form-control" Width="80%"></asp:Label>
                            </div>
                            <div class="label col-md-2">
                                Enter Reason :
                            </div>
                            <div class="col-md-3">
                                <asp:Label runat="server" ID="lblReason" CssClass="form-control" Width="80%"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Select Action :
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlAction" runat="server" Width="80%" class="form-control drpControl required3" OnSelectedIndexChanged="ddlAction_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Approve" Value="Approve"></asp:ListItem>
                                    <asp:ListItem Text="Disapprove" Value="Disapprove"></asp:ListItem>
                                    <asp:ListItem Text="Take Action" Value="Take Action"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="label col-md-2">
                                Enter Comments :
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox runat="server" ID="txtComment" Width="80%" Height="60px" TextMode="MultiLine"
                                    CssClass="form-control required3"> 
                                </asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-2">
                        </div>
                        <div class="col-lg-10">
                            <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm cls-btnSave3"
                                Style="margin-left: 20px;" OnClick="btnsubmit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm"
                                        Style="margin-left: 20px;" />
                        </div>
                    </div>
                    <br />
                </div>
            </div>

        </div>
        <div id="DivAction" runat="server" visible="false">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Kits
                    </h3>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="gvKitSubject" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    OnPreRender="grd_data_PreRender" CssClass="table table-bordered Datatable txt_center table-striped notranslate" OnRowCommand="gvKitSubject_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Subject Id">
                                            <ItemTemplate>
                                                <asp:Label ID="SUBJID" runat="server" Text='<%# Bind("SUBJID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dose / Visit">
                                            <ItemTemplate>
                                                <asp:Label ID="VISIT" runat="server" Text='<%# Bind("VISIT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Kit No.">
                                            <ItemTemplate>
                                                <asp:Label ID="KITNO" runat="server" Text='<%# Bind("KITNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnKitAction" CommandArgument='<%# Bind("KITNO") %>' runat="server" Text="Action" CssClass="btn btn-primary btn-sm" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Select Kit No. :
                                </div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlKitNo" runat="server" class="form-control drpControl required1" OnSelectedIndexChanged="ddlKitNo_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdnKitnoOld" runat="server" />
                                </div>
                                <div class="label col-md-1">
                                    Set Kit Status :
                                </div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlKitStatus" runat="server" class="form-control drpControl required1" OnSelectedIndexChanged="ddlKitStatus_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Dispense" Value="Dispensed"></asp:ListItem>
                                        <asp:ListItem Text="Quarantine" Value="Quarantined"></asp:ListItem>
                                        <asp:ListItem Text="Block" Value="Blocked"></asp:ListItem>
                                        <asp:ListItem Text="Damage" Value="Damaged"></asp:ListItem>
                                        <asp:ListItem Text="Destroy" Value="Destroyed"></asp:ListItem>
                                        <asp:ListItem Text="Return" Value="Returned"></asp:ListItem>
                                        <asp:ListItem Text="Reject" Value="Rejected"></asp:ListItem>
                                        <asp:ListItem Text="Back to Pool" Value="Back to Pool"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdnKitStatusOld" runat="server" />
                                </div>
                                <div id="divvisit" runat="server">
                                    <div class="label col-md-1">
                                        Select Visit :
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlKitVisit" runat="server" class="form-control drpControl">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hdnKitVisitOld" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Activity performed by :
                                </div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlKitActUser" runat="server" class="form-control drpControl select required1" SelectionMode="Single">
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdnActUserOld" runat="server" />
                                </div>
                                <div class="label col-md-1">
                                    Activity performed on :
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtKitActDate" runat="server" CssClass="form-control txtDate required1"></asp:TextBox>
                                    <asp:HiddenField ID="hdnKitActDateOld" runat="server" />
                                </div>
                                <div class="row" runat="server" id="DivKitReason" visible="false">
                                    <div class="label col-md-1">
                                        Enter Reason :
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtKitReason" TextMode="MultiLine" runat="server" Rows="3" CssClass="form-control required1"></asp:TextBox>
                                        <asp:HiddenField ID="hdnKitReasonOld" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <br />
                        <div class="row">
                            <div class="col-lg-2">
                            </div>
                            <div class="col-lg-10">
                                <asp:Button ID="btnSubmitKit" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm cls-btnSave1"
                                    Style="margin-left: 20px;" OnClick="btnSubmitKit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnCancelKit" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm"
                                        Style="margin-left: 20px;" />
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Subject Master
                    </h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Change Status :
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlStatus" runat="server" class="form-control drpControl required2">
                                </asp:DropDownList>
                                <asp:HiddenField ID="hdnStatusOld" runat="server" />
                            </div>
                            <div class="label col-md-2">
                                Randomization Number:
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="drpRandNo" runat="server" class="form-control drpControl">
                                </asp:DropDownList>
                                <asp:HiddenField ID="hdnRandNoOld" runat="server" />
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Last Visit :
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlLastVisit" runat="server" class="form-control drpControl required2">
                                </asp:DropDownList>
                                <asp:HiddenField ID="hdnLastVisitOld" runat="server" />
                            </div>
                            <div class="label col-md-2">
                                Last Visit Date:
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtLastVisitDate" runat="server" CssClass="form-control txtDate required2"></asp:TextBox>
                                <asp:HiddenField ID="hdnLastVisitDateOld" runat="server" />
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Next Visit :
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlNextVisit" runat="server" class="form-control drpControl">
                                </asp:DropDownList>
                                <asp:HiddenField ID="hdnNextVisitOld" runat="server" />
                            </div>
                            <div class="label col-md-2">
                                Next Visit Date:
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtNextVisitDate" runat="server" CssClass="form-control txtDate"></asp:TextBox>
                                <asp:HiddenField ID="hdnNextVisitDateOld" runat="server" />
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Early Visit Date:
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtEarlyDate" runat="server" CssClass="form-control txtDate"></asp:TextBox>
                                <asp:HiddenField ID="hdnEarlyDateOld" runat="server" />
                            </div>
                            <div class="label col-md-2">
                                Late Visit Date:
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtLateDate" runat="server" CssClass="form-control txtDate"></asp:TextBox>
                                <asp:HiddenField ID="hdnLateDateOld" runat="server" />
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Repeater runat="server" ID="repeatAdditionalFields">
                                <ItemTemplate>
                                    <div class="col-md-6">
                                        <div class="label col-md-4">
                                            <asp:Label runat="server" ID="lblFIELDNAME" Text='<%# Bind("FIELDNAME") %>' />
                                        </div>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtDATA" runat="server" CssClass="form-control" Text='<%# Bind("DATA") %>'></asp:TextBox>
                                            <asp:HiddenField ID="hdnOldDATA" runat="server" Value='<%# Bind("DATA") %>' />
                                        </div>
                                        <asp:HiddenField runat="server" ID="hdnVARIABLENAME" Value='<%# Bind("COL_NAME") %>' />
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="grdVisitDates" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                CssClass="table table-bordered Datatable txt_center table-striped notranslate" OnRowDataBound="grdVisitDates_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Subject Id">
                                        <ItemTemplate>
                                            <asp:Label ID="SUBJID" runat="server" Text='<%# Bind("SUBJID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Visit">
                                        <ItemTemplate>
                                            <asp:Label ID="VISIT" runat="server" Text='<%# Bind("VISIT") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Visit Date" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtVISIT_DATE" CssClass="txtDate form-control txt_center" runat="server" Text='<%# Bind("VISIT_DATE") %>'></asp:TextBox>
                                            <asp:HiddenField ID="hdnVISIT_DATEOld" runat="server" Value='<%# Bind("VISIT_DATE") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Early Date" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtEARLY_DATE" CssClass="txtDate form-control txt_center" runat="server" Text='<%# Bind("EARLY_DATE") %>'></asp:TextBox>
                                            <asp:HiddenField ID="hdnEARLY_DATEOld" runat="server" Value='<%# Bind("EARLY_DATE") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Late Date" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtLATE_DATE" runat="server" CssClass="txtDate form-control txt_center" Text='<%# Bind("LATE_DATE") %>'></asp:TextBox>
                                            <asp:HiddenField ID="hdnLATE_DATEOld" runat="server" Value='<%# Bind("LATE_DATE") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actual Visit Date" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtACTUAL_VISIT_DATE" runat="server" CssClass="txtDate form-control txt_center" Text='<%# Bind("ACTUAL_VISIT_DATE") %>'></asp:TextBox>
                                            <asp:HiddenField ID="hdnACTUAL_VISIT_DATEOld" runat="server" Value='<%# Bind("ACTUAL_VISIT_DATE") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-2">
                        </div>
                        <div class="col-lg-10">
                            <asp:Button ID="btnSubmitSubjectMaster" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm cls-btnSave2"
                                Style="margin-left: 20px;" OnClick="btnSubmitSubjectMaster_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                        </div>
                    </div>
                    <br />
                </div>
            </div>
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Subject Data
                    </h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Select Form. :
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlModule" runat="server" class="form-control drpControl required" Width="100%" OnSelectedIndexChanged="ddlModule_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div runat="server" id="divSubjectDataNote" visible="false">
                        <div class="row">
                            <div class="label col-md-12 text-red">
                                Note : This form can not be updated, as the related form has been frozen in Data Management.
                            </div>
                        </div>

                    </div>
                    <br />
                    <div class="form-group">
                        <asp:HiddenField runat="server" ID="hfFormCode" />
                        <asp:HiddenField runat="server" ID="hfMODULEID" />
                        <asp:HiddenField runat="server" ID="hfTablename" />
                        <asp:HiddenField runat="server" ID="hfRANDID" />
                        <asp:HiddenField runat="server" ID="hfSUBJID" />
                        <asp:HiddenField runat="server" ID="hfSITEID" />
                        <asp:HiddenField runat="server" ID="hfSUBSITEID" />
                        <asp:HiddenField runat="server" ID="hfINDICATION" />
                        <asp:HiddenField runat="server" ID="hfINDICATION_ID" />
                        <asp:HiddenField runat="server" ID="hfVISIT" />
                        <asp:HiddenField runat="server" ID="hfVISITNUM" />
                        <asp:HiddenField runat="server" ID="hfDM_SYNC" />
                        <asp:HiddenField runat="server" ID="hfDM_Tablename" />
                        <asp:HiddenField runat="server" ID="hfDM_MODULEID" />
                        <asp:HiddenField runat="server" ID="hfApplVisit" />
                        <div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="grd_Data" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                        ShowHeader="False" CssClass="table table-bordered table-striped ShowChild1" OnRowDataBound="grd_Data_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="VARIABLENAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVARIABLENAME" Text='<%# Bind("VARIABLENAME") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CONTROLTYPE" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCONTROLTYPE" Text='<%# Bind("CONTROLTYPE") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FIELD NAME" HeaderStyle-CssClass="align-center" ItemStyle-CssClass="align-left"
                                                ControlStyle-CssClass="label" ItemStyle-Width="30%" HeaderStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFieldName" Font-Size="Small" ToolTip='<%# Bind("Descrip") %>' Text='<%# Bind("FIELDNAME") %>'
                                                        Style="text-align: LEFT" runat="server"></asp:Label>
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
                                                    <div class="col-md-12">
                                                        <asp:DropDownList ID="DRP_FIELD" runat="server" onchange="showChild(this); DATA_Changed(this);" CssClass="RunChecks " Visible="false" Width="200px">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="TXT_FIELD" runat="server" Width="200px" autocomplete="off" Visible="false"
                                                            CssClass="RunChecks" onchange="showChild(this); DATA_Changed(this);"></asp:TextBox>

                                                        <asp:Repeater runat="server" ID="repeat_CHK">
                                                            <ItemTemplate>
                                                                <div class="col-md-4">
                                                                    <asp:CheckBox ID="CHK_FIELD" runat="server" onchange="showChild(this); DATA_Changed(this);"
                                                                        CssClass="checkbox RunChecks" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                        Text='<%# Bind("TEXT") %>' />
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <asp:Repeater runat="server" ID="repeat_RAD">
                                                            <ItemTemplate>
                                                                <div class="col-md-4">
                                                                    <asp:RadioButton ID="RAD_FIELD" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                        onchange=" showChild(this); DATA_Changed(this);" onfocus="myFocus(this)" onclick="return RadioCheck(this);"
                                                                        CssClass="radio RunChecks" Text='<%# Bind("TEXT") %>' />
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <asp:HiddenField runat="server" ID="HDN_OLD_VALUE" />
                                                        <asp:Button runat="server" ID="btnDATA_Changed" CssClass="disp-none" OnClick="DATA_Changed"></asp:Button>

                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVal_Child" Text='<%# Bind("VAL_Child") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PREFIXTEXT" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPREFIXTEXT" Text='<%# Bind("PREFIXTEXT") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="STRATA" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSTRATA" Text='<%# Bind("STRATA") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IWRS_DM_SYNC" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                <ItemTemplate>
                                                    <asp:Label ID="IWRS_DM_SYNC" Text='<%# Bind("IWRS_DM_SYNC") %>' runat="server"></asp:Label>
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
                                                                                    <asp:Label ID="lblVARIABLENAME" Text='<%# Bind("VARIABLENAME") %>' runat="server"></asp:Label>
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
                                                                                    <asp:Label ID="lblFieldName" Font-Size="Small" ToolTip='<%# Bind("Descrip") %>' Text='<%# Bind("FIELDNAME") %>'
                                                                                        Style="text-align: LEFT" runat="server"></asp:Label>
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
                                                                                    <div class="col-md-12">
                                                                                        <asp:DropDownList ID="DRP_FIELD" runat="server" onchange=" showChild(this); DATA_Changed(this);"
                                                                                            CssClass="RunChecks" Visible="false" Width="200px">
                                                                                        </asp:DropDownList>
                                                                                        <asp:TextBox ID="TXT_FIELD" runat="server" Width="200px" autocomplete="off" Visible="false"
                                                                                            CssClass="RunChecks" onchange=" showChild(this); DATA_Changed(this);"
                                                                                            onfocus="myFocus(this)"></asp:TextBox>

                                                                                        <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                            <ItemTemplate>
                                                                                                <div class="col-md-4">
                                                                                                    <asp:CheckBox ID="CHK_FIELD" runat="server" onchange=" showChild(this); DATA_Changed(this);"
                                                                                                        CssClass="checkboxRunChecks" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                        Text='<%# Bind("TEXT") %>' />
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:Repeater>
                                                                                        <asp:Repeater runat="server" ID="repeat_RAD">
                                                                                            <ItemTemplate>
                                                                                                <div class="col-md-4">
                                                                                                    <asp:RadioButton ID="RAD_FIELD" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                        onchange=" showChild(this);  DATA_Changed(this);" onfocus="myFocus(this)" onclick="return RadioCheck(this);"
                                                                                                        CssClass="radio RunChecks" Text='<%# Bind("TEXT") %>' />
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:Repeater>
                                                                                        <asp:HiddenField runat="server" ID="HDN_OLD_VALUE" />
                                                                                        <asp:Button runat="server" ID="btnDATA_Changed" CssClass="disp-none" OnClick="DATA_Changed"></asp:Button>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblVal_Child" Text='<%# Bind("VAL_Child") %>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="PREFIXTEXT" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPREFIXTEXT" Text='<%# Bind("PREFIXTEXT") %>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="STRATA" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSTRATA" Text='<%# Bind("STRATA") %>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="IWRS_DM_SYNC" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="IWRS_DM_SYNC" Text='<%# Bind("IWRS_DM_SYNC") %>' runat="server"></asp:Label>
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
                                                                                                                    <asp:Label ID="lblVARIABLENAME" Text='<%# Bind("VARIABLENAME") %>' runat="server"></asp:Label>
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
                                                                                                                    <asp:Label ID="lblFieldName" Font-Size="Small" ToolTip='<%# Bind("Descrip") %>' Text='<%# Bind("FIELDNAME") %>'
                                                                                                                        Style="text-align: LEFT" runat="server"></asp:Label>
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
                                                                                                                    <div class="col-md-12">
                                                                                                                        <asp:DropDownList ID="DRP_FIELD" runat="server" onchange=" showChild(this); DATA_Changed(this);"
                                                                                                                            CssClass="RunChecks" Visible="false" Width="200px">
                                                                                                                        </asp:DropDownList>
                                                                                                                        <asp:TextBox ID="TXT_FIELD" runat="server" Width="200px" autocomplete="off" Visible="false"
                                                                                                                            CssClass="RunChecks" onchange=" showChild(this); DATA_Changed(this);"
                                                                                                                            onfocus="myFocus(this)"></asp:TextBox>

                                                                                                                        <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                                                            <ItemTemplate>
                                                                                                                                <div class="col-md-4">
                                                                                                                                    <asp:CheckBox ID="CHK_FIELD" runat="server" onchange=" showChild(this); DATA_Changed(this);"
                                                                                                                                        CssClass=" checkboxRunChecks" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                                                        Text='<%# Bind("TEXT") %>' />
                                                                                                                                </div>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:Repeater>
                                                                                                                        <asp:Repeater runat="server" ID="repeat_RAD">
                                                                                                                            <ItemTemplate>
                                                                                                                                <div class="col-md-4">
                                                                                                                                    <asp:RadioButton ID="RAD_FIELD" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                                                        onchange=" showChild(this);  DATA_Changed(this);" onfocus="myFocus(this)" onclick="return RadioCheck(this);"
                                                                                                                                        CssClass="radioRunChecks" Text='<%# Bind("TEXT") %>' />
                                                                                                                                </div>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:Repeater>
                                                                                                                        <asp:HiddenField runat="server" ID="HDN_OLD_VALUE" />
                                                                                                                        <asp:Button runat="server" ID="btnDATA_Changed" CssClass="disp-none" OnClick="DATA_Changed"></asp:Button>
                                                                                                                    </div>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblVal_Child" Text='<%# Bind("VAL_Child") %>' runat="server"></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="PREFIXTEXT" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblPREFIXTEXT" Text='<%# Bind("PREFIXTEXT") %>' runat="server"></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="STRATA" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblSTRATA" Text='<%# Bind("STRATA") %>' runat="server"></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="IWRS_DM_SYNC" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="IWRS_DM_SYNC" Text='<%# Bind("IWRS_DM_SYNC") %>' runat="server"></asp:Label>
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
                                                                                                                                                    <asp:Label ID="lblVARIABLENAME" Text='<%# Bind("VARIABLENAME") %>' runat="server"></asp:Label>
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
                                                                                                                                                    <asp:Label ID="lblFieldName" Font-Size="Small" ToolTip='<%# Bind("Descrip") %>' Text='<%# Bind("FIELDNAME") %>'
                                                                                                                                                        Style="text-align: LEFT" runat="server"></asp:Label>
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
                                                                                                                                                    <div class="col-md-12">
                                                                                                                                                        <asp:DropDownList ID="DRP_FIELD" runat="server" onchange=" showChild(this); DATA_Changed(this);"
                                                                                                                                                            CssClass="RunChecks" Visible="false" Width="200px">
                                                                                                                                                        </asp:DropDownList>
                                                                                                                                                        <asp:TextBox ID="TXT_FIELD" runat="server" Width="200px" autocomplete="off" Visible="false"
                                                                                                                                                            CssClass="RunChecks" onchange=" showChild(this); DATA_Changed(this);"
                                                                                                                                                            onfocus="myFocus(this)"></asp:TextBox>

                                                                                                                                                        <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <div class="col-md-4">
                                                                                                                                                                    <asp:CheckBox ID="CHK_FIELD" runat="server" onchange=" showChild(this); DATA_Changed(this);"
                                                                                                                                                                        CssClass="checkboxRunChecks" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                                                                                        Text='<%# Bind("TEXT") %>' />
                                                                                                                                                                </div>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                        </asp:Repeater>
                                                                                                                                                        <asp:Repeater runat="server" ID="repeat_RAD">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <div class="col-md-4">
                                                                                                                                                                    <asp:RadioButton ID="RAD_FIELD" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                                                                                        onchange=" showChild(this);   DATA_Changed(this);" onfocus="myFocus(this)" onclick="return RadioCheck(this);"
                                                                                                                                                                        CssClass="radioRunChecks" Text='<%# Bind("TEXT") %>' />
                                                                                                                                                                </div>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                        </asp:Repeater>
                                                                                                                                                        <asp:HiddenField runat="server" ID="HDN_OLD_VALUE" />
                                                                                                                                                        <asp:Button runat="server" ID="btnDATA_Changed" CssClass="disp-none" OnClick="DATA_Changed"></asp:Button>
                                                                                                                                                    </div>
                                                                                                                                                </ItemTemplate>
                                                                                                                                            </asp:TemplateField>
                                                                                                                                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                                <ItemTemplate>
                                                                                                                                                    <asp:Label ID="lblVal_Child" Text='<%# Bind("VAL_Child") %>' runat="server"></asp:Label>
                                                                                                                                                </ItemTemplate>
                                                                                                                                            </asp:TemplateField>
                                                                                                                                            <asp:TemplateField HeaderText="PREFIXTEXT" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                                <ItemTemplate>
                                                                                                                                                    <asp:Label ID="lblPREFIXTEXT" Text='<%# Bind("PREFIXTEXT") %>' runat="server"></asp:Label>
                                                                                                                                                </ItemTemplate>
                                                                                                                                            </asp:TemplateField>
                                                                                                                                            <asp:TemplateField HeaderText="STRATA" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                                <ItemTemplate>
                                                                                                                                                    <asp:Label ID="lblSTRATA" Text='<%# Bind("STRATA") %>' runat="server"></asp:Label>
                                                                                                                                                </ItemTemplate>
                                                                                                                                            </asp:TemplateField>
                                                                                                                                            <asp:TemplateField HeaderText="IWRS_DM_SYNC" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                                <ItemTemplate>
                                                                                                                                                    <asp:Label ID="IWRS_DM_SYNC" Text='<%# Bind("IWRS_DM_SYNC") %>' runat="server"></asp:Label>
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
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-2">
                                </div>
                                <div class="col-lg-10">
                                    <asp:Button ID="btnDatasubmit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                        OnClick="btnDatasubmit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                </div>

                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

