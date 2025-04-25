<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CTMS_ViewAgenda.aspx.cs" Inherits="CTMS.CTMS_ViewAgenda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/jscript">

        function ConfirmReschedule() {
            confirm("Are you Sure Want to Reschedule the Meeting!");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="Upd_Pan_Sel_Dept" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">
                        View Meeting</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="lblError">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div>
                    </div>
                    <table>
                        <tr>
                            <td class="label">
                                Meeting From Date:
                            </td>
                            <td class="requiredSign">
                                <asp:Label ID="Label6" runat="server" Text="*"></asp:Label>
                            </td>
                            <td class="Control">
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control txtDate required"></asp:TextBox>
                            </td>
                            <td class="style10">
                            </td>
                            <td class="label">
                                Meeting To Date
                            </td>
                            <td class="requiredSign">
                                <asp:Label ID="Label1" runat="server" Text="*"></asp:Label>
                            </td>
                            <td class="Control">
                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control txtDate required"></asp:TextBox>
                            </td>
                            <td class="style10">
                            </td>
                            <td class="label">
                                Status
                            </td>
                            <td class="requiredSign">
                                <asp:Label ID="Label2" runat="server" Text="*"></asp:Label>
                            </td>
                            <td class="Control">
                                <asp:DropDownList runat="server" ID="drpStatus" CssClass="form-control">
                                </asp:DropDownList>
                            </td>
                            <td class="style10">
                            </td>
                            <td class="label">
                            </td>
                            <td class="requiredSign">
                                <asp:Label ID="Label3" runat="server" Text="*"></asp:Label>
                            </td>
                            <td class="Control">
                                <asp:Button ID="btnGetData" Text="Get Data" runat="server" CssClass="btn btn-primary btn-sm"
                                    OnClick="btnGetData_Click" />
                            </td>
                            <td class="style10">
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:GridView ID="grdMeeting" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                        OnRowCommand="grdMeeting_RowCommand" OnRowDataBound="grdMeeting_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Meeting ID" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="AgendaID" runat="server" Text='<%# Bind("AgendaID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Meeting Date" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="AgendaDT" runat="server" Text='<%# Bind("AgendaDT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Meeting Time" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="AgendaTM" runat="server" Text='<%# Bind("AgendaTM") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Venue" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="Venue" runat="server" Text='<%# Bind("Venue") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Created Date" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="DTENTERED" runat="server" Text='<%# Bind("DTENTERED") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Created By" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="ENTEREDBY" runat="server" Text='<%# Bind("ENTEREDBY") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="Status" runat="server" Style="color: Blue; font-weight: bold" Text='<%# Bind("Status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkCompleted" runat="server" Style="color: #333333" CommandArgument='<%# Bind("AgendaID") %>'
                                        CommandName="Completed" ToolTip="Completed">
                                            <span class="glyphicon glyphicon-ok"></span>
                                    </asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="lnkreschedule" runat="server" Style="color: #333333" CommandArgument='<%# Bind("AgendaID") %>'
                                        CommandName="Reschedule" ToolTip="Reschedule">
                                            <span class="glyphicon glyphicon-repeat"></span>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lnkFollowUp" runat="server" Style="color: #333333" CommandArgument='<%# Bind("AgendaID") %>'
                                        CommandName="FollowUp" ToolTip="FollowUp">
                                        <span class="glyphicon glyphicon-list-alt"></span>
                                    </asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="lnkCancel" Style="color: #333333;" runat="server" CommandArgument='<%# Bind("AgendaID") %>'
                                        CommandName="Cancelled" ToolTip="Cancel">
                                            <span class="glyphicon glyphicon-remove"></span>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
