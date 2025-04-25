<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UMT_SETUP_EMAILS.aspx.cs" Inherits="CTMS.UMT_SETUP_EMAILS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">Define Emails </h3>
                </div>
                <div class="form-group">
                    <div class="form-group has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div3" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">
                                    Site Activation / Deactivation </h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvSite" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("To") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CC") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCC") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnSubmitSite" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitSite_Click"
                                                 />&nbsp;&nbsp;
                                            <asp:Button ID="btnCancelSite" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnCancelSite_Click"
                                                 />
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div1" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">
                                    User Activation / Deactivation </h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvUsers" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("To") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CC") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCC") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnUsrActiSubmit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnUsrActiSubmit_Click"
                                                 />&nbsp;&nbsp;
                                            <asp:Button ID="btnUsrActiCancel" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnUsrActiCancel_Click"
                                                />
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div2" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">
                                    User Unlock </h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvUserUnlock" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("To") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CC") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCC") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnUserUnlockSub" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnUserUnlockSub_Click"
                                                 />&nbsp;&nbsp;
                                            <asp:Button ID="btnUserUnlockCan" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnUserUnlockCan_Click"
                                                 />
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" id="div4" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">
                                    User Role Assigned / Update </h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvUserRole" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 99%; border-collapse: collapse;">
                                            <Columns>
                                                <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("To") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("CC") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("BCC") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnUserRoleSubmit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnUserRoleSubmit_Click"
                                                 />&nbsp;&nbsp;
                                            <asp:Button ID="btnUserRoleCancel" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnUserRoleCancel_Click"
                                                 />
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br /> 
       </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
