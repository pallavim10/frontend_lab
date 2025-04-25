<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Doc_Project.aspx.cs" Inherits="CTMS.Doc_Project" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Project Documents
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
                <table>
                    <tr>
                        <td class="label">
                            Select Document:
                        </td>
                        <td class="requiredSign">
                            <asp:Label ID="lbl_Section" runat="server" Text="*"></asp:Label>
                        </td>
                        <td class="Control">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlPlan" runat="server" Width="250px" class="form-control drpControl"
                                        ValidationGroup="View" AutoPostBack="true" OnSelectedIndexChanged="ddlPlan_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="style10">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <asp:GridView ID="gvSection" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    CssClass="table table-bordered table-striped" OnRowDataBound="gvSection_RowDataBound">
                    <Columns>
                        <asp:TemplateField Visible="false" HeaderText="ID">
                            <ItemTemplate>
                                <asp:Label ID="lbl_ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sections" ItemStyle-Width="100%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_fieldName" Width="100%" CssClass="label" ToolTip='<%# Bind("SectionName") %>'
                                    runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                            <HeaderTemplate>
                                <asp:Button ID="Btn_Add_Fun" runat="server" OnClick="Btn_Add_Fun_Click" Text="Add"
                                    CssClass="btn btn-primary btn-sm cls-btnSave" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                            <HeaderTemplate>
                                <asp:Button ID="Btn_Remove_Fun" runat="server" OnClick="Btn_Remove_Fun_Click" Text="Remove"
                                    CssClass="btn btn-primary btn-sm cls-btnSave" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="Chk_Sel_Remove_Fun" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
