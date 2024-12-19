<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ACKNOWLEDGEMENT.aspx.cs" Inherits="SpecimenTracking.ACKNOWLEDGEMENT" %>

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
                        <h1 class="m-0 text-dark">Acknowledge</h1>
                    </div>
                    <!-- /.col -->
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home</a></li>
                            <li class="breadcrumb-item"><a href="AnalyzingLaboratoryDashboard.aspx">Analyzing Laboratory</a></li>
                            <li class="breadcrumb-item active">Acknowledge</li>
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
                                <h3 class="card-title">Acknowledge</h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="lbtnExport" runat="server" Font-Size="14px" Style="margin-top: 3px;" CssClass="btn btn-default btn-sm" OnClick="lbtnExport_Click" ForeColor="Black">Export Acknowledge Details &nbsp;<span class="fas fa-download btn-sm"></span></asp:LinkButton>
                                    &nbsp;&nbsp;
                                    <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="rows">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Select Site ID : &nbsp;</label>
                                                    <asp:DropDownList ID="drpSite" runat="server" AutoPostBack="true" class="form-control drpControl w-100 required select2" SelectionMode="Single" OnSelectedIndexChanged="drpSite_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Select <asp:Label runat="server" ID="lblSUBSCRID" Text=''></asp:Label> : &nbsp;</label>
                                                    <asp:DropDownList ID="drpSubject" runat="server" AutoPostBack="false" class="form-control drpControl w-100 required select2" SelectionMode="Single">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4" id="divSID" runat="server">
                                                <div class="form-group">
                                                    <label>Enter Specimen ID : &nbsp;</label>
                                                    <asp:TextBox ID="txtSpecimenID" runat="server" CssClass="form-control required numeric w-100"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Select From Date : &nbsp;</label>
                                                    <asp:TextBox ID="txtFROMDAT" runat="server" class="form-control txtDate w-100 required">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Select To Date : &nbsp;</label>
                                                    <asp:TextBox ID="txtTODAT" runat="server" class="form-control txtDate w-100 required"></asp:TextBox>
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
                                                            <asp:Label ID="lblID" Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                                                            <asp:HiddenField ID="HdnAcknowledge" runat="server" Value='<%# Eval("Acknowledge") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Acknowledge" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="drpAcknowledge" runat="server" CssClass="form-control w-100" AutoPostBack="false">
                                                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="align-left">
                                                        <HeaderTemplate>
                                                            <label>Entered Details</label><br />
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
                                        <asp:Button ID="btnSubmit" CssClass="btn btn-primary" Text="Submit" Visible="false" runat="server" OnClick="btnSubmit_Click" />

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="modal-lg" tabindex="-1" role="dialog" aria-labelledby="modal-lg-label" aria-hidden="true">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content" style="width: 66%;">
                        <div class="modal-header" style="background-color: gold;">
                            <h4 class="modal-title" id="modal-lg-label">Aliquot Details</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span>
                            </button>
                        </div>
                        <div class="modal-body txt_center" id="DivAliquotDetails">
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

