<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IWRS_Threshold_Metric.aspx.cs" Inherits="CTMS.IWRS_Threshold_Metric" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Define Threshold</h3>
                </div>
                <div class="form-group">
                    <div class="form-group has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="form-group">
                    <div class="has-warning">
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </div>
                    <div class="rows">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="gvSites" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    CssClass="table table-bordered table-striped txt_center" OnRowDataBound="gvSites_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Country">
                                            <ItemTemplate>
                                                <asp:Label ID="COUNTRYNAME" runat="server" Text='<%# Bind("COUNTRYNAME") %>'></asp:Label>
                                                <asp:HiddenField runat="server" ID="COUNTRYID" Value='<%# Bind("COUNTRYID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Site ID">
                                            <ItemTemplate>
                                                <asp:Label ID="SITEID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <tr>
                                                    <td colspan="100%" style="padding: 2px; padding-left: 2%;">
                                                        <div style="float: right; font-size: 13px;">
                                                        </div>
                                                        <div>
                                                            <div style="position: relative; overflow: auto;">
                                                                <asp:GridView ID="gvTriggers" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                                                    CssClass="table table-bordered table-striped table-striped1">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Treatment Code" ItemStyle-CssClass="width100px">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="TREAT_GRP" runat="server" Text='<%# Bind("TREAT_GRP") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Treatment Arms" ItemStyle-CssClass="width100px">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="TREAT_GRP_NAME" runat="server" Text='<%# Bind("TREAT_GRP_NAME") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Treatment Strength" ItemStyle-CssClass="width100px">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="TREAT_STRENGTH" runat="server" Text='<%# Bind("TREAT_STRENGTH") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Trigger Value" ItemStyle-Width="7%">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="TRIGGER_VAL" CssClass="form-control numeric txt_center" Width="100%"
                                                                                    runat="server" Text='<%# Bind("TRIGGER_VAL") %>'></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Responsible Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="25%">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="RESPO_EMAIL" CssClass="form-control noSpace" Width="100%" TextMode="MultiLine"
                                                                                    runat="server" Text='<%# Bind("RESPO_EMAIL") %>'></asp:TextBox>
                                                                                <asp:RegularExpressionValidator runat="server" ID="regexRESPO_EMAIL"
                                                                                    ControlToValidate="RESPO_EMAIL" CssClass="lblError"
                                                                                    ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"
                                                                                    Text="Invalid Email ID(s)"></asp:RegularExpressionValidator>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-10 txt_center">
                                <asp:Button ID="btnsubmit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnsubmit_Click" />
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>
