<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UMT_Site_User.aspx.cs" Inherits="CTMS.UMT_Site_User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="CommonFunctionsJs/UMT/UMT_ConfirmMsg.js"></script>
    <script type="text/javascript">

        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                stateSave: false
            });

            $(".Datatable").parent().parent().addClass('fixTableHead');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Create Site User
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    <asp:HiddenField ID="hdnID" runat="server" />
                </div>
            </div>
        </div>
    </div>

    <div class="box box-primary">
        <div class="box-header">
            <h3 class="box-title">Site Details
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Site Id : &nbsp;
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="drpSiteID" runat="server" AutoPostBack="true" CssClass="form-control required width200px">
                            </asp:DropDownList>
                        </div>
                        <div class="label col-md-2">
                            Study Role :&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="drpStudyRole" runat="server" AutoPostBack="true" CssClass="form-control required width200px">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            First Name : &nbsp;
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control required width200px"></asp:TextBox>
                        </div>
                        <div class="label col-md-2">
                            Last Name :&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control required width200px"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Email Id : &nbsp;
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtEmailid" CssClass="form-control required width200px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="regexEmailValid" ForeColor="Red" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmailid" ErrorMessage="Invalid Email"></asp:RegularExpressionValidator>
                        </div>
                        <div class="label col-md-2">
                            Contact No :&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtContactNo" TextMode="Number" Min="0" CssClass="form-control required width200px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revContactNumber" ForeColor="Red" runat="server" ControlToValidate="txtContactNo" ValidationExpression="^\d{10}$" ErrorMessage="Invalid contact number. Please enter a 10-digit number."></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Unblind : &nbsp;
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList runat="server" ID="drpUnblind" CssClass="form-control width200px required">
                                <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                <asp:ListItem Value="Blinded" Text="Blinded"></asp:ListItem>
                                <asp:ListItem Value="Unblinded" Text="Unblinded"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="label col-md-2">
                            &nbsp;
                               
                        </div>
                        <div class="col-md-3">
                        </div>

                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Select Systems :&nbsp;
                                <asp:Label ID="Label3" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-8">
                            <asp:Repeater runat="server" ID="repeatSystem" OnItemDataBound="repeatSystem_ItemDataBound">
                                <ItemTemplate>
                                    <div class="col-md-6">
                                        <asp:CheckBox ID="ChkSelect" runat="server" />
                                        &nbsp;
                                    <asp:Label ID="lblSystemName" runat="server" Text='<%# Bind("SystemName") %>'></asp:Label>
                                        <asp:Label ID="lblSystemID" runat="server" Text='<%# Bind("SystemID") %>' Visible="false"></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
                <br />
                <div class="form-group">
                    <br />
                    <div class="row txt_center">
                        <asp:LinkButton runat="server" ID="lbtnSubmit" Text="Submit" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnSubmit_Click"></asp:LinkButton>
                        &nbsp;&nbsp;
                                <asp:LinkButton runat="server" ID="lbnUpdate" Text="Update" ForeColor="White" CssClass="btn btn-primary btn-sm" OnClick="lbnUpdate_Click" Visible="false"></asp:LinkButton>&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton runat="server" ID="lbtnCancel" Text="Cancel" ForeColor="White" CssClass="btn btn-primary btn-sm" OnClick="lbtnCancel_Click"></asp:LinkButton>
                    </div>
                    <br />
                </div>
            </div>
        </div>
        <div class="rows">
            <div style="width: 100%; overflow: auto;">
                <div>
                    <asp:GridView ID="grdSiteUser" runat="server" AllowSorting="True" AutoGenerateColumns="false"
                        CssClass="table table-bordered table-striped Datatable" OnPreRender="grdUserDetails_PreRender" OnRowCommand="grdUser_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtedituser" runat="server" CommandArgument='<%# Bind("ID") %>'
                                        CommandName="EIDIT" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;&nbsp;
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                                HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserID" runat="server" Text='<%# Bind("UserID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="First Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblFirstName" runat="server" Text='<%# Bind("Fname") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Last Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblLastname" runat="server" Text='<%# Bind("Lname") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email Id">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmailID" runat="server" Text='<%# Bind("EmailID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contact No">
                                <ItemTemplate>
                                    <asp:Label ID="lblContactNo" runat="server" Text='<%# Bind("ContactNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Blinded">
                                <ItemTemplate>
                                    <asp:Label ID="lblBlinded" runat="server" Text='<%# Bind("Blind") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Study Role">
                                <ItemTemplate>
                                    <asp:Label ID="lblStudyRole" runat="server" Text='<%# Bind("StudyRole") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtdeleteuser" runat="server" CommandArgument='<%# Bind("ID") %>'
                                        CommandName="DELETED" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this user  : ", Eval("Fname") +" "+ Eval("Lname")) %>' ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

