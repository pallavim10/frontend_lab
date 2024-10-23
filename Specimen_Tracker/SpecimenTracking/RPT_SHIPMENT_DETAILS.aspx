<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RPT_SHIPMENT_DETAILS.aspx.cs" Inherits="SpecimenTracking.RPT_SHIPMENT_DETAILS" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">Shipment Details</h1>
                    </div>
                    <!-- /.col -->
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home</a></li>
                            <li class="breadcrumb-item"><a href="ReportAndListingDashboard.aspx">Report And Listing</a></li>
                            <li class="breadcrumb-item active">Shipment Details</li>
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
                                <h3 class="card-title">Shipment Details</h3>
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
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Select Site ID : &nbsp;</label>
                                                    <asp:DropDownList ID="drpSite" runat="server" AutoPostBack="false" class="form-control drpControl select2" SelectionMode="Single">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>From Date : &nbsp; </label>
                                                    <asp:TextBox runat="server" ID="txtDateFrom" CssClass="form-control  txtDate" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>To Date :&nbsp;</label>
                                                    <asp:TextBox runat="server" ID="txtDateTo" CssClass="form-control txtDate" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3 col-md-3 align-content-center">
                                                <div class="d-inline-flex align-items-center">
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
                <div class="row" runat="server" id="divRecord" visible="false">
                    <div class="col-md-12">
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Records</h3>
                                <div class="pull-right">
                                    <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="rows">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div style="width: 100%; height: 700px; overflow: auto;">
                                                <div>
                                                    <asp:GridView ID="grd_Data" runat="server" AutoGenerateColumns="false"
                                                        CssClass="table table-bordered table-striped Datatable" Width="100%" OnPreRender="grd_Data_PreRender" OnRowDataBound="grd_Data_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Site ID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSITEID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Specimen Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSPECTYP" runat="server" Text='<%# Bind("SPECTYP") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Visit Number">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVISITNUM" runat="server" Text='<%# Bind("VISITNUM") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Visit Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVISIT" runat="server" Text='<%# Bind("VISIT") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Aliquot Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblALIQUOTTYPE" runat="server" Text='<%# Bind("ALIQUOTTYPE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="From Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl" runat="server" Text='<%# Bind("FROMDAT") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="To Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTODAT" runat="server" Text='<%# Bind("TODAT") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Specimen ID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTOTAL_SID" runat="server" Text='<%# Bind("TOTAL_SID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Aliquot ID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTOTAL_ALQ" runat="server" Text='<%# Bind("TOTAL_ALQ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="align-left">
                                                                <HeaderTemplate>
                                                                    <label>Shipment Generated Details</label><br />
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
                                                            <asp:TemplateField HeaderText="Shipment Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSHIPMENTDAT" runat="server" Text='<%# Bind("SHIPMENTDAT") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Air Waybill Number">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAWBNUM" runat="server" Text='<%# Bind("AWBNUM") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="align-left">
                                                                <HeaderTemplate>
                                                                    <label>Shipment Shipped Details</label><br />
                                                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Shipped By]</label><br />
                                                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <div>
                                                                            <asp:Label ID="SHIPBYNAME" runat="server" Text='<%#Bind("SHIPBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                                        </div>
                                                                        <div>
                                                                            <asp:Label ID="SHIP_CAL_DAT" runat="server" Text='<%#Bind("SHIP_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                                        </div>
                                                                        <div>
                                                                            <asp:Label ID="SHIP_CAL_TZDAT" runat="server" Text='<%# Eval("SHIP_CAL_TZDAT") + " " + Eval("SHIP_TZVAL") %>' ForeColor="Red"></asp:Label>
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
                </div>
            </div>
        </section>
    </div>
</asp:Content>
