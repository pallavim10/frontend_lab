<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Doc_TrackChanges.aspx.cs"
    Inherits="CTMS.Doc_TrackChanges" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
    <link rel="icon" href="img/favicon.ico" type="image/x-icon" />
    <link rel="shortcut icon" href="img/favicon.ico" type="image/x-icon" />
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="js/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>
    <script src="js/plugins/datatables/dataTables.bootstrap.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="Styles/datatables/dataTables.bootstrap.css" />
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false
            });
        }
        
    </script>
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
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
    <script src="js/htmldiff.js" type="text/javascript"></script>
    <style type="text/css">
        ins
        {
            text-decoration: none;
            background-color: #d4fcbc;
        }
        
        del
        {
            text-decoration: line-through;
            background-color: #fbb6c2;
            color: #555;
        }
    </style>
    <script language="javascript" type="text/javascript">

        function setDiffvalue(element) {
            var oldHTML;
            var newHTML;
            var diffHTML;


            newHTML = $(element).closest('td').find('div:eq(5)').html().trim();
            oldHTML = $(element).closest('td').find('div:eq(6)').html().trim();

            // Diff HTML strings
            diffHTML = htmldiff(oldHTML, newHTML);

            // Show HTML diff output as HTML (crazy right?)!
            $(element).html(diffHTML);
        }

    </script>
    <script language="javascript" type="text/javascript">
        $(function diffChange() {

            var a;

            var divDiff = $("div[id*='divDiff']").toArray();
            for (a = 0; a < divDiff.length; ++a) {
                setDiffvalue(divDiff[a]);

            }

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="box box-warning">
                    <div class="box-header">
                        <h3 class="box-title">
                            <asp:Label ID="lblHeader" runat="server"></asp:Label>
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
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-5">
                                Legends : &nbsp;&nbsp;<ins>&nbsp;Inserted&nbsp; </ins>&nbsp;<del>&nbsp;Deleted &nbsp;</del>
                            </div>
                        </div>
                    </div>
                    <br />
                    <asp:GridView ID="gvAuditTrail" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        CssClass="table table-bordered table-striped">
                        <Columns>
                            <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                                HeaderStyle-CssClass="txt_center" ItemStyle-Width="2%">
                                <HeaderTemplate>
                                    <a href="JavaScript:ManipulateAll('_Audit_');" id="_Folder" style="color: #333333"><i
                                        id="img_Audit_" class="icon-plus-sign-alt"></i></a>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div runat="server" id="anchor">
                                        <a href="JavaScript:divexpandcollapse('_Audit_<%# Eval("ID") %>');" style="color: #333333">
                                            <i id="img_Audit_<%# Eval("ID") %>" class="icon-plus-sign-alt"></i></a>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false" HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_AuditID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User" ItemStyle-Width="80%">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_User" Width="100%" ToolTip='<%# Bind("User_Name") %>' CssClass="label"
                                        runat="server" Text='<%# Bind("User_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date-Time" ItemStyle-Width="20%" ItemStyle-CssClass="txt_center">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_DateTime" Width="100%" ToolTip='<%# Bind("DateTime") %>' CssClass="label"
                                        runat="server" Text='<%# Bind("DateTime") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <tr>
                                        <td colspan="100%" style="padding: 2px; background-color: White;">
                                            <div style="float: right; font-size: 13px;">
                                            </div>
                                            <div>
                                                <div class="rows">
                                                    <div class="col-md-12">
                                                        <div id="_Audit_<%# Eval("ID") %>" style="display: none; position: relative; overflow: auto;">
                                                            <%--<asp:Literal runat="server" ID="litNew" Visible="false" Text='<%# Eval("NEWDATA") %>'></asp:Literal>--%>
                                                            <%--<asp:Literal runat="server" ID="litOld" Visible="false" Text='<%# Eval("OLDDATA") %>'></asp:Literal>--%>
                                                            <%--<asp:Literal runat="server" ID="litDiff"></asp:Literal>--%>
                                                            <div id="divNew" class="disp-none">
                                                                <%# Eval("NEWDATA") %>
                                                            </div>
                                                            <div id="divOld" class="disp-none">
                                                                <%# Eval("OLDDATA") %>
                                                            </div>
                                                            <div id="divDiff" class="Diff">
                                                            </div>
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
