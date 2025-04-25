<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Sponsor_ProjectMgmt_OtherReports.aspx.cs" Inherits="CTMS.Sponsor_ProjectMgmt_OtherReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript">

        function Print(element) {


            if ($(element).closest('tr').find('td:eq(0)').find('input').val() == "1") {
                Print_TOM(element);
            }
            else if ($(element).closest('tr').find('td:eq(0)').find('input').val() == "2") {
                Print_PTIME(element);
            }
            else if ($(element).closest('tr').find('td:eq(0)').find('input').val() == "3") {
                Print_CRATIME(element);
            }
            else if ($(element).closest('tr').find('td:eq(0)').find('input').val() == "4") {
                Print_SITEISS(element);
            }
            else if ($(element).closest('tr').find('td:eq(0)').find('input').val() == "5") {
                Print_Budget(element);
            }


        }

        function Print_TOM(element) {

            var ProjectId = '<%= Session["PROJECTID"] %>'
            var Action = "get_TOM_ProjectTaks_Data";
            var test = "ReportBudgetTOM.aspx?ProjectId=" + ProjectId + "&Action=" + Action;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500px,width=1500px";
            window.open(test, '_blank', strWinProperty);
            return false;
        }


        function Print_PTIME(element) {

            var ProjectId = '<%= Session["PROJECTID"] %>'
            var Action = "get_TOM_ProjectTimeline_Data";
            var test = "ReportBudgetProjectPlan.aspx?ProjectId=" + ProjectId + "&Action=" + Action;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500px,width=1500px";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function Print_CRATIME(element) {
            if ($("#<%=drp_InvID.ClientID%>").val() == "0") {
                $("#<%=drp_InvID.ClientID%>").addClass("brd-1px-redimp");
                return false;
            }

            var ProjectId = '<%= Session["PROJECTID"] %>'
            var Action = "get_CRA_project_Plans_Data";
            var INVID = $("#<%= drp_InvID.ClientID%>").val();
            var test = "ReportCRA_ProjectTimeline.aspx?ProjectId=" + ProjectId + "&Action=" + Action + "&INVID=" + INVID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500px,width=1500px";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function Print_SITEISS(element) {
            if ($("#<%=drp_InvID.ClientID%>").val() == "99") {
                $("#<%=drp_InvID.ClientID%>").addClass("brd-1px-redimp");
                return false;
            }
            if ($("#<%=Drp_Status.ClientID%>").val() == "99") {
                $("#<%=Drp_Status.ClientID%>").addClass("brd-1px-redimp");
                return false;
            }

            var ProjectId = '<%= Session["PROJECTID"] %>';
            var INVID = $("#<%= drp_InvID.ClientID%>").val();
            var Status = $("#<%= Drp_Status.ClientID%>").val();
            var test = "ReportCTMS_IssueList.aspx?ProjectId=" + ProjectId + "&INVID=" + INVID + "&Status=" + Status;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500px,width=1500px";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function Print_Budget(element) {
            if ($("#<%=drp_InvID.ClientID%>").val() == "0") {
                $("#<%=drp_InvID.ClientID%>").addClass("brd-1px-redimp");
                return false;
            }

            var ProjectId = '<%= Session["PROJECTID"] %>'
            var SITEID = $("#<%= drp_InvID.ClientID%>").val();
            var action = "get_Visit_Task_Units_Data";
            var test = "ReportSB_UnitsDetails.aspx?Action=" + action + "&ProjectId=" + ProjectId + "&SITEID=" + SITEID;

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
                Reports
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group has-warning">
                <asp:Label ID="Label1" runat="server"></asp:Label>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2" style="width: auto;">
                            Select Site Id :
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="drp_InvID" runat="server" AutoPostBack="True" Style="text-align: center;"
                                class="form-control drpControl required">
                            </asp:DropDownList>
                        </div>
                        <div class="label col-md-2" style="width: auto;">
                            Select Status :
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="Drp_Status" runat="server" AutoPostBack="True" Style="text-align: center;"
                                class="form-control drpControl required">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
            </div>
        </div>
        </div>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="gvreport" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        CssClass="table table-bordered table-striped">
                        <Columns>
                            <asp:TemplateField HeaderText="ID" ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none">
                                <ItemTemplate>
                                    <asp:Label ID="lblid" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                    <asp:HiddenField ID="hfid" runat="server" Value='<%# Bind("ID") %>'></asp:HiddenField>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblname" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Options" ItemStyle-CssClass="txt_center" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnprint" runat="server" OnClientClick="Print(this);" Text="Print"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
</asp:Content>
