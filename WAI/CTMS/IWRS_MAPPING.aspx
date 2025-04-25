<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="IWRS_MAPPING.aspx.cs" Inherits="CTMS.IWRS_MAPPING" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
    <meta charset="UTF-8">
    <title>WAI</title>
    <script src="Scripts/Jquery1.12.4.js" type="text/javascript"></script>
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
    <script src="js/Input%20Mask/jquery.inputmask.bundle.js"></script>
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <link href="js/plugins/datatables/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
    <script src="js/CKEditor/ckeditor.js"></script>
    <link href="CommonStyles/ButtonColor.css" rel="stylesheet" />
    <link href="CommonStyles/DataEntry_Table.css" rel="stylesheet" />
    <link href="CommonStyles/ModalPopup.css" rel="stylesheet" />
    <script src="CommonFunctionsJs/CKEDITOR_Limited.js"></script>
    <%--<script src="CommonFunctionsJs/Datatable1.js"></script>--%>
    <script src="CommonFunctionsJs/TextBox_Options.js"></script>
    <script src="CommonFunctionsJs/Button_Mandatory.js"></script>
    <script src="CommonFunctionsJs/btnSave_Required.js"></script>
    <script src="CommonFunctionsJs/ControlJS.js"></script>
    <script src="CommonFunctionsJs/OpenPopUp.js"></script>
    <script src="CommonFunctionsJs/DB/callChange_Review.js"></script>
    <script src="CommonFunctionsJs/DB/showChild.js"></script>
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="CommonFunctionsJs/DivExpandCollapse.js"></script>
    <script src="Scripts/Select2.js" type="text/javascript"></script>

    <style type="text/css">
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #0000ff;
        }

        .select2-container--default .select2-selection--single {
            margin-top: 2px;
        }

        .Mandatory {
            border: solid 1px !important;
            border-color: Red !important;
        }

        .REQUIRED {
            background-color: yellow;
        }
    </style>
    <script type="text/javascript">
        function pageLoad() {
            $('.select').select2();

            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: false,
                fixedHeader: true
            });
        }

        function OpenModule(element) {
            var MODULEID = $(element).closest('tr').find('td:eq(7)').find('span').text();
            var MODULENAME = $(element).closest('tr').find('td:eq(8)').find('span').text();

            if (MODULEID != "0") {
                var test = "DM_Mappings.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME;
                var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
                window.open(test, '_blank');
                return false;
            }
        }
    </script>
