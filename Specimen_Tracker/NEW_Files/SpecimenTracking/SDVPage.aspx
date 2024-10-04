<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SDVPage.aspx.cs" Inherits="SpecimenTracking.SDVPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <link href="Style/Select2.css" rel="stylesheet" type="text/css" />
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
            $('.select').select2();

        }

        function showAliquot(element) {
            var ID = $(element).closest('tr').find('td').eq(0).text().trim();

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/SHOW_ALIQUOT_DATA",
                data: JSON.stringify({ SID: ID }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d === 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    } else {
                        $('#DivAliquotDetails').html(response.d);
                        $('#modal-lg').modal('show');
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
                        <h1 class="m-0 text-dark">SDV</h1>
                    </div>
                    <!-- /.col -->
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home</a></li>
                            <li class="breadcrumb-item active">SDV Details</li>
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
                                <h3 class="card-title">SDV Details</h3>
                                <div class="pull-right">
                                    <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="rows">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Select Site ID : &nbsp;</label>
                                                    <asp:DropDownList ID="drpSite" runat="server" AutoPostBack="true" class="form-control drpControl w-75 required select2" SelectionMode="Single" OnSelectedIndexChanged="drpSite_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Select Subject ID : &nbsp;</label>
                                                    <asp:DropDownList ID="drpSubject" runat="server" AutoPostBack="false" class="form-control drpControl w-75 required select2" SelectionMode="Single">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Specimen ID : &nbsp;</label>
                                                    <asp:TextBox ID="txtSpecimenID" runat="server" CssClass="form-control required numeric w-75"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mt-4">
                                                    <asp:LinkButton runat="server" ID="lbtnGETDATA" Text="Get Data" ForeColor="White" CssClass="btn btn-primary btn-sm" OnClick="lbtnGETDATA_Click"></asp:LinkButton>
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
        <section class="content">
            <div class="container-fluid">
                <div class="row" id="divRecord" runat="server" visible="false">
                    <div class="col-md-12">
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Records</h3>
                                <div class="pull-right">
                                    <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div style="width: 100%; height: 100%; overflow: auto;">
                                        <div>
                                            <asp:GridView ID="Grid_Data" runat="server" AutoGenerateColumns="true" CssClass="table table-bordered table-striped Datatable"
                                                Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnPreRender="GridData_PreRender" OnRowDataBound="Grid_Data_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ID" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="txt_center d-none"
                                                        ItemStyle-CssClass="txt_center d-none">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" Text='<%# Eval("Specimen ID") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Select" HeaderStyle-Width="10%" ItemStyle-Width="10%" HeaderStyle-CssClass="txt_center"
                                                        ItemStyle-CssClass="txt_center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Aliquot Details" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnAliquot" runat="server" class="btn-success btn-sm" OnClientClick="return showAliquot(this);" ToolTip="Aliquot Details"><i class="fa fa fa-flask"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="align-left">
                                                        <HeaderTemplate>
                                                            <label>Entry Details</label><br />
                                                            <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Entered By]</label><br />
                                                            <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                            <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <div>
                                                                <div>
                                                                    <asp:Label ID="ENTEREDBYNAME" runat="server" Text='<%#Bind("ENTEREDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                                </div>
                                                                <div>
                                                                    <asp:Label ID="ENTERED_CAL_DAT" runat="server" Text='<%#Bind("ENTERED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                                </div>
                                                                <div>
                                                                    <asp:Label ID="ENTERED_CAL_TZDAT" runat="server" Text='<%# Eval("ENTERED_CAL_TZDAT") + " " + Eval("ENTERED_TZVAL") %>' ForeColor="Red"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-5">
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Button ID="btnSDV" CssClass="btn btn-primary" Text="Submit" Visible="false" runat="server" OnClick="btnSDV_Click" />

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="modal-lg" tabindex="-1" role="dialog" aria-labelledby="modal-lg-label" aria-hidden="true">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content" style="max-width: 65%; height: 500px;">
                        <div class="modal-header" style="background-color: gold;">
                            <h4 class="modal-title" id="modal-lg-label">Aliquot Details</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span>
                            </button>
                        </div>
                        <div style="height: 590px; overflow: auto;">
                            <div class="modal-body" id="DivAliquotDetails">
                            </div>
                        </div>
                        <div class="modal-footer pull-right">
                            <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
