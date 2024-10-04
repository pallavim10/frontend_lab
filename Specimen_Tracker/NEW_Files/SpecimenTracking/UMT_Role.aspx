<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UMT_Role.aspx.cs" Inherits="SpecimenTracking.UMT_Role" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script src="Scripts/btnSave_Required.js"></script>
     <script type="text/javascript">
         function showAuditTrail(element) {

             var ID = $(element).closest('tr').find('td').eq(0).text().trim();
             var TABLENAME = 'UMT_Roles';

             $.ajax({
                 type: "POST",
                 url: "AjaxFunction.aspx/showAuditTrail",
                 data: JSON.stringify({ TABLENAME: TABLENAME, ID: ID }),
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (response) {
                     if (response.d === 'Object reference not set to an instance of an object.') {
                         alert("Session Expired");
                         var url = "SessionExpired.aspx";
                         $(location).attr('href', url);
                     } else {
                         $('#DivAuditTrail').html(response.d);
                         $('#modal-lg').modal('show'); // Show the modal after populating it
                     }
                 },
                 error: function (xhr, status, error) {
                     console.error('Error fetching audit trail:', status, error);
                     alert("An error occurred. Please contact the administrator.");
                 }
             });

             return false;
         }

     </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">User Roles</h1>
                    </div>
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home </a></li>
                            <li class="breadcrumb-item active">Manage Roles</li>
                            <li class="breadcrumb-item active">User Roles</li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
        <section class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">User Roles</h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="lbtnRoleExport" runat="server" Font-Size="14px" Style="margin-top: 3px;" CssClass="btn btn-default" OnClick="lbUserDetailsExport_Click" ForeColor="Black">Export User Roles&nbsp;<span class="fas fa-download btn-xs"></span></asp:LinkButton>
                                    &nbsp;&nbsp; 
                                    <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="col-sm-3">
                                                    <label>
                                                        Select System :</label>
                                                    <asp:Label ID="Label8" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:DropDownList ID="drpSystem" runat="server" AutoPostBack="true" class="form-control drpControl required width200px" OnSelectedIndexChanged="drpSystem_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <div id="DivROLE" runat="server" visible="false">
                                                        <label>Enter Role Name :</label>
                                                        <asp:Label ID="Label9" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                        <asp:TextBox runat="server" ID="txtRoleName" CssClass="form-control required width200px"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-6" id="divBlinded" runat="server" visible="false">
                                                <div class="col-sm-3">
                                                    <label>Unblinded :&nbsp;</label>
                                                    <asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:DropDownList runat="server" ID="ddlUnblind" CssClass="form-control drpControl required width200px">
                                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                        <asp:ListItem Value="Blinded" Text="Blinded"></asp:ListItem>
                                                        <asp:ListItem Value="Unblinded" Text="Unblinded"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-6" id="DiveSourceReadonly" runat="server" visible="false">
                                                <div class="col-sm-2">
                                                    <label>eSource :&nbsp;</label>
                                                </div>
                                                <div class="col-sm-4" style="display: inline-flex;">
                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" AutoPostBack="true" GroupName="eSource" ID="Check_CRA" OnCheckedChanged="Check_eSourceReadOnly_CheckedChanged" />
                                                    &nbsp;&nbsp;    
                            <label>SDR</label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" AutoPostBack="true" GroupName="eSource" ID="Check_eSourceReadOnly" OnCheckedChanged="Check_eSourceReadOnly_CheckedChanged" />
                                                    &nbsp;&nbsp;    
                            <label>ReadOnly</label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" GroupName="eSource" ID="Check_eSourceAdmin" />
                                                    &nbsp;&nbsp;    
                            <label>Admin</label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-6" id="DivMedicalAuth" runat="server" visible="false">
                                                <div class="col-sm-6">
                                                    <label>Medical Opinion :&nbsp;</label>
                                                </div>
                                                <div class="col-sm-6" style="display: inline-flex;">
                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkForm" />
                                                    &nbsp;&nbsp;
                                                        <label>Module </label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkfield" />
                                                    &nbsp;&nbsp;
                                                    <label>Field </label>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="col-sm-6" id="lblSignoff" runat="server" visible="false">
                                                    <label>SignOff Authority :&nbsp;</label>
                                                </div>
                                                <div class="col-sm-6" style="display: inline-flex;">
                                                    <div  id="divesource" runat="server" visible="false">
                                                        <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="Check_eSource" />
                                                        &nbsp;&nbsp;
                                                        <label>eSource</label>
                                                    </div>
                                                    <div  id="divPharmacovigilance" runat="server" visible="false">
                                                        <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="Check_Safety" />
                                                        &nbsp;&nbsp;
                                                        <label>Pharmacovigilance</label>
                                                    </div>
                                                    <div  id="diveCRF" runat="server" visible="false">
                                                        <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="Check_eCRF" />
                                                        &nbsp;&nbsp;
                                                        <label> eCRF</label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <center>
                                    <asp:Button ID="btnSUBMITRoles" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave" Text="Submit" OnClick="btnSUBMITRoles_Click" />
                                    &nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnUpdateRole" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave" Text="Update" OnClick="btnUpdateRole_Click" Visible="false" />&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnCancelROles" runat="server" CssClass="btn btn-primary btn-sm" Text="Cancel" OnClick="btnCancelROles_Click" />&nbsp;&nbsp;&nbsp;

                                </center>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </section>
        <br />
        <section class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Records</h3>
                                <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                            </div>
                            <div class="card-body">
                                <div class="rows">
                                    <div style="width: 100%; overflow: auto; height: auto">
                                        <div>
                                            <asp:GridView ID="grdRoles" runat="server" AllowSorting="True" AutoGenerateColumns="false"
                                                CssClass="table table-bordered table-striped Datatable1" OnPreRender="grdUserDetails_PreRender" OnRowCommand="grdRoles_RowCommand" OnRowDataBound="grdRoles_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none" HeaderText="ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbteditRole" runat="server" CommandArgument='<%# Bind("ID") %>' CssClass="btn-info btn-sm"
                                                                CommandName="EIDIT" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;&nbsp;
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none"
                                                        HeaderText="ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsystemID" runat="server" Text='<%# Bind("SystemID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="System Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSystemName" runat="server" Text='<%# Bind("SystemName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Role Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRoleName" runat="server" Text='<%# Bind("RoleName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Blinded/Unblinded">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBlined" runat="server" Text='<%# Bind("Blind") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail(this);" CssClass="btn-info btn-sm" ToolTip="Audit Trail"><i class="fa fa-history"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtdeleteRole" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                CommandName="DELETED" OnClientClick="return confirm(event);" CssClass="btn-danger btn-sm" ToolTip="Delete"><i class="fa fa-trash"></i></asp:LinkButton>
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
        </section>
    </div>
   
     <script type="text/javascript">

         function confirm(event) {
             event.preventDefault();

             swal({
                 title: "Warning!",
                 text: "Are you sure you want to delete this Record?",
                 icon: "warning",
                 buttons: true,
                 dangerMode: true
             }).then(function (isConfirm) {
                 if (isConfirm) {
                     var linkButton = event.target;
                     if (linkButton.tagName.toLowerCase() === 'i') {
                         linkButton = linkButton.parentElement;
                     }
                     linkButton.onclick = null;
                     linkButton.click();
                 } else {
                     Response.redirect(this);
                 }
             });
             return false;
         }

        
     </script>
</asp:Content>
