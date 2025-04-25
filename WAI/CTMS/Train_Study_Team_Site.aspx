<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Train_Study_Team_Site.aspx.cs" Inherits="CTMS.Train_Study_Team_Site" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);

            if (div.style.display == "none") {
                div.style.display = "inline";
                document.getElementById('img' + divname).className = 'icon-minus-sign-alt';

            } else {
                div.style.display = "none";
                document.getElementById('img' + divname).className = 'icon-plus-sign-alt';
            }
        }

        function ManipulateAll(ID) {
            var img = document.getElementById('img' + ID);

            if (img.className == 'icon-plus-sign-alt') {
                img.className = 'icon-minus-sign-alt'
                $("div[id*='" + ID + "']").css("display", "inline");
                $("i[id*='" + ID + "']").removeClass('icon-plus-sign-alt');
                $("i[id*='" + ID + "']").addClass('icon-minus-sign-alt');
            } else {
                img.className = 'icon-plus-sign-alt'
                $("div[id*='" + ID + "']").css("display", "none");
                $("i[id*='" + ID + "']").removeClass('icon-minus-sign-alt');
                $("i[id*='" + ID + "']").addClass('icon-plus-sign-alt');
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">
                        Manage Study Team
                    </h3>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Select Investigator : &nbsp;
                                    <asp:Label ID="Label3" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList runat="server" ID="ddlINV" CssClass="form-control required">
                                    </asp:DropDownList>
                                </div>
                                <div class="label col-md-2">
                                    &nbsp;
                                    <%--<asp:Label ID="Label4" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>--%>
                                </div>
                                <div class="col-md-4">
                                    &nbsp;
                                    <%--<asp:TextBox runat="server" ID="TextBox2" CssClass="form-control required"></asp:TextBox>--%>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Enter Name : &nbsp;
                                    <asp:Label ID="Label5" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox runat="server" ID="txtName" CssClass="form-control required"></asp:TextBox>
                                </div>
                                <div class="label col-md-2">
                                    Enter Role :&nbsp;
                                    <asp:Label ID="Label6" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox runat="server" ID="txtRole" CssClass="form-control required"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Enter EmailID : &nbsp;
                                    <asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox runat="server" ID="txtEmailID" CssClass="form-control required"></asp:TextBox>
                                </div>
                                <div class="label col-md-2">
                                    &nbsp;
                                </div>
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Start Date : &nbsp;
                                    <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox runat="server" ID="txtStartDate" CssClass="form-control txtDate required"></asp:TextBox>
                                </div>
                                <div class="label col-md-2">
                                    End Date :&nbsp;
                                    <%--<asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>--%>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox runat="server" ID="txtEndDate" CssClass="form-control txtDate"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row txt_center" style="margin-left: -20%;">
                            <asp:LinkButton runat="server" ID="lbtnUpdate" Text="Update" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave"
                                OnClick="lbtnUpdate_Click"></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lbtnSubmit" Text="Submit" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave"
                                OnClick="lbtnSubmit_Click"></asp:LinkButton>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton runat="server" ID="lbtnCancel" Text="Cancel" ForeColor="White" CssClass="btn btn-primary btn-sm"
                                OnClick="lbtnCancel_Click"></asp:LinkButton>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">
                        Study Team View
                    </h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <asp:GridView ID="gvTeam" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                            Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="gvTeam_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="INVID" ItemStyle-CssClass="txt_center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblINV" runat="server" ToolTip='<%# Bind("INVNAM") %>' Text='<%# Bind("INVID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" CssClass="label" runat="server" ToolTip='<%# Bind("Name") %>'
                                            Text='<%# Bind("Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Role" ItemStyle-CssClass="txt_center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRole" CssClass="label" runat="server" ToolTip='<%# Bind("Role") %>'
                                            Text='<%# Bind("Role") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email ID" ItemStyle-CssClass="txt_center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmailID" CssClass="label" runat="server" ToolTip='<%# Bind("EmailID") %>'
                                            Text='<%# Bind("EmailID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="From" ItemStyle-CssClass="txt_center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStartDate" CssClass="label" runat="server" ToolTip='<%# Bind("StartDate1") %>'
                                            Text='<%# Bind("StartDate1") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="To" ItemStyle-CssClass="txt_center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEndDate" CssClass="label" runat="server" ToolTip='<%# Bind("EndDate1") %>'
                                            Text='<%# Bind("EndDate1") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnupdateTeam" runat="server" CommandArgument='<%# Bind("ID") %>'
                                            CommandName="Edit1" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;&nbsp;
                                        <asp:LinkButton ID="lbtndeleteTeam" runat="server" CommandArgument='<%# Bind("ID") %>'
                                            CommandName="Delete1" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <br />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
