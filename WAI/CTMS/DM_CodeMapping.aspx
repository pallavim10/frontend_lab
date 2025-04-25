<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DM_CodeMapping.aspx.cs" Inherits="CTMS.DM_CodeMapping" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>WAI</title>
    <script src="Scripts/Jquery1.12.4.js" type="text/javascript"></script>
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Common-Bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap-theme.css" rel="stylesheet" type="text/css" />
    <link href="Styles/AdminLTE.css" rel="stylesheet" type="text/css" />
    <link href="Styles/ionicons.css" rel="stylesheet" type="text/css" />
    <link href="Styles/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="Styles/font-awesome.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="img/favicon.ico" type="image/x-icon" />
    <link rel="shortcut icon" href="img/favicon.ico" type="image/x-icon" />
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <!-- for Jquery Popup//-->
    <script src="https://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <link href="Styles/Jquery.ui.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/CommonFunction.js" type="text/javascript"></script>
    <!-- for Jquery Popup//-->
    <!-- for pikaday datepicker//-->
    <link href="Styles/pikaday.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/moment.js" type="text/javascript"></script>
    <script src="Scripts/pikaday.js" type="text/javascript"></script>
    <script src="Scripts/pikaday.jquery.js" type="text/javascript"></script>
    <!-- for pikaday datepicker//-->
    <script src="js/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>
    <script src="js/plugins/datatables/dataTables.bootstrap.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="Styles/datatables/dataTables.bootstrap.css" />
    <script src="js/plugins/moment/moment.min.js" type="text/javascript"></script>
    <script src="js/plugins/moment/datetime-moment.js" type="text/javascript"></script>
    <link href="js/plugins/datatables/jquery.dataTables.css" rel="stylesheet" type="text/css" />
    <!-- Bootstrap -->
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <!-- Morris.js charts -->
    <script src="//cdnjs.cloudflare.com/ajax/libs/raphael/2.1.0/raphael-min.js"></script>
    <%--  <script src="js/plugins/morris/morris.min.js" type="text/javascript"></script>--%>
    <!-- Sparkline -->
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <!-- jvectormap -->
    <script src="js/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js" type="text/javascript"></script>
    <script src="js/plugins/jvectormap/jquery-jvectormap-world-mill-en.js" type="text/javascript"></script>
    <!-- jQuery Knob Chart -->
    <script src="js/plugins/jqueryKnob/jquery.knob.js" type="text/javascript"></script>
    <!-- daterangepicker -->
    <script src="js/plugins/daterangepicker/daterangepicker.js" type="text/javascript"></script>
    <!-- Bootstrap WYSIHTML5 -->
    <script src="js/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js" type="text/javascript"></script>
    <!-- iCheck -->
    <script src="js/plugins/iCheck/icheck.min.js" type="text/javascript"></script>
    <!-- AdminLTE App -->
    <script src="js/AdminLTE/app.js" type="text/javascript"></script>
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script src="CommonFunctionsJs/btnSave_Required.js"></script>
    <script src="js/Input%20Mask/jquery.inputmask.bundle.js"></script>
    <script src="CommonFunctionsJs/ControlJS.js"></script>
    <script src="js/CKEditor/ckeditor.js" type="text/javascript"></script>
    <script src="CommonFunctionsJs/CKEDITOR_Limited.js"></script>
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <script src="js/Input%20Mask/jquery.inputmask.bundle.js"></script>
    <script src="CommonFunctionsJs/DB/DB_showAuditTrail.js"></script>
    <script src="CommonFunctionsJs/DM/DM_ConfirmMsg.js"></script>
    <style type="text/css">
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #0000ff;
        }

        .select2-container--default .select2-selection--single {
            margin-top: 2px;
        }
    </style>
    <script type="text/javascript">

        function DisableDiv() {
            var nodes = document.getElementById("Meddra").getElementsByTagName('*');
            for (var i = 0; i < nodes.length; i++) {
                nodes[i].disabled = true;
            }

            //Get Query String Value
            const params = new Proxy(new URLSearchParams(window.location.search), {
                get: (searchParams, prop) => searchParams.get(prop),
            });

            let FREEZE = params.FREEZE;

            $("#lblStatus").text("This module is " + FREEZE + ".");
        }

        function DisableDiv_grdWhoddData() {

            var nodes = document.getElementById("WHODData").getElementsByTagName('*');
            for (var i = 0; i < nodes.length; i++) {
                nodes[i].disabled = true;
            }

            //Get Query String Value
            const params = new Proxy(new URLSearchParams(window.location.search), {
                get: (searchParams, prop) => searchParams.get(prop),
            });

            let FREEZE = params.FREEZE;

            $("#lblStatus").text("This module is " + FREEZE + ".");
        }
    </script>
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: true,
                fixedHeader: true
            });
        }
    </script>
    <script>
        $(document).on("click", ".cls-btnSave1", function () {
            var test = "0";

            $('.').each(function (index, element) {
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
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div style="margin: 5px">
            <div class="row">
                <div class="lblError">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    <asp:HiddenField ID="hdnModuleStatus" runat="server" />
                </div>
            </div>
            <div class="box box-primary">
                <div class="box-header">
                    <h4 class="box-title" align="left">Code Mapping
                    </h4>
                    <span id="lblStatus" style="font-size: 16px; color: darkorange; font-weight: bold; margin-left: 51px;"></span>
                </div>
                <div class="row" id="divMeddraRecoard" runat="server" visible="false">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border" style="top: 0px; left: 0px;">
                                <h4 class="box-title" align="left">MedDRA Records</h4>
                            </div>
                            <br />
                            <div align="left" style="margin-left: 5px">
                                <div>
                                    <div class="rows">
                                        <div class="fixTableHead">
                                            <asp:GridView ID="grdMeddraData" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                                Style="border-collapse: collapse; width: 99%;" OnPreRender="grdMeddraData_PreRender" OnRowCommand="grdMeddraData_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                CommandName="EditMeddra" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="System Organ Class Column" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SOCNM" runat="server" Text='<%# Bind("SOCNM") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="System Organ Class code Column" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SOCCD" runat="server" Text='<%# Bind("SOCCD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="High Level Group Term Column" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="HLGTNM" runat="server" Text='<%# Bind("HLGTNM") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="High Level Group Term code Column" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="HLGTCD" runat="server" Text='<%# Bind("HLGTCD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="High Level Term Column" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="HLTNM" runat="server" Text='<%# Bind("HLTNM") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="High Level Term code Column" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="HLTCD" runat="server" Text='<%# Bind("HLTCD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Peferred Term Column" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PTNM" runat="server" Text='<%# Bind("PTNM") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Peferred Term code Column" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PTCD" runat="server" Text='<%# Bind("PTCD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Lowest Level Term Column" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LLTNM" runat="server" Text='<%# Bind("LLTNM") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Lowest Level Term code Column" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LLTCD" runat="server" Text='<%# Bind("LLTCD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dictionary Name" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="DICNM" runat="server" Text='<%# Bind("DICNM") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dictionary Version" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="DICNO" runat="server" Text='<%# Bind("DICNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('DB_CODEMAP', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
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
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtndelete" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                CommandName="DeleteMeddra" ToolTip="Edit"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                <div id="popup_AuditTrail" title="Audit Trail" class="disp-none">
                    <div id="DivAuditTrail" style="font-size: small;">
                    </div>
                </div>
                <div class="row" id="divWhoddRecoard" runat="server" visible="false">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border" style="top: 0px; left: 0px;">
                                <h4 class="box-title" align="left">WHODD Records</h4>
                            </div>
                            <br />
                            <div align="left" style="margin-left: 5px">
                                <div>
                                    <div class="rows">
                                        <div class="fixTableHead">
                                            <asp:GridView ID="grdWhoddData" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                                Style="border-collapse: collapse; width: 99%;" OnPreRender="grdWhoddData_PreRender" OnRowCommand="grdWhoddData_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none"
                                                        ItemStyle-CssClass="disp-none">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnupdateWhodd" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                CommandName="EditWhodd" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ATC Level 1 Column" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CMATC1C" runat="server" Text='<%# Bind("CMATC1C") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ATC Level 1 code Column" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CMATC1CD" runat="server" Text='<%# Bind("CMATC1CD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ATC Level 2 Column" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CMATC2C" runat="server" Text='<%# Bind("CMATC2C") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ATC Level 2 code Column" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CMATC2CD" runat="server" Text='<%# Bind("CMATC2CD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ATC Level 3 Column" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CMATC3C" runat="server" Text='<%# Bind("CMATC3C") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ATC Level 3 code Column" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CMATC3CD" runat="server" Text='<%# Bind("CMATC3CD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ATC Level 4 Column" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CMATC4C" runat="server" Text='<%# Bind("CMATC4C") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ATC Level 4 code Column" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CMATC4CD" runat="server" Text='<%# Bind("CMATC4CD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Drug name" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CMATC5C" runat="server" Text='<%# Bind("CMATC5C") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Drug code" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CMATC5CD" runat="server" Text='<%# Bind("CMATC5CD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dictionary Name" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="DICNM" runat="server" Text='<%# Bind("DICNM") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dictionary Version" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="DICNO" runat="server" Text='<%# Bind("DICNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('DB_CODEMAP', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                        <HeaderTemplate>
                                                            <label>Entered By Details </label>
                                                            <br />
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
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtndeleteWhodd" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                CommandName="DeleteWhodd" ToolTip="Edit"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                <br />
                <div class="row" runat="server" visible="false" id="Meddra">
                    <div class="col-md-12">
                        <div class="box box-primary" style="min-height: 300px;">
                            <div class="box-header with-border" style="float: left;">
                                <h4 class="box-title" align="left">MedDRA
                                </h4>
                            </div>
                            <br />
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <br />
                                    <br />
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="col-md-4">
                                                <label>
                                                    System Organ Class Column :</label>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" Width="330px" ID="drpSystemOrganClass" CssClass="form-control required">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="col-md-5">
                                                <label>
                                                    System Organ Class code Column :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:DropDownList runat="server" Width="330px" ID="drpSystemOrganClassCode" CssClass="form-control required">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="col-md-4">
                                                <label>
                                                    High Level Group Term Column :</label>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" Width="330px" ID="drpHighLevelGrpTerm" CssClass="form-control required">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="col-md-5">
                                                <label>
                                                    High Level Group Term code Column :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:DropDownList runat="server" Width="330px" ID="drpHighLevelGrpCode" CssClass="form-control required">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="col-md-4">
                                                <label>
                                                    High Level Term Column :</label>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" Width="330px" ID="drpHighlevelterm" CssClass="form-control required">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="col-md-5">
                                                <label>
                                                    High Level Term code Column :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:DropDownList runat="server" Width="330px" ID="drpHighleveltermCode" CssClass="form-control  required">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="col-md-4">
                                                <label>
                                                    Peferred Term Column :</label>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" Width="330px" ID="drpPererredTerm" CssClass="form-control required">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="col-md-5">
                                                <label>
                                                    Peferred Term code Column :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:DropDownList runat="server" Width="330px" ID="drpPererredTermCode" CssClass="form-control required">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="col-md-4">
                                                <label>
                                                    Lowest Level Term Column :</label>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" Width="330px" ID="drpLowestLevelTerm" CssClass="form-control required">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="col-md-5">
                                                <label>
                                                    Lowest Level Term code Column :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:DropDownList runat="server" Width="330px" ID="drpLowestLevelTermCode" CssClass="form-control required">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="col-md-4">
                                                <label>
                                                    Dictionary Name :</label>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" Width="330px" ID="drMedDictionaryName" CssClass="form-control required">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="col-md-5">
                                                <label>
                                                    Dictionary Version :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:DropDownList runat="server" Width="330px" ID="drMedDictionaryVer" CssClass="form-control required">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-5">
                                                &nbsp;
                                            </div>
                                            <div class="col-md-7">
                                                <asp:Button ID="btnMeddra" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnMeddra_Click" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnMeddraUpdate" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnMeddra_Click" />
                                                <asp:Button ID="btnMeddraCancel" Text="Cancel" runat="server" CssClass="btn btn-danger btn-sm" OnClick="btnMeddraCancel_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" runat="server" visible="false" id="WHODData">
                    <div class="col-md-12">
                        <div class="box box-primary" style="min-height: 300px;">
                            <div class="box-header with-border" style="float: left;">
                                <h4 class="box-title" align="left">WHODD 
                                </h4>
                            </div>
                            <br />
                            <br />
                            <div class="box-body" runat="server">
                                <div align="left" style="margin-left: 5px">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="col-md-4">
                                                <label>
                                                    ATC Level 1 Column :</label>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" Width="380px" ID="drpATCLEVEL1" CssClass="form-control required">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="col-md-4">
                                                <label>
                                                    ATC Level 1 code Column :</label>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" Width="380px" ID="drpATCLEVEL1Code" CssClass="form-control required">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="col-md-4">
                                                <label>
                                                    ATC Level 2 Column :</label>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" Width="380px" ID="drpATCLEVEL2" CssClass="form-control required">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="col-md-4">
                                                <label>
                                                    ATC Level 2 Code Column :</label>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" Width="380px" ID="drpATCLEVEL2Code" CssClass="form-control required">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="col-md-4">
                                                <label>
                                                    ATC Level 3 Column :</label>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" Width="380px" ID="drpATCLEVEL3" CssClass="form-control required">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="col-md-4">
                                                <label>
                                                    ATC Level 3 code Column :</label>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" Width="380px" ID="drpATCLEVEL3Code" CssClass="form-control required">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="col-md-4">
                                                <label>
                                                    ATC Level 4 Column :</label>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" Width="380px" ID="drpATCLEVEL4" CssClass="form-control required">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="col-md-4">
                                                <label>
                                                    ATC Level 4 code Column:</label>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" Width="380px" ID="drpATCLEVEL4Code" CssClass="form-control required ">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="col-md-4">
                                                <label>
                                                    Drug name Column :</label>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" Width="380px" ID="drpATCLEVEL5" CssClass="form-control required ">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="col-md-4">
                                                <label>
                                                    Drug code Column :</label>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" Width="380px" ID="drpATCLEVEL5Code" CssClass="form-control required ">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="col-md-4">
                                                <label>
                                                    Dictionary Name :</label>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" Width="380px" ID="drWhoDictionaryName" CssClass="form-control required ">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="col-md-4">
                                                <label>
                                                    Dictionary Version :</label>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" Width="380px" ID="drWhoDictionaryVer" CssClass="form-control required">
                                                </asp:DropDownList>
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
                                            <div class="col-md-5">
                                                <asp:Button ID="btnWHODData" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnWHODData_Click" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnWODData" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnWHODData_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnWHODDataCancel" Text="Cancel" runat="server" CssClass="btn btn-danger btn-sm" OnClick="btnWHODDataCancel_Click" />
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
        </div>
    </form>
</body>
</html>
