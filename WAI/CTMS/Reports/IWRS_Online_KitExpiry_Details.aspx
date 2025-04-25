<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IWRS_Online_KitExpiry_Details.aspx.cs" Inherits="CTMS.IWRS_Online_KitExpiry_Details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Kit Expiry List </h3>
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
                                    <asp:DropDownList ID="Drp_Project_Name" runat="server" CssClass="form-control" 
                                        AutoPostBack="True" 
                                        onselectedindexchanged="Drp_Project_Name_SelectedIndexChanged">
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
                                <asp:Label ID="lbl_gender" runat="server" Text='<%# Bind("Expiry_Date") %>' Font-Bold="true" ForeColor="Red"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

