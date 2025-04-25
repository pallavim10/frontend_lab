<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NSAE_Download.aspx.cs" Inherits="CTMS.NSAE_Download" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
        <asp:HiddenField ID="hdnid" runat="server" />
        <asp:HiddenField ID="hdnid1" runat="server" />
        <asp:HiddenField ID="hdnid2" runat="server" />
        <asp:HiddenField ID="hdnfieldname1" runat="server" />
        <asp:HiddenField ID="hdnfieldname2" runat="server" />
        <asp:HiddenField ID="hdnfieldname3" runat="server" />
        <asp:Repeater runat="server" ID="repeat_AllModule" OnItemDataBound="repeat_AllModule_ItemDataBound">
            <ItemTemplate>
                <div style="border-style: double; margin-bottom: 1px;">
                    <table>
                        <tr>
                            <td style="font-weight: bold; width: 15%; color: #a70808;">Module Name :&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblModuleName" runat="server" Text='<%# Bind("MODULENAME") %>' Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold; width: 15%; color: #a70808;">Module Status :&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblFormStatus" runat="server" Text='<%# Bind("STATUS") %>' Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold; width: 15%; color: #a70808;">Site Id :&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblSITEID" runat="server" Text='<%# Bind("INVID") %>' Font-Bold="true"></asp:Label>
                            </td>
                            <td style="font-weight: bold; color: #a70808;">Subject Id :&nbsp;
                            </td>
                            <td style="width: 35%">
                                <asp:Label ID="lblSUBJID" runat="server" Text='<%# Bind("SUBJID") %>' Font-Bold="true"></asp:Label>
                            </td>
                            <td style="font-weight: bold; color: #a70808;">Record Number. :&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="RECID" runat="server" Text='<%# Bind("RECORDNO") %>' Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="margin-bottom: 20px;">
                    <asp:GridView ID="grd_Data" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered1"
                        ShowHeader="false" CaptionAlign="Left" OnRowDataBound="grd_Data_RowDataBound">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblFieldName" Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT"
                                        ForeColor="Blue" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="col-md-12" style="padding-left: 0px;">
                                        <asp:Label ID="TXT_FIELD" Text='<%# Bind("DATAS") %>' runat="server"></asp:Label>
                                        <asp:GridView ID="grd_Data1" BorderStyle="None" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-striped table-bordered" ShowHeader="false" CaptionAlign="Left"
                                            OnRowDataBound="grd_Data1_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFieldName" Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT"
                                                            ForeColor="Maroon" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <div class="col-md-12" style="padding-left: 0px;">
                                                            <asp:Label ID="TXT_FIELD1" Text='<%# Bind("DATAS") %>' runat="server"></asp:Label>&nbsp
                                                                <asp:GridView ID="grd_Data2" BorderStyle="None" runat="server" AutoGenerateColumns="False"
                                                                    CssClass="table table-striped table-bordered" ShowHeader="false" CaptionAlign="Left"
                                                                    OnRowDataBound="grd_Data2_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFieldName" Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT"
                                                                                    ForeColor="Green" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <div class="col-md-12" style="padding-left: 0px;">
                                                                                    <asp:Label ID="TXT_FIELD2" Text='<%# Bind("DATAS") %>' runat="server"></asp:Label>
                                                                                    <asp:GridView ID="grd_Data3" BorderStyle="None" runat="server" AutoGenerateColumns="False"
                                                                                        CssClass="table table-striped table-bordered" ShowHeader="false" CaptionAlign="Left"
                                                                                        OnRowDataBound="grd_Data3_RowDataBound">
                                                                                        <Columns>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblFieldName" Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT"
                                                                                                        ForeColor="Red" runat="server"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    &nbsp
                                                                                                    <asp:Label ID="TXT_FIELD3" Text='<%# Bind("DATAS") %>' runat="server"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <table width="100%">
                    <tr id="TRAUDIT" runat="server">
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Audit Trail List" Font-Size="16px" Font-Bold="true"
                                Font-Names="Arial" CssClass="list-group-item" Style="border-color: Black; color: maroon;"></asp:Label>
                            <asp:GridView ID="grdAudit" HeaderStyle-ForeColor="Blue" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered"
                                CaptionAlign="Left">
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr id="TRQUERY" runat="server">
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Query List" Font-Size="16px" Font-Bold="true"
                                Font-Names="Arial" CssClass="list-group-item" Style="border-color: Black; color: maroon;"></asp:Label>
                            <asp:GridView ID="grdQuery" HeaderStyle-ForeColor="Blue" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered"
                                CaptionAlign="Left">
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr id="TRQUERY_COMMENT" runat="server">
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Query Comments" Font-Size="16px" Font-Bold="true"
                                Font-Names="Arial" CssClass="list-group-item" Style="border-color: Black; color: maroon;"></asp:Label>
                            <asp:GridView ID="grdQryComment" HeaderStyle-ForeColor="Blue" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered"
                                CaptionAlign="Left">
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr id="TRFIELDCOMMENT" runat="server">
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Field Comments" Font-Size="16px"
                                Font-Bold="true" Font-Names="Arial" CssClass="list-group-item" Style="border-color: Black; color: maroon;"></asp:Label>
                            <asp:GridView ID="grdFieldComment" HeaderStyle-ForeColor="Blue" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered"
                                CaptionAlign="Left">
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr id="TRPAGECOMMENT" runat="server">
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Module Comments" Font-Size="16px"
                                Font-Bold="true" Font-Names="Arial" CssClass="list-group-item" Style="border-color: Black; color: maroon;"></asp:Label>
                            <asp:GridView ID="grdModuleComment" HeaderStyle-ForeColor="Blue" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered"
                                CaptionAlign="Left">
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr id="TRDOCS" runat="server">
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="Supporting Document" Font-Size="16px"
                                Font-Bold="true" Font-Names="Arial" CssClass="list-group-item" Style="border-color: Black; color: maroon;"></asp:Label>
                            <asp:GridView ID="grdDocs" HeaderStyle-ForeColor="Blue" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered"
                                CaptionAlign="Left">
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr id="TREvent_Logs" runat="server">
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Event Logs" Font-Size="16px" Font-Bold="true"
                                Font-Names="Arial" CssClass="list-group-item" Style="border-color: Black; color: maroon;"></asp:Label>
                            <asp:GridView ID="grdEventLogs" HeaderStyle-ForeColor="Blue" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered"
                                CaptionAlign="Left">
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <div style='page-break-after: always;'>&nbsp;</div>
            </ItemTemplate>
        </asp:Repeater>
    </form>
</body>
</html>
