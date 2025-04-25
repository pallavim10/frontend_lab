<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User_Logs_Export.aspx.cs" Inherits="CTMS.User_Logs_Export" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <script type="text/javascript">
        function pageLoad() {

            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                stateSave: false
            });

        }
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="box box-primary">
        <div class="box-header">
            <h3 class="box-title">User Data Exports</h3>
        </div>
        <div class="box-header">
            <div class="lblError">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
     <%--   <div class="form-group">
            <div class="row">
                <div class="col-md-12">
                    <div class="label col-md-2">
                        Select List : &nbsp;
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlList" runat="server" class="form-control required drpControl width200px">
                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="User Details" Value="User_details"></asp:ListItem>
                            <asp:ListItem Text="User Group and Email Id" Value="User_Id"></asp:ListItem>
                            <asp:ListItem Text="User Rights" Value="User_Functions"></asp:ListItem>
                            <asp:ListItem Text="User Group Rights" Value="UserGroup_Functions"></asp:ListItem>
                            <asp:ListItem Text="Sponsor Master" Value="Sponsor_Master"></asp:ListItem>
                            <asp:ListItem Text="Sponsor Users" Value="Sponsor_Users"></asp:ListItem>
                            <asp:ListItem Text="Project Master" Value="Project_Master"></asp:ListItem>
                            <asp:ListItem Text="Employee Master" Value="Employee_Master"></asp:ListItem>
                            <asp:ListItem Text="Site Master" Value="Site_Master"></asp:ListItem>
                            <asp:ListItem Text="Investigator Master" Value="Investigator_Master"></asp:ListItem>
                            <asp:ListItem Text="Investigator Team Members" Value="Investigator_Team_Members"></asp:ListItem>
                            <asp:ListItem Text="Ethics Committee Master" Value="EC_Master"></asp:ListItem>
                            <asp:ListItem Text="Ethics Committee Users" Value="EC_Users"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-md-12">
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-1">
                        <asp:Button ID="btnGetData" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                            OnClick="btnGetData_Click" Text="Get Data" />
                    </div>
                    <div class="col-md-2">
                        <div id="Div1" class="dropdown" runat="server" style="display: inline-flex">
                            <a href="#" class="dropdown-toggle glyphicon glyphicon-download-alt" data-toggle="dropdown"
                                style="color: #333333" title="Export"></a>
                            <ul class="dropdown-menu dropdown-menu-sm">
                                <li>
                                    <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" ToolTip="Export to Excel"
                                        Text="Export to Excel" CssClass="dropdown-item" Style="color: #333333;">
                                    </asp:LinkButton></li>
                                <hr style="margin: 5px;" />
                                <li>
                                    <asp:LinkButton runat="server" CssClass="dropdown-item" ID="btnPDF" OnClick="btnPDF_Click"
                                        ToolTip="Export to PDF" Text="Export to PDF" Style="color: #333333;">
                                    </asp:LinkButton></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>

         
        </div>--%>
            <div class="box-body">
                    <div class="form-group">
                        <div style="display: inline-flex">
                            <label class="label ">
                               Select List :  
                            </label>
                            <div class="Control" style="display: inline-flex">
                            <asp:DropDownList ID="ddlList" runat="server" class="form-control required drpControl width200px">
                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="User Details" Value="User_details"></asp:ListItem>
                            <asp:ListItem Text="User Group and Email Id" Value="User_Id"></asp:ListItem>
                            <asp:ListItem Text="User Rights" Value="User_Functions"></asp:ListItem>
                            <asp:ListItem Text="User Group Rights" Value="UserGroup_Functions"></asp:ListItem>
                            <asp:ListItem Text="Sponsor Master" Value="Sponsor_Master"></asp:ListItem>
                            <asp:ListItem Text="Sponsor Users" Value="Sponsor_Users"></asp:ListItem>
                            <asp:ListItem Text="Project Master" Value="Project_Master"></asp:ListItem>
                            <asp:ListItem Text="Employee Master" Value="Employee_Master"></asp:ListItem>
                            <asp:ListItem Text="Site Master" Value="Site_Master"></asp:ListItem>
                            <asp:ListItem Text="Investigator Master" Value="Investigator_Master"></asp:ListItem>
                            <asp:ListItem Text="Investigator Team Members" Value="Investigator_Team_Members"></asp:ListItem>
                            <asp:ListItem Text="Ethics Committee Master" Value="EC_Master"></asp:ListItem>
                            <asp:ListItem Text="Ethics Committee Users" Value="EC_Users"></asp:ListItem>
                        </asp:DropDownList>
                            </div>
                        </div>
                        <div runat="server" id="Div3" style="display: inline-flex">
                            <div style="display: inline-flex">
                                 <asp:Button ID="btnGetData" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                            OnClick="btnGetData_Click" Text="Get Data" />
                            </div>
                        </div>
                        <div runat="server" id="Div4" style="display: inline-flex">
                            <div style="display: inline-flex">
                                &nbsp;&nbsp;
                            </div>
                        </div>
                        <div runat="server" id="Div5" style="display: inline-flex">
                            <div style="display: inline-flex">
                                <div id="Div1" class="dropdown" runat="server" style="display: inline-flex">
                                   <a href="#" class="dropdown-toggle glyphicon glyphicon-download-alt" data-toggle="dropdown"
                                style="color: #333333" title="Export"></a>
                                    <ul class="dropdown-menu dropdown-menu-sm">
                                        <li>
                                             <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" ToolTip="Export to Excel"
                                        Text="Export to Excel" CssClass="dropdown-item" Style="color: #333333;">
                                    </asp:LinkButton></li>
                                        <hr style="margin: 5px;" />
                                        <li>
                                            <asp:LinkButton runat="server" CssClass="dropdown-item" ID="btnPDF" OnClick="btnPDF_Click"
                                        ToolTip="Export to PDF" Text="Export to PDF" Style="color: #333333;">
                                    </asp:LinkButton></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        <br />
        <asp:GridView ID="grdUserDetails" runat="server" AllowSorting="True" AutoGenerateColumns="true"
            CssClass="table table-bordered table-striped Datatable" OnPreRender="grdUserDetails_PreRender">
            <Columns>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
