<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="LAB_MASTER.aspx.cs" Inherits="CTMS.LAB_MASTER" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Lab Details
            </h3>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="box-body">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="box box-primary" style="min-height: 300px;">
                                        <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                            <h4 class="box-title" align="left">
                                                Add Lab
                                            </h4>
                                        </div>
                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="col-md-4">
                                                                <label>
                                                                    Select Site ID:</label>
                                                            </div>
                                                            <div class="col-md-7">
                                                                <asp:DropDownList runat="server" Width="100%" ID="drpInvid" CssClass="form-control required"
                                                                    AutoPostBack="True" OnSelectedIndexChanged="drpInvid_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="col-md-4">
                                                                <label>
                                                                    Enter LAB ID:</label>
                                                            </div>
                                                            <div class="col-md-7">
                                                                <asp:TextBox runat="server" Width="100%" ID="txtLabID" CssClass="form-control required">
                                                                </asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="col-md-4">
                                                                <label>
                                                                    Enter LAB Name:</label>
                                                            </div>
                                                            <div class="col-md-7">
                                                                <asp:TextBox runat="server" Width="100%" ID="txtLabName" CssClass="form-control required">
                                                                </asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="col-md-4">
                                                                &nbsp;
                                                            </div>
                                                            <div class="col-md-7">
                                                                <asp:Button ID="btnsubmitLab" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                                    OnClick="btnsubmitLab_Click" />
                                                                <asp:Button ID="btnupdateLab" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                                    OnClick="btnupdateLab_Click" />&nbsp;&nbsp;
                                                                <asp:Button ID="btncancelLab" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                                    OnClick="btncancelLab_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="box box-primary">
                                        <div class="box-header with-border" style="float: left;">
                                            <h4 class="box-title" align="left">
                                                Records
                                            </h4>
                                        </div>
                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div>
                                                    <div class="rows">
                                                        <div style="width: 100%; height: 264px; overflow: auto;">
                                                            <div>
                                                                <asp:GridView ID="grdLab" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdVisit_RowCommand">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="ID" ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none"
                                                                            ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="INVID" ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none"
                                                                            ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblINVID" runat="server" Text='<%# Bind("INVID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Lab ID" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblLab_ID" runat="server" Text='<%# Bind("Lab_ID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Lab Name" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblLab_Name" runat="server" Text='<%# Bind("Lab_Name") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                                    CommandName="EditLab" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                                <asp:LinkButton ID="lbtndeleteSection" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                                    CommandName="DeleteLab" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
    </div>
</asp:Content>
