<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UMT_Request_List.aspx.cs" Inherits="SpecimenTracking.UMT_Request_List" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"/>
    <script src="Scripts/btnSave_Required.js" type="text/javascript"></script>
    <link type="text/css" rel="stylesheet" href="Style/Select2.css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>

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
                        <h1 class="m-0 text-dark">Users Requests Details</h1>
                    </div>
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home </a></li>
                            <li class="breadcrumb-item active">Users Requests Details</li>
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
                                <h3 class="card-title">Users Requests Details</h3>
                                <div class="pull-right">
                                    <asp:LinkButton runat="server" ID="lbUsersRequestsExport" Font-Size="14px" Style="margin-top: 3px;" OnClick="lbUsersRequestsExport_Click" CssClass="btn btn-default" ForeColor="Black">
			Export Site Users &nbsp;<span class="fas fa-download btn-xs"></span>
                                    </asp:LinkButton>&nbsp;&nbsp;
                                <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="rows">
                                    <div style="width: 100%; overflow: auto; height: auto">
                                      <div>
                        <asp:GridView ID="gvUserRequests" runat="server" AllowSorting="True" AutoGenerateColumns="false"
                            CssClass="table table-bordered table-striped Datatable1" OnPreRender="gvUserRequesr_PreRender" OnRowDataBound="gvUserRequests_RowDataBound" OnRowCommand="gvUserRequests_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none"
                                    HeaderText="ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserID" runat="server" Text='<%# Bind("UserID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Site ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SiteID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserName" runat="server" Text='<%#  Eval("Fname") +" "+ Eval("Lname") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Study Role">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStudyRole" runat="server" Text='<%# Bind("StudyRole") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("EmailID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContact" runat="server" Text='<%# Bind("ContactNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Request Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRequesttype" runat="server" Text='<%# Bind("REQ_DESC") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="text-left">
                                    <HeaderTemplate>
                                        <label>Request Details </label>
                                        <br />
                                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Requested By]</label><br />
                                        <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                        <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div runat="server">
                                            <div>
                                                <asp:Label ID="REQ_BYNAME" runat="server" Font-Bold="true" Text='<%# Bind("REQ_BYNAME") %>' ForeColor="Blue"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="REQ_CAL_DAT" runat="server" Text='<%# Bind("REQ_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="REQ_CAL_TZDAT" runat="server" Text='<%# Eval("REQ_CAL_TZDAT")+" , "+Eval("REQ_TZVAL") %>' ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                                        <asp:HiddenField ID="HiddenStatus" runat="server" Value='<%# Eval("REQ_STATUS") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnReqforActi" runat="server" CommandName="ACTIVATION" CommandArgument='<%# Bind("ID") %>'
                                            ToolTip="Request  for Activation"><i class="fa fa-toggle-on" style='color: blue;font-size:20px' ></i></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnReqForDactive" runat="server" CommandName="DEACTIVATION" CommandArgument='<%# Bind("ID") %>'
                                            ToolTip="Request for Deactivation"><i class="fa fa-toggle-off" style='color: blue;font-size:20px' ></i></asp:LinkButton>
                                        <asp:LinkButton ID="lbtUnlock" runat="server" CommandName="UNLOCK" CommandArgument='<%# Bind("ID") %>'
                                            ToolTip="Request for Unlock"><i class="fa fa-unlock" style='color: blue;font-size:20px'></i></i></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnGenReqPass" runat="server" CommandName="REQUEST_PASSWORD" CommandArgument='<%# Bind("ID") %>'
                                            ToolTip="Request reset Password"><i class="fa fa-envelope" style='color: blue;font-size:20px' ></i></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnGenReqSecQues" runat="server" CommandName="REQUEST_QUESTION" CommandArgument='<%# Bind("ID") %>'
                                            ToolTip="Request reset Security Question"><i class="fa fa-question-circle" style='color: blue;font-size:20px' ></i></asp:LinkButton>
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
