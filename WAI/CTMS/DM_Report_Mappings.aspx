<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DM_Report_Mappings.aspx.cs"
    Inherits="CTMS.DM_Report_Mappings" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
    <link rel="icon" href="img/favicon.ico" type="image/x-icon" />
    <link rel="shortcut icon" href="img/favicon.ico" type="image/x-icon" />
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="Scripts/ClientValidation.js" type="text/javascript"></script>
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
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title" style="width: 100%;">
                View Mappings
            </h3>
        </div>
        <div class="box-body" style="margin-left: 2%; margin-right: 2%;">
            <div class="form-group">
                <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;
                    font-size: 15px; margin-left: 7px"></asp:Label>
                <asp:Label ID="lblModuleName" runat="server" Text="" Font-Size="12px" Font-Bold="true"
                    Font-Names="Arial" CssClass="list-group-item"></asp:Label>
                <asp:Repeater ID="repeater_Data" OnItemDataBound="repeater_Data_ItemDataBound" runat="server">
                    <ItemTemplate>
                        <div class="row" style="border: groove;">
                            <div runat="server" id="divHeader" visible="false" class="col-md-12">
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <asp:Label ID="Label1" Text='<%# Bind("SEQNO") %>' Font-Bold="true" runat="server"></asp:Label>.&nbsp;&nbsp;<asp:Label
                                        ID="lblFieldName" Font-Size="Small" Font-Bold="true" Text='<%# Bind("FIELDNAME") %>'
                                        Style="text-align: LEFT" runat="server"></asp:Label><br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[<asp:Label ID="lblVARIABLENAME" Text='<%# Bind("VARIABLENAME") %>'
                                        runat="server"></asp:Label>]
                                </div>
                                <div class="col-md-1">
                                    <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                        OnClientClick="OpenModule(this);"></asp:LinkButton>
                                    <asp:TextBox ID="TXT_FIELD" runat="server" Width="100px" Visible="false" onchange="showChild(this);"></asp:TextBox>
                                    <asp:Repeater runat="server" ID="repeat_CHK">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CHK_FIELD" runat="server" onchange="showChild(this);" CssClass="checkbox"
                                                ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>' Text='<%# Bind("TEXT") %>' />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Repeater runat="server" ID="repeat_RAD">
                                        <ItemTemplate>
                                            <asp:RadioButton ID="RAD_FIELD" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                onchange="showChild(this);" onclick="return RadioCheck(this);" CssClass="radio"
                                                Text='<%# Bind("TEXT") %>' />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="col-md-9">
                                    <asp:Repeater ID="repeater_Data1" OnItemDataBound="repeater_Data1_ItemDataBound"
                                        runat="server">
                                        <ItemTemplate>
                                            <div runat="server" id="divHeader" visible="false" class="col-md-12">
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <asp:Label ID="lblFieldName" Font-Size="Small" Font-Bold="true" Text='<%# Bind("FIELDNAME") %>'
                                                        Style="text-align: LEFT" runat="server"></asp:Label><br />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[<asp:Label ID="lblVARIABLENAME" Text='<%# Bind("VARIABLENAME") %>'
                                                        runat="server"></asp:Label>]
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                        OnClientClick="OpenModule(this);"></asp:LinkButton>
                                                    <asp:TextBox ID="TXT_FIELD" runat="server" Width="100px" Visible="false" onchange="showChild(this);"></asp:TextBox>
                                                    <asp:Repeater runat="server" ID="repeat_CHK">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CHK_FIELD" runat="server" onchange="showChild(this);" CssClass="checkbox"
                                                                ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>' Text='<%# Bind("TEXT") %>' />
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <asp:Repeater runat="server" ID="repeat_RAD">
                                                        <ItemTemplate>
                                                            <asp:RadioButton ID="RAD_FIELD" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                onchange="showChild(this);" onclick="return RadioCheck(this);" CssClass="radio"
                                                                Text='<%# Bind("TEXT") %>' />
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <div class="col-md-12">
                                                        <asp:Repeater ID="repeater_Data2" OnItemDataBound="repeater_Data2_ItemDataBound"
                                                            runat="server">
                                                            <ItemTemplate>
                                                                <div runat="server" id="divHeader" visible="false" class="col-md-12">
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <asp:Label ID="lblFieldName" Font-Size="Small" Font-Bold="true" Text='<%# Bind("FIELDNAME") %>'
                                                                        Style="text-align: LEFT" runat="server"></asp:Label><br />
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[<asp:Label ID="lblVARIABLENAME" Text='<%# Bind("VARIABLENAME") %>'
                                                                        runat="server"></asp:Label>]
                                                                </div>
                                                                <div class="col-md-10">
                                                                    <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                                        OnClientClick="OpenModule(this);"></asp:LinkButton>
                                                                    <asp:TextBox ID="TXT_FIELD" runat="server" Width="100px" Visible="false" onchange="showChild(this);"></asp:TextBox>
                                                                    <asp:Repeater runat="server" ID="repeat_CHK">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="CHK_FIELD" runat="server" onchange="showChild(this);" CssClass="checkbox"
                                                                                ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>' Text='<%# Bind("TEXT") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                    <asp:Repeater runat="server" ID="repeat_RAD">
                                                                        <ItemTemplate>
                                                                            <asp:RadioButton ID="RAD_FIELD" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                onchange="showChild(this);" onclick="return RadioCheck(this);" CssClass="radio"
                                                                                Text='<%# Bind("TEXT") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                    <div class="col-md-12">
                                                                        <asp:Repeater ID="repeater_Data3" OnItemDataBound="repeater_Data3_ItemDataBound"
                                                                            runat="server">
                                                                            <ItemTemplate>
                                                                                <div runat="server" id="divHeader" visible="false">
                                                                                </div>
                                                                                <div class="col-md-4">
                                                                                    <asp:Label ID="lblFieldName" Font-Size="Small" Font-Bold="true" Text='<%# Bind("FIELDNAME") %>'
                                                                                        Style="text-align: LEFT" runat="server"></asp:Label><br />
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[<asp:Label ID="lblVARIABLENAME" Text='<%# Bind("VARIABLENAME") %>'
                                                                                        runat="server"></asp:Label>]
                                                                                </div>
                                                                                <div class="col-md-8">
                                                                                    <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                                                        OnClientClick="OpenModule(this);"></asp:LinkButton>
                                                                                    <asp:TextBox ID="TXT_FIELD" runat="server" Width="100px" Visible="false" onchange="showChild(this);"></asp:TextBox>
                                                                                    <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="CHK_FIELD" runat="server" onchange="showChild(this);" CssClass="checkbox"
                                                                                                ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>' Text='<%# Bind("TEXT") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                    <asp:Repeater runat="server" ID="repeat_RAD">
                                                                                        <ItemTemplate>
                                                                                            <asp:RadioButton ID="RAD_FIELD" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                onchange="showChild(this);" onclick="return RadioCheck(this);" CssClass="radio"
                                                                                                Text='<%# Bind("TEXT") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                </div>
                                                                                <br />
                                                                                <br />
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </div>
                                                                </div>
                                                                <br />
                                                                <br />
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <br />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <br />
                            <br />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
