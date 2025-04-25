<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DB_ANNOTATED_VISIT_MODULES_PRINT.aspx.cs" Inherits="CTMS.DB_ANNOTATED_VISIT_MODULES_PRINT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8"/>
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
    <script type="text/javascript" src="CommonFunctionsJs/TextBox_Options.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/Button_Mandatory.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/btnSave_Required.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/ControlJS.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/OpenPopUp.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/DB/callChange_Review.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/DB/showChild.js"></script>
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <script type="text/javascript" src="CommonFunctionsJS/TabIndex.js"></script>
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
            <asp:HiddenField ID="hdnfieldname1" runat="server" />
            <asp:HiddenField ID="hdnfieldname2" runat="server" />
            <asp:HiddenField ID="hdnfieldname3" runat="server" />
            <asp:HiddenField ID="hdnMODULEID" runat="server" />
            <asp:HiddenField ID="hdnMODULENAME" runat="server" />
            <asp:HiddenField ID="hdnVISITNUM" runat="server" />
            <asp:HiddenField ID="hdnVISIT" runat="server" />
            <asp:HiddenField ID="hdnSYSTEM" runat="server" />
            <asp:Repeater runat="server" ID="repeat_AllModule" OnItemDataBound="repeat_AllModule_ItemDataBound">
                <ItemTemplate>
                    <div style="margin-bottom: 20px; width: 100%;">
                        <div style="border-style: double; margin-bottom: 2px;">
                            <table>
                                <tr>
                                    <td style="font-weight: bold;">Visit Name :&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblVisit" runat="server" Font-Size="14px" Font-Bold="true" Text='<%# Bind("VISIT") %>'
                                            Font-Names="Arial"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;">Module Name :&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblModuleName" runat="server" Font-Size="14px" Font-Bold="true" Text='<%# Bind("MODULENAME") %>'
                                            Font-Names="Arial"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <asp:GridView ID="grd_Data" runat="server" Width="100%" AutoGenerateColumns="False"
                                CssClass="table table-striped" ShowHeader="false" CaptionAlign="Left" OnRowDataBound="grd_Data_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <div class="col-md-12" style="padding-left: 0px;">
                                                <asp:Label ID="lblSEQNO" Text='<%# Eval("SEQNO") + "." %>' runat="server" Font-Size="Small" Style="text-align: LEFT" Font-Bold="true"></asp:Label>&nbsp;&nbsp;
                                                <asp:Label ID="lblFieldName" Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT; color: Blue;"
                                                    runat="server"></asp:Label>
                                                <asp:Label ID="VARIABLENAME" Text='<%# " [" +  Eval("VARIABLENAME") + "]" %>' runat="server" Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" ForeColor="DarkOrange"></asp:Label>
                                                <asp:Label ID="lbltextType" Font-Size="14px" Font-Bold="true" Text='<%# "["+ Eval("CONTROLS") + "]" %>' ForeColor="Maroon"
                                                    runat="server"></asp:Label>
                                                <asp:Label ID="lblIndications" ForeColor="Brown" Font-Bold="true" Text="" runat="server"></asp:Label>
                                                <asp:Label ID="LBL_TXT_FIELD" runat="server" Width="150px" Height="16px" ForeColor="DarkBlue" CssClass="form-control"
                                                    Visible="false"></asp:Label>&nbsp;
                                                <asp:Label ID="LBL_FIELD" runat="server" ForeColor="Red" Visible="false"></asp:Label>&nbsp;
                                                    <asp:Repeater runat="server" ID="repeat_CHK" OnItemDataBound="repeat_CHK_ItemDataBound">
                                                        <ItemTemplate>
                                                            <div class="col-md-12" style="padding-left: 1%; display: inline-flex;">
                                                                <asp:Label ID="lblSEQNO" runat="server" ForeColor="DarkViolet" Visible="false"></asp:Label>&nbsp
                                                                     <asp:CheckBox ID="CHK_FIELD" Style="height: auto; width: 200px; font-weight: bold;"
                                                                         runat="server" CssClass="checkbox" Text='<%# Bind("TEXT") %>' ForeColor="DarkBlue"/>
                                                            </div>
                                                            <div style="padding-left: 25px;">
                                                                <asp:Repeater runat="server" ID="repeat_CHK1" OnItemDataBound="repeat_CHK1_ItemDataBound">
                                                                    <ItemTemplate>
                                                                        <div class="col-md-12" style="display: inline-flex;">
                                                                            <div style="width: 210px; margin-bottom: 5px;">
                                                                                <asp:Label ID="lblSEQNO" Text='<%# Eval("SEQNO") + "." %>' runat="server" Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" Visible="false"></asp:Label>&nbsp;&nbsp;
                                                                            <asp:Label ID="lblFieldName" Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT; color: Blue;" Visible="false"
                                                                                runat="server"></asp:Label>
                                                                                <asp:Label ID="lblVARIABLENAME" Text='<%# " [" +  Eval("VARIABLENAME") + "]" %>' runat="server" Visible="false"
                                                                                    Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" ForeColor="DarkOrange"></asp:Label>
                                                                                <asp:Label ID="lblIndications" ForeColor="Brown" Font-Bold="true" Text="" runat="server" Visible="false"></asp:Label>
                                                                            </div>
                                                                            <asp:Label ID="LBL_TXT_FIELD" runat="server" Width="150px" Height="16px" ForeColor="DarkBlue" CssClass="form-control"
                                                                                Visible="false"></asp:Label>&nbsp;
                                                                            <asp:Label ID="LBL_FIELD" runat="server" ForeColor="Red" Visible="false"></asp:Label>&nbsp;
                                                                            <asp:Label ID="lblOPTIONSEQNO" runat="server" ForeColor="DarkViolet" Text='<%# Eval("DRPSEQNO") + "." %>' Visible="false"></asp:Label>&nbsp;
                                                                            <asp:RadioButton ID="RAD_FIELD" Style="min-height: 5px; margin-bottom: 2px; font-weight: bold;" Text='<%# Bind("TEXT") %>' Visible="false"
                                                                                runat="server" CssClass="radio" ForeColor="DarkBlue" />&nbsp;
                                                                            <asp:CheckBox ID="CHK_FIELD" Style="height: auto; width: 250px; font-weight: bold;" Visible="false"
                                                                                runat="server" CssClass="checkbox" Text='<%# Bind("TEXT") %>' ForeColor="DarkBlue" />
                                                                            <asp:Label ID="lbltextType" Font-Size="14px" Font-Bold="true" Text='<%# "["+ Eval("CONTROLS") + "]" %>'
                                                                                Visible="false" ForeColor="Maroon" runat="server"></asp:Label>&nbsp;
                                                                        </div>
                                                                        <div style="padding-left: 240px;">
                                                                            <asp:Repeater runat="server" ID="repeat_CHK2" OnItemDataBound="repeat_CHK2_ItemDataBound">
                                                                                <ItemTemplate>
                                                                                    <div class="col-md-12" style="display: inline-flex;">
                                                                                        <div style="width: 210px; margin-bottom: 5px;">
                                                                                            <asp:Label ID="lblSEQNO" Text='<%# Eval("SEQNO") + "." %>' runat="server" Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" Visible="false"></asp:Label>&nbsp;&nbsp;
                                                                            <asp:Label ID="lblFieldName" Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT; color: Blue;" Visible="false"
                                                                                runat="server"></asp:Label>
                                                                                            <asp:Label ID="lblVARIABLENAME" Text='<%# " [" +  Eval("VARIABLENAME") + "]" %>' runat="server" Visible="false"
                                                                                                Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" ForeColor="DarkOrange"></asp:Label>
                                                                                            <asp:Label ID="lblIndications" ForeColor="Brown" Font-Bold="true" Text="" runat="server" Visible="false"></asp:Label>
                                                                                        </div>
                                                                                        <asp:Label ID="LBL_TXT_FIELD" runat="server" Width="150px" Height="16px" ForeColor="DarkBlue" CssClass="form-control"
                                                                                            Visible="false"></asp:Label>&nbsp;
                                                                                        <asp:Label ID="LBL_FIELD" runat="server" ForeColor="Red" Visible="false"></asp:Label>&nbsp;
                                                                            <asp:Label ID="lblOPTIONSEQNO" runat="server" ForeColor="DarkViolet" Text='<%# Bind("DRPSEQNO") %>' Visible="false"></asp:Label>&nbsp
                                                                            <asp:RadioButton ID="RAD_FIELD" Style="min-height: 5px; margin-bottom: 2px; font-weight: bold;" Text='<%# Bind("TEXT") %>' Visible="false"
                                                                                runat="server" CssClass="radio" ForeColor="DarkBlue" />
                                                                                        <asp:CheckBox ID="CHK_FIELD" Style="height: auto; width: 250px; font-weight: bold;" Visible="false"
                                                                                            runat="server" CssClass="checkbox" Text='<%# Bind("TEXT") %>' ForeColor="DarkBlue" />
                                                                                        <asp:Label ID="lbltextType" Font-Size="14px" Font-Bold="true" Text='<%# "["+ Eval("CONTROLS") + "]" %>'
                                                                                            Visible="false" ForeColor="Maroon" runat="server"></asp:Label>
                                                                                    </div>
                                                                                    <div style="padding-left: 240px;">
                                                                                        <asp:Repeater runat="server" ID="repeat_CHK3" OnItemDataBound="repeat_CHK3_ItemDataBound">
                                                                                            <ItemTemplate>
                                                                                                <div class="col-md-12" style="display: inline-flex;">
                                                                                                    <div style="width: 210px; margin-bottom: 5px;">
                                                                                                        <asp:Label ID="lblSEQNO" Text='<%# Eval("SEQNO") + "." %>' runat="server" Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" Visible="false"></asp:Label>&nbsp;&nbsp;
                                                                            <asp:Label ID="lblFieldName" Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT; color: Blue;" Visible="false"
                                                                                runat="server"></asp:Label>
                                                                                                        <asp:Label ID="lblVARIABLENAME" Text='<%# " [" +  Eval("VARIABLENAME") + "]" %>' runat="server" Visible="false"
                                                                                                            Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" ForeColor="DarkOrange"></asp:Label>
                                                                                                        <asp:Label ID="lblIndications" ForeColor="Brown" Font-Bold="true" Text="" runat="server" Visible="false"></asp:Label>
                                                                                                    </div>
                                                                                                    <asp:Label ID="LBL_TXT_FIELD" runat="server" Width="150px" Height="16px" ForeColor="DarkBlue" CssClass="form-control"
                                                                                                        Visible="false"></asp:Label>&nbsp;
                                                                                                    <asp:Label ID="LBL_FIELD" runat="server" ForeColor="Red" Visible="false"></asp:Label>&nbsp;
                                                                            <asp:Label ID="lblOPTIONSEQNO" runat="server" ForeColor="DarkViolet" Text='<%# Bind("DRPSEQNO") %>' Visible="false"></asp:Label>&nbsp
                                                                            <asp:RadioButton ID="RAD_FIELD" Style="min-height: 5px; margin-bottom: 2px; font-weight: bold;" Text='<%# Bind("TEXT") %>' Visible="false"
                                                                                runat="server" CssClass="radio" ForeColor="DarkBlue" />
                                                                                                    <asp:CheckBox ID="CHK_FIELD" Style="height: auto; width: 250px; font-weight: bold;" Visible="false"
                                                                                                        runat="server" CssClass="checkbox" Text='<%# Bind("TEXT") %>' ForeColor="DarkBlue" />
                                                                                                    <asp:Label ID="lbltextType" Font-Size="14px" Font-Bold="true" Text='<%# "["+ Eval("CONTROLS") + "]" %>'
                                                                                                        Visible="false" ForeColor="Maroon" runat="server"></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:Repeater>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                <asp:Repeater runat="server" ID="repeat_RAD" OnItemDataBound="repeat_RAD_ItemDataBound">
                                                    <ItemTemplate>
                                                        <div class="col-md-12" style="padding-left: 1%; display: inline-flex;">
                                                            <asp:Label ID="lblSEQNO" runat="server" ForeColor="DarkViolet" Visible="false"></asp:Label>&nbsp
                                                                    <asp:RadioButton ID="RAD_FIELD" Style="min-height: 5px; margin-bottom: 0px; font-weight: bold;"
                                                                        runat="server" CssClass="radio" Text='<%# Bind("TEXT") %>' ForeColor="DarkBlue" />
                                                        </div>
                                                        <div style="padding-left: 25px;">
                                                            <asp:Repeater runat="server" ID="repeat_RAD1" OnItemDataBound="repeat_RAD1_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <div class="col-md-12" style="padding-left: 1%; display: inline-flex;">
                                                                        <div style="width: 210px; margin-bottom: 5px;">
                                                                            <asp:Label ID="lblSEQNO" Text='<%# Eval("SEQNO") + "." %>' runat="server" Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" Visible="false"></asp:Label>&nbsp;&nbsp;
                                                                            <asp:Label ID="lblFieldName" Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT; color: Blue;" Visible="false"
                                                                                runat="server"></asp:Label>
                                                                            <asp:Label ID="lblVARIABLENAME" Text='<%# " [" +  Eval("VARIABLENAME") + "]" %>' runat="server" Visible="false"
                                                                                Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" ForeColor="DarkOrange"></asp:Label>
                                                                            <br />
                                                                            <asp:Label ID="lblIndications" ForeColor="Brown" Font-Bold="true" Text="" runat="server" Visible="false"></asp:Label>
                                                                        </div>
                                                                        <asp:Label ID="LBL_TXT_FIELD" runat="server" Width="150px" Height="16px" ForeColor="DarkBlue" CssClass="form-control"
                                                                            Visible="false"></asp:Label>&nbsp;
                                                                        <asp:Label ID="LBL_FIELD" runat="server" ForeColor="Red" Visible="false"></asp:Label>&nbsp;
                                                                            <asp:Label ID="lblOPTIONSEQNO" runat="server" ForeColor="DarkViolet" Text='<%# Eval("DRPSEQNO") + "." %>' Visible="false"></asp:Label>&nbsp;
                                                                            <asp:RadioButton ID="RAD_FIELD" Style="min-height: 5px; margin-bottom: 2px; font-weight: bold;" Text='<%# Bind("TEXT") %>' Visible="false"
                                                                                runat="server" CssClass="radio" ForeColor="DarkBlue" />&nbsp;
                                                                            <asp:CheckBox ID="CHK_FIELD" Style="height: auto; font-weight: bold;" Visible="false"
                                                                                runat="server" CssClass="checkbox" Text='<%# Bind("TEXT") %>' ForeColor="DarkBlue" />
                                                                        <asp:Label ID="lbltextType" Font-Size="14px" Font-Bold="true" Text='<%# "["+ Eval("CONTROLS") + "]" %>'
                                                                            Visible="false" ForeColor="Maroon" runat="server"></asp:Label>&nbsp;
                                                                            <asp:Label ID="lblIndications2" ForeColor="Brown" Font-Bold="true" Text="" runat="server" Visible="false"></asp:Label>
                                                                    </div>
                                                                    <div style="padding-left: 240px;">
                                                                        <asp:Repeater runat="server" ID="repeat_RAD2" OnItemDataBound="repeat_RAD2_ItemDataBound">
                                                                            <ItemTemplate>
                                                                                <div class="col-md-12" style="display: inline-flex;">
                                                                                    <div style="width: 210px; margin-bottom: 5px;">
                                                                                        <asp:Label ID="lblSEQNO" Text='<%# Eval("SEQNO") + "." %>' runat="server" Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" Visible="false"></asp:Label>&nbsp;&nbsp;
                                                                            <asp:Label ID="lblFieldName" Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT; color: Blue;" Visible="false"
                                                                                runat="server"></asp:Label>
                                                                                        <asp:Label ID="lblVARIABLENAME" Text='<%# " [" +  Eval("VARIABLENAME") + "]" %>' runat="server" Visible="false"
                                                                                            Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" ForeColor="DarkOrange"></asp:Label>
                                                                                        <asp:Label ID="lblIndications" ForeColor="Brown" Font-Bold="true" Text="" runat="server" Visible="false"></asp:Label>
                                                                                    </div>
                                                                                    <asp:Label ID="LBL_TXT_FIELD" runat="server" Width="150px" Height="16px" ForeColor="DarkBlue" CssClass="form-control"
                                                                                        Visible="false"></asp:Label>&nbsp;
                                                                                    <asp:Label ID="LBL_FIELD" runat="server" ForeColor="Red" Visible="false"></asp:Label>&nbsp;
                                                                            <asp:Label ID="lblOPTIONSEQNO" runat="server" ForeColor="DarkViolet" Text='<%# Bind("DRPSEQNO") %>' Visible="false"></asp:Label>&nbsp
                                                                            <asp:RadioButton ID="RAD_FIELD" Style="min-height: 5px; margin-bottom: 2px; font-weight: bold;" Text='<%# Bind("TEXT") %>' Visible="false"
                                                                                runat="server" CssClass="radio" ForeColor="DarkBlue" />
                                                                                    <asp:CheckBox ID="CHK_FIELD" Style="height: auto; font-weight: bold;" Visible="false"
                                                                                        runat="server" CssClass="checkbox" Text='<%# Bind("TEXT") %>' ForeColor="DarkBlue" />
                                                                                    <asp:Label ID="lbltextType" Font-Size="14px" Font-Bold="true" Text='<%# "["+ Eval("CONTROLS") + "]" %>'
                                                                                        Visible="false" ForeColor="Maroon" runat="server"></asp:Label>
                                                                                    <asp:Label ID="lblIndications2" ForeColor="Brown" Font-Bold="true" Text="" runat="server" Visible="false"></asp:Label>
                                                                                </div>
                                                                                <div style="padding-left: 240px;">
                                                                                    <asp:Repeater runat="server" ID="repeat_RAD3" OnItemDataBound="repeat_RAD3_ItemDataBound">
                                                                                        <ItemTemplate>
                                                                                            <div class="col-md-12" style="display: inline-flex;">
                                                                                                <div style="width: 210px; margin-bottom: 5px;">
                                                                                                    <asp:Label ID="lblSEQNO" Text='<%# Eval("SEQNO") + "." %>' runat="server" Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" Visible="false"></asp:Label>&nbsp;&nbsp;
                                                                            <asp:Label ID="lblFieldName" Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT; color: Blue;" Visible="false"
                                                                                runat="server"></asp:Label>
                                                                                                    <asp:Label ID="lblVARIABLENAME" Text='<%# " [" +  Eval("VARIABLENAME") + "]" %>' runat="server" Visible="false"
                                                                                                        Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" ForeColor="DarkOrange"></asp:Label>
                                                                                                    <asp:Label ID="lblIndications" ForeColor="Brown" Font-Bold="true" Text="" runat="server" Visible="false"></asp:Label>
                                                                                                </div>
                                                                                                <asp:Label ID="LBL_TXT_FIELD" runat="server" Width="150px" Height="16px" ForeColor="DarkBlue" CssClass="form-control"
                                                                                                    Visible="false"></asp:Label>&nbsp;
                                                                                                <asp:Label ID="LBL_FIELD" runat="server" ForeColor="Red" Visible="false"></asp:Label>&nbsp;
                                                                            <asp:Label ID="lblOPTIONSEQNO" runat="server" ForeColor="DarkViolet" Text='<%# Bind("DRPSEQNO") %>' Visible="false"></asp:Label>&nbsp
                                                                            <asp:RadioButton ID="RAD_FIELD" Style="min-height: 5px; margin-bottom: 2px; font-weight: bold;" Text='<%# Bind("TEXT") %>' Visible="false"
                                                                                runat="server" CssClass="radio" ForeColor="DarkBlue" />
                                                                                                <asp:CheckBox ID="CHK_FIELD" Style="height: auto; font-weight: bold;" Visible="false"
                                                                                                    runat="server" CssClass="checkbox" Text='<%# Bind("TEXT") %>' ForeColor="DarkBlue" />
                                                                                                <asp:Label ID="lbltextType" Font-Size="14px" Font-Bold="true" Text='<%# "["+ Eval("CONTROLS") + "]" %>'
                                                                                                    Visible="false" ForeColor="Maroon" runat="server"></asp:Label>
                                                                                                <asp:Label ID="lblIndications2" ForeColor="Brown" Font-Bold="true" Text="" runat="server" Visible="false"></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <table width="100%">
                                <tr id="TR1" runat="server">
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Font-Size="14px" Font-Bold="true" Font-Names="Arial"
                                            Width="100%" CssClass="list-group-item" ForeColor="DarkBlue" Style="border-color: Black;"><label>Key: <br />
                                                <label style="color:DarkOrange;">[ABC]</label> = VariableName, <label style="color:Maroon;"> [XYZ]</label> = Control Type, [AN] = Alphanumerical
                                                <br />
                                                <label style="color:black;">Display Features:</label> [<i class="fa fa-bold"></i>] = Bold, [<i class="fa fa-eye-slash"></i>] = Mask Field, [<label style="color:maroon;font-family:cursive">R</label>] = Read only<br />
                                                <label style="color:black;">Data Significance:</label> [<label style="width: 20px; height: 10px; vertical-align: middle; border:1px solid #f2e8e8; margin-top: 5px !important;background-color:yellow;"></label>] = Required Information, [*] and [<label style="width: 20px; height: 10px; vertical-align: middle; border: 1px solid #e11111; margin-top: 5px !important;"></label>] = Mandatory Information, [⚠️] = Critical Data Point, [<i class="fa fa-check-circle"></i>] = SDV/SDR, [<i class="fa fa-user-md"></i>] = Medical Authority Response, [<i class="fa fa-clone"></i>] = Duplicates Check Information<br />
                                                <label style="color:black;">Data Linkages:</label>  [<i class="fa fa-link"></i>(P)] = Linked Field (Parent), [<i class="fa fa-link"></i>(C)] = Linked Field (Child), [<i class="fa fa-flask"></i>] = Lab Referance Range, [<i class="fa fa-desktop"></i>] = AutoCode<br />
                                                <label style="color:black;">Multiple Data Entry:</label> [<i class="fa fa-sort-numeric-asc"></i>] = Sequential Auto-Numbering, [<i class="fa fa-exchange-alt"></i>] = Non-Repetitive, [<i class="fa fa-list"></i>] = In List Data<br />
                                        </asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <div style='page-break-after: always;'>&nbsp;</div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
