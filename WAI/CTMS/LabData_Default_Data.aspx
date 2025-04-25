<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="LabData_Default_Data.aspx.cs" Inherits="CTMS.LabData_Default_Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-5">
                                <div class="box box-primary" style="min-height: 300px;">
                                    <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                        <h4 class="box-title" align="left">
                                            Add Default Data
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
                                                            <asp:DropDownList runat="server" ID="drpInvid" CssClass="form-control required" AutoPostBack="true"
                                                                OnSelectedIndexChanged="drpInvid_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            <label>
                                                                Select Lab ID:</label>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:DropDownList runat="server" ID="drpLabID" CssClass="form-control required" AutoPostBack="true"
                                                                OnSelectedIndexChanged="drpLabID_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            <label>
                                                                Select Test Name:</label>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:DropDownList runat="server" ID="drpTests" CssClass="form-control required" AutoPostBack="true"
                                                                OnSelectedIndexChanged="drpTests_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                            Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemID" runat="server" Text='<%# Bind("ITEMID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Item Name" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItems" runat="server" Text='<%# Bind("ITEM") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Data" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtData" CssClass="form-control" runat="server" Width="100px" Text='<%# Bind("Data") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            &nbsp;
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:Button ID="btnsubmitLabData" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                                OnClick="btnsubmitLabData_Click" />
                                                            <asp:Button ID="btnupdateLabData" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                                OnClick="btnupdateLabData_Click" />
                                                            <asp:Button ID="btncancelLabData" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                                OnClick="btncancelLabData_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-7">
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
                                                            <asp:GridView ID="grdLabData" runat="server" AutoGenerateColumns="true" CssClass="table table-bordered table-striped txt_center"
                                                                Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowDataBound="grdLabData_RowDataBound">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="RECID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRECID" Font-Size="Small" Text='<%# Bind("RECID") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtnupdate" OnClick="lbtnupdate_Click" runat="server" CommandArgument='<%# Bind("RECID") %>'
                                                                                CommandName="EditLab" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                            <asp:LinkButton ID="lbtndelete" OnClick="lbtndelete_Click" runat="server" CommandArgument='<%# Bind("RECID") %>'
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
</asp:Content>
