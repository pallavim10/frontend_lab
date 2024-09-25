<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DATA_ENTRY.aspx.cs" Inherits="SpecimenTracking.DATA_ENTRY" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <link href="Style/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <script src="Scripts/btnSave_Required.js"></script>
    <script src="CommonFunctionsJs/Data_Change.js"></script>
    <style type="text/css">
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #0000ff;
        }

        .select2-container--default .select2-selection--single {
            margin-top: 2px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">Data Entry</h1>
                    </div>
                    <!-- /.col -->
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home</a></li>
                            <li class="breadcrumb-item active">Data Entry</li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <section class="content">
                    <div class="container-fluid">
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Data Entry</h3>
                                <div class="pull-right">
                                    <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="form-group">
                                    <div class="col-md-12 d-inline-flex">
                                        <div class="col-md-3">
                                            <label>Enter Site ID:</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="drpsite" runat="server" CssClass="form-control required" AutoPostBack="true" OnSelectedIndexChanged="drpsite_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <div class="col-md-12 d-inline-flex">
                                        <div class="col-md-3">
                                            <label>Select Specimen Type:</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="drpSpecimenType" runat="server" CssClass="form-control required" AutoPostBack="true" OnSelectedIndexChanged="drpSpecimenType_SelectedIndexChanged" >
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <div class="col-md-12 d-inline-flex">
                                        <div class="col-md-3">
                                            <label>Enter Specimen ID:</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="drpSpecimen" runat="server" CssClass="form-control required select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="drpSpecimen_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <div class="col-md-12 d-inline-flex">
                                        <div class="col-md-3">
                                            <label>Select Subject ID:</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="drpSubject" runat="server" CssClass="form-control required select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="drpSubject_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <div class="col-md-12 d-inline-flex">
                                        <div class="col-md-3">
                                            <label>Select Visit:</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="drpVisit" runat="server" CssClass="form-control required" AutoPostBack="true" OnSelectedIndexChanged="drpVisit_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <br />
                                    <asp:Repeater runat="server" ID="repeatFields" OnItemDataBound="repeatFields_ItemDataBound">
                                        <ItemTemplate>
                                            <br />
                                            <div class="col-md-12 d-inline-flex">
                                                <div class="col-md-3">
                                                    <label>
                                                        <asp:Label runat="server" ID="lblFIELDNAME" Text='<%# Eval("FIELDNAME") %>'></asp:Label>
                                                        :</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtDATA" runat="server" CssClass="form-control required" onchange="DATA_Changed(this);" Visible="false">
                                                    </asp:TextBox>
                                                    <asp:DropDownList ID="drpDATA" runat="server" CssClass="form-control required" onchange="DATA_Changed(this);" Visible="false">
                                                    </asp:DropDownList>
                                                    <div runat="server" id="divRadChk" visible="false">
                                                        <div class="col-md-12 row d-inline-flex">
                                                            <asp:Repeater ID="repeatRadio" runat="server">
                                                                <ItemTemplate>
                                                                    <div class="col-md-4">
                                                                        <asp:RadioButton runat="server" ID="radioDATA" Text='<%# Eval("OPTION_VALUE") %>' onclick="RadioCheck(this); DATA_Changed(this);" />
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                            <asp:Repeater ID="repeatChk" runat="server">
                                                                <ItemTemplate>
                                                                    <div class="col-md-4">
                                                                        <asp:CheckBox runat="server" ID="chkDATA" Text='<%# Eval("OPTION_VALUE") %>' onclick="DATA_Changed(this);" />
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </div>
                                                        <br />
                                                    </div>
                                                    <asp:HiddenField runat="server" ID="hdnVARIABLENAME" Value='<%# Eval("VARIABLENAME") %>' />
                                                    <asp:HiddenField runat="server" ID="hdnCONTROLTYPE" Value='<%# Eval("CONTROLTYPE") %>' />
                                                    <asp:Button runat="server" ID="btnDATA_Changed" CssClass="d-none btnDATA_Changed" OnClick="btnDATA_Changed_Click"></asp:Button>
                                                </div>
                                            </div>
                                            <br />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <div runat="server" id="divAliquotPrep" visible="false">
                                        <br />
                                        <hr />
                                        <h3>Aliquot Preparation</h3>
                                        <br />
                                        <asp:GridView runat="server" ID="gridAliquots" AutoGenerateColumns="false" CssClass="table table-bordered table-striped txt_center" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Aliquot ID" ItemStyle-Width="16%">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblALIQUOTID" Text='<%# Eval("ALIQUOTID") %>'></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hdnID" Value='<%# Eval("ID") %>'></asp:HiddenField>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Aliquot Type" ItemStyle-Width="16%">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblALIQUOTTYPE" Text='<%# Eval("ALIQUOTTYPE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Aliquot No." ItemStyle-Width="16%">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblALIQUOTNO_CONCAT" Text='<%# Eval("ALIQUOTNO_CONCAT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Aliquot No." ItemStyle-Width="16%">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtALIQUOTNO" CssClass="form-control txt_center"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Box Number" ItemStyle-Width="16%">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtBOXNO" CssClass="form-control txt_center"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Slot Number" ItemStyle-Width="16%">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtSLOTNO" CssClass="form-control txt_center"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <hr />
                                    </div>
                                    <br />
                                    <div class="form-group">
                                        <div class="col-md-12 row txt_center">
                                            <div class="col-md-3">
                                                &nbsp;
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Button runat="server" ID="btnSubmit" Text="Submit" CssClass="btn btn-success cls-btnSave" OnClick="btnSubmit_Click" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button runat="server" ID="btnCancel" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
                                            </div>
                                            <div class="col-md-3">
                                                &nbsp;
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
