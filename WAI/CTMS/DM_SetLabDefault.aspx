<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DM_SetLabDefault.aspx.cs" Inherits="CTMS.DM_SetLabDefault" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
            var nodes = document.getElementById("upl").getElementsByTagName('*');
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
   <%-- <script type="text/javascript" src="js/MaxLength.min.js"></script>
    <script type="text/javascript" src="js/MaxLength_Limit.js"></script>--%>
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
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Manage Lab Reference Range Mapping</h3>
                    <span id="lblStatus" style="font-size: 16px; color: darkorange; font-weight: bold; margin-left: 51px;"></span>
                </div>
                <div class="box-body">
                    <div align="left" style="margin-left: 5px">
                        <div>
                            <div class="rows">
                                <div class="fixTableHead">
                                    <asp:GridView ID="grdLabDefaults" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                        OnRowCommand="grdLabDefaults_RowCommand" OnPreRender="grdLabDefaults_PreRender">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                <ItemTemplate>
                                                    <asp:Label ID="ID1" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnEdit" CommandArgument='<%# Bind("ID") %>' CommandName="EditCrit"
                                                        runat="server" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('DB_LAB_DEFAULTS_MAPPING', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnDelete" CommandArgument='<%# Bind("ID") %>' CommandName="DeleteCrit" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this Lab Reference Range: ", Eval("ID")) %>'
                                                        runat="server" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Visit" ItemStyle-CssClass="txt_center">
                                                <ItemTemplate>
                                                    <asp:Label ID="VISIT" Text='<%# Bind("VISIT") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Primary Date Visit">
                                                <ItemTemplate>
                                                    <asp:Label ID="P_VISIT" Text='<%# Bind("P_VISIT") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Primary Date Module">
                                                <ItemTemplate>
                                                    <asp:Label ID="P_MODULE" Text='<%# Bind("P_MODULE") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Primary Date Field">
                                                <ItemTemplate>
                                                    <asp:Label ID="P_FIELD" Text='<%# Bind("P_FIELD") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Secondary Date Module">
                                                <ItemTemplate>
                                                    <asp:Label ID="S_MODULE" Text='<%# Bind("S_MODULE") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Secondary Date Field">
                                                <ItemTemplate>
                                                    <asp:Label ID="S_FIELD" Text='<%# Bind("S_FIELD") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sex Date Module">
                                                <ItemTemplate>
                                                    <asp:Label ID="SEX_MODULE" Text='<%# Bind("SEX_MODULE") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sex Date Field">
                                                <ItemTemplate>
                                                    <asp:Label ID="SEX_FIELD" Text='<%# Bind("SEX_FIELD") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Age Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="AGE_UNIT" Text='<%# Bind("AGE_UNIT") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Lab Id Column">
                                                <ItemTemplate>
                                                    <asp:Label ID="LABID_COL" Text='<%# Bind("LABID_COL") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Lower Limit Column">
                                                <ItemTemplate>
                                                    <asp:Label ID="LL_COL" Text='<%# Bind("LL_COL") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Upper Limit Column">
                                                <ItemTemplate>
                                                    <asp:Label ID="UL_COL" Text='<%# Bind("UL_COL") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Column">
                                                <ItemTemplate>
                                                    <asp:Label ID="UNIT_COL" Text='<%# Bind("UNIT_COL") %>' runat="server"></asp:Label>
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
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="popup_AuditTrail" title="Audit Trail" class="disp-none">
            <div id="DivAuditTrail" style="font-size: small;">
            </div>
        </div>
        <asp:UpdatePanel ID="upl" runat="server">
            <ContentTemplate>
                <div style="margin: 5px">
                    <div class="row">
                        <div class="lblError">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="box box-danger" style="min-height: 620px;">
                            <div class="box-header" style="float: left; top: 0px; left: 0px;">
                                <h4 class="box-title" align="left">
                                    <asp:Label ID="lblList" runat="server"></asp:Label>
                                </h4>
                            </div>
                            <br />
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <asp:Label ID="Label9" class="label" runat="server" Text="Select Visit :"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="drpVisit" class="form-control width300px required" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Label ID="lblAgeUnit" class="label" runat="server" Text="Age Unit :"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="drpAgeUnit" class="form-control width300px required" runat="server">
                                                <asp:ListItem Text="--Select--" Value="0" />
                                                <asp:ListItem Text="Year" Value="Year" />
                                                <asp:ListItem Text="Month" Value="Month" />
                                                <asp:ListItem Text="Days" Value="Days" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="col-md-12">
                                <div class="box box-info" style="min-height: 110px;">
                                    <div class="box-header" style="float: left; top: 0px; left: 0px;">
                                        <h4 class="box-title" align="left">Primary Date Field Mapping
                                        </h4>
                                    </div>
                                    <br />
                                    <div class="box-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <asp:Label ID="lblPrimaryVisit" class="label" runat="server" Text="Select Visit :"></asp:Label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="drpPrimaryVisit" class="form-control width300px required" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <asp:Label ID="lblPrimaryModuleName" class="label" runat="server" Text="Select Module :"></asp:Label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="DrpPrimaryModule" class="form-control width300px required" runat="server"
                                                        AutoPostBack="true" OnSelectedIndexChanged="DrpPrimaryModule_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Label ID="lblPrimaryFieldName" class="label" runat="server" Text="Select Field :"></asp:Label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="DrpPrimaryFields" class="form-control width300px required" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="box box-primary" style="min-height: 75px;">
                                    <div class="box-header" style="float: left; top: 0px; left: 0px;">
                                        <h4 class="box-title" align="left">Secondary Date Field Mapping
                                        </h4>
                                    </div>
                                    <br />
                                    <div class="box-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <asp:Label ID="Label2" class="label" runat="server" Text="Select Module :"></asp:Label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="DrpSecModule" class="form-control width300px required" runat="server"
                                                        AutoPostBack="true" OnSelectedIndexChanged="DrpSecModule_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Label ID="Label3" class="label" runat="server" Text="Select Field :"></asp:Label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="DrpSecFields" class="form-control width300px required" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="box box-success" style="min-height: 75px;">
                                    <div class="box-header" style="float: left; top: 0px; left: 0px;">
                                        <h4 class="box-title" align="left">Sex Field Mapping
                                        </h4>
                                    </div>
                                    <br />
                                    <div class="box-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <asp:Label ID="Label10" class="label" runat="server" Text="Select Module :"></asp:Label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="drpSexModule" class="form-control width300px required" runat="server"
                                                        AutoPostBack="true" OnSelectedIndexChanged="drpSexModule_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Label ID="Label8" class="label" runat="server" Text="Select Sex Column :"></asp:Label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="drpSexFieldId" class="form-control width300px required" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="box box-warning" style="min-height: 170px;">
                                    <div class="box-header" style="float: left; top: 0px; left: 0px;">
                                        <h4 class="box-title" align="left">Lab Field Mapping
                                        </h4>
                                    </div>
                                    <br />
                                    <div class="box-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <asp:Label ID="Label4" class="label" runat="server" Text="Select Lab Id Column :"></asp:Label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="drpLabIdField" class="form-control width300px required" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Label ID="Label7" class="label" runat="server" Text="Select Unit Column :"></asp:Label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="drpUnitFieldId" class="form-control width300px required" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <asp:Label ID="Label5" class="label" runat="server" Text="Reference Ranges Lower Limit Column :"></asp:Label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="drpLowLimitField" class="form-control width300px required" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Label ID="Label6" class="label" runat="server" Text="Reference Ranges Upper Limit Column :"></asp:Label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="drpUpperLimitField" class="form-control width300px required" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <asp:Label ID="Label1" class="label" runat="server" Text="Lab TestName Column :"></asp:Label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="drpTestNameField" class="form-control width300px required" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-3">
                                            <center>
                                                <div class="pull-Right" style="margin-right: 15px;">
                                                    <div class="pull-Right">
                                                        <asp:LinkButton ID="lbtnsubmitDefine" Text="Submit" runat="server"
                                                            Style="color: white;" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                            OnClick="lbtnsubmitDefine_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lbtnUpdateDefine" Text="Update" runat="server"
                                                            Style="color: white;" Visible="false" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                            OnClick="lbtnUpdate_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                      <asp:LinkButton ID="lbtnCancel" Text="Cancel" runat="server"
                                                          Style="color: white;" CssClass="btn btn-danger btn-sm" OnClick="lbtnCancel_Click" />
                                                    </div>
                                                </div>
                                            </center>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="DrpPrimaryModule" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="DrpSecModule" EventName="SelectedIndexChanged" />
                <asp:PostBackTrigger ControlID="lbtnsubmitDefine" />
                <asp:PostBackTrigger ControlID="lbtnUpdateDefine" />
                <asp:PostBackTrigger ControlID="lbtnCancel" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>
