<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MM_QUERY_HISTORY.aspx.cs" Inherits="CTMS.MM_QUERY_HISTORY" %>

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
    <link href="CommonStyles/MM/MM_Icons.css" rel="stylesheet" />

    <script type="text/jscript">

        function pageLoad() {
            //    $(".Datatable").dataTable();
            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                stateSave: true
            });
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">Query History
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
                <asp:GridView runat="server" ID="grdHistory" AutoGenerateColumns="False" CssClass="Datatable table table-bordered table-striped txtCenter" OnPreRender="grd_data_PreRender">
                    <HeaderStyle CssClass="txtCenter" />
                    <RowStyle CssClass="txtCenter" />
                    <Columns>
                        <asp:TemplateField HeaderText="Activity">
                            <ItemTemplate>
                                <asp:Label ID="ACT_PERFORMED" runat="server" Text='<%# Eval("ACT_PERFORMED") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Details">
                            <ItemTemplate>
                                <asp:Label ID="ACT_DETAILS" runat="server" Text='<%# Eval("ACT_DETAILS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <label>Activity Details</label><br />
                                <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Performed By]</label><br />
                                <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div>
                                    <div>
                                        <asp:Label ID="ACT_BYNAME" runat="server" Text='<%# Bind("ACT_BYNAME") %>' ForeColor="Blue"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="ACT_CAL_DAT" runat="server" Text='<%# Bind("ACT_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="ACT_CAL_TZDAT" runat="server" Text='<%# Bind("ACT_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
