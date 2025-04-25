<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="HOMEPAGE_DEFINE_TABS.aspx.cs" Inherits="CTMS.HOMEPAGE_DEFINE_TABS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <script>
        function set_AnsColor(element) {
            var acolor = element.value;
            $('#<% =hfAnsColor.ClientID %>').attr('value', acolor);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box-body">
                <div class="form-group">
                    <div class="form-group has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        <asp:HiddenField runat="server" ID="hfOLDVARIABLENAME" />
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="box box-primary" style="min-height: 264px;">
                                    <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                        <h4 class="box-title" align="left">
                                            Add Tabs Name
                                        </h4>
                                    </div>
                                    <div class="box-body">
                                        <div align="left" style="margin-left: 5px">
                                            <div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-5">
                                                            <label>
                                                                SEQNO. :</label>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:TextBox Style="width: 200px;" ID="txtSEQNO" runat="server" CssClass="form-control required"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-5">
                                                            <label>
                                                                Enter Tab Name :</label>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:TextBox Style="width: 200px;" ID="txtTabName" runat="server" CssClass="form-control required"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row" runat="server" visible="false">
                                                    <div class="col-md-12">
                                                        <div class="col-md-5">
                                                            <label>
                                                                Select Tab Background Color :</label>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <input type="color" value="<% =AnsColorValue %>" id="AnsColor" name="AnsColor" onchange="set_AnsColor(this);" />&nbsp;&nbsp;
                                                            <asp:HiddenField ID="hfAnsColor" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-5">
                                                            &nbsp;
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:Button ID="btnSubmit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                                OnClick="btnSubmit_Click" />
                                                            <asp:Button ID="btnUpdate" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                                OnClick="btnUpdate_Click" />
                                                            <asp:Button ID="btncancel" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                                OnClick="btncancel_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="box box-primary" style="min-height: 284px;">
                                    <div class="box-header with-border">
                                        <h4 class="box-title" align="left">
                                            Records
                                        </h4>
                                        <div class="pull-right" style="margin: 10px;">
                                            <a id="A1" href="HomePage.aspx" runat="server">Back to HomePage</a>
                                        </div>
                                    </div>
                                    <div class="box-body">
                                        <div align="left" style="margin-left: 5px">
                                            <div>
                                                <div class="rows">
                                                    <div style="width: 100%; height: 284px; overflow: auto;">
                                                        <div>
                                                            <asp:GridView ID="grdTabs" runat="server" AutoGenerateColumns="true" CssClass="table table-bordered table-striped"
                                                                Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdTabs_RowCommand"
                                                                OnRowDataBound="grdTabs_RowDataBound">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none"
                                                                        ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="SEQNO" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="SEQNO" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Tab Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="TABSNAME" runat="server" Text='<%# Bind("TABSNAME") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Tab color" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="TABSCOLOR" runat="server" Text='<%# Bind("TABSCOLOR") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                                CommandName="EditField" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp&nbsp&nbsp
                                                                            <asp:LinkButton ID="lbtndeleteSection" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                                CommandName="DeleteField" ToolTip="Delete"><i class="fa fa-trash"></i></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
