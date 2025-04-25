f<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="MM_UncodedEvents.aspx.cs" Inherits="Risk_Management.UncodedEvents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function UncodeToCode(element) {

            var Id = $(element).closest('tr').find('td:eq(0)').find('span').attr('commandargument');

            //  window.open("http://www.google.com", '', 'postwindow');


            var test = "MM_UpdateUncodedEvents.aspx?Id=" + Id;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500,width=1200";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function AdverseEventDetail(element) {

            var Subject = $(element).closest('tr').find('td:eq(1)').find('span').attr('commandargument');
            var EventCode = $(element).closest('tr').find('td:eq(2)').find('span').attr('commandargument');

            //  window.open("http://www.google.com", '', 'postwindow');


            var test = "MM_AdverseEventsDetails.aspx?Subject=" + Subject + "&EventCode=" + EventCode;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500,width=1200";
            window.open(test, '_blank', strWinProperty);
            return false;
        }
        function AllAdverseEventDetail(element) {
            var INVID = $(element).closest('tr').find('td:eq(0)').find('span').attr('commandargument');
            var Subject = $(element).closest('tr').find('td:eq(1)').find('span').attr('commandargument');

            var test = "MM_AE_SubjectList.aspx?Subject=" + Subject + "&INVID=" + INVID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=445,width=1300";
            window.open(test, '_blank', strWinProperty);
            return false;
        }
        function OpenIssue(element) {

            var Subject = $(element).closest('tr').find('td:eq(1)').find('span').attr('commandargument');
            var INVID = $(element).closest('tr').find('td:eq(0)').find('span').attr('commandargument');

            var Department = "Medical"
            var Source = "Adverse Events"
            var EventCode = $(element).closest('tr').find('td:eq(2)').find('span').attr('commandargument');
            var Department = "Medical"
            var Source = "Adverse Events"
            var Rule = "Uncoded Events"

            var test = "NewIssuePopup.aspx?Subject=" + Subject + "&INVID=" + INVID + "&Department=" + Department + "&Source=" + Source + "&EventCode=" + EventCode + "&Rule=" + Rule;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=340,width=550,resizable=no";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function NewQueryPopup(element) {
            var InvID = 0;
            var SUBJID = 0;
            var EventCode = 0;
            var EventTerm = 0;
            var Department = "Medical"
            var Source = "Adverse Events"
            var Rule = "Uncoded Events"

            InvID = $(element).closest('tr').find('td:eq(0)').find('span').attr('commandargument');
            SUBJID = $(element).closest('tr').find('td:eq(1)').find('span').attr('commandargument');
            EventCode = $(element).closest('tr').find('td:eq(2)').find('span').attr('commandargument');
            EventTerm = $(element).closest('tr').find('td:eq(3)').find('span').attr('commandargument');



            var test = "NewQueryPopup.aspx?Subject=" + SUBJID + "&INVID=" + InvID + "&Department=" + Department + "&Source=" + Source + "&EventCode=" + EventCode + "&EventTerm=" + EventTerm + "&Rule=" + Rule;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=250,width=500,resizable=no";
            window.open(test, '_blank', strWinProperty);
            return false;
        }
        function GetFilterPieChart() {
            var INVID = $("#<%=drpSite.ClientID%>").val();
            var SUBJID = $("#<%=drpSubject.ClientID%>").val();
            var RULE = "Uncoded Events"
            var RULE1 = "Rule3"

            var test = "MM_FilterPieChart.aspx?SUBJID=" + SUBJID + "&INVID=" + INVID + "&RULE=" + RULE + "&RULE1=" + RULE1;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=250,width=700,resizable=no";
            window.open(test, '_blank', strWinProperty);
            return false;
        }
        function QueryDetail(element) {
            var INVID = $(element).closest('tr').find('td:eq(0)').find('span').attr('commandargument');
            var Subject = $(element).closest('tr').find('td:eq(1)').find('span').attr('commandargument');
            var EventCode = $(element).closest('tr').find('td:eq(2)').find('span').attr('commandargument');
            var test = "MM_PopUpQueryList.aspx?Subject=" + Subject + "&INVID=" + INVID + "&EventCode=" + EventCode;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=445,width=1200";
            window.open(test, '_blank', strWinProperty);

            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="Upd_Pan_Sel_Subject" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">
                        Uncoded Adverse Events</h3>
                </div>
                <div class="box-body">
                    <div class="form-group has-warning" style="margin-left: 9px">
                        Reference -
                        <asp:Label ID="lblRefrence" runat="server" Text="Label"></asp:Label>
                    </div>
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                    <table>
                        <tr>
                            <td class="label">
                                Select Site
                            </td>
                            <td class="requiredSign">
                                <asp:Label ID="Label2" runat="server" Text="*"></asp:Label>
                            </td>
                            <td class="control">
                                <asp:DropDownList ID="drpSite" runat="server" CssClass="form-control drpControl required  width120px"
                                    AutoPostBack="True" OnSelectedIndexChanged="drpSite_SelectedIndexChanged">
                                </asp:DropDownList>
                                <td class="label">
                                    Select Subject
                                </td>
                                <td class="requiredSign">
                                    <asp:Label ID="Label1" runat="server" Text="*"></asp:Label>
                                </td>
                                <td class="control">
                                    <asp:DropDownList ID="drpSubject" runat="server" CssClass="form-control drpControl required width120px"
                                        AutoPostBack="True" OnSelectedIndexChanged="drpSubject_SelectedIndexChanged">
                                        <asp:ListItem Text="--All--" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    AE Type
                                </td>
                                <td class="requiredSign">
                                    <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                                </td>
                                <td class="control">
                                    <asp:DropDownList ID="drpAEType" runat="server" class="form-control drpControl required width120px"
                                        AutoPostBack="True" OnSelectedIndexChanged="drpAEType_SelectedIndexChanged">
                                        <asp:ListItem Text="--All--" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    Select Filters
                                </td>
                                <td class="requiredSign">
                                    <asp:Label ID="Label5" runat="server" Text="*"></asp:Label>
                                </td>
                                <td class="control">
                                    <asp:DropDownList ID="drpFilters" runat="server" CssClass="form-control
            drpControl required width120px" AutoPostBack="True" OnSelectedIndexChanged="drpFilters_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    &nbsp
                                </td>
                                <td>
                                    &nbsp
                                </td>
                                <td>
                                    &nbsp
                                </td>
                                <td>
                                    &nbsp
                                </td>
                                <td>
                                    <asp:Button ID="btnGetChart" runat="server" Text="Get Chart" OnClientClick="GetFilterPieChart()"
                                        CssClass="btn btn-primary btn-sm" />
                                </td>
                        </tr>
                        <tr>
                            <%--  <asp:CompareValidator ID="CompareValidator1" runat="server" 
                            ControlToValidate="drpSubject" ErrorMessage="Required" Font-Size="X-Small" 
                            ForeColor="#FF3300" Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>--%>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="box">
                <asp:GridView ID="grdUncodedAE" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    CssClass="table table-bordered table-striped Datatable" OnRowCommand="grdEventList_RowCommand"
                    OnPreRender="GridView_PreRender" OnRowDataBound="grdUncodedAE_RowDataBound">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Id" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:Label ID="lblid" CssClass="disp-none" runat="server" CommandArgument='<%#Eval("Id") %>'
                                    Text='<%# Eval("id") %>' CommandName="Id"></asp:Label>
                                <asp:LinkButton ID="lbtnId" runat="server" Text='<%#Eval("Id") %>' CommandArgument='<%#Eval("Id") %>'
                                    CommandName="Id" OnClientClick="return UncodeToCode(this);"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Site ID" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:Label ID="lblInvID" runat="server" CommandArgument='<%#Eval("InvID") %>' Text='<%# Eval("InvID") %>'
                                    CssClass="txt_center" CommandName="InvID"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subject ID" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:Label ID="SUBJID" runat="server" Text='<%# Eval("SUBJID") %>' CommandArgument='<%#Eval("SUBJID") %>'
                                    CssClass="disp-none" CommandName="Subject"></asp:Label>
                                <asp:LinkButton ID="lnk_SUBJID" runat="server" Text='<%# Eval("SUBJID") %>' OnClientClick="return AllAdverseEventDetail(this)"
                                    CommandArgument='<%#Eval("SUBJID") %>' CommandName="Subject"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Code" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:Label ID="Code" runat="server" Text='<%# Eval("AESPID") %>' CssClass="disp-noneimp"
                                    CommandArgument='<%#Eval("AESPID") %>' Width="40px" CommandName="Code"></asp:Label>
                                <asp:LinkButton ID="lblCode" runat="server" Text='<%# Eval("AESPID") %>' CommandArgument='<%#Eval("AESPID") %>'
                                    CssClass="txt_center" CommandName="Code" OnClientClick="return AdverseEventDetail(this)"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Event Term">
                            <ItemTemplate>
                                <asp:Label ID="lblETerm" runat="server" Text='<%# Eval("AETERM") %>' Width="180px"
                                    CommandArgument='<%# Eval("AETERM") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Preferred Term">
                            <ItemTemplate>
                                <asp:Label ID="lblPTerm" runat="server" Text='<%# Eval("AEPT") %>' Width="120px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Preferred Code">
                            <ItemTemplate>
                                <asp:Label ID="lblAEPTC" runat="server" Text='<%# Eval("AEPTC") %>' Width="80px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:Label ID="QR" runat="server" Text='<%# Eval("Status") %>' Width="30px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Query" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkQuery" runat="server" Text="Query" CommandName="Query"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reviewed" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkIgnore" runat="server" Text="Reviewed" CommandName="Reviewed"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MQ" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkMQ" runat="server" Text="MQ" CommandName="MQ" OnClientClick="return NewQueryPopup(this);"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Issue" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkIssue" runat="server" Text="Issue" CommandName="Issue" OnClientClick="return OpenIssue(this); "></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Query Count" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:LinkButton ID="linkQueryCount" runat="server" Text='<%# Eval("QueryCount") %>'
                                    Style="color: Black" CommandArgument='<%#Eval("QueryCount") %>' CommandName="QueryCount"
                                    OnClientClick="return QueryDetail(this)"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Review Status" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:Label ID="ReviewedStatus" runat="server" Text='<%# Eval("ReviewedStatus") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
