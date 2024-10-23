<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SHIPMENT_DETAILS.aspx.cs" Inherits="SpecimenTracking.SHIPMENT_DETAILS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
                        <h1 class="m-0 text-dark">Enter Shipment Details</h1>
                    </div>
                    <!-- /.col -->
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home</a></li>
                            <li class="breadcrumb-item"><a href="ShipmentManifestDashboard.aspx">Shipment Manifest</a></li>
                            <li class="breadcrumb-item active">Enter Shipment Details</li>
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
                                <h3 class="card-title">Enter Shipment Details</h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="lbtnExport" runat="server" Font-Size="14px" Style="margin-top: 3px;" CssClass="btn btn-default btn-sm" OnClick="lbtnExport_Click" ForeColor="Black">Export Shipment Details &nbsp;<span class="fas fa-download btn-sm"></span></asp:LinkButton>
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
                                                    <asp:DropDownList ID="drpSite" runat="server" class="form-control drpControl w-75 required select2" SelectionMode="Single">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Select Visit : &nbsp;</label>
                                                    <asp:DropDownList ID="drpVISIT" runat="server" class="form-control drpControl w-75 required">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Select Aliquot Type : &nbsp;</label>
                                                    <asp:DropDownList ID="drpALIQUOTTYPE" runat="server" class="form-control drpControl w-75 required">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Select From Date : &nbsp;</label>
                                                    <asp:TextBox ID="txtFROMDAT" runat="server" class="form-control txtDate w-75 required">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Select To Date : &nbsp;</label>
                                                    <asp:TextBox ID="txtTODAT" runat="server" class="form-control txtDate w-75 required">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <br />
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
        </section>
        <section>
            <div class="container-fluid">
                <div id="divRecord" runat="server" visible="false">
                    <div class="col-md-12">
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Shipments</h3>
                                <div class="pull-right">
                                    <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div style="width: 100%; overflow: auto;">
                                        <div>
                                            <asp:GridView ID="Grid_Data" runat="server" AllowSorting="True" AutoGenerateColumns="false"
                                                CssClass="table table-bordered table-striped Datatable1" OnPreRender="GridData_PreRender" OnRowCommand="Grid_Data_RowCommand" OnRowDataBound="Grid_Data_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none"
                                                        HeaderText="ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ship" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtEdit" runat="server" class="btn-info btn-sm" CommandArgument='<%# Bind("ID") %>'
                                                                CommandName="SHIP" ToolTip="Ship">
                                                                <i class="fa fa-truck"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Show Data" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtSHOW" runat="server" class="btn-info btn-sm" CommandArgument='<%# Bind("ID") %>'
                                                                CommandName="SHOW" ToolTip="Show">
                                                                <i class="fa fa-search"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Site ID" HeaderStyle-CssClass="text-center  align-middle" ItemStyle-CssClass="text-center align-middle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsiteid" runat="server" Text='<%#Eval("SITEID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Visit" HeaderStyle-CssClass="text-center  align-middle" ItemStyle-CssClass="text-center align-middle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVisit" runat="server" Text='<%#Eval("VISIT")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Specimen Type" HeaderStyle-CssClass="text-center  align-middle" ItemStyle-CssClass="text-center align-middle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSPECTYP" runat="server" Text='<%#Eval("SPECTYP")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Lab Name" HeaderStyle-CssClass="text-center  align-middle" ItemStyle-CssClass="text-center align-middle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLABNAME" runat="server" Text='<%#Eval("LABNAME")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Aliquot Type" HeaderStyle-CssClass="text-center  align-middle" ItemStyle-CssClass="text-center align-middle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblALIQUOTTYPE" runat="server" Text='<%#Eval("ALIQUOTTYPE")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Aliquot ID" HeaderStyle-CssClass="text-center  align-middle" ItemStyle-CssClass="text-center align-middle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblALIQUOTID" runat="server" Text='<%#Eval("ALIQUOTID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="From Date" HeaderStyle-CssClass="text-center  align-middle" ItemStyle-CssClass="text-center align-middle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFROMDAT" runat="server" Text='<%#Eval("FROMDAT")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="To Date" HeaderStyle-CssClass="text-center  align-middle" ItemStyle-CssClass="text-center align-middle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTODAT" runat="server" Text='<%#Eval("TODAT")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total SIDs" HeaderStyle-CssClass="text-center  align-middle" ItemStyle-CssClass="text-center align-middle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTOTAL_SID" runat="server" Text='<%#Eval("TOTAL_SID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Aliquots" HeaderStyle-CssClass="text-center  align-middle" ItemStyle-CssClass="text-center align-middle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTOTAL_ALQ" runat="server" Text='<%#Eval("TOTAL_ALQ")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="align-left">
                                                        <HeaderTemplate>
                                                            <label>Shipment Generation Details</label><br />
                                                            <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Generated By]</label><br />
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
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <asp:Label ID="openModalShipment" runat="server" Text=""></asp:Label>
    <asp:Label ID="closeModalShipment" runat="server" Text=""></asp:Label>
    <cc1:ModalPopupExtender ID="ModalShipment" runat="server" BehaviorID="mpe" PopupControlID="pnlReason" TargetControlID="openModalShipment" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlReason" runat="server" Style="display: none;">
        <div class="modal fade show">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Enter Shipment Details</h4>
                        <asp:HiddenField runat="server" ID="hdnID" />
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-3">
                                    <label>Site ID:</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="modalSiteID" Text="" CssClass="form-control"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <label>Visit:</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="modalVisit" Text="" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-3">
                                    <label>Specimen Type:</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="modalSPECTYP" Text="" CssClass="form-control"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <label>Analyzing Lab:</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="modalLABNAME" Text="" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-3">
                                    <label>Aliquot Type:</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="modalALIQUOTTYPE" Text="" CssClass="form-control"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <label>Aliquot ID:</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="modalALIQUOTID" Text="" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-3">
                                    <label>From Date:</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="modalFROMDAT" Text="" CssClass="form-control"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <label>To Date:</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="modalTODAT" Text="" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-6 d-inline-flex" id="divSID" runat="server" visible="false">
                                    <div class="col-md-6">
                                        <label>No. of SIDs:</label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label runat="server" ID="modalSID" Text="" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <label>No. of Aliquots:</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="modalALQS" Text="" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-3">
                                    <label>Enter Shipment Date:</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox runat="server" ID="txtSHIPMENTDAT" Text="" CssClass="form-control txtDate required2"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label>Enter AWB No.:</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox runat="server" ID="txtAWBNUM" Text="" CssClass="form-control required2"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12 txt_center">
                                    <asp:Button runat="server" ID="btnSubmit" Text="Submit" CssClass="btn btn-success cls-btnSave2" OnClick="btnSubmit_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button runat="server" ID="btnCancel" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Label ID="openDATA" runat="server" Text=""></asp:Label>
    <cc1:ModalPopupExtender ID="modalDATA" runat="server" BehaviorID="mpe2" PopupControlID="pnlDATA" TargetControlID="openDATA" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlDATA" runat="server" Style="display: none;">
        <div class="modal fade show">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Specimen IDs and Aliquots</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <asp:LinkButton ID="lbtncloseDATA" runat="server" Text="x" OnClick="lbtncloseDATA_Click"></asp:LinkButton>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <div style="width: 100%; height: 100%; max-height: 500px; overflow: auto;">
                                <div>
                                    <asp:GridView ID="grdDATA" runat="server" AutoGenerateColumns="true" CssClass="table table-bordered table-striped Datatable"
                                        Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnPreRender="GridData_PreRender" OnRowDataBound="grdDATA_RowDataBound">
                                        <Columns>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
