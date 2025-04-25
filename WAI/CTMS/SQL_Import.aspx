<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="SQL_Import.aspx.cs"
    Inherits="CTMS.SQL_Import" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
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
    <style type="text/css">
        .partHeader
        {
            font-weight: bold;
        }
        
        .bottom-Margin
        {
            margin-bottom: 0px !important;
        }
        
        .modalbackground
        {
            background-color: gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
        
        .modalpopup
        {
            /*border: 1px solid black;
        background-color:white;
        font-family:Verdana;
        font-size:small;
        padding:3px;*/
        }
        
        .myimageClass
        {
            padding: 2px;
            margin: 2px 2px 5px 2px;
            width: 20px;
            height: 20px;
        }
        
        /*.top-nav
        {
            margin-top: 0px;
            height: 25px;
        }*/
        #linkMailBox
        {
            margin-top: 3px;
        }
        /*.divDashboard
       {
           width :98%; height :200px;  margin-bottom :5px; margin-top :5px; margin-left :5px; margin-right :5px;
           
           }*/
        .productDescription
        {
            float: right;
            z-index: 1;
            position: absolute;
        }
        
        .notice-border
        {
            display: none;
            float: left;
            padding: 0px 0;
            margin: 2px 0 0;
            list-style: none;
            text-shadow: none;
            background-color: #fcfcfc;
            border: 4px solid rgba(0, 2, 1, 0.2);
            -webkit-border-radius: 2px;
            -moz-border-radius: 2px;
            border-radius: 2px;
            -webkit-box-shadow: 0 5px 10px rgba(0, 2, 1, 0.4);
            -moz-box-shadow: 0 5px 10px rgba(0, 2, 1, 0.4);
            box-shadow: 0 5px 10px rgba(0, 2, 1, 0.4);
            -webkit-background-clip: padding-box;
            -moz-background-clipp: padding;
            background-clip: padding-box;
            padding: 0px 0;
            margin: 0px;
            list-style: none;
            text-shadow: none;
        }
        
        .Star
        {
            background-image: url(Images/EmptyStar.png);
            height: 21px;
            width: 21px;
        }
        
        .WaitingStar
        {
            background-image: url(Images/WaitingStar.png);
            height: 21px;
            width: 21px;
        }
        
        .FilledStar
        {
            background-image: url(Images/FilledStar.png);
            height: 21px;
            width: 21px;
        }
        
        div.treeview td
        {
            vertical-align: bottom;
        }
        
        div.treeviewMiddle td
        {
            vertical-align: top;
            padding-top: 8px;
        }
        
        div.treeviewMiddle img
        {
            width: 11px;
            height: 11px; /*padding-top :5px;
           padding-right :5px;*/
            margin-top: -5px;
        }
        
        .tdTreeviewNode
        {
            vertical-align: top !important;
            padding-left: 2px;
        }
        
        .tdTreeviewNoPadding
        {
            vertical-align: middle !important;
            padding-top: 0px !important;
        }
    </style>
</head>
<body style="background-color: #ecf0f5;">
    <form id="form1" runat="server" style="margin-left: 50px; margin-right: 50px;">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--    <section class="content-header" style="margin-top: 10px; margin-bottom: 10px;">
            <h1>
                <asp:Label ID="Label14564" runat="server" Text="SQL Import Tool"></asp:Label>
            </h1>
        </section>--%>
    <div>
        <div class="no-padding">
            <div align="center">
                <div class="rows">
                    <div class="col-md-12" style="padding-left: 0px; padding-right: 0px;">
                        <div class="text-center">
                            <asp:Label runat="server" ID="lblError" Text="" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="rows">
                <div class="col-md-12" style="padding-left: 0px; padding-right: 0px;">
                    <div class="col-md-6">
                        <div class="box box-success" style="height: 130px;">
                            <div class="box-body">
                                <div>
                                    <div class="rows">
                                        <div class="col-md-12">
                                            <div class="col-md-4" style="text-align: left;">
                                                <label>
                                                    Select Excel Sheet :</label>
                                            </div>
                                            <div class="col-md-8" style="text-align: left;">
                                                <asp:FileUpload ID="fileUpload" runat="server" />
                                            </div>
                                        </div>
                                        <div class="col-md-12 text-center">
                                            <asp:Label ID="Label1" CssClass="warning" runat="server" ForeColor="Red" Text="[ Note : Excel Sheet Columns should not contain Space. ]"></asp:Label>
                                        </div>
                                        <%--<div class="col-md-12">
                                                <div class="col-md-4" style="text-align: left;">
                                                    <asp:CheckBox runat="server" ID="chkSheet"  AutoPostBack="true" OnCheckedChanged="chkSheet_CheckedChanged" Text="Enter Sheet Name (If Excel file contains multiple sheets.) :" />
                                                </div>
                                                <div class="col-md-8" style="text-align: left;">
                                                    <asp:TextBox Enabled="false" ID="txtSheet" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>--%>
                                        <div class="col-md-12 text-center">
                                            <br />
                                            <asp:LinkButton runat="server" ID="btnUpload" CssClass="btn btn-info" Text="Upload"
                                                OnClick="btnUpload_Click"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-success" style="height: 130px;">
                            <div class="box-body">
                                <div>
                                    <div class="rows">
                                        <div class="col-md-12">
                                            <div class="col-md-4" style="text-align: left;">
                                                <label>
                                                    Select Table :</label>
                                            </div>
                                            <div class="col-md-7" style="text-align: left;">
                                                <asp:DropDownList runat="server" ID="ddlTable" CssClass="form-control" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlTable_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-1" style="text-align: left;">
                                                <asp:RequiredFieldValidator ID="rfvddlTable" runat="server" ControlToValidate="ddlTable"
                                                    ForeColor="Red" ErrorMessage="*" InitialValue="-Select-" ValidationGroup="lbtnAdd"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                                <label>
                                                    Add Column</label>
                                                <br />
                                                <div class="text-center">
                                                    <asp:CheckBox ID="chkAddColumn" runat="server" AutoPostBack="true" OnCheckedChanged="chkAddColumn_CheckedChanged" />
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <label>
                                                    Column Name</label>
                                                <asp:TextBox Enabled="false" ID="txtColumn" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtColumn" runat="server" ControlToValidate="txtColumn"
                                                    ForeColor="Red" ErrorMessage="*" ValidationGroup="lbtnAdd"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-3">
                                                <label>
                                                    Data Type</label>
                                                <asp:DropDownList Enabled="false" runat="server" ID="ddlDataType" AutoPostBack="true"
                                                    CssClass="form-control">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlDataType" runat="server" ControlToValidate="ddlDataType"
                                                    ForeColor="Red" ErrorMessage="*" InitialValue="-Select-" ValidationGroup="lbtnAdd"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-3">
                                                <label>
                                                </label>
                                                <br />
                                                <asp:LinkButton runat="server" Enabled="false" ID="lbtnAdd" CssClass="btn btn-info"
                                                    Text="Add" ValidationGroup="lbtnAdd" OnClick="lbtnAdd_Click"></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="rows">
                <div class="col-md-12" style="padding-left: 0px; padding-right: 0px;">
                    <div class="col-md-4">
                        <div class="box box-success">
                            <div class="box-header with-border">
                                <h4>
                                    Excel Sheet Columns</h4>
                            </div>
                            <div style="overflow: auto; width: 100%; height: 200px;">
                                <div class="box-body">
                                    <div>
                                        <div class="rows">
                                            <div class="col-md-12">
                                                <asp:GridView ID="gvExcelCols" AutoGenerateColumns="false" CssClass="table-bordered table-advance table-hover"
                                                    runat="server">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Columns">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2" CommandArgument='<%# Eval("Column") %>' Text='<%# Eval("Column") %>'
                                                                    runat="server"></asp:Label>
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
                    <div class="col-md-8">
                        <div class="box box-success">
                            <div class="box-body">
                                <div>
                                    <div class="rows">
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                <h4>
                                                    Columns</h4>
                                                <div style="overflow: auto; width: 100%; height: 200px;">
                                                    <asp:GridView ID="gvSqlColumns" AutoGenerateColumns="false" CssClass="table-bordered table-advance table-hover"
                                                        runat="server">
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Columns">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label3" CommandArgument='<%# Eval("Columns") %>' Text='<%# Eval("Columns") %>'
                                                                        runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Data Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label4" CommandArgument='<%# Eval("DataType") %>' Text='<%# Eval("DataType") %>'
                                                                        runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="NULLABLE">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label5" CommandArgument='<%# Eval("NullAble") %>' Text='<%# Eval("NullAble") %>'
                                                                        runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <h4>
                                                    Constraints</h4>
                                                <div style="overflow: auto; width: 100%; height: 200px;">
                                                    <asp:GridView ID="gvSqlConstraint" Width="100%" AutoGenerateColumns="false" CssClass="table-bordered table-advance table-hover"
                                                        runat="server">
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Columns">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label6" CommandArgument='<%# Eval("Columns") %>' Text='<%# Eval("Columns") %>'
                                                                        runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Constraint Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label7" CommandArgument='<%# Eval("CONSTRAINT_TYPE") %>' Text='<%# Eval("CONSTRAINT_TYPE") %>'
                                                                        runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Constraint Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label8" CommandArgument='<%# Eval("CONSTRAINT_NAME") %>' Text='<%# Eval("CONSTRAINT_NAME") %>'
                                                                        runat="server"></asp:Label>
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
                    </div>
                </div>
            </div>
        </div>
        <div class="rows">
            <div class="col-md-12" style="padding-left: 0px; padding-right: 0px;">
                <div class="box box-success">
                    <div class="box-header with-border">
                        <h4 class="box-title">
                            Create Mappings</h4>
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label9" CssClass="warning" runat="server"
                            ForeColor="Red" Text="[ Note : Not Nullable Columns should be Mapped. ]"></asp:Label>
                    </div>
                    <%--<div style="overflow: auto; width: 100%; height: 200px;">--%>
                    <div class="box-body">
                        <div>
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <label>
                                        Select SQL Column :
                                    </label>
                                    <asp:DropDownList ID="ddlSqlCols" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDdlSqlCols" ControlToValidate="ddlSqlCols" ForeColor="Red"
                                        runat="server" InitialValue="-Select-" ValidationGroup="btnSubmit"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-2">
                                    <label>
                                        Select Excel Sheet Column :
                                    </label>
                                    <asp:DropDownList ID="ddlExcelCols" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlExcelCols" ControlToValidate="ddlExcelCols"
                                        ForeColor="Red" runat="server" InitialValue="-Select-" ValidationGroup="btnSubmit"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-1">
                                    <label>
                                        Merge :
                                    </label>
                                    <br />
                                    <asp:CheckBox ID="chkMerge" runat="server" AutoPostBack="true" OnCheckedChanged="chkMerge_CheckedChanged" />
                                </div>
                                <div class="col-md-4" visible="false" id="divDDLs" runat="server" style="text-align: left;">
                                    <asp:Panel runat="server" ID="pnlDDLs">
                                    </asp:Panel>
                                </div>
                                <div class="col-md-1" runat="server" id="divMerge" visible="false">
                                    <label>
                                    </label>
                                    <br />
                                    <asp:LinkButton ID="lbtnAddMergeCol" OnClick="lbtnAddMergeCol_Click" Enabled="false"
                                        runat="server">
                                        <i id="I1" runat="server" class="fa fa-plus-square fa fa-2x"></i>
                                    </asp:LinkButton>
                                </div>
                                <div class="col-md-2">
                                    <label>
                                    </label>
                                    <br />
                                    <asp:LinkButton ID="lbtnSubmit" CssClass="btn btn-info" Text="Submit" OnClick="lbtnSubmit_Click"
                                        runat="server" ValidationGroup="btnSubmit"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--</div>--%>
                </div>
            </div>
        </div>
        <div class="rows">
            <div class="col-md-12" style="padding-left: 0px; padding-right: 0px;">
                <div class="box box-success">
                    <div class="box-header with-border" align="left">
                        <h4 class="box-title">
                            Mappings</h4>
                        <div class="pull-right">
                            <asp:LinkButton runat="server" ID="lbtnRun" CssClass="btn btn-info" Text="Run Mappings"
                                OnClick="lbtnRun_Click"></asp:LinkButton>
                        </div>
                    </div>
                    <div class="box-body">
                        <div>
                            <div class="rows">
                                <div class="col-md-12">
                                    <asp:GridView runat="server" CssClass="table-bordered table-advance table-hover"
                                        Width="100%" ID="gvMappings" AutoGenerateColumns="false" OnRowCommand="gvMappings_RowCommand"
                                        OnRowDataBound="gvMappings_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Destination Table">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label10" runat="server" CssClass="form-control" Text='<%# Eval("DestTable") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Destination Column">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label11" runat="server" CssClass="form-control" Text='<%# Eval("DestColumn") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Source Column">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label12" runat="server" CssClass="form-control" Text='<%# Eval("Sources") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="form-control" CommandName="Del"
                                                        CommandArgument='<%# Eval("Id") %>'>
                                                            <i class="icon-trash"></i>
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
    </div>
    </form>
</body>
</html>
