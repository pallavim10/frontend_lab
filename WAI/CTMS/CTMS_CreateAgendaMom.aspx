<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CTMS_CreateAgendaMom.aspx.cs" Inherits="CTMS.CTMS_CreateAgendaMom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/jscript">
        function DownloadDoc(element) {
            var AgendaID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var test = "CTMS_CreateAgendaMomUpload.aspx?AgendaID=" + AgendaID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=370,width=1200";
            window.open(test, '_blank', strWinProperty);
            return false;
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
                        Upload MoM</h3>
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
                        OnRowDataBound="grdMeeting_RowDataBound">
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
                            <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Upload"    ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnUpload" runat="server" ToolTip="Study Material" OnClientClick="return DownloadDoc(this);">
                        <i class="glyphicon glyphicon-upload" style="color:#333333;" aria-hidden="true"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Download" ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnDownloadDoc" runat="server" Visible="false" ToolTip="Study Material" OnClientClick="return DownloadDoc(this);" CssClass="btn">
                        <i class="fa fa-download" style="color:#333333;" aria-hidden="true"></i>
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
