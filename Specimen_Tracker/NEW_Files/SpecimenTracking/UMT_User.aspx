<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UMT_User.aspx.cs" Inherits="SpecimenTracking.UMT_User" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <link href="Style/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <script src="Scripts/btnSave_Required.js"></script>
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
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">Manage Internal Users</h1>
                    </div>
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home </a></li>
                            <li class="breadcrumb-item active">Manage Internal Users</li>
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
                                <h3 class="card-title">Create Internal Users</h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="lbtnExport" runat="server" Font-Size="14px" Style="margin-top: 3px;" CssClass="btn btn-default" OnClick="lblUserDetailsExport_Click" ForeColor="Black">Export Internal Users&nbsp;<span class="fas fa-download btn-xs"></span></asp:LinkButton>
                                    &nbsp;&nbsp; 
                                    <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                </div>
                            </div>
                            <div>
                                <asp:HiddenField ID="hdnID" runat="server" />
                            </div>
                            <div class="card-body">
                                <div class="rows">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>First Name : &nbsp;</label>
                                                    <asp:Label ID="Label5" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control required"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Last Name :&nbsp; </label>
                                                    <asp:Label ID="Label6" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control required"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="rows">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Email Id : &nbsp;</label>
                                                    <asp:Label ID="Label9" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtEmailid" CssClass="form-control noSpace required"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ForeColor="Red" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmailid" ErrorMessage="Invalid Email"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Contact No :&nbsp; </label>
                                                    <asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtContactNo" CssClass="form-control required numeric"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Blinded/Unblinded : &nbsp; </label>
                                                    <asp:Label ID="Label7" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                    <asp:DropDownList runat="server" ID="drpUnblind" CssClass="form-control required">
                                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                        <asp:ListItem Value="Blinded" Text="Blinded"></asp:ListItem>
                                                        <asp:ListItem Value="Unblinded" Text="Unblinded"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Study Role :&nbsp;</label>
                                                    <asp:Label ID="Label14" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                    <asp:DropDownList ID="drpStudyRole" runat="server" AutoPostBack="true" CssClass="form-control required">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>TimeZone :&nbsp; </label>
                                                    <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                    <asp:DropDownList ID="ddlTimeZone" runat="server" CssClass="form-control drpControl required select" SelectionMode="Single">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Notes (If any):</label>
                                                    <asp:TextBox runat="server" ID="txtNotes" CssClass="form-control" TextMode="MultiLine" MaxLength="200"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <label>Select Systems & Privileges: </label>
                                            </div>
                                            <div class="col-md-9">
                                                <asp:UpdatePanel runat="server" ID="upnlSystems">
                                                    <ContentTemplate>
                                                        <table class="table table-bordered table-striped">
                                                            <tr>
                                                                <th class="col-md-4">Systems
                                                                </th>
                                                                <th class="col-md-4 d-none">Privileges
                                                                </th>
                                                                <th class="col-md-4">Notes (If any)
                                                                </th>
                                                            </tr>
                                                            <asp:Repeater runat="server" ID="repeatSystem" OnItemDataBound="repeatSystem_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td class="col-md-4">
                                                                            <asp:CheckBox ID="ChkSelect" runat="server" OnCheckedChanged="ChkSelect_CheckedChanged" AutoPostBack="true" />
                                                                            &nbsp;
                                                                            <asp:Label ID="lblSystemName" runat="server" Text='<%# Bind("SystemName") %>'></asp:Label>
                                                                            <asp:Label ID="lblSystemID" runat="server" Text='<%# Bind("SystemID") %>' Visible="false"></asp:Label>
                                                                            <asp:HiddenField runat="server" ID="HiddenSubSytem" Value='<%# Eval("SubSystem") %>' />
                                                                        </td>
                                                                        <td class="col-md-4 d-none">
                                                                            
                                                                        </td>
                                                                        <td class="col-md-4">
                                                                            <asp:TextBox runat="server" ID="txtSystemNotes" CssClass="form-control" Width="100%" TextMode="MultiLine" MaxLength="200"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <center>
                                    <asp:LinkButton runat="server" ID="lbtnSubmit" Text="Submit" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnSubmit_Click"></asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton runat="server" ID="lbnUpdate" Text="Update" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbnUpdate_Click" Visible="false"></asp:LinkButton>&nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton runat="server" ID="lbtnCancel" Text="Cancel" ForeColor="White" CssClass="btn btn-primary btn-sm" OnClick="lbtnCancel_Click"></asp:LinkButton>
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
                                    <div style="width: 100%; overflow: auto;">
                                        <div>
                                            <asp:GridView ID="grdUser" runat="server" AllowSorting="True" AutoGenerateColumns="false"
                                                CssClass="table table-bordered table-striped Datatable" OnPreRender="grdUserDetails_PreRender" OnRowCommand="grdUser_RowCommand" OnRowDataBound="grdUser_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none"
                                                        HeaderText="ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtedituser" runat="server" CommandArgument='<%# Bind("ID") %>' CssClass="btn-info btn-sm"
                                                                CommandName="EIDIT" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="User ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUserID" runat="server" Text='<%# Bind("UserID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText=" Name ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFirstName" runat="server" Text='<%# Eval("Fname") +" "+ Eval("Lname") %>'></asp:Label>
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
                                                    <asp:TemplateField HeaderText="Blinded/Unblinded">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBlinded" runat="server" Text='<%# Bind("Blind") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Study Role">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStudyRole" runat="server" Text='<%# Bind("StudyRole") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TimeZone">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTimeZole" runat="server" Text='<%# Bind("Timezone") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Notes">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNotes" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail(this);" CssClass="btn-info btn-sm" ToolTip="Audit Trail"><i class="fa fa-history"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtdeleteuser" runat="server" CommandArgument='<%# Bind("ID") %>' CssClass="btn-danger btn-sm"
                                                                CommandName="DELETED" OnClientClick="return confirm(event);" ToolTip="Delete"><i class="fa fa-trash"></i></asp:LinkButton>
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
