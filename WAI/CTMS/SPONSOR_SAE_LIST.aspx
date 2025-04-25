<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SPONSOR_SAE_LIST.aspx.cs" Inherits="CTMS.SPONSOR_SAE_LIST" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                List of SAE's
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
                            <asp:GridView ID="grdSAELog" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                OnRowCommand="grdSAELog_RowCommand" Width="100%" OnPreRender="GridView1_PreRender">
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" ItemStyle-CssClass="txt_center disp-none" HeaderStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>' Font-Bold="true"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SAEID" ItemStyle-CssClass="txt_center" HeaderStyle-CssClass="txt_center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSAEID" runat="server" Text='<%# Bind("SAEID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SAE" ItemStyle-CssClass="txt_center" HeaderStyle-CssClass="txt_center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSAE" runat="server" Text='<%# Bind("SAE") %>' Font-Bold="true"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Event ID" ItemStyle-CssClass="txt_center">
                                        <ItemTemplate>
                                            <asp:Label ID="AESPID" runat="server" Text='<%# Bind("AESPID") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Event Term">
                                        <ItemTemplate>
                                            <asp:Label ID="AETERM" runat="server" Text='<%# Bind("AETERM") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="txt_center" HeaderStyle-CssClass="txt_center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("STATUS") %>' Font-Bold="true"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SUBJECT ID" ItemStyle-CssClass="txt_center" HeaderStyle-CssClass="txt_center">
                                        <ItemTemplate>
                                            <asp:Label ID="SUBJID" runat="server" Text='<%# Bind("SUBJID") %>' Font-Bold="true"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SAE LOGGING DATE" ItemStyle-CssClass="txt_center"
                                        HeaderStyle-CssClass="txt_center">
                                        <ItemTemplate>
                                            <asp:Label ID="SAE_LOGING_DT" runat="server" Text='<%# Bind("DTENTERED") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SAE LOGGING DATE" ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="SAE_AWERNESS_DT" runat="server" Text='<%# Bind("SAEAWERDAT") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="NEW_FOLLOWUP" runat="server" Text='<%# Bind("NEW_FOLLOWUP") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="INVID" runat="server" Text='<%# Bind("INVID") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="EditSAE" CommandArgument='<%# Bind("SAE") %>'><i class="fa fa-search"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
