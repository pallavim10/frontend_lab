<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RPT_ACKNOWLEDGEMENT.aspx.cs" Inherits="SpecimenTracking.RPT_ACKNOWLEDGEMENT" %>

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
                        <h1 class="m-0 text-dark">Specimen Status Report</h1>
                    </div>
                    <!-- /.col -->
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home</a></li>
                            <li class="breadcrumb-item"><a href="AnalyzingLaboratoryDashboard.aspx">Analyzing Laboratory</a></li>
                            <li class="breadcrumb-item active">Specimen Status Report</li>
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
                                <h3 class="card-title">Specimen Status Report</h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="lbtnExport" runat="server" Font-Size="14px" Style="margin-top: 3px;" CssClass="btn btn-default btn-sm" OnClick="lbtnExport_Click" ForeColor="Black">Export Acknowledgement Report &nbsp;<span class="fas fa-download btn-sm"></span></asp:LinkButton>
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
                                                    <asp:DropDownList ID="drpSite" runat="server" AutoPostBack="true" class="form-control drpControl w-75 select2" SelectionMode="Single" OnSelectedIndexChanged="drpSite_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Select Subject ID : &nbsp;</label>
                                                    <asp:DropDownList ID="drpSubject" runat="server" AutoPostBack="false" class="form-control drpControl w-75 select2" SelectionMode="Single">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4" id="divSID" runat="server">
                                                <div class="form-group">
                                                    <label>Enter Specimen ID : &nbsp;</label>
                                                    <asp:TextBox ID="txtSpecimenID" runat="server" CssClass="form-control w-75 numeric"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Select Status : &nbsp;</label>
                                                    <asp:DropDownList ID="drpstatus" runat="server" AutoPostBack="false" class="form-control w-75 drpControl">
                                                        <asp:ListItem Value="All" Text="All"></asp:ListItem>
                                                        <asp:ListItem Value="Acknowledged" Text="Acknowledged"></asp:ListItem>
                                                        <asp:ListItem Value="Pending" Text="Pending"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4 align-content-center">
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
                                                    <asp:GridView ID="grd_Data" runat="server" AutoGenerateColumns="true"
                                                        CssClass="table table-bordered table-striped Datatable" Width="100%" OnPreRender="grd_Data_PreRender" OnRowDataBound="grd_Data_RowDataBound">
                                                        <columns>
                                                            <asp:TemplateField HeaderStyle-CssClass="align-left">
                                                                <headertemplate>
                                                                    <label>Entered Details</label><br />
                                                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Entered By]</label><br />
                                                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                                </headertemplate>
                                                                <itemtemplate>
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
                                                                </itemtemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="align-left">
                                                                <headertemplate>
                                                                    <label>Shipment Details</label><br />
                                                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Generated By]</label><br />
                                                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                                </headertemplate>
                                                                <itemtemplate>
                                                                    <div>
                                                                        <div>
                                                                            <asp:Label ID="GENERATEDBYNAME" runat="server" Text='<%#Bind("GENERATEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                                        </div>
                                                                        <div>
                                                                            <asp:Label ID="GENERATED_CAL_DAT" runat="server" Text='<%#Bind("GENERATED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                                        </div>
                                                                        <div>
                                                                            <asp:Label ID="GENERATED_CAL_TZDAT" runat="server" Text='<%# Eval("GENERATED_CAL_TZDAT") + " " + Eval("GENERATED_TZVAL") %>' ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </itemtemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="align-left">
                                                                <headertemplate>
                                                                    <label>Acknowledgement Details</label><br />
                                                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Acknowledge By]</label><br />
                                                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                                </headertemplate>
                                                                <itemtemplate>
                                                                    <div>
                                                                        <div>
                                                                            <asp:Label ID="ACKNOWLEDGEMENTNAME" runat="server" Text='<%#Bind("ACKNOWLEDGEMENTNAME") %>' ForeColor="Blue"></asp:Label>
                                                                        </div>
                                                                        <div>
                                                                            <asp:Label ID="ACKNOWLEDGEMENT_CAL_DAT" runat="server" Text='<%#Bind("ACKNOWLEDGEMENT_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                                        </div>
                                                                        <div>
                                                                            <asp:Label ID="ACKNOWLEDGEMENT_CAL_TZDAT" runat="server" Text='<%# Eval("ACKNOWLEDGEMENT_CAL_TZDAT") + " " + Eval("ACKNOWLEDGEMENT_TZVAL") %>' ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </itemtemplate>
                                                            </asp:TemplateField>
                                                        </columns>
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
