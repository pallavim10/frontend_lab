<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Sponsor_SiteBudget_Reports.aspx.cs" Inherits="CTMS.Sponsor_SiteBudget_Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript">

        function Print(element) {


            if ($(element).closest('tr').find('td:eq(0)').find('input').val() == "1") {
                Print_SITEBUDGET(element);
            }
            if ($(element).closest('tr').find('td:eq(0)').find('input').val() == "2") {
                Print_SubjectBUDGET(element);
            }
        }

        function Print_SITEBUDGET(element) {
            if ($("#<%=drp_InvID.ClientID%>").val() == "0") {
                $("#<%=drp_InvID.ClientID%>").addClass("brd-1px-redimp");
                return false;
            }

            var ProjectId = '<%= Session["PROJECTID"] %>'
            var INVID = $("#<%= drp_InvID.ClientID %>").val();
            var action = "get_Visit_Task_Units_Data";
            var test = "ReportSB_UnitsDetails.aspx?Action=" + action + "&ProjectId=" + ProjectId + "&SITEID=" + INVID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500px,width=1000px";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function Print_SubjectBUDGET(element) {
            if ($("#<%=drp_InvID.ClientID%>").val() == "0") {
                $("#<%=drp_InvID.ClientID%>").addClass("brd-1px-redimp");
                return false;
            }
            if ($("#<%=ddl_Subject.ClientID%>").val() == "99") {
                $("#<%=ddl_Subject.ClientID%>").addClass("brd-1px-redimp");
                return false;
            }

            var ProjectId = '<%= Session["PROJECTID"] %>'
            var INVID = $("#<%= drp_InvID.ClientID %>").val();
            var Status = $("#<%= ddl_Subject.ClientID %>").val();
            var action = "get_Visit_Task_Units_Sub_Data";
            var test = "ReportSB_UnitDetails_SUB.aspx?Action=" + action + "&ProjectId=" + ProjectId + "&SITEID=" + INVID + "&SubjectID=" + Status;

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
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2" style="width: auto;">
                            Select Site Id :
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="drp_InvID" Style="text-align: center;" runat="server" class="form-control drpControl required"
                                ValidationGroup="View" AutoPostBack="true" OnSelectedIndexChanged="ddl_INVID_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="label col-md-2" style="width: auto;">
                            Select Status :
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddl_Subject" runat="server" AutoPostBack="True" Style="text-align: center;"
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
    <%--<div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Reports
            </h3>
        </div>
        <div class="form-group has-warning">
            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
        </div>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <asp:GridView ID="gvreport" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    CssClass="table table-bordered table-striped" OnRowDataBound="gvreport_RowDataBound">
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
                        <asp:TemplateField HeaderText="Options">
                            <ItemTemplate>
                                <div class="col-md-12" style="display: inline-flex">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label4" runat="server" Visible="false" Text="Select Site Id:"></asp:Label>
                                        <asp:DropDownList ID="drp_InvID" runat="server" Visible="false" AutoPostBack="True"
                                            class="form-control drpControl required">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label ID="Label5" runat="server" Visible="false" Text="Select Subject :"></asp:Label>
                                        <asp:DropDownList ID="ddl_Subject" runat="server" Visible="false" AutoPostBack="True"
                                            class="form-control drpControl required">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Options">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnprint" runat="server" OnClientClick="Print(this);" Text="Print"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>--%>
</asp:Content>
