<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Train_Project.aspx.cs" Inherits="CTMS.Train_Project" %>

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
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlSec" runat="server" Width="250px" class="form-control drpControl"
                                        ValidationGroup="Sec" AutoPostBack="true" OnSelectedIndexChanged="ddlSec_SelectedIndexChanged">
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
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <asp:GridView ID="gvSubSec" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    CssClass="table table-bordered table-striped Datatable1" OnPreRender="gvSubSec_PreRender"
                    OnRowDataBound="gvSubSec_RowDataBound">
                    <Columns>
                        <asp:TemplateField Visible="false" HeaderText="ID">
                            <ItemTemplate>
                                <asp:Label ID="lbl_SectionID" runat="server" Text='<%# Bind("Section_ID") %>'></asp:Label>
                                <asp:Label ID="lbl_SubSectionID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tasks" ItemStyle-Width="100%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_SubSection" Width="100%" ToolTip='<%# Bind("SubSection") %>' CssClass="label"
                                    runat="server" Text='<%# Bind("SubSection") %>'></asp:Label>
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
</asp:Content>
