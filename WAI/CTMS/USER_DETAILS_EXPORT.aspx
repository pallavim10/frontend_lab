<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="USER_DETAILS_EXPORT.aspx.cs" Inherits="CTMS.USER_DETAILS_EXPORT" %>

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
            <h3 class="box-title">
                User Data Exports</h3>
        </div>
        <div class="box-header">
            <div class="lblError">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-md-12">
                    <div class="label col-md-2 float-left">
                        Select List : &nbsp;
                    </div>
                    <div class="col-md-3 float-right">
                        <asp:DropDownList ID="ddlList" runat="server" AutoPostBack="True" class="form-control required drpControl width200px"
                            OnSelectedIndexChanged="ddlList_SelectedIndexChanged">
                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="User Access List" Value="USER_DETAILS_FUNCTION_EXPORT"></asp:ListItem>
                            <asp:ListItem Text="Module Wise User Access List" Value="MODULE_USER_DETAILS_RIGHTS_EXPORT"></asp:ListItem>
                            <asp:ListItem Text="User Rights" Value="USER_RIGHTS_EXPORT"></asp:ListItem>
                            <asp:ListItem Text="User Activations List" Value="User_Activations_List"></asp:ListItem>
                            <asp:ListItem Text="User DeActivations List" Value="User_DeActivations_List"></asp:ListItem>
                            <asp:ListItem Text="Product Details" Value="GET_TH_MASTER"></asp:ListItem>
                           <asp:ListItem Text="County Master" Value="GET_COUNTY_MASTER"></asp:ListItem>

                           <asp:ListItem Text="Sponsor Details" Value="GET_Sponsor"></asp:ListItem>

                           <asp:ListItem Text="Sponsor Team Members" Value="GET_SponsorTeamMember"></asp:ListItem>
                           <asp:ListItem Text="Project Details" Value="GET_PROJECTDETAILS"></asp:ListItem>
                           <asp:ListItem Text="Employee Master" Value="GET_EmployeeMaster"></asp:ListItem>

                           <asp:ListItem Text="Site Details" Value="GET_SITE"></asp:ListItem>
                           <asp:ListItem Text="Investigator Details" Value="GET_InvDetails"></asp:ListItem>
                           <asp:ListItem Text="Project Investigator" Value="GET_AssignProJInv"></asp:ListItem>
                           
                            <asp:ListItem Text="Investigator Team Member" Value="Add_INV_TeamMember"></asp:ListItem>
                            <asp:ListItem Text="Project Investigator Team Member" Value="Assign_Project_INV_TeamMember"></asp:ListItem>
                           <asp:ListItem Text="Ethics Committee Details" Value="Get_Ethics_Committee_Details"></asp:ListItem>
                           <asp:ListItem Text="Ethics Committee Team Member" Value="Get_Ethics_Committee_TeamMember"></asp:ListItem>
                      
                            <asp:ListItem Text="User Group Master" Value="Get_Group"></asp:ListItem>
                           <asp:ListItem Text="User Project Groups" Value="Assign_Group_Project"></asp:ListItem>

                            <asp:ListItem Text="User Group Functions" Value="Get_Assign_User_GroupRights"></asp:ListItem>
                            <asp:ListItem Text="User Functions" Value="Get_Assign_UserRights"></asp:ListItem>

                        </asp:DropDownList>
                    </div>
                    <div id="divModules" runat="server" visible="false">
                        <div class="label col-md-2">
                            Modules : &nbsp;
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlModules" runat="server" class="form-control required drpControl width200px">
                                <asp:ListItem Text="ALL" Value="ALL"></asp:ListItem>
                                <asp:ListItem Text="IWRS" Value="IWRS"></asp:ListItem>
                                <asp:ListItem Text="Data Management" Value="Data Management"></asp:ListItem>
                                <asp:ListItem Text="eSource" Value="eSource"></asp:ListItem>
                                <asp:ListItem Text="Pharmacovigilance" Value="Pharmacovigilance"></asp:ListItem>
                                <asp:ListItem Text="Medical Monitoring" Value="Medical Monitoring"></asp:ListItem>
                                <asp:ListItem Text="Sponsor" Value="Sponsor"></asp:ListItem>
                                <asp:ListItem Text="Business Development" Value="Business Development"></asp:ListItem>
                                <asp:ListItem Text="eTMF" Value="eTMF"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-md-12">
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-1">
                         <div id="Div2" class="dropdown" runat="server" style="display: inline-flex">                        
                             <asp:Button ID="btnGetData" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                            OnClick="btnGetData_Click" Text="Get Data" />

                             </div>

                    </div>
                    <div class="col-md-2">
                        <div id="Div1" class="dropdown" runat="server" style="display: inline-flex">
                            <a href="#" class="dropdown-toggle glyphicon glyphicon-download-alt" data-toggle="dropdown" style="color: #333333;font-size:21px;" title="Export"></a>
                            <ul class="dropdown-menu dropdown-menu-sm">
                                <li>
                                    <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" ToolTip="Export to Excel" Text="Export to Excel" CssClass="dropdown-item" Style="color: #333333;">
                                    </asp:LinkButton></li>
                                <hr style="margin: 5px;" />
                                <li>
                                    <asp:LinkButton runat="server" CssClass="dropdown-item" ID="btnPDF" OnClick="btnPDF_Click" ToolTip="Export to PDF" Text="Export to PDF" Style="color: #333333;">
                                    </asp:LinkButton></li>
                            </ul>
                        </div>
                    </div>


                </div>
            </div>
        </div>
        <br />
            
                        <div style="width: 100%; overflow: auto;">
                            <div>
        <asp:GridView ID="grdUserDetails" runat="server" AllowSorting="True" AutoGenerateColumns="true"
            CssClass="table table-bordered table-striped Datatable" OnPreRender="grdUserDetails_PreRender">
            <Columns>
            </Columns>
        </asp:GridView>
    </div> </div> </div>
</asp:Content>
