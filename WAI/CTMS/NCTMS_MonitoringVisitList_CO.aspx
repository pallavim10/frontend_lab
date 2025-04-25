<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NCTMS_MonitoringVisitList_CO.aspx.cs" Inherits="CTMS.NCTMS_MonitoringVisitList_CO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>

        function pageLoad() {

            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                stateSave: false,
                aaSorting: [[1, 'asc']],
                width: '100%'
            });

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header" style="display: inline-flex; width: 100%;">
            <h3 class="box-title" style="width: 100%; display: inline-flex; font-size: medium;">
                Generate Close-out Letter
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
                </div>
                <div id="Div1" runat="server" class="form-group" style="display: inline-flex">
                    <div class="form-group" style="display: inline-flex">
                        <label class="label" style="color: Maroon;">
                            Site Id:
                        </label>
                        <div class="Control">
                            <asp:DropDownList ID="ddlSiteId" runat="server" ForeColor="Blue" CssClass="form-control required"
                                OnSelectedIndexChanged="ddlSiteId_SelectedIndexChanged" AutoPostBack="True" Style="width: 100%">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="form-group" style="display: inline-flex">
                    <label class="label" style="color: Maroon;">
                        Select Visit Type:
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="drpVisitType" runat="server" ForeColor="Blue" AutoPostBack="True"
                            CssClass="form-control required" Style="width: 100%" OnSelectedIndexChanged="drpVisitType_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group" style="display: inline-flex">
                    <label class="label" style="color: Maroon;">
                        Select Visit Id:
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="drpVisitID" ForeColor="Blue" runat="server" AutoPostBack="True"
                            CssClass="form-control required" OnSelectedIndexChanged="drpVisitID_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="box">
                    <asp:GridView runat="server" ID="grdVisits" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                        OnRowCommand="grdVisits_RowCommand" AllowSorting="True" OnPreRender="grd_data_PreRender"
                        Width="100%">
                        <Columns>
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
                            <asp:TemplateField HeaderText="VISITID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                <ItemTemplate>
                                    <asp:Label ID="VISITID" runat="server" Text='<%# Bind("VISITID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Visit Id" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="SVID" runat="server" Text='<%# Bind("SVID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User Name">
                                <ItemTemplate>
                                    <asp:Label ID="User_Name" runat="server" Text='<%# Bind("User_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="STATUS" runat="server" Text='<%# Bind("STATUS") %>'></asp:Label>
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
                            <asp:TemplateField HeaderText="Create Close-Out Letter" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnCreateCOL" ToolTip="Create Close Out letter" runat="server" CommandName="CreateCO" CommandArgument='<%# Bind("SVID") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Print Close-Out Letter" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnPrintCO" runat="server" CommandName="PrintCO" CommandArgument='<%# Bind("SVID") %>'><i class="fa fa-print"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
