<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="LabDataDashboard.aspx.cs" Inherits="CTMS.LabDataDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/jscript">
        function ShowListStatus(element, STATUS) {

            var INVID = $(element).closest('tr').find('td:eq(0)').text().trim();
            var CATEGORY = $("#MainContent_ddlCategory").val();

            var test = "MM_LabCatagoryData.aspx?INVID=" + INVID + "&STATUS=" + STATUS + "&CATEGORY=" + CATEGORY;
            var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
            window.open(test);
            return false;
        }

        function ShowGradeData(element, STATUS) {

            var INVID = $(element).closest('tr').find('td:eq(0)').text().trim();
            var Grade = $("#MainContent_ddlGrade").val();

            var test = "MM_GradeDashboardViewDetails.aspx?INVID=" + INVID + "&STATUS=" + STATUS + "&Grade=" + Grade;
            var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
            window.open(test);
            return false;
        }

        function pageLoad() {

            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: true
            });
        }

        function ShowList(ID, Name) {

            window.location.href = 'MM_LISTING_DATA.aspx?LISTID=' + ID + '&LISTNAME=' + Name + '';

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Category Dashboard</h3>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="box-body">
                    <div class="form-group has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="col-md-2" style="padding-right: 0px;">
                                <label>
                                    Categorys :</label>
                            </div>
                            <div class="col-md-6" style="padding-left: 0px;">
                                <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" CssClass="form-control"
                                    OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                    <asp:ListItem Value="Category 2" Text="Category 2"></asp:ListItem>
                                    <asp:ListItem Value="Category 3" Text="Category 3"></asp:ListItem>
                                    <asp:ListItem Value="Category 5" Text="Category 5"></asp:ListItem>
                                    <asp:ListItem Value="Category 6" Text="Category 6"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                    <asp:GridView ID="grdCATEGORYDASH" runat="server" AllowSorting="True" AutoGenerateColumns="false"
                        CssClass="table table-bordered Datatable table-striped notranslate" AlternatingRowStyle-CssClass="alt"
                        OnPreRender="grd_data_PreRender" PagerStyle-CssClass="pgr">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="Site ID" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSITEID" runat="server" Text='<%#Eval("INVID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="TOTAL" OnClientClick="return ShowListStatus(this, 'Total');"
                                        Style="color: Black;" runat="server" Text='<%# Eval("TOTAL") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Open" ItemStyle-CssClass="text-center fontReviews"
                                HeaderStyle-ForeColor="blue">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lblOPEN" OnClientClick="return ShowListStatus(this, 'Open');"
                                        Style="color: blue;" Text='<%# Eval("OPEN") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="For Peer-Review" ItemStyle-CssClass="text-center fontReviews"
                                HeaderStyle-ForeColor="green">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lblForPeerReview" OnClientClick="return ShowListStatus(this, 'For Peer-Review');"
                                        Style="color: green;" Text='<%# Eval("PR") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reviewed" ItemStyle-CssClass="text-center fontReviews"
                                HeaderStyle-ForeColor="green">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lblReviewed" OnClientClick="return ShowListStatus(this, 'Reviewed');"
                                        Style="color: green;" Text='<%# Eval("REVIEWED") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Query And Reviewed" ItemStyle-CssClass="text-center fontReviews"
                                HeaderStyle-ForeColor="green">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lblQueryAndReviewed" OnClientClick="return ShowListStatus(this, 'Query and Reviewed');"
                                        Style="color: green;" Text='<%# Eval("QNR") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reviewed with Peer View" ItemStyle-CssClass="text-center fontReviews"
                                HeaderStyle-ForeColor="green">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lblReviewedwithPeerView" OnClientClick="return ShowListStatus(this, 'Reviewed with Peer View');"
                                        Style="color: green;" Text='<%# Eval("PNR") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Queries" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lblOpenQuery" OnClientClick="return ShowListStatus(this, 'All Query');"
                                        Style="color: Black;" Text='<%# Eval("TOTAL_QUERY") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Open Queries" ItemStyle-CssClass="text-center" HeaderStyle-ForeColor="blue">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lblOpenQuery" OnClientClick="return ShowListStatus(this, 'Open Query');"
                                        Style="color: blue;" Text='<%# Eval("OPEN_QUERY") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Closed Queries" ItemStyle-CssClass="text-center" HeaderStyle-ForeColor="cornflowerblue">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lblCloseQuery" OnClientClick="return ShowListStatus(this, 'Close Query');"
                                        Style="color: cornflowerblue;" Text='<%# Eval("CLOSE_QUERY") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Issues" ItemStyle-CssClass="text-center" HeaderStyle-ForeColor="red">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lblIssue" OnClientClick="return ShowListStatus(this, 'Issue');"
                                        Style="color: red;" Text='<%# Eval("ISSUE") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br />
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Grade Dashboard</h3>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="col-md-2" style="padding-right: 0px;">
                                <label>
                                    Grades :</label>
                            </div>
                            <div class="col-md-6" style="padding-left: 0px;">
                                <asp:DropDownList ID="ddlGrade" runat="server" AutoPostBack="true" CssClass="form-control"
                                    OnSelectedIndexChanged="ddlGrade_SelectedIndexChanged">
                                    <asp:ListItem Value="Grade 1" Text="Grade 1"></asp:ListItem>
                                    <asp:ListItem Value="Grade 2" Text="Grade 2"></asp:ListItem>
                                    <asp:ListItem Value="Grade 3" Text="Grade 3"></asp:ListItem>
                                    <asp:ListItem Value="Grade 4" Text="Grade 4"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                    <asp:GridView ID="grdGRADEDASHBOARD" runat="server" AllowSorting="True" AutoGenerateColumns="false"
                        CssClass="table table-bordered Datatable table-striped notranslate" AlternatingRowStyle-CssClass="alt"
                        OnPreRender="grdGRADEDASHBOARD_PreRender" PagerStyle-CssClass="pgr">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="Site ID" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSITEID" runat="server" Text='<%#Eval("INVID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="TOTAL" OnClientClick="return ShowGradeData(this, 'Total');" Style="color: Black;"
                                        runat="server" Text='<%# Eval("TOTAL") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Open" ItemStyle-CssClass="text-center fontReviews"
                                HeaderStyle-ForeColor="blue">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lblOPEN" OnClientClick="return ShowGradeData(this, 'Open');"
                                        Style="color: blue;" Text='<%# Eval("OPEN") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="For Peer-Review" ItemStyle-CssClass="text-center fontReviews"
                                HeaderStyle-ForeColor="green">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lblForPeerReview" OnClientClick="return ShowGradeData(this, 'For Peer-Review');"
                                        Style="color: green;" Text='<%# Eval("PR") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reviewed" ItemStyle-CssClass="text-center fontReviews"
                                HeaderStyle-ForeColor="green">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lblReviewed" OnClientClick="return ShowGradeData(this, 'Reviewed');"
                                        Style="color: green;" Text='<%# Eval("REVIEWED") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Query And Reviewed" ItemStyle-CssClass="text-center fontReviews"
                                HeaderStyle-ForeColor="green">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lblQueryAndReviewed" OnClientClick="return ShowGradeData(this, 'Query and Reviewed');"
                                        Style="color: green;" Text='<%# Eval("QNR") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reviewed with Peer View" ItemStyle-CssClass="text-center fontReviews"
                                HeaderStyle-ForeColor="green">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lblReviewedwithPeerView" OnClientClick="return ShowGradeData(this, 'Reviewed with Peer View');"
                                        Style="color: green;" Text='<%# Eval("PNR") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Queries" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lblOpenQuery" OnClientClick="return ShowGradeData(this, 'All Query');"
                                        Style="color: Black;" Text='<%# Eval("TOTAL_QUERY") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Open Queries" ItemStyle-CssClass="text-center" HeaderStyle-ForeColor="blue">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lblOpenQuery" OnClientClick="return ShowGradeData(this, 'Open Query');"
                                        Style="color: blue;" Text='<%# Eval("OPEN_QUERY") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Closed Queries" ItemStyle-CssClass="text-center" HeaderStyle-ForeColor="cornflowerblue">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lblCloseQuery" OnClientClick="return ShowGradeData(this, 'Close Query');"
                                        Style="color: cornflowerblue;" Text='<%# Eval("CLOSE_QUERY") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Issues" ItemStyle-CssClass="text-center" HeaderStyle-ForeColor="red">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lblIssue" OnClientClick="return ShowGradeData(this, 'Issue');"
                                        Style="color: red;" Text='<%# Eval("ISSUE") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
