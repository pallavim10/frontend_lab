<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="IWRS_Online_KitExpires_IN.aspx.cs" Inherits="CTMS.IWRS_Online_KitExpires_IN" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Kit Expires In
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
                <table class="style5">
                    <tr>
                        <td class="label">
                            Select Project:
                        </td>
                        <td class="style9">
                            <asp:Label ID="Lbl_Project_Name" runat="server" Font-Size="Small" ForeColor="#FF3300"
                                Text="*"></asp:Label>
                        </td>
                        <td class="style10">
                            <asp:UpdatePanel ID="Up_Sel_Proj" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="Drp_Project_Name" runat="server" CssClass="form-control" AutoPostBack="True"
                                        OnSelectedIndexChanged="Drp_Project_Name_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="style11">
                            <asp:CompareValidator ID="Comp_Val_Sel_Project" runat="server" ControlToValidate="Drp_Project_Name"
                                ErrorMessage="Required" Font-Size="X-Small" ForeColor="#FF3300" Operator="NotEqual"
                                ValueToCompare="0"></asp:CompareValidator>
                        </td>
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
                                    <asp:DropDownList ID="Drp_Site_ID" runat="server" CssClass="form-control" AutoPostBack="true">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="style11">
                            <asp:CompareValidator ID="Comp_Val_Sel_Site_ID" runat="server" ControlToValidate="Drp_Site_ID"
                                ErrorMessage="Required" Font-Size="X-Small" ForeColor="#FF3300" Operator="NotEqual"
                                ValueToCompare="0"></asp:CompareValidator>
                        </td>
                        <td class="label">
                            Kit Expires In:
                        </td>
                        <td class="style9">
                            <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </td>
                        <td class="style10">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="Drp_Days" runat="server" CssClass="form-control" 
                                        AutoPostBack="true" onselectedindexchanged="Drp_Days_SelectedIndexChanged">
                                    <asp:ListItem Value="5">5 Days</asp:ListItem>
                                     <asp:ListItem Value="10">10 Days</asp:ListItem>
                                      <asp:ListItem Value="15">15 Days</asp:ListItem>
                                       <asp:ListItem Value="20">20 Days</asp:ListItem>
                                         <asp:ListItem Value="25">25 Days</asp:ListItem>
                                       <asp:ListItem Value="30">30 Days</asp:ListItem>
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="style11">
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="Drp_Site_ID"
                                ErrorMessage="Required" Font-Size="X-Small" ForeColor="#FF3300" Operator="NotEqual"
                                ValueToCompare="0"></asp:CompareValidator>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grd_data" runat="server" AllowSorting="True" CssClass="table table-bordered table-striped Datatable"
                    AutoGenerateColumns="false" OnPreRender="grd_data_PreRender">
                    <Columns>
                        <asp:TemplateField HeaderText="Kit Number" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:Label ID="lbl_kit_no" runat="server" Text='<%# Bind("Kit_No") %>' CssClass="txt_center"
                                    Font-Bold="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Site ID" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:Label ID="txt_Site_ID" runat="server" Text='<%# Bind("Site_ID") %>' CssClass="txt_center"
                                    Font-Bold="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Expiry Date" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:Label ID="lbl_gender" runat="server" Text='<%# Bind("Expiry_Date") %>' Font-Bold="true"
                                    ForeColor="Red"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
