<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ISSUES.aspx.cs" Inherits="CTMS.ISSUES1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function Issuedetails(element) {
            var IssueID = $(element).prev().attr('commandargument');
            var test = "IssueDetails.aspx?IssueID=" + IssueID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=700,width=950";
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
                ISSUE LIST</h3>
        </div>
        <div class="row">
            <div class="lblError">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="label col-md-2">
            Select Project:
        </div>
        <div class="col-md-2" style="margin-left: -9%">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="Drp_Project" class="form-control" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="Drp_Project_SelectedIndexChanged">
                    </asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="label col-md-2">
            Select Site Id:
        </div>
        <div class="col-md-2" style="margin-left: -9%;">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="drp_InvID" runat="server" class="form-control" AutoPostBack="True"
                        OnSelectedIndexChanged="drp_InvID_SelectedIndexChanged">
                    </asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="label col-md-2">
            Status:
        </div>
        <div class="col-md-2" style="margin-left: -12%;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="Drp_Status" runat="server" class="form-control" AutoPostBack="True"
                        OnSelectedIndexChanged="Drp_Status_SelectedIndexChanged">
                    </asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <br />
    <div class="box">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grdISSUES" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                    Width="100%" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" OnPreRender="grdISSUES_PreRender"
                    OnRowCommand="grdISSUES_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="ID" ItemStyle-CssClass="txt_center width20px">
                            <ItemTemplate>
                                <asp:Label ID="ID" runat="server" Text='<%# Bind("ISSUES_ID") %>' CommandArgument='<%# Eval("ISSUES_ID") %>'
                                    CssClass="disp-noneimp" />
                                <asp:LinkButton ID="lnkID" runat="server" Text='<%# Bind("ISSUES_ID") %>' CommandArgument='<%# Eval("ISSUES_ID") %>'
                                    OnClientClick="return Issuedetails(this);"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle CssClass="txt_center width20px" HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SiteID" ItemStyle-CssClass="txt_center width20px">
                            <ItemTemplate>
                                <asp:Label ID="INVID" runat="server" Text='<%# Bind("INVID") %>' CssClass="txt_center" />
                            </ItemTemplate>
                            <ItemStyle CssClass="txt_center width20px" HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SUBJID" ItemStyle-CssClass="txt_center width20px">
                            <ItemTemplate>
                                <asp:Label ID="SUBJID" runat="server" Text='<%# Bind("SUBJID") %>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="txt_center width20px" HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Summary" ItemStyle-CssClass="width20px">
                            <ItemTemplate>
                                <asp:Label ID="Summary" runat="server" Text='<%# Bind("Summary") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="txt_center width20px">
                            <ItemTemplate>
                                <asp:Label ID="Status" runat="server" Text='<%# Bind("Status") %>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="txt_center width20px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department" ItemStyle-CssClass="txt_center width20px">
                            <ItemTemplate>
                                <asp:Label ID="Department" runat="server" Text='<%# Bind("Department") %>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="txt_center width20px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Opened Date" ItemStyle-CssClass="txt_center width20px">
                            <ItemTemplate>
                                <asp:Label ID="OpenedDate" runat="server" Text='<%# Bind("ISSUEDate") %>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="txt_center width50px" HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Due Date" ItemStyle-CssClass="txt_center width20px">
                            <ItemTemplate>
                                <asp:Label ID="DueDate" runat="server" Text='<%# Bind("DueDate") %>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="txt_center width50px" HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Resolution" ItemStyle-CssClass="txt_center width20px">
                            <ItemTemplate>
                                <asp:Label ID="Resolution" runat="server" Text='<%# Bind("ResolutionDate") %>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="txt_center width20px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Opened By" ItemStyle-CssClass="txt_center width20px">
                            <ItemTemplate>
                                <asp:Label ID="ISSUEby" runat="server" Text='<%# Bind("ISSUEby") %>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="txt_center width20px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="AssignedTo" ItemStyle-CssClass="txt_center width20px">
                            <ItemTemplate>
                                <asp:Label ID="AssignedTo" runat="server" Text='<%# Bind("AssignedTo") %>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="txt_center width20px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="1%" ItemStyle-Width="1%"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtndelete" runat="server" CommandArgument='<%# Bind("ISSUES_ID") %>'
                                    CommandName="Delete1" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </div>
</asp:Content>
