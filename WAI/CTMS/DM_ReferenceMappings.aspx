<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DM_ReferenceMappings.aspx.cs"
    Inherits="CTMS.DM_ReferenceMappings" %>

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
    <link rel="icon" href="img/favicon.ico" type="image/x-icon" />
    <link rel="shortcut icon" href="img/favicon.ico" type="image/x-icon" />
    <script src="Scripts/ClientValidation.js" type="text/javascript"></script>
    <!-- for Jquery Popup//-->
    <%--<script src="https://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>--%>
    <%--<link href="Styles/Jquery.ui.css" rel="stylesheet" type="text/css" />--%>
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
    <!-- Bootstrap -->
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <!-- Morris.js charts -->
    <script src="//cdnjs.cloudflare.com/ajax/libs/raphael/2.1.0/raphael-min.js"></script>
    <script src="js/plugins/morris/morris.min.js" type="text/javascript"></script>
    <!-- Sparkline -->
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <!-- jvectormap -->
    <script src="js/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js" type="text/javascript"></script>
    <script src="js/plugins/jvectormap/jquery-jvectormap-world-mill-en.js" type="text/javascript"></script>
    <!-- fullCalendar -->
    <script src="js/plugins/fullcalendar/fullcalendar.min.js" type="text/javascript"></script>
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
    <!-- AdminLTE dashboard demo (This is only for demo purposes) -->
    <link href="Styles/graph.css" rel="stylesheet" type="text/css" />
    <script src="js/Input%20Mask/jquery.inputmask.bundle.js" type="text/javascript"></script>
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 100%;
            margin-right: 142px;
        }
        .style2
        {
            width: 540px;
            background-color: #E98454;
        }
        .fontbold
        {
            font-weight: bold;
        }
    </style>
    <script type="text/javascript" language="javascript">

        function Print() {
            var ProjectId = '<%= Session["PROJECTID"] %>'
            var MODULEID = $("#HDNMODULEID").val();
            var MODULENAME = $("#HDMODULENAME").val();
            var test = "ReportDM_Mappings.aspx?ProjectId=" + ProjectId + "&MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500px,width=1000px";
            window.open(test, '_blank', strWinProperty);
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
        
    </script>
</head>
<body style="cursor: auto;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning" style="margin-left: 30px; margin-right: 30px; width: 96%;">
        <div class="box-header">
            <h3 class="box-title">
                Visits
            </h3>
        </div>
        <div class="box-body">
            <div class="rows">
                <div style="width: 100%; overflow: auto;">
                    <asp:GridView ID="grdVisit" runat="server" AutoGenerateColumns="False" Style="width: 91%;
                        border-collapse: collapse; margin-left: 20px;margin-bottom:10px;" CellPadding="3" CellSpacing="2"
                        CssClass="table table-bordered table-striped">
                        <Columns>
                            <asp:TemplateField HeaderText="ID" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="txt_center"
                                ItemStyle-CssClass="txt_center">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("VISITNUM") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Visit Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblVisitName" runat="server" Text='<%# Bind("VISIT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <div class="box box-warning" style="margin-left: 30px; margin-right: 30px; width: 96%;">
        <div class="box-header">
            <h3 class="box-title">
                Modules & Fields
            </h3>
        </div>
        <div class="box-body">
            <div class="rows">
                <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;
                    font-size: 15px; margin-left: 7px"></asp:Label>
                <asp:GridView ID="grd_Module" runat="server" CellPadding="3" AutoGenerateColumns="False"
                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;margin-bottom:10px;" CssClass="table table-bordered table-striped"
                    OnRowDataBound="grd_Module_RowDataBound">
                    <Columns>
                        <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                            HeaderStyle-CssClass="txt_center">
                            <HeaderTemplate>
                                <a href="JavaScript:ManipulateAll('_Field_');" id="_Folder" style="color: #333333"><i
                                    id="img_Field_" class="icon-plus-sign-alt"></i></a>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div runat="server" id="anchor">
                                    <a href="JavaScript:divexpandcollapse('_Field_<%# Eval("ModuleID") %>');" style="color: #333333">
                                        <i id="img_Field_<%# Eval("ModuleID") %>" class="icon-plus-sign-alt"></i></a>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:Label ID="lblID" Text='<%# Bind("ModuleID") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Module Name">
                            <ItemTemplate>
                                <asp:Label ID="lblModule" Text='<%# Bind("MODULENAME") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <tr>
                                    <td colspan="100%" style="padding: 2px; padding-left: 4%;">
                                        <div style="float: right; font-size: 13px;">
                                        </div>
                                        <div>
                                            <%--<div class="rows">--%>
                                            <%--<div class="col-md-12">--%>
                                            <div id="_Field_<%# Eval("ModuleID") %>" style="display: none; position: relative;
                                                overflow: auto;">
                                                <asp:GridView ID="grd_Field" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                                    CssClass="table table-bordered table-striped table-striped1">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="ID" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="txt_center"
                                                            ItemStyle-CssClass="txt_center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" Text='<%# Bind("ID") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Field Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblField" Text='<%# Bind("FIELDNAME") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <%--</div>--%>
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
    </form>
</body>
</html>
