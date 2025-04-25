<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NSAE_REOPEN_REQUEST_LIST.aspx.cs" Inherits="CTMS.NSAE_REOPEN_REQUEST_LIST" %>

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
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Request List For Re-Open SAE</h3>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row" style="padding-top: 15px;">
            <div class="col-md-12">
                <div class="label col-md-2">
                    Site ID:
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="drpInvID" runat="server" AutoPostBack="True" CssClass="form-control required"
                        OnSelectedIndexChanged="drpInvID_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row" style="padding-top: 15px;">
            <div class="col-md-12">
                <div class="label col-md-2">
                    Subject ID:
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="drpSubID" runat="server" CssClass="form-control required" AutoPostBack="true"
                        OnSelectedIndexChanged="drpSubID_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div id="Div1" class="row" runat="server" style="padding-top: 15px;">
            <div class="col-md-12">
                <div class="label col-md-2">
                    SAE ID:
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="drpSAEID" runat="server" CssClass="form-control required" AutoPostBack="true"
                        OnSelectedIndexChanged="drpSAEID_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="box">
            <asp:GridView ID="grdSAE" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                AllowSorting="true" Width="100%" OnPreRender="grd_data_PreRender" OnRowCommand="grdSAE_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="ID" ItemStyle-CssClass="txt_center disp-none" HeaderStyle-CssClass="disp-none">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
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
                    <asp:TemplateField HeaderText="Raised Comment" ItemStyle-CssClass="txt_center" HeaderStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="RaisedComment" runat="server" Text='<%# Bind("RaisedComment") %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Raised By" ItemStyle-CssClass="txt_center" HeaderStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="RaisedBY" runat="server" Text='<%# Bind("RaisedBY") %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Raised Date-Time" ItemStyle-CssClass="txt_center"
                        HeaderStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="RaisedDateTime" runat="server" Text='<%# Bind("RaisedDateTime") %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="txt_center" HeaderStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("STATUS") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnApprove" runat="server" CommandName="SAEApprove" CommandArgument='<%# Bind("ID") %>'
                                Text="UnLock"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnReject" runat="server" CommandName="SAEReject" CommandArgument='<%# Bind("ID") %>'
                                Text="Reject"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
