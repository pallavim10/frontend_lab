<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SB_UnitDetails_SUB.aspx.cs" Inherits="CTMS.SB_UnitDetails_SUB" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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

        function Calculate(element) {
            var Unit = $(element).val(); //unique id for each row
            var Rate = $(element).closest('tr').find('td:eq(5)').find('span').html(); //unique id for each row

            var Total = Unit * Rate;
            //            alert(Total)
            $(element).closest('tr').find('td:eq(7)').find('span').html(Total);

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
            if ($("#<%=ddl_Site.ClientID%>").val() == "99") {
                $("#<%=ddl_Site.ClientID%>").addClass("brd-1px-redimp");
                return false;
            }

            if ($("#<%=ddl_Subject.ClientID%>").val() == "99") {
                $("#<%=ddl_Subject.ClientID%>").addClass("brd-1px-redimp");
                return false;
            }

            var ProjectId = '<%= Session["PROJECTID"] %>'
            var SITEID = $("#<%=ddl_Site.ClientID%>").val();
            var SubjectID = $("#<%=ddl_Subject.ClientID%>").val();
            var action = "get_Visit_Task_Units_Sub_Data";
            var test = "ReportSB_UnitDetails_SUB.aspx?Action=" + action + "&ProjectId=" + ProjectId + "&SITEID=" + SITEID + "&SubjectID=" + SubjectID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500px,width=1000px";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Manage Task Rates
                <asp:LinkButton ID="lbtnprint" runat="server" Text="Print" OnClientClick="return Print()"
                    CssClass="btn-sm">
      <span class="glyphicon glyphicon-print"></span>Print</asp:LinkButton>
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-1">
                            Select Site:
                        </div>
                        <div class="requiredSign col-md-1">
                            <asp:Label ID="Label4" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddl_Site" Style="text-align: center;" runat="server" class="form-control drpControl required"
                                ValidationGroup="View" AutoPostBack="true" OnSelectedIndexChanged="ddl_INVID_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="label col-md-1" style="width: auto;">
                            Select Subject :
                        </div>
                        <div class="requiredSign col-md-1">
                            <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddl_Subject" Style="text-align: center;" runat="server" class="form-control drpControl required"
                                ValidationGroup="View" AutoPostBack="true" OnSelectedIndexChanged="ddl_Subject_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
            </div>
        </div>
        <div>
            <asp:GridView ID="gvVisits" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CssClass="table table-bordered table-striped Datatable1" OnRowDataBound="gvVisits_RowDataBound">
                <Columns>
                    <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                        HeaderStyle-CssClass="txt_center">
                        <HeaderTemplate>
                            <a href="JavaScript:ManipulateAll('_mydiv');" id="_Folder" style="color: #333333"><i
                                id="img_mydiv" class="icon-plus-sign-alt"></i></a>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <a href="JavaScript:divexpandcollapse('_mydiv<%# Eval("ID") %>');" style="color: #333333">
                                <i id="img_mydiv<%# Eval("ID") %>" class="icon-plus-sign-alt"></i></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                        HeaderText="ID">
                        <ItemTemplate>
                            <asp:Label ID="lbl_VisitID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                            <asp:HiddenField ID="hf_VisitID" runat="server" Value='<%# Bind("ID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Visits" ItemStyle-Width="100%">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Visits" Width="100%" ToolTip='<%# Bind("Visit") %>' CssClass="label"
                                runat="server" Text='<%# Bind("Visit") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount" ControlStyle-CssClass="txt_center" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Amt" Width="100%" CssClass="label" runat="server"></asp:Label>
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
                                                <div id="_mydiv<%# Eval("ID") %>" style="display: none; position: relative; overflow: auto;">
                                                    <asp:GridView ID="gvRates" runat="server" OnRowDataBound="gvRates_RowDataBound" AutoGenerateColumns="false"
                                                        CssClass="table table-bordered table-striped Datatable1">
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                                                                HeaderText="ID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_ActID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                    <asp:HiddenField ID="hf_ActID" runat="server" Value='<%# Bind("ID") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                                                                HeaderText="ID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_TaskId" runat="server" Text='<%# Bind("Task_Id") %>'></asp:Label>
                                                                    <asp:HiddenField ID="hf_TaskID" runat="server" Value='<%# Bind("Task_Id") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                                                                HeaderText="ID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_SubTaskId" runat="server" Text='<%# Bind("Sub_Task_Id") %>'></asp:Label>
                                                                    <asp:HiddenField ID="hf_SubTaskID" runat="server" Value='<%# Bind("Sub_Task_Id") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sub-Tasks">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_TaskName" Width="60%" ToolTip='<%# Bind("Sub_Task_Name") %>' CssClass="label"
                                                                        runat="server" Text='<%# Bind("Sub_Task_Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Type" ItemStyle-CssClass="txt_center" ItemStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList runat="server" ID="ddlType" Style="text-align: center;" CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Rates" ItemStyle-CssClass="txt_center" ItemStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <asp:Label Style="text-align: center;" ID="lblRate" Text='<%# Bind("Rate") %>' runat="server"
                                                                        class="txt_center label"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Units" ItemStyle-CssClass="txt_center" ItemStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox Style="text-align: center;" ID="txtUnits" Text='<%# Bind("Units") %>'
                                                                        onchange="Calculate(this)" runat="server" class="txt_center form-control"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount" ItemStyle-CssClass="txt_center" ItemStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <asp:Label Style="text-align: center;" ID="lblAmount" Text='<%# Bind("Amount") %>'
                                                                        runat="server" class="txt_center label"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="8%" HeaderText="Shift To">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList runat="server" ID="ddlShift" Style="text-align: center; width: 100px;"
                                                                        CssClass="form-control">
                                                                    </asp:DropDownList>
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
            <br />
            <div class="txt_center">
                <asp:Button ID="btnsubmit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                    ValidationGroup="section" OnClick="btnsubmit_Click" />
            </div>
            <br />
            <br />
        </div>
    </div>
</asp:Content>
