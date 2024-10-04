<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage_Aliquats.aspx.cs" Inherits="SpecimenTracking.Manage_Aliquats" %>

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
            var TABLENAME = 'ALIQUOT_MASTER';

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/SETUP_showAuditTrail",
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
        function Check_SequenceNumber(element) {
            event.preventDefault();
            $(element).closest('td').find("input[id*='txtSeqtNo_Changed']").click();
            __doPostBack('<%= txtSeqtNo.ClientID %>', 'TextChanged');

        }

        function CheckAliquotID(element) {
            event.preventDefault();
            $(element).closest('td').find("input[id*='txtaliquotID_Changed']").click();
            __doPostBack('<%= txtaliquotID.ClientID %>', 'TextChanged');
        }

        function CheckAliquotNum(element) {
            event.preventDefault();
            $(element).closest('td').find("input[id*='txtaliquotnum_Changed']").click();
            __doPostBack('<%= txtaliquotnum.ClientID %>', 'TextChanged');
        }

        function Check_AliquotSEQALLOC(element) {
            event.preventDefault();
            $(element).closest('td').find("input[id*='allocatefrom_Changed']").click();
            __doPostBack('<%= allocatefrom.ClientID %>', 'TextChanged');
            $(element).closest('td').find("input[id*='allocateto_Changed']").click();
            __doPostBack('<%= allocateto.ClientID %>', 'TextChanged');

        }

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

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">Manage Aliquots</h1>
                    </div>
                    <!-- /.col -->
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home</a></li>
                            <li class="breadcrumb-item active">Manage Aliquots</li>
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
                                        <h3 class="card-title">Add Aliquot Details</h3>
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
                                                            <label>Enter Sequence Number : &nbsp;</label>
                                                            <asp:Label ID="Label4" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                            <asp:TextBox ID="txtSeqtNo" runat="server" CssClass="form-control required numeric w-100" AutoCompleteType="Disabled" AutoPostBack="true" OnTextChanged="Check_SequenceNumber" onChange="Check_SequenceNumber(this);"></asp:TextBox>
                                                            <asp:HiddenField runat="server" ID="hdnSeqno" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label>Enter Aliquot ID : &nbsp;</label>
                                                            <asp:Label ID="Label5" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                            <asp:TextBox ID="txtaliquotID" runat="server" CssClass="form-control required w-100" AutoCompleteType="Disabled" AutoPostBack="true" OnTextChanged="CheckAliquotID" onChange="CheckAliquotID(this);"></asp:TextBox>
                                                            <asp:HiddenField runat="server" ID="hdnAliquotID" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label>Enter Aliquot Type : &nbsp;</label>
                                                            <asp:Label ID="Label6" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                            <asp:TextBox ID="txtaliquottype" runat="server" CssClass="form-control required w-100" AutoCompleteType="Disabled"></asp:TextBox>
                                                            <asp:HiddenField runat="server" ID="hdnAliquotType" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label>Enter Aliquot Number : &nbsp;</label>
                                                            <asp:Label ID="Label7" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                            <asp:TextBox ID="txtaliquotnum" runat="server" CssClass="form-control required w-100" AutoCompleteType="Disabled" AutoPostBack="true" OnTextChanged="CheckAliquotNum" onChange="CheckAliquotNum(this);"></asp:TextBox>
                                                            <asp:HiddenField runat="server" ID="Hdnaliquotnum" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <asp:Label CssClass="font-weight-bold" Style="font-size: 14px;" runat="server">Sequence No. Allocation : &nbsp;</asp:Label>
                                                        <div class="form-group d-flex">
                                                            <div class="col-md-6">
                                                                <asp:Label ID="Label8" runat="server" Font-Size="Small" CssClass="font-weight-bold" ForeColor="Black">From :&nbsp;</asp:Label>
                                                                <asp:Label ID="Label10" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*" Visible="false"></asp:Label>
                                                                <asp:TextBox ID="allocatefrom" runat="server" CssClass="form-control numeric w-100" AutoCompleteType="Disabled" AutoPostBack="true" OnTextChanged="Check_AliquotSEQALLOC" onChange="Check_AliquotSEQALLOC(this);"></asp:TextBox>
                                                                <asp:HiddenField runat="server" ID="allocatefrm" />
                                                            </div>
                                                            &nbsp;&nbsp;                                         
                                                                <div class="col-md-6">
                                                                    <asp:Label ID="Label9" runat="server" Font-Size="Small" CssClass="font-weight-bold" ForeColor="Black">To : &nbsp;</asp:Label>
                                                                    <asp:Label ID="Label11" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*" Visible="false"></asp:Label>
                                                                    <asp:TextBox ID="allocateto" runat="server" CssClass="form-control numeric w-100" AutoCompleteType="Disabled" AutoPostBack="true" OnTextChanged="Check_AliquotSEQALLOC" onChange="Check_AliquotSEQALLOC(this);"></asp:TextBox>
                                                                    <asp:HiddenField runat="server" ID="allocatetwo" />
                                                                    <asp:Button runat="server" ID="btnDATA_Changed" CssClass="d-none"></asp:Button>
                                                                </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <center>
                                                            <asp:LinkButton runat="server" ID="lbtnSubmit" Text="Submit" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnSubmit_Click"></asp:LinkButton>
                                                            &nbsp;&nbsp;&nbsp;
                                                            <asp:LinkButton runat="server" ID="lbtnUpdate" Text="Update" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" Visible="false" OnClick="lbtnUpdate_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp;
                                                            <asp:LinkButton runat="server" ID="lbtnCancel" Text="Cancel" ForeColor="White" CssClass="btn btn-primary btn-sm" OnClick="lbtnCancel_Click"></asp:LinkButton>
                                                        </center>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-7">
                                <div class="card card-info">
                                    <div class="card-header">
                                        <h3 class="card-title">Records</h3>
                                        <div class="pull-right">
                                            <asp:LinkButton ID="lbtnExport" runat="server" Font-Size="14px" Style="margin-top: 3px;" CssClass="btn btn-default" OnClick="lbtnExport_Click" ForeColor="Black">Export Aliquot &nbsp;<span class="fas fa-download btn-xs"></span></asp:LinkButton>
                                            &nbsp;&nbsp;
                                            <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <div class="rows">
                                            <div class="col-md-12">
                                                <div style="width: 100%; height: 446px; overflow: auto;">
                                                    <div>
                                                        <asp:GridView ID="GrdAliquots" AutoGenerateColumns="false" runat="server" class="table table-bordered table-striped Datatable"  Width="100%" OnRowDataBound="GrdAliquot_RowDataBound" OnPreRender="GrdAliquots_PreRender">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="AliquotID" HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAliquotRecID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                        <asp:HiddenField ID="hfALIQUOTID" runat="server" Value='<%# Eval("ID") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="text-center align-middle" ItemStyle-CssClass="text-center align-middle">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkedit" runat="server" class="btn-info btn-sm" OnClick="lnkedit_Click"><i class="fas fa-edit"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Sequence No" HeaderStyle-CssClass="text-center align-middle" ItemStyle-CssClass="text-center align-middle">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSeqno" runat="server" Text='<%# Eval("SEQNO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Aliquot ID" HeaderStyle-CssClass="text-center align-middle" ItemStyle-CssClass="text-center align-middle">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAliquotID" runat="server" Text='<%# Eval("ALIQUOTID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Aliquot Type" HeaderStyle-CssClass="text-center align-middle" ItemStyle-CssClass="text-center align-middle">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAliquotType" runat="server" Text='<%# Eval("ALIQUOTTYPE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Aliquot Num" HeaderStyle-CssClass="text-center align-middle" ItemStyle-CssClass="text-center align-middle">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAliquotNum" runat="server" Text='<%# Eval("ALIQUOTNO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Aliquot Seq From" HeaderStyle-CssClass="text-center align-middle" ItemStyle-CssClass="text-center align-middle">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAliquotSeqFrom" runat="server" Text='<%# Eval("ALIQUOTSEQFROM") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Aliquot Seq To" HeaderStyle-CssClass="text-center align-middle" ItemStyle-CssClass="text-center align-middle">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAliquotSeqTo" runat="server" Text='<%# Eval("ALIQUOTSEQTO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Audit Trail" HeaderStyle-CssClass="text-center align-middle" ItemStyle-CssClass="text-center align-middle">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkaudit_trail" runat="server" class="btn-info btn-sm" ToolTip="Audit Trail" CommandArgument="Aliquot" OnClientClick="return showAuditTrail(this);"><i class="fas fa-history"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="text-center align-middle" ItemStyle-CssClass="text-center align-middle">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkdelete" runat="server" class="btn-danger btn-sm" OnClick="lnkDelete_Click" OnClientClick="return confirmDelete(event);"><i class="fas fa-trash"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </section>
        <section class="content">
            <div class="container-fluid">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="row">
                            <!-- left column -->
                            <div class="col-md-12">
                                <!-- Horizontal Form -->
                                <div class="card card-info">
                                    <div class="card-header">
                                        <h3 class="card-title" style="margin-bottom: 3px;">Mapping Visit</h3>
                                        <div class="pull-right">
                                            <asp:LinkButton ID="lbtnExportMapping" runat="server" Font-Size="14px" Style="margin-top: 3px;" CssClass="btn btn-default" OnClick="lbtnExportMapping_Click" ForeColor="Black">Export Aliquot Mapping &nbsp;<span class="fas fa-download btn-xs"></span></asp:LinkButton>
                                            &nbsp;&nbsp;
                                            <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <div class="form-group row m-2">
                                            <div class="col-md-2">
                                                <label>Select Visit: &nbsp;</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="dropdown_visits" runat="server" Width="100%" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="dropdown_visit_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row" id="MapAliquot" runat="server" style="margin-top: 10px;" visible="false">
                                            <div class="col-md-12 d-flex">
                                                <%-- <div class="row">--%>
                                                <div class="col-md-5 ">
                                                    <div class="card card-info">
                                                        <div class="card-header">
                                                            <h3 class="card-title" style="margin-bottom: 3px;">Add Aliquots</h3>
                                                            <div class="pull-right">
                                                                <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                                            </div>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="form-group row m-2">
                                                                <div class="col-md-12">
                                                                    <asp:GridView ID="grdviewAliquotVisit" runat="server" AutoGenerateColumns="False" class="table table-bordered table-striped responsive">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Select" HeaderStyle-Width="5%" ControlStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Aliquot" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:HiddenField runat="server" ID="hfID" Value='<%# Eval("ID") %>'></asp:HiddenField>
                                                                                    <asp:Label runat="server" ID="lblAliquotName" Text='<%# Eval("ALIQUOTID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%--  </div>--%>
                                                <div class="col-md-2">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <br />
                                                            <br />
                                                            <br />
                                                            <asp:Button ID="btn_Add" CssClass="btn btn-success btn-block" Text="Add" runat="server" OnClick="btn_Add_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <br />
                                                            <asp:Button ID="btn_Remove" CssClass="btn btn-danger btn-block" Text="Remove" runat="server" OnClick="btn_Remove_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <%-- <div class="row">--%>
                                                <div class="col-md-5">
                                                    <div class="card card-info">
                                                        <div class="card-header">
                                                            <h3 class="card-title" style="margin-bottom: 3px;">Added Aliquots</h3>
                                                            <div class="pull-right">
                                                                <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                                            </div>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="form-group row ">
                                                                <!--m-2-->
                                                                <div class="col-md-12">
                                                                    <asp:GridView ID="gridAddedAliquot" runat="server" AutoGenerateColumns="False" class="table table-bordered table-striped responsive">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Select" HeaderStyle-Width="5%" ControlStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Aliquots" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:HiddenField runat="server" ID="hfID" Value='<%# Eval("ID") %>'></asp:HiddenField>
                                                                                    <asp:Label runat="server" ID="lblAddedAliquot" Text='<%# Eval("ALIQUOTID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%-- </div>--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lbtnExport" />
                        <asp:PostBackTrigger ControlID="lbtnExportMapping" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </section>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
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

        });
        // Function to confirm deletion with SweetAlert
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
