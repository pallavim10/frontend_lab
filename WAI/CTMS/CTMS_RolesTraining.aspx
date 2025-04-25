<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CTMS_RolesTraining.aspx.cs" Inherits="CTMS.CTMS_RolesTraining" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        caption
        {
            font-weight: bold;
        }
    </style>
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

        function Print() {
            if ($("#<%=drpRole.ClientID%>").val() == "0") {
                $("#<%=drpRole.ClientID%>").addClass("brd-1px-redimp");
                return false;
            }

            var ProjectId = '<%= Session["PROJECTID"] %>'
            var Action = "GET_ROLE_TRAINING_DATA";
            var Role = $("#<%= drpRole.ClientID %>").val();
            var test = "ReportCTMS_RolesTraining.aspx?ProjectId=" + ProjectId + "&Action=" + Action + "&Role=" + Role;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500px,width=900px";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">
                        Training plan
                        <asp:LinkButton ID="lbtnprint" runat="server" Text="Print" OnClientClick="return Print()" CssClass="btn-sm">
      <span class="glyphicon glyphicon-print"></span>Print</asp:LinkButton>
                    </h3>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
                <table>
                    <tr>
                        <td class="label">
                            Select Role
                        </td>
                        <td class="requiredSign">
                            <asp:Label ID="Lbl_Dept" runat="server" Text="*"></asp:Label>
                        </td>
                        <td class="Control">
                            <asp:DropDownList ID="drpRole" class="form-control drpControl required" Width="200px"
                                runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpRole_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <br />
            </div>
            <div class="box">
                <asp:GridView ID="gvMain" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    CssClass="table table-bordered table-striped" OnRowDataBound="gvMain_RowDataBound">
                    <Columns>
                        <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                            HeaderStyle-CssClass="txt_center">
                            <HeaderTemplate>
                                <a href="JavaScript:ManipulateAll('_Main');" id="_Folder" style="color: #333333"><i
                                    id="img_Main" class="icon-plus-sign-alt"></i></a>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div runat="server" id="anchor">
                                    <a href="JavaScript:divexpandcollapse('_Main<%# Eval("Task_ID") %>');" style="color: #333333">
                                        <i id="img_Main<%# Eval("Task_ID") %>" class="icon-plus-sign-alt"></i></a>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Categories" ItemStyle-Width="100%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Dept" Width="100%" ToolTip='<%# Bind("Task_Name") %>' CssClass="label"
                                    runat="server" Text='<%# Bind("Task_Name") %>'></asp:Label>
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
                                                    <div id="_Main<%# Eval("Task_ID") %>" style="display: none; position: relative; overflow: auto;">
                                                        <asp:GridView ID="grdTask" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped "
                                                            AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Tasks" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Sub_Task_Name" runat="server" Text='<%# Bind("Sub_Task_Name") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
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
</asp:Content>
