<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DATA_ENTRY_l2.aspx.cs" Inherits="SpecimenTracking.DATA_ENTRY_l2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
    <asp:HiddenField runat="server" ID="hdnEditMode" Value="false" />
    <asp:HiddenField runat="server" ID="hdnEditID" />
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
                            <li class="breadcrumb-item"><a href="DataEntryDashboard.aspx">Data Entry</a></li>
                            <li class="breadcrumb-item active">Data Entry L2</li>
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
                                            <label>Site ID:</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label ID="lblsite" runat="server" CssClass="form-control">
                                            </asp:Label>
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <div class="col-md-12 d-inline-flex">
                                        <div class="col-md-3">
                                            <label>Specimen Type:</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label ID="lblSpecimenType" runat="server" CssClass="form-control">
                                            </asp:Label>
                                        </div>
                                    </div>
                                    <div runat="server" id="divSID" visible="false">
                                        <br />
                                        <div class="col-md-12 d-inline-flex">
                                            <div class="col-md-3">
                                                <label>Specimen ID:</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label ID="lblSpecimen" runat="server" CssClass="form-control">
                                                </asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div runat="server" id="divSubject" visible="false">
                                        <br />
                                        <div class="col-md-12 d-inline-flex">
                                            <div class="col-md-3">
                                                <label>Subject ID:</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label ID="lblSubject" runat="server" CssClass="form-control">
                                                </asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="col-md-12 d-inline-flex">
                                        <div class="col-md-3">
                                            <label>Visit:</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label ID="lblVisit" runat="server" CssClass="form-control">
                                            </asp:Label>
                                            <asp:HiddenField ID="hdnVISITNUM" runat="server"></asp:HiddenField>
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
                                                    <asp:HiddenField runat="server" ID="hdnINDEX" Value='<%# Container.ItemIndex %>'></asp:HiddenField>
                                                    <asp:HiddenField runat="server" ID="hdnOldDATA" />
                                                    <asp:TextBox ID="txtDATA" runat="server" CssClass="form-control" autocomplete="off" onchange="DATA_Changed(this);" Visible="false">
                                                    </asp:TextBox>
                                                    <asp:DropDownList ID="drpDATA" runat="server" CssClass="form-control" onchange="DATA_Changed(this);" Visible="false">
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
                                    <br />
                                    <div class="col-md-12 d-inline-flex">
                                        <div class="col-md-3">
                                            <label>Enter Comment:</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtComment" runat="server" CssClass="form-control" MaxLength="500" TextMode="MultiLine">
                                            </asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hdnOldComment" />
                                        </div>
                                    </div>
                                    <div runat="server" id="divAliquotPrep" visible="false">
                                        <br />
                                        <hr />
                                        <h3>Aliquot Preparation</h3>
                                        <br />
                                        <asp:HiddenField runat="server" ID="hdnALIQUOT_VERIFY" />
                                        <asp:GridView runat="server" ID="gridAliquots" OnRowDataBound="gridAliquots_RowDataBound" AutoGenerateColumns="false" CssClass="table table-bordered table-striped txt_center" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Aliquot ID" ItemStyle-Width="16%">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblALIQUOTID" Text='<%# Eval("ALIQUOTID") %>'></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hdnID" Value='<%# Eval("ID") %>'></asp:HiddenField>
                                                        <asp:HiddenField runat="server" ID="hdnINDEX" Value='<%# Container.DataItemIndex %>'></asp:HiddenField>
                                                        <asp:HiddenField runat="server" ID="hdnALIQUOTSEQFROM" Value='<%# Eval("ALIQUOTSEQFROM") %>'></asp:HiddenField>
                                                        <asp:HiddenField runat="server" ID="hdnALIQUOTSEQTO" Value='<%# Eval("ALIQUOTSEQTO") %>'></asp:HiddenField>
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
                                                        <asp:TextBox runat="server" ID="txtALIQUOTNO" CssClass="form-control txt_center" Text='<%# Eval("ALIQUOTNO") %>' AutoPostBack="true" OnTextChanged="txtALIQUOTNO_TextChanged"></asp:TextBox>
                                                        <asp:HiddenField runat="server" ID="hdnOldALIQUOTNO" Value='<%# Eval("ALIQUOTNO") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Box Number" ItemStyle-Width="16%">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtBOXNO" CssClass="form-control txt_center" Text='<%# Eval("BOXNO") %>' AutoPostBack="true" OnTextChanged="txtBOXNO_TextChanged"></asp:TextBox>
                                                        <asp:HiddenField runat="server" ID="hdnOldBOXNO" Value='<%# Eval("BOXNO") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Slot Number" ItemStyle-Width="16%">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtSLOTNO" CssClass="form-control txt_center" Text='<%# Eval("SLOTNO") %>' AutoPostBack="true" OnTextChanged="txtSLOTNO_TextChanged"></asp:TextBox>
                                                        <asp:HiddenField runat="server" ID="hdnOldSLOTNO" Value='<%# Eval("SLOTNO") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="0%">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td colspan="100%" style="padding: 2px; padding-left: 4%; background-color: white; text-align: left;">
                                                                <h6><u>Additional Fields</u>:</h6>
                                                                <div runat="server" id="divALIQUOT" style="position: relative; overflow: auto;">
                                                                    <div class="col-md-12 d-inline-flex">
                                                                        <asp:Repeater runat="server" ID="rptALIQUOT">
                                                                            <ItemTemplate>
                                                                                <div class="col-md-4 d-inline-flex">
                                                                                    <div class="col-md-4">
                                                                                        <label>
                                                                                            <asp:Label runat="server" ID="lblFIELDNAME" Text='<%# Eval("FIELDNAME") %>'></asp:Label>
                                                                                            :</label>
                                                                                    </div>
                                                                                    <div class="col-md-6">
                                                                                        <asp:HiddenField runat="server" ID="hdnINDEX" Value='<%# Container.ItemIndex %>'></asp:HiddenField>
                                                                                        <asp:HiddenField runat="server" ID="hdnOldDATA" />
                                                                                        <asp:TextBox ID="txtDATA" runat="server" CssClass="form-control" autocomplete="off" AutoPostBack="true" OnTextChanged="txtDATA_TextChanged">
                                                                                        </asp:TextBox>
                                                                                        <asp:HiddenField runat="server" ID="hdnVARIABLENAME" Value='<%# Eval("VARIABLENAME") %>' />
                                                                                    </div>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </div>
                                                                </div>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <hr />
                                    </div>
                                    <br />
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
                <asp:Label ID="openModalReason" runat="server" Text=""></asp:Label>
                <asp:Label ID="closeModalReason" runat="server" Text=""></asp:Label>
                <cc1:ModalPopupExtender ID="modalReason" runat="server" BehaviorID="mpe" PopupControlID="pnlReason" TargetControlID="openModalReason" BackgroundCssClass="">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="pnlReason" runat="server" Style="display: none;">
                    <div class="modal fade show">
                        <div class="modal-dialog modal-md">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="modal-title">Reason for Change</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <label>Field Name:</label>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:Label runat="server" ID="lblFieldName" Text="" CssClass="form-control"></asp:Label>
                                                <asp:HiddenField runat="server" ID="hdnReasonVARIABLENAME" />
                                                <asp:HiddenField runat="server" ID="hdnReasonINDEX" />
                                                <asp:HiddenField runat="server" ID="hdnReasonALQID" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-3">
                                                <label>Old Value:</label>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:Label runat="server" ID="lblOldVal" Text="" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-3">
                                                <label>New Value:</label>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:Label runat="server" ID="lblNewVal" Text="" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-3">
                                                <label>Entere Reason:</label>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox runat="server" ID="txtReason" CssClass="form-control required2" TextMode="MultiLine" MaxLength="200"></asp:TextBox>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12 txt_center">
                                                <asp:Button runat="server" ID="btnSubmitReason" Text="Submit" CssClass="btn btn-success cls-btnSave2" OnClick="btnSubmitReason_Click" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button runat="server" ID="btnCancelReason" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancelReason_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

