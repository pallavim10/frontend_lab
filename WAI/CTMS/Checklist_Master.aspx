<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" ValidateRequest="false"
    CodeBehind="Checklist_Master.aspx.cs" Inherits="CTMS.Checklist_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Checklist Master
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
                            Select Section:
                        </td>
                        <td class="requiredSign">
                            <asp:Label ID="lbl_Section" runat="server" Text="*"></asp:Label>
                        </td>
                        <td class="Control">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="Drp_Section" runat="server" Width="250px" class="form-control drpControl"
                                        ValidationGroup="View" AutoPostBack="true" OnSelectedIndexChanged="Drp_Section_SelectedIndexChanged">
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
                    <tr>
                        <td class="label">
                            Select Sub-Section:
                        </td>
                        <td class="requiredSign">
                            <asp:Label ID="lbl_SubSection" runat="server" Text="*"></asp:Label>
                        </td>
                        <td class="Control">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="Drp_SubSection" runat="server" Width="250px" class="form-control drpControl"
                                        ValidationGroup="View" AutoPostBack="true" OnSelectedIndexChanged="Drp_SubSection_SelectedIndexChanged">
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
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:GridView ID="gvChecklists" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    CssClass="table table-bordered table-striped" 
                    onrowdatabound="gvChecklists_RowDataBound" >
                    <Columns>
                        <asp:TemplateField Visible="false" HeaderText="ID">
                            <ItemTemplate>
                                <asp:Label ID="lbl_ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Checklists" ItemStyle-Width="100%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_fieldName" Width="100%" ToolTip='<%# Bind("FIELDNAME") %>' CssClass="label"
                                    runat="server" Text='<%# Bind("FIELDNAME") %>'></asp:Label>
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
    <div style="display: none;" class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                &nbsp;<asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:CheckBox ID="chk_moreChecklist" AutoPostBack="true" Text="Add more checklists..."
                            runat="server" OnCheckedChanged="chk_moreChecklist_CheckedChanged" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </h3>
        </div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div runat="server" id="divMoreChecks" visible="false" class="box-body">
                    <div class="form-group">
                        <table>
                            <tr>
                                <td class="label" style="width: 150px;">
                                    Select Section:
                                </td>
                                <td class="requiredSign">
                                    <asp:Label ID="Label1" runat="server" Text="*"></asp:Label>
                                </td>
                                <td class="Control">
                                    <asp:DropDownList ID="ddlSection" runat="server" Width="250px" class="form-control drpControl required"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged"
                                        ValidationGroup="addMore">
                                    </asp:DropDownList>
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
                            <tr>
                                <td class="label" style="width: 150px;">
                                    Select Sub-Section:
                                </td>
                                <td class="requiredSign">
                                    <asp:Label ID="Label2" runat="server" Text="*"></asp:Label>
                                </td>
                                <td class="Control">
                                    <asp:DropDownList ID="ddlSubSection" runat="server" Width="250px" ValidationGroup="addMore"
                                        class="form-control drpControl required">
                                    </asp:DropDownList>
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
                            <tr>
                                <td class="label" style="width: 150px;">
                                    Enter Checklist:
                                </td>
                                <td class="requiredSign">
                                    <asp:Label ID="Label3" runat="server" Text="*"></asp:Label>
                                </td>
                                <td class="Control">
                                    <asp:TextBox TextMode="MultiLine" Rows="5" runat="server" CssClass="form-control required"
                                        ValidationGroup="addMore" ID="txtChkList" Width="250px"></asp:TextBox>
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
                            <tr>
                                <td class="label" style="width: 150px;">
                                    Select Control Type:
                                </td>
                                <td class="requiredSign">
                                    <asp:Label ID="Label4" runat="server" Text="*"></asp:Label>
                                </td>
                                <td class="Control">
                                    <asp:DropDownList ID="ddlChkList" runat="server" Width="250px" class="form-control drpControl required"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlChkList_SelectedIndexChanged"
                                        ValidationGroup="addMore">
                                        <asp:ListItem Text="-Select-" Value="0">
                                        </asp:ListItem>
                                        <asp:ListItem Text="CHECKBOX" Value="CHECKBOX">
                                        </asp:ListItem>
                                        <asp:ListItem Text="DROPDOWN" Value="DROPDOWN">
                                        </asp:ListItem>
                                        <asp:ListItem Text="TEXTBOX" Value="TEXTBOX">
                                        </asp:ListItem>
                                    </asp:DropDownList>
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
                            <tr runat="server" visible="false" id="tr_datetype">
                                <td class="label" style="width: 150px;">
                                    Is this control is Date type?
                                </td>
                                <td class="requiredSign">
                                    <asp:Label ID="Label5" runat="server" Text="*"></asp:Label>
                                </td>
                                <td class="Control">
                                    <asp:CheckBox ToolTip="Select if 'YES'" runat="server" ID="chkDateType" />
                                </td>
                                <td class="style10">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr runat="server" visible="false" id="tr_datetype_BR">
                                <td colspan="5">
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="label" style="width: 150px;">
                                    Enter Variable Name:
                                </td>
                                <td class="requiredSign">
                                    <asp:Label ID="Label6" runat="server" Text="*"></asp:Label>
                                </td>
                                <td class="Control">
                                    <asp:TextBox runat="server" CssClass="form-control required" ValidationGroup="addMore"
                                        ID="txtVariable" Width="250px"></asp:TextBox>
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
                            <tr>
                                <td class="label" style="width: 150px;">
                                    Editable Sub-Section?
                                </td>
                                <td class="requiredSign">
                                    <asp:Label ID="Label7" runat="server" Text="*"></asp:Label>
                                </td>
                                <td class="Control">
                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkSubSection" />
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
                            <tr>
                                <td class="label">
                                </td>
                                <td class="requiredSign">
                                </td>
                                <td class="Control">
                                    <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-primary btn-sm cls-btnSave"
                                        ValidationGroup="addMore" Text="Submit" OnClick="btnSubmit_Click" />
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