</head>
<body style="cursor: auto;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="col-md-12">
            <asp:HiddenField ID="hdnMODULEID" runat="server" />
            <asp:HiddenField ID="hdnMODULENAME" runat="server" />
            <div class="box box-danger">
                <div class="box-body" style="margin-left: 2%; margin-right: 2%;">
                    <div class="form-group">
                        <br />
                        <asp:Label ID="lblModuleName" runat="server" Visible="false" Text="" Font-Size="12px" Font-Bold="true"
                            Font-Names="Arial" CssClass="list-group-item"></asp:Label>
                        <br />
                        <asp:GridView ID="grd_Data" runat="server" CellPadding="3" AutoGenerateColumns="False" EmptyDataText="No Record Available."
                            ShowHeader="False" CssClass="table table-bordered table-striped ShowChild1" OnRowDataBound="grd_Data_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="VARIABLENAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVARIABLENAME" Text='<%# Bind("VARIABLENAME") %>'
                                            runat="server"></asp:Label>
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
                                        <asp:Label ID="lblSEQNO" Text='<%# Eval("SEQNO") + "." %>' runat="server" Font-Size="Small" Style="text-align: LEFT" Font-Bold="true"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblFieldName" Font-Size="Small" ToolTip='<%# Eval("Descrip") %>'
                                    Text='<%# Eval("FIELDNAME") %>' Style="text-align: LEFT" runat="server"></asp:Label>
                                        <asp:Label ID="VARIABLENAME" Text='<%# " [" +  Eval("VARIABLENAME") + "]" %>' runat="server" Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" ForeColor="DarkOrange"></asp:Label>
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
                                        <div class="col-md-12" id="divcontrol" runat="server">
                                            <asp:Label ID="lblFieldName1" Text='<%# Bind("FIELDNAME") %>' Visible="false" runat="server"></asp:Label>
                                            <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'></asp:LinkButton>
                                            <asp:DropDownList ID="DRP_FIELD" runat="server" onchange="showChild_Review(this);" Visible="false"
                                                Width="200px">
                                            </asp:DropDownList>
                                            <asp:HiddenField runat="server" ID="hfValue1" />
                                            <asp:TextBox ID="TXT_FIELD" runat="server" Width="200px" Visible="false" onchange="showChild_Review(this);"></asp:TextBox>
                                            <asp:Repeater runat="server" ID="repeat_CHK">
                                                <ItemTemplate>
                                                    <div class="col-md-4">
                                                        <asp:CheckBox ID="CHK_FIELD" runat="server" onchange="showChild_Review(this);" CssClass="checkbox"
                                                            ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>' Text='<%# Bind("TEXT") %>' />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <asp:Repeater runat="server" ID="repeat_RAD">
                                                <ItemTemplate>
                                                    <div class="col-md-4">
                                                        <asp:RadioButton ID="RAD_FIELD" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                            onchange="showChild_Review(this);" onclick="return RadioCheck(this);" CssClass="radio"
                                                            Text='<%# Bind("TEXT") %>' />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:Label ID="lbltextType" Font-Size="14px" Font-Bold="true" Text='<%# "["+ Eval("CONTROLS") + "]" %>' ForeColor="Maroon"
                                                runat="server"></asp:Label>
                                            <div id="divDisplayFeatures" runat="server" visible="false">
                                                <label style="color: deeppink;">Display Features: </label>
                                                &nbsp;&nbsp;
                                                                    <asp:Label ID="lblDisplayFeature" Font-Size="14px" Font-Bold="true" ForeColor="darkviolet" runat="server"></asp:Label>
                                            </div>
                                            <div id="divDataSignificance" runat="server" visible="false">
                                                <label style="color: deeppink;">Data Significance: </label>
                                                &nbsp;&nbsp;
                                                                    <asp:Label ID="lblDataSignificance" Font-Size="14px" Font-Bold="true" ForeColor="darkviolet" runat="server"></asp:Label>
                                            </div>
                                            <div id="divDataLinkages" runat="server" visible="false">
                                                <label style="color: deeppink;">Data Linkages: </label>
                                                &nbsp;&nbsp;
                                                                    <asp:Label ID="lblDataLinkages" Font-Size="14px" Font-Bold="true" ForeColor="darkviolet" runat="server"></asp:Label>
                                            </div>
                                            <div id="divMultipleDataEntry" runat="server" visible="false">
                                                <label style="color: deeppink;">Multiple Data Entry: </label>
                                                &nbsp;&nbsp;
                                                                    <asp:Label ID="lblMultipleDataEntry" Font-Size="14px" Font-Bold="true" ForeColor="darkviolet" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="REQUIREDYN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:Label ID="lblREQUIREDYN" Text='<%# Bind("REQUIREDYN") %>' runat="server"></asp:Label>
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
                                                                        <asp:Label ID="lblVARIABLENAME" Text='<%# Bind("VARIABLENAME") %>'
                                                                            runat="server"></asp:Label>
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
                                                                        <asp:Label ID="lblSEQNO" Text='<%# Eval("SEQNO") + "." %>' runat="server" Font-Size="Small" Style="text-align: LEFT" Font-Bold="true"></asp:Label>&nbsp;&nbsp;
                                                                <asp:Label ID="lblFieldName" Font-Size="Small" ToolTip='<%# Eval("Descrip") %>'
                                                                    Text='<%# Eval("FIELDNAME") %>' Style="text-align: LEFT" runat="server"></asp:Label>
                                                                        <asp:Label ID="VARIABLENAME" Text='<%# " [" +  Eval("VARIABLENAME") + "]" %>' runat="server" Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" ForeColor="DarkOrange"></asp:Label>
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
                                                                        <div class="col-md-12" id="divcontrol" runat="server">
                                                                            <asp:Label ID="lblFieldName1" Text='<%# Bind("FIELDNAME") %>' Visible="false" runat="server"></asp:Label>
                                                                            <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'></asp:LinkButton>
                                                                            <asp:DropDownList ID="DRP_FIELD" runat="server" onchange="showChild_Review(this);" Visible="false"
                                                                                Width="200px">
                                                                            </asp:DropDownList>
                                                                            <asp:HiddenField runat="server" ID="hfValue1" />
                                                                            <asp:TextBox ID="TXT_FIELD" runat="server" Width="200px" Visible="false" onchange="showChild_Review(this);"></asp:TextBox>
                                                                            <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                <ItemTemplate>
                                                                                    <div class="col-md-4">
                                                                                        <asp:CheckBox ID="CHK_FIELD" runat="server" onchange="showChild_Review(this);" CssClass="checkbox"
                                                                                            ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>' Text='<%# Bind("TEXT") %>' />
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                            <asp:Repeater runat="server" ID="repeat_RAD">
                                                                                <ItemTemplate>
                                                                                    <div class="col-md-4">
                                                                                        <asp:RadioButton ID="RAD_FIELD" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                            onchange="showChild_Review(this);" onclick="return RadioCheck(this);" CssClass="radio"
                                                                                            Text='<%# Bind("TEXT") %>' />
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div>
                                                                        <div class="col-md-12">
                                                                            <asp:Label ID="lbltextType" Font-Size="14px" Font-Bold="true" Text='<%# "["+ Eval("CONTROLS") + "]" %>' ForeColor="Maroon"
                                                                                runat="server"></asp:Label>
                                                                            <div id="divDisplayFeatures" runat="server" visible="false">
                                                                                <label style="color: deeppink;">Display Features: </label>
                                                                                &nbsp;&nbsp;
                                                                    <asp:Label ID="lblDisplayFeature" Font-Size="14px" Font-Bold="true" ForeColor="darkviolet" runat="server"></asp:Label>
                                                                            </div>
                                                                            <div id="divDataSignificance" runat="server" visible="false">
                                                                                <label style="color: deeppink;">Data Significance: </label>
                                                                                &nbsp;&nbsp;
                                                                    <asp:Label ID="lblDataSignificance" Font-Size="14px" Font-Bold="true" ForeColor="darkviolet" runat="server"></asp:Label>
                                                                            </div>
                                                                            <div id="divDataLinkages" runat="server" visible="false">
                                                                                <label style="color: deeppink;">Data Linkages: </label>
                                                                                &nbsp;&nbsp;
                                                                    <asp:Label ID="lblDataLinkages" Font-Size="14px" Font-Bold="true" ForeColor="darkviolet" runat="server"></asp:Label>
                                                                            </div>
                                                                            <div id="divMultipleDataEntry" runat="server" visible="false">
                                                                                <label style="color: deeppink;">Multiple Data Entry: </label>
                                                                                &nbsp;&nbsp;
                                                                    <asp:Label ID="lblMultipleDataEntry" Font-Size="14px" Font-Bold="true" ForeColor="darkviolet" runat="server"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="REQUIREDYN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblREQUIREDYN" Text='<%# Bind("REQUIREDYN") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVal_Child" Text='<%# Bind("VAL_Child") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMODULEID" Text='<%# Bind("MODULEID") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMODULENAME" Text='<%# Bind("MODULENAME") %>' runat="server"></asp:Label>
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
                                                                                                        <asp:Label ID="lblVARIABLENAME" Text='<%# Bind("VARIABLENAME") %>'
                                                                                                            runat="server"></asp:Label>
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
                                                                                                        <asp:Label ID="lblSEQNO" Text='<%# Eval("SEQNO") + "." %>' runat="server" Font-Size="Small" Style="text-align: LEFT" Font-Bold="true"></asp:Label>&nbsp;&nbsp;
                                                                                                <asp:Label ID="lblFieldName" Font-Size="Small" ToolTip='<%# Eval("Descrip") %>'
                                                                                                    Text='<%# Eval("FIELDNAME") %>' Style="text-align: LEFT" runat="server"></asp:Label>
                                                                                                        <asp:Label ID="VARIABLENAME" Text='<%# " [" +  Eval("VARIABLENAME") + "]" %>' runat="server" Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" ForeColor="DarkOrange"></asp:Label>
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
                                                                                                        <div class="col-md-12" id="divcontrol" runat="server">
                                                                                                            <asp:Label ID="lblFieldName1" Text='<%# Bind("FIELDNAME") %>' Visible="false" runat="server"></asp:Label>
                                                                                                            <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'></asp:LinkButton>
                                                                                                            <asp:DropDownList ID="DRP_FIELD" runat="server" onchange="showChild_Review(this);" Visible="false"
                                                                                                                Width="200px">
                                                                                                            </asp:DropDownList>
                                                                                                            <asp:HiddenField runat="server" ID="hfValue1" />
                                                                                                            <asp:TextBox ID="TXT_FIELD" runat="server" Width="200px" Visible="false" onchange="showChild_Review(this);"></asp:TextBox>
                                                                                                            <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                                                <ItemTemplate>
                                                                                                                    <div class="col-md-4">
                                                                                                                        <asp:CheckBox ID="CHK_FIELD" runat="server" onchange="showChild_Review(this);" CssClass="checkbox"
                                                                                                                            ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>' Text='<%# Bind("TEXT") %>' />
                                                                                                                    </div>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:Repeater>
                                                                                                            <asp:Repeater runat="server" ID="repeat_RAD">
                                                                                                                <ItemTemplate>
                                                                                                                    <div class="col-md-4">
                                                                                                                        <asp:RadioButton ID="RAD_FIELD" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                                            onchange="showChild_Review(this);" onclick="return RadioCheck(this);" CssClass="radio"
                                                                                                                            Text='<%# Bind("TEXT") %>' />
                                                                                                                    </div>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:Repeater>
                                                                                                        </div>
                                                                                                        <div class="col-md-12">
                                                                                                            <asp:Label ID="lbltextType" Font-Size="14px" Font-Bold="true" Text='<%# "["+ Eval("CONTROLS") + "]" %>' ForeColor="Maroon"
                                                                                                                runat="server"></asp:Label>
                                                                                                            <div id="divDisplayFeatures" runat="server" visible="false">
                                                                                                                <label style="color: deeppink;">Display Features: </label>
                                                                                                                &nbsp;&nbsp;
                                                                    <asp:Label ID="lblDisplayFeature" Font-Size="14px" Font-Bold="true" ForeColor="darkviolet" runat="server"></asp:Label>
                                                                                                            </div>
                                                                                                            <div id="divDataSignificance" runat="server" visible="false">
                                                                                                                <label style="color: deeppink;">Data Significance: </label>
                                                                                                                &nbsp;&nbsp;
                                                                    <asp:Label ID="lblDataSignificance" Font-Size="14px" Font-Bold="true" ForeColor="darkviolet" runat="server"></asp:Label>
                                                                                                            </div>
                                                                                                            <div id="divDataLinkages" runat="server" visible="false">
                                                                                                                <label style="color: deeppink;">Data Linkages: </label>
                                                                                                                &nbsp;&nbsp;
                                                                    <asp:Label ID="lblDataLinkages" Font-Size="14px" Font-Bold="true" ForeColor="darkviolet" runat="server"></asp:Label>
                                                                                                            </div>
                                                                                                            <div id="divMultipleDataEntry" runat="server" visible="false">
                                                                                                                <label style="color: deeppink;">Multiple Data Entry: </label>
                                                                                                                &nbsp;&nbsp;
                                                                    <asp:Label ID="lblMultipleDataEntry" Font-Size="14px" Font-Bold="true" ForeColor="darkviolet" runat="server"></asp:Label>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="REQUIREDYN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblREQUIREDYN" Text='<%# Bind("REQUIREDYN") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblVal_Child" Text='<%# Bind("VAL_Child") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblMODULEID" Text='<%# Bind("MODULEID") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblMODULENAME" Text='<%# Bind("MODULENAME") %>' runat="server"></asp:Label>
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
                                                                                                                                        <asp:Label ID="lblVARIABLENAME" Text='<%# Bind("VARIABLENAME") %>'
                                                                                                                                            runat="server"></asp:Label>
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
                                                                                                                                        <asp:Label ID="lblSEQNO" Text='<%# Eval("SEQNO") + "." %>' runat="server" Font-Size="Small" Style="text-align: LEFT" Font-Bold="true"></asp:Label>&nbsp;&nbsp;
                                                                                                                                <asp:Label ID="lblFieldName" Font-Size="Small" ToolTip='<%# Eval("Descrip") %>'
                                                                                                                                    Text='<%# Eval("FIELDNAME") %>' Style="text-align: LEFT" runat="server"></asp:Label>
                                                                                                                                        <asp:Label ID="VARIABLENAME" Text='<%# " [" +  Eval("VARIABLENAME") + "]" %>' runat="server" Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" ForeColor="DarkOrange"></asp:Label>
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
                                                                                                                                        <div class="col-md-12" id="divcontrol" runat="server">
                                                                                                                                            <asp:Label ID="lblFieldName1" Text='<%# Bind("FIELDNAME") %>' Visible="false" runat="server"></asp:Label>
                                                                                                                                            <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'></asp:LinkButton>
                                                                                                                                            <asp:DropDownList ID="DRP_FIELD" runat="server" onchange="showChild_Review(this);" Visible="false"
                                                                                                                                                Width="200px">
                                                                                                                                            </asp:DropDownList>
                                                                                                                                            <asp:HiddenField runat="server" ID="hfValue1" />
                                                                                                                                            <asp:TextBox ID="TXT_FIELD" runat="server" Width="200px" Visible="false" onchange="showChild_Review(this);"></asp:TextBox>
                                                                                                                                            <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                                                                                <ItemTemplate>
                                                                                                                                                    <div class="col-md-4">
                                                                                                                                                        <asp:CheckBox ID="CHK_FIELD" runat="server" onchange="showChild_Review(this);" CssClass="checkbox"
                                                                                                                                                            ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>' Text='<%# Bind("TEXT") %>' />
                                                                                                                                                    </div>
                                                                                                                                                </ItemTemplate>
                                                                                                                                            </asp:Repeater>
                                                                                                                                            <asp:Repeater runat="server" ID="repeat_RAD">
                                                                                                                                                <ItemTemplate>
                                                                                                                                                    <div class="col-md-4">
                                                                                                                                                        <asp:RadioButton ID="RAD_FIELD" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                                                                            onchange="showChild_Review(this);" onclick="return RadioCheck(this);" CssClass="radio"
                                                                                                                                                            Text='<%# Bind("TEXT") %>' />
                                                                                                                                                    </div>
                                                                                                                                                </ItemTemplate>
                                                                                                                                            </asp:Repeater>
                                                                                                                                        </div>
                                                                                                                                        <div class="col-md-12">
                                                                                                                                            <asp:Label ID="lbltextType" Font-Size="14px" Font-Bold="true" Text='<%# "["+ Eval("CONTROLS") + "]" %>' ForeColor="Maroon"
                                                                                                                                                runat="server"></asp:Label>
                                                                                                                                            <div id="divDisplayFeatures" runat="server" visible="false">
                                                                                                                                                <label style="color: deeppink;">Display Features: </label>
                                                                                                                                                &nbsp;&nbsp;
                                                                    <asp:Label ID="lblDisplayFeature" Font-Size="14px" Font-Bold="true" ForeColor="darkviolet" runat="server"></asp:Label>
                                                                                                                                            </div>
                                                                                                                                            <div id="divDataSignificance" runat="server" visible="false">
                                                                                                                                                <label style="color: deeppink;">Data Significance: </label>
                                                                                                                                                &nbsp;&nbsp;
                                                                    <asp:Label ID="lblDataSignificance" Font-Size="14px" Font-Bold="true" ForeColor="darkviolet" runat="server"></asp:Label>
                                                                                                                                            </div>
                                                                                                                                            <div id="divDataLinkages" runat="server" visible="false">
                                                                                                                                                <label style="color: deeppink;">Data Linkages: </label>
                                                                                                                                                &nbsp;&nbsp;
                                                                    <asp:Label ID="lblDataLinkages" Font-Size="14px" Font-Bold="true" ForeColor="darkviolet" runat="server"></asp:Label>
                                                                                                                                            </div>
                                                                                                                                            <div id="divMultipleDataEntry" runat="server" visible="false">
                                                                                                                                                <label style="color: deeppink;">Multiple Data Entry: </label>
                                                                                                                                                &nbsp;&nbsp;
                                                                    <asp:Label ID="lblMultipleDataEntry" Font-Size="14px" Font-Bold="true" ForeColor="darkviolet" runat="server"></asp:Label>
                                                                                                                                            </div>
                                                                                                                                        </div>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>
                                                                                                                                <asp:TemplateField HeaderText="REQUIREDYN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblREQUIREDYN" Text='<%# Bind("REQUIREDYN") %>' runat="server"></asp:Label>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>
                                                                                                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblVal_Child" Text='<%# Bind("VAL_Child") %>' runat="server"></asp:Label>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>
                                                                                                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblMODULEID" Text='<%# Bind("MODULEID") %>' runat="server"></asp:Label>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>
                                                                                                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblMODULENAME" Text='<%# Bind("MODULENAME") %>' runat="server"></asp:Label>
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
                    <br />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
