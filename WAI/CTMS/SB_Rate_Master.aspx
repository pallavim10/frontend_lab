<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SB_Rate_Master.aspx.cs" Inherits="CTMS.SB_Rate_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable1").dataTable({ "bSort": false, "ordering": false,
                "bDestroy": true, stateSave: false
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Manage Task Rates</h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="Label11" runat="server"></asp:Label>
                </div>
                <table>
                    <tr>
                        <td class="label">
                            Select Site:
                        </td>
                        <td class="requiredSign">
                            <asp:Label ID="lbl_Version" runat="server" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfv_INVID" runat="server" InitialValue="99" ControlToValidate="ddl_INVID"
                                ValidationGroup="View"></asp:RequiredFieldValidator>
                        </td>
                        <td class="Control">
                            <asp:DropDownList ID="ddl_INVID" Style="text-align: center;" runat="server" Width="100%"
                                class="form-control drpControl required" ValidationGroup="View" AutoPostBack="true"
                                OnSelectedIndexChanged="ddl_INVID_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div>
            <asp:GridView ID="gvRates" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped" >
                <Columns>
                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                        HeaderText="ID">
                        <ItemTemplate>
                            <asp:Label ID="lbl_TaskId" runat="server" Text='<%# Bind("Task_Id") %>'></asp:Label>
                            <asp:HiddenField ID="hf_TaskID" runat="server" Value='<%# Bind("Task_Id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                        HeaderText="ID">
                        <ItemTemplate>
                            <asp:Label ID="lbl_SubTaskId" runat="server" Text='<%# Bind("Sub_Task_Id") %>'></asp:Label>
                            <asp:HiddenField ID="hf_SubTaskID" runat="server" Value='<%# Bind("Sub_Task_Id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sub-Tasks">
                        <ItemTemplate>
                            <asp:Label ID="lbl_TaskName" Width="60%" ToolTip='<%# Bind("Sub_Task_Name") %>' CssClass="label"
                                runat="server" Text='<%# Bind("Sub_Task_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rates" ItemStyle-CssClass="txt_center" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:TextBox Style="text-align: center;" ID="txtRate" Text='<%# Bind("Rate") %>'
                                runat="server" class="txt_center form-control"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <div class="txt_center">
                <asp:Button ID="btnsubmit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                    ValidationGroup="section" OnClick="btnsubmit_Click" />
            </div>
            <br />
            <br />
        </div>
    </div>
</asp:Content>
