<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PDList.aspx.cs" Inherits="CTMS.PDList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript">
        function PDPopup(element) {

            var UserType = $('#MainContent_hdnUserType').val();

            var PROTVOIL_ID = $(element).closest('tr').find('td:eq(1)').find('span').attr('commandargument');

            var ISSUEID = $(element).closest('tr').find('td:eq(2)').find('span').attr('commandargument');

            var test = "ProtDev.aspx?ISSUEID=" + ISSUEID + "&PROTVOIL_ID=" + PROTVOIL_ID + "&UserType=" + UserType;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=550,width=1100";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function NewPDPopup() {
            var test = "PDList_AddNew.aspx";
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=550,width=800";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function Print() {

            if ($("#<%=drp_INVID.ClientID%>").val() == "99") {
                $("#<%=drp_INVID.ClientID%>").addClass("brd-1px-redimp");
                return false;
            }

            if ($("#<%=drp_SUBJID.ClientID%>").val() == "99") {
                $("#<%=drp_SUBJID.ClientID%>").addClass("brd-1px-redimp");
                return false;
            }

            var INVID = $("#<%=drp_INVID.ClientID%>").val();
            var SUBJID = $("#<%=drp_SUBJID.ClientID%>").val();
            var test = "ReportPDList.aspx?ProjectId=" + ProjectId + "&INVID=" + INVID + "&SUBJID=" + SUBJID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=700,width=1200,resizable=no";
            window.open(test, '_blank', strWinProperty);
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">Protocol Deviation Logs
                        <asp:LinkButton ID="lbtnprint" runat="server" Text="Print" OnClientClick="return Print()"
                            CssClass="btn-sm disp-none">
      <span class="glyphicon glyphicon-print"></span>Print</asp:LinkButton>
                    </h3>
                </div>
                <asp:HiddenField runat="server" ID="hdnUserType" />
                <div class="box-body">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        </div>
                        <div style="display: inline-flex">
                            <div style="display: inline-flex">
                                <label class="label">
                                    Select Site
                                </label>
                                <div class="Control">
                                    <asp:DropDownList ID="drp_INVID" runat="server" CssClass="form-control drpControl"
                                        AutoPostBack="True" OnSelectedIndexChanged="drp_INVID_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div style="display: inline-flex">
                            <div style="display: inline-flex">
                                <label class="label">
                                    Select Subject
                                </label>
                                <div class="Control">
                                    <asp:DropDownList ID="drp_SUBJID" runat="server" CssClass="form-control drpControl"
                                        AutoPostBack="True" OnSelectedIndexChanged="drp_SUBJID_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div style="display: none;">
                            <div style="display: inline-flex">
                                <label class="label width60px">
                                    Select Duplicacy
                                </label>
                                <div class="Control">
                                    <asp:DropDownList ID="ddlDuplicacy" runat="server" CssClass="form-control drpControl"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlDuplicacy_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Text="--All--" Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="New" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Possibly Duplicate" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Duplicate" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div style="display: none;">
                            <asp:Button ID="btnAddNew" Visible="false" OnClientClick="return NewPDPopup();" runat="server" CssClass="btn btn-primary btn-sm"
                                Text="Add New" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <div id="Div1" class="dropdown" runat="server">
                                <a href="#" class="dropdown-toggle glyphicon glyphicon-download-alt" data-toggle="dropdown"
                                    style="color: #333333" title="Export"></a>
                                <ul class="dropdown-menu dropdown-menu-sm">
                                    <li>
                                        <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" ToolTip="Export to Excel"
                                            Text="Export to Excel" CssClass="dropdown-item" Style="color: #333333;">
                                        </asp:LinkButton></li>
                                    <hr style="margin: 5px;" />
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-warning">
                <div class="box-body">
                    <asp:Panel ID="Panel4" runat="server" Style="overflow: auto;">
                        <asp:GridView ID="grdProtVoil" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            Width="100%" CssClass="table table-bordered table-striped Datatable" OnPreRender="grdProtVoil_PreRender">
                            <Columns>
                                <asp:TemplateField HeaderText="Site Id" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblINVID" runat="server" Text='<%# Eval("INVID") %>' CommandArgument='<%#Eval("INVID") %>'
                                            CommandName="INVID"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deviation No." HeaderStyle-Width="10px" ItemStyle-Width="10px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPROTVOIL_ID" runat="server" Text='<%# Eval("PROTVOIL_ID") %>' CssClass="txt_center disp-none"
                                            CommandArgument='<%#Eval("PROTVOIL_ID") %>' CommandName="PROTVOIL_ID"></asp:Label>
                                        <asp:LinkButton ID="bnt_PROTVOIL_ID" runat="server" Text='<%# Eval("Refrence") %>'
                                            CssClass="txt_center" CommandArgument='<%#Eval("PROTVOIL_ID") %>' CommandName="PROTVOIL_ID"
                                            OnClientClick="return PDPopup(this)">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="10px" />
                                    <ItemStyle Width="10px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issue ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none"
                                    ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:Label ID="lblISSUES_ID" runat="server" Text='<%# Eval("ISSUES_ID") %>' CommandArgument='<%#Eval("ISSUES_ID") %>'
                                            CommandName="ISSUES_ID"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Subject Id" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSUBJID" runat="server" Text='<%# Eval("SUBJID") %>' CommandArgument='<%#Eval("SUBJID") %>'
                                            CommandName="SUBJID"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date of Occurance" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Dt_Otcome" runat="server" Text='<%# Eval("Dt_Otcome") %>' CommandArgument='<%#Eval("Dt_Otcome") %>'
                                            CommandName="Dt_Otcome"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepartment" runat="server" Text='<%# Eval("Department") %>' CommandArgument='<%#Eval("Department") %>'
                                            CommandName="Department"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MVID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMVID" runat="server" Text='<%# Eval("MVID") %>' CommandArgument='<%#Eval("MVID") %>'
                                            CommandName="MVID"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="disp-none" />
                                    <ItemStyle CssClass="disp-none" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' CommandArgument='<%#Eval("Description") %>'
                                            CommandName="Description"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Final Summary">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFinalSummary" runat="server" Text='<%# Eval("Rationalise") %>'
                                            CommandArgument='<%#Eval("Rationalise") %>' CommandName="Rationalise"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Process" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNature" runat="server" Text='<%# Eval("Nature") %>' CommandArgument='<%#Eval("Nature") %>'
                                            CommandName="MVID"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPDCODE1" runat="server" Text='<%# Eval("PDCODE1") %>' CommandArgument='<%#Eval("PDCODE1") %>'
                                            CommandName="PDCODE1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub-Category">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPDCODE2" runat="server" Text='<%# Eval("PDCODE2") %>' CommandArgument='<%#Eval("PDCODE2") %>'
                                            CommandName="PDCODE2"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="align-left">
                                    <HeaderTemplate>
                                        <label>Created details</label><br />
                                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Created By]</label><br />
                                        <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                        <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label ID="ENTEREDBYNAME" runat="server" Text='<%# Bind("ENTEREDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="ENTERED_CAL_DAT" runat="server" Text='<%# Bind("ENTERED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="ENTERED_CAL_TZDAT" runat="server" Text='<%# Bind("ENTERED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="align-left">
                                    <HeaderTemplate>
                                        <label>Updated details</label><br />
                                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Updated By]</label><br />
                                        <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                        <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label ID="UPDATEDBYNAME" runat="server" Text='<%# Bind("UPDATEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="UPDATED_CAL_DAT" runat="server" Text='<%# Bind("UPDATED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="UPDATED_CAL_TZDAT" runat="server" Text='<%# Bind("UPDATED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
