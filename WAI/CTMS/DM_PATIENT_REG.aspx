<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DM_PATIENT_REG.aspx.cs" Inherits="CTMS.DM_PATIENT_REG" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Patient Registration
            </h3>
        </div>
        <asp:UpdatePanel ID="upl" runat="server">
            <ContentTemplate>
                <div class="box-body">
                    <div class="row">
                        <div class="lblError">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                    <table>
                        <tr>
                            <td class="label">
                                Site ID:
                            </td>
                            <td class="requiredSign">
                                <asp:Label ID="Label2" runat="server" Text="*"></asp:Label>
                            </td>
                            <td class="Control">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="drpInvID" runat="server" OnSelectedIndexChanged="drpInvID_SelectedIndexChanged"
                                            AutoPostBack="True" CssClass="form-control required">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="style10">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Select Indication:
                            </td>
                            <td class="requiredSign">
                                <asp:Label ID="Label1" runat="server" Text="*"></asp:Label>
                            </td>
                            <td class="Control">
                                <asp:UpdatePanel ID="Upd_Pan_Sel_UG" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList runat="server" ID="drpIndication" CssClass="form-control required"
                                            AutoPostBack="True" OnSelectedIndexChanged="drpIndication_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="style10">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Subject ID:
                            </td>
                            <td class="requiredSign">
                                <asp:Label ID="Label3" runat="server" Text="*"></asp:Label>
                            </td>
                            <td class="Control">
                                <asp:UpdatePanel ID="Upd_Pan_Sel_User" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="drpSubID" runat="server" CssClass="form-control required" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="style10">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Initials:
                            </td>
                            <td class="requiredSign">
                                <asp:Label ID="Label4" runat="server" Text="*"></asp:Label>
                            </td>
                            <td class="Control">
                                <asp:TextBox ID="txtInitials" runat="server" CssClass="form-control required"></asp:TextBox>
                            </td>
                            <td class="style10">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Date Of Birth:
                            </td>
                            <td class="requiredSign">
                                <asp:Label ID="Label5" runat="server" Text="*"></asp:Label>
                            </td>
                            <td class="Control">
                                <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control txtDate required"></asp:TextBox>
                            </td>
                            <td class="style10">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                GENDER:
                            </td>
                            <td class="requiredSign">
                                <asp:Label ID="Label6" runat="server" Text="*"></asp:Label>
                            </td>
                            <td class="Control">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList runat="server" ID="drp_GENDER" CssClass="form-control required"
                                            AutoPostBack="True">
                                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="MALE" Value="MALE"></asp:ListItem>
                                            <asp:ListItem Text="FEMALE" Value="FEMALE"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="style10">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                            </td>
                            <td class="requiredSign">
                            </td>
                            <td class="style1">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                    CssClass="btn btn-primary btn-sm cls-btnSave" />
                                <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-primary"
                                    OnClick="btncancel_Click" />
                            </td>
                            <td class="style1">
                            </td>
                            <td class="style6">
                            </td>
                        </tr>
                        <tr>
                        <td></td>
                        </tr>
                         <tr>
                        <td></td>
                        </tr>
                         <tr>
                        <td></td>
                        </tr>
                    </table>
                    <div class="box">
                        <asp:GridView ID="Grd_PatientReg" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                            OnRowCommand="Grd_PatientReg_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="ID" ItemStyle-CssClass="txt_center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="INDICATION" ItemStyle-CssClass="txt_center">
                                    <ItemTemplate>
                                        <asp:Label ID="txtindicaton" runat="server" Text='<%# Bind("indication") %>' Width="59px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SUBJECT" ItemStyle-CssClass="txt_center">
                                    <ItemTemplate>
                                        <asp:Label ID="txtSUBJID" runat="server" Text='<%# Bind("SUBJID") %>' Enabled="false"
                                            Width="59px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="INITIAL" ItemStyle-CssClass="txt_center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInitial" runat="server" Text='<%# Bind("DM_INTIALS") %>' Enabled="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DOB" ItemStyle-CssClass="txt_center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDOB" runat="server" Text='<%# Bind("DM_DOBDAT") %>' Enabled="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GENDER" ItemStyle-CssClass="txt_center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgender" runat="server" Text='<%# Bind("DM_GENDER") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-CssClass="txt_center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnedit" runat="server" CommandName="reg_EDIT" CommandArgument='<%# Bind("ID") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDELETE" runat="server" CommandName="reg_DELETE" CommandArgument='<%# Bind("ID") %>'><i class="fa fa-trash-o"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
