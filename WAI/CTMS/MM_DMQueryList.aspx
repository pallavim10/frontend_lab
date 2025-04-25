<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MM_DMQueryList.aspx.cs"
    Inherits="CTMS.MM_DMQueryList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        var counter = 0;

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
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="Upd_Pan_Sel_Subject" runat="server">
                <ContentTemplate>
                    <div class="box box-warning">
                        <div class="box-header">
                            <h3 class="box-title">eCRF Query List
                            </h3>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <div class="form-group has-warning">
                                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box">
                        <asp:GridView ID="grd" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            CssClass="table table-bordered table-striped Datatable" AlternatingRowStyle-CssClass="alt"
                            PagerStyle-CssClass="pgr" OnRowDataBound="grd_RowDataBound" OnPreRender="GridView_PreRender">
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
                                                <asp:Label ID="QRYGEN_CAL_TZDAT" runat="server" Text='<%# Bind("QRYGEN_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
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
                                                                    CssClass="table table-bordered table-striped">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Comments">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblCOMMENTS" runat="server" Text='<%# Eval("COMMENT") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Reason">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblREASON" runat="server" Text='<%# Eval("REASON") %>'></asp:Label>
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
                                                                                        <asp:Label ID="QRYRES_CAL_TZDAT" runat="server" Text='<%# Bind("ENTERED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
