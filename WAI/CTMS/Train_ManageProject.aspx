<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Train_ManageProject.aspx.cs" Inherits="CTMS.Train_ManageProject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
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
                Manage Project Trainings
            </h3>
        </div>
        <table>
            <tr>
                <td class="label">
                    Select Section:
                </td>
                <td class="requiredSign">
                    <asp:Label ID="lblSection" runat="server" Text="*"></asp:Label>
                </td>
                <td class="Control">
                    <asp:DropDownList ID="ddlSec" runat="server" Width="250px" class="form-control drpControl"
                        ValidationGroup="Sec" AutoPostBack="true" OnSelectedIndexChanged="ddlSec_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="style10">
                    &nbsp;
                </td>
            </tr>
        </table>
        <div class="form-group has-warning">
            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
        </div>
        <div>
            <asp:GridView ID="gvSubSec" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CssClass="table table-bordered table-striped" 
                onrowdatabound="gvSubSec_RowDataBound" >
                <Columns>
                    <asp:TemplateField Visible="false" HeaderText="ID">
                        <ItemTemplate>
                            <asp:Label ID="lbl_SectionID" runat="server" Text='<%# Bind("Section_ID") %>'></asp:Label>
                            <asp:Label ID="lbl_SubSectionID" runat="server" Text='<%# Bind("SubSection_ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sub-Sections" ItemStyle-Width="100%">
                        <ItemTemplate>
                            <asp:Label ID="lbl_SubSection" Width="100%" ToolTip='<%# Bind("SubSection") %>' CssClass="label"
                                runat="server" Text='<%# Bind("SubSection") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="width30px txtCenter" ControlStyle-CssClass="txt_center"
                        HeaderText="Site">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSite" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="width30px txtCenter" ControlStyle-CssClass="txt_center"
                        HeaderText="Internal">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkInternal" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <br />
        <div class="row">
            <asp:LinkButton runat="server" ID="btnSubmit" Visible="false" Text="Submit" ForeColor="White"
                Style="margin-left: 120px" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnSubmit_Click"></asp:LinkButton>
        </div>
        <br />
    </div>
</asp:Content>
