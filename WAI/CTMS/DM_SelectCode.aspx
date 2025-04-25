<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DM_SelectCode.aspx.cs" Inherits="CTMS.DM_SelectCode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title"><asp:Label runat="server" ID="lblDictNameHeader" />&nbsp;||&nbsp;<asp:Label runat="server" ID="lblSUBJID" />&nbsp;||&nbsp;<asp:Label
                runat="server" ID="lblEVENTCODE" />&nbsp;||&nbsp;<asp:Label runat="server" ID="lblTERM" />
            </h3>
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </div>
                <div class="row">
                    <div class="col-md-12" style="display: inline-flex;">
                        <asp:Label ID="Label4" class="label" runat="server" Text="Enter Modified Term :"></asp:Label>&nbsp;&nbsp;
                        <asp:TextBox ID="txtModifiedTerm" runat="server" CssClass="form-control" OnTextChanged="txtModifiedTerm_TextChanged"
                            AutoPostBack="true"></asp:TextBox>
                    </div>
                </div>
                <br />
            </div>
        </div>
    </div>
    <div class="box">
        <div class="box-header">
            <h3 class="box-title"><asp:Label runat="server" ID="lblDictName" /></h3>
        </div>
        <div class="box-body expandBody" runat="server" id="divGV" style="overflow: auto;">
            <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CssClass="table table-bordered table-striped Datatable" OnPreRender="GridView1_PreRender"
                OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="System Organ Class" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="lblsoc_name" runat="server" Text='<%# Eval("soc_name") %>' CssClass="txt_center"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="High Level Group Term" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="lblhlgt_name" runat="server" Text='<%# Eval("hlgt_name") %>' CssClass="txt_center"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="High Level Term" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="lblhlt_name" runat="server" Text='<%# Eval("hlt_name") %>' CssClass="txt_center"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Prefered Term" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="lblMedDRACode" runat="server" Text='<%# Eval("pt_name") %>' CssClass="txt_center"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Low Level Term" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="lblllt_name" runat="server" Text='<%# Eval("llt_name") %>' CssClass="txt_center"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Select" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnCodeDone" runat="server" CommandName="Code" ToolTip='<%# Eval("ID") %>'
                                CommandArgument='<%# Eval("ID") %>' Style="color: Black"><i class="fa fa-check-circle"></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--
                    <asp:TemplateField ItemStyle-CssClass="width30px txt_center">
                        <HeaderTemplate>
                            <asp:Button ID="Btn_Add_Fun" runat="server" OnClick="Btn_Add_Fun_Click" Text="Submit"
                                CssClass="btn btn-primary btn-sm cls-btnSave" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
            <asp:GridView ID="GridView2" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CssClass="table table-bordered table-striped Datatable" OnPreRender="GridView1_PreRender"
                OnRowCommand="GridView2_RowCommand" OnRowDataBound="GridView2_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Drug Name" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="pt_name" runat="server" Text='<%# Eval("pt_name") %>' CssClass="txt_center"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ATC Level 1 Description" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="CMATC1C" runat="server" Text='<%# Eval("CMATC1C") %>' CssClass="txt_center"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ATC Level 1 Code" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="CMATC1CD" runat="server" Text='<%# Eval("CMATC1CD") %>' CssClass="txt_center"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ATC Level 2 Description" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="CMATC2C" runat="server" Text='<%# Eval("CMATC2C") %>' CssClass="txt_center"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ATC Level 2 Code" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="CMATC2CD" runat="server" Text='<%# Eval("CMATC2CD") %>' CssClass="txt_center"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ATC Level 3 Description" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="CMATC3C" runat="server" Text='<%# Eval("CMATC3C") %>' CssClass="txt_center"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ATC Level 3 Code" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="CMATC3CD" runat="server" Text='<%# Eval("CMATC3CD") %>' CssClass="txt_center"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ATC Level 4 Description" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="CMATC4C" runat="server" Text='<%# Eval("CMATC4C") %>' CssClass="txt_center"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ATC Level 4 Code" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="CMATC4CD" runat="server" Text='<%# Eval("CMATC4CD") %>' CssClass="txt_center"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ATC Level 5 Description" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="CMATC5C" runat="server" Text='<%# Eval("CMATC5C") %>' CssClass="txt_center"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ATC Level 5 Code" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="CMATC5CD" runat="server" Text='<%# Eval("CMATC5CD") %>' CssClass="txt_center"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Select" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnCodeDone" runat="server" CommandName="Code" ToolTip='<%# Eval("ID") %>'
                                CommandArgument='<%# Eval("ID") %>' Style="color: Black"><i class="fa fa-check-circle"></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
