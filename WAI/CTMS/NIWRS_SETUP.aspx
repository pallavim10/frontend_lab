<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NIWRS_SETUP.aspx.cs" Inherits="CTMS.NIWRS_SETUP" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="CommonFunctionsJs/IWRS/IWRS_ConfirmMsg.js"></script>
    <script src="CommonFunctionsJs/IWRS/IWRS_showAuditTrail.js"></script>
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <style type="text/css">
        .select2-container .select2-selection--multiple {
            min-height: 60px;
            width: 300px;
        }

        .fixTableHead {
            overflow-y: auto;
            max-height: 350px;
            min-height: 300px;
        }
    </style>
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

            $(function () {
                $(".Datatable").parent().parent().addClass('fixTableHead');
            });

        }

        function ConfigFrozen_MSG() {
            alert('Configuration has been Frozen');
            return false;
        }

        function Set_Condition(element) {
            var ID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var test = "NIWRS_SETUP_CONDITION.aspx?ID=" + ID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=250,width=1000";
            popupWindow = window.open(test, '_blank', strWinProperty);
            return false;
        }

        function Set_FormSpec(element) {
            var ID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var test = "IWRS_MngForms.aspx?FORMID=" + ID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=550,width=1350";
            popupWindow = window.open(test, '_blank', strWinProperty);
            return false;
        }

        function Set_OnSubmitCrits(element) {
            var ID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var test = "NIWRS_SETUP_FORM_OnSubmitCrits.aspx?ID=" + ID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=600,width=1300";
            popupWindow = window.open(test, '_blank', strWinProperty);
            return false;
        }
        function Iwrs_Mappings(element) {
            var ID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var test = "IWRS_MAPPING.aspx?ID=" + ID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=600,width=1300";
            popupWindow = window.open(test, '_blank', strWinProperty);
            return false;
        }

        function AddFields(element) {
            var ID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var test = "NIWRS_SETUP_ADDFIELDS.aspx?ID=" + ID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=340,width=1000";
            popupWindow = window.open(test, '_blank', strWinProperty);
            return false;
        }

        function showStatusCondition() {

            var chkIWRSGraph = document.getElementById('MainContent_chkIWRSGraph');
            var chkIWRSTile = document.getElementById('MainContent_chkIWRSTile');

            if ($(chkIWRSGraph).prop('checked') == true || $(chkIWRSTile).prop('checked') == true) {
                $('#MainContent_ddlStatusCondition').removeClass('disp-none');
            }
            else {
                $('#MainContent_ddlStatusCondition').addClass('disp-none');
            }

        }

        function Set_Multiple_Dependency(element) {
            var VISITNUM = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var test = "NIWRS_SETUP_Multiple_Dependency.aspx?VISITNUM=" + VISITNUM;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=550,width=1350";
            popupWindow = window.open(test, '_blank', strWinProperty);
            return false;
        }

        
        function NIWRS_MANAGE_SUBJECT_CRITERIA(element) {
            
            var test = "NIWRS_MANAGE_SUBJECT_CRITERIA.aspx?";
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=550,width=1350";
            popupWindow = window.open(test, '_blank', strWinProperty);
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

        $(document).on("click", ".cls-btnSave5", function () {
            var test = "0";

            $('.required5').each(function (index, element) {
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

        $(document).on("click", ".cls-btnSave6", function () {
            var test = "0";

            $('.required6').each(function (index, element) {
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

        $(document).on("click", ".cls-btnSave7", function () {
            var test = "0";

            $('.required7').each(function (index, element) {
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

        $(document).on("click", ".cls-btnSave8", function () {
            var test = "0";

            $('.required8').each(function (index, element) {
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

        $(document).on("click", ".cls-btnSave9", function () {
            var test = "0";

            $('.required9').each(function (index, element) {
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



        $(document).on("click", ".cls-btnSave10", function () {
            var test = "0";

            $('.required10').each(function (index, element) {
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



        $(document).on("click", ".cls-btnSave11", function () {
            var test = "0";

            $('.required11').each(function (index, element) {
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

        $(document).on("click", ".cls-btnSave12", function () {
            var test = "0";

            $('.required12').each(function (index, element) {
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
        $(document).on("click", ".cls-btnSave13", function () {
            var test = "0";

            $('.required13').each(function (index, element) {
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
        $(document).on("click", ".cls-btnSave14", function () {
            var test = "0";

            $('.required14').each(function (index, element) {
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
    <script type="text/javascript">
        $(document).ready(function () {
            $('.noSpace').keypress(function (e) {
                if (e.which === 32) {
                    return false;
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField runat="server" ID="hdnREVIEWSTATUS" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">Basic Configuration</h3>
                </div>
                <div class="form-group">
                    <div class="form-group has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="box box-primary" id="div24" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Participant ID nomenclature</h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="lbtnSUBJIDAuditTrail" runat="server" OnClientClick="return showAuditTrail('NIWRS_SETUP_QUES_SUBJECTTEXT', 1);" Font-Size="12px" Style="margin-top: 3px;" CssClass="btn btn-info" ForeColor="White">Audit Trail&nbsp;<span class="fa fa-clock-o"></span></asp:LinkButton>
                                    &nbsp;&nbsp;                       
                                </div>
                            </div>
                            <div class="rows">
                                <div style="height: 120px;">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-5">
                                                <label>Participant id must be showns :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:TextBox ID="txtSubjectID" runat="server" CssClass="form-control required8 width200px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-5">
                                                &nbsp;
                                            </div>
                                            <div class="col-md-7">
                                                <asp:Button ID="btnSubmitQues" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitQues_Click" />
                                                &nbsp;&nbsp;
                                                <asp:Button ID="btnCancelQues" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnCancelQues_Click" />&nbsp;&nbsp; 
                                                <asp:LinkButton ID="btnManaCriQues" runat="server" OnClientClick="return NIWRS_MANAGE_SUBJECT_CRITERIA(this)" Font-Size="12px" Style="margin-top: 3px;" CssClass="btn btn-primary btn-sm" ForeColor="White">&nbsp;Manage Criteria<span class="fa fa-cogs"></span></asp:LinkButton>
                                    
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-primary" id="div17" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Upload Unblinding Report Template </h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="lbtnExportRptTemp" runat="server" OnClientClick="return showAuditTrail('NIWRS_SETUP_QUES_RPT_TEMPLATE', 1);" Font-Size="12px" Style="margin-top: 3px;" CssClass="btn btn-info" ForeColor="White">Audit Trail&nbsp;<span class="fa fa-clock-o"></span></asp:LinkButton>
                                    &nbsp;&nbsp;                       
                                </div>
                            </div>
                            <div class="rows">
                                <div style="height: 120px;">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>Upload Template:</label>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:FileUpload ID="UploadRPTTEMPLATE" runat="server" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                    ControlToValidate="UploadRPTTEMPLATE"
                                                    ValidationExpression="^.*\.(docx|doc)$"
                                                    ErrorMessage="Only Word(.docx/.doc) File is Allow"
                                                    Display="Dynamic"
                                                    ForeColor="Red" />
                                                <br />
                                                <asp:LinkButton ID="lbtnDownloadtemp" runat="server" Text="" Font-Bold="true" ForeColor="Blue" OnClick="lbtnDownloadtemp_Click"></asp:LinkButton>
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
                                                <asp:Button ID="btnUploadTemp" Text="Upload" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnUploadTemp_Click"  />
                                                &nbsp;&nbsp;
                                                <asp:Button ID="btnCancelTemp" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnCancelTemp_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="box box-primary" id="div11" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Sub-Sites</h3>
                            </div>
                            <div class="rows">
                                <div style="height: 264px;">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Select Site:</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:DropDownList runat="server" ID="drpSites" CssClass="form-control required8 width200px"
                                                    AutoPostBack="true" OnSelectedIndexChanged="drpSites_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Enter Sub-Site ID. :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:TextBox Style="width: 60px;" MaxLength="5" ID="txtSubSiteID" runat="server"
                                                    CssClass="form-control numeric required8">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Enter Sub-Site Name :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:TextBox ID="txtSubSiteName" runat="server" CssClass="form-control required8 width200px"></asp:TextBox>
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
                                                <asp:Button ID="btnSubmitSubSite" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave8"
                                                    OnClick="btnSubmitSubSite_Click" />
                                                <asp:Button ID="btnUpdateSubSite" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave8"
                                                    OnClick="btnUpdateSubSite_Click" />
                                                &nbsp;&nbsp;
                                        <asp:Button ID="btnCancelSubSite" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                            OnClick="btnCancelSubSite_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-primary" id="div12" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Sub-Sites</h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="lbnsubsiteExport" runat="server" OnClick="lbnsubsiteExport_Click" Font-Size="12px" Style="margin-top: 3px;" CssClass="btn btn-info" ForeColor="White">Export Sub-Sites&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                                    &nbsp;&nbsp;
                       
                                </div>
                            </div>
                            <div class="rows" style="margin-top: 5px;">
                                <div>
                                    <asp:GridView ID="grdSubSites" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                        Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdSubSites_RowCommand" OnPreRender="grdSubSites_PreRender" OnRowDataBound="grdSubSites_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none" HeaderText="ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="LabelID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                        CommandName="EditSubSite" ToolTip="Edit">
                                                        <i class="fa fa-edit"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sub-Site ID" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("SubSiteID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sub-Site Name" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblname" runat="server" Text='<%# Bind("SubSiteName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('NIWRS_SUBSITES', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color: blue; font-size: 15px"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtndeleteSection" runat="server" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this sub-site : ", Eval("SubSiteName")) %>' CommandArgument='<%# Bind("ID") %>'
                                                        CommandName="DeleteSubSite" ToolTip="Delete">
                                                        <i class="fa fa-trash-o"></i>
                                                    </asp:LinkButton>
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
              <div class="box-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="box box-primary" id="div3" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Status</h3>
                            </div>
                            <div class="rows">
                                <div style="height: 264px;">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Enter Status Code. :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:TextBox Style="width: 60px;" MaxLength="5" ID="txtStatusCode" runat="server"
                                                    CssClass="form-control numeric required1">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Enter Status Name :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:TextBox ID="txtStatusName" runat="server" CssClass="form-control width200px required1"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Dashboard :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <div class="col-md-4">
                                                    <asp:CheckBox runat="server" ID="chkIWRSGraph" onchange="showStatusCondition()" />
                                                    &nbsp;Graph
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:CheckBox runat="server" ID="chkIWRSTile" onchange="showStatusCondition()" />
                                                    &nbsp;Tile
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList runat="server" ID="ddlStatusCondition" CssClass="form-control width60px required disp-none">
                                                        <asp:ListItem Selected="True" Text="=" Value="="></asp:ListItem>
                                                        <asp:ListItem Text="!=" Value="!="></asp:ListItem>
                                                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                                                        <asp:ListItem Text="=<" Value="=<"></asp:ListItem>
                                                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                                                        <asp:ListItem Text="=>" Value="=>"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
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
                                                <asp:Button ID="btnsubmitStatus" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave1"
                                                    OnClick="btnsubmitStatus_Click" />
                                                <asp:Button ID="btnupdateStatus" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave1"
                                                    OnClick="btnupdateStatus_Click" />
                                                &nbsp;&nbsp;
                                                <asp:Button ID="btndeleteStatus" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                    OnClick="btncancelnStatus_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-primary" id="div4" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Status</h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="LbtnStatusExport" runat="server" OnClick="LbtnStatusExport_Click" Font-Size="12px" CssClass="btn btn-info" Style="margin-top: 3px;" ForeColor="White">Export Status&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                                    &nbsp;&nbsp; 
                                </div>
                            </div>
                            <div class="rows" style="margin-top: 5px;">
                                <div>
                                    <asp:GridView ID="grdStatus" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                        Style="width: 96%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdStatus_RowCommand" OnRowDataBound="grdStatus_RowDataBound" OnPreRender="grdSubSites_PreRender">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none" HeaderText="ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="LabelID2" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                        CommandName="EditStatus" ToolTip="Edit">
                                                        <i class="fa fa-edit"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status Code" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSecSEQ" runat="server" Text='<%# Bind("STATUSCODE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status Name" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcategory" runat="server" Text='<%# Bind("STATUSNAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('NIWRS_STATUS', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color: blue; font-size: 15px"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtndeleteSection" runat="server" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this status : ", Eval("STATUSNAME")) %>' CommandArgument='<%# Bind("ID") %>'
                                                        CommandName="DeleteStatus" ToolTip="Delete">
                                                        <i class="fa fa-trash-o"></i>
                                                    </asp:LinkButton>
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
              <div class="box-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="box box-primary" id="div5" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Forms</h3>
                            </div>
                            <div class="rows">
                                <div style="height: 264px;">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Enter Form Name. :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:TextBox ID="txtFormName" runat="server" CssClass="form-control width200px required2"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Select Module :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:DropDownList runat="server" ID="ddlModule" AutoPostBack="true" CssClass="form-control required2 width200px"
                                                    OnSelectedIndexChanged="ddlModule_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Select Fields for Strata (if required) :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:ListBox ID="lstFIELDS" runat="server" CssClass="width300px select" SelectionMode="Multiple"></asp:ListBox>
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
                                                <asp:Button ID="btnSubmitForm" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave2"
                                                    OnClick="btnSubmitForm_Click" />
                                                <asp:Button ID="btnUpdateForm" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave2"
                                                    OnClick="btnUpdateForm_Click" />
                                                &nbsp;&nbsp;
                                        <asp:Button ID="btnCancelForm" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                            OnClick="btnCancelForm_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-primary" id="div6" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Forms</h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="LbtnFormsExport" runat="server" OnClick="LbtnFormsExport_Click" Font-Size="12px" CssClass="btn btn-info" Style="margin-top: 3px;" ForeColor="White">Export Forms&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                                    &nbsp;&nbsp;
                                </div>
                            </div>
                            <div class="rows" style="margin-top: 5px;">
                                <div>
                                    <asp:GridView ID="grdForm" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                        Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdForm_RowCommand" OnRowDataBound="grdForm_RowDataBound" OnPreRender="grdSubSites_PreRender">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFORMID" Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                        CommandName="EditForm" ToolTip="Edit">
                                                        <i class="fa fa-edit"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Form Name" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFORMNAME" runat="server" Text='<%# Bind("FORMNAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Module Name" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMODULENAME" runat="server" Text='<%# Bind("MODULENAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('NIWRS_SETUP_FORMS', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color: blue; font-size: 15px"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtndeleteSection" runat="server" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this form : ", Eval("FORMNAME")) %>' CommandArgument='<%# Bind("ID") %>'
                                                        CommandName="DeleteForm" ToolTip="Delete">
                                                        <i class="fa fa-trash-o"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Define Form Specifications" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnSpecs" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                        OnClientClick="return Set_FormSpec(this)" ToolTip="Define Form Specifications">
                                                        <i class="fa fa-cog"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Define OnSubmit Criterias" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnOnSubmitCrit" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                        OnClientClick="return Set_OnSubmitCrits(this)" ToolTip="Define OnSubmit Criterias">
                                                        <i class="fa fa-asterisk"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IWRS Mapping" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LbtnIwrsMapping" runat="server" CommandArgument='<%# Bind("ID") %>' OnClientClick="return Iwrs_Mappings(this)" ToolTip="IWRS Mapping"><i class="fa fa-print"></i></asp:LinkButton>
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
            <div class="box-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="box box-primary" id="div1" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Visits</h3>
                            </div>
                            <div class="rows">
                                <div style="min-height: 264px;">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Enter Visit No. :</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox Style="width: 60px;" MaxLength="5" ID="txtVisitSEQNO" runat="server"
                                                    CssClass="form-control numeric required">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-md-5">
                                                <label>
                                                    Dosing Visit</label>&nbsp;&nbsp;:&nbsp;&nbsp;
                                                <asp:CheckBox runat="server" ID="chkDosing" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Enter Visit Seq For Summary. :</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox Style="width: 60px;" MaxLength="5" ID="txtVisitSummarySeq" runat="server"
                                                    CssClass="form-control numeric">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-md-5">
                                                <label>
                                                    Add to Visit Summary</label>&nbsp;&nbsp;:&nbsp;&nbsp;
                                                <asp:CheckBox runat="server" ID="chkVisitSummary" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Enter Visit Name :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:TextBox ID="txtVisitName" runat="server" CssClass="form-control required width200px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Select Dependency :</label>
                                            </div>
                                            <div class="col-md-7" style="display: inline-flex;">
                                                <asp:DropDownList runat="server" ID="ddlVisModule" CssClass="form-control width200px"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlVisModule_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:DropDownList runat="server" Visible="false" ID="ddlVisField" CssClass="form-control required width200px">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Enter Window Period :</label>
                                            </div>
                                            <div class="col-md-7" style="display: inline-flex;" runat="server" id="divtxtWindow">
                                                <asp:TextBox Style="width: 60px;" MaxLength="5" ID="txtWindow" runat="server" CssClass="form-control numeric "></asp:TextBox>
                                                &nbsp;&nbsp;&nbsp;Days
                                            </div>
                                            <div class="col-md-7" style="display: inline-flex;" id="divddlWindows" runat="server">
                                                <asp:DropDownList ID="ddlWinPeroids" runat="server" CssClass="form-control width200px required"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Value="NONE">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="EARLIEST">EARLIEST</asp:ListItem>
                                                    <asp:ListItem Value="LATEST">LATEST</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Enter Early Window Period :</label>
                                            </div>
                                            <div class="col-md-7" style="display: inline-flex;" runat="server" id="divtxtearlyWindow">
                                                <asp:TextBox Style="width: 60px;" MaxLength="5" ID="txtEarly" runat="server" CssClass="form-control numeric "></asp:TextBox>
                                                &nbsp;&nbsp;&nbsp;Days
                                            </div>
                                            <div class="col-md-7" style="display: inline-flex;" id="divddlEarly" runat="server">
                                                <asp:DropDownList ID="EarlyWinPeroid" runat="server" CssClass="form-control width200px required"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Value="NONE">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="EARLIEST">EARLIEST</asp:ListItem>
                                                    <asp:ListItem Value="LATEST">LATEST</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Enter Late Window Period :</label>
                                            </div>
                                            <div class="col-md-7" style="display: inline-flex;" id="divtxtlateWindow" runat="server">
                                                <asp:TextBox Style="width: 60px;" MaxLength="5" ID="txtLate" runat="server" CssClass="form-control numeric "></asp:TextBox>
                                                &nbsp;&nbsp;&nbsp;Days
                                            </div>
                                            <div class="col-md-7" style="display: inline-flex;" id="divddllate" runat="server">
                                                <asp:DropDownList ID="LateWinPeroid" runat="server" CssClass="form-control width200px required"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Value="NONE">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="EARLIEST">EARLIEST</asp:ListItem>
                                                    <asp:ListItem Value="LATEST">LATEST</asp:ListItem>
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
                                                <asp:Button ID="btnsubmitVisit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                    OnClick="btnsubmitVisit_Click" />
                                                <asp:Button ID="btnupdateVisit" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                    OnClick="btnupdateVisit_Click" />
                                                &nbsp;&nbsp;
                                                <asp:Button ID="btncancelnVisit" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                    OnClick="btncancelnVisit_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-primary" id="div2" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Visits</h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="LbtnVisitExport" runat="server" OnClick="LbtnVisitExport_Click" CssClass="btn btn-info" ForeColor="White" Font-Size="12px" Style="margin-top: 3px;">Export Visits&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                                    &nbsp;&nbsp;
                                </div>
                            </div>
                            <div class="rows" style="margin-top: 5px;">
                                <div>
                                    <asp:GridView ID="grdVisit" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                        Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdVisit_RowCommand" OnRowDataBound="grdVisit_RowDataBound" OnPreRender="grdSubSites_PreRender">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none" HeaderText="ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="LabelID1" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                        CommandName="EditVisit" ToolTip="Edit">
                                                        <i class="fa fa-edit"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Visit No." ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSecSEQ" runat="server" Text='<%# Bind("VISITNUM") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Visit Name" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcategory" runat="server" Text='<%# Bind("VISIT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Manage Multiple Dependency" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnmultipleDependency" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                        ToolTip="Manage Multiple Dependency" Visible="false" OnClientClick="return Set_Multiple_Dependency(this)">
                                                        <i class="fa fa-cog"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('NIWRS_VISITDETAILS', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color: blue; font-size: 15px"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtndeleteSection" runat="server" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this visit : ", Eval("VISIT")) %>' CommandArgument='<%# Bind("ID") %>'
                                                        CommandName="DeleteVisit" ToolTip="Delete">
                                                        <i class="fa fa-trash-o"></i>
                                                    </asp:LinkButton>
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
          
            <div class="box-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="box box-primary" id="div9" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Additional Fields (if required for Workflow)</h3>
                            </div>
                            <div class="rows">
                                <div style="height: 264px;">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Enter Field Name :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:TextBox ID="txtFieldName" runat="server" CssClass="form-control width200px required4"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Enter Column Name :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:TextBox ID="txtColName" runat="server" CssClass="form-control width200px required4"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Dashboard :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <div class="col-md-4">
                                                    <asp:CheckBox runat="server" ID="chkColGraph" />
                                                    &nbsp;Graph
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:CheckBox runat="server" ID="chkColTile" />
                                                    &nbsp;Tile
                                                </div>
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
                                                <asp:Button ID="btnSubmitCol" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave4"
                                                    OnClick="btnSubmitCol_Click" />
                                                <asp:Button ID="btnUpdateCol" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave4"
                                                    OnClick="btnUpdateCol_Click" />
                                                &nbsp;&nbsp;
                                                <asp:Button ID="btnCancelCol" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                    OnClick="btnCancelCol_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-primary" id="div10" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Additional Fields</h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="LbtnAddFieldExport" runat="server" OnClick="LbtnAddFieldExport_Click" Font-Size="12px" CssClass="btn btn-info" ForeColor="White" Style="margin-top: 3px;">Export Additional&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                                    &nbsp;&nbsp; 
                                </div>
                            </div>
                            <div class="rows" style="margin-top: 5px;">
                                <div>
                                    <asp:GridView ID="grdCols" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                        Style="width: 95%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdCols_RowCommand" OnRowDataBound="grdCols_RowDataBound" OnPreRender="grdSubSites_PreRender">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none" HeaderText="ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="LabelID3" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                        CommandName="EditCol" ToolTip="Edit">
                                                        <i class="fa fa-edit"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Field Name" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFIELDNAME" runat="server" Text='<%# Bind("FIELDNAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Column Name" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCOL_NAME" runat="server" Text='<%# Bind("COL_NAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('NIWRS_SETUP_COLS', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color: blue; font-size: 15px"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtndeleteSection" runat="server" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this field : ", Eval("FIELDNAME")) %>' CommandArgument='<%# Bind("ID") %>'
                                                        CommandName="DeleteCol" ToolTip="Delete">
                                                        <i class="fa fa-trash-o"></i>
                                                    </asp:LinkButton>
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
                <div class="box-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="box box-primary" id="div7" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Listing</h3>
                            </div>
                            <div class="rows">
                                <div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Enter Listing Name. :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:TextBox ID="txtListName" runat="server" CssClass="form-control width200px required3"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Select Module :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:ListBox ID="lstModule" AutoPostBack="true" runat="server" CssClass="width300px select"
                                                    SelectionMode="Multiple" OnSelectedIndexChanged="lstModule_SelectedIndexChanged"></asp:ListBox>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Select Fields :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <div style="width: 100%; height: 264px; overflow: auto;">
                                                    <asp:GridView ID="grd_Field" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                                        CssClass="table table-bordered table-striped table-striped1" OnRowDataBound="grd_Field_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMODULEID" Text='<%# Eval("MODULEID") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkListing" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Seq. No." HeaderStyle-Width="20%" HeaderStyle-CssClass="txt_center"
                                                                ItemStyle-CssClass="txt_center">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtSEQNO" Text='<%# Eval("NIWRS_SEQNO") %>' Style="text-align: center;"
                                                                        runat="server" Width="100%" CssClass="form-control">
                                                                    </asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Field Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblField" Text='<%# Eval("FIELDNAME") %>' ToolTip='<%# Eval("MODULENAME") %>'
                                                                        runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
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
                                                <asp:Button ID="btnSubmitList" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave3"
                                                    OnClick="btnSubmitList_Click" />
                                                <asp:Button ID="btnUpdateList" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave3"
                                                    OnClick="btnUpdateList_Click" />
                                                &nbsp;&nbsp;
                                        <asp:Button ID="btnCancelList" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                            OnClick="btnCancelList_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-primary" id="div8" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Listings</h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="LbtnListingExport" runat="server" OnClick="LbtnListingExport_Click" CssClass="btn btn-info" Style="margin-top: 3px;" Font-Size="12px" ForeColor="White">Export Listings&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                                    &nbsp;&nbsp; 
                                </div>
                            </div>
                            <div class="rows" style="margin-top: 5px;">
                                <div>
                                    <asp:GridView ID="grdList" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                        Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdList_RowCommand"
                                        OnRowDataBound="grdList_RowDataBound" OnPreRender="grdSubSites_PreRender">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID" ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                                                HeaderStyle-CssClass="txt_center">
                                                <HeaderTemplate>
                                                    <a href="JavaScript:ManipulateAll('_Field_');" id="_Folder" style="color: #333333"><i
                                                        id="img_Field_" class="icon-plus-sign-alt"></i></a>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div runat="server" id="anchor">
                                                        <a href="JavaScript:divexpandcollapse('_Field_<%# Eval("ID") %>');" style="color: #333333">
                                                            <i id="img_Field_<%# Eval("ID") %>" class="icon-plus-sign-alt"></i></a>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                        CommandName="EditList" ToolTip="Edit">
                                                        <i class="fa fa-edit"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Listing Name" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLISTNAME" runat="server" Text='<%# Bind("LISTNAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('NIWRS_SETUP_LISTINGS', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color: blue; font-size: 15px"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Set Condition" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnCondition" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                        ToolTip="Set Condition" OnClientClick="return Set_Condition(this)">
                                                        &nbsp;  <i class="fa fa-cog"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Manage Additional Fields" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnAddFields" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                        ToolTip="Manage Additional Fields" OnClientClick="return AddFields(this)">
                                                        <i class="fa fa-plus-circle"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtndeleteSection" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                        CommandName="DeleteList" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this listing : ", Eval("LISTNAME")) %>' ToolTip="Delete">
                                                        <i class="fa fa-trash-o"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td colspan="100%" style="padding: 2px; padding-left: 4%;">
                                                            <div style="float: right; font-size: 13px;">
                                                            </div>
                                                            <div>
                                                                <div id="_Field_<%# Eval("ID") %>" style="display: none; position: relative; overflow: auto;">
                                                                    <asp:GridView ID="grdListField" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                                                        CssClass="table table-bordered table-striped table-striped1">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="SEQNO" HeaderStyle-Width="20%" HeaderStyle-CssClass="txt_center"
                                                                                ItemStyle-CssClass="txt_center">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="txtSEQNO" Text='<%# Eval("SEQNO") %>' Style="text-align: center;"
                                                                                        runat="server"></asp:Label>
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
                        </div>
                    </div>
                </div>
            </div>
            <div runat="server" id="divStrata" class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Define Stratifications</h3>
                </div>
                <div class="rows">
                    <div class="row">
                        <div class="col-md-12">
                            <div>
                                <asp:GridView ID="grdStrataFields" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                    Style="width: 96%; border-collapse: collapse; margin-left: 20px;" OnRowDataBound="grdStrataFields_RowDataBound" OnPreRender="grdSubSites_PreRender">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFIELDID" runat="server" Text='<%# Bind("FIELDID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVARIABLENAME" runat="server" Text='<%# Bind("VARIABLENAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Module Name" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMODULENAME" runat="server" Text='<%# Bind("MODULENAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Field Name" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFIELDNAME" runat="server" Text='<%# Bind("FIELDNAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <tr>
                                                    <td colspan="100%" style="padding: 2px;">
                                                        <div>
                                                            <div class="rows">
                                                                <div class="col-md-12">
                                                                    <asp:GridView ID="grdANS_STRATA" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped">
                                                                        <Columns>
                                                                            <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Sequence No.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSEQNO" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Answers">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblANS" runat="server" Text='<%# Bind("TEXT") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Strata" ItemStyle-CssClass="txt_center" ControlStyle-CssClass="txt_center form-control">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtSTRATA" runat="server" Style="display: inherit;" Text='<%# Bind("STRATA") %>'></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <RowStyle ForeColor="Blue" />
                                                                        <HeaderStyle ForeColor="Blue" />
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
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-4">
                                &nbsp;
                            </div>
                            <div class="col-md-7">
                                <asp:Button ID="btnSubmitSTRATA" Visible="false" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                    OnClick="btnSubmitSTRATA_Click" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnCancelSTRATA" Visible="false" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                    OnClick="btnCancelSTRATA_Click" />
                            </div>
                        </div>
                    </div>
                    <br />
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="box box-primary" id="div13" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Graphs</h3>
                            </div>
                            <div class="rows">
                                <div style="height: 264px;">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Enter Graph Header :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:TextBox ID="txtGraphHeader" runat="server" CssClass="form-control width200px required9"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Select Form :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:DropDownList runat="server" ID="ddlGraphForm" AutoPostBack="true" CssClass="form-control required9 width200px"
                                                    OnSelectedIndexChanged="ddlGraphForm_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Select Field :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:DropDownList runat="server" ID="ddlGraphField" CssClass="form-control required9 width200px">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Select Graph Type :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <div class="col-md-4">
                                                    <asp:CheckBox runat="server" ID="chkBar" />
                                                    &nbsp;Bar
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:CheckBox runat="server" ID="chkPie" />
                                                    &nbsp;Pie
                                                </div>
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
                                                <asp:Button ID="btnSubmitGraph" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave9"
                                                    OnClick="btnSubmitGraph_Click" />
                                                <asp:Button ID="btnUpdateGraph" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave9"
                                                    OnClick="btnUpdateGraph_Click" />
                                                &nbsp;&nbsp;
                                        <asp:Button ID="btnCancelGraph" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                            OnClick="btnCancelGraph_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-primary" id="div14" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Graphs</h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="LbtnGraphExport" runat="server" OnClick="LbtnGraphExport_Click" CssClass="btn btn-info" Font-Size="12px" ForeColor="White" Style="margin-top: 3px;">Export Graphs&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                                    &nbsp;&nbsp;
                                </div>
                            </div>
                            <div class="rows" style="margin-top: 5px;">
                                <div>
                                    <asp:GridView ID="grdGraphs" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                        Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdGraphs_RowCommand" OnPreRender="grdSubSites_PreRender" OnRowDataBound="grdGraphs_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID" ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID1" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                        CommandName="EditGraph" ToolTip="Edit">
                                                        <i class="fa fa-edit"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Header" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHeader" runat="server" Text='<%# Bind("HEADER") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Field Name" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFIELDNAME" runat="server" Text='<%# Bind("FIELDNAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('NIWRS_GRAPHS', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color: blue; font-size: 15px"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtndeleteSection" runat="server" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this graph : ", Eval("HEADER")) %>' CommandArgument='<%# Bind("ID") %>'
                                                        CommandName="DeleteGraph" ToolTip="Delete">
                                                        <i class="fa fa-trash-o"></i>
                                                    </asp:LinkButton>
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
            <div class="box-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="box box-primary" id="div15" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Status Dashboard</h3>
                            </div>
                            <div class="rows">
                                <div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Enter Graph/Tile Header :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:TextBox ID="txtDashboardHeader" runat="server" CssClass="form-control width200px required10"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Enter Seq. No. :</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox Style="width: 60px;" MaxLength="5" ID="txtDashboardSeqNo" runat="server"
                                                    CssClass="form-control numeric required10">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-md-5">
                                                <label>
                                                    Enrollment Record
                                                </label>
                                                &nbsp;&nbsp;:&nbsp;&nbsp;
                                                <asp:CheckBox runat="server" ID="chkDashboardStatusEnroll" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Dashboard :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <div class="col-md-4">
                                                    <asp:CheckBox runat="server" ID="chkDashboardStatusGraph" />
                                                    &nbsp;Graph
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:CheckBox runat="server" ID="chkDashboardStatusTile" />
                                                    &nbsp;Tile
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Select Status :</label>
                                            </div>
                                            <div class="col-md-8">
                                                <div style="width: 100%; height: 264px; overflow: auto;">
                                                    <asp:GridView ID="gvDashboardStatus" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                                        CssClass="table table-bordered table-striped table-striped1 Datatable" OnRowDataBound="gvDashboardStatus_RowDataBound" OnPreRender="grdSubSites_PreRender">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Select" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkDashboard" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="disp-none"
                                                                ItemStyle-CssClass="disp-none">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList runat="server" Width="100%" ID="ddlCondition" CssClass="form-control">
                                                                        <asp:ListItem Text="+" Value="+"></asp:ListItem>
                                                                        <asp:ListItem Text="-" Value="-"></asp:ListItem>
                                                                        <asp:ListItem Text="x" Value="*"></asp:ListItem>
                                                                        <asp:ListItem Text="/" Value="/"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Seq. No." HeaderStyle-Width="20%" HeaderStyle-CssClass="txt_center"
                                                                ItemStyle-CssClass="txt_center">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtSEQNO" Text='<%# Eval("SEQNO") %>' Style="text-align: center;"
                                                                        MaxLength="5" runat="server" Width="100%" CssClass="form-control">
                                                                    </asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status Code" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSTATUSCODE" Text='<%# Eval("STATUSCODE") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSTATUSNAME" Text='<%# Eval("STATUSNAME") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
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
                                                <asp:Button ID="btnSubmit_Dashborad" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave10"
                                                    OnClick="btnSubmit_Dashborad_Click" />
                                                <asp:Button ID="btnUpdate_Dashborad" Text="Update" Visible="false" runat="server"
                                                    CssClass="btn btn-primary btn-sm cls-btnSave10" OnClick="btnUpdate_Dashborad_Click" />
                                                &nbsp;&nbsp;
                                                <asp:Button ID="btnCancel_Dashborad" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                    OnClick="btnCancel_Dashborad_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-primary" id="div16" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Status Dashboard</h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="LbtnDashboardExport" runat="server" OnClick="LbtnDashboardExport_Click" CssClass="btn btn-info" Font-Size="12px" ForeColor="White" Style="margin-top: 3px;">Export Dashboard&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                                    &nbsp;&nbsp; 
                                </div>
                            </div>
                            <div class="rows" style="margin-top: 5px;">
                                <div>
                                    <asp:GridView ID="gvStatusDashboard" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                        Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="gvStatusDashboard_RowCommand" OnPreRender="grdSubSites_PreRender" OnRowDataBound="gvStatusDashboard_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID" ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID3" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                        CommandName="EditDash" ToolTip="Edit">
                                                        <i class="fa fa-edit"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Seq. No." ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSEQNO" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Header" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDashboard" runat="server" Text='<%# Bind("Header") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('NIWRS_DASHBOARD', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color: blue; font-size: 15px"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtndelete" runat="server" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this status dashboard : ", Eval("Header")) %>' CommandArgument='<%# Bind("ID") %>'
                                                        CommandName="DeleteDash" ToolTip="Delete">
                                                        <i class="fa fa-trash-o"></i>
                                                    </asp:LinkButton>
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
            
            <div class="box-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="box box-primary" id="div19" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Reasons for Unblinding</h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="LbtnReaUnbliExport" runat="server" OnClick="LbtnReaUnbliExport_Click" CssClass="btn btn-info" Font-Size="12px" ForeColor="White" Style="margin-top: 3px;">Export Unblinding&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                                    &nbsp;&nbsp;
                                </div>
                            </div>
                            <div class="rows">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <label>
                                                Enter Sequence No. :</label>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:TextBox Style="width: 60px;" MaxLength="5" ID="txtUnblindSeqNo" runat="server"
                                                CssClass="form-control numeric required11">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <label>
                                                Enter Reason :</label>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:TextBox ID="txtUnblindReason" runat="server" CssClass="form-control required11 width200px"></asp:TextBox>
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
                                            <asp:Button ID="btnSubmitUnblindReason" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave11"
                                                OnClick="btnSubmitUnblindReason_Click" />
                                            <asp:Button ID="btnUpdateUnblindReason" Text="Update" Visible="false" runat="server"
                                                CssClass="btn btn-primary btn-sm cls-btnSave11" OnClick="btnUpdateUnblindReason_Click" />
                                            &nbsp;&nbsp;
                                    <asp:Button ID="btnCancelUnblindReason" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                        OnClick="btnCancelUnblindReason_Click" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div style="width: 100%; height: 264px; overflow: auto;">
                                    <div>
                                        <asp:GridView ID="gvUnblindReasons" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                            Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="gvUnblindReasons_RowCommand" OnPreRender="grdSubSites_PreRender" OnRowDataBound="gvUnblindReasons_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none" HeaderText="ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelID5" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                            CommandName="EditReason" ToolTip="Edit">
                                                            <i class="fa fa-edit"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sequence No." ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSEQNO" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reason" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReason" runat="server" Text='<%# Bind("TEXT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('NIWRS_OPTIONS_MASTER', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color: blue; font-size: 15px"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtndelete" runat="server" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this reason for unblinding : ", Eval("TEXT")) %>' CommandArgument='<%# Bind("ID") %>'
                                                            CommandName="DeleteReason" ToolTip="Delete">
                                                            <i class="fa fa-trash-o"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-primary" id="div20" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Reasons for DCF</h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="LbtnReaDCFExport" runat="server" OnClick="LbtnReaDCFExport_Click" CssClass="btn btn-info" Font-Size="12px" ForeColor="White" Style="margin-top: 3px;">Export DCF&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                                    &nbsp;&nbsp;
                                </div>
                            </div>
                            <div class="rows">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <label>
                                                Enter Sequence No. :</label>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:TextBox Style="width: 60px;" MaxLength="5" ID="txtDCFSeqNo" runat="server" CssClass="form-control required12 numeric "></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <label>
                                                Enter Reason :</label>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:TextBox ID="txtDCFReason" runat="server" CssClass="form-control required12  width200px"></asp:TextBox>
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
                                            <asp:Button ID="btnSubmitDCFReason" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave12"
                                                OnClick="btnSubmitDCFReason_Click" />
                                            <asp:Button ID="btnUpdateDCFReason" Text="Update" Visible="false" runat="server"
                                                CssClass="btn btn-primary btn-sm cls-btnSave12" OnClick="btnUpdateDCFReason_Click" />
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnCancelDCFReason" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                OnClick="btnCancelDCFReason_Click" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div style="width: 100%; height: 264px; overflow: auto;">
                                    <div>
                                        <asp:GridView ID="gvDCFReason" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                            Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="gvDCFReason_RowCommand" OnPreRender="grdSubSites_PreRender" OnRowDataBound="gvDCFReason_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none" HeaderText="ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelID6" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                            CommandName="EditReason" ToolTip="Edit">
                                                            <i class="fa fa-edit"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sequence No." ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSEQNO" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reason" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReason" runat="server" Text='<%# Bind("TEXT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('NIWRS_OPTIONS_MASTER', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color: blue; font-size: 15px"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtndelete" runat="server" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this reasons for dcf : ", Eval("TEXT")) %>' CommandArgument='<%# Bind("ID") %>'
                                                            CommandName="DeleteReason" ToolTip="Delete">
                                                            <i class="fa fa-trash-o"></i>
                                                        </asp:LinkButton>
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

            <div class="box-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="box box-primary" id="div22" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Randomization Arms</h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="LbtnRandArmsExport" runat="server" OnClick="LbtnRandArmsExport_Click" CssClass="btn btn-info" Font-Size="12px" ForeColor="White" Style="margin-top: 3px;">Export Randomization&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                                    &nbsp;&nbsp;
                                </div>
                            </div>
                            <div class="rows">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <label>
                                                Enter Treatment Code. :</label>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:TextBox Style="width: 60px;" MaxLength="5" ID="txtTreatmentCode" runat="server"
                                                CssClass="form-control required13 numeric ">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <label>
                                                Enter Treatment Arms :</label>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:TextBox ID="txtTreatmentName" runat="server" CssClass="form-control required13 width200px"></asp:TextBox>
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
                                            <asp:Button ID="btnSubmitRandomizationArm" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave13"
                                                OnClick="btnSubmitRandomizationArm_Click" />
                                            <asp:Button ID="btnUpdateRandomizationArm" Text="Update" Visible="false" runat="server"
                                                CssClass="btn btn-primary btn-sm" OnClick="btnUpdateRandomizationArm_Click" />
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnCancleRandomizationArm" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                OnClick="btnCancleRandomizationArm_Click" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div style="width: 100%; height: 264px; overflow: auto;">
                                    <div>
                                        <asp:GridView ID="grdRandArm" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                            Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdRandArm_RowCommand" OnPreRender="grdSubSites_PreRender" OnRowDataBound="grdRandArm_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none" HeaderText="ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelID7" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                            CommandName="EditRand" ToolTip="Edit">
                                                            <i class="fa fa-edit"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sequence No." ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSEQNO" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Treatment Code" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltreatment" runat="server" Text='<%# Bind("TREAT_GRP") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Treatment Arms" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltreatmegrp" runat="server" Text='<%# Bind("TREAT_GRP_NAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('NIWRS_SETUP_RANDARMS', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color: blue; font-size: 15px"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtndelete" runat="server" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this randomization arms : ", Eval("TREAT_GRP_NAME")) %>' CommandArgument='<%# Bind("ID") %>'
                                                            CommandName="DeleteRand" ToolTip="Delete">
                                                            <i class="fa fa-trash-o"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-primary" id="div23" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Dosing Arms</h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="LbtnDosingArmsExport" runat="server" OnClick="LbtnDosingArmsExport_Click" CssClass="btn btn-info" Font-Size="12px" ForeColor="White" Style="margin-top: 3px;">Export Dosing&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                                    &nbsp;&nbsp;
                                </div>
                            </div>
                            <div class="rows">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <label>
                                                Enter Treatment Code. :</label>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:TextBox Style="width: 60px;" MaxLength="5" ID="txtDosingCode" runat="server" CssClass="form-control required14 numeric "></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <label>
                                                Enter Treatment Arms :</label>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:TextBox ID="txtDosingName" runat="server" CssClass="form-control required14  width200px"></asp:TextBox>
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
                                            <asp:Button ID="btnSubmitDosingArm" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave14"
                                                OnClick="btnSubmitDosingArm_Click" />
                                            <asp:Button ID="btnUpdateDosingArm" Text="Update" Visible="false" runat="server"
                                                CssClass="btn btn-primary btn-sm" OnClick="btnUpdateDosingArm_Click" />
                                            &nbsp;&nbsp;
                                    <asp:Button ID="btnCancelDosingArm" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                        OnClick="btnCancelDosingArm_Click" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div style="width: 100%; height: 264px; overflow: auto;">
                                    <div>
                                        <asp:GridView ID="grdDosArm" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                            Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdDosArm_RowCommand" OnPreRender="grdSubSites_PreRender" OnRowDataBound="grdDosArm_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none" HeaderText="ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelID8" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                            CommandName="EditDose" ToolTip="Edit">
                                                            <i class="fa fa-edit"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sequence No." ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSEQNO" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Treatment Code" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDoseGrp" runat="server" Text='<%# Bind("TREAT_GRP") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Treatment Arms" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDoseName" runat="server" Text='<%# Bind("TREAT_GRP_NAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('NIWRS_SETUP_DOSEARMS', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color: blue; font-size: 15px"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtndelete" runat="server" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this dosing arms : ", Eval("TREAT_GRP_NAME")) %>' CommandArgument='<%# Bind("ID") %>'
                                                            CommandName="DeleteDose" ToolTip="Delete">
                                                            <i class="fa fa-trash-o"></i>
                                                        </asp:LinkButton>
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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmitRandomizationArm" />
            <asp:PostBackTrigger ControlID="btnUpdateRandomizationArm" />
            <asp:PostBackTrigger ControlID="btnSubmitDosingArm" />
            <asp:PostBackTrigger ControlID="btnUpdateDosingArm" />
            <asp:PostBackTrigger ControlID="btnSubmitQues" />
            <asp:PostBackTrigger ControlID="btnUploadTemp" />
            <asp:PostBackTrigger ControlID="btnSubmitSubSite" />
            <asp:PostBackTrigger ControlID="btnUpdateSubSite" />
            <asp:PostBackTrigger ControlID="btnsubmitVisit" />
            <asp:PostBackTrigger ControlID="btnupdateVisit" />
            <asp:PostBackTrigger ControlID="btnsubmitStatus" />
            <asp:PostBackTrigger ControlID="btnupdateStatus" />
            <asp:PostBackTrigger ControlID="btnSubmitCol" />
            <asp:PostBackTrigger ControlID="btnUpdateCol" />
            <asp:PostBackTrigger ControlID="btnSubmitForm" />
            <asp:PostBackTrigger ControlID="btnUpdateForm" />
            <asp:PostBackTrigger ControlID="btnSubmitSTRATA" />
            <asp:PostBackTrigger ControlID="btnSubmitList" />
            <asp:PostBackTrigger ControlID="btnUpdateList" />
            <asp:PostBackTrigger ControlID="btnSubmitGraph" />
            <asp:PostBackTrigger ControlID="btnUpdateGraph" />
            <asp:PostBackTrigger ControlID="btnSubmit_Dashborad" />
            <asp:PostBackTrigger ControlID="btnUpdate_Dashborad" />
            <asp:PostBackTrigger ControlID="btnSubmitUnblindReason" />
            <asp:PostBackTrigger ControlID="btnUpdateUnblindReason" />
            <asp:PostBackTrigger ControlID="btnSubmitDCFReason" />
            <asp:PostBackTrigger ControlID="btnUpdateDCFReason" />
            <asp:PostBackTrigger ControlID="LbtnDosingArmsExport" />
            <asp:PostBackTrigger ControlID="LbtnRandArmsExport" />
            <asp:PostBackTrigger ControlID="LbtnReaDCFExport" />
            <asp:PostBackTrigger ControlID="LbtnReaUnbliExport" />
            <asp:PostBackTrigger ControlID="LbtnDashboardExport" />
            <asp:PostBackTrigger ControlID="LbtnGraphExport" />
            <asp:PostBackTrigger ControlID="LbtnListingExport" />
            <asp:PostBackTrigger ControlID="LbtnGraphExport" />
            <asp:PostBackTrigger ControlID="LbtnListingExport" />
            <asp:PostBackTrigger ControlID="LbtnFormsExport" />
            <asp:PostBackTrigger ControlID="LbtnAddFieldExport" />
            <asp:PostBackTrigger ControlID="LbtnStatusExport" />
            <asp:PostBackTrigger ControlID="LbtnVisitExport" />
            <asp:PostBackTrigger ControlID="lbnsubsiteExport" />
            <asp:PostBackTrigger ControlID="lbtnDownloadtemp" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
