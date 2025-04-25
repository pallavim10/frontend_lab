<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ViewProjects.aspx.cs" Inherits="CTMS.ViewProjects" %>

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
            <div>
            <h3 class="box-title">
                Project Lists
            </h3>
                </div>

                   <div id="Div2" class="dropdown" runat="server" style="display: inline-flex"><h3 class="box-title">
               <asp:LinkButton runat="server" ID="lbExportProject" OnClick="lbExportProject_Click" ToolTip="Export to Excel"
                 Text="" CssClass="dropdown-item dropdown-toggle glyphicon glyphicon-download-alt" Style="color: darkblue;"></asp:LinkButton>
		      </h3>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="box-body">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="rows">
                            <div style="width: 100%; height: 500px; overflow: auto;">
                                <div>
                                    <asp:GridView ID="grdprojects" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable1"
                                        OnRowCommand="grdprojects_RowCommand" OnRowDataBound="grdprojects_RowDataBound"
                                        AllowSorting="True" OnPreRender="grdprojects_PreRender">
                                        <Columns>
                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("Project_ID") %>'
                                                        CommandName="EDIT" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                            </ItemTemplate>
                                                </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblprojid" runat="server" Text='<%# Bind("Project_ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Study Name" HeaderStyle-CssClass="width100px" ItemStyle-CssClass="width100px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstudyname" runat="server" Text='<%# Bind("PROJNAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Study Title" HeaderStyle-CssClass="width300px" ItemStyle-CssClass="width300px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstudytitle" runat="server" Text='<%# Bind("PROJTITLE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Phase">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblphase" runat="server" Text='<%# Bind("PHASE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Comparator Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcomparatorname" runat="server" Text='<%# Bind("ComparatorName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sponsor">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsponsor" runat="server" Text='<%# Bind("SPONSOR") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Study Start Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstartdate" runat="server" Text='<%# Bind("PROJSTDATE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Study End Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblenddate" runat="server" Text='<%# Bind("PROJENDDATE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtndelete" runat="server" CommandArgument='<%# Bind("Project_ID") %>'
                                                        Visible="true" CommandName="DELETE" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
