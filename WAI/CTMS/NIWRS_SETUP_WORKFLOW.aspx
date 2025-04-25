<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NIWRS_SETUP_WORKFLOW.aspx.cs" Inherits="CTMS.NIWRS_SETUP_WORKFLOW" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="js/CKEditor/ckeditor.js" type="text/javascript"></script>
    <script src="CommonFunctionsJs/IWRS/IWRS_ConfirmMsg.js"></script>
    <script src="CommonFunctionsJs/IWRS/IWRS_showAuditTrail.js"></script>
    <link href="CommonStyles/ModalPopup.css" rel="stylesheet" />
    <script type="text/javascript">
        CKEDITOR.config.toolbar = [
            ['Bold', 'Italic', 'Underline', 'StrikeThrough', '-', 'Undo', 'Redo', '-', 'Outdent', 'Indent', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
            ['Styles', 'Format', 'Font', 'FontSize']
        ];

        CKEDITOR.config.height = 250;

        function CallCkedit() {

            CKEDITOR.replace("MainContent_txtEmailBody");

        }
    </script>
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">


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

        function AddCRITs(element) {
            var ID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var test = "NIWRS_WORKFLOW_CRITs.aspx?ID=" + ID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=600,width=1300";
            popupWindow = window.open(test, '_blank', strWinProperty);
            return false;
        }

        function AddCLICKs(element) {
            var ID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var test = "NIWRS_WORKFLOW_CLICKs.aspx?ID=" + ID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=600,width=1300";
            popupWindow = window.open(test, '_blank', strWinProperty);
            return false;
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

        function showDivParent() {

            var element = document.getElementById('MainContent_chkNavMenu');
            if ($(element).prop('checked') == true) {
                $('#divNAVPRENT').removeClass('disp-none');
            }
            else if ($(element).prop('checked') == false) {
                $('#divNAVPRENT').addClass('disp-none');
            }

        }

        function showDivNextVisit() {

            var element = document.getElementById('MainContent_ddlVisit');
            if ($(element).val() != "0") {
                $('#divNextVisit').removeClass('disp-none');
            }
            else if ($(element).val() == "0") {
                $('#divNextVisit').addClass('disp-none');
            }
        }

        function ShowEmailDiv() {

            var element = document.getElementById('MainContent_chkSendEmail');
            if ($(element).prop('checked') == true) {
                $('#divEmail').removeClass('disp-none');
            }
            else if ($(element).prop('checked') == false) {
                $('#divEmail').addClass('disp-none');
            }
        }

        function MngDosage(element) {
            var ID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var test = "NIWRS_WORKFLOW_DOSAGE.aspx?ID=" + ID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=600,width=1300";
            popupWindow = window.open(test, '_blank', strWinProperty);
            return false;
        }

        function ConfigFrozen_MSG() {
            alert('Configuration has been Frozen');
            return false;
        }


    </script>
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: false,
                fixedHeader: true
            });

            $(".Datatable").parent().parent().addClass('fixTableHead');
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.noSpace').keypress(function (e) {
                if (e.which === 32) {
                    return false;
                }
            });
        });

    </script>
 <%--   <script type="text/javascript" src="js/MaxLength.min.js"></script>
    <script type="text/javascript" src="js/MaxLength_Limit.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField runat="server" ID="hdnREVIEWSTATUS" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="dropdown-menu dropdown-menu-sm" id="context-menu">
                <ul style="margin-left: -23px;">
                    <li>
                        <div class="col-md-12">
                            <div class="col-md-6">
                                Site ID :
                            </div>
                            <div class="col-md-6">
                                [SITEID]
                            </div>
                        </div>
                    </li>
                    <li>
                        <div class="col-md-12">
                            <div class="col-md-6">
                                Subject ID :
                            </div>
                            <div class="col-md-6">
                                [SUBJID]
                            </div>
                        </div>
                    </li>
                </ul>
            </div>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title" align="left">Define Workflow</h3>

                </div>
                <div class="form-group">
                    <div class="form-group has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="box box-primary">
                <div class="box-header">
                    <h4 class="box-title" align="left">Added Steps
                    </h4>
                    <div class="pull-right" style="margin-right: 10px;">
                        <asp:LinkButton ID="lbtnExport" runat="server" Text="Export" OnClick="lbtnExport_Click" Font-Size="12px" CssClass="btn btn-info" Style="margin-top: 3px;" ForeColor="White">
                        Export Added Steps&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                    </div>
                </div>
                <div class="rows" style="width: 100%; overflow: auto; margin-top: 5px;">
                    <asp:GridView ID="grdSteps" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                        OnRowCommand="grdSteps_RowCommand" Style="width: 98%; border-collapse: collapse; margin-left: 20px;" OnRowDataBound="grdSteps_RowDataBound" OnPreRender="grdSteps_PreRender">
                        <Columns>
                            <asp:TemplateField HeaderText="ID" ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnEdit" CommandArgument='<%# Bind("ID") %>' CommandName="EditStep"
                                        runat="server" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No." ItemStyle-CssClass="txt_center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSEQNO" Text='<%# Bind("SEQNO") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Header">
                                <ItemTemplate>
                                    <asp:Label ID="lblHEADER" Text='<%# Bind("HEADER") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Source">
                                <ItemTemplate>
                                    <asp:Label ID="lblSource" Text='<%# Bind("SOURCE") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Navigate To">
                                <ItemTemplate>
                                    <asp:Label ID="lblNAVIGATE" Text='<%# Bind("NAVIGATE") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Activity">
                                <ItemTemplate>
                                    <asp:Label ID="lblPERFORM" Text='<%# Bind("PERFORM") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('NIWRS_WORKFLOW_STEPS', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Manage Additional Criterias" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnAddCrit" CommandArgument='<%# Bind("ID") %>' runat="server"
                                        OnClientClick="return AddCRITs(this);" ToolTip="Manage Additional Criterias"><i class="fa fa-cog"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Manage Additional Clicks" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnAddClicks" Visible="false" CommandArgument='<%# Bind("ID") %>'
                                        OnClientClick="return AddCLICKs(this);" runat="server" ToolTip="Manage Additional Clicks"><i class="fa fa-plus-circle"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Manage Dosage" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnMngDosage" Visible="false" CommandArgument='<%# Bind("ID") %>'
                                        OnClientClick="return MngDosage(this);" runat="server" ToolTip="Manage Dosage"><i class="fa fa-medkit"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnDelete" CommandArgument='<%# Bind("ID") %>' CommandName="DeleteStep"
                                        runat="server" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this workflow :  ", Eval("HEADER")) %>' ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Add Steps</h3>
                </div>
                <div class="rows">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <label>
                                    Enter Page Header:</label>
                            </div>
                            <div class="col-md-10">
                                <asp:TextBox ID="txtHeader" runat="server" Width="50%" CssClass="form-control required"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <label>
                                    Enter Sequence Number:</label>
                            </div>
                            <div class="col-md-10">
                                <asp:TextBox ID="txtSEQNO" runat="server" Width="10%" CssClass="form-control numeric required"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <label>
                                    Select Page Type. :</label>
                            </div>
                            <div class="col-md-10">
                                <asp:DropDownList runat="server" ID="ddlSOURCE_TYPE" CssClass="form-control required width200px"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlSOURCE_TYPE_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Listing" Value="Listing"></asp:ListItem>
                                    <asp:ListItem Text="Form" Value="Form"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <label>
                                    Select Source :</label>
                            </div>
                            <div class="col-md-10">
                                <asp:DropDownList runat="server" ID="ddlSource" CssClass="form-control required width200px">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <label>
                                    Select Auto-Populate List :</label>
                            </div>
                            <div class="col-md-10">
                                <asp:DropDownList runat="server" ID="ddlAutoPopList" CssClass="form-control width200px">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-3" style="width: 195px;">
                                <label>
                                    Publish to Navigation Menu :</label>
                            </div>
                            <div class="col-md-9" style="display: inline-flex;">
                                <asp:CheckBox runat="server" ID="chkNavMenu" onchange="return showDivParent();" ToolTip="Select if 'Yes'" />
                            </div>
                        </div>
                    </div>
                    <br />
                    <div id="divNAVPRENT" class="disp-none">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <label>
                                        Select Parent Menu :</label>
                                </div>
                                <div class="col-md-2" style="display: inline-flex;">
                                    <asp:DropDownList runat="server" ID="drpNAV_PARENT" CssClass="form-control width200px">
                                        <asp:ListItem Selected="True" Text="None" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Report (Blinded)" Value="Blinded"></asp:ListItem>
                                        <asp:ListItem Text="Report (Unblinded)" Value="Unblinded"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-8" style="display: inline-flex;">
                                    <asp:LinkButton ID="lbtnMngParent" runat="server" CssClass="btn btn-info" ForeColor="White" ToolTip="Manage Parent Menu" OnClientClick="return showDivParent();">
                                        Manage Parent Menu
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <br />
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-3" style="width: 195px;">
                                <label>
                                    Downloadable :</label>
                            </div>
                            <div class="col-md-9" style="display: inline-flex;">
                                <asp:CheckBox runat="server" ID="chkDownloadAble" ToolTip="Select if 'Yes'" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-group" style="background-color: lightcyan;">
                            <div class="box-header">
                                <h3 class="box-title" align="left">Actions</h3>
                                <div class="pull-right" style="margin-right: 10px;">
                                    <asp:LinkButton ID="lbtnHelp" runat="server" CssClass="anish" ToolTip="Help" Style="font-size: 15px;">
                                <span class="glyphicon glyphicon-question-sign 3x anish" style="color:#333333;" ></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <label>
                                            Applicable Visit :</label>
                                    </div>
                                    <div class="col-md-10">
                                        <asp:DropDownList runat="server" ID="ddlVisit" onchange="return showDivNextVisit();"
                                            CssClass="form-control" Width="50%">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div id="divNextVisit" class="disp-none">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <label>
                                                Next Visit :</label>
                                        </div>
                                        <div class="col-md-10">
                                            <asp:DropDownList runat="server" ID="ddlNextVisit" CssClass="form-control" Width="50%">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <label>
                                            Activity :</label>
                                    </div>
                                    <div class="col-md-10">
                                        <asp:DropDownList ID="ddlPerform" runat="server" CssClass="form-control" Width="50%">
                                            <asp:ListItem Selected="True" Text="None" Value="None"></asp:ListItem>
                                            <asp:ListItem Text="Assign Screening Id" Value="Screening"></asp:ListItem>
                                            <asp:ListItem Text="Pre-Randomization" Value="Pre-Randomization"></asp:ListItem>
                                            <asp:ListItem Text="Assign Randomization Number" Value="Randomization"></asp:ListItem>
                                            <asp:ListItem Text="Unassign Randomization Number" Value="De-Randomization"></asp:ListItem>
                                            <asp:ListItem Text="Pre-Dosing" Value="Pre-Dosing"></asp:ListItem>
                                            <asp:ListItem Text="Allocate Kit" Value="Dosing"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <label>
                                            Messagebox :</label>
                                    </div>
                                    <div class="col-md-10">
                                        <asp:TextBox runat="server" ID="txtMSGBOX" Height="50px" Width="50%" TextMode="MultiLine"
                                            CssClass="form-control"> 
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <label>
                                            Event History :</label>
                                    </div>
                                    <div class="col-md-10">
                                        <asp:TextBox runat="server" ID="txtEventHistory" Height="50px" Width="50%" TextMode="MultiLine"
                                            CssClass="form-control"> 
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <label>
                                            Set Field Value :</label>
                                    </div>
                                    <div class="col-md-10">
                                        <div class="col-md-2" style="padding: 0;">
                                            <asp:Label ID="Label1" runat="server" Text="Status" CssClass="label"></asp:Label>
                                        </div>
                                        <div class="col-md-4" style="padding: 0;">
                                            <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control width200px">
                                                <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Listing" Value="Listing"></asp:ListItem>
                                                <asp:ListItem Text="Form" Value="Form"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <asp:Repeater runat="server" ID="repeatSetFields">
                                    <ItemTemplate>
                                        <br />
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                <label>
                                                    &nbsp;</label>
                                                <asp:HiddenField ID="hfCOLNAME" runat="server" Value='<%# Eval("COL_NAME") %>' />
                                            </div>
                                            <div class="col-md-10">
                                                <div class="col-md-2" style="padding: 0;">
                                                    <asp:Label ID="lblFIELDNAME" runat="server" Text='<%# Eval("FIELDNAME") %>' CssClass="label"></asp:Label>
                                                </div>
                                                <div class="col-md-4" style="padding: 0;">
                                                    <asp:TextBox runat="server" ID="txtSetFieldVal" Text='<%# Eval("Set_VALUE") %>' CssClass="form-control width200px"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <label>
                                            Navigate To :</label>
                                    </div>
                                    <div class="col-md-10">
                                        <div class="col-md-2" style="padding: 0;">
                                            <asp:Label ID="Label2" runat="server" Text="Navigation Type :" CssClass="label"></asp:Label>
                                        </div>
                                        <div class="col-md-4" style="padding: 0;">
                                            <asp:DropDownList runat="server" ID="ddlNavType" CssClass="form-control width200px"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlNavType_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Listing" Value="Listing"></asp:ListItem>
                                                <asp:ListItem Text="Form" Value="Form"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <label>
                                            &nbsp;</label>
                                    </div>
                                    <div class="col-md-10">
                                        <div class="col-md-2" style="padding: 0;">
                                            <asp:Label ID="Label3" runat="server" Text="Navigation Source :" CssClass="label"></asp:Label>
                                        </div>
                                        <div class="col-md-4" style="padding: 0;">
                                            <asp:DropDownList runat="server" ID="ddlNavTo" CssClass="form-control width200px">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <label>
                                            Send Email :</label>
                                    </div>
                                    <div class="col-md-10">
                                        <div class="col-md-10" style="padding: 0;">
                                            <asp:CheckBox ID="chkSendEmail" runat="server" Text="   Select, if Yes." onchange="ShowEmailDiv()" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="disp-none" id="divEmail">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-7">
                                            <label>
                                                Email IDs :</label>
                                            <asp:GridView runat="server" ID="gvEmailds" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                Style="width: 99%; border-collapse: collapse;">
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Site ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                                runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="regexEmailValid" ForeColor="Red" runat="server" ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                                ControlToValidate="txtEMAILIDs" ErrorMessage="Invalid Email "></asp:RegularExpressionValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                                runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="regexCCEMAILIDsd" ForeColor="Red" runat="server" ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                                ControlToValidate="txtCCEMAILIDs" ErrorMessage="Invalid Email "></asp:RegularExpressionValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                                runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="regexBCCEMAILIDs" ForeColor="Red" runat="server" ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                                ControlToValidate="txtBCCEMAILIDs" ErrorMessage="Invalid Email "></asp:RegularExpressionValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="rows">
                                                <div class="row">
                                                    <label>
                                                        Email Subject :</label>
                                                    <asp:TextBox runat="server" ID="txtEmailSubject" Height="50px" Width="99%" TextMode="MultiLine"
                                                        CssClass="form-control"> 
                                                    </asp:TextBox>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <label>
                                                        Email Body :</label>
                                                    <asp:TextBox runat="server" ID="txtEmailBody" CssClass="ckeditor" Height="50%" TextMode="MultiLine"
                                                        Width="99%"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                        <br />
                        <br />
                        <div class="form-group">
                            <div class="row txt_center">
                                <asp:Button ID="btnsubmit" Text="Submit" runat="server" Style="color: white;"
                                    CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnsubmit_Click" />
                                <asp:Button ID="btnUpdate" Text="Update" runat="server" Style="color: white;"
                                    Visible="false" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnUpdate_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lbtnCancel" Text="Cancel" runat="server" Style="color: white;"
                                CssClass="btn btn-primary btn-sm" OnClick="lbtnCancel_Click" />
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
                <asp:ModalPopupExtender ID="modalMngParent" runat="server" PopupControlID="pnlMngParent" TargetControlID="lbtnMngParent"
                    BackgroundCssClass="Background">
                </asp:ModalPopupExtender>
                <asp:Panel ID="pnlMngParent" runat="server" Style="display: none;" CssClass="Popup1">
                    <h5 class="heading">Manage Parent Menu</h5>
                    <div class="modal-body" runat="server" style="width: 620px;">
                        <div id="ModelPopupBlock">
                            <div class="row">
                                <div class="col-md-5">
                                    <asp:Label ID="Label5" CssClass="label" runat="server" Text="Enter Sequence No."></asp:Label>:
                                </div>
                                <div class="col-md-7">
                                    <asp:TextBox ID="txtParentMenuSEQNO" CssClass="form-control-model required1" runat="server" Style="width: 250px;"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-5">
                                    <asp:Label ID="Label4" CssClass="label" runat="server" Text="Enter Parent Menu Name"></asp:Label>:
                                </div>
                                <div class="col-md-7">
                                    <asp:TextBox ID="txtParentMenu" CssClass="form-control-model required1" runat="server" Style="width: 250px;"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-3">
                                    &nbsp;
                                </div>
                                <div class="col-md-9">
                                    <asp:Button ID="btnSubmitParentMenu" runat="server" Style="margin-left: 37px; height: 34px; width: 71px; font-size: 14px;" CssClass="btn btn-DarkGreen cls-btnSave1"
                                        Text="Submit" OnClick="btnSubmitParentMenu_Click" />
                                    &nbsp;
                                <asp:Button ID="btnUpdateParentMenu" runat="server" Style="margin-left: 37px; height: 34px; width: 71px; font-size: 14px;" CssClass="btn btn-DarkGreen cls-btnSave1"
                                    Visible="false" Text="Update" OnClick="btnUpdateParentMenu_Click" />
                                    &nbsp;
                                <asp:Button ID="btnCancelParentMenu" runat="server" Text="Cancel"
                                    CssClass="btn btn-warning" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="btnCancelParentMenu_Click" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div style="width: 100%; max-height: 264px; overflow: auto;">
                                    <div>
                                        <asp:GridView ID="grdParentMenu" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                            Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdParentMenu_RowCommand" OnPreRender="grdSteps_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                            CommandName="EditMenu" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sequence No." ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblParentMenuSEQNO" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Parent Menu" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblParentMenu" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('NIWRS_NAV_PARENT', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtndelete" runat="server" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this parent menu : ", Eval("Name")) %>' CommandArgument='<%# Bind("ID") %>'
                                                            CommandName="DeleteMenu" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="popup_AuditTrail_NEW" title="Audit Trail" style="z-index: 999999" class="disp-none">
                        <div id="DivAuditTrail_NEW" style="font-size: small;">
                        </div>
                    </div>
                </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnsubmit" />
            <asp:PostBackTrigger ControlID="btnUpdate" />
            <asp:PostBackTrigger ControlID="lbtnExport" />
            <asp:PostBackTrigger ControlID="btnSubmitParentMenu" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
