<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frnCOUNTRYDETAILS.aspx.cs" Inherits="CTMS.frnCOUNTRYDETAILS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Country Details</h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
                <table>
                    <tr>
                        <td class="label">
                            Select Project:
                        </td>
                        <td class="requiredSign">
                            <asp:Label ID="Label2" runat="server" Text="*"></asp:Label>
                        </td>
                        <td class="Control">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="Drp_Project" runat="server" class="form-control drpControl required"
                                        AutoPostBack="True" OnSelectedIndexChanged="Drp_Project_Name_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="style10">
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Country Code
                        </td>
                        <td class="requiredSign">
                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="Control">
                            <asp:TextBox ID="txtCountryCode" runat="server" class="form-control"></asp:TextBox>
                            <%--  <asp:RequiredFieldValidator ID="Req_Dep_Prefix" runat="server" 
                        ControlToValidate="txtCountryCode" ErrorMessage="Required" Font-Size="X-Small" 
                        ForeColor="Red"></asp:RequiredFieldValidator>--%>
                        </td>
                        <td class="style10">
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Country
                        </td>
                        <td class="requiredSign">
                            <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="Control">
                            <asp:TextBox ID="txtCountry" runat="server" class="form-control"></asp:TextBox>
                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtCountry" ErrorMessage="Required" Font-Size="X-Small" 
                        ForeColor="Red"></asp:RequiredFieldValidator>--%>
                        </td>
                        <td class="style10">
                        </td>
                    </tr>
                    <tr>
                        <td class="style14">
                            &nbsp;
                        </td>
                        <td class="style6">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table>
                            <asp:Repeater ID="rptCountryAdded" runat="server" EnableViewState="False">
                                <HeaderTemplate>
                                    <table class="table table-bordered table-striped width120px margin-left10">
                                        <td scope="col" style="width: 150px; font-size: 12px text-decoration: underline;
                                            font-weight: bold;">
                                            Country Added
                                        </td>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td style="font-size: 11px">
                                            <%# Eval("COUNTRY")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="box">
        <asp:GridView ID="CountryMaster" Caption="Select Country" runat="server" AutoGenerateColumns="False"
            CssClass="table table-bordered table-striped width300px" AlternatingRowStyle-CssClass="alt"
            PagerStyle-CssClass="pgr">
            <Columns>
                <asp:TemplateField HeaderText="COUNTRY ID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none"
                    ItemStyle-CssClass="disp-none">
                    <ItemTemplate>
                        <asp:TextBox ID="CNTRYID" runat="server" Text='<%# Bind("CNTRYID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="COUNTRY CODE" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-CssClass="width100px">
                    <ItemTemplate>
                        <asp:Label ID="COUNTRYCOD" runat="server" Text='<%# Bind("COUNTRYCOD") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="COUNTRY" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="width100px">
                    <ItemTemplate>
                        <asp:Label ID="COUNTRY" runat="server" Text='<%# Bind("COUNTRY") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="OrderID Prefix" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-CssClass="txtCenter">
                    <ItemTemplate>
                        <asp:TextBox ID="txtOrderID_Prefix" runat="server" Text='<%# Bind("Order_ID_Prefix") %>'
                            CssClass="form-control width100px txtCenter"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="OrderID Last NO" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-CssClass="txtCenter">
                    <ItemTemplate>
                        <asp:TextBox ID="txtOrderID_LastNo" runat="server" Text='<%# Bind("Order_ID_Last_No") %>'
                            CssClass="form-control width100px numeric txtCenter"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="OrderID SD Prefix" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-CssClass="txtCenter">
                    <ItemTemplate>
                        <asp:TextBox ID="txtOrderID_SDPrefix" runat="server" Text='<%# Bind("Order_ID_SD_Prefix") %>'
                            CssClass="form-control width100px txtCenter"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="OrderID SD Last No" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-CssClass="txtCenter">
                    <ItemTemplate>
                        <asp:TextBox ID="txtOrderID_SDLastNo" runat="server" Text='<%# Bind("Order_ID_SD_Last_No") %>'
                            CssClass="form-control numeric width100px txtCenter"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Select" ItemStyle-CssClass="width100px">
                    <ItemTemplate>
                        <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Button ID="bntSave" runat="server" Text="Save" Style="margin-bottom: 6px" OnClick="bntSave_Click"
            CssClass="btn btn-primary btn-sm cls-btnSave margin-top4 margin-left10" />
    </div>
</asp:Content>
