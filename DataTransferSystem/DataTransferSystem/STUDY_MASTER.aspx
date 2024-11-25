<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="STUDY_MASTER.aspx.cs" Inherits="DataTransferSystem.STUDY_MASTER" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script src="Scripts/btnSave_Required.js" type="text/javascript"></script>
    <style type="text/css">
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #0000ff;
        }

        .select2-container--default .select2-selection--single {
            margin-top: 2px;
        }
    </style>
    <script type="text/javascript">

        function showAuditTrail(element) {

            var ID = $(element).closest('tr').find('td').eq(0).text().trim();
            var TABLENAME = 'STUDY_MASTER';

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/STUDY_MASTER_showAuditTrail",
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
                        <h1 class="m-0 text-dark">Study Master</h1>
                    </div>
                    <!-- /.col -->
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home</a></li>
                            <li class="breadcrumb-item active"><a href="STUDY_MASTER.aspx">Study Master</a></li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
        <section class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-5">
                                <div class="card card-info">
                                    <div class="card-header">
                                        <h3 class="card-title">Define Study </h3>
                                        <div class="pull-right">
                                            <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <div class="rows">
                                            <div class="col-md-12">
                                                 <div class="row">
                                                     <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label for="txtStudyname">Study Name: &nbsp;</label>
                                                            <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300"  Text="*"></asp:Label>
                                                            <asp:TextBox ID="txtStudyname" runat="server" CssClass="form-control required numeric w-100" AutoCompleteType="Disabled" AutoPostBack="true"></asp:TextBox>
                                                            <%--onChange="CheckVisitNumber(this);" OnTextChanged="VisitNumChanged"--%>
                                                            <asp:HiddenField ID="hdnStudyName" runat="server" />
                                                            <div class="form-group has-warning">
                                                                <asp:Label ID="lblNumError" CssClass="text-danger font-weight-bold" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                   </div>
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label for="txtSponsorName">Sponsor Name: &nbsp;</label>
                                                            <asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="#FF3300"  Text="*"></asp:Label>
                                                            <asp:TextBox ID="txtSponsorName" runat="server" CssClass="form-control required numeric w-100" AutoCompleteType="Disabled" AutoPostBack="true"></asp:TextBox>
                                                            <%--onChange="CheckVisitNumber(this);" OnTextChanged="VisitNumChanged"--%>
                                                            <asp:HiddenField ID="hdnSponsorName" runat="server" />
                                                            <div class="form-group has-warning">
                                                                <asp:Label ID="lblErrorrMsg" CssClass="text-danger font-weight-bold" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                   </div>
                                                </div>
                                                 <div class="row">
                                                <div class="col-md-12">
                                                    <center>
                                                        <asp:LinkButton runat="server" ID="lbtnSubmit" Text="Submit" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnSubmit_Click"></asp:LinkButton>
                                                        &nbsp;&nbsp;&nbsp;
                                                                <asp:LinkButton runat="server" ID="lbtnUpdate" Text="Update" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" Visible="false" OnClick="lbtnUpdate_Click"></asp:LinkButton>
                                                        &nbsp;&nbsp;&nbsp;
                                                                <asp:LinkButton runat="server" ID="lbtnCancel" Text="Cancel" ForeColor="White" CssClass="btn btn-primary btn-sm" OnClick="lbtnCancel_Click"></asp:LinkButton>
                                                    </center>
                                                </div>
                                            </div>
                                            </div>                                           
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                            <div class="col-md-7">
                                <div class="card card-info">
                                    <div class="card-header">
                                        <h3 class="card-title">Records</h3>
                                        <div class="pull-right">
                                            <asp:LinkButton ID="lbtnExport" runat="server" Font-Size="14px" Style="margin-top: 3px;" CssClass="btn btn-default" ForeColor="Black">Export Study &nbsp;<span class="fas fa-download btn-sm"></span></asp:LinkButton>
                                            &nbsp;&nbsp;
                                                    <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <div class="rows">
                                            <div class="col-md-12">
                                                <div style="width: 100%; height: 607px; overflow: auto;">
                                                    <div>
                                                        <asp:GridView ID="GrdStudyDetails" runat="server" AllowSorting="True" AutoGenerateColumns="false" OnRowCommand="GrdStudyDetails_RowCommand" CssClass="table table-bordered table-striped Datatable1" OnPreRender="GrdStudyDetails_PreRender">
                                                            <Columns>
                                                        <asp:TemplateField HeaderText="STUDY_ID" HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSTUDYID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="text-center align-middle" ItemStyle-CssClass="text-center align-middle">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkedit" CommandName="EditStudydetails" CommandArgument='<%# Eval("ID") %>' runat="server" class="btn-info btn-sm" ><i class="fas fa-edit"></i></asp:LinkButton> <%--OnClick="lnkedit_Click"--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Study Name" HeaderStyle-CssClass="text-center align-middle" ItemStyle-CssClass="text-center align-middle">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSTUDYNAME" runat="server" Text='<%# Eval("STUDYNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sponsor Name" HeaderStyle-CssClass="text-center align-middle" ItemStyle-CssClass="text-center align-middle">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSPONSORNAME" runat="server" Text='<%# Eval("SPONSORNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Audit Trail" HeaderStyle-CssClass="text-center align-middle" ItemStyle-CssClass="text-center align-middle">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkaudit_trail" runat="server" class="btn-info btn-sm" OnClientClick="return showAuditTrail(this);" ToolTip="Audit Trail"  CommandArgument='<%# Eval("ID") %>'><i class="fas fa-history"></i></asp:LinkButton>
                                                                <%--CommandName="AuditTrailVisits"--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="text-center align-middle" ItemStyle-CssClass="text-center align-middle">
                                                            <ItemTemplate>

                                                                <asp:LinkButton ID="lnkdelete" runat="server" class="btn-danger btn-sm" CommandName="DeleteStudydetails" CommandArgument='<%# Eval("ID") %>'  OnClientClick="return confirmDelete(event);"><i class="fas fa-trash"></i></asp:LinkButton>
                                                               
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
        </section>
    </div>
    <script src="Scripts/btnSave_Required.js" type="text/javascript"></script>
    <script type="text/javascript">
        function confirmDelete(event) {
            event.preventDefault();

            swal({
                title: "Warning!",
                text: "Are you sure you want to Delete this Records?",
                icon: "warning",
                buttons: true,
                dangerMode: true
            }).then(function (isConfirm) {
                console.log("Confirmation result: " + isConfirm);
                if (isConfirm) {
                    // Get the original link button element
                    var linkButton = event.target;

                    // If the target is an icon inside the link button, get the parent link button
                    if (linkButton.tagName.toLowerCase() === 'i') {
                        linkButton = linkButton.parentElement;
                    }

                    // Trigger the link button's click event
                    linkButton.onclick = null; // Remove the onclick event handler to avoid recursion
                    linkButton.click();
                } else {
                    Response.redirect(this);
                }
            });

            return false;
        }
    </script>
</asp:Content>

