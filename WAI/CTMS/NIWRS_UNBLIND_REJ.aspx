<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NIWRS_UNBLIND_REJ.aspx.cs" Inherits="CTMS.NIWRS_UNBLIND_REJ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Rejected For Unblinding</h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
                <div style="display: inline-flex;">
                    <div runat="server" id="divDDLS">
                        <div class="form-group" runat="server" id="divSite" style="display: inline-flex">
                            <label class="label">
                                Select Site:
                            </label>
                            <div class="Control">
                                <asp:DropDownList ID="drpSite" AutoPostBack="true" runat="server" CssClass="form-control required"
                                    OnSelectedIndexChanged="drpSite_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group" runat="server" id="divSubSite" style="display: none">
                            <label class="label">
                                Select Sub-Site:
                            </label>
                            <div class="Control">
                                <asp:DropDownList ID="drpSubSite" runat="server" AutoPostBack="true" CssClass="form-control "
                                    OnSelectedIndexChanged="drpSubSite_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
            </div>
        </div>
    </div>
    <div class="box">
        <asp:GridView ID="grdUNBLINDREJ" runat="server" AutoGenerateColumns="false" AllowSorting="True"
            CssClass="table table-bordered table-striped">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="Site ID" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="txt_Site_ID" runat="server" Text='<%# Bind("SITEID") %>' Enabled="False"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sub Site ID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="Sub_SiteID" runat="server" Text='<%# Bind("SUBSITEID") %>' Enabled="False"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Subject ID" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="txt_Subject_ID" runat="server" Text='<%# Bind("SUBJID") %>' Enabled="False"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Randomization Number" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="txt_Rand_No" runat="server" ReadOnly="True" Text='<%# Bind("RAND_NUM") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reason for Unblinding" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="UNBLIND_REQ_REASN" runat="server" ReadOnly="True" Text='<%# Bind("UNBLIND_REQ_REASN") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <HeaderTemplate>
                        <label>Request Details</label><br />
                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Requested By]</label><br />
                        <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                        <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div>
                            <div>
                                <asp:Label ID="UNLIND_REQ_USERNAME" runat="server" Text='<%# Bind("UNLIND_REQ_USERNAME") %>' ForeColor="Blue"></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="UNLIND_REQ_CAL_DAT" runat="server" Text='<%# Bind("UNLIND_REQ_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="UNLIND_REQ_CAL_TZDAT" runat="server" Text='<%# Eval("UNLIND_REQ_CAL_TZDAT") +" "+ Eval("UNLIND_REQ_TZVAL") %>' ForeColor="Red"></asp:Label>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <HeaderTemplate>
                        <label>Disapproved Details</label><br />
                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Disapproved By]</label><br />
                        <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                        <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div>
                            <div>
                                <asp:Label ID="UNLIND_REQ_APPREJ_USERNAME" runat="server" Text='<%# Bind("UNLIND_REQ_APPREJ_USERNAME") %>' ForeColor="Blue"></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="UNLIND_REQ_APPREJ_CAL_DAT" runat="server" Text='<%# Bind("UNLIND_REQ_APPREJ_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="UNLIND_REQ_APPREJ_CAL_TZDAT" runat="server" Text='<%# Eval("UNLIND_REQ_APPREJ_CAL_TZDAT") +" "+ Eval("UNLIND_REQ_APPREJ_TZVAL") %>' ForeColor="Red"></asp:Label>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Comment by Disapprover" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="UNLIND_REQ_APPREJ_REASN" runat="server" ReadOnly="True" Text='<%# Bind("UNLIND_REQ_APPREJ_REASN") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
