<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ActiveSite.aspx.cs" Inherits="CTMS.ActiveSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Activate Site</h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
                <table class="style5">
                    <tr>
                        <td class="label">
                            Select Site ID:
                        </td>
                        <td class="style9">
                            <asp:Label ID="Lbl_Site_ID" runat="server" Font-Size="Small" ForeColor="#FF3300"
                                Text="*"></asp:Label>
                        </td>
                        <td class="style10">
                            <asp:UpdatePanel ID="Up_Sel_Site_ID" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="Drp_Site_ID" runat="server" Style="margin-top: 4px" CssClass="form-control width150px">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="style11">
                            <asp:CompareValidator ID="Comp_Val_Sel_Site_ID" runat="server" ControlToValidate="Drp_Site_ID"
                                ErrorMessage="Required" Font-Size="X-Small" ForeColor="#FF3300" Operator="NotEqual"
                                ValueToCompare="0"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:Button ID="Btn_SendMail" runat="server" Style="margin-left: 5px" CssClass="btn btn-primary btn-sm"
                                Text="Activate" Visible="true" OnClick="Btn_SendMail_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
