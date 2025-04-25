<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Manage_User_Groups.aspx.cs" Inherits="CTMS.Manage_User_Groups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Manage User Groups
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
                                                Add Groups
                                            </h4>
                                        </div>
                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div>
                                                    <div class="rows">
                                                        <div class="col-md-12">
                                                            <div class="col-md-3">
                                                                <label>
                                                                    Enter Group Name:</label>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtGroupsname" runat="server" Text="" CssClass="form-control required"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:RequiredFieldValidator ID="reqcategory" runat="server" ControlToValidate="txtGroupsname"
                                                                    ValidationGroup="Sub" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12" align="center" style="margin-top: 5px; margin-left: -36px;">
                                                        <asp:Button ID="btnsubmit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                            OnClick="btnsubmit_Click" ValidationGroup="Sub" />
                                                        <asp:Button ID="btnupdate" Text="Update" runat="server" Visible="false" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                            OnClick="btnupdate_Click" ValidationGroup="Sub" />
                                                        <asp:Button ID="btncancel" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                            OnClick="btncancel_Click" />
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
                      <div id="Div1" class="dropdown" runat="server" style="display: inline-flex"><h3 class="box-title">                 
               <asp:LinkButton runat="server" ID="lbAddGroupExport" OnClick="lbAddGroupExport_Click" ToolTip="Export to Excel Assign Project Groups"
                 Text="" CssClass="dropdown-item dropdown-toggle glyphicon glyphicon-download-alt" Style="color: darkblue;"></asp:LinkButton>
                          </h3></div>
                                            </div>

                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div>
                                                    <div class="rows">
                                                        <div style="width: 100%; height: 264px; overflow: auto;">
                                                            <div>
                                                                    <asp:GridView ID="grdUserGroups" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdUserGroups_RowCommand"
                                                                    OnRowDataBound="grdUserGroups_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                                    CommandName="EditGROUP" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Group Name" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgroupname" runat="server" Text='<%# Bind("GroupName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                         <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtndelete" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                                    CommandName="DeleteGROUP" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="box box-primary" style="min-height: 300px;">
                                        <div class="box-header with-border" style="float: left;">
                                            <h4 class="box-title" align="left">
                                                Assign Project Groups
                                            </h4>
                                        </div>
                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div>
                                                    <div class="rows">
                                                        <div class="rows">
                                                            <div class="col-md-12">
                                                                <div class="col-md-4" style="padding-right: 0px">
                                                                    <label>
                                                                        User Groups:</label>
                                                                </div>
                                                                <div class="col-md-6" style="padding-left: 0px">
                                                                    <asp:DropDownList ID="ddlGroups" runat="server" AutoPostBack="true" class="form-control drpControl"
                                                                        OnSelectedIndexChanged="ddlGroups_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-md-1">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlGroups"
                                                                        InitialValue="0" ValidationGroup="subvate" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-12" style="margin-top: 5px;">
                                                            <div class="col-md-4" style="padding-right: 0px">
                                                                <label>
                                                                    Country:</label>
                                                            </div>
                                                            <div class="col-md-6" style="padding-left: 0px">
                                                                <asp:DropDownList ID="drpCountry" runat="server" class="form-control drpControl">
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-md-1">
                                                                <asp:RequiredFieldValidator ID="reqc" runat="server" ControlToValidate="drpCountry"
                                                                    InitialValue="0" ValidationGroup="subvate" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-12" style="margin-top: 5px;">
                                                            <div class="col-md-4" style="padding-right: 0px">
                                                                <label>
                                                                    Projects:</label>
                                                            </div>
                                                            <div class="col-md-6" style="padding-left: 0px">
                                                                <asp:DropDownList ID="Drp_Project" runat="server" class="form-control drpControl"
                                                                    AutoPostBack="true" OnSelectedIndexChanged="Drp_Project_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-md-1">
                                                                <asp:RequiredFieldValidator ID="reqsubcategory" runat="server" ControlToValidate="Drp_Project"
                                                                    InitialValue="0" ValidationGroup="subvate" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="rows">
                                                        <div class="col-md-12" align="center" style="margin-top: 5px; margin-left: -10px;">
                                                            <asp:Button ID="btnAssigngroup" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                                                OnClick="btnAssigngroup_Click" ValidationGroup="subvate" />
                                                            <asp:Button ID="btnupdateassigngroup" Text="Update" runat="server" Visible="false"
                                                                CssClass="btn btn-primary btn-sm" OnClick="btnupdateassigngroup_Click" ValidationGroup="subvate" />
                                                            <asp:Button ID="btncancelassigngroup" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                                OnClick="btncancelassigngroup_Click" />
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
                                              

                                                   <div id="Div2" class="dropdown" runat="server" style="display: inline-flex"><h3 class="box-title">
               <asp:LinkButton runat="server" ID="lbAssignProjGroupsExport" OnClick="lbAssignProjGroupsExport_Click" ToolTip="Export to Excel Assign Project Groups"
                 Text="" CssClass="dropdown-item dropdown-toggle glyphicon glyphicon-download-alt" Style="color: darkblue;"></asp:LinkButton>
		      </h3>
            </div>



                                        </div>
                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div>
                                                    <div class="rows">
                                                        <div style="width: 100%; height: 264px; overflow: auto;">
                                                            <div>
                                                                <asp:GridView ID="grdAssignGroups" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdAssignGroups_RowCommand">
                                                                    <Columns>

                                                                          <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtnupdateAssignGroup" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                                    CommandName="EditASSIGNGROUP" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                              </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Project Name" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblProjname" runat="server" Text='<%# Bind("PROJNAME") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="User Group Name" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblusergroupname" runat="server" Text='<%# Bind("UserGroup_Name") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Country" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblcountry" runat="server" Text='<%# Bind("COUNTRY") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                          <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                  <asp:LinkButton ID="lbtndeleteAssignGroup" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                                    CommandName="Delete1" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
            <Triggers>

                <asp:PostBackTrigger ControlID="lbAddGroupExport" />
                <asp:PostBackTrigger ControlID="lbAssignProjGroupsExport" />

            </Triggers>

        </asp:UpdatePanel>
    </div>
</asp:Content>
