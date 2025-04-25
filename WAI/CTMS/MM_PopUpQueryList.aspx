<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MM_PopUpQueryList.aspx.cs"
    Inherits="CTMS.MM_PopUpQueryList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/Jquery1.12.4.js" type="text/javascript"></script>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Common-Bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap-theme.css" rel="stylesheet" type="text/css" />
    <link href="Styles/AdminLTE.css" rel="stylesheet" type="text/css" />
    <link href="Styles/ionicons.css" rel="stylesheet" type="text/css" />
    <link href="Styles/font-awesome.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="img/favicon.ico" type="image/x-icon">
    <link rel="shortcut icon" href="img/favicon.ico" type="image/x-icon" />
    <%-- <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap-theme.css" rel="stylesheet" type="text/css" />--%>
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <!-- for pikaday datepicker//-->
    <link href="Styles/pikaday.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/moment.js" type="text/javascript"></script>
    <script src="Scripts/pikaday.js" type="text/javascript"></script>
    <script src="Scripts/pikaday.jquery.js" type="text/javascript"></script>
    <!-- for pikaday datepicker//-->
    <!-- for jquery Datatable.1.10.15//-->
    <script src="Scripts/Datatable.1.10.15.UI.js" type="text/javascript"></script>
    <script src="Scripts/Datatable1.10.15.js" type="text/javascript"></script>
    <script type="text/jscript">

        function pageLoad() {
            //    $(".Datatable").dataTable();
            $(".Datatable1").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                stateSave: true
            });

            $(".nav-tabs a").click(function (event) {
                event.preventDefault();
                $(this).parent().addClass("active");
                $(this).parent().siblings().removeClass("active");
                var tab = $(this).attr("href");
                $(".tab-content").not(tab).css("display", "none");
                $(tab).fadeIn();
            });
        }
    </script>
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script src="CommonFunctionsJs/MM/MM_DivExpand.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="Upd_Pan_Sel_Subject" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    <div id="tab" class="nav-tabs-custom ">
                        <ul class="nav nav-tabs">
                            <li class="active"><a href="#tab-1" data-toggle="tab">Medical Monitoring Queries </a>
                            </li>
                            <li><a href="#tab-2" data-toggle="tab">Data Management Queries </a></li>
                        </ul>
                        <div id="tab-1" class="tab-content active">
                            <div class="box">
                                <asp:GridView ID="grd" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    CssClass="table table-bordered table-striped Datatable" AlternatingRowStyle-CssClass="alt"
                                    PagerStyle-CssClass="pgr" OnPreRender="GridView_PreRender"
                                    OnRowDataBound="grd_RowDataBound">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                                            HeaderStyle-CssClass="txt_center">
                                            <HeaderTemplate>
                                                <a href="JavaScript:ManipulateAll('_MMQUERY');" id="_MMQUERY" style="color: #333333">
                                                    <i id="img_MMQUERY" class="icon-plus-sign-alt"></i></a>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div runat="server" id="anchor">
                                                    <a href="JavaScript:divexpandcollapse('_MMQUERY<%# Eval("ID") %>');" style="color: #333333">
                                                        <i id="img_MMQUERY<%# Eval("ID") %>" class="icon-plus-sign-alt"></i></a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Query Id" ItemStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Query Text">
                                            <ItemTemplate>
                                                <asp:Label ID="QUERYTEXT" runat="server" Text='<%# Eval("QUERYTEXT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Listing" ItemStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                <asp:Label ID="SOURCE" runat="server" Text='<%# Eval("SOURCE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Query Type" ItemStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                <asp:Label ID="QUERYTYPE" runat="server" Text='<%# Eval("QUERYTYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                <asp:Label ID="STATUS" runat="server" Text='<%# Eval("STATUS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="align-left">
                                            <HeaderTemplate>
                                                <label>Raised</label><br />
                                                <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Raised By]</label><br />
                                                <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="RAISEDBYNAME" runat="server" Text='<%# Bind("RAISEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="RAISED_CAL_DAT" runat="server" Text='<%# Bind("RAISED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="RAISED_CAL_TZDAT" runat="server" Text='<%# Eval("RAISED_CAL_TZDAT")+" "+ Eval("RAISED_TZVAL") %>' ForeColor="Red"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="align-left">
                                            <HeaderTemplate>
                                                <label>Deleted</label><br />
                                                <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Deleted By]</label><br />
                                                <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                <label style="font-weight: lighter; margin-bottom: 0px;">[Reason for Delete]</label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="DELETEDBYNAME" runat="server" Text='<%# Bind("DELETEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="DELETED_CAL_DAT" runat="server" Text='<%# Bind("DELETED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="DELETED_CAL_TZDAT" runat="server" Text='<%# Eval("DELETED_CAL_TZDAT")+" "+Eval("DELETED_TZVAL") %>' ForeColor="Red"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="DELETED_COMMENTS" runat="server" Text='<%# Bind("DELETED_COMMENTS") %>' ForeColor="Red"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="align-left">
                                            <HeaderTemplate>
                                                <label>Reviewed</label><br />
                                                <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Reviewed By]</label><br />
                                                <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="MM_REVIEWBYNAME" runat="server" Text='<%# Bind("MM_REVIEWBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="MM_REVIEW_CAL_DAT" runat="server" Text='<%# Bind("MM_REVIEW_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="MM_REVIEW_CAL_TZDAT" runat="server" Text='<%# Eval("MM_REVIEW_CAL_TZDAT") +" "+ Eval("MM_REVIEW_TZVAL") %>' ForeColor="Red"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="align-left">
                                            <HeaderTemplate>
                                                <label>Pushed/Linked</label><br />
                                                <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Pushed/Linked By]</label><br />
                                                <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="LINKED_PUSHED_BYNAME" runat="server" Text='<%# Bind("LINKED_PUSHED_BYNAME") %>' ForeColor="Blue"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="LINKED_PUSHED_CAL_DAT" runat="server" Text='<%# Bind("LINKED_PUSHED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="LINKED_PUSHED_CAL_TZDAT" runat="server" Text='<%# Eval("LINKED_PUSHED_CAL_TZDAT")+" "+ Eval("LINKED_PUSHED_TZVAL") %>' ForeColor="Red"></asp:Label>
                                                </div>
                                                <div>
                                                    DM Query ID :
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("DM_QUERYID") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="align-left">
                                            <HeaderTemplate>
                                                <label>Resolved</label><br />
                                                <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Resolved By]</label><br />
                                                <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="RESOLVEDBYNAME" runat="server" Text='<%# Bind("RESOLVEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="RESOLVED_CAL_DAT" runat="server" Text='<%# Bind("RESOLVED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="RESOLVED_CAL_TZDAT" runat="server" Text='<%# Eval("RESOLVED_CAL_TZDAT") +" "+ Eval("RESOLVED_TZVAL") %>' ForeColor="Red"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <tr>
                                                    <td colspan="100%" style="padding: 2px;">
                                                        <div style="float: right; font-size: 13px;">
                                                        </div>
                                                        <div>
                                                            <div class="rows">
                                                                <div class="col-md-12">
                                                                    <div id="_MMQUERY<%# Eval("ID") %>" style="display: none; position: relative; overflow: auto;">
                                                                        <asp:GridView ID="grdComments" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                            CssClass="Datatable table table-bordered table-striped" OnPreRender="GridView_PreRender">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Reason">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Reason" runat="server" Text='<%# Eval("Reason") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Comments">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblCOMMENTS" runat="server" Text='<%# Eval("COMMENT") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderStyle-CssClass="align-left">
                                                                                    <HeaderTemplate>
                                                                                        <label>Comment Details</label><br />
                                                                                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Commented By]</label><br />
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
                                                                                                <asp:Label ID="ENTERED_CAL_TZDAT" runat="server" Text='<%# Eval("ENTERED_CAL_TZDAT")+" "+Eval("ENTERED_TZVAL") %>' ForeColor="Red"></asp:Label>
                                                                                            </div>
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <RowStyle ForeColor="Blue" />
                                                                            <HeaderStyle ForeColor="Blue" />
                                                                        </asp:GridView>
                                                                    </div>
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
                        <div id="tab-2" class="tab-content">
                            <div class="box">
                                <asp:GridView ID="grdDM" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    CssClass="table table-bordered table-striped Datatable" AlternatingRowStyle-CssClass="alt"
                                    PagerStyle-CssClass="pgr" OnRowDataBound="grdDM_RowDataBound" OnPreRender="GridView_PreRender">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                                            HeaderStyle-CssClass="txt_center">
                                            <HeaderTemplate>
                                                <a href="JavaScript:ManipulateAll('_DMQUERY');" id="_DMQUERY" style="color: #333333">
                                                    <i id="img_DMQUERY" class="icon-plus-sign-alt"></i></a>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div runat="server" id="anchor">
                                                    <a href="JavaScript:divexpandcollapse('_DMQUERY<%# Eval("ID") %>');" style="color: #333333">
                                                        <i id="img_DMQUERY<%# Eval("ID") %>" class="icon-plus-sign-alt"></i></a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Query Id" ItemStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                <asp:Label ID="Id" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Site ID">
                                            <ItemTemplate>
                                                <asp:Label ID="INVID" runat="server" Text='<%# Bind("INVID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Subject ID">
                                            <ItemTemplate>
                                                <asp:Label ID="SUBJID" runat="server" Text='<%# Bind("SUBJID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Visit" HeaderStyle-CssClass="align-left">
                                            <ItemTemplate>
                                                <asp:Label ID="VISIT" runat="server" Text='<%# Bind("VISIT") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Module">
                                            <ItemTemplate>
                                                <asp:Label ID="MODULENAME" runat="server" Text='<%# Bind("MODULENAME") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Field Name" HeaderStyle-CssClass="align-left">
                                            <ItemTemplate>
                                                <asp:Label ID="FIELDNAME" runat="server" Text='<%# Bind("FIELDNAME") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rule description" HeaderStyle-CssClass="align-left">
                                            <ItemTemplate>
                                                <asp:Label ID="Description" runat="server" Text='<%# Bind("Description") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Query text" HeaderStyle-CssClass="align-left">
                                            <ItemTemplate>
                                                <asp:Label ID="QUERYTEXT" runat="server" Text='<%# Bind("QUERYTEXT") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Query Status" HeaderStyle-CssClass="align-left">
                                            <ItemTemplate>
                                                <asp:Label ID="STATUSTEXT" runat="server" Text='<%# Bind("STATUSTEXT") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Query Type" HeaderStyle-CssClass="align-left">
                                            <ItemTemplate>
                                                <asp:Label ID="QUERYTYPE" runat="server" Text='<%# Bind("QUERYTYPE") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="align-left">
                                            <HeaderTemplate>
                                                <label>Generated</label><br />
                                                <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Generated By]</label><br />
                                                <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div runat="server" id="divGenerated">
                                                    <div>
                                                        <asp:Label ID="QRYGENBYNAME" runat="server" Text='<%# Bind("QRYGENBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="QRYGEN_CAL_DAT" runat="server" Text='<%# Bind("QRYGEN_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="QRYGEN_CAL_TZDAT" runat="server" Text='<%# Eval("QRYGEN_CAL_TZDAT") + " " + Eval("") %>' ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="align-left">
                                            <HeaderTemplate>
                                                <label>Resolved</label><br />
                                                <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Resolved By]</label><br />
                                                <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div runat="server" id="divResolved">
                                                    <div>
                                                        <asp:Label ID="QRYRESBYNAME" runat="server" Text='<%# Bind("QRYRESBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="QRYRES_CAL_DAT" runat="server" Text='<%# Bind("QRYRES_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="QRYRES_CAL_TZDAT" runat="server" Text='<%# Bind("QRYRES_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <tr>
                                                    <td colspan="100%" style="padding: 2px;">
                                                        <div style="float: right; font-size: 13px;">
                                                        </div>
                                                        <div>
                                                            <div class="rows">
                                                                <div class="col-md-12">
                                                                    <div id="_DMQUERY<%# Eval("ID") %>" style="display: none; position: relative; overflow: auto;">
                                                                        <asp:GridView ID="grdDMComments" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                            CssClass="Datatable table table-bordered table-striped" OnPreRender="GridView_PreRender">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Reason">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblREASON" runat="server" Text='<%# Eval("REASON") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Comments">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblCOMMENTS" runat="server" Text='<%# Eval("COMMENT") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderStyle-CssClass="align-left">
                                                                                    <HeaderTemplate>
                                                                                        <label>Comment</label><br />
                                                                                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Commented By]</label><br />
                                                                                        <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                                                        <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <div runat="server" id="divResolved">
                                                                                            <div>
                                                                                                <asp:Label ID="QRYRESBYNAME" runat="server" Text='<%# Bind("ENTEREDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                                                            </div>
                                                                                            <div>
                                                                                                <asp:Label ID="QRYRES_CAL_DAT" runat="server" Text='<%# Bind("ENTERED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                                                            </div>
                                                                                            <div>
                                                                                                <asp:Label ID="QRYRES_CAL_TZDAT" runat="server" Text='<%# Eval("ENTERED_CAL_TZDAT")+" "+Eval("ENTERED_TZVAL") %>' ForeColor="Red"></asp:Label>
                                                                                            </div>
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <RowStyle ForeColor="Blue" />
                                                                            <HeaderStyle ForeColor="Blue" />
                                                                        </asp:GridView>
                                                                    </div>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
