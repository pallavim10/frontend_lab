<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NCTMS_VISITS.aspx.cs" Inherits="CTMS.NCTMS_VISITS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        function pageLoad() {

            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                stateSave: false,
                aaSorting: [[1, 'asc']]
            });

        }

        function GoToVisitReportEntry(element) {

            var SITEID = $(element).closest('tr').find('td:eq(1)').find('span').html();
            var VISITTYPE = $(element).closest('tr').find('td:eq(2)').find('span').html();
            var VISITTYPEID = $(element).closest('tr').find('td:eq(3)').find('span').html();
            var VISITID = $(element).text();

            var test = "NCTMS_SITE_VISIT_OPEN_CRF.aspx?CTMS_CRF_SITEID=" + SITEID + "&CTMS_CRF_VISITTYPE=" + VISITTYPE + "&CTMS_CRF_VISITID=" + VISITID + "&CTMS_VISITTYPEID=" + VISITTYPEID;

            window.location.href = test;
            return false;

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-primary">
        <div class="box-header">
            <h3 class="box-title">
                Manage Visits
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Select Site Id :&nbsp;
                            <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlSiteId" runat="server" CssClass="form-control required width200px"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlSiteId_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="label col-md-2">
                            Select Visit Type: &nbsp;
                            <asp:Label ID="Label7" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlVisitType" runat="server" CssClass="form-control required width200px"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlVisitType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Visit Id :&nbsp;
                            <asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtVisitNomenclature" Enabled="false" runat="server" CssClass="form-control required width200px"></asp:TextBox>
                        </div>
                        <div class="label col-md-2">
                            Select Employee :&nbsp;
                            <asp:Label ID="Label3" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control required width200px">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Enter Start Date :&nbsp;
                            <asp:Label ID="Label9" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control required txtDate width200px"></asp:TextBox>
                        </div>
                        <div class="label col-md-2">
                            Enter End Date :&nbsp;
                            <asp:Label ID="Label12" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control required txtDate width200px"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div style="margin-left: 25%;">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm cls-btnSave"
                            OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary btn-sm cls-btnSave"
                            Visible="false" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm"
                            OnClick="btnCancel_Click"></asp:Button>
                    </div>
                </div>
                <br />
            </div>
            <asp:GridView runat="server" ID="grdVisits" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                Width="100%" OnRowCommand="grdVisits_RowCommand" AllowSorting="True" OnPreRender="gvEmp_PreRender"
                OnRowDataBound="grdVisits_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                        HeaderText="ID">
                        <ItemTemplate>
                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Site Id" HeaderStyle-CssClass="txtCenter" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="SITEID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Visit Type">
                        <ItemTemplate>
                            <asp:Label ID="VISIT_NAME" runat="server" Text='<%# Bind("VISIT_NAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="VisitTypeId" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                        <ItemTemplate>
                            <asp:Label ID="VISIT_TYPE_ID" runat="server" Text='<%# Bind("VISIT_TYPE_ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Visit Initial" HeaderStyle-CssClass="txtCenter" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="VISIT_INITIAL" runat="server" Text='<%# Bind("VISIT_INITIAL") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Visit Id" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblVISIT_NOM" runat="server" Text='<%# Bind("VISIT_NOM") %>' CssClass="disp-none"></asp:Label>
                            <asp:LinkButton ID="lbtnVISIT_NOM" runat="server" Text='<%# Bind("VISIT_NOM") %>'
                                OnClientClick="return GoToVisitReportEntry(this);"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Current Status">
                        <ItemTemplate>
                            <asp:Label ID="SUB_STATUS" runat="server" Text='<%# Bind("SUB_STATUS") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="User Name">
                        <ItemTemplate>
                            <asp:Label ID="User_Name" runat="server" Text='<%# Bind("User_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Start Date" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="STARTDT" runat="server" Text='<%# Bind("STARTDT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="End Date" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="ENDDT" runat="server" Text='<%# Bind("ENDDT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none"
                        ItemStyle-CssClass="disp-none">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnUpdateVisit" runat="server" CommandArgument='<%# Bind("ID") %>'
                                CommandName="EDITVisit" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;&nbsp;
                            <asp:LinkButton ID="lbtnDeleteVisit" runat="server" CommandArgument='<%# Bind("ID") %>'
                                CommandName="DeleteVisit" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
