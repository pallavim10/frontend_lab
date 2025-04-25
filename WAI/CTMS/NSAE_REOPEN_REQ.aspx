<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NSAE_REOPEN_REQ.aspx.cs" Inherits="CTMS.NSAE_REOPEN_REQ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">

        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                stateSave: true
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">List Of Requests For Re-Open
            </h3>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="box-body">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
                        </div>
                        <div runat="server" id="DivINV" class="form-group" style="display: inline-flex">
                            <div class="form-group" style="display: inline-flex">
                                <label class="label">
                                    Site ID:
                                </label>
                                <div class="Control">
                                    <asp:DropDownList ID="drpInvID" runat="server" AutoPostBack="True" CssClass="form-control "
                                        OnSelectedIndexChanged="drpInvID_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                Subject ID:
                            </label>
                            <div class="Control">
                                <asp:DropDownList ID="drpSubID" runat="server" CssClass="form-control required" AutoPostBack="True"
                                    OnSelectedIndexChanged="drpSubID_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="box">
                            <div style="width: 100%; overflow: auto;">
                                <asp:GridView ID="grdSAELog" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                    OnRowCommand="grdSAELog_RowCommand" AllowSorting="true" Width="100%" OnPreRender="grd_data_PreRender">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="EditSAE" CommandArgument='<%# Bind("SAE") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="INVID" ItemStyle-CssClass="txt_center " HeaderStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                <asp:Label ID="INVID" runat="server" Text='<%# Bind("INVID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Subject Id" ItemStyle-CssClass="txt_center" HeaderStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                <asp:Label ID="SUBJID" runat="server" Text='<%# Bind("SUBJID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SAEID" ItemStyle-CssClass="txt_center" HeaderStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSAEID" runat="server" Text='<%# Bind("SAEID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SAE" ItemStyle-CssClass="txt_center" HeaderStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSAE" runat="server" Text='<%# Bind("SAE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Event ID" ItemStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                <asp:Label ID="DM_SPID" runat="server" Text='<%# Bind("DM_SPID") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Event Term">
                                            <ItemTemplate>
                                                <asp:Label ID="DM_TERM" runat="server" Text='<%# Bind("DM_TERM") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="align-left">
                                            <HeaderTemplate>
                                                <label>SAE Logging</label><br />
                                                <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[SAE Logging By]</label><br />
                                                <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div runat="server" id="divSAELogging">
                                                    <div>
                                                        <asp:Label ID="ENTEREDBYNAME" runat="server" Enabled="false" Font-Bold="true" Text='<%# Bind("ENTEREDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="ENTERED_CAL_DAT" runat="server" Text='<%# Bind("ENTERED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="ENTERED_CAL_TZDAT" runat="server" Text='<%# Bind("ENTERED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="align-left">
                                            <HeaderTemplate>
                                                <label>Last Data Modified</label><br />
                                                <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Last Data Modified By]</label><br />
                                                <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div runat="server">
                                                    <div>
                                                        <asp:Label ID="LAST_AT_ENTEREDBYNAME" runat="server" Enabled="false" Font-Bold="true" Text='<%# Bind("LAST_AT_ENTEREDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="LAST_AT_ENTERED_CAL_DAT" runat="server" Text='<%# Bind("LAST_AT_ENTERED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="LAST_AT_ENTERED_CAL_TZDAT" runat="server" Text='<%# Bind("LAST_AT_ENTERED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="align-left">
                                            <HeaderTemplate>
                                                <label>Last Medical Reviewed</label><br />
                                                <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Last Medical Reviewed By]</label><br />
                                                <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div runat="server">
                                                    <div>
                                                        <asp:Label ID="LAST_MR_ENTEREDBYNAME" runat="server" Enabled="false" Font-Bold="true" Text='<%# Bind("LAST_MR_ENTEREDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="LAST_MR_ENTERED_CAL_DAT" runat="server" Text='<%# Bind("LAST_MR_ENTERED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="LAST_MR_ENTERED_CAL_TZDAT" runat="server" Text='<%# Bind("LAST_MR_ENTERED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="align-left">
                                            <HeaderTemplate>
                                                <label>Status</label><br />
                                                <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Closed By]</label><br />
                                                <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div runat="server">
                                                    <div>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("STATUS") %>'></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="CLOSEDBYNAME" runat="server" Enabled="false" Font-Bold="true" Text='<%# Bind("CLOSEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="CLOSED_CAL_DAT" runat="server" Text='<%# Bind("CLOSED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="CLOSED_CAL_TZDAT" runat="server" Text='<%# Bind("CLOSED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="align-left">
                                            <HeaderTemplate>
                                                <label>Re-Open Request</label><br />
                                                <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Request By]</label><br />
                                                <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div runat="server">
                                                    <div>
                                                        <asp:Label ID="REQ_BYNAME" runat="server" Enabled="false" Font-Bold="true" Text='<%# Bind("REQ_BYNAME") %>' ForeColor="Blue"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="REQ_CAL_DAT" runat="server" Text='<%# Bind("REQ_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="REQ_CAL_TZDAT" runat="server" Text='<%# Bind("REQ_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
