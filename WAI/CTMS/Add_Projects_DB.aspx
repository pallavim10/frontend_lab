<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Add_Projects_DB.aspx.cs" Inherits="CTMS.Add_Projects_DB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Create Project Database</h3>

        </div>
        <!-- /.box-header -->
        <!-- text input -->
        <div class="box-body">
            <div class="row">
                <div class="lblError">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
            </div>
            <div class="box box-primary">
                <div class="box-header">
                </div>
                <div class="box-body">
                    <asp:GridView ID="grdProjectDB" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        CssClass="table table-bordered table-striped" OnRowDataBound="grdProjectDB_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="ProjectID" HeaderStyle-CssClass="txtCenter" ItemStyle-CssClass="txtCenter">
                                <ItemTemplate>
                                    <asp:Label ID="lblProjectID" runat="server" Text='<%# Eval("Project_ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PROJECT NAME">
                                <ItemTemplate>
                                    <asp:Label ID="lblPROJNAME" runat="server" Text='<%# Eval("PROJNAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PROJECT DATE">
                                <ItemTemplate>
                                    <asp:Label ID="lblprojectdate" runat="server" Text='<%# Eval("projectdate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DB CREATED">
                                <ItemTemplate>
                                    <asp:Label ID="lblDBCREATED" runat="server" Text='<%# Eval("DBCREATED") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CREATED BY">
                                <ItemTemplate>
                                    <asp:Label ID="lblENTEREDBY" runat="server" Text='<%# Eval("ENTEREDBY") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                <HeaderTemplate>
                                    <asp:Button ID="btnadd" runat="server" Text="Add" CssClass="btn btn-primary btn-sm cls-btnSave"
                                        OnClick="btnadd_Click" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CHKDB" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
