<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UMT_Manage_All_Users.aspx.cs" Inherits="SpecimenTracking.UMT_Manage_All_Users" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <style type="text/css">
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #0000ff;
        }

        .select2-container--default .select2-selection--single {
            margin-top: 2px;
        }
    </style>
    <script type="text/javascript">

        function pageLoad() {

            $(".Datatable1").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                stateSave: true,
                fixedHeader: true
            });

            $(".numeric").on("keypress keyup blur", function (event) {
                $(this).val($(this).val().replace(/[^\d].+/, ""));
                if ((event.which < 48 || event.which > 57)) {
                    event.preventDefault();
                }
            });

            //only for numeric value
            $('.numeric').keypress(function (event) {

                if (event.keyCode == 8 || event.keyCode == 9 || event.charCode == 48 || event.charCode == 49 || event.charCode == 50 || event.charCode == 51
                    || event.charCode == 52 || event.charCode == 52 || event.charCode == 53 || event.charCode == 54 || event.charCode == 55 || event.charCode == 56 || event.charCode == 57) {
                    // let it happen, don't do anything
                    return true;
                }
                else {
                    event.preventDefault();
                }
            });

            $('.select').select2();
        }

        function showAuditTrail(element) {

            var ID = $(element).closest('tr').find('td').eq(0).text().trim();
            var TABLENAME = 'UMT_Users';

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
    <script type="text/javascript">
        $(document).ready(function () {
            $('.noSpace').keypress(function (e) {
                if (e.which === 32) {
                    return false;
                }
            });
        });
    </script>
    <style type="text/css">
        .Background {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .Popup1 {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            min-width: 500px;
            max-width: 650px;

        }

        h5 {
            background-color: #007bff;
            padding: 0.4em 1em;
            margin-top: 0px;
            font-weight: bold;
            color: white;
        }

        .modal-body {
            position: relative;
            padding: 0px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">Manage All Users</h1>
                    </div>
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home </a></li>
                            <li class="breadcrumb-item active">Manage Users</li>
                            <li class="breadcrumb-item active">Manage All Users</li>
                        </ol>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="form-group">
                            <div class="form-group has-warning">
                                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                            </div>
                        </div>
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
                                <h3 class="card-title">Users Details</h3>
                                <div class="pull-right">
                                    <asp:LinkButton runat="server" Font-Size="14px" CssClass="btn btn-default" ForeColor="Black" ID="lblUserDetailsExport" ToolTip="Export to Excel" OnClick="lblUserDetailsExport_Click"> Export All Users &nbsp;<span class="fas fa-download btn-xs"></span>
                                    </asp:LinkButton>
                                    <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                </div>
                                
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12 d-flex">
                                        <div class="col-md-4">
                                            <div class="col-md-4">
                                                <label>
                                                    User Type:</label>
                                            </div>
                                            <div class="col-md-12">
                                                <asp:DropDownList ID="DrpUsertype" class="form-control drpControl  width200px"
                                                    runat="server" Style="margin-bottom: 1px">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="col-md-4">
                                                <label>
                                                    Status :</label>
                                            </div>
                                            <div class="col-md-12">
                                                <asp:DropDownList ID="DrpStatus" class="form-control drpControl  width200px"
                                                    runat="server" Style="margin-bottom: 1px">
                                                    <asp:ListItem Enabled="True" Text="--All--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Active" Value="Active"></asp:ListItem>
                                                    <asp:ListItem Text="Deactivated" Value="Deactivated"></asp:ListItem>
                                                    <asp:ListItem Text="Locked" Value="Locked"></asp:ListItem>
                                                    <asp:ListItem Text="Expired" Value="Expired"></asp:ListItem>
                                                    <asp:ListItem Text="Dormant" Value="Dormant"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="col-md-4">
                                                <label>
                                                    Study Role:</label>
                                            </div>
                                            <div class="col-md-12">
                                                <asp:DropDownList ID="DrpStudyROLE" class="form-control drpControl  width200px"
                                                    runat="server" Style="margin-bottom: 1px">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12 d-flex">
                                        <div class="col-md-4">
                                            <div class="col-md-4">
                                                <label>
                                                    Name :</label>
                                            </div>
                                            <div class="col-md-12">
                                                <asp:TextBox runat="server" ID="txtName" CssClass="form-control  width200px"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="col-md-4">
                                                <label>
                                                    Email ID :</label>
                                            </div>
                                            <div class="col-md-12">
                                                <asp:TextBox runat="server" ID="TxtEmail" CssClass="form-control  width200px"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="col-md-4">
                                                <label>
                                                    Contact No. :</label>
                                            </div>
                                            <div class="col-md-12">
                                                <asp:TextBox runat="server" ID="txtContact" CssClass="form-control  width200px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12 d-flex justify-content-center ">
                                        <div class="row txt_center ">
                                            <asp:LinkButton runat="server" ID="lbtnGetDetails" Text="Get Data" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnGetDetails_Click"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
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
                    <asp:GridView ID="grdUsers" runat="server" AllowSorting="True" AutoGenerateColumns="false"
                        CssClass="table table-bordered table-striped Datatable1" OnPreRender="grdUsers_PreRender" OnRowDataBound="grdUsers_RowDataBound" OnRowCommand="grdUsers_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                                HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblType" runat="server" Text='<%# Bind("UserType") %>'></asp:Label>
                                    <asp:HiddenField ID="HiddenActive" runat="server" Value='<%# Eval("ACTIVE") %>' />
                                    <asp:HiddenField ID="HiddenLOCK" runat="server" Value='<%# Eval("LOCK") %>' />
                                    <asp:HiddenField ID="HiddenSECURITY" runat="server" Value='<%# Eval("SECURITY") %>' />
                                    <asp:HiddenField ID="ACTIVATION_Pending" runat="server" Value='<%# Eval("ACTIVATION_Pending") %>' />
                                    <asp:HiddenField ID="DEACTIVATION_Pending" runat="server" Value='<%# Eval("DEACTIVATION_Pending") %>' />
                                    <asp:HiddenField ID="UNLOCK_Pending" runat="server" Value='<%# Eval("UNLOCK_Pending") %>' />
                                    <asp:HiddenField ID="RESET_PASSWORD_Pending" runat="server" Value='<%# Eval("RESET_PASSWORD_Pending") %>' />
                                    <asp:HiddenField ID="SECURITY_QUESTION_Pending" runat="server" Value='<%# Eval("SECURITY_QUESTION_Pending") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Site ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SiteID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblUSerId" runat="server" Text='<%# Bind("UserID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Fname")+" "+Eval("Lname") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Study Role ">
                                <ItemTemplate>
                                    <asp:Label ID="lblRole" runat="server" Text='<%# Bind("StudyRole") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("EmailID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contact No">
                                <ItemTemplate>
                                    <asp:Label ID="lblContact" runat="server" Text='<%# Bind("ContactNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TimeZone">
                                <ItemTemplate>
                                    <asp:Label ID="lblTimeZone" runat="server" Text='<%# Bind("Timezone") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Notes">
                                <ItemTemplate>
                                    <asp:Label ID="lblNotes" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User Roles" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <div class="dropdown">
                                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" style="color: white;">
                                            <i class="fa fa-user" style='color: #333; font-size: 20px'></i>
                                        </a>
                                        <ul class="dropdown-menu dropdown-menu-sm" style="text-align: left;">
                                            <li>
                                                <asp:LinkButton runat="server" ID="lbtnShow" ToolTip="Show Roles" CommandName="ShowRoles" CommandArgument='<%# Bind("UserID") %>'
                                                    Text="Show Roles" CssClass="dropdown-item" Style="color: blue;">
                                                </asp:LinkButton>
                                            </li>
                                            <hr style="margin: 5px;" />
                                            <li>
                                                <asp:LinkButton runat="server" ID="lbtnChange" ToolTip="Change Roles" CommandName="ChangeRoles" CommandArgument='<%# Bind("UserID") %>'
                                                    Text="Change Roles" CssClass="dropdown-item" Style="color: blue;">
                                                </asp:LinkButton>
                                            </li>
                                            <hr style="margin: 5px;" />
                                        </ul>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-left">
                                <HeaderTemplate>
                                    <label>Activation / Deactivation Details </label>
                                    <br />
                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Activated By]</label><br />
                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Deactivated By]</label><br />
                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div runat="server">
                                        <div>
                                            <asp:Label ID="ACTIVEBYNAME" runat="server" Enabled="false" Font-Bold="true" Text='<%# Bind("ACTIVATEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="ACTIVE_CAL_DAT" runat="server" Text='<%# Bind("ACTIVATED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="ACTIVE_CAL_TZDAT" runat="server" Text='<%# Eval("ACTIVATED_CAL_TZDAT")+" "+Eval("ACTIVATED_TZVAL") %>' ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                    <div runat="server">
                                        <div>
                                            <asp:Label ID="DEACTIVEBYNAME" runat="server" Enabled="false" Font-Bold="true" Text='<%# Bind("DEACTIVATEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="DEACTIVE_CAL_DAT" runat="server" Text='<%# Bind("DEACTIVATED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="DEACTIVE_CAL_TZDAT" runat="server" Text='<%# Eval("DEACTIVATED_CAL_TZDAT")+" "+Eval("DEACTIVATED_TZVAL") %>' ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Activation/Deactivation" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtActive" runat="server" CommandArgument='<%# Bind("ID") %>'
                                        CommandName="DEACTIVATION" ToolTip="Deactivate"><i class="fa fa-toggle-on" style='color: #333;font-size: 20px' ></i></asp:LinkButton>
                                    <asp:LinkButton ID="lbtDeactive" runat="server" CommandArgument='<%# Bind("ID") %>'
                                        CommandName="ACTIVATION" ToolTip="Activate"><i class="fa fa-toggle-off" style='color: #333;font-size: 20px' ></i></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnReqforActi" runat="server" CommandName="ACTIVATION" CommandArgument='<%# Bind("ID") %>'
                                        ToolTip="Request  for Activation"><i class="fa fa-toggle-on" style='color: blue;font-size: 20px' ></i></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnReqForDactive" runat="server" CommandName="DEACTIVATION" CommandArgument='<%# Bind("ID") %>'
                                        ToolTip="Request for Deactivation"><i class="fa fa-toggle-off" style='color: blue;font-size: 20px' ></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <label>Last Login Details </label>
                                    <br />
                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div runat="server">
                                        <div>
                                            <asp:Label ID="LASTLOGIN_CAL_DAT" runat="server" Text='<%# Bind("LASTLOGIN_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="LASTLOGIN_CAL_TZDAT" runat="server" Text='<%# Eval("LASTLOGIN_CAL_TZDAT")+" "+Eval("LASTLOGIN_TZVAL") %>' ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Lock/Unlock" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtLock" runat="server" CommandArgument='<%# Bind("ID") %>'
                                        CommandName="LOCK" ToolTip="Lock"><i class="fa fa-lock" style='color: #333;font-size: 20px' ></i></asp:LinkButton>
                                    <asp:LinkButton ID="lbtUnlock" runat="server" Enabled="false" ToolTip="Unlock"><i class="fa fa-unlock" style='color: #333;font-size: 20px'></i></i></asp:LinkButton>
                                    <asp:LinkButton ID="LbtnUNLOCK" runat="server" CommandName="LOCK" CommandArgument='<%# Bind("ID") %>'
                                        ToolTip="Request for Unlock"><i class="fa fa-unlock" style='color: blue;font-size: 20px'></i></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Resend Password" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbresendPassword" runat="server" CommandArgument='<%# Bind("ID") %>'
                                        CommandName="Resend_Password" ToolTip="Resend Password"><i class="fa fa-envelope" style='color: #333;font-size: 20px' ></i></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnGenReqPass" runat="server" CommandName="Resend_Password" CommandArgument='<%# Bind("ID") %>'
                                        ToolTip="Request reset Password"><i class="fa fa-envelope" style='color: blue;font-size: 20px' ></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reset Security Question" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblResetQuestion" runat="server" CommandArgument='<%# Bind("ID") %>'
                                        CommandName="ReSet_Question" ToolTip="Reset Security Question"><i class="fa fa-question-circle" style='color: #333;font-size: 20px' ></i></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnGenReqSecQues" runat="server" CommandName="ReSet_Question" CommandArgument='<%# Bind("ID") %>'
                                        ToolTip="Request reset Security Question"><i class="fa fa-question-circle" style='color: blue;font-size: 20px' ></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail(this);" CssClass="btn-info btn-sm" ToolTip="Audit Trail"><i class="fa fa-history"></i></asp:LinkButton>
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
    <cc1:ModalPopupExtender ID="modalRoles" runat="server" PopupControlID="pnlRoles" TargetControlID="btnRoles"
        BackgroundCssClass="Background">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlRoles" runat="server" Style="display: none;" CssClass="Popup1">
        <asp:Button runat="server" ID="btnRoles" Style="display: none" />
        <h5 class="heading">User Roles</h5>
        <div class="txtCenter" runat="server" style="padding: 10px;">
            <div class="txtCenter">
                <div style="text-align: center; height: auto;">
                    <asp:GridView ID="grdUserRoles" runat="server" AutoGenerateColumns="true"
                        CssClass="table table-bordered table-striped">
                    </asp:GridView>
                </div> 
                <br />
                <div class="row">
                    <div class="col-md-12 txt_center">
                        <asp:Button ID="btnOK" runat="server" CssClass="btn btn-success btn-sm" Text="OK" />
                    </div>
                </div>
                <br />
            </div>
        </div>
    </asp:Panel>
</asp:Content>

