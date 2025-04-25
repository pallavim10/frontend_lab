<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Budget_ComplexityMaster.aspx.cs"
    MasterPageFile="~/Site.Master" Inherits="CTMS.Budget_ComplexityMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        function pageLoad() {
            $(".Datatable1").dataTable({ "bSort": false, "ordering": false,
                "bDestroy": true, stateSave: true
            });
        }


        function Print() {

            var ProjectId = '<%= Session["PROJECTID"] %>'
            var test = "ReportBudget_ComplexityMaster.aspx?ProjectId=" + ProjectId;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500px,width=1000px";
            window.open(test, '_blank', strWinProperty);
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Manage Complexity Rate
                <asp:LinkButton ID="lbtnprint" runat="server" Text="Print" OnClientClick="return Print()"
                    CssClass="btn-sm">
      <span class="glyphicon glyphicon-print"></span>Print</asp:LinkButton>
            </h3>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div>
            <asp:GridView ID="gvComplexity" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CssClass="table table-bordered table-striped Datatable1">
                <Columns>
                    <asp:TemplateField HeaderText="Roles">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" CssClass="label" Text='<%# Bind("Role") %>'></asp:Label>
                            <asp:Label ID="lblRole" runat="server" CssClass="label" Visible="false" Text='<%# Bind("Role_ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="1" ItemStyle-CssClass="txt_center" ControlStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:TextBox Style="width: 50px; text-align: center;" ID="txtRate1" Text='<%# Bind("1") %>'
                                runat="server" CssClass="form-control required"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="2" ItemStyle-CssClass="txt_center" ControlStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:TextBox Style="width: 50px; text-align: center;" ID="txtRate2" Text='<%# Bind("2") %>'
                                runat="server" CssClass="form-control required"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="3" ItemStyle-CssClass="txt_center" ControlStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:TextBox Style="width: 50px; text-align: center;" ID="txtRate3" Text='<%# Bind("3") %>'
                                runat="server" CssClass="form-control required"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="4" ItemStyle-CssClass="txt_center" ControlStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:TextBox Style="width: 50px; text-align: center;" ID="txtRate4" Text='<%# Bind("4") %>'
                                runat="server" CssClass="form-control required"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="5" ItemStyle-CssClass="txt_center" ControlStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:TextBox Style="width: 50px; text-align: center;" ID="txtRate5" Text='<%# Bind("5") %>'
                                runat="server" CssClass="form-control required"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="6" ItemStyle-CssClass="txt_center" ControlStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:TextBox Style="width: 50px; text-align: center;" ID="txtRate6" Text='<%# Bind("6") %>'
                                runat="server" CssClass="form-control required"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="7" ItemStyle-CssClass="txt_center" ControlStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:TextBox Style="width: 50px; text-align: center;" ID="txtRate7" Text='<%# Bind("7") %>'
                                runat="server" CssClass="form-control required"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="8" ItemStyle-CssClass="txt_center" ControlStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:TextBox Style="width: 50px; text-align: center;" ID="txtRate8" Text='<%# Bind("8") %>'
                                runat="server" CssClass="form-control required"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="9" ItemStyle-CssClass="txt_center" ControlStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:TextBox Style="width: 50px; text-align: center;" ID="txtRate9" Text='<%# Bind("9") %>'
                                runat="server" CssClass="form-control required"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="10" ItemStyle-CssClass="txt_center" ControlStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:TextBox Style="width: 50px; text-align: center;" ID="txtRate10" Text='<%# Bind("10") %>'
                                runat="server" CssClass="form-control required"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <asp:Button ID="btnsubmit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                ValidationGroup="section" OnClick="btnsubmit_Click" />
            <br />
            <br />
        </div>
    </div>
</asp:Content>
