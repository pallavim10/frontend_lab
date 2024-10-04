<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RPT_ALIQUOT_DETAILS.aspx.cs" Inherits="SpecimenTracking.RPT_ALIQUOT_DETAILS" %>

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
                        <h1 class="m-0 text-dark">Aliquot Details</h1>
                    </div>
                    <!-- /.col -->
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home</a></li>
                            <li class="breadcrumb-item active">Aliquot Details</li>
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
                                <h3 class="card-title">Aliquot Details</h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="lbtnExport" runat="server" Font-Size="14px" Style="margin-top: 3px;" CssClass="btn btn-default btn-sm" OnClick="lbtnExport_Click" ForeColor="Black">Export Aliquot Details &nbsp;<span class="fas fa-download btn-sm"></span></asp:LinkButton>
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
                                                    <asp:DropDownList ID="drpSite" runat="server" AutoPostBack="true" class="form-control drpControl w-75  select2" SelectionMode="Single" OnSelectedIndexChanged="drpSite_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Select Subject ID : &nbsp;</label>
                                                    <asp:DropDownList ID="drpSubject" runat="server" AutoPostBack="false" class="form-control drpControl w-75  select2" SelectionMode="Single">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Specimen ID : &nbsp;</label>
                                                    <asp:TextBox ID="txtSpecimenID" runat="server" CssClass="form-control  numeric w-75"></asp:TextBox>
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
                                            <div style="width: 100%; height:700px; overflow: auto;">
                                                <div>
                                                    <asp:GridView ID="grd_Data" runat="server" AutoGenerateColumns="false"
                                                        CssClass="table table-bordered table-striped Datatable" Width="100%" OnPreRender="grd_Data_PreRender" OnRowDataBound="grd_Data_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Site ID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSITEID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Specimen ID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSUBJID" runat="server" Text='<%# Bind("SID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Subject ID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOption" runat="server" Text='<%# Bind("SUBJID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Visit Number">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVISITNUM" runat="server" Text='<%# Bind("VISITNUM") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Option">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVISIT" runat="server" Text='<%# Bind("VISIT") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Aliquot ID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblALIQUOTID" runat="server" Text='<%# Bind("ALIQUOTID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Aliquot Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblALIQUOTTYPE" runat="server" Text='<%# Bind("ALIQUOTTYPE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Aliquot Number">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAlqiNo" runat="server" Text='<%# Bind("ALIQUOTNO_CONCAT") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Box Number">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBOXNO" runat="server" Text='<%# Bind("BOXNO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="slot Number">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSLOTNO" runat="server" Text='<%# Bind("SLOTNO") %>'></asp:Label>
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
                                                            <asp:TemplateField HeaderStyle-CssClass="align-left">
                                                                <HeaderTemplate>
                                                                    <label>Last Modified Details</label><br />
                                                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Last Modified By]</label><br />
                                                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <div>
                                                                            <asp:Label ID="UPDATEDBYNAME" runat="server" Text='<%#Bind("UPDATEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                                        </div>
                                                                        <div>
                                                                            <asp:Label ID="UPDATED_CAL_DAT" runat="server" Text='<%#Bind("UPDATED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                                        </div>
                                                                        <div>
                                                                            <asp:Label ID="UPDATED_CAL_TZDAT" runat="server" Text='<%# Eval("UPDATED_CAL_TZDAT") + " " + Eval("UPDATED_TZVAL") %>' ForeColor="Red"></asp:Label>
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

