﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GENERATE_SHIPMENTMENIFEST.aspx.cs" Inherits="SpecimenTracking.GENERATE_SHIPMENTMENIFEST" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script src="Scripts/btnSave_Required.js"></script>
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
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">Generate Shipment</h1>
                    </div>
                    <!-- /.col -->
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home</a></li>
                            <li class="breadcrumb-item"><a href="ShipmentManifestDashboard.aspx">Shipment Manifest</a></li>
                            <li class="breadcrumb-item active">Generate Shipment Manifest</li>
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
                                <h3 class="card-title">Generate Shipment Manifest</h3>
                                <div class="pull-right">
                                    <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                </div>
                            </div>
                            <asp:Label runat="server" ID="lblError"></asp:Label>
                            <div class="card-body">
                                <div class="rows">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-4" runat="server" id="Div1">
                                                <div class="form-group">
                                                    <label>Select Site ID : &nbsp;</label>
                                                    <asp:Label ID="Label3" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                    <asp:DropDownList ID="drpsite" runat="server" AutoPostBack="true" CssClass="form-control required" OnSelectedIndexChanged="drpsite_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>
                                                        Select
                                                        <asp:Label runat="server" ID="lblSUBSCRID" Text=""></asp:Label>
                                                        : &nbsp;</label>
                                                    <asp:DropDownList ID="drpSubject" runat="server" AutoPostBack="false" class="form-control drpControl w-100 select2" SelectionMode="Single">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Visit : &nbsp;</label>
                                                    <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                    <asp:DropDownList ID="drpvisit" runat="server" CssClass="form-control required"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-4" runat="server" id="DivSpeciType" visible="false">
                                                <div class="form-group">
                                                    <label>Specimen Type : &nbsp;</label>
                                                    <asp:Label ID="Label4" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                    <asp:DropDownList ID="drpspecimentype" runat="server" CssClass="form-control required"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Aliquot Type : &nbsp;</label>
                                                    <asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                    <asp:DropDownList ID="drpAliquottype" runat="server" CssClass="form-control required" AutoPostBack="true" OnSelectedIndexChanged="drpAliquottype_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <label>Aliquot ID : &nbsp;</label>
                                                <asp:ListBox runat="server" ID="lstAliquotID" CssClass="form-control required select2" SelectionMode="Multiple" autocomplete="off"></asp:ListBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Analyzing Lab : &nbsp;</label>
                                                    <asp:Label ID="Label5" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                    <asp:DropDownList ID="drpLab" runat="server" CssClass="form-control required"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <label>From Date : &nbsp;</label>
                                                <asp:TextBox runat="server" ID="txtFormDate" CssClass="form-control txtDate " autocomplete="off"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <label>To Date : &nbsp;</label>
                                                <asp:TextBox runat="server" ID="txtToDate" CssClass="form-control txtDate " autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <center>
                                            <asp:LinkButton runat="server" ID="lbtnShowData" Text="Show Data" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnShowData_Click"></asp:LinkButton>
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:LinkButton runat="server" ID="lbtnSubmit" Text="Generate Shipment Manifest" ForeColor="White" CssClass="btn btn-success btn-sm cls-btnSave" OnClick="lbtnSubmit_Click"></asp:LinkButton>
                                            &nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton runat="server" ID="lbtnCancel" Text="Cancel" ForeColor="White" CssClass="btn btn-danger btn-sm" OnClick="lbtnCancel_Click"></asp:LinkButton>
                                        </center>
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
                                                Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnPreRender="Grid_Data_PreRender" OnRowDataBound="Grid_Data_RowDataBound">
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
            </div>
        </section>
    </div>
</asp:Content>
