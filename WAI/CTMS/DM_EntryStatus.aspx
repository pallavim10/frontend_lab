<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DM_EntryStatus.aspx.cs" Inherits="CTMS.DM_EntryStatus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>





    <div class="box">
      <div class="lblError">
            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
        </div>
        <div runat="server" id="Div1" class="form-group" style="display: inline-flex">
            <div class="form-group" style="display: inline-flex">
                <label class="label">
                    Select Entry Status:
                </label>
                <div class="Control">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="drpIncompStatus" runat="server" AutoPostBack="True" CssClass="form-control "
                                OnSelectedIndexChanged="drpIncompStatus_SelectedIndexChanged">
                                <asp:ListItem Value="0">---Select---</asp:ListItem>
                               <%-- <asp:ListItem Value="MISSING_VIS">Missing Visit</asp:ListItem>--%>
                                <asp:ListItem Value="INCOMP_VIS">Incomplet Visit</asp:ListItem>
                                <asp:ListItem Value="INCOMP_MOD">Incomplet Module</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div runat="server" id="DivINV" class="form-group" style="display: inline-flex">
            <div class="form-group" style="display: inline-flex">
                <label class="label">
                    Site ID:
                </label>
                <div class="Control">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="drpInvID" runat="server" OnSelectedIndexChanged="drpInvID_SelectedIndexChanged"
                                AutoPostBack="True" CssClass="form-control ">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="form-group" style="display: inline-flex">
            <label class="label">
                Subject ID:
            </label>
            <div class="Control">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="drpSubID" runat="server" CssClass="form-control" 
                            OnSelectedIndexChanged="drpSubID_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grdEntryStatus" runat="server" CssClass="table table-bordered table-striped"
                    OnRowDataBound="grdEntryStatus_RowDataBound">
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
